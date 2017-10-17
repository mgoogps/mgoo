using MgooGps.com;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class main : MG_BLL.Common.UIPage
    { 
        public string imei = "";
        public string userid = "";
        public string searchText = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Utils.logoutUrl = "";
            com.Utils.isLogin();
            
            if (Utils.logoutUrl == "")
            {
                try
                {
                    Utils.logoutUrl = Request.UrlReferrer.AbsoluteUri;
                }
                catch (Exception )
                {
                     
                } 
            }
           
            //Utils.log("设置logoutUrl：" + Utils.logoutUrl);
            com.Utils.isPlayAudio = com.MyTeam.GetIsPlay();
            com.Utils.LowerMsg = com.MyTeam.GetLowerMsg(); 
            if (!string.IsNullOrWhiteSpace(Request.QueryString["imei"]))
            {
                  imei=  Request.QueryString["imei"];
            }
            if (!string.IsNullOrWhiteSpace(Request.QueryString["userid"]))
            {
                userid = Request.QueryString["userid"];
            }
            if (!string.IsNullOrWhiteSpace(Request.QueryString["search"]))
            {
                searchText=  Request.QueryString["search"];
            }
        
        } 
       
    }
}