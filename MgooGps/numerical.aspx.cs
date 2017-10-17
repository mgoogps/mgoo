using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class numerical : System.Web.UI.Page
    {
        public DataTable dt = new DataTable();
        public String type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            com.Utils.isLogin();
            com.Utils.isPlayAudio = com.MyTeam.GetIsPlay();
            com.Utils.LowerMsg = com.MyTeam.GetLowerMsg();
           // dt =  com.MyTeam.getAlarmList(com.Utils.GetSession("UserInfo").UserID);

            if (!string.IsNullOrWhiteSpace(Request.QueryString["type"]) && Request.QueryString["type"] == "1")
            {
                type = "AlarmMessage.aspx";
            }
            else
            {
                type = "AlarmMessage.aspx"; 
            }
            if (com.Utils.GetSession("UserInfo").LoginType == "1")
            {
                type = "RemainView.aspx";
            }
          
        }
    }
}