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
    /// MgooPay 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class MgooPay : System.Web.Services.WebService
    {
        public AuthHeader myHeader = new AuthHeader();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="deviceid">设备id</param>
        /// <param name="openid">用户的openid</param>
        /// <param name="orderno"></param>
        /// <param name="transactionno">微信或支付宝的支付单号</param>
        /// <param name="feetype">货币类型(默认为CNY)</param>
        /// <param name="totalfee">总价</param>
        /// <param name="paydate">交易时间</param>
        /// <param name="tradetype"></param>
        /// <param name="orderdate">订单时间(必须跟微信支付或者支付宝支付的订单时间一致)</param>
        /// <returns></returns>
        [SoapHeader("myHeader")]
        [WebMethod]
        public string AddOrder(string userid,string deviceid,string openid,string transactionno, string feetype,string totalfee,string paydate, string tradetype,string orderdate)
        {
            //orderno
            return "";
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "微信支付")]
        public string WeixinPay(string userid, string deviceid, string money, string day)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            return "您选择了微信支付";
        }
        [WebMethod(Description = "支付宝支付")]
        [SoapHeader("myHeader")]
        public string AliPay()
        {
            return "您选择的是支付宝支付";
        }
    }
}
