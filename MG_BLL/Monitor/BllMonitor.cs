using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MG_DAL;
using System.Web.Script.Serialization;
using MG_BLL.Entity;
using System.Collections;

namespace MG_BLL
{
    public class BllMonitor : baseclass
    {
        public List<Dictionary<string, string>> GetUserZTree(String UserType = null)
        { 
            MgoogpsWebClient mwc = new MgoogpsWebClient();
            mwc.RequestMethodType = "GET";
            mwc.RequestMethodName = "/service/getusers/?user=" + SessionOper.GetSessionUserID();

            String jsonStr = mwc.RequestSend();
            List<Dictionary<string, string>> zTreeList = Utils.ToList(jsonStr);
            return zTreeList; 
        }


        /// <summary>
        /// 根据用户ID获取设备列表，包括该用户ID的报警消息条目
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="msgCount"></param>
        /// <returns></returns>
        public List<Entity.Devices> GetDevicesByUserID_Bll(String UserID, ref string msgCount)
        {
            List<Entity.Devices> deviceList = new List<Devices>();
            try
            {
                MgoogpsWebClient mwc = new MgoogpsWebClient();
                mwc.RequestMethodName = "/service/getlocations?user=" + UserID;
                mwc.RequestMethodType = "GET";
                String jsonStr = mwc.RequestSend();
                JavaScriptSerializer js = new JavaScriptSerializer();
                Dictionary<string, object> dic = js.Deserialize<Dictionary<string, object>>(jsonStr);
                if (dic.Count < 2)
                {
                    throw new ArgumentNullException(dic["error"].ToString());
                }
                msgCount = Convert.ToString(dic["alertcount"]);
                ArrayList list = dic["locatlist"] as ArrayList;

                DateTime connectTime;
                double lat, lng;
                Devices device;

                for (int i = 0; i < list.Count; i++)
                {
                    device = new Devices();
                    dic = list[i] as Dictionary<string, object>;
                    lat = 0; lng = 0;
                    connectTime = Convert.ToDateTime(dic["connecttime"]);
                    if (!string.IsNullOrWhiteSpace(dic["lbs"].toStringEmpty()))
                    {
                        Dictionary<string, object> lbs = (Dictionary<string, object>)dic["lbs"];
                        if (lbs["time"].toStringEmpty().toDateTime() > connectTime)
                        {
                            dic["lat"] = lbs["lat"]; dic["lon"] = lbs["lon"];
                        }
                        else if (Convert.ToDouble(dic["lat"]) == 0.00 || Convert.ToDouble(dic["lon"]) == 0.00)
                        {
                            dic["lat"] = lbs["lat"]; dic["lon"] = lbs["lon"];
                        }
                        device.ConnectTime = lbs["time"].toStringEmpty().toDateTime().AddHours(8).ToString();
                    }
                    Gps g = EvilTransform.PositionUtil.gcj02_To_Bd09(Convert.ToDouble(dic["lat"]), Convert.ToDouble(dic["lon"]));
                    lat = g.getWgLat();
                    lng = g.getWgLon(); 
                    device.Lat = lat + ""; device.Lon = lng + "";

                    device.ConnectTime = connectTime.ToString("yyyy-MM-dd HH:mm:ss");
                    device.Id = Convert.ToString(dic["id"]);
                    device.Name = Convert.ToString(dic["name"]);
                    device.EndTime = Convert.ToString(dic["endtime"]); device.DeviceTime = Convert.ToString(dic["devicetime"]);
                    device.Direction = Convert.ToString(dic["direction"]);
                    device.Speed = Convert.ToString(dic["speed"]); device.Status = Convert.ToString(dic["status"]);
                    device.OnLine = !((DateTime.Now - connectTime).TotalMinutes > Utils.OffLineMinute);

                    if (connectTime < Convert.ToDateTime("0010-01-01"))
                    {
                        device.DeviceStatus = (int)DeviceStatus.未激活 + "," + device.Speed;
                    }
                    else if ((DateTime.Now - connectTime).TotalMinutes > Utils.OffLineMinute)
                    {
                        device.DeviceStatus = (int)DeviceStatus.离线 + "," + Convert.ToInt32((DateTime.Now - connectTime).TotalMinutes);
                    }
                    else if (Convert.ToInt32(device.Speed) > 0)
                    {
                        device.DeviceStatus = (int)DeviceStatus.行驶 + "," + Convert.ToDouble(device.Speed) + "km";
                    }
                    else if (Convert.ToInt32(device.Speed) <= 0 && (DateTime.Now - connectTime).TotalMinutes <= Utils.OffLineMinute)
                    {
                        device.DeviceStatus = (int)DeviceStatus.停止 + "," + (DateTime.Now - Convert.ToDateTime(connectTime)).TotalMinutes;
                    }
                    deviceList.Add(device);
                }
                return deviceList;
            }
            catch (Exception ex)
            {
                Utils.log("GetDevicesByUserID_Bll方法出错：" + ex.Message + ", 堆栈信息：" + ex.StackTrace);
                return deviceList;
            }
        }

