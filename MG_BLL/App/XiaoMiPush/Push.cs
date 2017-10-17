using MG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.App.XiaoMiPush
{
    public class Push
    {
        private string HOST = "https://api.xmpush.xiaomi.com/v3/message/regid";
        public bool PushAudio { get; set; }
        public bool PushShock { get; set; }

        public int DefaulSound { set; get; }
        public void PushMessageToSingle(string regid, string title, string description, string appsecret, string packagename, string msgtype, string os , string exceptionid)
        { 
            if (os == "ios")
            {
                PushIOS(appsecret, regid, title, description, 600000, exceptionid);
            }
            else
            { 
                PushAndroid(packagename, appsecret, regid, title, description, "{'ExceptionID':" + exceptionid + "}", 600000, msgtype);
            }
            
        }

        public void ExpiredPush(string PackageName, string AppSecret, string ClientID, string OS, string DeviceID, string DeviceName, string ExpiredDate)
        {
            if (OS.ToLower() == "android")
            {
                PushAndroid(PackageName, AppSecret, ClientID, "过期提醒-" + DeviceName, "过期时间-" + ExpiredDate, "{'DeviceID':" + DeviceID + "}", 86400000, new Random().Next(10, 1000).ToString());
            }
            else
            {
                PushIOS(AppSecret,ClientID, "过期提醒-" + DeviceName, "过期时间-" + ExpiredDate, 86400000,"");
            }
        }
        private void PushAndroid(string PackageName, string AppSecret, string ClientID, string Title, string Description, string Payload, long TimeToLive, string NotifyId)
        {
            try
            {
                MgoogpsWebClient mgoo = new MgoogpsWebClient();
                mgoo.RequestUrl = HOST;
                mgoo.RequestMethodType = "POST";
                mgoo.RequestContentType = "application/x-www-form-urlencoded";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "key=" + AppSecret); //  LrXeTJHe6tWQ0rOo2pcqbQ== 
                StringBuilder sb = new StringBuilder();
                sb.Append("payload=" + Payload + "&"); //	消息的内容。
                sb.Append("restricted_package_name=" + PackageName + "&"); //App的包名。
                sb.Append("pass_through=0&"); //0 表示通知栏消息  1 表示透传消息
                sb.Append("title=" + Title + "&"); //通知栏展示的通知的标题。
                sb.Append("description='" + Description + "'&"); //	通知栏展示的通知的描述。
                sb.Append("notify_type="+this.DefaulSound+"&");  //DEFAULT_ALL = -1;  DEFAULT_SOUND  = 1;  // 使用默认提示音提示；DEFAULT_VIBRATE = 2;  // 使用默认震动提示；DEFAULT_LIGHTS = 4;   // 使用默认led灯光提示；
                sb.Append("time_to_live=" + TimeToLive + "&"); //1天 可选项。如果用户离线，设置消息在服务器保存的时间，单位：ms。服务器默认最长保留两周。
                sb.Append("notify_id=" + NotifyId + "&");
                // sb.Append("extra.notify_effect=3&");// 可选项 “3″：通知栏点击后打开网页（开发者还需要传入extra.web_uri）。
                //sb.Append("extra.web_uri=https://www.baidu.com&");//	可选项，打开某一个网页。参考2.2.3.3
                // sb.Append("extra.sound_uri=android.resource://com.mgoogps.oubaoyun/tuisong&");
                sb.Append("registration_id=" + ClientID + ""); //0czrCPue2ny19RfpAYNU44/n4niilHAHn48lNLWQTE0=   
                
                                                               //AcHKMOVNigJtkRluQU3FAyWnCbdsvoBYKGPdV1gLHJA=, rStPppMYedb5FCdnM/QPSWf0n+C61RhycDMz5JrFJBY=,SZzVN/bbQz0IhRSQ37M8EnvYPxYdaPOMj9F4M0QeivQ=,Sw/1lgU8KyA5zYiA3OrFT6cHoA5e7rpEuy5SAKdnn0g=,VOtpeNF2jSciN5cmDJFyVhkNCEvgMIr+weCzZPH7u94=
                mgoo.RequestPostData = Encoding.UTF8.GetBytes(sb.ToString());
                string reulst = mgoo.RequestSend(headers);
                Utils.log("Android---"+ Title+":" + reulst, "MiPush" + DateTime.Now.ToString("yyyyMM") + "-" + DateTime.Now.DayOfWeek + ".log");
            }
            catch (Exception ex)
            {
                Utils.log("PushAndroid Error:" + ex.Message+ ",Source:" + ex.Source+ ",StackTrace:" + ex.StackTrace);
            }
        }

        private void PushIOS(string AppSecret, string ClientID, string Title, string Body, long TimeToLive,string exceptionid)
        {
            try
            {
                MgoogpsWebClient mgoo = new MgoogpsWebClient();
                mgoo.RequestUrl = HOST;
                mgoo.RequestMethodType = "POST";
                mgoo.RequestContentType = "application/x-www-form-urlencoded";
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "key="+AppSecret);
                StringBuilder sb = new StringBuilder();
                // sb.Append("description=" + "测试推送" + "&");
                sb.Append("aps_proper_fields.title=" + Title + "&");
                //sb.Append("payload=" + payload + "&"); //	消息的内容。
                // sb.Append("aps_proper_fields.subtitle=" + "测试subtitle" + "&");
                sb.Append("aps_proper_fields.body=" + Body + "&");
                sb.Append("time_to_live=" + TimeToLive + "&");
                //sb.Append("extra.badge=1&"); //可选项。通知角标。
                sb.Append("aps_proper_fields.mutable-content=" + exceptionid + "&");
                sb.Append("extra.sound_url=default&"); //可选项，自定义消息铃声。当值为空时为无声，default为系统默认声音。
                sb.Append("registration_id=" + ClientID + "");
                mgoo.RequestPostData = Encoding.UTF8.GetBytes(sb.ToString());
                string reulst = mgoo.RequestSend(headers);
                Utils.log("IOS---" + reulst, "MiPush" + DateTime.Now.ToString("yyyyMM") + "-" + DateTime.Now.DayOfWeek + ".log");
            }
            catch (Exception ex)
            {
                Utils.log("PushIOS Error:" + ex.Message + ",Source:" + ex.Source + ",StackTrace:" + ex.StackTrace);
            }

        }
    }
}
