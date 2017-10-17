using MG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MG_BLL.Common.SMSService
{
    class ShangTong
    {
        

        private static void Send(string phone)
        {
            string server_host_test = "http://121.41.16.92/smsSend.do";
            string server_host = "http://120.55.248.18/smsSend.do";
             

            string username = "mgdz";
            MD5 md5 = MD5.Create("");
            var md5Byte = MD5.Create("mgdz" + "hI6eI2cN").ComputeHash(Encoding.UTF8.GetBytes(""));

            string password = System.BitConverter.ToString(md5Byte).Replace("-", "").ToUpper();

            string mobile = "18507480591";

            string content = string.Format("【美谷科技】您的设备{0}，在{1}触发了{2}，请留意。", "测试", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "震动报警");


            string[] parsList = new string[4];
            parsList[0] = "username=mgdz";
            parsList[1] = "password=" + password;
            parsList[2] = "mobile=" + phone;
            parsList[3] = "content=" + content;

            MgoogpsWebClient webClient = new MgoogpsWebClient();
            webClient.RequestMethodType = "POST";
            webClient.RequestContentType = "application/x-www-form-urlencoded";
            webClient.RequestPostData = Encoding.UTF8.GetBytes(string.Join("&", parsList));
            webClient.RequestUrl = server_host;
            string res = webClient.RequestSend();
             
        }
    }
}
