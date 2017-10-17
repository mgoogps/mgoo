using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Net.Http;
namespace MG_DAL
{
    public class MgoogpsWebClient
    {
       /// <summary>
       /// 发送请求(POST,GET),默认是POST请求
       /// </summary>
       /// <returns></returns>
        public String RequestSend(Dictionary<string, string> headers = null) 
        { 
            try
            {
                System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.RequestUrl + this.RequestMethodName);
                request.Method = this.RequestMethodType.ToUpper(); 
                if (ToKen != null)
                {
                    request.Headers.Add("ToKen", ToKen);
                }
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> item in headers)
                    {
                        request.Headers.Add(item.Key, item.Value);
                    }
                }
                if (this.RequestMethodType.ToUpper() == "POST")
                {
                    request.ContentType = this.RequestContentType; //采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
                    request.ContentLength = this.RequestPostData.Length;
                    request.Timeout = 6 * 1000;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(this.RequestPostData, 0, this.RequestPostData.Length);
                    }
                } 
                using (WebResponse response = (WebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"))) 
                    {
                        return reader.ReadToEnd();
                    }
                } 
            }
            catch (WebException wex)
            {
               // log("WebException:" + wex.Message + ",RequestUrl" + this.RequestUrl + "-" + this.RequestMethodName + " --- 堆栈：" + wex.StackTrace);
                HttpWebResponse errorResponse = wex.Response as HttpWebResponse;
                HttpStatusCode errorHttpStatusCode = errorResponse.StatusCode;
                if (errorHttpStatusCode.ToString() == "Unauthorized")
                {
                    SessionOper.RemoveSession();
                    throw new LoginInformationFail("ToKen已失效！");
                }
                throw wex;
            }
            catch (Exception ex)
            {
               // log("Exception:" + ex.Message + ",RequestUrl" + this.RequestUrl + "-" + this.RequestMethodName + " --- 堆栈：" + ex.StackTrace);
                throw ex;
            } 
        } 

        //public String RequestGet()
        //{ 
        //    WebClient webClient = new WebClient();
        //    webClient.Headers.Add("ToKen",ToKen);
        //    return webClient.DownloadString(this.RequestUrl + this.RequestMethodName);  
        //}
      
        private String requestUrl; 
        private String requestMethodType;
        private String requestContentType;
        private byte[] requestPostData;
        private static String toKen; 
        private String requestMethodName;

        /// <summary>
        /// 请求的地址，根据配置文件读取前半部分
        /// </summary>
        public string RequestUrl
        {
            get
            {
                if (requestUrl == null)
                {
                    requestUrl = ConfigurationManager.ConnectionStrings["url"].ConnectionString;
                   
                   // requestUrl = "http://api.mgoogps.com/";
                }
                return requestUrl;
            }

            set
            {
                requestUrl = value;
            }
        }
         
        /// <summary>
        /// 请求的方式(POST,GET,DELETE)，默认是POST
        /// </summary>
        public string RequestMethodType
        {
            get
            {
                if (requestMethodType == null)
                {
                    this.requestMethodType = "POST";
                }
                return requestMethodType;
            }

            set
            {
                requestMethodType = value;
            }
        }

        /// <summary>
        /// 数据格式以及编码方式
        /// </summary>
        public string RequestContentType
        {
            get
            {
                if (this.requestContentType == null)
                {
                    requestContentType = "application/json;charset=utf-8";
                }
                return requestContentType;
            }

            set
            {
                requestContentType = value;
            }
        }
         
        /// <summary>
        /// POST提交时的参数，默认是new byte[0]
        /// </summary>
        public byte[] RequestPostData
        {
            get
            {
                if (requestPostData == null)
                {
                    requestPostData = new byte[0];
                }
                return requestPostData;
            }

            set
            {
                requestPostData = value;
            }
        }
        /// <summary>
        /// 请求的方法名称
        /// </summary>
        public string RequestMethodName
        {
            get
            {
                return requestMethodName;
            }

            set
            {
                requestMethodName = value;
            }
        }

        /// <summary>
        /// 登录的Token
        /// </summary>
        public static string ToKen
        {
            get
            {
                return toKen;
            } 
            set
            {
                toKen = value;
            }
        }
         
        /// <summary>
        /// 写入Log信息
        /// </summary>
        /// <param name="LogStr">要写入的字符串</param>
        /// <param name="path">写入的路径(默认的是D:\Log)</param>
        public static void log(string LogStr, string path = null)
        {
            string p = "D://Log";
            if (path == null)
            {
                path = @"D:/Log/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            }
            else
            {
                p = path;
                path = p + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            }
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            if (!File.Exists(path))
            { 
                File.Create(path).Close();
            }
            StreamWriter sw = null;
            try
            {
                LogStr = DateTime.Now.ToLocalTime().ToString() + "  \n" + LogStr;
                sw = new StreamWriter(path, true);
                sw.WriteLine(LogStr);
            }
            catch (Exception  )
            {

            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }

        }

    }
}
