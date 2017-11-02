using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class PlayBack : System.Web.UI.Page
    {
        public int DeviceID = 0;
        public String date = "", DeviceName = "", IMEI = "", SpeedLimit = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd ") + " 00:00" + "到" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:50";
            if (Request.QueryString["deviceid"] != null && Request.QueryString["deviceid"] != "" && Request.QueryString["id"]!=null && Request.QueryString["id"] != "")
            {
                try
                {
                    DeviceID = int.Parse(Request.QueryString["deviceid"]); 
                    DataRow dr = com.Dao.Select("select SerialNumber,DeviceName,SpeedLimit from Devices where DeviceID="+DeviceID ); 
                    IMEI = dr["SerialNumber"].ToString();
                    DeviceName = dr["DeviceName"].ToString() == "" ? IMEI : dr["DeviceName"].ToString();
                    SpeedLimit = dr["SpeedLimit"].ToString();
                }
                catch (Exception)
                {

                }

            }
        }
    }
}