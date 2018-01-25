using MgooGps.com;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] == "logout")
            {
                Utils.log("退出URL：" + Utils.logoutUrl);
                string url = Utils.logoutUrl;
                Utils.logoutUrl = "";
                //MgooGps.com.Utils.LoginOut();
                SessionUtil.LoginOut();
                Response.Redirect(url == "" ? "login.aspx" : url);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["t"]) && !string.IsNullOrEmpty(Request.QueryString["token"]))
            {
                try
                {
                    string token = Request.QueryString["token"];
                    string ticks = Request.QueryString["t"];
                    DateTime d = new DateTime(long.Parse(ticks));
                    Utils.log(token + "," + ticks + "," + d.ToString("yyyyMMdd HH:mm:ss"));
                    if ((DateTime.Now - d).TotalSeconds < 10)
                    {
                        if (HttpRuntime.Cache.Get(token + ticks) != null)
                        {
                            Utils.logoutUrl = "";
                            UserInfo userinfo = (UserInfo)HttpRuntime.Cache.Get(token + ticks); 
                            HttpRuntime.Cache.Remove(token + ticks);
                            Utils.log("default>自动登录成功:"+userinfo.UserID+","+userinfo.LoginName);
                            //Utils.SetSession("UserInfo", userinfo);
                            SessionUtil.SetSession(SessionUtil.KEY, userinfo);
                            Response.Redirect("main.aspx",false);
                            return;
                        }
                    }
                    else
                    {
                        Utils.log("default>自动登录链接失效:跳回原地址" + Request.UrlReferrer.AbsoluteUri);
                        Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                    }

                }
                catch (Exception ex)
                {
                    Utils.log("default>自动登录出错:" + ex.Message + ",堆栈:" + ex.StackTrace + ",跳回原地址：" + Request.UrlReferrer.AbsoluteUri);
                    Response.Redirect(Request.UrlReferrer.AbsoluteUri);
                }
            }
            Response.Redirect("~/login.aspx");
        }
    }
}