        /// <summary>
        /// 根据IMEI号获取设备信息（实时跟踪也用此方法）
        /// </summary>
        /// <param name="Imei"></param>
        /// <returns></returns>
        public string GetDeviceData(string Imei)
        {
            try
            {
                MgoogpsWebClient mwc = new MgoogpsWebClient();
                mwc.RequestMethodName = "/service/getlocationbydevice/?id=" + Imei;
                mwc.RequestMethodType = "GET";
                string jsonStr = mwc.RequestSend();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Entity.DeviceDetail device = jss.Deserialize<Entity.DeviceDetail>(jsonStr);
                DateTime connectTime = Convert.ToDateTime(device.ConnectTime);

                if (connectTime < Convert.ToDateTime("0010-01-01"))
                {
                    device.DeviceStatus = (int)DeviceStatus.未激活 + "," + device.Speed;
                }
                else if ((DateTime.Now - connectTime).TotalMinutes > Utils.OffLineMinute)
                {
                    device.DeviceStatus = (int)DeviceStatus.离线 + "," + Convert.ToInt32((DateTime.Now - connectTime).TotalMinutes);
                }
                else if (Convert.ToInt32(device.Speed) > 0)
                {
                    device.DeviceStatus = (int)DeviceStatus.行驶 + "";
                }
                else if (Convert.ToInt32(device.Speed) <= 0 && (DateTime.Now - connectTime).TotalMinutes <= Utils.OffLineMinute)
                {
                    device.DeviceStatus = (int)DeviceStatus.停止 + "," + (DateTime.Now - Convert.ToDateTime(connectTime)).TotalMinutes; ;
                }
                double lat = 0, lng = 0;
                if (!string.IsNullOrWhiteSpace(device.Lbs.toStringEmpty()))
                {
                    Dictionary<string, object> lbs = (Dictionary<string, object>)device.Lbs;
                    if (lbs["time"].toStringEmpty().toDateTime() > device.ConnectTime.toDateTime())
                    {
                        device.Lat = lbs["lat"].toStringEmpty(); device.Lon = lbs["lon"].toStringEmpty();
                    }
                    else if (device.Lat.toDouble() == 0.00 || device.Lon.toDouble() == 0.00)
                    {
                        device.Lat = lbs["lat"].toStringEmpty(); device.Lon = lbs["lon"].toStringEmpty();
                    }
                    device.DeviceTime = lbs["time"].toStringEmpty().toDateTime().AddHours(8).ToString();
                }
                Gps gps = EvilTransform.PositionUtil.gcj02_To_Bd09(double.Parse(device.Lat), double.Parse(device.Lon));
                lat = gps.getWgLat();
                lng = gps.getWgLon();
                device.Lat = lat + ""; device.Lon = lng + "";

                device.Address = bllGetAddress(double.Parse(device.Lat), double.Parse(device.Lon));
                return jss.Serialize(device);
            }
            catch (Exception ex)
            {
                Utils.log("GetDeviceData方法出错：" + ex.Message + ", 堆栈信息：" + ex.StackTrace);
                throw ex;
            }

        }
        /// <summary>
        /// 获取用户下所有设备的报警信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<Alarms> GetAlarms(string UserID)
        {
            MgoogpsWebClient mwc = new MgoogpsWebClient();
            mwc.RequestMethodName = "/service/getalarms/?user=" + UserID;
            mwc.RequestMethodType = "GET";
            string jsonStr = mwc.RequestSend();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Alarms> list = js.Deserialize<List<Alarms>>(jsonStr);
            double lat = 0, lng = 0;
            for (int i = 0; i < list.Count; i++)
            {
               Gps g = EvilTransform.PositionUtil.gcj02_To_Bd09(Convert.ToDouble(list[i].Lat), Convert.ToDouble(list[i].Lon)); 
                lat = g.getWgLat();
                lng = g.getWgLon();
                list[i].Lat = lat.ToString(); list[i].Lon = lng.ToString(); 
            }
            return list;
        }
        /// <summary>
        /// 标记已读消息
        /// </summary>
        /// <param name="ObjectID">消息ID</param>
        /// <returns></returns>
        public string DeleteMsg(string ObjectID)
        {
            MgoogpsWebClient mwc = new MgoogpsWebClient();
            mwc.RequestMethodName = "/service/deletealarmone/?objectId=" + ObjectID;
            mwc.RequestMethodType = "DELETE";
            mwc.RequestPostData = Encoding.UTF8.GetBytes("objectId=" + ObjectID);
            return mwc.RequestSend();
        }

