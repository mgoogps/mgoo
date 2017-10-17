using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using MG_GPS;
using MG_BLL;
using System.Configuration;

namespace MG_GPS
{
    public class Global : HttpApplication 
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            // AuthConfig.RegisterOpenAuth();
            //RouteConfig.RegisterRoutes(RouteTable.Routes); 
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码 
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            MG_BLL.Utils.log("Global.asax --> Application_Error:" + ex.Message+ ",Source:" + ex.Source+ ",StackTrace:" + ex.StackTrace);
            HttpException he = ((HttpException)ex) ;
            if (he.GetHttpCode() == 404)
            {
                Response.Redirect("404.html");
            }
            if (he.GetHttpCode() == 500)
            {
                // (sender as Global).Context.ClearError();
                Server.ClearError();
            }
            // 在出现未处理的错误时运行的代码 
        }
    }
}
