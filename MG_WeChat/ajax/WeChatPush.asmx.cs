using MG_BLL;
using MG_BLL.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using MG_BLL.App;

namespace MG_WeChat.ajax
{
    /// <summary>
    /// WeChatPush 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WeChatPush : System.Web.Services.WebService
    {
        [WebMethod]
        public string Pushed(string token, string userid, string message, string devicename, string date, string lat, string lng, string remark, string exceptionid)
        {
            try
            {
                WeixinOper wo = new WeixinOper(token);
                if (!wo.istoken())
                {
                    return Utils.GetResult("token错误!", statusCode.Code.failure);
                }
                return wo.StartPush(userid,exceptionid,devicename,message,date,  remark);
            }
            catch (Exception ex)
            {
                return Utils.GetResult(ex.Message, statusCode.Code.error);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="deviceid">设备ID</param>
        /// <param name="devicename">设备名称</param>
        /// <param name="expireddate">到期时间</param>
        /// <returns></returns>
      //  [WebMethod]
       // public void ExpiredPush(string token, string packagename, string appsecret, string clientid, string os, string deviceid, string devicename, string expireddate)
        //{
            //string PackageName, string AppSecret, string ClientID, string OS, string DeviceID, string DeviceName, string ExpiredDate
           // WeixinOper wo = new WeixinOper(token);
           // if (!wo.istoken())
            //{
            //    Utils.GetResult("token错误!", statusCode.Code.failure);
            //    return;

           // } 
           // AppPush ap = new AppPush();
            //Android APP推送
           // ap.ExpiredPush(packagename, appsecret, clientid, os, deviceid, devicename, expireddate);
      //  }
    }
}