        /// <summary>
        /// 根据经纬度获取中文地址
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="mapType">地图类型，默认（BAIDU）</param>
        /// <param name="language">语言，默认（ZH-CN）</param>
        /// <returns></returns>
        public string bllGetAddress(double lat, double lon, string mapType = null, string language = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mapType))
                {
                    mapType = "BAIDU";
                }
                if (string.IsNullOrWhiteSpace(language))
                {
                    language = "ZH-CN";
                }
                // POIService.POIServiceSoapClient ps = new POIService.POIServiceSoapClient();
                // return ps.GetAddressByLatlng((decimal)lat, (decimal)lon, mapType, language);
                return "";
            }
            catch
            {
                return "未知";
            }

        }
        /// <summary>
        /// 根据IMEI获取历史数据
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public List<History> GetHistory(string ID, string StartTime, string EndTime)
        {
            try
            {
                MgoogpsWebClient mwc = new MgoogpsWebClient();
                mwc.RequestMethodName = "/service/gethistory/?param=id," + ID + "&starttime," + Convert.ToDateTime(StartTime).Ticks + "&endtime," + Convert.ToDateTime(EndTime).Ticks;
                mwc.RequestPostData = new byte[0];
                mwc.RequestContentType = "application/x-www-form-urlencoded";
                string data = mwc.RequestSend();
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                List<History> his = js.Deserialize<List<History>>(data);
                List<History> retData = new List<History>();
                double lat = 0, lng = 0;
                foreach (History item in his)
                {
                    if (!string.IsNullOrWhiteSpace(item.Lat) || !string.IsNullOrWhiteSpace(item.Lon))
                    {
                        Gps g = EvilTransform.PositionUtil.gcj02_To_Bd09(Convert.ToDouble(item.Lat), Convert.ToDouble(item.Lon));
                        lat = g.getWgLat();
                        lng = g.getWgLon();
                        item.Lat = lat.ToString(); item.Lon = lng.ToString();
                        retData.Add(item);
                    }
                }
                return retData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据IMEI号获取设备的信息
        /// </summary>
        /// <param name="imei"></param>
        /// <returns></returns>
        public string GetDeviceInfo(string imei)
        {
            MgoogpsWebClient webClient = new MgoogpsWebClient();
            webClient.RequestMethodType = "GET";
            webClient.RequestMethodName = "/service/getdevicebyid/?id=" + imei;
            string str = webClient.RequestSend();
            JavaScriptSerializer js = new JavaScriptSerializer();
            DeviceInfo info = js.Deserialize<DeviceInfo>(str);
            return js.Serialize(info);
        }

        /// <summary>
        /// 修改指定设备的信息
        /// </summary>
        /// <param name="id">IMEI</param>
        /// <param name="name">设备名称</param>
        /// <param name="carnumber">车牌号</param>
        /// <param name="contact">联系人</param>
        /// <param name="cellphone">联系号码</param>
        /// <param name="image">图像名称</param>
        /// <param name="islbs">是否启用lbs</param>
        /// <returns></returns>
        public string SetDeviceInfo(string id, string name, string carnumber, string contact, string cellphone, string image = null, string islbs = null)
        {
            MgoogpsWebClient webClient = new MgoogpsWebClient();
            webClient.RequestMethodType = "POST";
            string par = "/service//modifydevice/?param=id," + id + "&name," + name + "&carnumber," + carnumber + "&contact," + contact + "&cellphone," + cellphone;
            if (image != null)
            {
                par += "&image," + image;
            }
            if (islbs != null)
            {
                par += "&islbs," + islbs;
            }
            webClient.RequestMethodName = par;
            return webClient.RequestSend();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetUserInfo()
        {
            MgoogpsWebClient webClient = new MgoogpsWebClient();
            webClient.RequestMethodType = "GET";
            webClient.RequestMethodName = "/service/getuserinfo/?user=" + Utils.GetSessionUserID();
            return webClient.RequestSend();
        }

        public string SetUserInfo(string user, string username, string email, string address)
        {
            MgoogpsWebClient webClient = new MgoogpsWebClient();
            webClient.RequestMethodName = "/service/modifyuser/?param=user," + user + "&username," + username + "&email," + email + "&address," + address;
            string str = webClient.RequestSend();
            if (str == "1")
            {
                Utils.SetSessionUserName(username);
            }
            return str;
        }

        public string AddGroups(string id,string parent,string username,string password,string cellphone,string address,string path ,string email,string weixin)
        {
            MgoogpsWebClient webClient = new MgoogpsWebClient();

            webClient.RequestPostData = Encoding.UTF8.GetBytes(string.Format("_id={0}&parent={1}&username={2}&password={3}&cellphone={4}&address={5}&path={6}&email={6}&weixin={7}"
                                                         , id,parent,username, "123456","135445","东莞","path","weixin")); // //

            webClient.RequestMethodName = "account/AddAccount/";
            string str = webClient.RequestSend();
        //public string _id { get; set; }
        //public string cellphone { get; set; }
        //public string username { get; set; }
        //public string password { get; set; }
        //public string address { get; set; }
        //public string parent { get; set; }
        //public string path { get; set; }
        //public string email { get; set; }
        //public string weixin { get; set; }

            return str;  
        }

    }
}
