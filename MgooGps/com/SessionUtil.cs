using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace MgooGps.com
{
    public class SessionUtil: IRequiresSessionState
    {
        public static string KEY
        {
            get
            {
                return "SESSION_KEY_USER";
            }
        }
        public static void SetSession(string key,object val)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[key] != null)
                HttpContext.Current.Session.Remove(key);
            HttpContext.Current.Session[key] = val;
        }
        public static object GetSession(string key)
        {
            return System.Web. HttpContext.Current.Session[key];
        }

        public static T GetSession<T>(string key)
        {
            try
            {
                return (T) GetSession(key); 
            }
            catch (Exception ex)
            {

                throw ex;
            } 
        }
        public static void LoginOut()
        {
            HttpContext.Current.Session.Remove(KEY); 
        }
    }
}