using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class AllDevices : System.Web.UI.Page
    {
        public DataTable dt = new DataTable();
        public DataTable UserTable = new DataTable();
        public DataTable  DictionaryTable  = new DataTable();
        public String zTreeSelectUserID;
        public DataRow DeviceInfoRow = null;
        public string imei = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            com.Utils.isLogin();
            if (Request.QueryString["u"]!= null && Request.QueryString["u"]!="")
            {
                try
                {
                    zTreeSelectUserID = Request.QueryString["u"];
                    if (com.Utils.strImeiBatch != "")
                    {
                        dt = com.MyTeam.GetDevicesListReturnDataTable(null, com.Utils.strImeiBatch);
                        com.Utils.strImeiBatch = "";
                    }
                    else
                        dt = com.MyTeam.GetDevicesListReturnDataTable(zTreeSelectUserID);
                 
                    UserTable = com.MyTeam.GetUserInfoChild(zTreeSelectUserID);
                    DictionaryTable = com.MyTeam.GetDictionaryList();
                    DeviceInfoRow = com.MyTeam.GetUserDeviceInfo(zTreeSelectUserID);

                    if (!string.IsNullOrWhiteSpace(Request.QueryString["imei"]))
                    {
                        imei = Request.QueryString["imei"];
                    }
                }
                catch (Exception)
                { 
                }
            
            }
         
        }
    }
}