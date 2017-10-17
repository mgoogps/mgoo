using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MG_GPS.Monitor
{
    public partial class History : MG_BLL.BasePage
    {

        public String date = "", DeviceName = "", IMEI = "", SpeedLimit = "",UserID="";
        protected void Page_Load(object sender, EventArgs e)
        { 
            date = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00" + "到" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:50";
            if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]) && !string.IsNullOrWhiteSpace(Request.QueryString["deviceid"]))
            {
                IMEI = Request.QueryString["deviceid"];
                UserID = Request.QueryString["id"];
                DeviceName = Request.QueryString["name"];
            }
        }
    }
}