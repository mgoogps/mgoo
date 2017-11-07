using MG_BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MG_BLL.Common;
namespace MG_WeChat
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //初始化写日志的等级（默认是3）
            //0.不输出日志; 1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
            MG_BLL.Common.Log.LOG_LEVEL = Convert.ToInt32(ConfigurationManager.AppSettings["LOG_Level"]);

            try
            {
                //写LOG的路径 
                var path = ConfigurationManager.AppSettings["LOG_Path"].ToString();
                var curDate = DateTime.Now;
                path = path.Replace("yyyy", curDate.Year + "").Replace("mm", curDate.Month + "").Replace("dd", curDate.Day + "");
                if (System.IO.Path.IsPathRooted(path))
                {
                    //绝对路径
                    MG_BLL.Common.Log.LOG_PATH = path;
                }
                else
                {
                    //相对路径
                    MG_BLL.Common.Log.LOG_PATH = System.AppDomain.CurrentDomain.BaseDirectory + path;
                }
                if (!System.IO.Directory.Exists(MG_BLL.Common.Log.LOG_PATH))//如果日志目录不存在就创建
                {
                    System.IO.Directory.CreateDirectory(MG_BLL.Common.Log.LOG_PATH);
                }
            }
            catch (Exception)
            {
                MG_BLL.Common.Log.LOG_LEVEL = 0;
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception ex = Server.GetLastError();
                Utils.log("----------------" + ex.Message);
                Response.Redirect("device/DeviceList.html");
            }
            catch (Exception ex)
            {
                Utils.log("Application_Error Error:"+ex.Message);
            } 
            Server.ClearError();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}