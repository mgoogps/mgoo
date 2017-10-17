using MG_BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace MG_WeChat.ajax
{
    /// <summary>
    /// Common 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class Common : System.Web.Services.WebService
    {

        public MG_BLL.Common.AuthHeader myHeader = new AuthHeader();

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据经纬度获取地址")]
        public string GetAddressByLatlng(decimal lat, decimal lng)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            return "";
        }
    }
}
