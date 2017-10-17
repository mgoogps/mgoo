using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.Common.lib
{
    public class Config
    {
        /// <summary>
        /// 澳博美谷的  UserID
        /// </summary>
        //public static int aoboUserID { get { return 3437; }   private set { }  }
        public static int SMSNoticeUserID { get; private set; } = 4927;


        private static string _CurrentHost;

        private static string _CurrentHostLogo;

        private static string _CurrentHostWelcomeMessage;

        public static string CurrentHost
        {
            get
            {
                return _CurrentHost;
            }

            set
            {
                _CurrentHost = value;
            }
        }

        /// <summary>
        /// 根据访问的域名 显示不同的Logo图片
        /// </summary>
        public static string CurrentHostLogo
        {
            get
            {
                return _CurrentHostLogo;
            }
            set
            {
                _CurrentHostLogo = value;
            }
        }
        /// <summary>
        /// 根据访问的域名不同，显示不同的欢迎语
        /// </summary>
        public static string CurrentHostWelcomeMessage
        {
            get
            {
                return _CurrentHostWelcomeMessage;
            }

            set
            {
                _CurrentHostWelcomeMessage = value;
            }
        }

        public class LogName
        {
            private static string MessageLogName { get { return ""; } }
        }
    }  
  
}
