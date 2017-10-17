using MG_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using MG_BLL.Entity;
using MG_BLL.Mileage;
using System.Data;
namespace MG_GPS.AjaxService
{
    /// <summary>
    /// Statistics 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
   // [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
     [System.Web.Script.Services.ScriptService]
    public class Statistics : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string GetUsers()
        {
            BllMonitor mo = new BllMonitor();
            List<Dictionary<string, string>> list = mo.GetUserZTree();
            for (int i = 0; i < list.Count; i++)
            {
                Dictionary<string, string> dic = list[i];
                
            }
            StringBuilder sbStr = new StringBuilder();
            sbStr.Append("[");
            foreach (Dictionary<string, string> item in list)
            {
                sbStr.Append("{id:\"" + item["_id"] + "\",pId:\"" + (item["parent"] == "" ? "0" : item["parent"]) + "\",name:\"" + (item["username"] == "" ? item["_id"] : item["username"]) + "\"},");
            }
            sbStr.Remove(sbStr.Length - 1, 1);
            sbStr.Append("]");
            //  [{ id:"captain",  pId:"0",      name:"东莞美谷电子科技"},
            //   { id:"test",     pId:"captain",name:"测试账号"},
            //   { id:"dongguan", pId:"test",   name:"东莞3"},
            //   { id:"jupiter",  pId:"captain",name:"jupiter"}]
         string json =   Utils.ToJson(list);
            return json;
        }


        /// <summary>
        /// 获取指定用户下的设备(名称，ID)
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
         [WebMethod(EnableSession = true)]
        public string GetDevicesName(string UserID)
        {
            MG_BLL.BllMonitor mo = new MG_BLL.BllMonitor();
            string msgCount = default(string);
            List<Devices> json = mo.GetDevicesByUserID_Bll(UserID, ref msgCount);
          
            return Utils.ToJson(json);
          
        }

        [WebMethod(EnableSession = true)]
        public string GetMileage(string DeviceID , string StartTime, string EndTime)
        {
            return new BllMileage().GetMileage(DeviceID, StartTime, EndTime); 
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="file"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        //[WebMethod(EnableSession = true)]
        //public string GetDerivedData() {

        //    return Utils.DataSetToExcel();
        //}

        /// <summary>
        /// 获取设备停留详单
        /// </summary>
        /// <param name="DeviceID">设备ＩＤ</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public string GetStopDetail(string DeviceID, string StartTime, string EndTime)
        {
            return new BllMileage().GetStopDetail(DeviceID, StartTime, EndTime);
        }
        [WebMethod(EnableSession = true)]
        public string GetEcharts(string DeviceID,string Time)
        { 
            return new BllMileage().GetEcharts(DeviceID,Time);
        }

        [WebMethod(EnableSession=true)]
        public string GetDevicesCount()
        {
            return "";
        }
        [WebMethod(EnableSession = true)]
        public string GetAlarmPreview(string DeviceID,string StartTime,string EndTime)
        {
          return  new BllMileage().GetAlarmPreview(DeviceID,StartTime,EndTime);
          
        }
        [WebMethod(EnableSession = true)]
        public string GetAlarmDetail(string DeviceID, string StartTime, string EndTime)
        {
            return new BllMileage().GetAlarmDetail(DeviceID, StartTime, EndTime);
        }
        [WebMethod(EnableSession = true)]
        public string GetGenFencesCount(string DeviceID, string StartTime, string EndTime)
        {
            return new BllMileage().GetGenFencesCount(DeviceID, StartTime, EndTime);
        }
       
        [WebMethod(EnableSession = true)]
        public string GetExecl(string DeviceID, string StartTime, string EndTime, string OilWear) {

            return  new BllMileage().GetOutExecel(DeviceID,StartTime,EndTime,OilWear);// new BllMileage()
        }

    }
}
