using MG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MG_BLL
{
    /// <summary>
    /// 所有页面引用
    /// </summary>
    public class BasePage : System.Web.UI.Page
    { 
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!MG_DAL.SessionOper.isLogin())
            {
                Response.Redirect("~/login.aspx");
            }
        }
    }
    /// <summary>
    /// 所有类引用
    /// </summary>
    public class baseclass  
    { 
        public baseclass( )
        {
            // if (!MG_DAL.SessionOper.isLogin())
            // { 
            //  throw new LoginInformationFail("----登录信息已失效"); 
            // }
            //if (HttpRuntime.Cache.Get(token) == null)
            //{

            //}
            //else
            //{
            //    HttpRuntime.Cache.Insert(token, HttpRuntime.Cache.Get(token), null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
            //}

        }
    }
    public class BasePageUrl
    {
        private static string _previousUrl;

        public static string PreviousUrl
        {
            get
            {
                return _previousUrl;
            }

            set
            {
                _previousUrl = value;
            }
        }
    }

    
}
