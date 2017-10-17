using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MG_BLL;
using System.Data;
using System.Web.Script.Services;
using MG_BLL.Entity;

namespace MG_GPS.AjaxService
{
    /// <summary>
    /// Monitor 的摘要说明
    /// </summary>
    [WebService(Namespace = "")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class  Monitor : System.Web.Services.WebService
    { 



        [WebMethod(EnableSession = true)]
        public String GetzTree(String UserType = null)
        {
            //MG_BLL.BllMonitor zTree = new MG_BLL.BllMonitor();
            return null; //zTree.GetUserZTree(UserType);
        }

        /// <summary>
        /// 根据用户ID获取设备列表，包括该用户的报警消息的条数
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        //[SoapHeader("SoapHeaderValue")]
        public string GetDevicesListByUserID(String UserID)
        { 
            MG_BLL.BllMonitor mo = new MG_BLL.BllMonitor();
            string msgCount = default(string);
            List<Devices> json = mo.GetDevicesByUserID_Bll (UserID,ref msgCount); 
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("msgCount", msgCount); dic.Add("data",  json );
            return Utils.ToJson(dic);  
        }
        /// <summary>
        /// 获取设备的实时信息
        /// </summary>
        /// <param name="Imei"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)] 
        public string GetDeviceData(string Imei)
        {
            BllMonitor bmo = new BllMonitor();
            return bmo.GetDeviceData(Imei);
        }

        /// <summary>
        /// 获取报警消息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        public string GetAlarms(string UserID)
        {
            BllMonitor bm = new BllMonitor();
            List<Alarms> list = bm.GetAlarms(UserID);
            return Utils.ToJson(list);
        }
        /// <summary>
        /// 设置消息为已读
        /// </summary>
        /// <param name="ObjectID">消息ID</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        public string DeleteMsg(string ObjectID)
        {
            BllMonitor bm = new BllMonitor();
           return bm.DeleteMsg(ObjectID);
        }
        /// <summary>
        /// 获取实时跟踪数据 
        /// </summary>
        /// <param name="DeviceID">设备ID</param>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        public string Tracking(string DeviceID, string UserID)
        {
            BllMonitor bm = new BllMonitor();
            return bm.GetDeviceData(DeviceID); 
        }

        /// <summary>
        /// 获取历史轨迹数据
        /// </summary> 
        /// <param name="ID">设备ID</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        public string GetHistory(string ID,string StartTime,string EndTime)
        {
            try
            {
                BllMonitor bm = new BllMonitor();
                return Utils.ToJson(bm.GetHistory(ID, StartTime, EndTime), true);
            }
            catch (Exception)
            { 
                throw new Exception("查询历史轨迹数据出错！");
            } 
        }
        /// <summary>
        /// 获取中文地址
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="mapType">地图类型</param>
        /// <param name="language">语言</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        public string GetAddress(string lat,string lon,string mapType,string language)
        {
            BllMonitor bm = new BllMonitor(); 
            return bm.bllGetAddress(Convert.ToDouble(lat), Convert.ToDouble(lon), mapType, language); 
        }
        /// <summary>
        /// 根据IMEI号获取该设备的信息
        /// </summary>
        /// <param name="Imei"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        public string GetDeviceInfo(string Imei)
        {
            BllMonitor bm = new BllMonitor(); 
            return bm.GetDeviceInfo(Imei);
        }
        /// <summary>
        /// 修改设备信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="CarNumber"></param>
        /// <param name="Contact"></param>
        /// <param name="CellPhone"></param>
        /// <param name="Image"></param>
        /// <param name="IsLbs"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        public string UpdateDeviceInfo(string ID,string Name,string CarNumber,string Contact,string CellPhone,string Image,string IsLbs)
        { 
            return new BllMonitor().SetDeviceInfo(ID, Name, CarNumber, Contact, CellPhone, Image, IsLbs); 
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        public string GetUserInfo()
        { 
            return new BllMonitor().GetUserInfo();
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="User"></param>
        /// <param name="UserName"></param>
        /// <param name="eMail"></param>
        /// <param name="Address"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        public string UpdateUserInfo(string User,string UserName,string eMail,string Address)
        {
            return new BllMonitor().SetUserInfo(User,UserName,eMail,Address);
        }

        /// <summary>
        /// 获取设备发送时保存的命令状态
        /// </summary>
        /// <param name="deviceid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        public string GetCommandConfig(string deviceid,string userid)
        {
            XmlOperate xml = new XmlOperate(deviceid, userid); 
            return xml.GetCommandXmlNode();
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false)]
        public string AddGroups(string id, string parent, string username, string password, string cellphone, string address, string path, string email, string weixin)
        {
            BllMonitor bm = new BllMonitor();
            return bm.AddGroups(id,parent,username,password,cellphone,address,path,email,weixin);
        }
    }
}
