using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MG_WeChat.pay
{
    public partial class Recharge : System.Web.UI.Page
    {
        public List<Dictionary<string, string>> PriceList = new List<Dictionary<string, string>>();
        /// <summary>
        /// IMEI号
        /// </summary>
        public string imei { set; get; }
        public string device_name { set; get; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public string expire_date { set; get; }
        /// <summary>
        /// 使用情况
        /// </summary>
        public int use_situation { set; get; } 
        public int user_id { set; get; }
        public string user_name { set; get; }

        public string device_count { set; get; }
        public string expire_count { set; get; }
        public int device_id { set; get; }
        public string open_id { set; get; }
        /// <summary>
        /// 余下多少个月
        /// </summary>
        public int the_rest;  

        protected void Page_Load(object sender, EventArgs e)
        {
            string deviceid = Request.QueryString["deviceid"];
            open_id = Request.QueryString["openid"];
            // imei = Request.QueryString["imei"];
            if (!string.IsNullOrEmpty(deviceid) && !string.IsNullOrEmpty(open_id))
            {
                try
                {
                    device_id = Convert.ToInt32(deviceid);
                    MG_DAL.SQLServerOperating s = new MG_DAL.SQLServerOperating();
                    string strSql = @"select DeviceID,SerialNumber,DeviceName,PhoneNum,ActiveDate,HireExpireDate,u.UserID,u.UserName,di.DataText,cast((datediff(DAY,ActiveDate,GETDATE()) + 0.1)/(datediff(DAY,ActiveDate,HireExpireDate)+0.1) as  numeric(10,2)) as UseSituation, datediff(MM,GETDATE(),HireExpireDate) TheRest,
                                 (select count(*) from devices where Deleted=0 and UserID=d.UserID) DeviceCount,
                                 (select count(*) from devices where Deleted=0 and UserID=d.UserID and HireExpireDate > GETDATE() and HireExpireDate <= DATEADD(MM,1,GETDATE())) ExpireCount
                                  from devices d inner join Users u on u.UserID=d.UserID inner join dictionary di on di.DataValue = d.Model where d.Deleted=0 and DeviceID=@DeviceID";
                    DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("DeviceID", device_id) });
                    //X11 8 50   开头的 都是60一年，其他都是120一年    60 100 130               120 200 250 
                    if (dt.Rows.Count <= 0)
                    {
                        return;
                    }
                    DataRow DeviceInfo = dt.Rows[0];
                    expire_date = Convert.ToDateTime(DeviceInfo["HireExpireDate"].ToString()).ToString("yyyy-MM-dd");
                    use_situation = Convert.ToInt32(Convert.ToDouble(DeviceInfo["UseSituation"].ToString()) * 100);
                    user_name = DeviceInfo["UserName"].ToString();
                    imei = DeviceInfo["SerialNumber"].ToString();
                    device_count = DeviceInfo["DeviceCount"].ToString();
                    expire_count = DeviceInfo["ExpireCount"].ToString();
                    device_name = string.IsNullOrEmpty(DeviceInfo["DeviceName"].ToString()) ? DeviceInfo["SerialNumber"].ToString() : DeviceInfo["DeviceName"].ToString();
                    user_id = Convert.ToInt32(DeviceInfo["UserID"].ToString());
                    int.TryParse(DeviceInfo["TheRest"].ToString(),out the_rest);
                    the_rest = the_rest - 1;
                    MG_BLL.Pay.MgooOrders.Orders o = new MG_BLL.Pay.MgooOrders.Orders();
                    PriceList = o.GetPriceList(DeviceInfo["DataText"].ToString());
                }
                catch (Exception)
                {
                     
                }
            }
        }
       
    }
}