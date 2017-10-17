using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class AnalyticStatistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            com.Utils.isLogin();
            com.Utils.isPlayAudio = com.MyTeam.GetIsPlay();
            com.Utils.LowerMsg = com.MyTeam.GetLowerMsg();
        }
    }
}