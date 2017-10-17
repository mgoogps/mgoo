using MG_BLL;
using MG_BLL.Common;
using MG_BLL.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace MG_WeChat.ajax
{
    /// <summary>
    /// UsersAjax 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class UsersAjax : System.Web.Services.WebService
    {
        public MG_BLL.Common.AuthHeader myHeader = new AuthHeader();

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据用户id获取用户详细信息.")]
        public string GetUserInfoByID(string userid)
        {
            string valid = myHeader.isValid(userid);
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Users users = new Users(myHeader);
           return users.GetUsersInfoByID(userid);
        }
        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据用户id修改用户信息.")]
        public string UpdateUserInfoByID(string userid, string firstname, string callphone, string primaryemail, string address)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Users u = new Users(myHeader);
            return u.UpdateUsersInfoByID(firstname, callphone, primaryemail, address, userid);
        }
        [SoapHeader("myHeader")]
        [WebMethod(Description = "修改密码.")]
        public string UpdatePassword(string userid, string oldpwd, string newpwd)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Users u = new Users(myHeader);
            return u.UpdatePassword(userid, oldpwd, newpwd);
        }
        [SoapHeader("myHeader")]
        [WebMethod(Description = "获取微信config参数.")]
        public string GetWxConfig()
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            string url = HttpContext.Current.Request.UrlReferrer.OriginalString;
            Utils.log(url);
            WeixinOper wo = new WeixinOper();
            Dictionary<string, string> config = new Dictionary<string, string>();
            config["appId"] = wo.wx_appid;
            config["timestamp"] = wo.getTimestamp();
            config["nonceStr"] = wo.getNoncestr();
            config["signature"] = wo.Getsignature(config["nonceStr"], config["timestamp"],url);
            return Utils.ToJson(config);
        }
    }
}
