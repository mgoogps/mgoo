using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using MgooGps.com;
using System.Web.SessionState;

namespace MgooGps
{
    public partial class login : MG_BLL.Common.UIPage, IRequiresSessionState
    {
        public String language = "";
        public string welcome = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 登录  

            if (!string.IsNullOrWhiteSpace(Request.QueryString["action"]) && Request.QueryString["action"] == "logout")
            { 
                SessionUtil.LoginOut();
            }
            if (com.Utils.GetSession(SessionUtil.KEY) != null)
            {
                //Response.Redirect("~/main.aspx");
            } 
           // MgooGps.com.Utils.log("访问的IP地址（GetHostAddresses）：" + System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(0).ToString());
            language = "<script src=\"/js/Language/language01-zh-cn.js\" type=\"text/javascript\"></script>";
            if (!string.IsNullOrWhiteSpace(Request.QueryString["language"]))
            {
                MgooGps.com.Utils.language = Request.QueryString["language"];
                switch (Request.QueryString["language"])
                {
                    case "en-us": //英语
                        language = "<script src=\"/js/Language/language01-en-us.js\" type=\"text/javascript\"></script>";
                        break;
                    case "pl":  // 波兰语
                        language = "<script src=\"/js/Language/language01-pl.js\" type=\"text/javascript\"></script>";
                        break;
                    case "vi":  //越南
                        language = "<script src=\"/js/Language/language01-vi.js\" type=\"text/javascript\"></script>";
                        break;
                    case "tr": // 土耳其
                        language = "<script src=\"/js/Language/language01-tr.js\" type=\"text/javascript\"></script>";
                        break;
                    case "pt":  //葡萄牙
                        language = "<script src=\"/js/Language/language01-pt.js?v=20130918\" type=\"text/javascript\"></script>";
                        break;
                    case "hk": //香港
                        language = "<script src=\"/js/Language/language01-hk.js\" type=\"text/javascript\"></script>";
                        break;
                    case "ru":  //俄语
                        language = "<script src=\"/js/Language/language01-ru.js?v=20130918\" type=\"text/javascript\"></script>";
                        break;
                    case "de":  //德国
                        language = "<script src=\"/js/Language/language01-de.js?v=20130918\" type=\"text/javascript\"></script>";
                        break;
                    case "fr":  //法国
                        language = "<script src=\"/js/Language/language01-fr.js\" type=\"text/javascript\"></script>";
                        break;
                    case "es": //西班牙语
                        language = "<script src=\"/js/Language/language01-es.js\" type=\"text/javascript\"></script>";
                        break;
                    case "ar"://沙特阿拉伯王国
                        language = "<script src=\"/js/Language/language01-ar.js?v=20130918\" type=\"text/javascript\"></script>";
                        break;
                    case "it-IT"://意大利
                        language = "<script src=\"/js/Language/language01-it-IT.js?v=20130918\" type=\"text/javascript\"></script>";
                        break; 
                    default://中文
                        language = "<script src=\"/js/Language/language01-zh-cn.js\" type=\"text/javascript\"></script>";
                        break;
                }
            }
            #endregion
             
        } 
    }
}