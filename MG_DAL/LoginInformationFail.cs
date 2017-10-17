using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MG_DAL
{
    public class LoginInformationFail :Exception
    {
        public LoginInformationFail(string auxMessage) :
        base(auxMessage)
        {
            // this.HelpLink = "http://msdn.microsoft.com";
            this.Source = "美谷科技";
        }
        public override string StackTrace
        {
            get
            { 
                return "登录信息已失效.请重新登录！";
            }
        }
    }
}
