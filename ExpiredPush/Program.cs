using MG_BLL;
using MG_BLL.Weixin;
using MG_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics; 
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ExpiredPush
{
    class Program
    {
        private static string connectionString{ get; set; }
        private static string log_name = "ExpiredPush" + DateTime.Now.ToString("yyyyMM") + ".log";
        /// <summary>
        /// 通过windows系统新建计划任务，每天晚上调用exe程序，
        /// exe程序将查询数据库视图得到即将过期的设备，然后在调用webservice进行APP和微信的推送通知用户
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            connectionString = "database=YiwenGPS;user=sa;pwd=mgoo2016;Data Source=.;Max Pool Size = 512;";
                 
            if (args.Length == 2)
            { 
                //离线报警推送
                string date =  Convert.ToDateTime(args[0] + " " + args[1]).ToString("yyyy-MM-dd HH:mm:ss");
                OffLineMessagePush(date);
            }
            else
            { 
                //app设备过期通知推送，支付功能没做，暂时不推送
                //Thread appPush = new Thread(new ThreadStart(QueryExpireDevices));
                //appPush.Start();
                //微信过期通知
                Thread weChatPush = new Thread(new ThreadStart(WeChatExpirePush));
                weChatPush.Start();
            }
        }
        private static string server_host_test = "http://121.41.16.92/smsSend.do";
        private static string server_host = "http://120.55.248.18/smsSend.do";

        private  static void SMSSend()
        {

            string phone = "18507480591";  
            var password = GetMD5( "mgdz" + GetMD5("hI6eI2cN"));  
            string content = string.Format("【美谷科技】您的设备{0}，在{1}触发了{2}，请留意。", "测试", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "震动报警");
             
            string[] parsList = new string[4];
            parsList[0] = "username=mgdz";
            parsList[1] = "password=" + password;
            parsList[2] = "mobile=" + phone;
            parsList[3] = "content="+content;// + HttpUtility.UrlEncode(content ,Encoding.UTF8) ;
            var postData  = string.Join("&", parsList);

            Console.WriteLine(postData);

            MgoogpsWebClient webClient = new MgoogpsWebClient();
            webClient.RequestMethodType = "POST";
            webClient.RequestPostData = Encoding.UTF8.GetBytes(postData);
            webClient.RequestContentType = "application/x-www-form-urlencoded";
            webClient.RequestUrl = server_host;// +"?"+postData;
            string res = webClient.RequestSend();


           // MG_BLL.Pay.WeixinPay.lib.HttpService.Post();
            Console.WriteLine(res);
            Console.Read();
        }
        public static string GetMD5(string encypStr, string charset="UTF-8")
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
            retStr = retStr.Replace("-", "").ToLower();
            return retStr;
        }
        private static void QueryExpireDevices()
        {
             
            //30天内一个星期推送一次，7天内一天推送一次
            try
            { 
                //发送推送给30天内过期并且在七天内没有发送过推送的设备，APP平台

                MG_DAL.SQLServerOperating s = new MG_DAL.SQLServerOperating(connectionString);
                string strSql = @"SELECT d.UserID, d.DeviceID, d.DeviceName, d.SerialNumber, d.HireExpireDate, ma.ClientID, ma.OS, a.PackageName, a.AppID, a.AppKey, a.AppSecret, epr.AppLastPushTime
                                FROM dbo.Devices AS d INNER JOIN
                                    dbo.MobileAppInfo AS ma ON ma.UserID = d.UserID INNER JOIN
                                    dbo.Apps AS a ON a.ID = ma.AppsID LEFT OUTER JOIN
                                    dbo.ExpiredPushRecord AS epr ON epr.UserID = d.UserID
                                WHERE d.Deleted=0
                                AND d.HireExpireDate > GETDATE() 
                                AND(a.PackageName <> 'HBuilder') 
                                AND ma.LastDate>DATEADD(MM,-2,GETDATE())  ";
                //AND d.UserID in (6,7)
                //一个月内过期的设备并且是7天内没有推送过期通知的设备
                string monthWhere = " AND d.HireExpireDate < DATEADD(MM, 1, GETDATE()) AND(DATEDIFF(day, epr.AppLastPushTime, GETDATE()) > 7 or epr.AppLastPushTime IS NULL) ";
                //7天内过期的设备，一天推送一次
                string weekWhere = " AND d.HireExpireDate < DATEADD(DAY, 7, GETDATE()) AND(DATEDIFF(day, epr.AppLastPushTime, GETDATE()) >= 1 or epr.AppLastPushTime IS NULL) ";

                /// 查询7天内过期的设备进行APP推送
                DataTable dt = s.Selects(strSql + weekWhere);
                MG_BLL.App.XiaoMiPush.Push push = new MG_BLL.App.XiaoMiPush.Push();
                foreach (DataRow row in dt.Rows)
                {
                    string DeviceName = string.IsNullOrEmpty(row["DeviceName"].ToString()) ? row["SerialNumber"].ToString() : row["DeviceName"].ToString();
                    push.ExpiredPush(row["PackageName"].ToString(), row["AppSecret"].ToString(), row["ClientID"].ToString(), row["OS"].ToString(), row["DeviceID"].ToString(), DeviceName, row["HireExpireDate"].ToString());
                    ExpiredPushRecord(row["UserID"].ToString(), row["DeviceID"].ToString(), row["HireExpireDate"].ToString(), "app");
                }
                //查询30天内过期的设备并且在7天内没有进行过推送的设备进行APP推送
                dt = s.Selects(strSql + monthWhere);
                foreach (DataRow row in dt.Rows)
                {
                    string DeviceName = string.IsNullOrEmpty(row["DeviceName"].ToString()) ? row["SerialNumber"].ToString() : row["DeviceName"].ToString();
                    push.ExpiredPush(row["PackageName"].ToString(), row["AppSecret"].ToString(), row["ClientID"].ToString(), row["OS"].ToString(), row["DeviceID"].ToString(), DeviceName, row["HireExpireDate"].ToString());
                    ExpiredPushRecord(row["UserID"].ToString(), row["DeviceID"].ToString(), row["HireExpireDate"].ToString(), "app");
                }
            }
            catch (Exception ex)
            {
                Utils.log("QueryExpireDevices Error:" + ex.Message, log_name);
            }
        }

        private static void WeChatExpirePush()
        {
            try
            { 
                MG_DAL.SQLServerOperating s = new MG_DAL.SQLServerOperating(connectionString);
                string strSql = @"select chat.OpenID,chat.UserID,d.DeviceName,d.DeviceID,d.SerialNumber,d.HireExpireDate,epr.WeChatLastPushTime from 
                                WeChatUsers chat inner join Devices d  on d.UserID=chat.UserID 
                                LEFT OUTER JOIN dbo.ExpiredPushRecord AS epr ON epr.DeviceID = d.DeviceID
                                where d.Deleted = 0  
                                AND d.HireExpireDate > GETDATE()  
                                AND chat.UpdateTime>DATEADD(MM,-2,GETDATE()) 
                              ";///  AND d.UserID in (6,7)
                //一个月内过期的设备并且是7天内没有推送过期通知的设备
                string monthWhere = " AND d.HireExpireDate < DATEADD(MM, 1, GETDATE()) AND(DATEDIFF(day, epr.WeChatLastPushTime, GETDATE()) >= 7 or epr.WeChatLastPushTime IS NULL) ";
                //7天内过期的设备，一天推送一次
                string weekWhere = " AND d.HireExpireDate < DATEADD(DAY, 7, GETDATE()) AND(DATEDIFF(day, epr.WeChatLastPushTime, GETDATE()) >= 1 or epr.WeChatLastPushTime IS NULL) ";
                string logText = "";
                DataTable dt = s.Selects(strSql + weekWhere);
                logText = "7天内过期设备： "+dt.Rows.Count+" 台";
                WeixinOper wo = new WeixinOper();
                string access_token = wo.AccessToken();
                MgoogpsWebClient mwc = new MgoogpsWebClient();
                mwc.RequestUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token;
                string rulst = "";

                foreach (DataRow row in dt.Rows)
                {
                    string DeviceName = string.IsNullOrEmpty(row["DeviceName"].ToString()) ? row["SerialNumber"].ToString() : row["DeviceName"].ToString();
                    string pushContent = wo.GetExpiresPushText(row["OpenID"].ToString(), row["DeviceID"].ToString(), DeviceName, row["HireExpireDate"].ToString());
                    mwc.RequestPostData = Encoding.UTF8.GetBytes(pushContent);
                    rulst = mwc.RequestSend();
                    Console.WriteLine(rulst);
                    Dictionary<string, string> res = Utils.ToDictionary(rulst);
                    if (res["errcode"].Equals("0") && res["errmsg"].Equals("ok"))
                    {
                        ExpiredPushRecord(row["UserID"].ToString(), row["DeviceID"].ToString(),row["HireExpireDate"].ToString(), "wechat");
                    }
                    else
                    {
                        Utils.log("7天内过期推送失败" + rulst, log_name);
                    }
                }

                dt = s.Selects(strSql + monthWhere);
                logText += "        30天内过期设备： " + dt.Rows.Count + " 台";
                foreach (DataRow row in dt.Rows)
                {
                    string DeviceName = string.IsNullOrEmpty(row["DeviceName"].ToString()) ? row["SerialNumber"].ToString() : row["DeviceName"].ToString();
                    string pushContent = wo.GetExpiresPushText(row["OpenID"].ToString(), row["DeviceID"].ToString(), DeviceName, row["HireExpireDate"].ToString());
                    mwc.RequestPostData = Encoding.UTF8.GetBytes(pushContent);
                    rulst = mwc.RequestSend();
                    Dictionary<string, string> res = Utils.ToDictionary(rulst);
                    if (res["errcode"].Equals("0") && res["errmsg"].Equals("ok"))
                    {
                        ExpiredPushRecord(row["UserID"].ToString(), row["DeviceID"].ToString(), row["HireExpireDate"].ToString(), "wechat");
                    }
                    else
                    {
                        Utils.log("30天内过期推送失败" + rulst, log_name);
                    }
                }
                Utils.log(logText,log_name);
            }
            catch (Exception ex)
            {
                Utils.log("WeChatExpirePush Error:"+ex.Message, log_name);
            }
        }
         
        private static void ExpiredPushRecord(string UserID,string DeviceID,string ExpireDate,string Platform)
        {
            try
            {
                SQLServerOperating s = new SQLServerOperating(connectionString);
                var days = (ExpireDate.toDateTime() - DateTime.Now).TotalDays; 
                if (Platform == "app")
                {
                    string strSql = @"IF EXISTS(select UserID from ExpiredPushRecord where DeviceID = @DeviceID)
                                 update ExpiredPushRecord set AppLastPushTime = GETDATE(),Days=@Days where DeviceID = @DeviceID
                                 ELSE
                                 Insert into ExpiredPushRecord(UserID, DeviceID, AppLastPushTime,Days)values(@UserID, @DeviceID, GETDATE(),@Days)";
                    s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("DeviceID", DeviceID), new SqlParameter("UserID", UserID),new SqlParameter("Days",days) });
                }
                else
                {
                    string strSql = @"IF EXISTS(select UserID from ExpiredPushRecord where DeviceID = @DeviceID)
                                 update ExpiredPushRecord set WeChatLastPushTime = GETDATE(),Days=@Days  where DeviceID = @DeviceID
                                 ELSE
                                 Insert into ExpiredPushRecord(UserID,DeviceID, WeChatLastPushTime,Days)values(@UserID,@DeviceID, GETDATE(),@Days)";
                    s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("UserID", UserID), new SqlParameter("DeviceID", DeviceID),new SqlParameter("Days",days) });
                }
            }
            catch (Exception ex)
            {
                Utils.log("ExpiredPushRecord Error:"+ex.Message, log_name);
            } 
        }

        private static void OffLineMessagePush(string date)
        {
            try
            {
                //AccountID 1有线，2无线
                string strSql = @"select em.ExceptionID,d.DeviceID,em.NotificationType,em.Message,em.Created,em.OLat,em.OLng ,d.UserID ,
                              case when d.DeviceName ='' then d.SerialNumber else d.DeviceName end DeviceName
                              from ExceptionMessage em inner join Devices d on d.DeviceID=em.DeviceID inner join Dictionary di on di.DataValue =d.Model
                              where convert(varchar(16), dateadd(HH, 8, em.Created), 20) = convert(varchar(16), dateadd(mi,case when di.AccountID=1 then -di.SortOrder else 1450 end ,@date),20) and notificationtype = 9 ";

                SQLServerOperating s = new SQLServerOperating(connectionString);
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("date", date) });

                WeixinOper wo = new WeixinOper(s);
                int index = 0;
                // Console.WriteLine(dt.Rows.Count);
                foreach (DataRow row in dt.Rows)
                {
                    index++;
                    string res = wo.StartPush(row["UserID"].ToString(), row["ExceptionID"].ToString(), row["DeviceName"].ToString(), row["Message"].ToString(), row["Created"].ToString());
                    //  Console.WriteLine(index+":"+res);
                }
            }
            catch (Exception ex)
            {
                Utils.log("OffLineMessagePush Error:" + ex.Message+ " StackTrace:" + ex.StackTrace+ " Source:" + ex.Source, log_name);
            }
        }
    }
   
}
