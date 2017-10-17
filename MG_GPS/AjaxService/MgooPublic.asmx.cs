using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MG_GPS.AjaxService
{
    /// <summary>
    /// MgooPublic 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class MgooPublic : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public string GetAddress(string lat,string lon)
        {
            MG_BLL.BllMonitor bmo = new MG_BLL.BllMonitor();
           return bmo.bllGetAddress(double.Parse(lat), double.Parse(lon)); 
        }
    }
}
