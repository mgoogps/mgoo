using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MG_GPS.AjaxService
{
    public class WebServiceBasePage : System.Web.Services.WebService
    {
        protected override void Dispose(bool disposing)
        {
            
            base.Dispose(disposing);
            bool isLogin = false;
            try
            {
                isLogin = !MG_BLL.Utils.isLogin();
            }
            catch 
            {
                 
            }
            if (isLogin)
            {
                throw new NotImplementedException("登录信息已失效");
            }

        }
       
    }
}
