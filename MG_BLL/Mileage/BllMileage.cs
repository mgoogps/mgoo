using MG_BLL.Entity;
using MG_DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace MG_BLL.Mileage
{
    public class BllMileage : baseclass
    {
        JavaScriptSerializer js = new JavaScriptSerializer(); 
        public string GetMileage(string DeviceID, string StartTime, string EndTime)
        {
            if (string.IsNullOrWhiteSpace(DeviceID) || DeviceID == "null") return "";
            long sticks = Convert.ToDateTime(StartTime).Ticks;
            long eticks = Convert.ToDateTime(EndTime).Ticks;
            BllMonitor bm = new BllMonitor();
            try
            {
                List<History> his = bm.GetHistory(DeviceID, StartTime, EndTime);
                DataTable dt = new DataTable();
                dt.Columns.Add("time"); dt.Columns.Add("lat"); dt.Columns.Add("lon"); 
                int stopCount = 0; DateTime tempTime = default(DateTime);
                for (int i = 0; i < his.Count; i++)
                {
                    History h = his[i];
                    if (h.Speed < Utils.SpeedFilter)
                        continue;
                    DataRow dr = dt.NewRow(); 
                    dr["time"] = h.DeviceTime;
                    dr["lat"] = h.Lat;
                    dr["lon"] = h.Lon; 
                    dt.Rows.Add(dr);
                }

                MgoogpsWebClient myWeb = new MgoogpsWebClient();
                myWeb.RequestMethodName = "/service/getalarmhistorybydevice/?param=id," + DeviceID + "&starttime," + sticks + "&endtime," + eticks;
                string json = myWeb.RequestSend();
                List<Dictionary<string, string>> alarmList = js.Deserialize<List<Dictionary<string, string>>>(json);
                Hashtable hb = new Hashtable();
                Dictionary<string, string> retu = new Dictionary<string, string>();
                string key = "";
                for (int i = 0; i < alarmList.Count; i++)
                {
                    Dictionary<string, string> dicAlarm = alarmList[i];
                    dicAlarm["alarmtime"] = Convert.ToDateTime(dicAlarm["alarmtime"]).ToString("yyyy-MM-dd");
                    key = dicAlarm["alarmtime"];// + "," + dicAlarm["alarmtype"];
                    if (retu.ContainsKey(key))
                        retu[key] = (Convert.ToInt32(retu[key]) + 1) + "";
                    else
                        retu[key] = "1";
                }
                Dictionary<string, string> rJson = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> item in retu)
                { 
                    List<Dictionary<string, string>> points = new List<Dictionary<string, string>>();
                    Dictionary<string, string> alarmTypeCount = new Dictionary<string, string>(); 
                    key = item.Key.ToString(); 
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime t = Convert.ToDateTime(dt.Rows[i]["time"]);//.ToString();
                        if (key == t.ToString("yyyy-MM-dd"))
                        {
                            if (tempTime != default(DateTime))
                            {
                                double stopTime = (Convert.ToDateTime(dt.Rows[i]["time"]) - Convert.ToDateTime(tempTime)).TotalSeconds;
                                if (stopTime > Utils.StopSeconds)
                                {
                                    stopCount++;
                                    tempTime = default(DateTime);
                                }
                            }
                            tempTime = t;
                            Dictionary<string, string> point = new Dictionary<string, string>();
                            point.Add("lat", dt.Rows[i]["lat"].ToString()); point.Add("lon", dt.Rows[i]["lon"].ToString());
                            points.Add(point);
                        }
                    }
                    Dictionary<string, string> ss = new Dictionary<string, string>();
                    ss.Add("alarmcount", retu[key]); ss.Add("stopcount", stopCount + ""); ss.Add("points", Utils.ToJson(points));
                    rJson[key] = Utils.ToJson(ss); 
                    stopCount = 0; 
                }
                return Utils.ToJson(rJson);
            }
            catch  
            {
                return "";
            }
        }

        public string GetStopDetail(string DeviceID,string StartTime,string EndTime)
        {

            try
            {
                BllMonitor bm = new BllMonitor();
                List<History> his = bm.GetHistory(DeviceID, StartTime, EndTime);
                List<Dictionary<string, string>> retuList = new List<Dictionary<string, string>>();
                for (int i = 1; i < his.Count; i++)
                {
                    History sh = his[i - 1], eh = his[i];
                    DateTime start = Convert.ToDateTime(sh.DeviceTime);
                    DateTime end = Convert.ToDateTime(eh.DeviceTime);

                    TimeSpan ts = (end - start);
                    if (ts.TotalSeconds > Utils.StopSeconds)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("starttime", start.ToString("yyyy-MM-dd HH:mm:ss"));
                        dic.Add("endtime", end.ToString("yyyy-MM-dd HH:mm:ss"));
                        dic["lon"] = Convert.ToDouble(sh.Lon).ToString("0.00000");
                        dic["lat"] = Convert.ToDouble(sh.Lat).ToString("0.00000");
                        string time = "";
                        if (ts.Days > 0)
                            time += ts.Days + "天";
                        if (ts.Hours > 0)
                            time += ts.Hours + "时";
                        if (ts.Minutes > 0)
                            time += ts.Minutes + "分";
                        dic["time"] = time;
                        dic["address"] = "";
                        retuList.Add(dic);
                        i++;
                    }
                }
                return Utils.ToJson(retuList);

            }
            catch (Exception)
            {

               return "";
            }
          
        }
        /// <summary>
        /// 获取停留折线图数据
        /// </summary>
        /// <param name="DeviceID">设备id</param>
        /// <param name="Time">时间（一天）</param>
        /// <returns></returns>
        public string GetEcharts(string DeviceID, string Time)
        {
            BllMonitor bm = new BllMonitor();
            History sh = new History();
            sh.DeviceTime = Convert.ToDateTime(Convert.ToDateTime(Time).ToString("yyyy-MM-dd 00:00:00")).ToString();
            sh.Speed = 0;
            History eh = new History();
            eh.DeviceTime = Convert.ToDateTime(Convert.ToDateTime(Time).ToString("yyyy-MM-dd 23:59:59")).ToString(); ;
            eh.Speed = 0;

            List<History> his = bm.GetHistory(DeviceID, Convert.ToDateTime(Time).ToString("yyyy-MM-dd 00:00:00"), Convert.ToDateTime(Time).ToString("yyyy-MM-dd 23:59:59"));
            his.Insert(0, sh);
            his.Add(eh);
            List<History> dtErr = new List<History>();
            List<History> conErr = new List<History>();
            for (int i = 0; i < his.Count; i++)
            {
                if (Convert.ToDateTime(his[i].DeviceTime) < Convert.ToDateTime(sh.DeviceTime))
                {
                    dtErr.Add(his[i]);
                }
                if (Convert.ToDateTime(his[i].ConnectTime) < Convert.ToDateTime(sh.ConnectTime))
                {
                    conErr.Add(his[i]);
                }
            }

            List<Dictionary<string, string>> retuList = new List<Dictionary<string, string>>();
            for (int i = 1; i < his.Count; i++)
            {
                if (Convert.ToDateTime(his[i].DeviceTime) < Convert.ToDateTime(sh.DeviceTime) || Convert.ToDateTime(his[i - 1].DeviceTime) < Convert.ToDateTime(sh.DeviceTime))
                {
                    continue;
                }
                History s = his[i - 1];
                History e = his[i];
                TimeSpan ts = (Convert.ToDateTime(e.DeviceTime) - Convert.ToDateTime(s.DeviceTime));

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["Speed"] = s.Speed + "";
                dic["DeviceTime"] = Convert.ToDateTime(s.DeviceTime).ToString();
                retuList.Add(dic);
                if (ts.TotalSeconds > 25)
                {
                    //填充数据
                    int seconds = (int)(ts.TotalSeconds / 10);
                    for (int j = 0; j < seconds; j++)
                    {
                        dic = new Dictionary<string, string>();
                        dic["Speed"] = "0";
                        dic["DeviceTime"] = Convert.ToDateTime(s.DeviceTime).AddSeconds((j + 1) * 10).ToString("yyyy-MM-dd HH:mm:ss");
                        retuList.Add(dic);
                        //i--;
                    }
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;
            return js.Serialize(retuList);
        }
          

        public string GetDevicesCount()
        {
            BllMonitor bo = new BllMonitor();
          //  bo.GetDeviceData

            return "";
        }

        /// <summary>
        /// 获取报警预览数据
        /// </summary>
        /// <param name="DeviceID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public string GetAlarmPreview(string DeviceID, string StartTime, string EndTime)
        { 
            string json =  GetAlarmByDeviceID(DeviceID, StartTime, EndTime);
            List<Dictionary<string, string>> list = js.Deserialize<List<Dictionary<string, string>>>(json);
         
           // Dictionary<string, string> retuDic = new Dictionary<string, string>();
         
            Dictionary<string, string> tempDic = new Dictionary<string, string>();

            for (int i = 0; i < list.Count; i++)
            {
                Dictionary<string, string> d = list[i];
                d["alarmtime"] = Convert.ToDateTime(list[i]["alarmtime"]).ToString("yyyy-MM-dd");
                string key = d["alarmtime"] + "," + d["alarmtype"];
                if (tempDic.ContainsKey(key))
                {
                    tempDic[key] = (Convert.ToInt32(tempDic[key]) + 1) + "";
                }
                else
                    tempDic[key] = "1"; 
            }
            list = new List<Dictionary<string, string>>();
            Dictionary<string, string> Dic = new Dictionary<string, string>();
            List<string> temp = new List<string>();
            string tempDate = "";
            foreach (KeyValuePair<string,string> item in tempDic)
            {
                string[] keys = item.Key.Split(',');
               
                if (tempDate !="" && tempDate != keys[0])
                {
                    list.Add(Dic);
                    Dic = new Dictionary<string, string>();
                }
                Dic["alarmtype" + keys[1]] = tempDic[item.Key];
                Dic["date"] = keys[0];
                tempDate = keys[0];
                
                // if (retuDic.ContainsKey(keys[0]))
                // {
                //     retuDic[keys[0]] = retuDic[keys[0]] + ",{alarmtype:\"" + keys[1] + "\",alarmcount:\"" + tempDic[item.Key] + "\"}";
                // }
                //  else
                // {
             
                   // retuDic[keys[0]] = "{alarmtype:\"" + keys[1] + "\",alarmcount:\"" + tempDic[item.Key] + "\"}";
                //} 
            }
            if (Dic.Count > 0) list.Add(Dic);
          
            return Utils.ToJson(list);
        }


        public string GetAlarmByDeviceID(string DeviceID, string StartTime, string EndTime)
        {
            MgoogpsWebClient mwc = new MgoogpsWebClient();
            mwc.RequestMethodName = "/service/getalarmhistorybydevice/?param=id," + DeviceID + "&starttime," + Convert.ToDateTime(StartTime).Ticks + "&endtime," + Convert.ToDateTime(EndTime).Ticks + "";
            return mwc.RequestSend(); 
        }

        public string GetAlarmDetail(string DeviceID, string StartTime, string EndTime)
        {
            string json = GetAlarmByDeviceID(DeviceID, StartTime, EndTime);
          //  List<Dictionary<string, string>> list = js.Deserialize<List<Dictionary<string, string>>>(json);

            return json;
        }

        public string GetGenFencesCount(string DeviceID, string StartTime, string EndTime)
        {
            //   "1": "震动报警";"2":"断电报警"; "3":"接通报警"; "4":"SOS"; "5": "出电子围栏报警";"6":"入电子围栏报警";"7": "超速报警";"其他报警";
            string json = GetAlarmByDeviceID(DeviceID, StartTime, EndTime);
            List<Dictionary<string, string>> list = js.Deserialize<List<Dictionary<string, string>>>(json);
            List<Dictionary<string, string>> retuList = new List<Dictionary<string, string>>();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < list.Count; i++)
            {
                //if (dic.Count > 0)
                //{
                //    dic.Add("imei", list[i]["IMEI"]);
                //    dic.Add("lat", list[i]["lat"]);
                //    dic.Add("lon", list[i]["lon"]);
                //    retuList.Add(list[i]);
                //}
                if (list[i]["alarmtype"] == "5") //出点子围栏的报警
                {
                    dic = new Dictionary<string, string>();
                   //dic.Add("alarmtype1", list[i]["alarmtype"]);
                    dic.Add("entertime", list[i]["alarmtime"]);
                    dic.Add("enterlat", list[i]["lat"]);
                    dic.Add("enterlon", list[i]["lon"]);
                }
                if ( list[i]["alarmtype"] == "6") //进点子围栏的报警
                {
                   //dic.Add("alarmtype2", list[i]["alarmtype"]);
                    dic["outtime"] = list[i]["alarmtime"];
                   // dic.Add("imei", list[i]["IMEI"]);
                    dic["outlat" ] = list[i]["lat"];
                    dic["outlon"]  = list[i]["lon"];
                    if (dic.Count == 6) 
                        retuList.Add(dic); 
                } 
            } 
            return Utils.ToJson(retuList);
        }
        public string GetOutExecel(string DeviceID, string StartTime, string EndTime, string OilWear) {

        string str=GetMileage(DeviceID,StartTime,EndTime);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("DeviceID",typeof(string));
            dt.Columns.Add("StartTime", typeof(string));
            dt.Columns.Add("EndTime", typeof(string));
            dt.Columns.Add("OilWear", typeof(string));
           dictionary = Utils.ToDictionary(str); ;
            foreach (KeyValuePair < string,string> item in dictionary)
            {
                DataRow row = dt.NewRow();
                row["DeviceID"] = DeviceID;
                row["StartTime"] = StartTime;
                row["EndTime"] = EndTime;
                row["OilWear"] = OilWear;

                dt.Rows.Add(row);
            }
            String path = System.Web.HttpContext.Current.Server.MapPath("../") + "\\Excel";
            string fileName = Guid.NewGuid() +".xls";
            string title= @" <h4>时间：" + StartTime + "-" + EndTime + @"</h4>
                        <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称 </td><td> 时间 </td><td> 里程(公里)  </td><td>  报警 </td><td> 停留 </td><td> 油耗(升) </td></tr>";
            Utils.DataSetToExcel(dt, path+"\\"+fileName, title);
            return "";

        }
    }
}
