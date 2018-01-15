using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MG_DAL;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Web;
using System.Security.Cryptography;

namespace MG_BLL
{
    public class BllLogin
    {
        public LoginUserInfo SystemLogin_Bll(string loginName, string passWord,string identifies, string loginType)
        {
            try
            {
                string strSql = "select UserName,UserID,LoginName,UserType,SuperAdmin,PassWord from users where Deleted=0 and LoginName=@LoginName";
                SqlParameter[] parameter = new SqlParameter []{ new SqlParameter("LoginName",loginName) };
                SQLServerOperating s = new SQLServerOperating();
                DataTable dt = s.Selects(strSql, parameter);
                if (dt.Rows.Count <= 0)
                { 
                    return null;
                }
                else
                {
                    DataRow loginUserDic = dt.Rows[0];
                    // 加密   
                    //string EncryptPWD = FormsAuthentication.HashPasswordForStoringInConfigFile(loginUserDic["PassWord"].toStringEmpty(), "MD5");
                    string EncryptPWD = Utils.GetMD5(loginUserDic["PassWord"].toStringEmpty());
                   //  = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes())).Replace("-", "").ToUpper();
                    if (EncryptPWD.Equals(passWord.ToLower()))
                    {
                        LoginUserInfo _loginUserInfo = new  LoginUserInfo(); 
                        _loginUserInfo.UserID = loginUserDic["UserID"].toStringEmpty();
                        _loginUserInfo.UserName = loginUserDic["UserName"].toStringEmpty();
                        _loginUserInfo.LoginName = loginUserDic["LoginName"].toStringEmpty();
                        _loginUserInfo.UserType = loginUserDic["UserType"].toStringEmpty();
                        _loginUserInfo.SuperAdmin = loginUserDic["SuperAdmin"].toStringEmpty();
                        _loginUserInfo.LoginTime = DateTime.Now;
                        _loginUserInfo.LoginType = LoginType.User;
                        _loginUserInfo.ToKen = Guid.NewGuid().ToString().Replace("-", "").ToLower();
                        _loginUserInfo.Identifies = identifies;
                        if(identifies.Split('@').Length == 2)
                        {
                            string mt = identifies.Split('@')[1];
                            if (mt.ToUpper() == "BAIDU")
                            {
                                _loginUserInfo.MapType =  MapType .BAIDU;
                            }
                            else
                            {
                                _loginUserInfo.MapType = MapType.AMAP;
                            } 
                        }
                        else
                        {
                            _loginUserInfo.MapType = MapType.AMAP;
                        }
                   
                        //string ip = Utils.GetIP(); 
                        //HttpRuntime.Cache.Insert("ip_"+_loginUserInfo.ToKen, ip, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                         
                        //SessionOper.SetSession(_loginUserInfo, SessionOper.SessionName);
                        return _loginUserInfo;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utils.log("登录出错：" +loginName+","+passWord +" --异常信息："+ex.Message+",堆栈信息："+ex.StackTrace);  
                return null;
            } 
        }

        public LoginUserInfo Login_Imei(string LoginName, string PassWord, string Identifies)
        {
            try
            {
                string strSql = "select DeviceID,SerialNumber, DeviceName, UserID,DevicePassword from devices where serialnumber = @serialnumber and deleted =0";
                SQLServerOperating s = new SQLServerOperating();
                DataTable dt = s.Selects(strSql,new SqlParameter[] { new SqlParameter("serialnumber", LoginName) });
                if (dt.Rows.Count > 0)
                {
                    DataRow loginUserDic = dt.Rows[0];
                    // 加密   
                    //string EncryptPWD = FormsAuthentication.HashPasswordForStoringInConfigFile(loginUserDic["DevicePassword"].toStringEmpty(), "MD5");
                    string EncryptPWD = Utils.GetMD5(loginUserDic["DevicePassword"].toStringEmpty());
                    if (EncryptPWD.Equals(PassWord.ToLower()))
                    {
                        LoginUserInfo _loginUserInfo = new LoginUserInfo();
                        _loginUserInfo.DeviceID = loginUserDic["DeviceID"].toStringEmpty();
                        _loginUserInfo.UserID = loginUserDic["UserID"].toStringEmpty();
                        _loginUserInfo.UserName = loginUserDic["DeviceName"].toStringEmpty();
                        _loginUserInfo.DeviceName = loginUserDic["DeviceName"].toStringEmpty();
                        _loginUserInfo.LoginTime = DateTime.Now;
                        _loginUserInfo.Identifies = Identifies;
                        _loginUserInfo.LoginType =  LoginType.Imei;
                        _loginUserInfo.ToKen = Guid.NewGuid().toStringEmpty().Replace("-", "").ToLower();
                        if (Identifies.Split('@').Length == 2)
                        { 
                            string mt = Identifies.Split('@')[1];
                            if (mt.ToUpper() == "BAIDU")
                            {
                                _loginUserInfo.MapType = MapType.BAIDU;
                            }
                            else
                            {
                                _loginUserInfo.MapType = MapType.AMAP;
                            }
                        }
                        else
                        {
                            _loginUserInfo.MapType = MapType.AMAP;
                        }
                        return _loginUserInfo;
                    }

                }

            }
            catch (Exception ex)
            {
                Utils.log("Login_Imei Error:" + ex.Message);
            }
            return null;
        }

        public bool AddOpenID(string OpenID, string UserID,string LoginName)
        {
            //StringBuilder sb = new StringBuilder();
            try
            {
                SQLServerOperating s = new SQLServerOperating();
                string strSql = "select count(-1) from WechatUsers where OpenID=@OpenID";
                string exist = s.Select(strSql, new SqlParameter[] { new SqlParameter("OpenID", OpenID), new SqlParameter("UserID", UserID) });
                if (Convert.ToInt32(exist) == 0)
                {
                    //DataTable dt = s.Selects(" select NotificationType from ExceptionMessage group by NotificationType"); 
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    sb.Append(row["NotificationType"]+",");
                    //}
                    //sb = sb.Length > 0 ? sb.Remove(sb.Length-1, 1) : sb;
                    strSql = "insert into WechatUsers (UserID,LoginName, OpenID, CreateTime, UpdateTime,PushMsg)values(@UserID,@LoginName,@OpenID,GETDATE(),GETDATE(),@PushMsg)";
                    return s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("UserID", UserID), new SqlParameter("OpenID", OpenID),new SqlParameter("LoginName", LoginName),new SqlParameter("PushMsg", "") }) > 0;
                }
                else
                {
                    strSql = "update WechatUsers set UpdateTime=GETDATE(),UserID=@UserID,LoginName=@LoginName, Deleted=0 where OpenID=@OpenID";
                    return s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("UserID", UserID),new SqlParameter("OpenID", OpenID),new SqlParameter ("LoginName", LoginName) }) > 0;
                }
            }
            catch (Exception ex)
            {
                Utils.log("AddOpenID ERROR：" + ex.Message+ ",StackTrace:" + ex.StackTrace+",OpenID:"+ OpenID+ ",UserID:"+UserID+",LoginName:" + LoginName);
                return false;
            }
          
        }

        public bool DeleteOpenID(string OpenID, string UserID)
        {
            string strSql = "update WeChatUsers set deleted = 1 where UserID = @UserID";
            // if (!string.IsNullOrEmpty(OpenID))
            //{
            strSql += " and OpenID=@OpenID  ";
            //} 
            SQLServerOperating s = new SQLServerOperating();
            if (s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("OpenID", OpenID.Split('@')[0]), new SqlParameter("UserID", UserID) }) > 0)
            {
                return true;
            }
            else
            {
                Utils.log("DeleteOpenID 操作失败:sql:" + strSql + ";OpenID:" + OpenID + ",UserID：" + UserID);
                return false;
            }
        }

        public bool WebSystemLogin_Bll(string loginName, string passWord, string loginType)
        {
            MgoogpsWebClient webClient = new MgoogpsWebClient();
            try
            {
                webClient.RequestMethodName = "/account/login";
                webClient.RequestPostData = Encoding.UTF8.GetBytes("loginName=" + loginName + "&passWord=" + passWord.Substring(8, 16));
                webClient.RequestContentType = "application/x-www-form-urlencoded";
                string data = webClient.RequestSend();
                if (data.IndexOf("error") > 0)
                {
                    return false;
                }
                else
                {
                    MG_DAL. LoginUserInfo _loginUserInfo = new MG_DAL.LoginUserInfo();
                    Dictionary<string, object> d = (Dictionary<string, object>)Utils.ToObject(data);

                    Dictionary<string, object> loginUserDic = (Dictionary<string, object>)d["UserInfo"];

                    _loginUserInfo.ToKen = d["Token"].toStringEmpty();
                    MgoogpsWebClient.ToKen = d["Token"].toStringEmpty();

                    _loginUserInfo.UserID = loginUserDic["_id"].toStringEmpty();
                    _loginUserInfo.UserName = loginUserDic["username"].toStringEmpty();
                    _loginUserInfo.Address = loginUserDic["address"].toStringEmpty();
                    _loginUserInfo.EMail = loginUserDic["email"].toStringEmpty();
                    _loginUserInfo.LoginTime = DateTime.Now;
                    _loginUserInfo.LoginType =  LoginType.User;
                    SessionOper.SetSession(_loginUserInfo, SessionOper.SessionName);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.log("登录出错：" + loginName + "," + passWord + " --异常信息：" + ex.Message + ",堆栈信息：" + ex.StackTrace);
                Utils.log(ex.Message + "-" + webClient.RequestUrl + "-" + webClient.RequestMethodName);
                //  throw new Exception(ex.Message+"-"+webClient.RequestUrl+"-" + webClient.RequestMethodName);
                throw ex;
            }
        }


        public bool MobileInfo(string AppID, string AppKey, string ClientID, string Token, string Model, string Vendor, string IMEI,string UUID, string IMSI, string Resolution, string DPI, string OS,string OSVersion, string UserID, string APPVersion)
        {
         
            try
            {
                if (string.IsNullOrEmpty(ClientID) || string .IsNullOrEmpty(Token) || string .IsNullOrEmpty(AppID) || string.IsNullOrEmpty(AppKey)||string.IsNullOrEmpty(OS))
                {
                    return false;
                }
                if (ClientID.ToLower() == "null" || Token.ToLower() == "null" || AppID.ToLower() == "null" || AppKey.ToLower() == "null" || OS.ToLower() == "null" || ClientID.ToLower() == "(null)")
                {
                    return false;
                }
                string strSql = "select COUNT(*) from MobileAppInfo where ClientID=@ClientID";
                SQLServerOperating s = new SQLServerOperating();
                SqlParameter[] pars = new SqlParameter[] { new SqlParameter("ClientID", ClientID) };
                string count = s.Select(strSql, pars);
                strSql = "select ID from apps where AppID=@AppID and AppKey=@AppKey";
                string appsid = s.Select(strSql,new SqlParameter[] { new SqlParameter("AppID",  AppID ),new SqlParameter ("AppKey", AppKey) });
                if (Convert.ToInt32(count) > 0)
                {
                    strSql = "update MobileAppInfo set UserID=@UserID,LastDate=GETDATE(),OS=@OS,OSVersion=@OSVersion,AppsID=@AppsID,APPVersion=@APPVersion where ClientID=@ClientID";
                    pars = new SqlParameter[] {
                        new SqlParameter("ClientID", ClientID) ,
                        new SqlParameter("UserID", UserID),
                        new SqlParameter("OS",OS),
                        new SqlParameter("OSVersion", OSVersion),
                        new SqlParameter("AppsID",appsid),
                        new SqlParameter("APPVersion",APPVersion)
                    };
                    s.ExecuteSql(strSql, pars);
                }
                else
                {
                    
                    strSql = @"Insert into MobileAppInfo(UserID, ClientID, Token, Model, Vendor, IMEI, UUID, IMSI, Resolution, DPI, OS,OSVersion, Created, LastDate,AppsID,APPVersion)
                              values(@UserID, @ClientID, @Token, @Model, @Vendor, @IMEI, @UUID,@IMSI,@Resolution,@DPI, @OS,@OSVersion, GETDATE(), GETDATE(),@AppsID,@APPVersion)";

                    pars = new SqlParameter[] { new SqlParameter("ClientID", ClientID),
                        new SqlParameter("UserID", UserID),
                        new SqlParameter("Token", Token),
                        new SqlParameter("Model", Model),
                        new SqlParameter("Vendor", Vendor),
                        new SqlParameter("IMEI", IMEI),
                        new SqlParameter("UUID", UUID),
                        new SqlParameter("IMSI", IMSI),
                        new SqlParameter("Resolution", Resolution),
                        new SqlParameter("DPI", DPI),
                        new SqlParameter("OS", OS),
                        new SqlParameter("OSVersion",OSVersion),
                        new SqlParameter("AppsID",appsid),
                        new SqlParameter("APPVersion",APPVersion)

                    };
                    s.ExecuteSql(strSql, pars);
                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.log("MobileInfo Error:"+ex.Message+ ",StackTrace" + ex.StackTrace);
                return false;
            }
       
        }

        public int MobileApps(string AppID,string AppKey,string PackageName, string OS)
        {
            try
            {
                if (string.IsNullOrEmpty(AppID) || string.IsNullOrEmpty(AppKey) || string.IsNullOrEmpty(PackageName))
                {
                    return -1;
                }
                if (AppID == "null" || AppKey == "null")
                {
                    return -1;
                }
                
                string strSql = "select count(*) from apps where AppID=@AppID and AppKey=@AppKey";
                SQLServerOperating s = new SQLServerOperating();
                string count = s.Select(strSql,new SqlParameter[] { new SqlParameter ("AppID", AppID), new SqlParameter ("AppKey", AppKey) });
                if (Convert.ToInt32(count) <= 0)
                {
                    strSql = @"insert into Apps(AppID,  AppKey,  PackageName, OS)
                               values(@AppID, @AppKey,@PackageName, @OS)";//AppSecret,MasterSecret,  ,@AppSecret @MasterSecret,
                    return s.ExecuteSql(strSql, new SqlParameter[] {
                        new SqlParameter("AppID",AppID),
                       // new SqlParameter("AppSecret", AppSecret),
                        new SqlParameter("AppKey", AppKey),
                       // new SqlParameter("MasterSecret",MasterSecret),
                        new SqlParameter("PackageName",PackageName),
                        new SqlParameter("OS",OS)
                    });
                }
                else
                {
                    return 2;
                } 
            }
            catch (Exception ex)
            {
                Utils.log("MobileApps Error:"+ex.Message);
                return -3;
            }
           
        }
    }
}
