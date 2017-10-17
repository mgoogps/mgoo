using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class ModalShowOfflineDevice : System.Web.UI.Page
    {
        public String date = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            com.Utils.isLogin();
            date = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd") + " 到 " + DateTime.Now.ToString("yyyy-MM-dd");

        }
    }
}