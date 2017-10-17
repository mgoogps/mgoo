using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace MG_BLL.Common.lib
{
    /// <summary>
    ///  MG-X21BF、MG-X83BF 两个型号 ，如果移动端 进入监控页面，则发送指令给设备，3秒回传一次数据，离开监控页面后发指令给设备30秒回传一次数据 ，以防止设备卡超流量产生其他费用
    ///  单例模式
    /// </summary>
    public class AoboCache
    {
        private static AoboCache _instance = null;

        private static readonly object lockHelper = new object();
         
        private List<string> list = new List<string>();

        private string CacheKeyImei { get; set; }
        private AoboCache() {   }
        
        public static AoboCache CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                    {
                        _instance = new AoboCache();
                    }
                }
            }
            return _instance;
        }

        public void Action(string Imei,string model)
        {
            if (model.StartsWith("MG-X21B"))
            {
                DateTime dateNow = DateTime.Now.AddMinutes(3);
                CacheKeyImei = "AoboCache_" + Imei;// + "_" + dateNow.Ticks;
                if (!GetCache())
                {
                    DeviceRate(Imei, 3);
                }
                SetCache(dateNow);
            } 
        }

        private void SetCache(DateTime absoluteExpiration)
        { 
            HttpRuntime.Cache.Insert(CacheKeyImei,
                absoluteExpiration,
                null,
                absoluteExpiration, 
                System.Web.Caching.Cache.NoSlidingExpiration, 
                System.Web.Caching.CacheItemPriority.Default,
                RemoveCallBack);
        }

        private bool GetCache()
        {
            return !(HttpRuntime.Cache.Get(CacheKeyImei) == null);
        }

        private void RemoveCallBack(string strIdentify, object Info, CacheItemRemovedReason reason)
        {
            if (Info.GetType().FullName == "System.DateTime")
            {
                var dateNow = DateTime.Now;
                DateTime expireDate = Info.ToString().toDateTime();
                if ((expireDate - dateNow).Milliseconds <= 0)
                {
                    var Imei = strIdentify.Split('_')[1];
                    DeviceRate(Imei,30);
                }
            }
        }

        private void DeviceRate(string Imei , int rate)
        {
            Task.Run(() => {
                string cmd = string.Format("VTR-Command-{0}-Rate,{1}", Imei, rate);
                string cmdRes = Utils.SendTcpCmd(cmd);
                Utils.log("AoboCache > cmd :" + cmd + ",return:" + cmdRes);
            }); 
        }
    }
}
