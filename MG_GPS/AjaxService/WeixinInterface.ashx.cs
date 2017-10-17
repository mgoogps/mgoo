using MG_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MG_GPS.AjaxService
{
    /// <summary>
    /// WeixinInterface 的摘要说明
    /// </summary>
    public class WeixinInterface : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            HttpResponse res = context.Response;
            HttpRequest req = context.Request;
           // for (int i = 0; i < req.Params.Count; i++)
           // {
              //  Utils.log(req.Params.AllKeys[i] + ":" + req.Params[i]);
           // }

            string signature = req["signature"];
            string timestamp = req["timestamp"];
            string nonce = req["nonce"];
            string echostr = req["echostr"];
            // log.logI("signature:" + signature); log.logI("timestamp:" + timestamp); log.logI("nonce:" + nonce); log.logI("echostr:" + echostr);
            string token = "0af6e4fc67874967";
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            // Array.Sort(  new Array() { "",""} );
            var arrString = string.Join("", arr);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));

            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }

            if (enText.ToString() == signature)
            {
                res.Output.Write(echostr);
            }
        
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}