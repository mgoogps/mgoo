using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services; 
using MG_BLL;

namespace MG_WeChat.ajax
{
    /// <summary>
    /// update 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class update : System.Web.Services.WebService
    {

        [WebMethod]
        public string appcheck_wgt(string version,string package)
        {
            string result;
            try
            {
                if (string.IsNullOrEmpty(version))
                {
                    return Utils.GetResult("未检测到新版本.", statusCode.Code.failure);
                }
                if (string.IsNullOrEmpty(package))
                {
                    package = "MgooGps";
                }
                DirectoryInfo dir = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"/update/" + package);
                FileInfo[] fi = dir.GetFiles("*.wgt");
                if (fi.Length <= 0)
                {
                    result = Utils.GetResult("已经是最新版本.", statusCode.Code.failure); 
                }
                DateTime time = new DateTime(2000, 1, 1);
                int index = 0;
                for (int i = 0; i < fi.Length; i++)
                {
                    if (fi[i].Name.EndsWith(".wgt") && fi[i].Name.Split('_').Length > 1)
                    {
                        if (fi[i].CreationTime > time)
                        {
                            time = fi[i].CreationTime;
                            index = i;
                        } 
                    } 
                }
                if (fi[index].Name.Split('_')[1].Replace(".wgt", "").CompareTo(version) == 1)// 比较ASC码，大返回1，等于返回0，小返回-1
                {
                    string domain = HttpContext.Current.Request.Url.Host.ToString(); //当前服务器的 域名 or ip
                    string port = HttpContext.Current.Request.Url.Port.ToString();  //当前访问的端口号
                    string url= "http://" + domain +":"+port+ "/update/"+package+"/" + fi[index].Name;
                    //有新版本，则返回下载地址.
                    result = Utils.GetResult("有新版本,请下载更新.", statusCode.Code.success, url);
                }
                else
                {
                    result = Utils.GetResult("已经是最新版本.", statusCode.Code.failure);
                }
            }
            catch (Exception ex)
            {
                result = Utils.GetResult(ex.Message, statusCode.Code.error);
            } 
            return result;
        }
        [WebMethod]
        public string appcheck_apk(string version,string package)
        {
            string result;
            try
            {
                if (string.IsNullOrEmpty(version))
                {
                    return Utils.GetResult("未检测到新版本.", statusCode.Code.failure);
                }
                if (string.IsNullOrEmpty(package))
                {
                    package = "MgooGps";
                }
                if (package == "JinNai")
                {
                    return Utils.GetResult("有新版本,请下载更新.", statusCode.Code.success, "http://120.76.152.131:8070/update/OuBaoYun/oubaoyun_1.0.14.apk");
                }
                DirectoryInfo dir = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"/update/"+package);
                FileInfo[] fi = dir.GetFiles("*.apk");
                DateTime time = new DateTime(2000, 1, 1);
                int index = 0;
                for (int i = 0; i < fi.Length; i++)
                {
                    string fiName = fi[i].Name;
                    if (fiName.EndsWith(".apk") && fiName.IndexOf("beta") >= 0 && fiName.Split('_').Length > 1)
                    {
                        index = i;
                        break;
                    }
                    if (fiName.EndsWith(".apk") && fiName.Split('_').Length > 1)
                    {
                        if (fi[i].CreationTime > time)
                        {
                            time = fi[i].CreationTime;
                            index = i;
                        }
                    }
                }
                if (fi[index].Name.Split('_')[1].Replace(".apk", "").CompareTo(version) == 1)// 比较ASC码，大返回1，等于返回0，小返回-1
                {
                    string domain = HttpContext.Current.Request.Url.Host.ToString(); //当前服务器的 域名 or ip
                    string port = HttpContext.Current.Request.Url.Port.ToString();  //当前访问的端口号
                    string url = "http://"+ domain + ":" + port + "/update/"+ package+"/" + fi[index].Name;
                    //有新版本，则返回下载地址.
                    result = Utils.GetResult("有新版本,请下载更新.", statusCode.Code.success, url);
                }
                else
                {
                    result = Utils.GetResult("已经是最新版本.", statusCode.Code.failure);
                }
            }
            catch (Exception ex)
            {
                result = Utils.GetResult(ex.Message, statusCode.Code.error);
            }
            return result; 
        }
    }
}
