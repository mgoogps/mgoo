using MgooGps.com;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps.Statistics
{
    public partial class DevicesHireExpire : System.Web.UI.Page
    {
        public int day;
        public string type ="";
        protected void Page_Load(object sender, EventArgs e)
        {
            Utils.isLogin();
            if (Request.QueryString["day"]!= null)
            {
                try
                {
                    day = int.Parse(Request.QueryString["day"]);
                    if ( !string.IsNullOrWhiteSpace( Request.QueryString["t"]))
                    {
                        type = Request.QueryString["t"];
                    }
                  
                }
                catch (Exception)
                { 

                }
               
            }
        }
    }
}