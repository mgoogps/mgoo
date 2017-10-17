using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.Common
{
    public class UIPage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
             
            Common.lib.Config.CurrentHost = Request.Url.Host;
            //if (host.Equals("localhost"))
            //{
            //    Common.lib.Config.CurrentHostWelcomeMessage = "本地测试";
            //}
            //else if (host.Equals("aobo.mgoogps.com"))
            //{
            //    Common.lib.Config.CurrentHostWelcomeMessage = "澳博车贷GPS管理平台";
            //} 
            //else
            //{
            //    Common.lib.Config.CurrentHostWelcomeMessage = "GPS物联网监控平台";
            //}
            //Common.lib.Config.CurrentHostLogo = host+".png";
        }
    }
}
