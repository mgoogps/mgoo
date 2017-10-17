using MG_BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace MG_GPS
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
           // Utils.log("AbsoluteUri--" + Request.Url.AbsoluteUri); 
           // if (Request.Url.AbsoluteUri.IndexOf("m.mgoogps.com") > 0)
            //{  
               // Utils.log("跳转到m.mgoogps.com");
              //  Response.Redirect("m.mgoogos.com");
           // }
            //if (Request.Url.AbsoluteUri.IndexOf("mgoogps.com") > 0)
            //{
             //   Utils.log("跳转到m.mgoogps.com");
                //  Response.Redirect("www.mgoogos.com");
           // }
            #region Web 
            // string url1 = "http://apis.baidu.com/heweather/weather/free";
            // string param = "city=beijing";
            //  string result = request(url1, param);

            if (!string.IsNullOrWhiteSpace(Request.QueryString["action"]))
            {
                if (Request.QueryString["action"] == "weixin")
                {
                    string wxCode = Request.Params["code"]; 
                    Response.Redirect("http://m.mgoogps.com:8070/login.aspx?action=weixin&code=" + wxCode);
                }
              
                if (Request.QueryString["action"] == "logout")
                {
                    MG_BLL.Utils.RemoveSession();
                    Response.Redirect("login.aspx");
                }
                if (Request.QueryString["action"] == "clearAbsoluteUri")
                {
                    BasePageUrl.PreviousUrl = null;
                    Response.Redirect("login.aspx");
                } 
            }
            if (!string.IsNullOrEmpty(Request.QueryString["deviceid"]))
            {
                string wxCode = Request.Params["code"];
                string deviceid = Request.QueryString["deviceid"];
                Utils.log(deviceid);
                string openID = weixin(wxCode);
                Utils.log("微信支付测试。openid" + openID); 
                Response.Redirect("http://m.mgoogps.com:8070/pay/pay.html?deviceid=" + deviceid + "&type=1&openid=" + openID);
            }
            if (Utils.isLogin())
            {
                Response.Redirect("Index.aspx");
            } 
            string url = "/Index.aspx"; 
           // Utils.log("访问的IP地址（UserHostAddress）：" + HttpContext.Current.Request.UserHostAddress);
            //Utils.log("访问的IP地址（REMOTE_ADDR）：" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            //Utils.log("访问的IP地址（HTTP_X_FORWARDED_FOR）：" + HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
           // Utils.log("访问的IP地址（GetHostAddresses）：" + System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).GetValue(0).ToString());
            if (!string.IsNullOrWhiteSpace(Request.Form["username"]) && !string.IsNullOrWhiteSpace(Request.Form["passwordmd5"]))
            { 
                try
                { 
                    //string pwdMd5 = Request.Form["passwordmd5"].ToString().Substring(8, 16);
                    MG_BLL.BllLogin login = new MG_BLL.BllLogin();
                    bool loginBool = login.WebSystemLogin_Bll(Request.Form["username"], Request.Form["passwordmd5"] ,"System");
                    if (loginBool)
                    {
                        url =  BasePageUrl.PreviousUrl ?? "/Index.aspx";
                       // Utils.log("登录跳转url："+url);
                        Response.Redirect("Index.aspx", false);
                    }
                    else
                    {
                        this.txtErrorMsg.Text = "登录失败,账号或密码错误！";
                        this.txtErrorMsg.Visible = true;
                    }

                    //string postString = "loginName=" + Request.Form["username"] + "&passWord=" + pwdMd5;//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
                    //byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
                    //string url = "http://api.mgoogps.com/account/login ";//地址  
                    //WebClient webClient = new WebClient();
                    //webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
                    //byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
                    //string srcString = Encoding.UTF8.GetString(responseData);//解码
                    // Response.Redirect("Index.aspx");
                }
                catch (Exception ex)
                {
                    Utils.log("登录出错啦！"+ex.Message );
                    this.txtErrorMsg.Text = "登录出错！";
                    this.txtErrorMsg.Visible = true;
                }


            }
            #endregion

            #region 微信登录测试
            //string code = Request.Params["code"];
            //Utils.log("----------code:" + code);
            //if (Request.QueryString["action"] == "weixin")
            //{

            //    //code: 02175a07935c30ee0d542055b5cd653y
            //      code = Request["code"];
            //    weixin(code);
            //    Utils.log("一共：" + Request.Params.Count);
            //    for (int i = 0; i < Request.Params.Count; i++)
            //    {
            //        //Utils.log(Request.Params.AllKeys[i] + ":" + Request.Params[i]);
            //        //Utils.log(Request.Params.Keys[i]+":"+ Request.Params[i]);
            //    }
            //    Stream stream = Request.InputStream;
            //    XmlDocument dom = new XmlDocument();
            //    dom.Load(stream);
            //    XmlElement root = dom.DocumentElement;
            //    string toUserName = root.SelectSingleNode("ToUserName").InnerText;
            //    string fromUserName = root.SelectSingleNode("FromUserName").InnerText;
            //    Utils.log(toUserName + "," + fromUserName); 
            //}
            #endregion
        }

        #region 获取web客户端ip
        /// <summary>
        /// 获取web客户端ip
        /// </summary>
        /// <returns></returns>
        public   string GetWebClientIp()
        {

            string userIP = "未获取用户IP";

            try
            {
                if (System.Web.HttpContext.Current == null
            || System.Web.HttpContext.Current.Request == null
            || System.Web.HttpContext.Current.Request.ServerVariables == null)
                    return "";

                string CustomerIP = "";

                //CDN加速后取到的IP simone 090805
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];


                if (!String.IsNullOrEmpty(CustomerIP))
                    return CustomerIP;

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (CustomerIP == null)
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                }

                if (string.Compare(CustomerIP, "unknown", true) == 0)
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                return CustomerIP;
            }
            catch { }

            return userIP;

        }
        #endregion


        
        /// <summary>
        /// 发送HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="param">请求的参数</param>
        /// <returns>请求结果</returns>
        public static string request(string url, string param)
        {
            string strURL = url + '?' + param;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", "012942978a52f2000873cb0899262998");
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }

        public string weixin(string code)
        {

            //https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code
            string sUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=wxda27104d229a3608&secret=a754e16403dd067372dbfe2f17a6c587&code=" + code + "&grant_type=authorization_code";
            Utils.log(sUrl);
            WebClient webClient = new WebClient();
            Byte[] bytes = webClient.DownloadData(sUrl);
            string result = Encoding.GetEncoding("utf-8").GetString(bytes);
            Utils.log("-------result:" + result);
            Dictionary<string,string> res = Utils.ToDictionary(result);
            //if (string.IsNullOrEmpty(res["openid"]))
           // {
               // sUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxda27104d229a3608&redirect_uri=http://m.mgoogps.com/login.aspx?action=weixin&type=wxpay&response_type=code&scope=snsapi_base&state=STATE&connect_redirect=1#wechat_redirect";
           // }
            //Utils.log("-------access_token:" + res["access_token"]);
            //Utils.log("-------expires_in:" + res["expires_in"]);
            //Utils.log("-------refresh_token:" + res["refresh_token"]);
            //Utils.log("-------openid:" + res["openid"]);
            //Utils.log("-------scope:" + res["scope"]);

            // JSONObject obj = JSONConvert.DeserializeObject(result);
            // string sParam = Request["state"];
            //  string sRedictUrl = "http://www.baidu.com";
            // switch (sParam)
            // {
            //    case "myorder":
            //        sRedictUrl = "/Account/Order?OpenID=" + obj["openid"] + "&Param=order";
            //        break;
            //    case "book":
            //        sRedictUrl = "/Book/WXHotels";
            //        break; 
            //    default:
            //        sRedictUrl = "/Account/Index?OpenID={0}" + obj["openid"];
            //        break;
            //}
            // Response.Redirect(sRedictUrl);
            return res["openid"];

    }

}
}