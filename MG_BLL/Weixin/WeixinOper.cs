using MG_BLL.App;
using MG_BLL.Entity;
using MG_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MG_BLL.Weixin
{
    public class WeixinOper
    {
        public string wx_appid;
        private string wx_secret;
        //private string wx_access_token;
        //private string wx_jsapi_ticket;
        private bool bool_token;
        private SQLServerOperating sqlserver_oper = null;
        public WeixinOper(SQLServerOperating s)
        {
            setWx_Info();
            sqlserver_oper = s;
        }
        public WeixinOper()
        {
            setWx_Info();
        }
        public WeixinOper(string token)
        {
            setWx_Info();
            string _token = ConfigurationManager.AppSettings["WeChatPushToken"].ToString();
            bool_token = _token == token;
            //this.token = token;
        }
        public void setWx_Info()
        {
            this.wx_appid = Pay.WeixinPay.lib.WxPayConfig.APPID;//System.Configuration.ConfigurationManager.AppSettings["MgooWXAppID"].ToStringEmpty();
            this.wx_secret = Pay.WeixinPay.lib.WxPayConfig.APPSECRET; // "a754e16403dd067372dbfe2f17a6c587"; //ConfigurationManager.AppSettings["MgooWXAppSecret"].ToStringEmpty();
            AccessToken();
        }
        private SQLServerOperating GetSQLServerOperating()
        { 
            if (this.sqlserver_oper != null)
            {
                return this.sqlserver_oper;
            }
            else
            {
                return new SQLServerOperating();
            }
        }
        public bool istoken()
        {
            if (!this.bool_token)
            {
                return false;
            }
            return true;
        }
        public string StartPush(string userid,string exceptionid,string devicename,string message,string date,string remark = null)
        {
            Dictionary<string, string> exMsg =  GetExceptionByID(exceptionid);
            var logName = "PushedMessage" + DateTime.Now.ToString("yyyyMM") + ".log";
            if (FilterMsgType(userid, exMsg["NotificationType"]))
            {
                //判断该设备是否过滤掉震动报警
                //3008000000,3528888000开头的两款设备没有撤防设防指令，ServerID2保存的是状态，0是过滤，1是不过滤  
                if (exMsg["NotificationType"] == "7" && exMsg["ServerID2"] == "0")
                {
                    Utils.log("设备已过滤掉震动报警：DeviceName:" + devicename, logName);
                    return Utils.GetResult("该设备已过滤掉该报警信息", statusCode.Code.failure);
                }
                if (string.IsNullOrEmpty(remark))
                {
                    remark = "您好，您的车辆有报警！";
                }
              
                Task.Factory.StartNew(()=> {
                    AppPush ap = new AppPush(devicename, message, exceptionid, exMsg["Created"], exMsg["NotificationType"]);
                    ap.sqlOper = GetSQLServerOperating();
                    ap.push(userid);
                }); 
                Task.Factory.StartNew(() => {
                    //短信通知,同一个号码24小时内最多只能发送50条短信
                    Common.SMSService.MiaodiYun sms = new Common.SMSService.MiaodiYun();
                    sms.SMSNotice(exMsg["UserID"], exMsg["UserName"], exMsg["CellPhone"], devicename, exMsg["Created"], message);
                    //型号是 “MG-X30B”时，发生断电报警要通知到设备绑定的手机号
                    if (exMsg["Model"] == "213")
                    {
                        sms.SMSX30B(exMsg["SerialNumber"], devicename, exMsg["Created"], exMsg["DeviceCellPhone"]);
                    }
                });
                Task.Factory.StartNew(()=> Pushed(exMsg["UserID"], devicename, exMsg["Message"], exMsg["Created"], exMsg["OLat"], exMsg["OLng"], remark, exceptionid, exMsg["DeviceID"]));
                //微信推送
                return "";//Pushed(exMsg["UserID"], devicename, exMsg["Message"], exMsg["Created"], exMsg["OLat"], exMsg["OLng"], remark, exceptionid,exMsg["DeviceID"]);
            }
            else
            {
                // Utils.log("userid:" + userid+ ",exceptionid"+ exceptionid + ",message:" + message+ ",devicename" + devicename);
                return Utils.GetResult("该用户已过滤掉该报警类型.", statusCode.Code.failure, logName);
            }

        }
        public string GetWeixinOpenID(string code, string logName = null)
        {
            string result = "";
            try
            {
                //https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code

                string sUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + this.wx_appid + "&secret=" + this.wx_secret + "&code=" + code + "&grant_type=authorization_code";
                WebClient webClient = new WebClient();
                Byte[] bytes = webClient.DownloadData(sUrl);
                result = Encoding.GetEncoding("utf-8").GetString(bytes);
            }
            catch (Exception ex)
            {
                Utils.log("出错啦：" + ex.Message);
                // throw ex;
            }
            return result;
        }
        public string Pushed(string UserID, string DeviceName, string Message, string Date, string Lat, string Lng, string Remark, string exceptionid,string DeviceID)
        {
            try
            {
                int tryIndex = 0;
                SQLServerOperating s = GetSQLServerOperating();
                string strSql = "select ID, UserID,LoginName, OpenID, CreateTime, UpdateTime from WechatUsers where UserID = @UserID and Deleted=0";
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("UserID", UserID) });
                if (dt.Rows.Count > 0)
                {
                    string _lat = Lat;
                    string _lng = Lng;
                    MgoogpsWebClient mwc = new MgoogpsWebClient();
                    string access_token = AccessToken();
                    if (string.IsNullOrEmpty(access_token))
                        return "未获取到access_token.";
                    int count = 0;
                    string rulst = "";
                    string key = Utils.GetAmapKey();
                    string logName = "PushedMessage" + DateTime.Now.ToString("yyyyMM") + ".log";
                    if (Lat.toDouble() == -1.0 && Lng.toDouble() == -1.0)
                    {
                        Dictionary<string, string> dic = s.Selects("select OLat,OLng from lklocation where deviceid=@DeviceID", new SqlParameter[] { new SqlParameter("DeviceID", DeviceID) }).toDictionary();
                        Lat = dic["OLat"];
                        Lng = dic["OLng"];
                    }
                    Gps g = Utils.gps84_To_Gcj02(Lat, Lng, key);
                    mwc.RequestUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token;
                    string time = Date.toDateTime().ToString("yyyy-MM-dd HH:mm:ss");

                    //List<Task> taskList = new List<Task>();
                    // TaskFactory taskFactory = new TaskFactory();
                    // List<string> openids = new List<string>();
                    foreach (DataRow item in dt.Rows)
                    {
                        DataRow row = item;

                        //Task task = taskFactory.StartNew(() =>
                        // {
                        string openid = row["OpenID"].toStringEmpty();
                        string pushContent = GetAlarmPushText(openid, DeviceName, Message, time, g.Address, Remark, exceptionid);
                        mwc.RequestPostData = Encoding.UTF8.GetBytes(pushContent);
                        rulst = mwc.RequestSend();
                        Dictionary<string, string> res = Utils.ToDictionary(rulst);
                        Utils.log(string.Format("{0},{1},{2},{3},{4},{5}", UserID, row["LoginName"], DeviceName, openid, time, Message), logName);
                        if (res["errcode"].Equals("0") && res["errmsg"].Equals("ok"))
                        {
                            count++;
                            continue;
                            //return Utils.GetResult("发送成功.", statusCode.Code.success);
                        }
                        else if (res["errcode"] == "40001") //获取access_token时AppSecret错误，或者access_token无效。请开发者认真比对AppSecret的正确性，或查看是否正在为恰当的公众号调用接口
                        {
                            if (tryIndex < 1)
                            {
                                Utils.SetCache("access_token", "");
                                AccessToken();
                                tryIndex++;
                                return Pushed(UserID, DeviceName, Message, time, _lat, _lng, Remark, exceptionid, DeviceID);
                            }
                        }
                        else if (res["errcode"] == "43004") //接收者没有关注公众号
                        {
                            string sql = "delete from wechatusers where OpenID=@OpenID";
                            s.ExecuteSql(sql, new SqlParameter[] { new SqlParameter("OpenID", openid) });
                        }
                        Utils.log(string.Format("----发送失败 ：{0},{1},{2},{3},{4},{5}", UserID, row["LoginName"], DeviceName, openid, time, Message), logName);
                        Utils.log("----rulst ：" + rulst, logName);
                        // openids.Add(openid);
                        // return Utils.GetResult("发送失败.", statusCode.Code.failure);
                        //  });
                        //  taskList.Add(task);
                    }
                    //Task.WaitAll(taskList.ToArray());

                    if (count > 0)
                    {
                        return Utils.GetResult("发送成功.", statusCode.Code.success);
                    }
                    else
                    {
                        return Utils.GetResult("发送失败.", statusCode.Code.failure, rulst);
                    }
                }
                return Utils.GetResult("该用户未绑定微信.", statusCode.Code.failure);
            }
            catch (Exception ex)
            {
                Utils.log("Pushed ERROR:" + ex.Message + ",堆栈:" + ex.StackTrace);
                throw ex;
            }

        }

        public string AccessToken()
        {
            string result = "";
            MgoogpsWebClient mwc = new MgoogpsWebClient();
            try
            {
                //string token = ConfigurationManager.AppSettings["token"].ToStringEmpty();
                //if (!this.bool_token)
                //{
                //    return Utils.GetResult("token错误!", statusCode.Code.failure);
                //}

             
                string wx_access_token = Utils.GetCache<string>("access_token");
                if (string.IsNullOrEmpty(wx_access_token))
                {
                    mwc.RequestUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + this.wx_appid + "&secret=" + this.wx_secret;
                   
                    mwc.RequestMethodType = "GET";
                    result = mwc.RequestSend();
                    // {"access_token":"dV2gTgBkohEiOm8rnC6eJ2YHFTNdBXe5vFizvwJShc2z0lLLuE9TvidwWFQNuezOJ3IGswlV5tPLAbASteP8myaahTbA7zLrnzeaF17KCgwVZX0SV1bbnNDLl31ULqb3JMKgCEAUXV","expires_in":7200}
                    Dictionary<string, string> res = Utils.ToDictionary(result);
                    wx_access_token = res["access_token"];
                    if (!string.IsNullOrEmpty(wx_access_token))
                    {
                        Utils.SetCache("access_token", wx_access_token, Convert.ToDouble(res["expires_in"]) / 60);
                        return wx_access_token;
                    } 
                    return string.Empty;
                }
                return wx_access_token;
            }
            catch (Exception ex)
            { 
                Utils.log("WeixinOper.AccessToken:" + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 报警推送
        /// </summary>
        /// <param name="OpenID"></param>
        /// <param name="DeviceName">设备名称或者IMEI</param>
        /// <param name="Message">消息内容</param>
        /// <param name="Date">报警时间</param>
        /// <param name="address">报警地址</param>
        /// <param name="Remark">备注</param>
        /// <param name="exceptionid">报警消息ID</param>
        /// <returns></returns>
        private string GetAlarmPushText(string OpenID, string DeviceName, string Message, string Date, string address, string Remark, string exceptionid)
        {
            string temp = "{\"touser\": \"" + OpenID + "\"," +
                        "\"template_id\": \"DmSlT-3CT2xqMHm-tfAwLgt-yU7nh30759L1oemqSM0\", " +
                        "\"topcolor\": \"#FF0000\", " +
                        "\"url\":\"http://m.mgoogps.com:8070/message/msgdetail.html?id=" + exceptionid + "&t=" + DateTime.Now.Ticks + "\"," +
                        "\"data\": " +
                        "{\"first\": {\"value\": \"" + Message + "\",\"color\":\"#498FF4\"}," +
                        "\"keyword1\": { \"value\": \"" + DeviceName + "\",\"color\":\"#498FF4\"}," +
                        "\"keyword2\": { \"value\": \"" + Date + "\",\"color\":\"#498FF4\"}," +
                        "\"keyword3\": { \"value\": \"" + address + "\",\"color\":\"#498FF4\"}," +
                        "\"remark\": {\"value\": \"" + Remark + "\" ,\"color\":\"#FF0000\"}}}";
            return temp;
        }

        /// <summary>
        /// 设备过期提醒通知
        /// </summary>
        /// <param name="OpenID">openid</param>
        /// <param name="DeviceID">设备ID</param>
        /// <param name="DeviceName">设备名称或者IMEI</param>
        /// <param name="ExpireDate">到期时间</param>
        /// <returns></returns>
        public string GetExpiresPushText(string OpenID,string DeviceID,string DeviceName,string ExpireDate)
        {
            string xml =   "{\"touser\": \"" + OpenID + "\"," +
                          "\"template_id\": \"H3LFc8ySkngM2vCS07Yznfbd5yXb0KzsM4aJgOe15TQ\", " +
                          "\"topcolor\": \"#FF0000\", " +
                          "\"url\":\"http://m.mgoogps.com:8070/pay/Recharge.aspx?deviceid=" + DeviceID + "&openid="+OpenID+"&t=" + DateTime.Now.Ticks + "\"," +
                          "\"data\": " +
                          "{\"first\": {\"value\": \"您好,您有设备即将到期,请尽快充值!\",\"color\":\"red\"}," +
                          "\"keyword1\": { \"value\": \"" + DeviceName + "\",\"color\":\"red\"}," +
                          "\"keyword2\": { \"value\": \"" + ExpireDate + "\",\"color\":\"red\"}," + 
                          "\"remark\": {\"value\": \"点击在线充值\" ,\"color\":\"#FF0000\"}}}";
            return xml; 
        }

        public string GetPaySuccessText(string OpenID,string DeviceName,string Fee,string PayDate,string OrderNo,string BankName,string ProductBody,string year)
        { 
            string xml = "{\"touser\": \"" + OpenID + "\"," +
                         "\"template_id\": \"5JlOn2rDjm6hVlEPtXtdWfoI6_pebHlFAZ9Xy4MuTrI\", " +
                         "\"topcolor\": \"#FF0000\", " +
                         "\"url\":\"\"," + //http://m.mgoogps.com:8070/pay/Recharge.aspx?deviceid=" + DeviceID + "&openid=" + OpenID + "&t=" + DateTime.Now.Ticks + "
                         "\"data\": " +
                         "{\"first\": {\"value\": \""+ DeviceName+"已成功续费"+ year + "年" + "\",\"color\":\"#498FF4\"}," +
                         "\"keyword1\": { \"value\": \"" + (Convert.ToDouble(Fee) / 100).ToString("0.00")+"元" + "\",\"color\":\"#498FF4\"}," +
                         "\"keyword2\": { \"value\": \"" + BankName + "\",\"color\":\"#498FF4\"}," +
                         "\"keyword3\": { \"value\": \"" + ProductBody + "\",\"color\":\"#498FF4\"}," +
                         "\"keyword4\": { \"value\": \"" + OrderNo + "\",\"color\":\"#498FF4\"}," +
                         "\"keyword5\": { \"value\": \"" + "流量充值将在24小时内到账。" + "\",\"color\":\"#498FF4\"}," +
                         "\"remark\": {\"value\": \"感谢您对美谷科技的支持!\" ,\"color\":\"#FF0000\"}}}";
            return xml;

        }
               
        public bool FilterMsgType(string UserID,string NotificationType)
        {
            try
            {
                string strSql = string.Format(" select PushAudio,PushShock,PushPeriod from UsersConfig where UserID=@UserID and (PushMsgType like '%,{0},%')", NotificationType);
                SQLServerOperating s = GetSQLServerOperating();
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("UserID", UserID) });
                if (dt.Rows.Count > 0)
                {
                    string period = dt.Rows[0]["PushPeriod"].ToString();
                    //1 是全天接收
                    if (period =="1")
                    {
                        return true;
                    }
                    var now = DateTime.Now;
                    var date1 = default(DateTime);
                    var date2 = default(DateTime);
                    //晚上接收
                    if (period == "2") 
                    { 
                         date1 = now.ToString("yyyy-MM-dd 20:00:00").toDateTime();
                         date2 = now.AddDays(1).ToString("yyyy-MM-dd 07:59:59").toDateTime();
                        if (now >= date1 && now <= date2) 
                            return true;
                        Utils.log("免打扰时间-晚上接收: UserID:" + UserID + ",NotificationType:" + NotificationType);
                    }
                    //白天接收
                    if (period == "3")
                    {
                         date1 = now.ToString("yyyy-MM-dd 08:00:00").toDateTime();
                         date2 = now.ToString("yyyy-MM-dd 19:59:59").toDateTime();
                        if (now>= date1 && now <= date2)
                            return true;
                        Utils.log("免打扰时间-白天接收: UserID:" + UserID + ",NotificationType:" + NotificationType);
                    } 
                    return false;
                } 
            }
            catch (Exception ex)
            {
                Utils.log("FilterMsgType Error:" + ex.Message); 
            }
            return false;
        }

        public Dictionary<string,string> GetExceptionByID(string ExceptionID)
        {
            string strSql = @"select d.UserID,u.UserName,ISNULL(d.ServerID2,1)ServerID2,case when d.DeviceName = '' then  d.SerialNumber else d.DeviceName end DeviceName,em.DeviceID,d.Model,d.CellPhone DeviceCellPhone,d.SerialNumber,
                            case when geo.FenceName is null then em.Message else em.Message+':'+geo.FenceName end Message,em.NotificationType,DATEADD(HH,8,em.Created) Created,em.OLat,em.OLng ,u.CellPhone
                            from ExceptionMessage em inner join Devices d on em.DeviceID=d.DeviceID inner join Users u on u.UserID=d.UserID left join GeoFence geo on geo.GeofenceID=em.GeoFenceID  where ExceptionID = @ExceptionID";
            SQLServerOperating s = GetSQLServerOperating();
            return s.Selects(strSql,new SqlParameter[] { new SqlParameter("ExceptionID", ExceptionID) }).toDictionary();
        }



        /// <summary>
        /// 生成时间戳
        /// 从 1970 年 1 月 1 日 00：00：00 至今的秒数，即当前的时间，且最终需要转换为字符串形式
        /// </summary>
        /// <returns></returns>
        public string getTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <returns></returns>
        public string getNoncestr()
        {
            Random random = new Random();
            return MD5Util.GetMD5(random.Next(1000).ToString(), "GBK").ToLower();
        }
        public string Getsignature(string nonceStr, string timespanstr,string url)
        { 
            string wx_jsapi_ticket = Getjsapi_ticket();  
            string str = "jsapi_ticket=" + wx_jsapi_ticket + "&noncestr=" + nonceStr + "&timestamp=" + timespanstr + "&url=" + url;// +"&wxref=mp.weixin.qq.com";
            string singature = SHA1Util.getSha1(str).ToLower();
            return singature;
        }

        private string Getjsapi_ticket()
        {
            MgoogpsWebClient mwc = new MgoogpsWebClient();
            string wx_jsapi_ticket = Utils.GetCache<string>("jsapi_ticket");
            if (string.IsNullOrEmpty(wx_jsapi_ticket))
            {
                string wx_access_token = AccessToken();
                mwc.RequestUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token="+ wx_access_token + "&type=jsapi";
                mwc.RequestMethodType = "GET";
                string result = mwc.RequestSend();
                //{"errcode":0,"errmsg":"ok","ticket":"bxLdikRXVbTPdHSM05e5u5sUoXNKd8-41ZO3MhKoyN5OfkWITDGgnr2fwJ0m9E8NYzWKVZvdVtaUgWvsdshFKA","expires_in":7200}
                Dictionary<string, string> res = Utils.ToDictionary(result);
                wx_jsapi_ticket = res["ticket"];
                if (!string.IsNullOrEmpty(wx_jsapi_ticket))
                {
                    Utils.SetCache("jsapi_ticket", wx_jsapi_ticket, Convert.ToDouble(res["expires_in"]) / 60);
                    return wx_jsapi_ticket;
                }
                return string.Empty;
            }
            return wx_jsapi_ticket;
        }
    }
    /// <summary>
    /// MD5Util 的摘要说明。
    /// </summary>
    public class MD5Util
    {
        public MD5Util()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /** 获取大写的MD5签名结果 */
        public static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception ex)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }
    }
    class SHA1Util
    {
        public static String getSha1(String str)
        {
            //建立SHA1对象
            SHA1 sha = new SHA1CryptoServiceProvider();
            //将mystr转换成byte[] 
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(str);
            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            //将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");
            return hash;
        }
    }
}
