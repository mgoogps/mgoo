using MG_BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using MG_BLL;

namespace MG_WeChat.ajax
{
    /// <summary>
    /// Orders 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Orders : System.Web.Services.WebService
    {
        public AuthHeader myHeader = new AuthHeader();
        [SoapHeader("myHeader")]
        [WebMethod]
        public string GetOrderList(int userid,string  orderno)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            MG_BLL.Pay.MgooOrders.Orders o = new MG_BLL.Pay.MgooOrders.Orders(myHeader);
            return Utils.ToJson(o.GetOrderList(userid, orderno));
        }

        [SoapHeader("myHeader")]
        [WebMethod]
        public string GetTariffList(string model)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            MG_BLL.Pay.MgooOrders.Orders o = new MG_BLL.Pay.MgooOrders.Orders(myHeader);
            return Utils.ToJson(o.GetPriceList(model));
        }

        [SoapHeader("myHeader")]
        [WebMethod]
        public string AddOrders(string deviceid,string tariffid,string tradetype)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid; 
            }
            MG_BLL.Pay.MgooOrders.Orders o = new MG_BLL.Pay.MgooOrders.Orders(myHeader);
           // o.AddOrder(myHeader.UserID,deviceid,"","",);
            return "Hello World";
        }
        [SoapHeader("myHeader")]
        [WebMethod]
        public string QueryOrder(string orderno,string transactionid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            MG_BLL.Pay.MgooOrders.Orders o = new MG_BLL.Pay.MgooOrders.Orders();
            var orderStatus = o.QueryOrder(orderno,transactionid);
            if (orderStatus == "2")
            {
                return MG_BLL.Utils.GetResult("SUCCESS", MG_BLL.statusCode.Code.success);
            }
            return MG_BLL.Utils.GetResult("FAILURE", MG_BLL.statusCode.Code.failure); 
        }
    }
}
