using MgooGps.com;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps.Statistics
{
    public partial class RenewalsExport : System.Web.UI.Page
    {

        public String date = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            com.Utils.isLogin();
            DateTime d = DateTime.Now;
            string LastMonth_firstDay = d.AddDays(1 - d.Day).ToString("yyyy-MM-dd");
            string LastMonth_lastDay = d.AddDays(1 - d.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            date = d.ToString("yyyy-MM-01") + " ~ " + d.AddDays(1- d.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
       
       
            if (Utils.GetSession("UserInfo").SuperAdmin != "1")
            {
                Response.Redirect("Statistics.aspx");
            }

        }
    }
}