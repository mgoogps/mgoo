using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class ParkingEcharts : System.Web.UI.Page
    {
        public String date = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            com.Utils.isLogin();
            date = DateTime.Now.ToString("yyyy-MM-dd") ;
        }
    }
}