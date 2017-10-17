using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class Geofences : System.Web.UI.Page
    {
        public DataTable dt = new DataTable();

        public DataTable DeviceDt = new DataTable();

        /// <summary>
        /// 是否有创建围栏的权限
        /// </summary>
        public bool isCreateFence { get; set; }
        public string UserID { set; get; }
        public string DeviceName { set; get; }
        public string DeviceID { set; get; }

        public bool isAobo { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            com.Utils.isLogin();
            // if (Request.QueryString["deviceid"] != null && Request.QueryString["deviceid"] != "" && Request.QueryString["id"] != null && Request.QueryString["id"] != "")
            // {
            try
            {
                DeviceID = Request.QueryString["deviceid"];
                UserID = Request.QueryString["id"];
                if (string.IsNullOrEmpty(UserID))
                {
                    UserID = com.Utils.GetSession("UserInfo").UserID;
                }
                else
                {
                    UserID = int.Parse(UserID).ToString();
                }
                isCreateFence = true;

                isAobo = MG_BLL.Common.lib.Permission.IsSMSNotice(UserID);
                if (isAobo)
                {
                    isCreateFence = false;
                }
              
                Hashtable table = com.MyTeam.GetDeviceNumber(com.Utils.GetSession("UserInfo").UserID);
                string[] userids = table["userids"].ToString().Split(',');
                if (!userids.Contains(UserID.ToString()))
                {
                    Response.Write("<script>alert('参数错误！!')</script>");
                    Response.Redirect("~/main.aspx");
                }
                if (!string.IsNullOrEmpty(DeviceID))
                { 
                    dt = com.MyTeam.getDeviceByDeviceID(DeviceID);
                    DeviceName = dt.Rows[0]["DeviceName"].ToString() == "" ? dt.Rows[0]["SerialNumber"].ToString() : dt.Rows[0]["DeviceName"].ToString();
                }

                string strSql = "select GeofenceID,FenceName,Latitude,Longitude,UserID,DeviceID,FenceType,Description,Created from geofence where Deleted=0 and DeviceID= -1 and UserID=@UserID";
                if (isAobo)
                {
                    strSql = "select GeofenceID,FenceName,Latitude,Longitude,UserID,BoundBindIds,DeviceID,FenceType,Description,Created,case when  FenceType=1 then '设备' else (select UserName from users u where  u.UserID=g.BoundBindIds) end objName from geofence g where Deleted=0 and DeviceID= -1 and (UserID=@UserID or convert(varchar(1000),BoundBindIds)=@UserID)";
                }
                
                dt = com.Dao.Selects(strSql,new SqlParameter[] {new SqlParameter("UserID",UserID) });
                strSql = string.Format(@"select d.DeviceID,case when DeviceName='' then d.SerialNumber else DeviceName end DeviceName,d.SerialNumber,l.OLat BaiduLat,l.OLng BaiduLng,datediff(MI,l.LastCommunication, getdate()) status,d.Icon,l.Course,di.SortOrder as offLineMi
                        from devices d inner join LKLocation l on l.deviceid=d.DeviceID inner join dictionary di on di.DataValue=d.Model where userid={0} and deleted=0", UserID); // and OLat!=-1 and OLng!=-1
                if (com.Utils.GetSession("UserInfo").LoginType == "1")
                {
                    strSql += " and d.SerialNumber='" + com.Utils.GetSession("UserInfo").SerialNumber + "'";
                }
                DeviceDt = com.Dao.Selects(strSql);
                double lng = 0, lat = 0;

                for (int i = 0; i < DeviceDt.Rows.Count; i++)
                {
                    try
                    {
                        com.EvilTransform.bd_encrypt(Convert.ToDouble(DeviceDt.Rows[i]["BaiduLat"]), Convert.ToDouble(DeviceDt.Rows[i]["BaiduLng"]), ref lat, ref lng);
                        DeviceDt.Rows[i]["BaiduLat"] = lat;
                        DeviceDt.Rows[i]["BaiduLng"] = lng;
                    }
                    catch (Exception ex)
                    {
                         
                    }
                  
                }
            }
            catch (Exception)
            {
            }
            //  }
        }
    }
}