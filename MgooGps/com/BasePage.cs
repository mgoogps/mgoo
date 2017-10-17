using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MgooGps.com
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        { 
            if (HttpContext.Current.Session["UserInfo"] == null)
            {
                HttpContext.Current.Response.Redirect("~/login.aspx");
            }
        }
    }
}