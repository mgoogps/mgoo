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
    public partial class DeletedDevice : System.Web.UI.Page
    {
        public DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
             com.Utils.isLogin();
          
            if (Utils.GetSession("UserInfo").SuperAdmin != "1")
            {
                Response.Redirect("Statistics.aspx");
            }
						    
        }
    }
}