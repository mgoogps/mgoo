using MG_BLL;
using MG_BLL.DeviceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace MG_GPS.AjaxService
{
    /// <summary>
    /// DeviceManager 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
     [System.Web.Script.Services.ScriptService]
    public class DeviceManager : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession =true)]
        public string GetDeviceList(string user,string devicename)
        {
            BllDeviceManager bdm = new BllDeviceManager();

            return bdm.GetDeviceList(user,devicename.Trim());
        }

        [WebMethod(EnableSession = true)]
        /// <summary>
        /// 统计设备数量
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetDeviceCount(string user)
        {
            BllDeviceManager bdm = new BllDeviceManager(); 
            return Utils.ToJson(bdm.GetDeviceCount(user));
        }

        [WebMethod(EnableSession = true)]
        /// <summary>
        /// 统计所有用户数量
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string GetALLUser(string user) {

            BllDeviceManager bdm = new BllDeviceManager();
            return bdm.GetALLUser(user) +"";
        }

        [WebMethod(EnableSession = true)] 
        [ScriptMethod(UseHttpGet = false)] 
        public string SendCommand(string command,string userid, string id, string phone,string mode, string sens)
        {
            MG_BLL.DeviceManager.CommandOper co = new CommandOper();
            //string CommandType, string Id, string Phone, string mode = null, string sens = null
            if (command == "testty")
            {
                return co.SendCommand(id);
            }
            else
            {
                return co.SendCommand(command, userid, id, phone == "" ? null : phone, mode == "" ? null : mode, sens == "" ? null : sens);
            }
        }

      
    }
}
