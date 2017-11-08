using MG_BLL.Common;
using MG_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MG_BLL.Weixin
{
    public class Devices
    {
        private Common.AuthHeader myHeader = new Common.AuthHeader();
        private string cacheKey { set; get; }
        public Devices(Common.AuthHeader header)
        {
            this.myHeader = header;

            cacheKey = myHeader.Identifies + myHeader.UserID + myHeader.Token;
        }
        public List<Dictionary<string, string>> GetGroupList(string userid)
        {
            try
            {
                if (userid != myHeader.UserID)
                {
                    return new List<Dictionary<string, string>>();
                }
                SQLServerOperating s = new SQLServerOperating();
                string strSql = "select GroupID, GroupName from Groups where UserID=@UserID and Deleted=0";
                List<Dictionary<string, string>> list = s.Selects(strSql, new SqlParameter[] { new SqlParameter("UserID", userid) }).toListDictionary();
                Dictionary<string, string> defaultGroup = new Dictionary<string, string>();
                defaultGroup.Add("GroupID", "-1");
                defaultGroup.Add("GroupName", "默认组");
                list.Insert(0, defaultGroup);
                return list;
            }
            catch (Exception)
            { 
                return new List<Dictionary<string, string>>();
            }
        
        }
        /// <summary>
        /// 根据用户id获取设备列表
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> GetDevicesList(string userid)
        {
            string deviceID = "";
            try
            { 
                string strSql = @"select d.DeviceID,d.SerialNumber, case when DeviceName='' then d.SerialNumber else DeviceName end DeviceName,l.LastCommunication ,l.DataContext,l.Speed,d.GroupID,d.HireExpireDate,datediff(MI,StopStartUtcDate,serverutcdate) StopTime,ISNULL(d.ServerID2,1) Shock,
                            (select COUNT(-1) from ExceptionMessage e where e.Deleted=0 and e.DeviceID=d.DeviceID)ExceptionCount,l.OLat,l.OLng,di.DataText as Model
                            from devices d 
                            left join lklocation l on l.DeviceID = d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                            where d.UserID = @UserID and d.deleted = 0
                            group by d.DeviceID,d.SerialNumber,d.DeviceName,l.DataContext,l.Speed ,d.GroupID,l.LastCommunication,d.HireExpireDate,StopStartUtcDate,serverutcdate,l.OLat,l.OLng,di.DataText,d.ServerID2
                            order by  d.DeviceName collate Chinese_PRC_CS_AS_KS_WS asc"; //count(e.ExceptionID) ExceptionCount left join ExceptionMessage e on e.DeviceID = d.deviceid

                SQLServerOperating s = new SQLServerOperating();

                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("UserID", userid) });
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach (DataColumn c in dt.Columns)
                    {
                        dic[c.ColumnName] = row[c.ColumnName].toStringEmpty();
                    }

                    string IsStop = "1"; //运动
                    if (dic["Speed"].toDouble() < Utils.SpeedFilter) //速度 小于7.5 的过滤掉
                    {
                        if (string.IsNullOrEmpty(dic["Speed"]))
                            dic["Speed"] = "0.00";
                        IsStop = "0"; // 停止
                    }
                    string[] ContextList = dic["DataContext"].Split('-');

                    if (ContextList.Length == 1) {
                        dic["DataContext"] = "0-0-0-0-" + dic["DataContext"];
                        ContextList = dic["DataContext"].Split('-');
                    }
                    string imei = dic["SerialNumber"];
                    //这两款设备没有撤防设防指令，只有保存状态，替换DataContext中的撤防设防
                    if (imei.StartsWith("3008000000") || imei.StartsWith("3528888000"))
                    {
                        if (ContextList.Length > 1)
                            ContextList[1] = dic["Shock"];//0 撤防，1设防
                        dic["DataContext"] = string.Join("-", ContextList);
                    }
                     
                    string status = GetDevicesStatus(dic["LastCommunication"], dic["HireExpireDate"]); // 1在线 2离线 3未激活 4已到期
                    dic["Status"] = status;
                    dic["IsStop"] = IsStop;
                    dic["StatusTimeInfo"] = "";
                    string statusmin = "0";
                    if (status == "2") {
                        statusmin = dic["LastCommunication"] == "" ? "" : (DateTime.Now - Convert.ToDateTime(dic["LastCommunication"])).TotalMinutes.ToString("0");
                        dic["StatusTimeInfo"] = Utils.MinuteToHour((DateTime.Now - Convert.ToDateTime(dic["LastCommunication"])).TotalMinutes, true);
                    }
                    else if (status == "4") {
                        statusmin = (DateTime.Now - Convert.ToDateTime(dic["HireExpireDate"])).TotalMinutes.ToString("0");
                        dic["StatusTimeInfo"] = Utils.MinuteToHour((DateTime.Now - Convert.ToDateTime(dic["HireExpireDate"])).TotalMinutes, true);
                    } else if (status == "1" && IsStop == "0") {
                        dic["StatusTimeInfo"] = Utils.MinuteToHour( Convert.ToDouble(dic["StopTime"]) , true);
                        statusmin = dic["StopTime"];
                    }
                    deviceID = dic["DeviceID"];
                    Geocoding geo = GetCurrentMapType();
                    Gps gps = geo.Translate(dic["OLat"],dic["OLng"],false);
                    dic["OLat"] = gps.getWgLat().toStringEmpty();
                    dic["OLng"] = gps.getWgLon().toStringEmpty();
                    dic["StatusMinute"] = statusmin;
                    dic.Remove("StopTime");
                    list.Add(dic);
                }
                return list;
            }
            catch (Exception ex)
            {
                Utils.log("GetDevicesList ERROR:"+ ex.Message+",堆栈："+ex.StackTrace+","+ex.Source);
                return new List<Dictionary<string, string>>();
            }
        }

        public Dictionary<string, string> GetMonitorByDeviceID(string deviceid)
        {
            try
            {
                int devid = 0;
                int.TryParse(deviceid, out devid);
                if (devid <= 0)
                {
                    return new Dictionary<string, string>();
                }
                string strSql = ""; 
                
                    strSql = @"select d.DeviceID,d.DeviceName,d.SerialNumber,l.LastCommunication,DATEADD(HH,8, DeviceUtcDate)DeviceDate,d.HireExpireDate ,l.DataContext,l.Speed,l.OLng,l.OLat,l.Course,l.CarStatus,
                               (select count(em.ExceptionID) from ExceptionMessage em where em.DeviceID=d.deviceid and em.deleted = 0 ) ExceptionCount,di.DataText as Model,datediff(MI,StopStartUtcDate,serverutcdate) StopTime
                             from devices d left join lklocation l on l.DeviceID = d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                             where d.deleted = 0 and d.DeviceID = @DeviceID and d.UserID=@UserID
                             group by d.DeviceID,d.DeviceName,d.SerialNumber,l.LastCommunication ,l.DataContext,l.Speed,l.OLng,l.OLat,l.Course,DeviceUtcDate,d.HireExpireDate,di.DataText,StopStartUtcDate,serverutcdate,l.CarStatus";
           
                 
                SQLServerOperating s = new SQLServerOperating();
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("DeviceID", deviceid), new SqlParameter("UserID", myHeader.UserID) });

                Dictionary<string, string> dic = new Dictionary<string, string>();
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    foreach (DataColumn c in dt.Columns)
                    {
                        dic[c.ColumnName] = row[c.ColumnName].toStringEmpty();
                    }
                    if (string.IsNullOrEmpty(dic["DeviceName"]))
                    {
                        dic["DeviceName"] = dic["SerialNumber"];
                    }
                    string dc = dic["DataContext"];
                    if (dc.Split('-').Length == 1)
                    {
                        dic["DataContext"] = "0-0-0-0-" + dc;
                    }
                    dic["CourseName"] = Utils.GetCoureName(dic["Course"]); 
                    Geocoding geo = GetCurrentMapType();
                    Gps gps = geo.Translate(dic["OLat"], dic["OLng"], true); 
                    dic["OLng"] = gps.getWgLon().ToString();
                    dic["OLat"] = gps.getWgLat().ToString();
                    dic["Address"] = gps.Address;
                    string IsStop = "1"; //运动
                    if (dic["Speed"].toDouble() < Utils.SpeedFilter) //速度 小于7.5 的过滤掉
                    {
                        if (string.IsNullOrEmpty(dic["Speed"]))
                            dic["Speed"] = "0.00";
                        IsStop = "0"; // 停止
                    }
                    string status= GetDevicesStatus(dic["LastCommunication"], dic["HireExpireDate"]);
                    dic["Status"] = status;
                    string statusmin = "0";
                    if (status == "2")
                    {
                        statusmin = dic["LastCommunication"] == "" ? "" : (DateTime.Now - Convert.ToDateTime(dic["LastCommunication"])).TotalMinutes.ToString("0"); 
                    }
                    else if (status == "4")
                    {
                        statusmin = (DateTime.Now - Convert.ToDateTime(dic["HireExpireDate"])).TotalMinutes.ToString("0"); 
                    }
                    else if (status == "1" && IsStop == "0")
                    { 
                        statusmin = dic["StopTime"];
                    } 
                    dic.Remove("StopTime");
                    dic["StatusMinute"] = statusmin;

                    //Thread thread = new Thread(new ThreadStart(delegate {
                    //    Common.lib.AoboCache.CreateInstance().Action(dic["SerialNumber"], dic["Model"]);
                    //}));
                    //thread.Start();
                }
                return dic;
            }
            catch (Exception ex)
            {
                Utils.log("Devices>GetMonitorByDeviceID Error:" + ex.Message);
                return new Dictionary<string, string>();
            }
        }
        public Geocoding GetCurrentMapType()
        {
            Geocoding geo = null;
            try
            {
                if (Utils.GetCache<LoginUserInfo>(cacheKey).MapType == MG_DAL.MapType.BAIDU)
                {
                    geo = new Baidu();
                    geo.key = Utils.GetBaiDuKey();
                }
                else
                {
                    geo = new Amap();
                    geo.key = Utils.GetAmapKey();
                }
            }
            catch (Exception ex)
            {
                Utils.log("Devices GetCurrentMapType Error:"+ex.Message);  
            } 
            return geo;
        }
        public string GetDevicesStatus(string LastCommunication,string HireExpireDate)
        {
            string status = string.Empty;
            if (string.IsNullOrEmpty(LastCommunication))
            {
                status = "3";  // 未激活
                return status;
            }
            if ((DateTime.Now - Convert.ToDateTime(LastCommunication)).TotalMinutes < Utils.OffLineMinute)
            {
                status = "1";  //在线
            }
            else
            {
                status = "2"; //离线
            }
         
            if (HireExpireDate.toDateTime() <= DateTime.Now && HireExpireDate.toDateTime() > Convert.ToDateTime("1900-1-1 0:00:00"))
            {
                status = "4"; //已过期
            } 
            return status;
        }

        public Dictionary<string, string> GetDeviceInfoByID(string deviceid)
        {
            try
            {
                int did = 0;
                if (string.IsNullOrEmpty(deviceid) && !int.TryParse(deviceid,out did))
                {
                    return new Dictionary<string, string>();
                }
                string strSql = string.Format(@"select d.DeviceID, SerialNumber, DeviceName,CarUserName,PhoneNum, CarNum, CellPhone,ActiveDate,HireExpireDate,di.DataText,d.GroupID,d.Description,ISNULL(dc.ShockSens,2) Sens
                                            from Devices d inner join Dictionary di on d.Model=di.DataValue inner join DevicesConfig dc on dc.deviceid=d.deviceid where d.Deleted=0 and d.DeviceID = @DeviceID and d.UserID=@UserID");
                SQLServerOperating s = new SQLServerOperating();
                Dictionary<string, string> dic = s.Selects(strSql, new SqlParameter[] { new SqlParameter("DeviceID", deviceid),new SqlParameter("UserID",myHeader.UserID) }).toDictionary();
                DateTime HireExpireDate = Convert.ToDateTime(dic["HireExpireDate"]);
                DateTime ActiveDate = Convert.ToDateTime(dic["ActiveDate"]);
                dic["HireExpireDate"] = HireExpireDate.ToString("yyyy-MM-dd 23:59:59");
                dic["ActiveDate"] = ActiveDate.ToString("yyyy-MM-dd HH:mm:ss");
                return dic;
            }
            catch (Exception)
            {
                return new Dictionary<string, string>();
            }
        
        }
        public string UpdateDeviceInfoByID(string deviceid, string deviceName, string carNum, string carusername, string cellphone,string groupid,string description,string sens=null)
        {
            try
            {
                if (string.IsNullOrEmpty(deviceid) || string.IsNullOrEmpty(groupid))
                {
                    return Utils.GetResult("参数错误.", statusCode.Code.failure);
                } 
                string strSql = string.Format(@"update devices set devicename=@devicename,carNum=@carNum,carusername=@carusername,cellphone=@cellphone,GroupID=@GroupID,Description=@Description where Deleted=0 and DeviceID = @DeviceID and UserID=@UserID");
                SQLServerOperating s = new SQLServerOperating();
                List<string> sqlList = new List<string>();
                sqlList.Add(strSql);
                if (string.IsNullOrEmpty(sens)) 
                    sens = "2"; 
              
                List<SqlParameter[]> parsList = new List<SqlParameter[]>();
                var par = new SqlParameter[] {
                    new SqlParameter("devicename", deviceName),
                    new SqlParameter ("carNum",carNum),
                    new SqlParameter ("carusername",carusername),
                    new SqlParameter ("cellphone",cellphone),
                    new SqlParameter ("DeviceID",deviceid),
                    new SqlParameter ("UserID",myHeader.UserID),
                    new SqlParameter ("GroupID",groupid),
                    new SqlParameter("Description",description)
                };
                parsList.Add(par);
                if (!string.IsNullOrEmpty(sens))
                { 
                    strSql = "update DevicesConfig set ShockSens=@ShockSens,UpdateTime=GETDATE() where DeviceID=@DeviceID";
                    sqlList.Add(strSql);
                    par = new SqlParameter[] { new SqlParameter("ShockSens",sens),new SqlParameter("DeviceID",deviceid) };
                    parsList.Add(par);
                }
                int status = s.ExecuteSql(sqlList,parsList); 
                if (status > 0)
                {
                    return Utils.GetResult("修改成功", statusCode.Code.success);
                }
                else
                {
                    return Utils.GetResult("修改失败", statusCode.Code.failure);
                }
            }
            catch (Exception ex)
            {
                Utils.log("UpdateDeviceInfoByID ERROR "+ex.Message+",堆栈信息："+ex.StackTrace);
                return Utils.GetResult(ex.Message, statusCode.Code.error);
            }
        }

        /// <summary>
        /// 获取历史轨迹
        /// </summary>
        /// <param name="deviceid"></param>
        /// <param name="date"></param>
        /// <param name="maptype"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> GetHistoryLocus(string deviceid,string date,string maptype= "AMAP")
        {
            try
            {
                if (string.IsNullOrEmpty(deviceid) || string.IsNullOrEmpty(date) ||  deviceid == "undefined")
                {
                    return new List<Dictionary<string, string>>();
                }
                DateTime historyDate = Convert.ToDateTime(date);
                if ((DateTime.Now - historyDate).TotalDays > 90)
                {
                    return new List<Dictionary<string, string>>();
                }
                string DataBaseBefore = ConfigurationManager.AppSettings["DataBaseName"].ToStringEmpty();
                string DateBase = DataBaseBefore + historyDate.ToString("yyyyMM");
                int TableName = Convert.ToInt32(historyDate.ToString("dd"));
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                SQLServerOperating s = new SQLServerOperating();
                string speedfilter = "0";
                string model = s.Select("select di.DataText from Devices d inner join Dictionary di on di.DataValue=d.Model where DeviceID=@DeviceID",
                    new SqlParameter[] { new SqlParameter("DeviceID", deviceid) });
                if (model.EndsWith("W") || model.EndsWith("WD") || model.EndsWith("WF"))
                {
                    speedfilter = "-1";
                }
                string strSql = "select dateadd(HH,8,DeviceUTCTime)DeviceTime, OLat, OLng, Speed, Course from [" + DateBase + "].[dbo].[Location" + TableName + @"]
                              where speed>"+ speedfilter+" and deviceid=" + deviceid  + " order by DeviceTime asc";
                DataTable dt = s.Selects(strSql);
                Geocoding geo = null;// = GetCurrentMapType();
                if (maptype == "BAIDU")
                {
                    geo = new Baidu();
                }
                else if (maptype == "AMAP")
                {
                    geo = new Amap();
                }
                else
                {
                    geo = GetCurrentMapType(); 
                }
                Utils.log("GetHistoryLocus> deviceid:" + deviceid+","+geo.GetType().toStringEmpty());
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dic[dc.ColumnName] = row[dc.ColumnName].toStringEmpty();
                    }
                    Gps gps = geo.Translate(dic["OLat"], dic["OLng"],false);
                    dic["OLat"] = gps.getWgLat().toStringEmpty();
                    dic["OLng"] = gps.getWgLon().toStringEmpty(); 
                    list.Add(dic);
                }
                return list;
            }
            catch (Exception ex)
            {
                Utils.log("GetHistoryLocus Error1:" + ex.Message + ",堆栈信息:" + ex.StackTrace + "," + deviceid + "-" + date + "-" + maptype+"-"+myHeader.Identifies);
                return new List<Dictionary<string, string>>();
            }
        
        }

        public List<Dictionary<string, string>> GetHistoryLocus(string deviceid, string startdate,string enddate,string speedfilter)
        {
            if (string.IsNullOrEmpty(deviceid) || string.IsNullOrEmpty(startdate) || string.IsNullOrEmpty(enddate))
            {
                return new List<Dictionary<string, string>>();
            }
            if (string.IsNullOrEmpty(speedfilter))
            {
                speedfilter = Utils.SpeedFilter.toStringEmpty();
            }
            try
            {
                DateTime startTime = Convert.ToDateTime(startdate);
                DateTime endTime = Convert.ToDateTime(enddate);
                if (startTime >= endTime)
                {
                    return new List<Dictionary<string, string>>();
                } 
                if ((DateTime.Now - startTime).TotalDays > 90)
                {
                    return new List<Dictionary<string, string>>();
                }
                TimeSpan ts = endTime - startTime;
                double days = Math.Ceiling(ts.TotalDays);
                //一次最多只能看5天的数据
                if (days > 5)
                {
                    return new List<Dictionary<string, string>>();
                }
                string DataBaseBefore = ConfigurationManager.AppSettings["DataBaseName"].ToStringEmpty();
                StringBuilder strSql = new StringBuilder();
              
                string startTimeUtc = startTime.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss");
                string endTimeUtc = endTime.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss");

                strSql.Append(" select DeviceTime, OLat, OLng, Speed, Course from ( ");
                string where = " where speed > @speedfilter and deviceid=@deviceid and DeviceUTCTime>@startTimeUtc and DeviceUTCTime<@endTimeUtc";
                for (int i = 0; i < days; i++)
                {
                    string DateBase = DataBaseBefore + startTime.ToString("yyyyMM");
                    int TableName = Convert.ToInt32(startTime.ToString("dd")); 
                    strSql.Append("select dateadd(HH,8,DeviceUTCTime)DeviceTime, OLat, OLng, Speed, Course from  [" + DateBase + "].[dbo].[Location" + TableName + @"]");
                    strSql.Append(where);
                    if (i != days-1)
                    {
                        strSql.Append(" union all ");
                    }
                    startTime = startTime.AddDays(1);
                }
                strSql.Append(" )t order by DeviceTime");
                SQLServerOperating s = new SQLServerOperating();
                string model = s.Select("select di.DataText from Devices d inner join Dictionary di on di.DataValue=d.Model where DeviceID=@DeviceID",new SqlParameter[] { new SqlParameter("DeviceID", deviceid)});
                if (model.EndsWith("W") || model.EndsWith("WD") || model.EndsWith("WF"))
                {
                    speedfilter = "-1";
                }
                SqlParameter[] pars = new SqlParameter[] {
                    new SqlParameter("speedfilter", speedfilter),
                    new SqlParameter("deviceid", deviceid),
                    new SqlParameter("startTimeUtc", startTimeUtc),
                    new SqlParameter("endTimeUtc", endTimeUtc)
                };
                
                DataTable dt = s.Selects(strSql.ToString(), pars);
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                Geocoding geo = GetCurrentMapType();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dic[dc.ColumnName] = row[dc.ColumnName].toStringEmpty();
                    }
                    Gps gps = geo.Translate(dic["OLat"], dic["OLng"], false);
                    dic["OLat"] = gps.getWgLat().toStringEmpty();
                    dic["OLng"] = gps.getWgLon().toStringEmpty();
                    list.Add(dic);
                }
                return list;  
            }
            catch (Exception ex)
            {
                Utils.log("GetHistoryLocus Error2:" + ex.Message + ",堆栈信息:" + ex.StackTrace + "," + deviceid + "-" + startdate + "-" + enddate);
                return new List<Dictionary<string, string>>();
            } 
        }

        public string AddDevice(string imei, string vccode, string userid, string groupid,Dictionary<string,string> parmenters = null)
        {
            try
            {
                if (myHeader.UserID != userid)
                {
                    return Utils.GetResult("用户ID错误！", statusCode.Code.failure);
                }
                if (string.IsNullOrEmpty(imei) || string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(groupid))
                {
                    return Utils.GetResult("参数不能为空.", statusCode.Code.failure);
                }
               
                Dictionary<string, string> dic = VCCodeExist(imei);
                //Utils.log("AddDevice dic长度:" + dic.Count+ ",imei：" + imei);
                if (dic.Count > 0 && !string.IsNullOrEmpty(dic["DeviceID"]))
                {
                    string deviceid = dic["DeviceID"];
                    SQLServerOperating s = new SQLServerOperating();
                    string car_where = "";
                    Dictionary<string, string> parsDic = new Dictionary<string, string>();
                    if (parmenters != null)
                    {
                        //pars = new SqlParameter[pars.Length + parmenters.Count];
                        foreach (KeyValuePair<string, string> item in parmenters)
                        {
                            parsDic[item.Key] = item.Value;
                            car_where += "," + item.Key + "=@" + item.Key;
                        }
                        ///修改设备名称: X83开头的设备为无线，其他为有线, 名称格式：设备联系人-无线(有线)
                        car_where += ",DeviceName=" + string.Format("CASE WHEN (select di.DataText from Devices d inner join Dictionary di on di.DataValue = d.Model where SerialNumber=@SerialNumber and deleted = 0) like'MG-X8%' THEN '{0}'+'-无线'ELSE '{0}'+'-有线'END", parmenters["CarUserName"]);
                        // parsDic["DeviceName"] = ;
                    }

                    string vccode1 = dic["DevicePassword"];
                    DateTime created = dic["Created"].toDateTime();
                    string vc_where = "";
                    if (!string.IsNullOrEmpty(vccode1) && created > "2016-10-22 00:00:28".toDateTime() && dic["DevicePassword"] != "123456") //从 2016-10-22 起开始有VC码
                    {
                        if (string.IsNullOrEmpty(vccode))
                        {
                            return Utils.GetResult("请输入VC码.", statusCode.Code.failure);
                        }
                        vc_where = " and DevicePassword=@vccode ";
                        parsDic["vccode"] = vccode;
                    }

                    //当前添加设备的所属用户ID
                    string parentUserID = dic["UserID"];
                    var isCurrentUserID = parentUserID == userid;
                    if (isCurrentUserID && parmenters == null)
                    {
                        return Utils.GetResult("该设备已在当前用户下.", statusCode.Code.failure);
                    }
                    string strSql = "update Devices set UserID=@UserID ,GroupID=@GroupID " + car_where + " where SerialNumber=@SerialNumber and deleted = 0 " + vc_where;
                    parsDic["UserID"] = userid;
                    parsDic["GroupID"] = groupid;
                    parsDic["SerialNumber"] = imei;
                    SqlParameter[] pars = new SqlParameter[parsDic.Count];
                    int index = 0;
                    foreach (KeyValuePair<string, string> kvp in parsDic)
                    {
                        pars[index] = new SqlParameter(kvp.Key, kvp.Value); index++;
                    }
                    // pars[0] = new SqlParameter("UserID", userid);
                    // pars[1] = new SqlParameter("GroupID", groupid);
                    //pars[2] = new SqlParameter("serialnumber", imei);

                    List<string> sqlList = new List<string>();
                    List<SqlParameter[]> parsList = new List<SqlParameter[]>();
                    sqlList.Add(strSql);
                    parsList.Add(pars);
                    if (!isCurrentUserID)
                    {

                        #region 判断当前添加的设备是否是属于 父用户或者子用户  
                        bool isUpdateParent = true;
                        List<string> useridList = new List<string>();
                        useridList.Add("2651"); //13826966214
                        useridList.Add("3977"); //burt
                        useridList.Add("7");  //test
                        useridList.Add("1336"); //测试用户
                        useridList.Add("871"); //xietong
                        if (useridList.Contains(userid) || useridList.Contains(parentUserID))
                        {
                            isUpdateParent = false;
                        }

                        List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                        if (isUpdateParent)
                        {
                            strSql = @"with subqry(UserID,UserName,ParentID) as (
                                  select UserID,UserName,ParentID from Users where UserID = @UserID
                                  union all
                                  select Users.UserID,Users.UserName,Users.ParentID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted!=1 
                              )
                              select UserID,UserName,ParentID from subqry";
                            //查询该设备之前的UserID是否属于当前添加用户的 子用户，如果是子用户(包括子用户的子用户···) 则不修改他的父级ID
                            list = s.Selects(strSql, new SqlParameter[] { new SqlParameter("UserID", userid) }).toListDictionary();

                            foreach (Dictionary<string, string> item in list)
                            {
                                if (item.ContainsValue(parentUserID))
                                {
                                    isUpdateParent = false;
                                    break;
                                }
                            }
                        }
                        if (isUpdateParent)
                        {
                            strSql = @"with subqry(UserID,UserName,ParentID) as (
                                  select UserID, UserName, ParentID from Users   where UserID = @UserID
                                  union all
                                  select Users.UserID,Users.UserName,Users.ParentID from Users,subqry
                                  where Users.UserID = subqry.ParentID and users.Deleted != 1
                              )
                             select UserID, UserName, ParentID from subqry ";
                            //查询该设备之前的UserID是否属于当前添加用户的 父级用户(包括父级的父级···),如果是父级用户则不修改他的父级ID
                            list = s.Selects(strSql, new SqlParameter[] { new SqlParameter("UserID", userid) }).toListDictionary();
                            foreach (Dictionary<string, string> item in list)
                            {
                                if (item.ContainsValue(parentUserID))
                                {
                                    isUpdateParent = false;
                                    break;
                                }
                            }
                        }
                        if (isUpdateParent)
                        {
                            strSql = "update Users set ParentID=@ParentID where UserID=@UserID";
                            pars = new SqlParameter[] { new SqlParameter("ParentID", parentUserID), new SqlParameter("UserID", userid) };
                            parsList.Add(pars);
                            sqlList.Add(strSql);
                        }
                        #endregion
                    }
                    int status = s.ExecuteSql(sqlList, parsList);
                    if (status > 0)
                    {
                       // string oleParentID = s.Select("select ParentID from Users where UserID = @UserID", new SqlParameter[] { new SqlParameter("UserID", userid) });
                        Utils.log("DeviceID:"+deviceid+",设备IMEI:"+imei+",设备原UserID:"+parentUserID+",设备现UserID:"+userid , "AddDevice.log"); //+ ",原ParentID:" + oleParentID + ",现ParentID:" + parentUserID
                        Task.Run(() => {
                            Utils.SendTcpCmd("VTR-UpdateSpeedLimitDevice-" + deviceid);
                        }); 
                        return Utils.GetResult("添加成功.", statusCode.Code.success);
                    }
                    else
                    {
                        Utils.log("长度：" + pars.Length + ",strSql:" + strSql + ",结果:" + status + ",值:" + imei + "," + vccode + "," + userid + "," + groupid);
                        return Utils.GetResult("添加失败.", statusCode.Code.failure);
                    }
                }
                else
                {
                    return Utils.GetResult("未找到设备.", statusCode.Code.failure) ;
                }
                #region //新增一条数据
                //if (imei.Length > 30 || vccode.Length > 30)
                //{
                //    return Utils.GetResult("参数错误！", statusCode.Code.failure);
                //}
                //strSql = @"insert into Devices(SerialNumber,DeviceName,DevicePassword,CarUserName, CarNum, CellPhone,Status,PhoneNum,Model, Description,Created,Deleted ,ActiveDate,HireStartDate,HireExpireDate,SpeedLimit,UserID,GroupID,Icon,AddHireDay,OILCoefficient,BSJIP,ServerID,CarImg)
                //            values(@SerialNumber,@DeviceName,@DevicePassword,@CarUserName, @CarNum, @CellPhone,@Status,@PhoneNum,@Model,@Description,@Created,@Deleted ,@ActiveDate,@HireStartDate,@HireExpireDate,@SpeedLimit,@UserID,@GroupID,@Icon,@AddHireDay,@OILCoefficient,@BSJIP,@ServerID,@CarImg);select @@identity";
                //SqlParameter[] sqlParameter = new SqlParameter[] {
                //     new SqlParameter("@SerialNumber", imei),
                //     new SqlParameter("@DeviceName",""),
                //     new SqlParameter("@DevicePassword", 123456),
                //     new SqlParameter("@CarUserName",""),
                //     new SqlParameter("@CarNum",""),
                //     new SqlParameter("@CellPhone",""),
                //     new SqlParameter("@Status", 1),
                //     new SqlParameter("@PhoneNum",""),
                //     new SqlParameter("@Model","-1"),
                //     new SqlParameter("@Description",""),
                //     new SqlParameter("@Created",DateTime.Now.AddHours(-8)),
                //     new SqlParameter("@Deleted", "0"),
                //     new SqlParameter("@ActiveDate", "1900-01-01 00:00:00.000"),
                //     new SqlParameter("@HireStartDate",DateTime.Now.AddHours(-8)),
                //     new SqlParameter("@HireExpireDate", "1900-01-01 00:00:00.000"),
                //     new SqlParameter("@SpeedLimit",  "0.00"),
                //     new SqlParameter("@UserID", userid),
                //     new SqlParameter("@GroupID", groupid),
                //     new SqlParameter("@Icon", "1"),
                //     new SqlParameter("@AddHireDay", "0"),
                //     new SqlParameter("@OILCoefficient","0"),
                //     new SqlParameter("@BSJIP",vccode),
                //     new SqlParameter("@ServerID", "1"),
                //     new SqlParameter("@CarImg","")
                //};
                //string NewID = s.Select(strSql, sqlParameter);
                //if (!NewID.Equals(string.Empty))
                //{
                //    return Utils.GetResult("添加设备成功.", statusCode.Code.success, NewID);
                //}
                //return Utils.GetResult("添加设备失败.", statusCode.Code.failure);
                #endregion
              
            }
            catch (Exception ex)
            {
                return Utils.GetResult(ex.Message, statusCode.Code.error);
            }
        }

        public string AddDeviceBatch(string carnum, string carusername, string cellphone, string devicelist)
        {
            if (string.IsNullOrEmpty(devicelist))
                return Utils.GetResult("请至少添加一台GPS设备.", statusCode.Code.failure);
            List<Dictionary<string, string>> list = Utils.ToList(devicelist);
            if (list.Count <= 0)
                return Utils.GetResult("请至少添加一台GPS设备.", statusCode.Code.failure);
            if (string.IsNullOrEmpty(carnum))
                return Utils.GetResult("车牌号不能为空.", statusCode.Code.failure);
            if (string.IsNullOrEmpty(carusername))
                return Utils.GetResult("联系人不能为空.", statusCode.Code.failure);
            if (string.IsNullOrEmpty(cellphone) || !System.Text.RegularExpressions.Regex.IsMatch(cellphone, @"^[1]+[8,3,5,7]+\d{9}"))
                return Utils.GetResult("号码格式错误.", statusCode.Code.failure);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["CarNum"] = carnum;
            dic["CarUserName"] = carusername;
            dic["CellPhone"] = cellphone;
            if (list.Count == 1)
            {
                ajaxResult ar = Utils.ToObjects<ajaxResult>(AddDevice(list[0]["imei"], list[0]["vc"], myHeader.UserID, "-1",dic));
                return Utils.ToJson(ar);
            }
            else
            { 
                Groups g = new Groups(myHeader);
                ajaxResult ar = Utils.ToObjects<ajaxResult>(g.AddGroups(myHeader.UserID, carusername + "-" + carnum));
                if (ar.StatusCode == statusCode.Code.success && !string.IsNullOrEmpty(ar.Result))
                {
                   
                    var groupid = ar.Result;
                    int ErrCount = 0;
                    StringBuilder sbMsg = new StringBuilder();
                    for (int i = 0; i < list.Count; i++)
                    {
                        ar = Utils.ToObjects<ajaxResult>(AddDevice(list[i]["imei"], list[i]["vc"], myHeader.UserID, groupid, dic));
                        if (ar.StatusCode != statusCode.Code.success)
                        {
                            sbMsg.Append(list[i]["imei"] + ":" + ar.Message);
                            ErrCount++;
                        }
                    }
                    if (ErrCount == 0)
                    {
                        return Utils.GetResult("添加成功.", statusCode.Code.success);
                    }
                    else if (ErrCount == list.Count)
                    {
                        g.DeleteGroups(groupid);
                        return Utils.GetResult("添加失败.", statusCode.Code.failure);
                    }
                    else
                    {
                        return Utils.GetResult("共" + list.Count + "台,添加失败" + ErrCount + "台.", statusCode.Code.failure, sbMsg.ToString());
                    }
                }
                else
                {
                    return Utils.GetResult("添加分组失败.", statusCode.Code.failure);
                }
            }
        }

        /// <summary>
        /// 判断是否存在VCCode，就是数据库的DevicePassword字段
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <returns></returns> 
        public Dictionary<string, string> VCCodeExist(string SerialNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(SerialNumber))
                {
                    return new Dictionary<string, string>();
                }
                string strSql = "select DeviceID,DevicePassword,Created,UserID from devices where Deleted=0 and serialnumber = @SerialNumber  ";
                SQLServerOperating s = new SQLServerOperating();
                Dictionary<string, string> dic = s.Selects(strSql, new SqlParameter[] { new SqlParameter("SerialNumber", SerialNumber) }).toDictionary();
                return dic;
            }
            catch (Exception ex)
            {
                Utils.log("VCCodeExist Error:"+ex.Message);
                return new Dictionary<string, string>();
            }
        }

        public string DeleteDeviceByID(string deviceid)
        {
            try
            {
                if (string.IsNullOrEmpty(deviceid))
                {
                    return Utils.GetResult("参数错误.", statusCode.Code.failure);
                }
                int DeviceID = int.Parse(deviceid);
                string strSql = string.Format("update devices set Deleted=1 where Deleted=0 and deviceID=@deviceID and UserID=@UserID");
                SQLServerOperating s = new SQLServerOperating();
                int status = s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("deviceID", DeviceID), new SqlParameter("UserID", myHeader.UserID) });
                if (status > 0)
                {
                    Task.Run(() => {
                        Utils.SendTcpCmd("VTR-DeleteDevice-" + DeviceID);
                    }); 
                    return Utils.GetResult("删除设备成功.", statusCode.Code.success);
                }
                else
                    return Utils.GetResult("删除设备失败.", statusCode.Code.failure);
            }
            catch (Exception ex)
            {
                return Utils.GetResult(ex.Message, statusCode.Code.error);
            }
        }

        public List<Dictionary<string, string>> GetModelList()
        {
            try
            {
                string strSql = "select DataValue,DataText from [Dictionary]";
                SQLServerOperating s = new SQLServerOperating();
                return s.Selects(strSql).toListDictionary();
            }
            catch (Exception)
            {
                return new List<Dictionary<string, string>>();
            }  
        }

        public string AddGeoFence(string FenceName, string UserID, string Deviceid, string Latitude, string Longitude, string Radius, string Description)
        {
            try
            {
                if (string.IsNullOrEmpty(FenceName) || string .IsNullOrEmpty(UserID) || string.IsNullOrEmpty(Deviceid) || 
                    string .IsNullOrEmpty(Latitude) || string.IsNullOrEmpty(Longitude) || string.IsNullOrEmpty(Radius) || myHeader.UserID != UserID)
                {
                    return Utils.GetResult("参数错误.", statusCode.Code.failure);
                }
                string strSql = @"insert into GeoFence(FenceName, Latitude, Longitude, [Entry], [Exit], Radius, IsInclusion, Created, Deleted, Lat1, Lng1, FenceType, Width, UserID, DeviceID, [Description])
                              values(@FenceName, @Latitude, @Longitude, 0, 0, @Radius, -1, GETDATE(), 0, -1.00000000000000000000, -1.00000000000000000000, 0, -1, 
                             @UserID, @Deviceid, @Description) select @@IDENTITY as gid ";
                Geocoding geo = GetCurrentMapType();
                Gps gps = geo.TranslateGps84(Latitude, Longitude);
                SQLServerOperating s = new SQLServerOperating();
                string gid = s.Select(strSql, new SqlParameter[] {
                    new SqlParameter("FenceName", FenceName),
                    new SqlParameter("Latitude",gps .getWgLat()),
                    new SqlParameter("Longitude",gps .getWgLon()),
                    new SqlParameter ("UserID",UserID),
                    new SqlParameter ("Deviceid",Deviceid),
                    new SqlParameter ("Radius",Radius),
                    new SqlParameter ("Description",Description.ToStringEmpty()),
                });
                if (!gid.Equals(string.Empty))
                {
                    Task.Run(() => {
                        string res = Utils.SendTcpCmd("VTR-DZWL-" + Deviceid);
                        Utils.log("AddGeoFence VTR-DZWL-" + Deviceid + " Result：" + res);
                    }); 
                    return Utils.GetResult("添加成功.", statusCode.Code.success, gid);
                }
                else
                {
                    return Utils.GetResult("添加失败.", statusCode.Code.failure, "");
                }
            }
            catch (Exception ex)
            {
                return Utils.GetResult(ex.Message, statusCode.Code.error, "");
            }
        }

        public string UpdateGeoFence(string FenceID,string FenceName,string Latitude, string Longitude, string Radius, string Description)
        {
            try
            {
                if (string.IsNullOrEmpty(FenceName) || string.IsNullOrEmpty(Latitude) || string.IsNullOrEmpty(Longitude) || string.IsNullOrEmpty(Radius))
                {
                    return Utils.GetResult("参数错误.", statusCode.Code.failure);
                }
                string strSql = "update GeoFence set FenceName=@FenceName,Radius=@Radius,Latitude=@Latitude, Longitude=@Longitude,Description=@Description where GeofenceID=@GeofenceID";
                SQLServerOperating s = new SQLServerOperating();
                Geocoding geo = GetCurrentMapType();
                Gps gps = geo.TranslateGps84(Latitude,Longitude);
                int status = s.ExecuteSql(strSql,new SqlParameter[] {
                    new SqlParameter("GeofenceID", FenceID),
                    new SqlParameter("FenceName",FenceName),
                    new SqlParameter ("Radius",Radius),
                    new SqlParameter ("Latitude",gps.getWgLat()),
                    new SqlParameter ("Longitude",gps.getWgLon()),
                    new SqlParameter ("Description",Description.ToStringEmpty())
                });
                if (status > 0)
                {
                    strSql = "select DeviceID from GeoFence where GeofenceID=@GeofenceID";
                    string Deviceid = s.Select(strSql, new SqlParameter[] { new SqlParameter("GeofenceID", FenceID) });
                    Task.Run(() => {
                        string res = Utils.SendTcpCmd("VTR-DZWL-" + Deviceid);
                        Utils.log("AddGeoFence VTR-DZWL-" + Deviceid + " Result：" + res);
                    }); 
                    return Utils.GetResult("修改成功.", statusCode.Code.success);
                }
                else
                {
                    return Utils.GetResult("修改失败.", statusCode.Code.failure);
                }
            }
            catch (Exception ex)
            {
                return Utils.GetResult(ex.Message, statusCode.Code.error);
            }
        }

        public string DeleteGeoFence(string FenceID)
        {
            try
            {
                string strSql = "delete from GeoFence where GeofenceID=@GeofenceID";
                SQLServerOperating s = new SQLServerOperating();
                int status = s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("GeofenceID", FenceID),new SqlParameter("UserID", myHeader.UserID) });
                if (status > 0)
                {
                    strSql = "select DeviceID from GeoFence where GeofenceID=@GeofenceID";
                    string DeviceID = s.Select(strSql, new SqlParameter[] { new SqlParameter("GeofenceID", FenceID) });
                    Task.Run(()=> {
                        string res = Utils.SendTcpCmd("VTR-DZWL-" + DeviceID);
                        Utils.log("AddGeoFence VTR-DZWL-" + DeviceID + " Result：" + res);
                    }); 
                    return Utils.GetResult("删除成功.", statusCode.Code.success);
                }
                else
                {
                    return Utils.GetResult("删除失败.", statusCode.Code.failure);
                }
            }
            catch (Exception ex)
            {
                return Utils.GetResult( ex.Message, statusCode.Code.error);
            }
        }

        public List<Dictionary<string, string>> GetGeoFenceList(string UserID)
        {
            
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            try
            {
                if (string.IsNullOrEmpty(UserID))
                {
                    return list;
                }
                int UID = int.Parse(UserID);
                string strSql = " select GeofenceID, FenceName, Latitude, Longitude,   Radius,  Created,  UserID, DeviceID, Description from GeoFence where deleted=0 and deviceid>0 and radius>0 and UserID=@UserID";
                SQLServerOperating s = new SQLServerOperating();
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("UserID", UserID) }); 
                Geocoding geo = GetCurrentMapType();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dic[dc.ColumnName] = row[dc.ColumnName].toStringEmpty();
                    } 
                    Gps gps = geo.Translate(dic["Latitude"], dic["Longitude"],false);
                    dic["Latitude"] = gps.getWgLat().toStringEmpty();
                    dic["Longitude"] = gps.getWgLon().toStringEmpty();
                    list.Add(dic);
                }
            }
            catch (Exception ex)
            {
                Utils.log("GetGeoFenceList Error:" + ex.Message + ",堆栈：" + ex.StackTrace); 
            }
            return list;
        }

        public Dictionary<string, string> GetGeoFenceInfoByID(string FenceID)
        {
            try
            {
                int GeoFenceID = 0;
                int.TryParse(FenceID,out GeoFenceID);
                if (string.IsNullOrEmpty(FenceID) || GeoFenceID == 0)
                {
                    return new Dictionary<string, string>();
                }
                string strSql = " select GeofenceID, FenceName, Latitude, Longitude,   Radius,  Created,  UserID, DeviceID, Description from GeoFence where deleted=0 and deviceid>0 and radius>0 and userid=@UserID and GeofenceID=@GeofenceID";
                SQLServerOperating s = new SQLServerOperating();
                Dictionary<string, string> dic = s.Selects(strSql, new SqlParameter[] { new SqlParameter("GeofenceID", GeoFenceID), new SqlParameter("UserID", myHeader.UserID) }).toDictionary();
                // if (dic.Count > 0)
                // {
                Geocoding geo = GetCurrentMapType();
                Gps gps = geo.Translate(dic["Latitude"], dic["Longitude"], false);

                dic["Latitude"] = gps.getWgLat().toStringEmpty();
                dic["Longitude"] = gps.getWgLon().toStringEmpty();
                // }
                return dic;
            }
            catch (Exception ex)
            {
                Utils.log("GetGeoFenceInfoByID Error:" + ex.Message + ",堆栈：" + ex.StackTrace+ ",FenceID:"+ FenceID);
                return new Dictionary<string, string>();
            }
        
        }

        public List<Dictionary<string, string>> GetGeoFenceListByDeviceID(string DeviceID)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            try
            {
                if (string.IsNullOrEmpty( DeviceID) || DeviceID == "undefined")
                {
                    return list;
                }
                string strSql = " select GeofenceID, FenceName, Latitude, Longitude, Radius,  Created,  UserID, DeviceID, Description from GeoFence where deleted=0 and deviceid>0 and radius>0 and DeviceID=@DeviceID";
                SQLServerOperating s = new SQLServerOperating();
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("DeviceID", DeviceID) }); 
                Geocoding geo = GetCurrentMapType();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dic[dc.ColumnName] = row[dc.ColumnName].toStringEmpty();
                    }
                    Gps gps = geo.Translate(dic["Latitude"], dic["Longitude"], false); 
                    dic["Latitude"] = gps.getWgLat().toStringEmpty();
                    dic["Longitude"] = gps.getWgLon().toStringEmpty();
                    list.Add(dic);
                }
            }
            catch (Exception ex)
            {
                Utils.log("GetGeoFenceInfoByID Error:" + ex.Message + ",堆栈：" + ex.StackTrace); 
            }
            return list;
        }

        /// <summary>
        /// 给设备添加一键围栏
        /// </summary>
        /// <param name="DeviceID">设备ID</param>
        /// <param name="Radius">半径</param>
        /// <param name="Lat">经度</param>
        /// <param name="Lng">纬度</param>
        /// <returns></returns>
        public string SetGeoFenceByDeviceID(string DeviceID,string Radius,string Lng,string Lat) {
            try
            {
                string strSql = "select GeofenceID from GeoFence where deleted = 0 and DeviceID = @DeviceID";
                SQLServerOperating s = new SQLServerOperating();
                string GeofenceID = s.Select(strSql,new SqlParameter[] { new SqlParameter("DeviceID", DeviceID) });
                if (!GeofenceID.Equals(""))
                {
                    return UpdateGeoFence(GeofenceID, "一键围栏", Lat, Lng, Radius, "");
                }
                else
                {
                   return AddGeoFence("一键围栏", myHeader.UserID, DeviceID, Lat, Lng, Radius, "");
                }
            }
            catch (Exception ex)
            {
                Utils.log("SetGeoFenceByDeviceID Error:"+ex.Message);
                return Utils.GetResult(ex.Message, statusCode.Code.error);
            }
        }

        public string SendCommand(string command)
        {
            try
            {
                //command = "4210001185,SF";
                string[] sps = command.Split(',');
                if (string.IsNullOrEmpty(sps[0]) || sps[0].Length > 30)
                {
                    Utils.log("SendCommand 参数错误:" + (sps.Length >= 1 ? sps[0] : "") + "," + (sps.Length >= 2 ? sps[1] : ""));
                    return Utils.GetResult("参数错误.", statusCode.Code.failure);
                }
                var cmd = sps[1];
                string strSql = "select UserID,Model from devices where serialnumber = @serialnumber and deleted = 0";
                SQLServerOperating s = new SQLServerOperating ();
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("serialnumber", sps[0]) });
                string UserID = dt.Rows[0]["UserID"].ToString();
                string Model = dt.Rows[0]["Model"].ToString();
                if (UserID != myHeader.UserID)
                {
                    return Utils.GetResult("您无权操作此设备.", statusCode.Code.failure);
                }
                string recStr;
                string Prefix = "VTR-Command-";
                if (Model == "81")//型号是X11BDY
                {
                    //VTR-Command-13350000146-DEFENSE,ON#
                    if (cmd.IndexOf("SF") >= 0)
                        cmd = "DEFENSE,ON#";
                    if (cmd.IndexOf("SF") >= 0)
                        cmd = "DEFENSE,OFF#";
                    if (cmd.IndexOf("DY") >= 0)
                        cmd = "BRAKE,ON#";
                    if (cmd.IndexOf("TY") >= 0)
                        cmd = "BRAKE,OFF#";
                    cmd = Prefix + sps[0] + "-" + cmd.Replace("/", ",");
                    Log.Info(this, cmd);
                    recStr = Utils.SendTcpCmd(cmd);
                }
                else
                {
                    List<string> cmds = new List<string>();
                    cmd = (cmd == "GM" ? "SM" : cmd);
                    cmds.Add("SF"); cmds.Add("CF"); cmds.Add("DY"); cmds.Add("TY"); cmds.Add("SHUT"); cmds.Add("KM"); cmds.Add("SM");
                    if (!cmds.Contains(cmd) && cmd.IndexOf("Rate") < 0)
                    {
                        return Utils.GetResult("指令错误.", statusCode.Code.failure);
                    }
                    #region  3008000000和3528888000开头的设备特殊处理 
                    if (sps[0].StartsWith("3008000000") || sps[0].StartsWith("3528888000"))
                    {
                        string serverid2 = "";
                        if (cmd == "SF")
                        {
                            serverid2 = "1";
                        }
                        else if (cmd == "CF")
                        {
                            serverid2 = "0";
                        }
                        if (serverid2.Equals(""))
                        {
                            //return Utils.GetResult("发送失败.", statusCode.Code.failure);
                            //cmd = "";
                        }
                        else
                        {
                            //启用数据库Devices表ServerID2列， 等于1则关闭微信推送震动报警，为0或者null则要微信推送震动报警,数据库默认为null
                            strSql = "update Devices set ServerID2=" + serverid2 + " where SerialNumber=@SerialNumber and Deleted=0";
                            int count = s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("SerialNumber", sps[0]) });
                            if (count > 0)
                            {
                                return Utils.GetResult("发送成功.", statusCode.Code.success);
                            }
                            else
                            {
                                return Utils.GetResult("发送失败.", statusCode.Code.failure);
                            }
                        }
                    }
                    #endregion

                    if (cmd.IndexOf("Rate") > 0)
                    {
                        cmd = cmd.Replace("-", ",");
                    }

                    cmd = Prefix + string.Join("-", sps);

                    recStr = Utils.SendTcpCmd(cmd);
                }
                if (recStr == "1")
                {
                    return Utils.GetResult("发送成功.", statusCode.Code.success); 
                }
                else
                {
                    return Utils.GetResult("发送失败.", statusCode.Code.failure);
                }
            }
            catch (Exception ex)
            {
                return Utils.GetResult(ex.Message, statusCode.Code.error);
            }
        }
 
        
    }
}
