using MG_BLL;
using MG_BLL.Common;
using MG_BLL.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace MG_WeChat.ajax
{
    /// <summary>
    /// MessageAjax 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class MessageAjax : System.Web.Services.WebService
    {
        public AuthHeader myHeader = new AuthHeader();

        [SoapHeader("myHeader")]
        [WebMethod(Description = "获取报警消息列表")]
        public string GetMessageList(string currentindex, string pagecount, string userid,string type)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Message m = new Message(myHeader);
            return Utils.ToJson(m.GetMessageList(currentindex, pagecount, userid,type));
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "清除报警消息")]
        public string DeleteMessage(string userid, string exceptionid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Message m = new Message(myHeader);
            return m.DeleteMessage(userid, exceptionid);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据id获取报警信息")]
        public string GetMessageInfoByID(string exceptionid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Message m = new Message(myHeader);
            return Utils.ToJson(m.GetMessageInfoByID(exceptionid));
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据设备ID查询该设备的报警信息")]
        public string GetMessageByDeviceID(string currentindex, string pagecount, string deviceid,string type)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Message m = new Message(myHeader);
            return Utils.ToJson(m.GetMessageByDeviceID(currentindex,pagecount,deviceid,type));
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "获取所有的报警消息类型")]
        public string GetMessageType()
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Message m = new Message(myHeader);
            return Utils.ToJson(m.GetExceptionType());
        }
        [SoapHeader("myHeader")]
        [WebMethod(Description = "获取希望推送的报警类型的id")]
        public string GetPushMsgByUserID(string userid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Message m = new Message(myHeader);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["MsgType"] =  m.GetPushMsgByUserID(userid);
            return Utils.ToJson(dic);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "设置希望推送的报警类型的id")]
        public string SetPushMsg(string msgtype)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Message m = new Message(myHeader);
            return m.SetPushMsgType(msgtype); 
        }
        [SoapHeader("myHeader")]
        [WebMethod(Description = "获取当前用户所有的报警消息类型")]
        public string GetMessageTypeList()
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Message m = new Message(myHeader);
            return Utils.ToJson(m.GetMessageTypeList());
        }
        [SoapHeader("myHeader")]
        [WebMethod(Description = "获取当前用户所有的报警消息类型")]
        public string GetUsersConfig()
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Message m = new Message(myHeader);
            return Utils.ToJson(m.GetUsersConfig());
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "设置当前用户所有的报警消息类型")]
        public string SetUsersConfig(string config)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Message m = new Message(myHeader);
            return m.SetUsersConfig(config);
        }
        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据设备id获取20秒内有没有产生报警消息")]
        public string GetMessageByDeviceIDIsExist(int deviceid,int second)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Message m = new Message(myHeader);
            return Utils.ToJson(m.GetMessageByDeviceID(deviceid, second));
        }
    }
}
