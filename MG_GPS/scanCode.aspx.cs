using MG_BLL;
using MG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MG_GPS
{
    public partial class scanCode : System.Web.UI.Page
    {
        public string times;
        public string noncestr;
        public string signature;
        public string appid = "wxda27104d229a3608";
        protected void Page_Load(object sender, EventArgs e)
        {
            times = getTimestamp();
            noncestr = getNoncestr();
            signature= Getsignature(noncestr, times);
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
        public string Getsignature(string nonceStr, string timespanstr )
        {
            string url = HttpContext.Current.Request.Url.ToString();
            string wx_jsapi_ticket = Getjsapi_ticket();
            string str = "jsapi_ticket=" + wx_jsapi_ticket + "&noncestr=" + nonceStr + "&timestamp=" + timespanstr + "&url=" + url;// +"&wxref=mp.weixin.qq.com";
            string singature = SHA1Util.getSha1(str).ToLower();
            return singature;
        }
        public string AccessToken()
        {
            try
            {
                //string token = ConfigurationManager.AppSettings["token"].ToStringEmpty();
                //if (!this.bool_token)
                //{
                //    return Utils.GetResult("token错误!", statusCode.Code.failure);
                //}

                MgoogpsWebClient mwc = new MgoogpsWebClient();
                string wx_access_token = (string)HttpRuntime.Cache.Get("access_token");// Utils.GetCache<string>("access_token");
                if (string.IsNullOrEmpty(wx_access_token))
                {
                    mwc.RequestUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxda27104d229a3608&secret=a754e16403dd067372dbfe2f17a6c587" ;
                    mwc.RequestMethodType = "GET";
                    string result = mwc.RequestSend();
                    // {"access_token":"dV2gTgBkohEiOm8rnC6eJ2YHFTNdBXe5vFizvwJShc2z0lLLuE9TvidwWFQNuezOJ3IGswlV5tPLAbASteP8myaahTbA7zLrnzeaF17KCgwVZX0SV1bbnNDLl31ULqb3JMKgCEAUXV","expires_in":7200}
                    Dictionary<string, string> res = MG_BLL.Utils.ToDictionary(result);
                    wx_access_token = res["access_token"];
                    if (!string.IsNullOrEmpty(wx_access_token))
                    {
                        HttpRuntime.Cache.Insert("access_token", wx_access_token, null, DateTime.Now.AddMinutes(Convert.ToDouble(res["expires_in"]) / 60), TimeSpan.Zero);
                     
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

        public string Getjsapi_ticket()
        {
            MgoogpsWebClient mwc = new MgoogpsWebClient();
            string wx_jsapi_ticket =(string )HttpRuntime.Cache.Get("jsapi_ticket"); 
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
                    HttpRuntime.Cache.Insert("jsapi_ticket", wx_jsapi_ticket, null, DateTime.Now.AddMinutes(Convert.ToDouble(res["expires_in"]) / 60), TimeSpan.Zero);
                     
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