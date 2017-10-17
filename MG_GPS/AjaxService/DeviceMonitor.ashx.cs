using MG_BLL;
using MG_BLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MG_GPS.AjaxService
{
    /// <summary>
    /// DeviceMonitor 的摘要说明
    /// </summary>
    public class DeviceMonitor : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string action = context.Request.QueryString["action"];
            switch (action)
            {
                case "GetDevicesListByUserID":
                    MG_BLL.BllMonitor mo = new MG_BLL.BllMonitor();
                    string UserID = context.Request.Params["UserID"];
                    string msgCount = default(string);
                    List<Devices> json = mo.GetDevicesByUserID_Bll(UserID,ref msgCount);

                    // return "{\"aa\":\"11\"}";
                    // return Utils.GetSession() as LoginUserInfo;
                    context.Response.Write(Utils.ToJson(json)); 
                    break;
                case "GetDeviceInfo":
                    string Imei = context.Request.Params["Imei"];
                    BllMonitor bmo = new BllMonitor();
          
                    context.Response.Write(bmo.GetDeviceInfo(Imei));
                    break;
                default:
                    break;
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}