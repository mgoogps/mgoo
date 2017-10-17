using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;

namespace MgooGps
{
    public partial class PlayBack : System.Web.UI.Page
    {
        public int DeviceID = 0;
        public String date = "", DeviceName = "", IMEI = "", SpeedLimit = "";
        public string modal { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            com.Utils.isLogin();
            date = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00" + "到" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:50";
            if (Request.QueryString["deviceid"] != null && Request.QueryString["deviceid"] != "" && Request.QueryString["id"]!=null && Request.QueryString["id"] != "")
            {
                try
                {
                    DeviceID = int.Parse(Request.QueryString["deviceid"]);
                    int UserID = int.Parse(Request.QueryString["id"]);
                    Hashtable table = com.MyTeam.GetDeviceNumber(com.Utils.GetSession("UserInfo").UserID);
                    string[] userids = table["userids"].ToString().Split(',');
                    if (!userids.Contains(UserID.ToString()))
                    {
                        Response.Write("<script>alert('参数错误！!')</script>");
                        Response.Redirect("~/main.aspx");
                    }
                    string strSql = @"select SerialNumber,DeviceName,SpeedLimit,di.datatext from Devices d inner join dictionary di on di.DataValue=d.Model
                                      where UserID = @UserID and d.DeviceID = @DeviceID";
                    table = com.Dao.Select(strSql,new SqlParameter[] {new SqlParameter("UserID",UserID),new SqlParameter("DeviceID", DeviceID) });
                    if (table != null)
                    {
                        IMEI = table["SerialNumber"].ToString();
                        DeviceName = table["DeviceName"].ToString() == "" ? IMEI : table["DeviceName"].ToString();
                        SpeedLimit = table["SpeedLimit"].ToString();
                        modal = table["datatext"].ToString().StartsWith("MG-X83") ? "'X83'" : "''";
                    }
                    else
                    {
                        Response.Write("<script>alert('参数错误！!')</script>");
                    } 
                }
                catch (Exception)
                {

                }
            }
            else
            {
                Response.Write("<script>alert('参数错误！')</script>");
            }
        }
    }
}