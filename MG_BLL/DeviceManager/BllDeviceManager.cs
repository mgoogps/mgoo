using MG_BLL.Entity;
using MG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections;

namespace MG_BLL.DeviceManager
{
    public class BllDeviceManager
    {
        public string GetDeviceList(string user,string devicename)
        {
            MgoogpsWebClient mwc = new MgoogpsWebClient();
            mwc.RequestMethodType = "GET";
            mwc.RequestMethodName = "/service/getdevices/?user=" + user;
            string json = mwc.RequestSend();


            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;
            List<DeviceInfo> dList = js.Deserialize<List<DeviceInfo>>(json);
            List<DeviceInfo> resultList;
            if (!string.IsNullOrWhiteSpace(devicename))
            {
                resultList = new List<DeviceInfo>();
                for (int i = 0; i < dList.Count; i++)
                {
                    if (dList[i].Name.IndexOf(devicename) >= 0 || dList[i].Id .IndexOf(devicename) >= 0)
                    {
                        resultList.Add(dList[i]);
                    }
                }
                if (resultList != null)
                {
                    return Utils.ToJson(resultList);
                }
            }
            return Utils.ToJson(dList);
        }
        //获得当前登录用户的设备数量
        public Dictionary<string, string> GetDeviceCount(string user)
        {
            try
            {
                MgoogpsWebClient mwc = new MgoogpsWebClient();
                mwc.RequestMethodType = "GET";
                //  http://api.mgoogps.com/service/getusers/?user=
                mwc.RequestMethodName = "/service/getlocations?user=" + user;
                string json = mwc.RequestSend();
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = int.MaxValue;
                Dictionary<string, object> total = js.Deserialize<Dictionary<string, object>>(json);
                ArrayList list = total["locatlist"] as ArrayList;

                //获取过期设备
                MgoogpsWebClient mvc = new MgoogpsWebClient();
                mvc.RequestMethodType = "GET";
                mvc.RequestMethodName = "/service/getdevices?user=" + user;
                string jsons = mvc.RequestSend();
                int days = 0;
                List<Dictionary<string, string>> totall = js.Deserialize<List<Dictionary<string, string>>>(jsons);
                for (int i = 0; i < totall.Count; i++)
                {
                    DateTime d = Convert.ToDateTime(totall[i]["activetime"]);
                    if ((DateTime.Now - d).TotalDays > 7 && d > DateTime.MinValue.AddHours(8))
                    {
                        days++;
                    }
                }

                int onLine = 0;
                int offLine = 0;
                int overdue = 0;
                int expire = 0;
                Dictionary<string, object> dic;
                for (int i = 0; i < list.Count; i++)
                {
                    dic = list[i] as Dictionary<string, object>;
                    DateTime act = Convert.ToDateTime(totall[i]["activetime"]);
                    DateTime connectTime = Convert.ToDateTime(dic["connecttime"]);
                    DateTime endtime = Convert.ToDateTime(dic["endtime"]);
                    //int days = Convert.ToInt32( activetime);
                    //在线设备
                    if ((DateTime.Now - connectTime).TotalMinutes < Utils.OffLineMinute)
                    {
                        onLine++;
                    }
                    //离线设备
                    if ((DateTime.Now - connectTime).TotalMinutes > Utils.OffLineMinute && connectTime > DateTime.MinValue.AddHours(8))
                    {
                        offLine++;
                    }
                    //未激活设备
                    //小于当前时间并且不等于0001-01-01

                    if (act <= DateTime.MinValue.AddHours(8))
                    {
                        overdue++;
                    }
                    if (endtime < DateTime.Now && endtime > DateTime.MinValue.AddHours(8))
                    {
                        expire++;
                    }
                }
                Dictionary<string, string> dicTotal = new Dictionary<string, string>();
                dicTotal["count"] = list.Count + "";
                dicTotal["onLine"] = onLine + "";
                dicTotal["offLine"] = offLine + "";
                dicTotal["days"] = days + "";
                dicTotal["overdue"] = overdue + "";
                dicTotal["expire"] = expire + "";

                return dicTotal;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取所有子用户的设备数量
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetALLUser(string user)
        {
            MgoogpsWebClient mvc = new MgoogpsWebClient();
            mvc.RequestMethodType = "GET";
            mvc.RequestMethodName = "/service/getusers/?user=" + user;
            string json = mvc.RequestSend();
            JavaScriptSerializer js = new JavaScriptSerializer();
 
            //dic为当前登录所有用户
            List<Dictionary<string, string>> dic = js.Deserialize<List<Dictionary<string, string>>>(json);//获得所有子用户
            int allcount = 0;//所有设备数量
            int allonLine = 0;//在线设备
            int alloffLine = 0;//离线身边
            int allday = 0;//七天
            int allnotactive = 0;
            int allexpire = 0;
            for (int i = 0; i < dic.Count; i++)
            { 
                Dictionary<string, string> total = GetDeviceCount(dic[i]["_id"]);

                allcount += Convert.ToInt32(total["count"]);
                allonLine += Convert.ToInt32(total["onLine"]);
                alloffLine += Convert.ToInt32(total["offLine"]);
                allday += Convert.ToInt32(total["days"]);
                allnotactive += Convert.ToInt32(total["overdue"]);
                allexpire += Convert.ToInt32(total["expire"]); 
            }
            Dictionary<string, string> dicc = new Dictionary<string, string>();
            dicc["allcount"] = allcount + "";
            dicc["allonLine"] = allonLine + "";
            dicc["alloffLine"] = alloffLine + "";
            dicc["alldays"] = allday + "";
            dicc["allnotactive"] =allnotactive+"";
            dicc["allexpire"] = allexpire + "";
            Dictionary<string, string> DeivceCount = GetDeviceCount(user); 
            dicc["count"] = DeivceCount["count"];
            dicc["onLine"] = DeivceCount["onLine"];
            dicc["offLine"] = DeivceCount["offLine"];
            dicc["days"] = DeivceCount["days"];
            dicc["overdue"] = DeivceCount["overdue"];
            dicc["expire"] = DeivceCount["expire"];
             
            return Utils.ToJson(dicc); //集合转JSon 
        }

 
    }
}

