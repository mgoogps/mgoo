using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web; 

namespace MG_DAL
{
    public class SessionOper:  IHttpHandler 
    {
        
        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
        private static String sessionName;
         
        public static string SessionName
        {
            get
            {
                if (sessionName == null)
                {
                    sessionName = "LoginUserInfo";
                }
                return sessionName;
            }

            set
            {
                sessionName = value;
            }
        }
        /// <summary>
        /// 清除登录信息 
        /// </summary>
        public static void RemoveSession()
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
            } 
        }

        public static string GetSessionUserID()
        {
            try
            {
                if (isLogin())
                    return (HttpContext.Current.Session[SessionOper.SessionName] as LoginUserInfo).UserID;
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string GetSessionUserName()
        {
            try
            {
                if (isLogin())
                    return (HttpContext.Current.Session[SessionOper.SessionName] as LoginUserInfo).UserName;
                else
                    return "";

            }
            catch (Exception)
            {
                return "";
            }
        }
        //public static string GetSessionLoginType()
        //{
        //    try
        //    {
        //        if (isLogin())
        //            return (HttpContext.Current.Session[SessionOper.SessionName] as LoginUserInfo).LoginType;
        //        else
        //            return "";
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }
        //}
        public static LoginUserInfo GetSession()
        {
            return HttpContext.Current.Session[SessionOper.SessionName] as LoginUserInfo;
        }

        public static void SetSessionUserName(string name)
        {
            (HttpContext.Current.Session[SessionOper.SessionName] as LoginUserInfo).UserName = name;
        }

        public static void SetSession(object value, string key)
        {
            HttpContext.Current.Session[key] = value;
        }
       
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public static bool isLogin(string SessionName = null)
        {
            try
            {
                if (HttpContext.Current.Session[SessionOper.SessionName] == null)
                {
                    return false;
                    // HttpContext.Current.Response.Redirect("~/login.aspx");
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        
        }

    }
}
