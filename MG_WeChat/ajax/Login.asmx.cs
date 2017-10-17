using MG_BLL;
using MG_BLL.Weixin;
using MG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace MG_WeChat.ajax
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class Login : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true, Description = "登录(loginname:用户名,password:密码，code:微信code(可为空字符串))")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false, XmlSerializeString = false)]
        public string MgLogin(string loginname, string password,string identifies, string code)
        {
            Dictionary<string, dynamic> LoginResult = new Dictionary<string, dynamic>();
            try
            {
                string type = "Phone";
                string result = "";
                //string logName = "wx-" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".log"; 
                if (!string.IsNullOrWhiteSpace(code))
                {
                    WeixinOper wo = new WeixinOper();
                    result = wo.GetWeixinOpenID(code);
                    type = "Weixin";
                    Utils.log("weixin result:" + result);
                }

                Utils.log(string.Format("loginname:{0},identifies:{1},code:{2}", loginname, identifies, code));

                MG_BLL.BllLogin login = new MG_BLL.BllLogin();
                LoginUserInfo loginUser = login.SystemLogin_Bll(loginname, password, identifies, type);

                if (loginUser != null)
                {
                    #region 如果是微信登录,把OpenID存下来 
                    if (result != "")
                    {
                        Dictionary<string, string> res = Utils.ToDictionary(result);
                        if (res.ContainsKey("openid") && !string.IsNullOrEmpty(res["openid"]))
                        {
                            login.AddOpenID(res["openid"], loginUser.UserID, loginUser.LoginName);
                            loginUser.ToKen = res["openid"] + "@" + loginUser.ToKen;
                            Utils.log("登录账号：" + loginname + "，OpenID=" + res["openid"] + "，wxCode=" + code);
                        }
                        else
                        {
                            LoginResult.Add("Message", "微信绑定失败,获取OpenID失败.");
                        }
                       
                    }
                    #endregion

                    LoginResult.Add("StatusCode", statusCode.Code.success);
                    LoginResult.Add("UserID", loginUser.UserID);
                    LoginResult.Add("Token", loginUser.ToKen);
                    LoginResult.Add("Url", "device/DeviceList.html");
                    LoginResult.Add("LoginType", (int)LoginType.User);
                    if (identifies.IndexOf("OuBaoYun") >= 0)
                    {
                        LoginResult.Add("UserName",loginUser.UserName);
                    }
                    // LoginResult.Add("UserName", loginUser.UserName);
                    HttpRuntime.Cache.Insert(identifies + loginUser.UserID + loginUser.ToKen, loginUser, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);

                    return Utils.ToJson(LoginResult);
                }
                else
                { 
                    loginUser = login.Login_Imei(loginname, password, identifies);
                    if (loginUser != null)
                    {
                        LoginResult.Add("StatusCode", statusCode.Code.success);
                        LoginResult.Add("UserID", loginUser.UserID); 
                        LoginResult.Add("Token", loginUser.ToKen);
                        LoginResult.Add("Url", "");
                        LoginResult.Add("DeviceID", loginUser.DeviceID);
                        LoginResult.Add("DeviceName", loginUser.UserName);
                        LoginResult.Add("LoginType", (int)LoginType.Imei);
                        if (identifies.IndexOf("Weixin") > 0)
                        {
                            LoginResult["Url"]= "device/Tracking.html?deviceid="+loginUser.DeviceID;
                        } 
                        HttpRuntime.Cache.Insert(identifies + loginUser.UserID + loginUser.ToKen, loginUser, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                        return Utils.ToJson(LoginResult);
                    }
                    else
                    {
                        LoginResult.Add("StatusCode", statusCode.Code.failure);
                        LoginResult.Add("UserID", "");
                        LoginResult.Add("Token", "");
                        LoginResult.Add("Url", "Login.aspx");
                        return Utils.ToJson(LoginResult);
                    } 
                }
            }
            catch (Exception ex)
            {
                Utils.log(this.GetType().ToString()+ " > MgLogin Error" + ex.Message);
                return "error";
            }
           
        }

        [WebMethod(Description = "注册账号(phone:手机号,password:密码,code:手机验证码).")]
        public string MgRegister(string phone, string password, string code,string username)
        {
            Register r = new Register();
            ajaxResult ar = new ajaxResult();
            if (code.Equals(string.Empty) || !r.VerificationCode(phone, code))
            {
                ar.StatusCode = statusCode.Code.failure;
                ar.Message = "验证码输入错误.";
                ar.Result = "";
                return Utils.ToJson(ar);
            }
            if (phone.Equals(string.Empty) || r.VerificationPhone(phone))
            {
                ar.StatusCode = statusCode.Code.failure;
                ar.Message = "该手机号码已被注册.";
                ar.Result = "";
                return Utils.ToJson(ar);
            }
            if (password.Equals(string.Empty) || r.MgRegister(phone, password, username))
            {
                ar.StatusCode = statusCode.Code.success;
                ar.Message = "注册成功.";
            }
            else
            {
                ar.StatusCode = statusCode.Code.failure;
                ar.Message = "注册失败,请重新注册.";
            }
            ar.Result = "";
            return Utils.ToJson(ar);
        }

        [WebMethod(Description = "发送验证码.")]
        public string SMSCodes(string phone,string type)
        {
            Register r = new Register();
            ajaxResult ar = new ajaxResult();
            if (string.IsNullOrEmpty(phone) || !phone.StartsWith("1") || phone.Length!=11)
            {
                return Utils.GetResult("号码格式错误.", statusCode.Code.failure );
            }
            if (type == "retrieve" && !r.VerificationPhone(phone)) //register 注册
            {
                ar.StatusCode = statusCode.Code.failure;
                ar.Message = "该手机号码未注册.";
                ar.Result = "";
                return Utils.ToJson(ar);
            }
            string result = r.SMSCodes(phone);
            if (result.Equals(string.Empty))
            {
                ar.StatusCode = statusCode.Code.success;
                ar.Message = "验证码发送成功.";
                ar.Result = "";
                return Utils.ToJson(ar);
            }
            else
            {
                ar.StatusCode = statusCode.Code.failure;
                if (result.Equals("00104")) 
                    ar.Message = "同一个号码,1小时内只能发送4条,并且24小时内只能发10条.";
                else if(result.Equals("00025"))
                    ar.Message = "手机格式不对.";
                else if (result.Equals("00008"))
                    ar.Message = "操作频繁.";
                else if (result.Equals("00007"))
                    ar.Message = "重复提交.";
                else
                    ar.Message = "验证码发送失败.";
                Utils.log("验证码发送失败:"+phone+" - "+ result);
                ar.Result = result;
                return Utils.ToJson(ar);
            } 
        }

        [WebMethod(Description = "重置密码.")]
        public string RetrievePassword(string phone, string password, string code)
        {
            ajaxResult ar = new ajaxResult();
            try
            {
                int cacheCode = Utils.GetCache<int>("SMS" + phone); 
                if (string.IsNullOrEmpty(cacheCode.toStringEmpty()) || !code.Equals(cacheCode.toStringEmpty()))
                {
                    ar.StatusCode = statusCode.Code.failure;
                    ar.Message = "验证码错误!";
                }
                else
                {
                    Register r = new Register();
                    ar = r.RetrievePassword(phone, password);
                    if (ar.StatusCode == statusCode.Code.success)
                    {
                        HttpRuntime.Cache.Remove("SMS" + phone);
                    } 
                    //if (r.RetrievePassword(phone, password))
                    //{
                    //    HttpRuntime.Cache.Remove("SMS" + phone);
                    //    ar.StatusCode = statusCode.Code.success;
                    //    ar.Message = "密码重设成功.";
                    //    ar.Result = "";
                    //    return Utils.ToJson(ar);
                    //}
                    //else
                    //{
                    //    ar.StatusCode = statusCode.Code.failure;
                    //    ar.Message = "密码重设失败.";
                    //    ar.Result = "";
                    //}
                }
                return Utils.ToJson(ar);
            }
            catch (Exception)
            {
                return Utils.GetResult("错误的验证码.", statusCode.Code.error);
            }
          
        }

        //[WebMethod(Description = "验证改手机号十分钟内的验证码是否正确.")]
        //public string VerificationCode(string phone,string code)
        //{
        //    string cacheCode = HttpRuntime.Cache.Get("SMS" + phone).toStringEmpty();
        //    ajaxResult ar = new ajaxResult();
        //    if (!string.IsNullOrEmpty(code) && code.Equals(cacheCode))
        //    { 
        //        ar.StatusCode = statusCode.Code.success;
        //        ar.Message = "验证通过.";
        //        ar.Result = (Guid.NewGuid().ToString() + Guid.NewGuid().ToString()).Replace("-", "");
        //       // HttpRuntime.Cache.Remove("SMS"+phone);
        //       // HttpRuntime.Cache.Insert("SMS_KEY" + phone, ar.Result, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
        //    }
        //    else
        //    {
        //        ar.StatusCode = statusCode.Code.failure;
        //        ar.Message = "验证失败."; 
        //    } 
        //    return Utils.ToJson(ar);
        //}

        [WebMethod(Description = "删除OpenID.")]
        public string DeleteOpenID(string UserID,string OpenID)
        {
            BllLogin bl = new BllLogin();
            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxda27104d229a3608&redirect_uri=http://m.mgoogps.com/login.aspx?action=weixin&type=wxpay&response_type=code&scope=snsapi_base&state=STATE&connect_redirect=1#wechat_redirect";
            if (bl.DeleteOpenID(OpenID, UserID))
            {
               return Utils.GetResult("清除成功.", statusCode.Code.success,url);
            }
            else
            {
                return Utils.GetResult("清除失败.", statusCode.Code.failure,url);
            }
        }

        [WebMethod(Description ="手机信息")]
        public string MobileInfo(string appid,string appkey, string clientid,string token,string model,string vendor, string imei,string uuid,string imsi,string resolution,string dpi,string os,string osversion, string userid,string appversion)
        {
            MG_BLL.BllLogin login = new MG_BLL.BllLogin();
            bool b = login.MobileInfo(appid, appkey, clientid,token,model, vendor, imei, uuid, imsi,resolution,dpi,os,osversion,userid, appversion);
            if (b)
            {
                return Utils.GetResult("操作成功.", statusCode.Code.success);
            }
            return Utils.GetResult("操作失败.", statusCode.Code.failure);
        }
        [WebMethod(Description = "APP信息")]
        public string MobileApps(string appid, string appkey, string packagename, string os)
        {
            BllLogin bl = new BllLogin();
            int status  = bl.MobileApps(appid, appkey, packagename, os);
            if (status == -1)
            {
                return Utils.GetResult("参数错误.", statusCode.Code.failure);
            }
            else if (status == 2)
            {
                return Utils.GetResult("已存在.", statusCode.Code.success);
            }
            else if (status > 0)
            {
                return Utils.GetResult("操作成功.", statusCode.Code.success); 
            }
            return Utils.GetResult("操作失败.", statusCode.Code.failure);
        }
    }
}
