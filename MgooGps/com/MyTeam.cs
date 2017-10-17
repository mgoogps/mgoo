using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;

namespace MgooGps.com
{
    public class MyTeam
    {
        public static DataTable GetMyTeamList(String UserType = null)
        {

            String strSql = @"with subqry(UserID,UserName,ParentID,UserType) as (
                              select UserID,UserName,ParentID,UserType from Users   where UserID =" + Utils.GetSession("UserInfo").UserID + @"
                              union all
                              select Users.UserID,Users.UserName,Users.ParentID,Users.UserType from Users,subqry
                              where Users.ParentID = subqry.UserID and users.Deleted!=1 " + (UserType == null ? "" : " and Users.UserType=2 ") + @"
                              )
                              select UserID,UserName,ParentID,UserType from subqry order by username collate Chinese_PRC_CS_AS_KS_WS;";
            return com.Dao.Selects(strSql);
            //return com.Dao.Selects("select UserID,ParentID,UserName,LoginName,UserType,FirstName,Address1,CellPhone,Status,Created,Deleted,SuperAdmin from Users where Deleted!=1 and UserID= " + ((UserInfo)Utils.GetSession("UserInfo")).UserID + " or ParentID = " + ((UserInfo)Utils.GetSession("UserInfo")).UserID);
        }
        /// <summary>
        ///  根据用户ID 查询 该经销商下所有的设备包括分组信息
        /// </summary>
        /// <param name="UserID">经销商ＩＤ或用户ＩＤ</param>
        /// <param name="SerialNumber">如果是用IMEI号登录的，就根据IMEI号查询</param>
        /// <returns></returns>
        public static DataTable getDevicesList(String UserID = null, String SerialNumber = null, string LowerDevice = null)
        {
            try
            {
                String strSql = "";
                if (!string.IsNullOrEmpty(SerialNumber))
                {
                    strSql = string.Format(@"select d.DeviceID,d.SerialNumber,d.DeviceName,l.LastCommunication, datediff(MI,l.LastCommunication, getdate()) status,l.BaiduLat,l.BaiduLng,Speed,l.DataContext,l.Course,d.Icon
                                          ,DATEADD(HH,8, l.DeviceUtcDate)DeviceUtcDate,datediff(MI,StopStartUtcDate,serverutcdate) StopTime,d.CarImg,d.Model,di.DataText, DATEDIFF(mi,l.lastcommunication,getdate()) OfflineTime,l.DataType,DATEADD(HH,8 ,l.StopStartUtcDate)StopStartUtcDate,case when di.AccountID=2 then 7 else di.SortOrder end as offLineMi
                                          from Devices d left join LKLocation l on l.DeviceID = d.DeviceID left join Dictionary di on d.Model=di.DataValue
                                          where d.SerialNumber = '{0}' and d.Deleted = 0", SerialNumber);
                }
                else if (!string.IsNullOrEmpty(LowerDevice) && LowerDevice == "true")
                {
                    //查询该用户以及下属用户所有设备
                    strSql = string.Format(@" with subqry(UserID ) as (
                            select UserID from Users where UserID = {0}
                            union all
                            select Users.UserID from Users,subqry
                            where Users.ParentID = subqry.UserID and users.Deleted != 1
                            )
                         select d.DeviceID,d.SerialNumber,d.DeviceName,ISNULL(g.GroupID, -1) GroupID, ISNULL(GroupName, '默认分组')GroupName, d.UserID, Username,l.LastCommunication,
                         datediff(MI, l.LastCommunication, getdate()) status,Speed,l.DataContext,l.Course,d.Icon
                        ,DATEADD(HH, 8, l.DeviceUtcDate)DeviceUtcDate,datediff(MI, StopStartUtcDate, serverutcdate) StopTime,d.CarImg,d.Model,di.DataText,
                         DATEDIFF(mi, l.lastcommunication, getdate()) OfflineTime,l.DataType,DATEADD(HH, 8, l.StopStartUtcDate)StopStartUtcDate,d.HireExpireDate,l.OLat BaiduLat, l.OLng BaiduLng,case when di.AccountID=2 then 7 else di.SortOrder end as offLineMi from
                              devices d left
                        join Groups g on g.GroupID = d.GroupID left
                        join LKLocation l on l.DeviceID = d.DeviceID left
                        join Dictionary di on d.Model = di.DataValue
                       where d.userID in(select UserID from subqry ) and d.Deleted = 0", UserID);
                }
                else
                {
                    strSql = string.Format(@"select d.DeviceID,d.SerialNumber,d.DeviceName,ISNULL(g.GroupID,-1) GroupID, ISNULL(GroupName,'默认分组')GroupName, d.UserID, Username,l.LastCommunication, datediff(MI,l.LastCommunication, getdate()) status,Speed,l.DataContext,l.Course,d.Icon
                              ,DATEADD(HH,8, l.DeviceUtcDate)DeviceUtcDate,datediff(MI,StopStartUtcDate,serverutcdate) StopTime,d.CarImg,d.Model,di.DataText, DATEDIFF(mi,l.lastcommunication,getdate()) OfflineTime,l.DataType,DATEADD(HH,8 ,l.StopStartUtcDate)StopStartUtcDate,d.HireExpireDate,l.OLat BaiduLat,l.OLng BaiduLng,case when di.AccountID=2 then 7 else di.SortOrder end as offLineMi,l.CarStatus
                              from Devices d full join Groups g on g.GroupID=d.GroupID left join LKLocation l on l.DeviceID = d.DeviceID left join Dictionary di on d.Model=di.DataValue
                              where  (g.UserID = {0} or d.UserID={0}) and d.Deleted !=1 order by g.GroupID desc, d.DeviceName collate Chinese_PRC_CS_AS_KS_WS  asc, StopTime, [status],OfflineTime desc  ", UserID);
                }

                DataTable dt = com.Dao.Selects(strSql);

                double bd_lat = 0, bd_lng = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(dt.Rows[i]["BaiduLng"].ToString()) && !string.IsNullOrWhiteSpace(dt.Rows[i]["BaiduLat"].ToString()))
                    {
                       // if (Convert.ToDouble(dt.Rows[i]["BaiduLng"].ToString()) > -1.00000000000000000000 && Convert.ToDouble(dt.Rows[i]["BaiduLat"].ToString()) > -1.00000000000000000000)
                       // { 
                            com.EvilTransform.bd_encrypt(Convert.ToDouble(dt.Rows[i]["BaiduLat"]), Convert.ToDouble(dt.Rows[i]["BaiduLng"]), ref bd_lat, ref bd_lng); 
                            dt.Rows[i]["BaiduLat"] = bd_lat;
                            dt.Rows[i]["BaiduLng"] = bd_lng;
                            if (dt.Rows[i]["DataText"].ToString().StartsWith("MG-X83B") && dt.Rows[i]["CarStatus"]!=null && dt.Rows[i]["CarStatus"].ToString()!="")
                            {
                                var cs = dt.Rows[i]["CarStatus"].ToString();
                                dt.Rows[i]["DataContext"] = "0-0-0-0-" + cs.Split(',')[2];
                            }
                       // }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Utils.log("getDevicesList Error:" + ex.Message);
                return new DataTable();
            } 
        }

  

        /// <summary>
        /// 实时跟踪 数据
        /// </summary>
        /// <returns></returns>
        public static DataTable Tracking(String DeviceID, String UserID)
        {
            String strSql = " select LocationID,LastCommunication,ServerUtcDate,DATEADD(HH,8, DeviceUtcDate)DeviceUtcDate,DATEADD(HH,8,StopStartUtcDate)StopStartUtcDate,l.olat BaiduLat,l.olng BaiduLng,Speed,Course,DataType,d.DeviceName,d.SerialNumber,d.Icon,datediff(MI,l.LastCommunication, getdate()) status from LKLocation l inner join Devices d on l.DeviceID=d.DeviceID where l.DeviceID = " + DeviceID;
            DataTable dt = com.Dao.Selects(strSql);
            double bd_lat = 0, bd_lng = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(dt.Rows[i]["BaiduLng"].ToString()) && !string.IsNullOrWhiteSpace(dt.Rows[i]["BaiduLat"].ToString()))
                {
                    if (Convert.ToDouble(dt.Rows[i]["BaiduLng"].ToString()) > -1.00000000000000000000 && Convert.ToDouble(dt.Rows[i]["BaiduLat"].ToString()) > -1.00000000000000000000)
                    {
                        com.EvilTransform.bd_encrypt(Convert.ToDouble(dt.Rows[i]["BaiduLat"]), Convert.ToDouble(dt.Rows[i]["BaiduLng"]), ref bd_lat, ref bd_lng);
                        dt.Rows[i]["BaiduLat"] = bd_lat;
                        dt.Rows[i]["BaiduLng"] = bd_lng;
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// 新增电子围栏
        /// </summary>
        /// <param name="data">电子围栏经纬度数据</param>
        /// <param name="name">名称</param>
        /// <param name="userid">用户id</param>
        /// <param name="deviceid">设备id</param>
        /// <returns></returns>
        public static String addGeofences(String data, String name, String userid, String deviceid,string south_west,string north_east,string type)
        {
            string fenceType = "1";
            if (type.Equals("2"))
            {
                fenceType = "2";
            }
            if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(deviceid) || string.IsNullOrEmpty(south_west) || string.IsNullOrEmpty(north_east))
            {
                return "{\"success\":\"false\"}";
            }
            if (MG_BLL.Common.lib.Permission.IsSMSNotice(userid)) // && Convert.ToInt32( userid) != MG_BLL.Common.lib.Config.aoboUserID
            {
                return "{\"success\":\"false\"}";
            }
            //27.0002,111.19794|26.973609,111.201246|26.976764,111.26887|27.007346,111.257588
            string strSql = @"insert into GeoFence(FenceName, Latitude, Longitude, [Entry], [Exit], Radius, IsInclusion, Created, Deleted, Lat1, Lng1, FenceType, Width, UserID, DeviceID, [Description],
                            SouthWestLat,SouthWestLng,NorthEastLat,NorthEastLng,Bounds,BoundBindIds)
                            values(@name,'-1.00000000000000000000','-1.00000000000000000000',0,0,-1,-1,GETDATE(),0,-1.00000000000000000000,-1.00000000000000000000,@FenceType,-1,@UserID,@DeviceID, @Description,
                            @SouthWestLat,@SouthWestLng,@NorthEastLat,@NorthEastLng,@Bounds,@BoundBindIds)
                            select @@IDENTITY as gid";
           string[] sws = south_west.Split(',');
           string[] nes = north_east.Split(',');
           Hashtable ht = com.Dao.Select(strSql, new SqlParameter[] {
                new SqlParameter("name",name),
                new SqlParameter("FenceType",fenceType),
                new SqlParameter("UserID",userid),
                new SqlParameter("DeviceID","-1"),
                new SqlParameter("Description",""),
                new SqlParameter("SouthWestLat",sws[0]),
                new SqlParameter("SouthWestLng",sws[1]),
                new SqlParameter("NorthEastLat",nes[0]),
                new SqlParameter("NorthEastLng",nes[1]),
                new SqlParameter("Bounds",data),
                new SqlParameter("BoundBindIds",deviceid)
            });
            if (ht !=null)
            {
                return "{\"success\":\"true\",\"gid\":\""+ht["gid"]+"\",\"created\":\"\"}";
            }
            else
            {
                return "{\"success\":\"false\"}";
            } 

           // Geofence.GeofenceAjaxSoapClient geofence = new Geofence.GeofenceAjaxSoapClient();
            //int status = geofence.SavePolygon(Convert.ToInt32(userid), 0, name, data, deviceid);
            //if (status == 0)
            //{
            //    return "{\"success\":\"true\",\"gid\":\"11\",\"created\":\"" + status + "\"}";
            //}
            //else
            //{
            //    return "{\"success\":\"false\"}";
            //}
        }
        public static DataTable GetPolygonDetail(string ZoneID)
        {
           
            try
            {
                if (string.IsNullOrEmpty(ZoneID))
                {
                    return new DataTable();
                }
                string strSql = @"  select GeofenceID, FenceName, Latitude, Longitude, Radius, Created, UserID, DeviceID, Description,SouthWestLat, SouthWestLng, NorthEastLat, NorthEastLng, Bounds, BoundBindIds
                               from GeoFence where deleted = 0 
                               and GeofenceID = @GeofenceID";
                return com.Dao.Selects(strSql,new SqlParameter[] { new SqlParameter("GeofenceID", ZoneID)} );
                //com.Dao.Selects(strSql, new SqlParameter[] { new SqlParameter("GeofenceID", ZoneID)});
                //Geofence.GeofenceAjaxSoapClient gf = new Geofence.GeofenceAjaxSoapClient();
                //str = gf.GetPolygonDetail(ZoneID);
            }
            catch (Exception ex)
            {
                Utils.log("GetPolygonDetail方法出错啦！！！！！ ZoneID:" + ZoneID);
                Utils.log(ex.Message);
                return new DataTable();
            } 
        }
        /// <summary>
        /// 根据DeviceID 查询设备信息
        /// </summary>
        /// <param name="deviceid">设备ID</param>
        /// <returns></returns>
        public static DataTable getDeviceByDeviceID(String deviceid, String UserID = null)
        {
            string where = "";
            SqlParameter[] pars = new SqlParameter[1];
            if (!string.IsNullOrEmpty( UserID))
            {
                where = " and d.UserID = @UserID";
                pars = new SqlParameter[2];
                pars[1] = new SqlParameter("UserID",UserID);
            }
            pars[0] = new SqlParameter("deviceid",deviceid);
            String strSql = "select l.OLat BaiduLat,l.OLng BaiduLng,l.Speed,l.Course,d.Icon,d.DeviceName,d.SerialNumber,d.SpeedLimit,datediff(MI,l.LastCommunication, getdate()) status from Devices d inner join  LKLocation l on d.DeviceID=l.DeviceID where d.DeviceID= @deviceid "+where;
            return Dao.Selects(strSql,pars);
        }
        /// <summary>
        /// 根据电子围栏表ID删除电子围栏
        /// </summary>
        /// <param name="geofenid">ＩＤ</param>
        /// <returns></returns>
        public static String DeleteGeofences(string geofenid)
        {
            Geofence.GeofenceAjaxSoapClient gf = new Geofence.GeofenceAjaxSoapClient();
            int status = gf.DelPolygon(Convert.ToInt32(geofenid));
            if (status == 0)
            {
                String strSql = " select COUNT(*) from GeoFence where GeofenceID=" + geofenid;
                DataRow dr = Dao.Select(strSql);
                if (Convert.ToInt32(dr[0].ToString()) == 0)
                    return "{\"success\":\"true\"}";
                else
                    return "{\"success\":\"false\"}";
            }
            else
                return "{\"success\":\"false\"}";
            //String strSql = "delete from GeoFence where GeofenceID=" + geofenid;
            //if (Dao.ExecutionSQL(strSql) > 0)
            //   return "{\"success\":\"true\"}";
            //else
            //    return "{\"success\":\"false\"}";
        }

        /// <summary>
        /// 获取历史回放轨迹数据
        /// </summary>
        /// <param name="deviceid">设备id</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public static StringBuilder getPlayBack(String deviceid, String startDate, String endDate)
        {
            String strSql = "select z.DeviceUTCTime,z.BaiduLat,z.BaiduLng,z.Speed,z.Course from dbo.ZLocation_" + deviceid + @" z
                      where z.DeviceUTCTime  >= DATEADD(HH,-8,'" + startDate + "') and z.DeviceUTCTime<=DATEADD(HH,-8,'" + endDate + "') order by DeviceUTCTime   ";
            DataTable dt = com.Dao.Selects(strSql);
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
                sb.Append(dr["BaiduLng"] + "," + dr["BaiduLat"] + "," + Utils.GetTimeLikeJS(DateTime.Parse(dr["DeviceUTCTime"].ToString())) + "," + dr["Speed"] + ";");
            return sb;
        }

        /// <summary>
        /// 历史轨迹回放
        /// </summary>
        /// <param name="deviceid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static DataTable getPlayBack(String deviceid, String startDate, String endDate, String strWhere = null)
        {
            DateTime sDate = Convert.ToDateTime(startDate).AddHours(-8);
            DateTime eDate = Convert.ToDateTime(endDate).AddHours(-8);
            DateTime spDate = Convert.ToDateTime("2015-11-04");
            ArrayList list = new ArrayList();
            String strSql = "";
            DataTable dt = null;
            SqlConnection conn = null;
            if (sDate < spDate && eDate < spDate)
            {
                strSql = "select DATEADD(HH,8,z.DeviceUTCTime)DeviceUTCTime,z.BaiduLat,z.BaiduLng,z.Speed,z.Course from dbo.ZLocation_" + deviceid + @" z
                      where z.DeviceUTCTime  >= DATEADD(HH,-8,'" + startDate + "') and z.DeviceUTCTime<=DATEADD(HH,-8,'" + endDate + "') " + strWhere + " order by DeviceUTCTime";
                dt = com.Dao.Selects(strSql);
            }
            else if (sDate < spDate && eDate >= spDate)
            {
                strSql = "select DATEADD(HH,8,z.DeviceUTCTime)DeviceUTCTime,z.BaiduLat,z.BaiduLng,z.Speed,z.Course from dbo.ZLocation_" + deviceid + @" z
                      where z.DeviceUTCTime  >= DATEADD(HH,-8,'" + startDate + "') and z.DeviceUTCTime<=DATEADD(HH,-8,'" + endDate + "') " + strWhere + " order by DeviceUTCTime";
                dt = com.Dao.Selects(strSql);
                double day = (DateTime.Parse(eDate.ToString("yyyy-MM-dd")) - DateTime.Parse(spDate.ToString("yyyy-MM-dd"))).TotalDays + 1;
                conn = Dao.CreateConn(spDate);
                DataTable dTable = new DataTable();
                object[] objArray = new object[dt.Columns.Count];
                double lat = 0, lng = 0;
                for (int i = 0; i < day; i++)
                {
                    if (i > 0 && Convert.ToDateTime(sDate).AddDays(i).Month != Convert.ToDateTime(sDate).AddDays(i - 1).Month)
                    {
                        conn = Dao.CreateConn(Convert.ToDateTime(sDate).AddDays(i));
                    }
                    strSql = "select DATEADD(HH,8,DeviceUTCTime)DeviceUTCTime,OLat BaiduLat,OLng BaiduLng, Speed,Course  from Location" + spDate.AddDays(i).Day + " where DeviceID=" + deviceid + " and DeviceUTCTime  <= '" + eDate + "' " + strWhere + " order by DeviceUTCTime ";
                    dTable = com.Dao.Selects(strSql, conn);
                    for (int j = 0; j < dTable.Rows.Count; j++)
                    {
                        com.EvilTransform.bd_encrypt(Convert.ToDouble(dTable.Rows[j]["BaiduLat"]), Convert.ToDouble(dTable.Rows[j]["BaiduLng"]), ref lat, ref lng);
                        dTable.Rows[j]["BaiduLat"] = lat + "";
                        dTable.Rows[j]["BaiduLng"] = lng + "";
                        dTable.Rows[j].ItemArray.CopyTo(objArray, 0);
                        dt.Rows.Add(objArray);
                    }
                }

            }
            else if (sDate >= spDate && eDate >= spDate)
            {
                dt = new DataTable();
                dt.Columns.Add("DeviceUTCTime"); dt.Columns.Add("BaiduLat"); dt.Columns.Add("BaiduLng"); dt.Columns.Add("Speed"); dt.Columns.Add("Course"); dt.Columns.Add("Type");
                DataTable dTable = new DataTable();
                double day = (DateTime.Parse(eDate.ToString("yyyy-MM-dd")) - DateTime.Parse(sDate.ToString("yyyy-MM-dd"))).TotalDays + 1;
                
                if (day > 33)
                { 
                    return dt;
                }
                
                conn = Dao.CreateConn(sDate);
              
                object[] objArray = new object[dt.Columns.Count];
                double lat = 0, lng = 0;
                for (int i = 0; i < day; i++)
                {
                    if (i > 0 && Convert.ToDateTime(sDate).AddDays(i).Month != Convert.ToDateTime(sDate).AddDays(i - 1).Month)
                    {
                        conn = Dao.CreateConn(Convert.ToDateTime(sDate).AddDays(i));
                    }
                    strSql = "select DATEADD(HH,8,DeviceUTCTime)DeviceUTCTime,OLat BaiduLat,OLng BaiduLng,Speed,Course,DataType from Location" + sDate.AddDays(i).Day + " where DeviceID=" + deviceid + " and  DeviceUTCTime >= '" + sDate + "' and  DeviceUTCTime <= '" + eDate + "' " + strWhere + " order by DeviceUTCTime ";
                    dTable = com.Dao.Selects(strSql, conn);
                    for (int j = 0; j < dTable.Rows.Count; j++)
                    {
                        if (list.Contains(dTable.Rows[j]["DeviceUTCTime"].ToString()))
                            continue;
                        list.Add(dTable.Rows[j]["DeviceUTCTime"].ToString());
                        com.EvilTransform.bd_encrypt(Convert.ToDouble(dTable.Rows[j]["BaiduLat"]), Convert.ToDouble(dTable.Rows[j]["BaiduLng"]), ref lat, ref lng);
                        dTable.Rows[j]["BaiduLat"] = lat + "";
                        dTable.Rows[j]["BaiduLng"] = lng + "";
                        dTable.Rows[j].ItemArray.CopyTo(objArray, 0);
                        dt.Rows.Add(objArray);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 定时刷新报警消息
        /// </summary>
        /// <returns></returns>
        public static DataTable alarmTime(string userid, string lower)
        {
            string strWhere = "  ";
            String strSql = string.Format("select Address2 from Users where UserID={0}", userid);
            string strUserID = userid;

            DataRow dr = Dao.Select(strSql);
            if (dr[0] != null && dr[0].ToString() != "")
                strWhere += " and e.NotificationType in(" + dr[0].ToString() + ") ";
            if (lower != null && bool.Parse(lower))
                strUserID = GetDeviceNumber(userid)["userids"].ToString();

            strSql = string.Format(@" select top 101 e.ExceptionID,e.DeviceID,d.DeviceName,d.SerialNumber,u.UserName,u.UserID,e.NotificationType,case when geo.FenceName is null then Message else Message+':'+geo.FenceName end Message,DataText,DATEADD(HH,8, e.Created)Created 
                                      from ExceptionMessage e inner join Devices d on e.DeviceID = d.DeviceID inner join Dictionary di on di.DataValue = d.Model inner join Users u on d.UserID=u.UserID
                                      left join GeoFence geo on geo.GeofenceID=e.GeoFenceID
                                      where d.Deleted=0  and e.deleted =0 and e.Created > DATEADD(DAY,-10, DATEADD(HH,-8, GETDATE())) and u.UserID in ({0}) {1}  order by e.Created desc ", strUserID, strWhere);
            DataTable dt = com.Dao.Selects(strSql);
            if (dr != null)
            {
                string[] str = dr[0].ToString().Split(',');
                if (str[str.Length - 1] == "-1")
                {
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow row = dt.Rows[i];
                        if (row["NotificationType"].ToString() == "9" && (row["DataText"].ToString() == "MG-X83" || row["DataText"].ToString() == "MG-X82" || row["DataText"].ToString() == "MG-X81"))
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 根据用户id获取30天内的报警信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static DataTable getAlarmList(String userid, String lower = null, String Status = null,string type = null)
        {

            string strWhere = "  ";
            String strSql = string.Format("select Address2 from Users where UserID={0}", userid);
            string strUserID = userid;
            DataRow dr = null;
            if (Status != null && Status.Trim() != "")
            {
                strWhere += "and e.Deleted= " + Status + " ";
                strUserID = GetDeviceNumber(userid)["userids"].ToString();
            }
            else
            {
                strWhere += "and e.Deleted=0 ";
                dr = Dao.Select(strSql);
                if (dr[0] != null && dr[0].ToString() != "")
                    strWhere += "and e.NotificationType in(" + dr[0].ToString() + ") ";
                if (lower != null && bool.Parse(lower))
                    strUserID = GetDeviceNumber(userid)["userids"].ToString();
            }
            string cols = "top 1500 e.ExceptionID,e.SerialNumber,e.DeviceID,u.UserName,u.UserID,e.NotificationType,case when e.[Message]='' then '脱落报警' else e.[Message] end [Message],DATEADD(HH,8, e.Created)Created,e.BaiduLat,e.BaiduLng,DATEADD(HH,8, e.DeviceUTCTime)DeviceUTCTime,d.DeviceName ,di.DataText ,e.ClearBy, case when e.deleted=0 then '未处理' else '已处理' end status";
            if (!string.IsNullOrEmpty(type))
            {
                cols = "ROW_NUMBER() OVER( order by e.created desc) rowIndex, d.DeviceName,d.SerialNumber,u.UserName,e.Message,e.Created,DATEADD(HH,8, e.DeviceUTCTime)DeviceUTCTime,di.DataText,case when e.deleted=0 then '未处理' else '已处理' end status ";
                strUserID = GetDeviceNumber(userid)["userids"].ToString();
            }
             
            strSql = string.Format(@"select "+ cols + @" from ExceptionMessage e inner join Devices d on e.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model inner join users u on d.UserID=u.UserID  
                where d.Deleted=0 and e.Created > DATEADD(DAY,-30, DATEADD(HH,-8, GETDATE())) and u.UserID in({0}) " + strWhere + " order by e.Created desc", strUserID);

            return com.Dao.Selects(strSql);
        }

        public static DataTable GetMessageList(string UserID, string Model, string MsgType, string StartTime, string EndTime)
        {
            try
            { 
                string where = "";
                Dictionary<string, string> parList = new Dictionary<string, string>();
                if (string.IsNullOrEmpty(UserID))
                {
                    UserID = Utils.GetSession().UserID;
                }
                if (!string.IsNullOrEmpty(UserID))
                { 
                    parList.Add("UserID", UserID);
                }
                if (!string.IsNullOrEmpty(Model))
                {
                    parList.Add("Model", Model);
                }
                if (!string.IsNullOrEmpty(MsgType))
                {
                    parList.Add("NotificationType", MsgType);
                }
                if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
                {
                    parList.Add("StartTime", Convert.ToDateTime(StartTime).ToString("yyyy-MM-dd 00:00:00"));
                    parList.Add("EndTime", Convert.ToDateTime(EndTime).ToString("yyyy-MM-dd 23:59:59"));
                }
                if (parList.Count == 0)
                {
                    return new DataTable();
                }
                SqlParameter[] pars = new SqlParameter[parList.Count];
                var index = 0;
                foreach (var item in parList)
                {
                    if (item.Key .Equals("StartTime"))
                    {
                        where += " and em.Created>=@StartTime";
                    }
                    else if (item.Key.Equals("EndTime"))
                    {
                        where += " and em.Created<=@EndTime";
                    }
                    else if (item.Key.Equals("UserID"))
                    {
                        where += " and d.UserID=@UserID";
                    }
                    else
                    {
                        where += " and " + item.Key + "=@" + item.Key;
                    }
                    pars[index++] = new SqlParameter(item.Key, item.Value);
                }
                string strSql = @"select ROW_NUMBER() OVER( order by em.created desc) rowIndex,d.DeviceName,d.SerialNumber,u.UserName,case when geo.FenceName is null then em.Message else em.Message+':'+geo.FenceName end Message,DATEADD(HH,8, em.Created)Created,DATEADD(HH,8, em.DeviceUTCTime)DeviceUTCTime,di.DataText 
                            from ExceptionMessage em inner join Devices d on d.DeviceID=em.DeviceID inner join Users u on u.UserID=d.UserID inner join Dictionary di on di.DataValue=d.Model
                            left join GeoFence geo on geo.GeofenceID=em.GeoFenceID
                            where 1=1 " + where + " order by em.Created desc";
                return Dao.Selects(strSql, pars);
            }
            catch (Exception  )
            {
                return new DataTable();
            }
        }
        /// <summary>
        ///  根据用户id把所有报警信息设为已读
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static String AlarmAllReadonly(String userid, String ExceptionID)
        {
            String strSql = "update ExceptionMessage set Deleted=1,ClearDate=GETDATE(),ClearBy=" + Utils.GetSession("UserInfo").UserID + " where DeviceID in (select DeviceID from Devices where UserID=" + userid + " and Deleted=0) and Deleted=0";
            if (ExceptionID != null)
                strSql += " and ExceptionID = " + ExceptionID;

            if (com.Dao.ExecutionSQL(strSql) > 0)
                return "{\"success\": true }";
            else
                return "{\"success\": false }";

        }

        /// <summary>
        /// 运行总览
        /// </summary>
        /// <param name="dName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetRemainView(String SerialNumber, String startTime, String endTime)
        {
            string where = toDic(SerialNumber, "dr.");
            if (Utils.GetSession("UserInfo").LoginType == "0") {
                where += "  and d.UserID=" + Utils.GetSession("UserInfo").UserID;
            }
            else {
                where += " and d.SerialNumber='" + Utils.GetSession("UserInfo").SerialNumber + "'";
            }
            String strSql = @"select case when d.DeviceName='' then dr.SerialNumber else d.DeviceName end DeviceName,convert(varchar(10),DATEADD(HH,8, dr.UpdateTime),120) date,sum([NowDistance]) nowDistance,sum([WarnCount])warn,sum([SpeedLimitCount])speedlimit,sum([StopCount])Stop from DevicesReport dr inner join Devices d on dr.DeviceID=d.DeviceID
                      where DATEADD(HH,8, dr.UpdateTime) >= '" + DateTime.Parse(startTime).ToString("yyyy-MM-dd 00:00:00") + "' and  DATEADD(HH,8, dr.UpdateTime) <= '" + DateTime.Parse(endTime).ToString("yyyy-MM-dd 23:59:59") + "' " + where + @"  
                                and d.Deleted=0 group by dr.SerialNumber,convert(varchar(10),DATEADD(HH,8, dr.UpdateTime),120),d.DeviceName order by date";
            return Dao.Selects(strSql);
        }

        /// <summary>
        /// 里程统计
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMileage(String yh, String SerialNumber, String stime, String etime)
        {
            if (SerialNumber == "")
            {
                return new DataTable();
            }
            DateTime sDate = Convert.ToDateTime(DateTime.Parse(stime).ToString("yyyy-MM-dd 00:00:00")).AddHours(0);
            DateTime eDate = Convert.ToDateTime(DateTime.Parse(etime).ToString("yyyy-MM-dd 23:59:59")).AddHours(0);
            DateTime spDate = Convert.ToDateTime("2015-11-09");
            DataTable dt = new DataTable();
            string where = toDic(SerialNumber, "d.");
            String strSql = "select DeviceID from Devices d where d.Deleted=0 " + where;
            string DeviceID = Dao.Select(strSql)[0].ToString();
            if (Utils.GetSession("UserInfo").LoginType == "0")
            {
                where += " and d.UserID=" + Utils.GetSession("UserInfo").UserID;
            }
            else
            {
                where += " and d.SerialNumber='" + Utils.GetSession("UserInfo").SerialNumber + "'";
            }
           
            strSql = @"select ROW_NUMBER() OVER (ORDER BY convert(varchar(10),DATEADD(HH,8,dr.CreateTime),120)) num,case when d.DeviceName='' then d.SerialNumber else d.DeviceName end DeviceName ,d.SerialNumber,convert(varchar(10),DATEADD(HH,8,dr.CreateTime),120) ct, 
                        SUM(dr.NowDistance) nd,SUM(WarnCount) wc,SUM(SpeedLimitCount) sl,SUM(StopCount)sc,cast( (" + yh + @"/100.0)*SUM(dr.NowDistance) as numeric(10,2))yh from Devices d inner join DevicesReport dr on d.DeviceID=dr.DeviceID  
                        where d.Deleted=0 " + where + " and dr.CreateTime > DATEADD(HH,-8,'" + sDate + "') and dr.CreateTime < DATEADD(HH,-8,'" + eDate + @"')
                        group by d.SerialNumber,convert(varchar(10),DATEADD(HH,8,dr.CreateTime),120),d.DeviceName order by ct";
         
            dt = Dao.Selects(strSql);
            return dt;
        }

        /// <summary>
        /// 停留详单，直接查询SpeedReport
        /// </summary>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        /// <param name="dName"></param>
        /// <returns></returns>
        public static DataTable GetSpeedReport(String sTime, String eTime, String dName)
        {
            string where = toDic(dName, "d.");
            if (Utils.GetSession("UserInfo").LoginType == "0") {
                where += " and d.UserID=" + Utils.GetSession("UserInfo").UserID;
            }
            else {
                where += " and d.SerialNumber='" + Utils.GetSession("UserInfo").SerialNumber + "'";
            }
            String strSql = @"select ROW_NUMBER() OVER (ORDER BY  sr.startTime desc) num,case when d.DeviceName='' then d.SerialNumber else d.DeviceName end DeviceName, DATEADD(HH,8, sr.StartTime)st, DATEADD(HH,8, sr.EndTime)et,Latitude,Longitude,sr.Address,sr.TimediffMinute,sr.SerialNumber from
                       SpeedReport sr inner join Devices d on sr.DeviceID=d.DeviceID where d.Deleted=0 and StartTime>='" + DateTime.Parse(sTime).ToString("yyyy-MM-dd 00:00:00") + "' and sr.EndTime<='" + DateTime.Parse(eTime).ToString("yyyy-MM-dd 23:59:59") + "' " + where + " order by StartTime desc";
            return Dao.Selects(strSql);
        }

        /// <summary>
        /// 停留详单，根据历史轨迹算出停留点
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="dName">IMEI号的集合</param>
        /// <param name="DeviceID">设备ID</param>
        /// <param name="Filter">是否过滤无线</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public static DataTable GetSpeedReport(String sTime, String eTime, String dName, ref String DeviceID,string Filter = null, string strWhere = null)
        {

            string where = toDic(dName, "");
            string strSql = "";
            int FindType = 0; //按设备查询
            if (string.IsNullOrEmpty(where))
            {
                FindType = 1; //按用户查询
            }
            //else
            //{
            //    strSql = "select DeviceID from Devices d where d.Deleted=0 " + where;
            //    DeviceID = Dao.Select(strSql)[0].ToString();
            //}
            
            SqlConnection conn = null;
            if (!string.IsNullOrWhiteSpace(sTime))
            {
                where += " and DATEADD(HH,8,DeviceUTCTime) >= '" + DateTime.Parse(sTime).ToString("yyyy-MM-dd 00:00:00") + "'";
            }
            if (!string.IsNullOrWhiteSpace(eTime))
            {
                where += " and DATEADD(HH,8,DeviceUTCTime)<='" + DateTime.Parse(eTime).ToString("yyyy-MM-dd 23:59:59") + "'";
            }
            DataTable dt = null;
            DateTime sDate = Convert.ToDateTime(DateTime.Parse(sTime).ToString("yyyy-MM-dd 00:00:00")).AddHours(-8);
            DateTime spDate = Convert.ToDateTime("2015-11-04");
            DataTable dTable = new DataTable();
            if (string.IsNullOrWhiteSpace(eTime) && !string.IsNullOrWhiteSpace(sTime))
            {
                #region 停留折线图
                where = " and convert(char(10) ,DATEADD(HH,8,DeviceUTCTime) , 120) = '" + sTime + "'";
                if (sDate >= spDate)
                {
                    conn = Dao.CreateConn(sDate);
                }
                if (conn == null)
                {
                    strSql = "select DeviceID,Speed,DATEADD(HH,8,DeviceUTCTime) DeviceTime,Latitude,Longitude from ZLocation_" + DeviceID + " where 1=1 " + where + strWhere;
                    dt = Dao.Selects(strSql);
                }
                else
                {
                    double day = 2;//(DateTime.Parse(eDate.ToString("yyyy-MM-dd")) - DateTime.Parse(sDate.ToString("yyyy-MM-dd"))).TotalDays + 1;
                    dt = new DataTable();
                    dt.Columns.Add("DeviceID"); dt.Columns.Add("Speed"); dt.Columns.Add("DeviceTime"); dt.Columns.Add("Latitude"); dt.Columns.Add("Longitude");
                    object[] objArray = new object[dt.Columns.Count];
                    double lat = 0, lng = 0;
                    for (int i = 0; i < day; i++)
                    {
                        if (i > 0 && Convert.ToDateTime(sTime).AddDays(i).Month != Convert.ToDateTime(sTime).AddDays(i - 1).Month)
                        {
                            conn = Dao.CreateConn(Convert.ToDateTime(sTime).AddDays(i));//不同的月份需要换新的数据库连接字符串
                        }
                        strSql = "select DeviceID,Speed,DATEADD(HH,8,DeviceUTCTime) DeviceTime,OLat Latitude,OLng Longitude from Location" + sDate.AddDays(i).Day + " where DeviceID=" + DeviceID + " and  DeviceUTCTime >= '" + sDate + "' and  DeviceUTCTime <= '" + sDate.AddHours(24) + "' " + strWhere;
                        dTable = Dao.Selects(strSql, conn);
                        for (int j = 0; j < dTable.Rows.Count; j++)
                        {
                            com.EvilTransform.bd_encrypt(Convert.ToDouble(dTable.Rows[j]["Latitude"]), Convert.ToDouble(dTable.Rows[j]["Longitude"]), ref lat, ref lng);
                            dTable.Rows[j]["Latitude"] = lat + "";
                            dTable.Rows[j]["Longitude"] = lng + "";
                            dTable.Rows[j].ItemArray.CopyTo(objArray, 0);
                            dt.Rows.Add(objArray);
                        }
                    }

                    // strSql = "select DeviceID,Speed,DATEADD(HH,8,DeviceUTCTime) DeviceTime,OLat Latitude,OLng Longitude from Location" + sDate.Day + " where DeviceID=" + DeviceID + " " + where + strWhere;
                    //dt = Dao.Selects(strSql, conn);
                }
                DataRow AppendRow = dt.NewRow();
                AppendRow["Speed"] = "0";
                AppendRow["DeviceTime"] = sTime + " 23:59:59";
                dt.Rows.InsertAt(AppendRow, dt.Rows.Count);

                AppendRow = dt.NewRow();
                AppendRow["DeviceTime"] = sTime + " 00:00:00";
                AppendRow["Speed"] = "0";
                dt.Rows.InsertAt(AppendRow, 0);
                if (dt.Rows.Count > 0)
                {
                    //填充数据。
                    DataRow Rear = null, Ago = null;
                    DateTime RearTime, AgoTime;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Rear = dt.Rows[i];   //当前DataRow
                        Ago = dt.Rows[(i == 0 ? i : i - 1)]; //上一个DataRow
                        RearTime = Convert.ToDateTime(Rear["DeviceTime"]);
                        AgoTime = Convert.ToDateTime(Ago["DeviceTime"]);
                        double s = (RearTime - AgoTime).TotalSeconds;
                        if (s > 25)
                        {
                            Utils.insertDataRow(ref dt, Ago, Rear, i);
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region 停留详单

                DateTime eDate = Convert.ToDateTime(DateTime.Parse(eTime).ToString("yyyy-MM-dd 23:59:59")).AddHours(-8);
                conn = Dao.CreateConn(sDate);
                if (sDate < spDate && eDate < spDate)
                {
                    #region 2015-11-04之前
                    strSql = "select DeviceID,Speed,DATEADD(HH,8,DeviceUTCTime) DeviceTime,Latitude,Longitude from ZLocation_" + DeviceID + " where 1=1 " + where + strWhere;
                    dt = Dao.Selects(strSql);
                    #endregion
                }
                else if (sDate < spDate && eDate >= spDate)
                {
                    #region 包括2015-11-04 
                    strSql = "select DeviceID,Speed,DATEADD(HH,8,DeviceUTCTime) DeviceTime,Latitude,Longitude from ZLocation_" + DeviceID + " where 1=1 " + where + strWhere;
                    dt = Dao.Selects(strSql);
                    double day = (DateTime.Parse(eDate.ToString("yyyy-MM-dd")) - DateTime.Parse(spDate.ToString("yyyy-MM-dd"))).TotalDays + 1;
                    object[] objArray = new object[dt.Columns.Count];
                    double lat = 0, lng = 0;
                    for (int i = 0; i < day; i++)
                    {
                        if (i > 0 && sDate.AddDays(i).Month != sDate.AddDays(i - 1).Month)
                            conn = Dao.CreateConn(Convert.ToDateTime(sTime).AddDays(i));//不同的月份需要换新的数据库连接字符串 
                        strSql = "select DeviceID,Speed,DATEADD(HH,8,DeviceUTCTime) DeviceTime,OLat Latitude,OLng Longitude from Location" + Convert.ToDateTime("2015-11-04").AddDays(i).Day + " where DeviceID=" + DeviceID + " and DeviceUTCTime <= '" + eDate + "' " + strWhere;
                        dTable = Dao.Selects(strSql, conn);
                        for (int j = 0; j < dTable.Rows.Count; j++)
                        {
                            com.EvilTransform.bd_encrypt(Convert.ToDouble(dTable.Rows[j]["Latitude"]), Convert.ToDouble(dTable.Rows[j]["Longitude"]), ref lat, ref lng);
                            dTable.Rows[j]["Latitude"] = lat + "";
                            dTable.Rows[j]["Longitude"] = lng + "";
                            dTable.Rows[j].ItemArray.CopyTo(objArray, 0);
                            dt.Rows.Add(objArray);
                        }
                    }
                    #endregion
                }
                else if (sDate >= spDate && eDate >= spDate)
                {
                    #region 2015-11-04之后 
                    double day = (DateTime.Parse(eDate.ToString("yyyy-MM-dd")) - DateTime.Parse(sDate.ToString("yyyy-MM-dd"))).TotalDays + 1;
                    if (day > 7 || day <=0)
                    {
                        return null;
                    }
                    if (FindType == 1)
                    {
                        strSql = @"with subqry(UserID) as (
                                select UserID from Users where UserID = @
                                union all
                                select Users.UserID from Users,subqry where Users.ParentID = subqry.UserID and users.Deleted = 0
                                )  
                                select l.DeviceID,dev.DeviceName,Speed,DATEADD(HH, 8, DeviceUTCTime) DeviceTime,OLat Latitude, OLng Longitude from YWData201707.dbo.Location12 l
                                inner join (select DeviceID, DeviceName from Devices d where exists(select * from subqry s where s.UserID = d.UserID)) dev on dev.DeviceID = l.DeviceID
                                where DeviceUTCTime >= '2017-6-13 09:59:21' and DeviceUTCTime <= '2017-7-13 09:59:26' " + strWhere;
                    }
                 
                    dt = new DataTable();
                    dt.Columns.Add("DeviceID"); dt.Columns.Add("Speed"); dt.Columns.Add("DeviceTime"); dt.Columns.Add("Latitude"); dt.Columns.Add("Longitude");
                    object[] objArray = new object[dt.Columns.Count];
                    double lat = 0, lng = 0;
                    for (int i = 0; i < day; i++)
                    {
                        if (i > 0 && Convert.ToDateTime(sDate).AddDays(i).Month != Convert.ToDateTime(sDate).AddDays(i - 1).Month)
                            conn = Dao.CreateConn(Convert.ToDateTime(sTime).AddDays(i));
                        strSql = "select DeviceID,Speed,DATEADD(HH,8,DeviceUTCTime) DeviceTime,OLat Latitude,OLng Longitude from Location" + sDate.AddDays(i).Day + " where DeviceID in(" + DeviceID + ") and  DeviceUTCTime >= '" + sDate + "' and  DeviceUTCTime <= '" + eDate + "' " + strWhere;
                        dTable = Dao.Selects(strSql, conn);
                        for (int j = 0; j < dTable.Rows.Count; j++)
                        {
                            com.EvilTransform.bd_encrypt(Convert.ToDouble(dTable.Rows[j]["Latitude"]), Convert.ToDouble(dTable.Rows[j]["Longitude"]), ref lat, ref lng);
                            dTable.Rows[j]["Latitude"] = lat + "";
                            dTable.Rows[j]["Longitude"] = lng + "";
                            dTable.Rows[j].ItemArray.CopyTo(objArray, 0);
                            dt.Rows.Add(objArray);
                        }
                    }
                    #endregion
                }

                DataTable eDt = new DataTable();
                eDt.Columns.Add("rowid"); eDt.Columns.Add("DeviceName"); eDt.Columns.Add("startTime"); eDt.Columns.Add("endTime"); eDt.Columns.Add("Longitude"); eDt.Columns.Add("Latitude"); eDt.Columns.Add("address"); eDt.Columns.Add("time"); eDt.Columns.Add("imei");
                int len = dt.Rows.Count;
                DataRow imeiAndName = Dao.Select("select case when DeviceName='' then SerialNumber else DeviceName end DeviceName,SerialNumber from Devices where deviceid in(" + DeviceID + ")");
                string deviceName = imeiAndName["DeviceName"].ToString();
                string imei = imeiAndName["SerialNumber"].ToString();
                for (int i = 1; i < len; i++)
                {
                    DataRow lastRow = dt.Rows[i - 1]; DataRow currentRow = dt.Rows[i];
                    DateTime start = Convert.ToDateTime(lastRow["DeviceTime"]);
                    DateTime end = Convert.ToDateTime(currentRow["DeviceTime"]);
                    TimeSpan ts = start.Subtract(end).Duration();
                    if (ts.TotalSeconds > 600)
                    {
                        DataRow dr = eDt.NewRow();
                        dr["startTime"] = start.ToString("yyyy-MM-dd HH:mm:ss");
                        dr["endTime"] = end.ToString("yyyy-MM-dd HH:mm:ss");
                        dr["Longitude"] = Convert.ToDouble(lastRow["Longitude"]).ToString("0.00000");
                        dr["Latitude"] = Convert.ToDouble(lastRow["Latitude"]).ToString("0.00000");
                        // dr["address"] = lastRow["Course"];
                        string time = "";
                        if (ts.Days > 0)
                        {
                            time += ts.Days + "天";
                        }
                        if (ts.Hours > 0)
                        {
                            time += ts.Hours + "时";
                        }
                        if (ts.Minutes > 0)
                        {
                            time += ts.Minutes + "分";
                        }
                        dr["time"] = time; //lastRow["Course"];
                        dr["address"] = "";// MyTeam.GetAddressByLatlng(lastRow["Latitude"].ToString(), lastRow["Longitude"].ToString()); //lastRow["SerialNumber"];
                        dr["imei"] = imei;
                        eDt.Rows.Add(dr);
                        i++;
                    }
                }
                #endregion
            }
            return dt;
        }

        /// <summary>
        /// 查询历史轨迹 统计停留点
        /// </summary>
        /// <param name="UserID">用户ID（如果为0就按设备ID查询）</param>
        /// <param name="DeviceID">设备ID（如果为0就按用户ID查询）</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="FilterStopHour">停留大于多少，单位（小时）</param>
        /// <param name="FilterWireless">是否过滤无线  1：是，0：否</param>
        /// <param name="LastStop">是否只查询设备的最后停留点 1：是，0：否</param>
        /// <returns></returns>
        public static MG_BLL.ajaxResult GetStopDetail(string UserID, string DeviceID, string StartTime, string EndTime, string FilterStopHour, string FilterWireless, string LastStop)
        {
            MG_BLL.ajaxResult ar = new MG_BLL.ajaxResult();
            double filterSecond = 120; //多长时间算停留点 ，默认120秒
            #region 参数验证
            if (string.IsNullOrEmpty(UserID))
            {
                ar.Message = "请选择用户.";
                return ar; 
            }
            //if (string.IsNullOrEmpty(UserID) && string.IsNullOrEmpty(DeviceID))
            //{
            //    ar.Message = "请选择用户.";
            //    return ar;
            //}
            var st = Convert.ToDateTime(StartTime);
            var et = Convert.ToDateTime(EndTime);
            if ((et  - st).Days < 0)
            {
                ar.Message = "结束时间不能比开始时间小.";
                return ar;
            }
            if (!string.IsNullOrEmpty(DeviceID) && !DeviceID.Equals("0"))
            {
                UserID = "0";
                if ((et - st).TotalDays > 30)
                {
                    ar.Message = "一次最多只支持查询30天的数据.";
                    return ar;
                }
            }
            else
            {
                DeviceID = "0";
                if ((et - st).TotalDays != 0 && LastStop.Equals("0"))
                {
                    ar.Message = "按用户查找因数据量过大,目前只支持一次查询一天的数据.";
                    return ar;
                }
                if (string.IsNullOrEmpty(UserID) || UserID.Equals("0"))
                {
                    ar.Message = "请选择一个用户.";
                    return ar;
                }
            }
            if (!FilterWireless.Equals("0") && !FilterWireless.Equals("1"))
            {
                ar.Message = "参数错误.";
                return ar;
            }
            if (!LastStop.Equals("0") && !LastStop.Equals("1"))
            {
                ar.Message = "参数错误.";
                return ar;
            }
            if (!string.IsNullOrEmpty(FilterStopHour) && FilterStopHour != "0")
            {
                if (double.TryParse(FilterStopHour, out filterSecond))
                {
                    filterSecond = filterSecond > 0 ? filterSecond * 60 * 60 : filterSecond;
                }  
            }
            #endregion

            try
            {
                List<Dictionary<string, string>> resList = new List<Dictionary<string, string>>();
                MG_DAL.SQLServerOperating sqlHelper = new MG_DAL.SQLServerOperating();
                ///只查看最后停留点
                if (LastStop.Equals("1"))
                {
                    #region 查询最后停留点
                    string where = "where  d.Deleted = 0 and  datediff(ss, StopStartUtcDate, ServerUtcDate) >= @filterSecond";
                    if (FilterWireless.Equals("1"))  //过滤无线
                    {
                        where += @" and AccountID!=2 ";
                    }
                    string strSql = @"   with subqry(UserID,UserName) as
                     (
                        select UserID,UserName from Users where UserID = @UserID and Users.Deleted = 0 
                        union all 
                        select Users.UserID,Users.UserName from Users inner join subqry on Users.ParentID = subqry.UserID 
                     )
                   select UserName,d.DeviceID,d.DeviceName,d.SerialNumber,DATEADD(hh, 8, l.StopStartUtcDate) startTime, DATEADD(hh, 8, ServerUtcDate) endTime,s.UserID,l.OLat,l.OLng , datediff(MI, StopStartUtcDate, ServerUtcDate) time
                        , l.LastCommunication ,di.SortOrder
                       from Devices d inner join LKLocation l on l.deviceid = d.DeviceID inner join subqry s on s.UserID = d.UserID
                    inner join Dictionary di on di.DataValue=d.Model " + where + " Order by StopStartUtcDate desc"; 
                    DataTable dt = sqlHelper.Selects(strSql, new SqlParameter[] { new SqlParameter("UserID", UserID), new SqlParameter("filterSecond", filterSecond) });
                    //List<Dictionary<string, string>> listDic = new List<Dictionary<string, string>>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            dic[col.ColumnName] = row[col.ColumnName].ToString();
                        } 
                        var geo = new MG_BLL.Baidu();
                        MG_BLL.Gps gps = geo.Translate(dic["OLat"], dic["OLng"], false);
                        dic["OLng"] = gps.getWgLon().ToString("0.00000");
                        dic["OLat"] = gps.getWgLat().ToString("0.00000");
                        TimeSpan ts = (DateTime.Now - Convert.ToDateTime(dic["LastCommunication"]));
                        var offlineTime  = Convert.ToInt32(dic["SortOrder"]); //该型号多长时间算离线
                        if (ts.TotalMinutes > offlineTime)
                        { 
                            dic["Status"] = "离线-" +Utils.MinuteToHour(ts.TotalMinutes);
                        }
                        else
                        {
                            dic["Status"] = "在线";
                        }
                        dic.Remove("SortOrder");
                        dic.Remove("LastCommunication");
                        dic["time"] = Utils.MinuteToHour(Convert.ToDouble(dic["time"]));
                        dic["address"] = "";
                        resList.Add(dic);
                    } 
                    #endregion
                     
                }
                else
                {
                    #region 通过存储过程 查询历史记录
                     
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic["UserID"] = UserID;
                    dic["DeviceID"] = DeviceID;
                    dic["StartTime"] = st.ToString("yyyy-MM-dd 00:00:01");
                    dic["EndTime"] = et.ToString("yyyy-MM-dd 23:59:59");
                    dic["FilterWireless"] = FilterWireless;
                    dic["FilterSpeed"] = "10";
                    DataTable dt = sqlHelper.QueryByProc("SP_GetHistory", dic);

                    //Dictionary<string, List<Dictionary<string, string>>> list = new Dictionary<string, List<Dictionary<string, string>>>();
                    List<Dictionary<string, string>> listDic = new List<Dictionary<string, string>>();
                    foreach (DataRow row in dt.Rows)
                    {
                        dic = new Dictionary<string, string>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            dic[col.ColumnName] = row[col.ColumnName].ToString();
                        }
                        listDic.Add(dic);
                    }
                 
                    var listByDeviceID = listDic.GroupBy((l) => l["DeviceID"]).ToList();
                    for (int i = 0; i < listByDeviceID.Count; i++)
                    {
                        var t = listByDeviceID[i].OrderBy(o => o["DeviceTime"]).ToList();

                        for (int j = 1; j < t.Count; j++)
                        {
                            Dictionary<string, string> lastDic = t[j - 1];
                            Dictionary<string, string> currentDic = t[j];
                            DateTime start = Convert.ToDateTime(lastDic["DeviceTime"]);
                            DateTime end = Convert.ToDateTime(currentDic["DeviceTime"]);
                            TimeSpan ts = start.Subtract(end).Duration();
                            if (ts.TotalSeconds > filterSecond) //停留2分钟算作一个停留点
                            {
                                Dictionary<string, string> newDic = new Dictionary<string, string>();
                                newDic["startTime"] = start.ToString("yyyy-MM-dd HH:mm:ss");
                                newDic["endTime"] = end.ToString("yyyy-MM-dd HH:mm:ss");
                                MG_BLL.Geocoding geo = new MG_BLL.Baidu();
                                MG_BLL.Gps gps = geo.Translate(lastDic["OLat"], lastDic["OLng"], false);
                                newDic["OLng"] = gps.getWgLon().ToString("0.00000");
                                newDic["OLat"] = gps.getWgLat().ToString("0.00000");
                                string time = "";
                                if (ts.Days > 0)
                                    time += ts.Days + "天";
                                if (ts.Hours > 0)
                                    time += ts.Hours + "时";
                                if (ts.Minutes > 0)
                                    time += ts.Minutes + "分";
                                newDic["time"] = time;
                                newDic["address"] = "";
                                newDic["DeviceName"] = lastDic["DeviceName"];
                                newDic["SerialNumber"] = lastDic["SerialNumber"];
                                newDic["UserName"] = lastDic["UserName"];
                                resList.Add(newDic);
                                j++;
                            }
                        }
                    }
                    ar.Message = ""; 
                    #endregion
                }

               
                ar.Result = Dao.ToJson(resList); 
                ar.StatusCode = MG_BLL.statusCode.Code.success;
                ar.Message = "";
            }
            catch (Exception ex)
            {
                ar.Message = ex.Message;
            } 
            return ar;
        }

        /// <summary>
        /// 获取离线统计 所有的离线设备
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="Hour">过滤的小时（离线大于）</param>
        /// <returns></returns>

        public static MG_BLL.ajaxResult GetOnlineDeviceList(string UserID, string StartTime, string EndTime, string Hour)
        {
            MG_BLL.ajaxResult ar = new MG_BLL.ajaxResult();

            try
            {
                string where = " and datediff(mi,l.LastCommunication,getdate()) > di.SortOrder";
                Dictionary<string, string> dicPars = new Dictionary<string, string>();
                if (string.IsNullOrEmpty(UserID))
                {
                    ar.Message = "请选择一个用户.";
                    ar.StatusCode = MG_BLL.statusCode.Code.failure;
                    return ar;
                    //UserID = Utils.GetSession().UserID;
                }
                if (!string.IsNullOrEmpty(Hour))
                {
                    var parHour = 0.0;
                    double.TryParse(Hour,out parHour);
                    if (parHour > 0)
                    {
                        where = " and datediff(mi,l.LastCommunication,getdate()) > @Hour";
                        dicPars["Hour"] = (parHour * 60).ToString();
                    } 
                }
                if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
                {
                    where += " and l.LastCommunication >= @StartTime  and l.LastCommunication <= @EndTime";
                    dicPars["StartTime"] = StartTime;
                    dicPars["EndTime"] = EndTime;
                }
                dicPars["UserID"] = UserID;
                string strSql = @"with temp  as 
                            (
	                            select UserID,UserName from Users where UserID = @UserID and users.Deleted = 0
	                            union all
	                            select Users.UserID,Users.UserName from Users, temp where Users.ParentID = temp.UserID
                            )
                            select t.UserName,d.SerialNumber,d.DeviceName,di.DataText,d.CarNum,l.OLat,l.OLng,l.LastCommunication  from temp t inner join Devices d on d.UserID = t.UserID inner join LKLocation l on l.DeviceID = d.DeviceID
                            inner join Dictionary di on di.DataValue=d.Model
                            where 1=1   "+ where;
                var pars = new SqlParameter[dicPars.Count]; 
                int index = 0;
                foreach (KeyValuePair<string, string> item in dicPars)
                {
                    pars[index++] = new SqlParameter(item.Key, item.Value);
                } 
                MG_DAL.SQLServerOperating sqlHelper = new MG_DAL.SQLServerOperating();
                DataTable dt = sqlHelper.Selects(strSql, pars);
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                foreach (DataRow row in dt.Rows)
                {
                    dicPars = new Dictionary<string, string>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        dicPars[col.ColumnName] = row[col.ColumnName].ToString();
                    }
                    var geo = new MG_BLL.Baidu();
                    MG_BLL.Gps gps = geo.Translate(dicPars["OLat"], dicPars["OLng"], false);
                    dicPars["OLng"] = gps.getWgLon().ToString();
                    dicPars["OLat"] = gps.getWgLat().ToString();
                    TimeSpan ts = (DateTime.Now - Convert.ToDateTime(dicPars["LastCommunication"]));
                    string time = "";
                    if (ts.Days > 0)
                        time += ts.Days + "天";
                    if (ts.Hours > 0)
                        time += ts.Hours + "时";
                    if (ts.Minutes > 0)
                        time += ts.Minutes + "分";
                    dicPars["offlineTime"] = time;
                    list.Add(dicPars);
                }
                ar.Result = MG_BLL.Utils.ToJson(list);
                ar.StatusCode = MG_BLL.statusCode.Code.success;
            }
            catch (Exception ex)
            {
                ar.Message = ex.Message;
                ar.StatusCode = MG_BLL.statusCode.Code.failure;
            }
            return ar;
        }

        /// <summary>
        /// 报警总览
        /// </summary>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <param name="dName"></param>
        /// <returns></returns>
        public static DataTable GetExceptionAll(String st, String et, String dName)
        {
            string where = toDic(dName, "d.");
            if (Utils.GetSession("UserInfo").LoginType == "0")
                where += " and d.UserID=" + Utils.GetSession("UserInfo").UserID;
            else
                where += " and d.SerialNumber='" + Utils.GetSession("UserInfo").SerialNumber + "'";

            String strSql = @"select  d.SerialNumber,d.DeviceName,count(Message) c, NotificationType, '0' did,'0' duand,'0' zd,'0'sos 
                    	from ExceptionMessage ex inner join Devices d on d.DeviceID=ex.DeviceID where d.Deleted=0 " + where + " and NotificationType in(6,7,5) and ex.Created >='" + DateTime.Parse(st).ToString("yyyy-MM-dd 00:00:00") + "' and ex.Created<='" + DateTime.Parse(st).ToString("yyyy-MM-dd 23:59:59") + "'  group by NotificationType,d.SerialNumber,Message,d.DeviceName";
            DataTable dt = Dao.Selects(strSql);
            Hashtable ht = new Hashtable();
            Hashtable RemoveHt = new Hashtable();
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["DeviceName"].ToString().Trim() == "")
                    dr["DeviceName"] = dr["SerialNumber"];
                if (ht[dr["SerialNumber"].ToString()] != null)
                {
                    switch (dr["NotificationType"].ToString())
                    {
                        case "5":
                            dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString()].ToString())]["sos"] = Convert.ToInt32(dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString()].ToString())]["sos"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "6":
                            dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString()].ToString())]["duand"] = Convert.ToInt32(dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString()].ToString())]["duand"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "7":
                            dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString()].ToString())]["zd"] = Convert.ToInt32(dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString()].ToString())]["zd"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        default:
                            break;
                    }
                    RemoveHt[dr] = i;
                }
                else
                {
                    ht[dr["SerialNumber"].ToString()] = i;
                    switch (dr["NotificationType"].ToString())
                    {
                        case "5":
                            dt.Rows[i]["sos"] = Convert.ToInt32(dt.Rows[i]["sos"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "6":
                            dt.Rows[i]["duand"] = Convert.ToInt32(dt.Rows[i]["duand"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "7":
                            dt.Rows[i]["zd"] = Convert.ToInt32(dt.Rows[i]["zd"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        default:
                            break;
                    }
                    // newHt[i] = (dr);
                }
            }
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (RemoveHt[dr] != null)
                {
                    dt.Rows.Remove(dr);
                }
            }
            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                if (dt.Columns[i].ColumnName == "c" || dt.Columns[i].ColumnName == "NotificationType")
                {
                    dt.Columns.Remove(dt.Columns[i]);
                }
            }
            return dt;
        }
        /// <summary>
        /// 报警统计
        /// </summary>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <param name="dName"></param>
        /// <returns></returns>
        public static DataTable GetExceptionView(String st, String et, String dName)
        {
            string where = toDic(dName, "d.");
            if (Utils.GetSession("UserInfo").LoginType == "0")
                where += " and d.UserID=" + Utils.GetSession("UserInfo").UserID;
            else
                where += " and d.SerialNumber='" + Utils.GetSession("UserInfo").SerialNumber + "'";
            String strSql = @"select  d.SerialNumber,case when d.DeviceName='' then d.SerialNumber else d.DeviceName end DeviceName, convert(varchar(10), DATEADD(HH,8,ex.Created),120) date,COUNT(-1)c,ex.NotificationType , '0' did,'0' duand,'0' zd,'0'sos,'0'indzwl,'0'outdzwl,'0'wy from ExceptionMessage ex inner join Devices d on d.DeviceID=ex.DeviceID 
                        where  d.Deleted=0 " + where + " and NotificationType in(1,2,6,7,5,8) and ex.Created>='" + DateTime.Parse(st).ToString("yyyy-MM-dd 00:00:00") + "' and ex.Created <'" + DateTime.Parse(et).ToString("yyyy-MM-dd 23:59:59") + @"'
                        group by  d.SerialNumber,convert(varchar(10), DATEADD(HH,8,ex.Created),120) ,d.DeviceName,ex.NotificationType order by date";
            Hashtable ht = new Hashtable();
            Hashtable RemoveHt = new Hashtable();
            DataTable dt = Dao.Selects(strSql);
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["DeviceName"].ToString().Trim() == "")
                    dr["DeviceName"] = dr["SerialNumber"];
                if (ht[dr["SerialNumber"].ToString() + dr["date"].ToString()] != null)
                {
                    switch (dr["NotificationType"].ToString())
                    {
                        case "5": //SOS报警
                            dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["sos"] = Convert.ToInt32(dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["sos"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "6": //断电报警
                            dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["duand"] = Convert.ToInt32(dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["duand"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "7": //震动报警
                            dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["zd"] = Convert.ToInt32(dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["zd"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "8": //位移报警
                            dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["wy"] = Convert.ToInt32(dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["wy"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "1": //进围栏报警
                            dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["indzwl"] = Convert.ToInt32(dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["indzwl"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "2"://出围栏报警
                            dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["outdzwl"] = Convert.ToInt32(dt.Rows[Convert.ToInt32(ht[dr["SerialNumber"].ToString() + dr["date"].ToString()].ToString())]["outdzwl"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                    }
                    RemoveHt[dr] = i;
                }
                else
                {
                    ht[dr["SerialNumber"].ToString() + dr["date"].ToString()] = i;
                    switch (dr["NotificationType"].ToString())
                    {
                        case "5":
                            dt.Rows[i]["sos"] = Convert.ToInt32(dt.Rows[i]["sos"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "6":
                            dt.Rows[i]["duand"] = Convert.ToInt32(dt.Rows[i]["duand"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "7":
                            dt.Rows[i]["zd"] = Convert.ToInt32(dt.Rows[i]["zd"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "8": //位移报警
                            dt.Rows[i]["wy"] = Convert.ToInt32(dt.Rows[i]["wy"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "1": //进围栏报警
                            dt.Rows[i]["indzwl"] = Convert.ToInt32(dt.Rows[i]["indzwl"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                        case "2"://出围栏报警
                            dt.Rows[i]["outdzwl"] = Convert.ToInt32(dt.Rows[i]["outdzwl"]) + Convert.ToInt32(dr["c"].ToString());
                            break;
                    }
                    // newHt[i] = (dr);
                }
            }
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (RemoveHt[dr] != null)
                {
                    dt.Rows.Remove(dr);
                }
            }
            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                if (dt.Columns[i].ColumnName == "c" || dt.Columns[i].ColumnName == "NotificationType")
                {
                    dt.Columns.Remove(dt.Columns[i]);
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取所有的报警类型
        /// </summary>
        /// <returns></returns>
        public static DataTable GetExceptionType()
        {
            String strSql = "select Message,NotificationType from ExceptionMessage group by Message,NotificationType";
            return Dao.Selects(strSql);
        }

        /// <summary>
        /// 报警详单
        /// </summary>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <param name="type"></param>
        /// <param name="dName"></param>
        /// <returns></returns>
        public static DataTable GetExctptionDetail(string st, string et, string type, string dName)
        {
            String where = "";
            if (type.Trim() != "" && Convert.ToInt32(type) > 0)
                where = " and ex.NotificationType = " + type;
            where += toDic(dName, "d.");
            if (Utils.GetSession("UserInfo").LoginType == "0")
                where += " and d.UserID=" + Utils.GetSession("UserInfo").UserID;
            else
                where += " and d.SerialNumber='" + Utils.GetSession("UserInfo").SerialNumber + "'";
            String strSql = string.Format(@"select ROW_NUMBER() OVER (ORDER BY ex.Created  ) num,case when d.DeviceName='' then d.SerialNumber else d.DeviceName end DeviceName,ex.Message,d.SerialNumber, DATEADD(HH,8, ex.Created) edate,DATEADD(HH,8,ex.DeviceUTCTime) ddate,ex.BaiduLat,ex.BaiduLng ,'解析' Address from ExceptionMessage ex inner join Devices d on d.DeviceID=ex.DeviceID
                                            where d.Deleted=0 and dateadd(HH,8, ex.Created)>='{0}' and dateadd(HH,8, ex.Created)<='{1}' " + where + " order by edate", DateTime.Parse(st).ToString("yyyy-MM-dd 00:00:00"), DateTime.Parse(et).ToString("yyyy-MM-dd 23:59:59"));
            return Dao.Selects(strSql);
        }

        public static String toDic(String SerialNumber, String table)
        {
            String where = "";
            if (SerialNumber != null && SerialNumber != "" && SerialNumber != "null")
            {
                Dictionary<string, object> dic = Dao.ToDict(SerialNumber);
                if (dic["dn"] != null && dic["dn"].ToString() != "")
                {
                    where += " and " + table + "SerialNumber in (";
                    if (dic["dn"] is String)
                    {
                        where += "'" + dic["dn"] + "')";
                    }
                    else if (dic["dn"] is ArrayList)
                    {
                        ArrayList arr = (ArrayList)dic["dn"];
                        for (int i = 0; i < arr.Count; i++)
                        {
                            where += "'" + arr[i] + "',";
                        }
                        where = where.Substring(0, where.Length - 1) + ")";
                    }
                }
            }
            return where;
        }

        /// <summary>
        /// 电子围栏统计
        /// </summary>
        /// <returns></returns>
        public static DataTable GetFencesView(String st, String et, String SerialNumber)
        {
            string where = toDic(SerialNumber, "d.");
            if (Utils.GetSession("UserInfo").LoginType == "0")
                where += " and d.UserID=" + Utils.GetSession("UserInfo").UserID;
            else
                where += " and d.SerialNumber='" + Utils.GetSession("UserInfo").SerialNumber + "'";
            //  strSql = @"select SerialNumber,(select case when  DeviceName='' then  SerialNumber else DeviceName end DeviceName from Devices where DeviceID=ex.DeviceID) DeviceName, DATEADD(HH,8,ex.Created) st,''et,Note,NotificationType,BaiduLat,BaiduLng,'解析' Address 
            //            from ExceptionMessage ex where NotificationType in(1,2) and GeoFenceID in(select GeoFenceID from GeoFence where DeviceID in(select DeviceID from Devices d where Created>='" + st.Trim() + " 00:00:00' and Created <= '" + et.Trim() + " 23:59:59' " + where + ")) order by Note ,Created desc ";
            string strSql = "";
            strSql += string.Format(@"select SerialNumber,(select case when  DeviceName='' then  SerialNumber else DeviceName end DeviceName from Devices
                                     where DeviceID=ex.DeviceID) DeviceName, DATEADD(HH,8,ex.Created) st,''et,Note,NotificationType,BaiduLat,BaiduLng,'解析' Address 
                                    from ExceptionMessage ex where NotificationType in(1,2) and ex.Created>='{0}' and ex.Created <= '{1}' {2} 
                                    order by Note ,Created desc ", st.Trim(), et.Trim(), where);
            strSql = string.Format(@" select d.SerialNumber,d.DeviceName,
                                    DATEADD(HH,8,ex.Created) st,''et,Note,NotificationType,BaiduLat,BaiduLng,'解析' Address 
                                    from ExceptionMessage ex inner join Devices d on d.DeviceID=ex.DeviceID where NotificationType in(1,2) and ex.Created>='{0}' and ex.Created <= '{1}'
                                   {2} order by Note ,ex.Created desc ", st.Trim(), et.Trim(), where);
            DataTable dt = Dao.Selects(strSql);
            Hashtable ht = new Hashtable();
            Hashtable RemoveHt = new Hashtable();
            int tempI = 0;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                switch (dr["NotificationType"].ToString())
                {
                    case "1":
                        ht[dr["Note"].ToString() + i] = i;
                        tempI = i;
                        break;
                    case "2":
                        dt.Rows[tempI]["et"] = dr["st"];
                        RemoveHt[dr] = i;
                        break;
                }
            }
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (RemoveHt[dr] != null)
                {
                    dt.Rows.Remove(dr);
                }
            }
            DataColumn dc = new DataColumn();
            dc.ColumnName = "TimediffMinute";

            dt.Columns.Add(dc);
            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                if (dt.Columns[i].ColumnName == "NotificationType")
                {
                    dt.Columns.Remove(dt.Columns[i]);
                }
                if (dt.Columns[i].ColumnName == "TimediffMinute")
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["et"].ToString() != "" && dt.Rows[j]["st"].ToString() != "")
                        {
                            double mi = (DateTime.Parse(dt.Rows[j]["et"].ToString()) - DateTime.Parse(dt.Rows[j]["st"].ToString())).TotalMinutes;
                            dt.Rows[j][dt.Columns[i]] = mi;
                        }
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// 获取当前用户下的设备名称和IMEI号
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCurrentUserDeviceName(string UserID = null)
        {
            String strSql = "", strWhere = null;
            if (UserID == null)
            {
                UserID = Utils.GetSession("UserInfo").UserID;
            }
            if (Utils.GetSession("UserInfo").LoginType == "0")
            {
                strWhere += " and d.UserID= " + UserID;
            }
            else
            {
                strWhere += " and d.SerialNumber= '" + Utils.GetSession("UserInfo").SerialNumber + "'";
            }
            strSql = "select distinct case DeviceName when '' then d.SerialNumber else DeviceName end DeviceName ,d.SerialNumber,d.DeviceID,d.SpeedLimit from Devices d inner join LKLocation l on l.deviceid=d.deviceid where d.Deleted = 0 " + strWhere + " order by d.SerialNumber desc";
            return Dao.Selects(strSql);
        }

        /// <summary>
        /// 查询设备列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static StringBuilder GetDevicesList(String userid)
        {
            String strSql = "select DeviceID,SerialNumber,DeviceName,CellPhone,di.DataText,d.Created,d.HireStartDate,d.HireExpireDate from Devices d inner join Dictionary di on d.Model=di.DataValue  where  d.UserID=" + userid + " and Deleted=0 order by d.Created";
            DataTable dt = Dao.Selects(strSql);
            StringBuilder StrJson = new StringBuilder();
            StrJson.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                StrJson.Append("{\"did\":\"" + dr["DeviceID"] + "\",\"DeviceName\":\"" + (dr["DeviceName"].ToString().Trim() == "" ? dr["SerialNumber"] : dr["DeviceName"]) + "\",\"IMEI\":\"" + dr["SerialNumber"] + "\",\"create\":\"" + Convert.ToDateTime(dr["Created"]).AddHours(8).ToString("yyyy-mm-dd") + "\",\"jh\":\"" + dr["HireStartDate"] + "\",\"dq\":\"" + dr["HireExpireDate"] + "\",\"phone\":\"" + dr["CellPhone"] + "\",\"model\":\"" + dr["DataText"] + "\"}");
                if (i < dt.Rows.Count - 1)
                    StrJson.Append(",");
            }
            StrJson.Append("]");
            return StrJson;
        }
        /// <summary>
        /// 根据用户id查询设备信息，返回DataTabel  '"+imeis[i]+"'
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static DataTable GetDevicesListReturnDataTable(String userid = null, string imei = null)
        {
            try
            {
                StringBuilder where = new StringBuilder();
                SqlParameter[] paras = null;
                if (imei != null)
                {
                    // JsonSerializer js = new JsonSerializer();
                    // JsonReader jr = new JsonTextReader(new StringReader(imei));
                    // Dictionary<string, object> dic = js.Deserialize<Dictionary<string, object>>(jr);
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    Dictionary<string, object> dic = jss.Deserialize<Dictionary<string, object>>(imei);
                    string type = dic["type"].ToString();
                    imei = dic["imeis"].ToString();

                    string[] imeis = imei.TrimEnd().Split('\n');
                    paras = new SqlParameter[imeis.Length];
                    StringBuilder sb = new StringBuilder();
                    string keyName = "";
                    if (type == "1")
                    {
                        keyName = "SerialNumber";
                    }
                    else if (type == "2")
                    {
                        keyName = "PhoneNum";
                    }
                    else
                    {
                        keyName = "DeviceName";
                    }
                    where.Append(" and d." + keyName + " in(");
                    for (int i = 0; i < imeis.Length; i++)
                    {
                        where.Append("@" + keyName + i);
                        if (i < imeis.Length - 1)
                        {
                            where.Append(",");
                        }
                        SqlParameter sp = new SqlParameter("@" + keyName + i, imeis[i]);
                        paras[i] = sp;
                    }
                    where.Append(" )");
                }
                if (userid == null)
                {
                    Hashtable ht = GetDeviceNumber(Utils.GetSession("UserInfo").UserID);
                    userid = ht["userids"].ToString();
                }
                String strSql = string.Format(@" select d.DeviceID,d.SerialNumber,DeviceName, PhoneNum,d.CellPhone,di.DataText, d.Created,l.LastCommunication,d.HireStartDate ,case when d.ActiveDate = '1900-01-01 00:00:00.000' then '未激活' else CONVERT(varchar, d.ActiveDate,120) end ActiveDate,case when d.HireExpireDate = '1900-01-01 00:00:00.000' then '未激活' else CONVERT(varchar, d.HireExpireDate,120) end HireExpireDate,u.UserID,u.UserName,di.SortOrder as offLineMi
                                             from Devices d inner join Dictionary di on d.Model=di.DataValue inner join Users u on d.UserID=u.UserID
                                             left join lklocation l on l.deviceid=d.deviceid
                                            where d.UserID in ({0}) and d.Deleted=0 and u.deleted=0 " + where.ToString() + " order by d.Created", userid);

                if (paras != null)
                    return Dao.Selects(strSql, paras);
                else
                    return Dao.Selects(strSql);

            }
            catch (Exception ex)
            {
                Utils.log("GetDevicesListReturnDataTable :" + ex.Message);
                return new DataTable();
            }
        }

        public static DataRow GetUserDeviceInfo(String UserID)
        {
            string strSql = string.Format(@"with subqry(UserID,ParentID) as (
                                    select UserID,ParentID from Users where UserID =  " + UserID + @" union all select Users.UserID,Users.ParentID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted!=1)  select UserID,LoginName,UserType,case when UserType=2 then '经销商' when UserType=1 then '用户' end UserTypeName,UserName,FirstName,CellPhone,Address1 , (select count(*) from devices where userid={0} and deleted=0) InStock,  
                                            (select COunt(*) from devices where  deleted=0 and userid in(select userid from subqry)) purchase from users where deleted=0 and  userid={0}", UserID);
            return Dao.Select(strSql);
        }

        /// <summary>
        /// 查询设备的详细信息
        /// </summary>
        /// <param name="DeviceID"></param>
        /// <returns></returns>
        public static DataTable GetDevicesDetailByDeviceID(String DeviceID)
        {
            String strSql = "select DeviceID,SerialNumber,DeviceName,CellPhone,di.DataText, DATEADD(HH,8,d.Created) Created,d.HireStartDate,d.HireExpireDate,d.SpeedLimit,d.Icon,d.CarUserName,d.CarNum,d.Description,d.ServerID from Devices d inner join Dictionary di on d.Model=di.DataValue  where d.DeviceID=" + DeviceID + " and Deleted=0 order by d.Created";
            return Dao.Selects(strSql);
        }

        /// <summary>
        /// 根据用户id查询用户下面的用户和经销商
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static DataTable GetUserInfoChild(String userid)
        {
            String strSql = "select u.UserID,u.UserName,u.LoginName,UserType,u.FirstName,CellPhone,(select COUNT(1) from Devices d where d.UserID=u.UserID and d.Deleted=0)DevicesCount from Users u where u.Deleted =0 and ParentID=" + userid + " order by UserName";
            return Dao.Selects(strSql);
        }

        public static DataTable GetUserInfoByUserID(String UserID)
        {
            String strSql = "select UserName,LoginName,FirstName,Address1,PrimaryEmail,CellPhone,UserType from Users where UserID= " + UserID;
            return Dao.Selects(strSql);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static String UpdateUserInfo(String data)
        {
            Dictionary<String, Object> dic = Dao.ToDict(data);
            //String strSql = "update Users set Address1='" + dic["address"] + "',FirstName='" + dic["fn"] + "',CellPhone='" + dic["phone"] + "',PrimaryEmail='" + dic["eMail"] + "' where UserID= " + Utils.GetSession("UserInfo").UserID;
            int status = Dao.Update(dic, "Users", "UserID");
            // int status = Dao.ExecutionSQL(strSql);
            if (status > 0)
                return "{\"success\":true}";
            else
                return "{\"success\":false}";
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="type"></param>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <param name="dn"></param>
        /// <param name="emtype"></param>
        /// <param name="yh"></param>
        /// <param name="userid"></param>
        /// <returns></returns> 
        public static String ImportExcel(HttpContext context)
        {
            try
            {
                string type = context.Request.Form["type"], st = context.Request.Form["st"], et = context.Request.Form["et"], dn = context.Request.Form["dn"],
                    emtype = context.Request.Form["emtype"], yh = context.Request.Form["yh"], userid = context.Request.Form["userid"];
                String path = System.Web.HttpContext.Current.Server.MapPath(".") + "\\Log";
                String FileName = "";
                String title = "";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string resMessage = "";
                switch (type)
                {
                    case "AlarmMessage":
                        FileName = "报警消息(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @" <h4>时间：一个月内的报警消息 </h4>
                        <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称 </td><td> IMEI号 </td><td> 所属用户  </td><td>  报警类型 </td><td> 报警时间 </td><td> 定位时间 </td><td> 设备型号 </td><td> 状态 </td></tr>";
                        Utils.DataSetToExcel(getAlarmList(userid,type:"excel"), path += "\\" + FileName, title);
                        break;
                    case "ExceptionView":
                        FileName = "报警总览(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @" <h4>时间：" + st + "-" + et + @"</h4>
                          <tr style='border:1px solid #000000'><td>IMEI号</td><td> 设备名称 </td><td> 低电报警 </td><td> 断电报警  </td><td>  震动报警 </td><td> 求救报警 </td> </tr>";
                        Utils.DataSetToExcel(GetExceptionAll(st, et, dn), path += "\\" + FileName, title);
                        break;
                    case "ExceptionCount":
                        FileName = "报警统计(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @" <h4>时间：" + st + "-" + et + @"</h4>
                         <tr style='border:1px solid #000000'><td>IMEI号</td><td> 设备名称 </td><td> 时间 </td><td>低电报警</td><td>断电报警</td><td>震动报警</td><td>求救报警</td><td>进电子围栏</td><td>出电子围栏</td><td>位移报警</td></tr>";
                        Utils.DataSetToExcel(GetExceptionView(st, et, dn), path += "\\" + FileName, title);
                        break;
                    case "ExceptionDetail":
                        FileName = "报警详单(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @" <h4>时间：" + st + "-" + et + @"</h4>
                         <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称 </td><td>报警类型</td><td>IMEI号</td><td>报警时间</td><td>定为时间</td><td>经度</td><td>纬度</td><td>地址</td> </tr>";
                        Utils.DataSetToExcel(GetExctptionDetail(st, et, emtype, dn), path += "\\" + FileName, title);
                        break;
                    case "GeoFencesView":
                        FileName = "电子围栏统计(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @" <h4>时间：" + st + "-" + et + @"</h4>
                         <tr style='border:1px solid #000000'><td>IMEI号</td><td> 设备名称 </td><td>进入时间</td><td>离开时间</td><td>电子围栏</td><td>经度</td><td>纬度</td><td>地址</td><td>持续时间</td> </tr>";
                        Utils.DataSetToExcel(GetFencesView(st, et, dn), path += "\\" + FileName, title);
                        break;
                    case "RemainView":
                        FileName = "运行总览(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @" <h4>时间：" + st + "-" + et + @"</h4>
                         <tr style='border:1px solid #000000'> <td> 设备名称 </td><td> 时间 </td><td> 里程(公里)  </td><td>  报警 </td><td> 超速 </td><td> 停留 </td></tr>";
                        Utils.DataSetToExcel(GetRemainView(dn, st, et), path += "\\" + FileName, title);
                        break;
                    case "StopDetail":
                        #region 停留详单
                        var pars = context.Request.Form["otherData"];
                        var dicPars = MG_BLL.Utils.ToDictionary(pars);
                        MG_BLL.ajaxResult ar = GetStopDetail(dicPars["userid"], dicPars["deviceid"], dicPars["starttime"], dicPars["endtime"], dicPars["stopday"], dicPars["wireless"], dicPars["laststop"]);
                        if (ar.StatusCode == MG_BLL.statusCode.Code.success)
                        {
                            List<Dictionary<string,string>> list = MG_BLL.Utils.ToList(ar.Result);
                            FileName = "停留统计(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                            title = @" <h4>时间：" + dicPars["starttime"] + "-" + dicPars["endtime"] + @"</h4>
                             <tr style='border:1px solid #000000'><td>序号</td><td>用户</td><td>IMEI号</td><td> 设备名称 </td><td> 开始时间  </td><td> 结束时间  </td><td>  经度 </td><td> 纬度 </td><td> 停留时间 </td> <td> 地址 </td></tr>";
                            int index = 1;
                            List<List<string>> listArray = new List<List<string>>();
                            foreach (Dictionary<string,string> item in list)
                            {
                                List<string> temp = new List<string>();
                                temp.Add( index++.ToString());
                                temp.Add(item["UserName"]);
                                temp.Add(item["SerialNumber"]);
                                temp.Add(item["DeviceName"]);
                                temp.Add(item["startTime"]);
                                temp.Add(item["endTime"]);
                                temp.Add(item["OLng"]);
                                temp.Add(item["OLat"]);
                                temp.Add(item["time"]);
                                temp.Add(GetAddressByLatlng(item["OLat"], item["OLng"]));
                                listArray.Add(temp);
                            }
                            Utils.ListToExcel(listArray, path += "\\" + FileName, title);
                        }
                       
                        #endregion
                        break;
                    case "OnLineList":
                        #region 离线统计 
                        pars = context.Request.Form["otherData"];
                        dicPars = MG_BLL.Utils.ToDictionary(pars);
                        ar = GetOnlineDeviceList(dicPars["userid"], dicPars["starttime"], dicPars["endtime"], dicPars["hour"]);
                        if (ar.StatusCode == MG_BLL.statusCode.Code.success)
                        {
                            List<Dictionary<string, string>> list = MG_BLL.Utils.ToList(ar.Result);
                            FileName = "离线统计(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                            title = @" <h4>时间：" + dicPars["starttime"] + "-" + dicPars["endtime"] + @"</h4>
                             <tr style='border:1px solid #000000'><td>序号</td><td>用户</td><td>IMEI号</td><td> 设备名称 </td><td> 车牌号  </td><td> 型号  </td><td> 离线时长  </td><td>  经度 </td><td> 纬度 </td> <td> 最后地址 </td></tr>";
                            int index = 1;
                            List<List<string>> listArray = new List<List<string>>();
                            foreach (Dictionary<string, string> item in list)
                            {
                                List<string> temp = new List<string>();
                                temp.Add(index++.ToString());
                                temp.Add(item["UserName"]);
                                temp.Add(item["SerialNumber"]);
                                temp.Add(item["DeviceName"]);
                                temp.Add(item["CarNum"]);
                                temp.Add(item["DataText"]);
                                temp.Add(item["offlineTime"]);
                                temp.Add(item["OLng"]);
                                temp.Add(item["OLat"]); 
                                temp.Add(GetAddressByLatlng(item["OLat"], item["OLng"]));
                                listArray.Add(temp);
                            }
                            Utils.ListToExcel(listArray, path += "\\" + FileName, title);
                        } 
                        #endregion
                        break;
                    case "Mileage":
                        #region 里程统计
                        string where = toDic(dn, "");
                        String strSql = "select DeviceID,DeviceName,SpeedLimit from Devices d where d.Deleted=0 " + where;
                        DataRow row = Dao.Select(strSql);
                        string DeviceID = row[0].ToString();
                        string DeviceName = row[1].ToString();
                        double SpeedLimit = Convert.ToDouble(row[2]);
                        int rowIndex = 1;
                        Dictionary<string, object> dic = Dao.ToDict(context.Request.Form["otherData"].Replace("/", "").Replace("-", ""));
                        FileName = "里程统计(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @" <h4>时间：" + st + "-" + et + "    百公里油耗：" + yh + @"</h4>
                         <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称 </td> <td> 时间 </td><td> 里程(公里)  </td><td>  报警 </td><td> 超速 </td><td> 停留 </td><td> 油耗(升) </td></tr>";
                        DataTable excelTable = new DataTable();
                        excelTable.Columns.Add("rowid"); excelTable.Columns.Add("DeviceName"); excelTable.Columns.Add("date"); excelTable.Columns.Add("km"); excelTable.Columns.Add("msg");
                        excelTable.Columns.Add("speedLimit"); excelTable.Columns.Add("stop"); excelTable.Columns.Add("yh");
                        DataTable dt = getPlayBack(DeviceID, st + " 00:00:00", et + " 23:59:59", " and Speed>=10");
                        string tempDate = "";
                        int stopCount = 0, speedLimitCount = 0;
                        Hashtable hTable = Utils.dsTransToHashtable(GetExceptionMessageCount(DeviceID, st, et));
                        int len = dt.Rows.Count - 1;
                        for (int i = 0; i < len; i++)
                        {
                            row = dt.Rows[i];
                            DateTime nextDate = Convert.ToDateTime(dt.Rows[i + 1]["DeviceUTCTime"].ToString());
                            DateTime currentDate = Convert.ToDateTime(row["DeviceUTCTime"].ToString());
                            if ((nextDate - currentDate).TotalSeconds > 600)
                            {
                                stopCount++;
                            }
                            if (SpeedLimit != 0 && Convert.ToDouble(row["Speed"]) > SpeedLimit)
                            {
                                speedLimitCount++;
                            }
                            if ((tempDate != "" && tempDate != currentDate.ToString("yyyy-MM-dd")) || i == len - 1) {
                                DataRow newRow = excelTable.NewRow();
                                double km = 0;
                                if (dic.ContainsKey(tempDate.Replace("/", "").Replace("-", "")))
                                {
                                    km = Convert.ToDouble(dic[tempDate.Replace("/", "").Replace("-", "")]);
                                }
                                newRow["rowid"] = rowIndex++; newRow["DeviceName"] = DeviceName; newRow["date"] = tempDate; newRow["km"] = km; newRow["msg"] = hTable.ContainsKey(tempDate) ? hTable[tempDate] : 0;
                                newRow["speedLimit"] = speedLimitCount; newRow["stop"] = stopCount; newRow["yh"] = ((Convert.ToInt32(yh) / 100.0) * km).ToString("0.00");
                                excelTable.Rows.Add(newRow);
                                stopCount = 0;
                                speedLimitCount = 0;
                            }
                            tempDate = currentDate.ToString("yyyy-MM-dd");
                        }
                        Utils.DataSetToExcel(excelTable, path += "\\" + FileName, title);
                        #endregion
                        break;
                    case "alldevices":
                        #region 设备列表 
                        FileName = "设备列表(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        int day = Convert.ToInt32(context.Request.Form["day"]);
                        strSql = "";
                        if (day > 0) {  //多少天内过期的设备
                            title = @" <h4>" + day + @"天内过期设备</h4>
                              <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称 </td><td> IMEI号 </td><td> 车牌号  </td><td> 型号  </td><td>设备号码</td><td>联系号码</td><td> 所属用户 </td><td> 激活时间 </td><td> 到期时间 </td> <td> 离线时间 </td><td> 状态 </td> </tr>";
                            strSql = @"with subqry(UserID,ParentID) as (
                                    select UserID,ParentID from Users where UserID =  " + userid + @" union all select Users.UserID,Users.ParentID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted!=1) select ROW_NUMBER() over(order by d.DeviceID asc)rowid, case when DeviceName='' then d.SerialNumber else DeviceName end DeviceName,d.SerialNumber,CarNum,di.DataText , PhoneNum,d.CellPhone,UserName,case when ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),ActiveDate,120) end as ActiveDate
                                 ,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate,l.LastCommunication,''DeviceStatus from Devices d inner join Users u on d.UserID=u.UserID inner join Dictionary di on di.DataValue=d.Model inner join lklocation l on l.deviceid=d.deviceid
                          where DATEDIFF(DAY,GETDATE(), HireExpireDate) < " + day + " and DATEDIFF(DAY,GETDATE(), HireExpireDate)>0 and d.Deleted=0 and d.userid in(select UserID from subqry) ";
                        }
                        else if (day == 0) {
                            title = @" <h4>全部设备</h4>
                               <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称</td><td> IMEI号 </td><td> 车牌号  </td><td> 型号  </td><td>设备号码</td><td>联系号码</td><td> 所属用户 </td><td> 激活时间 </td><td> 到期时间 </td> </tr>";
                            strSql = @"with subqry(UserID,ParentID) as (
                                    select UserID,ParentID from Users where UserID =  " + userid + @" union all select Users.UserID,Users.ParentID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted!=1) select ROW_NUMBER() over(order by DeviceID asc) rowid, case when DeviceName='' then SerialNumber else DeviceName end DeviceName,SerialNumber,CarNum,di.DataText , PhoneNum,d.CellPhone,UserName,case when ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),ActiveDate,120) end as ActiveDate
                                 ,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate from Devices d inner join Users u on d.UserID=u.UserID inner join Dictionary di on di.DataValue=d.Model
                                  where d.userid in(select UserID from subqry) and d.Deleted=0  ";
                        }
                        Utils.DataSetToExcel(Dao.Selects(strSql), path += "\\" + FileName, title);
                        #endregion
                        break;
                    case "overdue":
                        #region 导出过期设备数
                        FileName = "已过期设备数(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @"<h4>已过期设备数</h4>           
                      <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称</td><td> IMEI号 </td><td> 设备号码 </td><td> 联系号码 </td><td> 所属用户  </td><td> 联系电话  </td><td>创建时间</td><td>激活时间</td><td>到期时间</td><td> 离线时间 </td> </tr>";
                        strSql = @"with subqry(UserID,ParentID) as (
                                    select UserID,ParentID from Users where UserID =  " + userid + @" union all select Users.UserID,Users.ParentID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted!=1) select ROW_NUMBER() over(order by d.DeviceID asc)rowid,DeviceName,d.SerialNumber,PhoneNum,d.CellPhone,u.UserName,u.CellPhone as UserCellPhone,d.Created
                            ,case when d.ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),d.ActiveDate,120) end as ActiveDate
                            ,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate 
                             ,l.LastCommunication from Devices d inner join Users u on u.UserID=d.UserID inner join Dictionary di on di.DataValue=d.Model inner join LKLocation l on d.deviceid=l.DeviceID
                             where DATEADD(HH,8, HireExpireDate) < GETDATE() and d.Deleted=0 and HireExpireDate != '1900-01-01 00:00:00.000' and d.UserID in (select UserID from subqry)";
                        Utils.DataSetToExcel(Dao.Selects(strSql), path += "\\" + FileName, title);
                        #endregion
                        break;
                    case "use":
                        #region 在线使用设备数
                        FileName = "使用设备数(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @"<h4>已过期设备数</h4>     
                            <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称</td><td> IMEI号 </td><td> 设备号码 </td><td> 联系号码 </td><td> 所属用户  </td><td> 联系电话  </td><td>创建时间</td><td>激活时间</td><td>到期时间</td><td> 离线时间 </td> </tr>";
                        strSql = @"with subqry(UserID,ParentID) as (
                                    select UserID,ParentID from Users where UserID =  " + userid + @" union all select Users.UserID,Users.ParentID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted!=1) select ROW_NUMBER() over(order by d.DeviceID asc)rowid, DeviceName, d.SerialNumber,PhoneNum, d.CellPhone, u.UserName, u.CellPhone as UserCellPhone,d.Created
                            ,case when d.ActiveDate = '1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19), d.ActiveDate, 120) end as ActiveDate
                            ,case when HireExpireDate = '1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19), HireExpireDate, 120) end HireExpireDate
                                 , l.LastCommunication from Devices d inner join Users u on u.UserID = d.UserID inner join Dictionary di on di.DataValue = d.Model inner join LKLocation l on d.deviceid = l.DeviceID
                             where d.Deleted=0 and HireStartDate != '1900-01-01 00:00:00.000' and d.UserID in (select UserID from subqry)";
                        Utils.DataSetToExcel(Dao.Selects(strSql), path += "\\" + FileName, title);
                        #endregion
                        break;
                    case "offlinedevices":
                        #region 导出离线设备数
                        FileName = "离线设备数(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @"<h4>当前离线设备数</h4>
                        <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称</td><td> IMEI号 </td><td> 型号 </td><td> 设备号码 </td><td> 联系号码 </td><td> 所属用户  </td><td> 联系电话  </td><td>创建时间</td><td>激活时间</td><td>到期时间</td><td> 离线时间 </td> </tr>";
                        strSql = @"with subqry(UserID,ParentID) as (
                                    select UserID,ParentID from Users where UserID =  " + userid + @" union all select Users.UserID,Users.ParentID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted!=1) select ROW_NUMBER() over(order by d.DeviceID asc)rowid,DeviceName,d.SerialNumber,di.DataText,PhoneNum,d.CellPhone,u.UserName,u.CellPhone as UserCellPhone,d.Created
                            ,case when d.ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),d.ActiveDate,120) end as ActiveDate
                            ,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate 
                             ,l.LastCommunication from Devices d inner join Users u on u.UserID=d.UserID inner join Dictionary di on di.DataValue=d.Model inner join LKLocation l on d.deviceid=l.DeviceID                         
                        where d.UserID in(select UserID from subqry)and d.Deleted = 0 and datediff(MI, ISNULL(LastCommunication,'1900-01-01 00:00:00.000'),GETDATE()) >= di.SortOrder ";
                        Utils.DataSetToExcel(Dao.Selects(strSql), path += "\\" + FileName, title);
                        #endregion
                        break;
                    case "overtime":
                        #region 导出超过7天设备数
                        FileName = "超过7天设备数(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @"<h4>超过7天设备数</h4>
                        <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称</td><td> IMEI号 </td><td> 设备号码 </td><td> 联系号码 </td><td> 所属用户  </td><td> 联系电话  </td><td>创建时间</td><td>激活时间</td><td>到期时间</td><td> 离线时间 </td> </tr>";
                        strSql = @"with subqry(UserID,ParentID) as (
                                    select UserID,ParentID from Users where UserID =  " + userid + @" union all select Users.UserID,Users.ParentID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted!=1   ) select ROW_NUMBER() over(order by d.DeviceID asc)rowid,DeviceName,d.SerialNumber,PhoneNum,d.CellPhone,u.UserName,u.CellPhone as UserCellPhone,d.Created
                            ,case when d.ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),d.ActiveDate,120) end as ActiveDate
                            ,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate 
                            ,l.LastCommunication from Devices d inner join Users u on u.UserID=d.UserID inner join Dictionary di on di.DataValue=d.Model inner join LKLocation l on d.deviceid=l.DeviceID 
                            where d.UserID in(select UserID from subqry) and d.Deleted=0 and datediff(DAY,  HireStartDate , GETDATE()) >= 7  and ActiveDate > '1900-01-01 00:00:00.000' ";
                        Utils.DataSetToExcel(Dao.Selects(strSql), path += "\\" + FileName, title);
                        #endregion
                        break;
                    case "onlinedevices":
                        #region 导出在线设备数
                        FileName = "导出在线设备数(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @"<h4>在线设备数</h4>
                        <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称</td><td> IMEI号 </td><td> 设备号码 </td><td> 联系号码 </td><td> 所属用户  </td><td> 联系电话  </td><td>创建时间</td><td>激活时间</td><td>到期时间</td><td> 离线时间 </td> </tr>";
                        strSql = @" with subqry(UserID,ParentID) as (
                                    select UserID,ParentID from Users where UserID =  " + userid + @" union all select Users.UserID,Users.ParentID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted!=1   ) select ROW_NUMBER() over(order by d.DeviceID asc)rowid,DeviceName,d.SerialNumber,PhoneNum,d.CellPhone,u.UserName,u.CellPhone as UserCellPhone,d.Created
                            ,case when d.ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),d.ActiveDate,120) end as ActiveDate
                            ,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate 
                            ,l.LastCommunication from Devices d inner join Users u on u.UserID=d.UserID inner join Dictionary di on di.DataValue=d.Model inner join LKLocation l on d.deviceid=l.DeviceID 
                            where d.UserID in(select UserID from subqry) and d.Deleted=0 and  DATEDIFF(MI,  l.LastCommunication ,getdate()) < " + Utils.offLineMinute;
                        Utils.DataSetToExcel(Dao.Selects(strSql), path += "\\" + FileName, title);
                        #endregion
                        break;
                    case "adevices":
                        #region 全部设备数
                        FileName = "全部设备数(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @"<h4>全部设备数</h4>
                        <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称</td><td> IMEI号 </td><td> 设备号码 </td><td> 车牌号 </td><td> 型号 </td><td> 联系电话 </td><td> 所属用户  </td><td>创建时间</td> <td>激活时间</td><td>到期时间</td><td> 离线时间 </td><td> 状态 </td> </tr>";
                        strSql = @"  with subqry(UserID,ParentID) as (
                                    select UserID,ParentID from Users where UserID =  " + userid + @" union all select Users.UserID,Users.ParentID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted!=1   )
                           select ROW_NUMBER() over(order by d.DeviceID asc) rowid, case when DeviceName='' then d.SerialNumber else DeviceName end DeviceName,d.SerialNumber,PhoneNum,CarNum,di.DataText , d.CellPhone,UserName,d.Created,case when ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),ActiveDate,120) end as ActiveDate
                                 ,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate ,l.LastCommunication,'' DeviceStatus 
								  from Devices d inner join Users u on d.UserID=u.UserID inner join Dictionary di on di.DataValue=d.Model left join lklocation l on l.deviceid=d.deviceid
                           where d.UserID in(select UserID from subqry) and d.Deleted=0";
                        Utils.DataSetToExcel(Dao.Selects(strSql), path += "\\" + FileName, title);
                        #endregion
                        break;
                    case "notenabled":
                        #region 未启用设备数
                        FileName = "未启用设备数(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @"<h4>未启用设备数</h4>
                        <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称</td><td> IMEI号 </td><td> 设备号码 </td><td> 联系号码 </td><td> 所属用户  </td><td> 联系电话  </td><td>创建时间</td><td>激活时间</td><td>到期时间</td><td> 离线时间 </td> </tr>";
                        strSql = @" with subqry(UserID,ParentID) as (
                                    select UserID,ParentID from Users where UserID =  " + userid + @" union all select Users.UserID,Users.ParentID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted!=1) 
                                select ROW_NUMBER() over(order by d.DeviceID asc)rowid, DeviceName, d.SerialNumber,PhoneNum, d.CellPhone, u.UserName, u.CellPhone as UserCellPhone,d.Created
                                ,case when d.ActiveDate = '1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19), d.ActiveDate, 120) end as ActiveDate
                                ,case when HireExpireDate = '1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19), HireExpireDate, 120) end HireExpireDate 
                                ,l.LastCommunication  from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model  inner join Users u on u.UserID=d.UserID                                 
                                where d.UserID in(select UserID from subqry)and d.Deleted=0 and lastcommunication is null";

                        Utils.DataSetToExcel(Dao.Selects(strSql), path += "\\" + FileName, title);
                        #endregion
                        break;
                    case "RenewalsExport":
                        #region 续费设备导出
                        strSql = string.Format(@"with subqry(UserID) as (select UserID from Users where UserID = {0} union all select Users.UserID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted = 0)   
						          select row_number() over(order by d.DeviceID asc) rowid, u.UserName,d.SerialNumber, d.devicename, d.ActiveDate,d.HireExpireDate, phonenum, l.lastcommunication 
                                  from devices d inner
                                  join users u on u.userid = d.userid inner
                                  join lklocation l  on l.deviceid = d.deviceid
                                  where d.deleted = 0 and d.ActiveDate >= '{1}' and d.ActiveDate <= '{2}' and d.userid in (select Userid from subqry)", userid == "" ? Utils.GetSession().UserID : userid, st + " 00:00:00", et + " 23:59:59");
                        dt = Dao.Selects(strSql);

                        FileName = "续费设备统计(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                        title = @" <h4>时间：" + st + "-" + et + @"</h4>
                         <tr style='border:1px solid #000000'> <td> 序号 </td> <td> 所属用户 </td> <td> IMEI号  </td> <td> 设备名称</td> <td> 激活时间 </td><td> 到期时间 </td><td> 流量卡号 </td><td> 与平台最后通信时间 </td></tr>";
                        Utils.DataSetToExcel(dt, path += "\\" + FileName, title);
                        #endregion
                        break;
                }
                var res = new { success = true ,FileName = FileName, Message = resMessage };
                return Dao.ToJson(res);
                //return "{\"success\":\"true\",\"FileName\":\"" + FileName + "\"}";
            }
            catch (Exception ex)
            {
                var res = new { success = false, FileName = "", Message = ex.Message };
                return Dao.ToJson(res);
            }

        }

        public static DataTable GetDeviceModel()
        {
            String strSql = "select DataText,DataValue from  Dictionary";
            return Dao.Selects(strSql);
        }

        /// <summary>
        ///  根据userid获取用户下所有的设备（包括下面的经销商下的设备）还有所有下属用户的USERID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static Hashtable GetDeviceNumber(String UserID)
        {
            String strSql = @" with subqry(UserID,UserName,ParentID, AllDeviceCount) as (
                              select UserID,UserName,ParentID,AllDeviceCount from Users where UserID = " + UserID + @"
                              union all
                              select Users.UserID,Users.UserName,Users.ParentID, Users.AllDeviceCount from Users,subqry
                              where Users.ParentID = subqry.UserID and users.Deleted!=1 
                              )
                              select * from subqry";
            DataTable dt = Dao.Selects(strSql);
            int count = 0;
            StringBuilder userids = new StringBuilder();
            Hashtable ht = new Hashtable();
            foreach (DataRow dr in dt.Rows)
            {
                count += Convert.ToInt32(dr["AllDeviceCount"].ToString());
                userids.Append(dr["UserID"] + ",");
            }
            ht["count"] = count;
            ht["userids"] = userids.Length > 0 ? userids.ToString().Substring(0, userids.Length - 1) : "";
            return ht;
        }

        /// <summary>
        /// 根据用户id 查询设备统计信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <returns></returns>
        public static String GetUserDeviceInfo(string model, String st, String et)
        { 
            string where = "";
            Dictionary<string, string> dicPars = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(model))
            {
                var models = model.Split(',');
                where = " and d.Model in(";
                for (int i = 0; i < models.Length; i++)
                {
                    dicPars["Model"+i] = models[i];
                    where += "@Model"+i+",";
                }
                where = where.Substring(0,where.Length - 1);
                where+=")"; 
            }
            if (!string.IsNullOrEmpty(st) && !string.IsNullOrEmpty(et))
            {
                where += " and d.Created>=@StartTime and d.Created<=@EndTime ";
                dicPars["StartTime"] = st;
                dicPars["EndTime"] = et;
            }
            var UserID = Utils.GetSession().UserID;
            dicPars["UserID"] = UserID;
            string strSql = @"with subqry(UserID, ParentID) as 
                            (
                                select UserID, ParentID from Users where UserID = @UserID and users.Deleted = 0 
                                union all 
                                select Users.UserID, Users.ParentID from Users, subqry where Users.ParentID = subqry.UserID
                            )
                           select d.DeviceID, di.SortOrder, di.AccountID, d.ActiveDate, d.HireExpireDate,d.HireStartDate, l.LastCommunication from
                           Devices d  inner join subqry s on s.UserID = d.UserID inner join dictionary di on di.DataValue = d.Model
                           left join LKLocation l on l.DeviceID = d.DeviceID where d.Deleted = 0 " + where;
            var pars = new SqlParameter[dicPars.Count];
            var index = 0;
            foreach (KeyValuePair<string,string> item in dicPars)
            {
                pars[index++] = new SqlParameter(item.Key,item.Value);
            }
            int allCount = 0, onlineCount = 0, offlineCount = 0, useCount = 0, online7DayCount = 0, noEnableCount = 0, arrearsCount = 0;
            try
            {
                MG_DAL.SQLServerOperating sql = new MG_DAL.SQLServerOperating();
                DataTable dt = sql.Selects(strSql, pars);
             
                allCount = dt.Rows.Count; //所有设备
                for (int i = 0; i < allCount; i++)
                {
                    var row = dt.Rows[i];
                    if (string.IsNullOrEmpty( row["LastCommunication"].ToString()))
                    {
                        noEnableCount++; //未激活的设备
                        continue;
                    }
                    var LastCommunication = Convert.ToDateTime(row["LastCommunication"]);
                    TimeSpan ts = (DateTime.Now - LastCommunication);
                    var offlineTime = row["AccountID"].Equals("2") ? 1450 : Convert.ToInt32(row["SortOrder"]);
                    if (ts.TotalMinutes <= offlineTime)
                    {
                        onlineCount++; //在线设备
                    }
                    else
                    {
                        offlineCount++; // 离线设备
                    }
                    var HireStartDate = Convert.ToDateTime(row["HireStartDate"]);
                    if ((DateTime.Now - HireStartDate).TotalDays >= 7)
                    {
                        online7DayCount++; // 七天内使用过的设备
                    }
                    if (HireStartDate.ToString("yyyy") != "1900")
                    {
                        useCount++; //使用过的设备
                    }
                    var HireExpireDate = Convert.ToDateTime(row["HireExpireDate"]);
                    if (HireExpireDate.ToString("yyyy") != "1900" && HireExpireDate.AddHours(8) <= DateTime.Now)
                    {
                        arrearsCount++; //过期的设备
                    }
                }
            }
            catch (Exception ex)
            {
                 
            }
           
          
            if (Utils.GetSession().SuperAdmin == "1")
            { 
                var info = new
                {
                    count = allCount + 500000,
                    deviceOnline = onlineCount + 400000,
                    deviceOffline = offlineCount + 20000,
                    useDeviceCount = useCount + 400000,
                    Online7Day = online7DayCount + 400000,
                    NoEnable = noEnableCount + 15000,
                    Arrears = arrearsCount + 20000
                };
                return MG_BLL.Utils.ToJson(info);
            }
            else
            {
                var info = new
                {
                    count = allCount,
                    deviceOnline = onlineCount,
                    deviceOffline = offlineCount,
                    useDeviceCount = useCount,
                    Online7Day = online7DayCount,
                    NoEnable = noEnableCount,
                    Arrears = arrearsCount
                };
                return MG_BLL.Utils.ToJson(info); 
            } 
        }


        /// <summary>
        /// 查询多少天内过期的设备
        /// </summary>
        /// <param name="Day">查询多少天的设备</param>
        /// <param name="uid">查询那个用户下的设备</param>
        /// <param name="current">当前页</param>
        /// <param name="rowCount">一页显示多少行</param>
        /// <param name="sort">排序</param>
        /// <param name="searchPhrase">搜索内容．．．</param> 
        /// <returns></returns>
        public static String GetDeviceHireExpire(String Day, String uid, string t, string current, string rowCount, string sort, string searchPhrase, string byUser = null)
        {
            int day = Convert.ToInt32(Day);
            String strSql = "", countSql = "";
            // Hashtable table = GetDeviceNumber(uid);

            string where = "", count = "0";
            string sortStr = "", sortROW_NUMBER = "";
            if (searchPhrase.Trim() != "")
            {
                where = @" and (DeviceName like @searchPhrase or d.SerialNumber like @searchPhrase or u.UserName like @searchPhrase 
                          or CarNum like @searchPhrase or case when ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),ActiveDate,120) end like @searchPhrase 
                          or case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end like @searchPhrase
                          or PhoneNum like @searchPhrase or d.CellPhone like @searchPhrase or di.DataText like @searchPhrase )";
            }
            if (!string.IsNullOrWhiteSpace(byUser))
            {
                where += " and u.UserID=" + byUser;
            }
            if (!string.IsNullOrWhiteSpace(sort))
            {
                sortStr = (" order by " + sort).Replace("rowid", "DeviceID");
                sortROW_NUMBER = sortStr;
                if (sortROW_NUMBER.IndexOf("UserCellPhone") > 0)
                {
                    sortROW_NUMBER = sortROW_NUMBER.Replace("UserCellPhone", "u.CellPhone");
                }
                else if (sortROW_NUMBER.IndexOf("CellPhone") > 0)
                {
                    sortROW_NUMBER = sortROW_NUMBER.Replace("CellPhone", "d.CellPhone");
                }
                sortROW_NUMBER = sortROW_NUMBER.Replace("SerialNumber", "d.SerialNumber").Replace("DeviceID", "d.DeviceID");
            }
            String with = "with subqry(UserID) as (  select UserID from Users where UserID = " + uid + @" union all select Users.UserID from Users,subqry
                                  where Users.ParentID = subqry.UserID and users.Deleted != 1) ";
            if (t != null)
            {
                switch (t)
                {
                    #region 统计数据
                    case "1":   //查询设备统计的所有设备列表 ROW_NUMBER() OVER (ORDER BY d.deviceid)num,
                        countSql = with + @"select count(*) from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model inner join Users u on u.UserID=d.UserID where d.UserID in(select UserID from subqry ) and d.Deleted=0 " + where;
                        strSql = with + @" select top " + rowCount + @" * from ( select ROW_NUMBER() over(" + sortROW_NUMBER + @") as rowid,d.DeviceID, DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,d.CellPhone,u.UserName,d.Created,case when d.ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),d.ActiveDate,120) end as ActiveDate,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate ,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText ,u.UserID ,u.CellPhone as UserCellPhone 
	                            from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model inner join Users u on u.UserID=d.UserID where d.UserID in(select UserID from subqry) and d.Deleted=0 " + where + ") allDevices where rowid > (" + rowCount + "*(" + current + "-1))" + sortStr;
                        break;
                    case "2":  //当前在线的设备
                        countSql = with + @" select count(*) from  Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model inner join Users u on u.UserID=d.UserID  where d.UserID in(select UserID from subqry)
                                    and d.Deleted=0 and  DATEDIFF(MI,  l.LastCommunication ,getdate()) < di.SortOrder " + where + "";
                        strSql = with + @"select top " + rowCount + @" * from (select ROW_NUMBER() over(" + sortROW_NUMBER + @") rowid,d.DeviceID, DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,d.CellPhone,d.Created,case when d.ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),d.ActiveDate,120) end as ActiveDate,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate ,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText ,u.UserName,u.UserID ,u.CellPhone as UserCellPhone 
                                    from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model inner join Users u on u.UserID=d.UserID
                                    where d.UserID in(select UserID from subqry) 
                                    and d.Deleted=0 and  DATEDIFF(MI,  l.LastCommunication ,getdate()) < di.SortOrder " + where + @" )
                                   onLineDevices where rowid > (" + rowCount + "*(" + current + "-1))" + sortStr;
                        break;
                    case "3": //使用超过七天的设备
                        countSql = with + @"  select count(*) from Devices d inner join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model inner join Users u on u.UserID=d.UserID where d.UserID in(select UserID from subqry)
                                    and d.Deleted=0 and datediff(DAY,  HireStartDate , GETDATE()) >= 7 and ActiveDate > '1900-01-01 00:00:00.000' " + where + " ";
                        strSql = with + @"select top " + rowCount + @" * from ( select ROW_NUMBER() over(" + sortROW_NUMBER + @") rowid, d.DeviceID,DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,d.CellPhone,d.Created,case when d.ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),d.ActiveDate,120) end as ActiveDate,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate ,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText ,u.UserName,u.UserID ,u.CellPhone as UserCellPhone 
                                   from Devices d inner join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model inner join Users u on u.UserID=d.UserID
                                    where d.UserID in(select UserID from subqry)
                                    and d.Deleted=0 and datediff(DAY,  HireStartDate , GETDATE()) >= 7 and ActiveDate > '1900-01-01 00:00:00.000' " + where + ") use7Day where rowid>(" + rowCount + "*(" + current + "-1))" + sortStr;
                        break;
                    case "4": //离线设备数
                        countSql = with + @" select count(*) from Devices d inner join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model inner join Users u on u.UserID=d.UserID 
                            where d.UserID in(select UserID from subqry) and d.Deleted=0 and datediff(MI,ISNULL(LastCommunication,'1900-01-01 00:00:00.000'),GETDATE()) >= di.SortOrder " + where + "";
                        strSql = with + @" select top " + rowCount + " * from (select ROW_NUMBER() over (" + sortROW_NUMBER + @") rowid, d.DeviceID,DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,d.CellPhone,d.Created,case when d.ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),d.ActiveDate,120) end as ActiveDate,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate ,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText,u.UserName,u.UserID ,u.CellPhone as UserCellPhone 
                             from Devices d inner join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model  inner join Users u on u.UserID=d.UserID
                            where d.UserID in(select UserID from subqry) and d.Deleted=0 and datediff(MI,ISNULL(LastCommunication,'1900-01-01 00:00:00.000'),GETDATE()) >= di.SortOrder " + where + ") offLine30Day where rowid>(" + rowCount + "*(" + current + "-1))" + sortStr;
                        break;
                    case "5": //使用设备数，上线即算使用
                        countSql = with + @"  select count(*) from Devices d right join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model  inner join Users u on u.UserID=d.UserID 
                               where d.UserID in(select UserID from subqry)and d.Deleted=0 and HireStartDate != '1900-01-01 00:00:00.000' " + where + "";
                        strSql = with + @"select top " + rowCount + @" * from ( select ROW_NUMBER()over(" + sortROW_NUMBER + @") rowid, d.DeviceID,DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,d.CellPhone,d.Created,case when d.ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),d.ActiveDate,120) end as ActiveDate,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate ,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText,u.UserName,u.UserID ,u.CellPhone as UserCellPhone 
                               from Devices d right join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model  inner join Users u on u.UserID=d.UserID
                               where d.UserID in(select UserID from subqry)and d.Deleted=0 and HireStartDate != '1900-01-01 00:00:00.000' " + where + ") useCountDevices where rowid>(" + rowCount + "*(" + current + "-1))" + sortStr;
                        break;
                    case "6": //已过期设备数
                        countSql = with + "select COUNT(*) from Devices d inner join Users u on u.UserID=d.UserID inner join Dictionary di on di.DataValue=d.Model inner join LKLocation l on d.deviceid=l.DeviceID where DATEADD(HH,8, d.HireExpireDate) < GETDATE() and d.Deleted=0 and HireExpireDate != '1900-01-01 00:00:00.000' and d.UserID in (select UserID from subqry)" + where + "";
                        strSql = with + @" select top " + rowCount + @" * from (
                                   select ROW_NUMBER() over(" + sortROW_NUMBER + @") rowid, d.DeviceID,d.SerialNumber,DeviceName,CarUserName,CarNum,d.CellPhone,PhoneNum,di.DataText,d.Created,case when d.ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),d.ActiveDate,120) end as ActiveDate,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate ,u.UserName,u.UserID ,u.CellPhone as UserCellPhone ,l.LastCommunication
                                    from Devices d inner join Users u on u.UserID=d.UserID inner join Dictionary di on di.DataValue=d.Model inner join LKLocation l on d.deviceid=l.DeviceID
                                   where  DATEADD(HH,8, HireExpireDate) < GETDATE() and d.Deleted=0 and HireExpireDate != '1900-01-01 00:00:00.000' and d.UserID in (select UserID from subqry)" + where + @"
                                  ) a where rowid > (" + rowCount + "*(" + current + "-1))" + sortStr;
                        break;
                    case "7": //未启用设备数
                        countSql = with + @"select count(*) from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model inner join Users u on u.UserID=d.UserID  where d.UserID in(select UserID from subqry) and d.Deleted=0 and lastcommunication is null " + where + "";
                        strSql = with + @"select top " + rowCount + @" * from (select ROW_NUMBER() over(" + sortROW_NUMBER + @") rowid, d.DeviceID, DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,d.CellPhone,d.Created,case when d.ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),d.ActiveDate,120) end as ActiveDate,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate ,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText,u.UserName,u.UserID ,u.CellPhone as UserCellPhone 
                                      from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model  inner join Users u on u.UserID=d.UserID
                        where d.UserID in(select UserID from subqry) and d.Deleted=0 and lastcommunication is null " + where + ") InactiveDevices where rowid>(" + rowCount + "*(" + current + "-1))" + sortStr;
                        break;
                        #endregion
                }
            }
            else
            {
                #region 查询多少天过期设备 
                if (day > 0)
                {  //多少天内过期的设备
                    countSql = with + "select count(*) from Devices d inner join Users u on d.UserID=u.UserID inner join Dictionary di on di.DataValue=d.Model where DATEDIFF(DAY,GETDATE(), HireExpireDate) < " + day + " and DATEDIFF(DAY,GETDATE(), HireExpireDate)>0 and d.Deleted=0 and d.userid in(select UserID from subqry) " + where + "";

                    strSql = with + @"select top " + rowCount + @" * from( select ROW_NUMBER() over(" + sortROW_NUMBER + @")rowid, d.DeviceID,DeviceName,CarNum,d.SerialNumber,d.Created,ActiveDate,HireExpireDate,UserName,di.DataText,d.CellPhone , PhoneNum,u.CellPhone as UserCellPhone,l.LastCommunication,u.UserID
                          from Devices d inner join Users u on d.UserID=u.UserID inner join Dictionary di on di.DataValue=d.Model left join LKLocation l on l.DeviceID=d.DeviceID
                          where DATEDIFF(DAY,GETDATE(), HireExpireDate) < " + day + " and DATEDIFF(DAY,GETDATE(), HireExpireDate)>0 and d.Deleted=0 and d.userid in(select UserID from subqry)" + where + @"
                           ) 多少天内过期的设备 where rowid>(" + rowCount + @"*(" + current + @"-1))  " + sortStr;
                }
                else if (day == 0)
                { //全部设备
                    countSql = with + "select count(*) from Devices d inner join Users u on d.UserID=u.UserID inner join Dictionary di on di.DataValue=d.Model where d.userid in(select UserID from subqry) and d.Deleted=0 " + where + " ";


                    strSql = with + @" select top " + rowCount + @" * from (select ROW_NUMBER() over(" + sortROW_NUMBER + @") rowid, d.DeviceID,DeviceName,CarNum,d.SerialNumbe,d.Created,r,case when ActiveDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),ActiveDate,120) end as ActiveDate
                                 ,case when HireExpireDate='1900-01-01 00:00:00.000' then '未激活' else convert(varchar(19),HireExpireDate,120) end HireExpireDate ,UserName,di.DataText,d.CellPhone , PhoneNum  ,u.CellPhone as UserCellPhone,l.LastCommunication,u.UserID
                                from Devices d inner join Users u on d.UserID=u.UserID inner join Dictionary di on di.DataValue=d.Model left join LKLocation l on l.DeviceID=d.DeviceID
                                where  d.userid in(select UserID from subqry)
                               and d.Deleted=0 " + where + @"
                               ) a where rowid>(" + rowCount + "*(" + current + @"-1))" + sortStr;
                }
                else if (day < 0)
                {   //已经过期的设备

                    //                    strSql = @" select top " + rowCount + @" DeviceID,DeviceName,CarNum,SerialNumber,ActiveDate,HireExpireDate,UserName from Devices d inner join Users u on d.UserID=u.UserID  
                    //                             where HireExpireDate < GETDATE() and HireExpireDate > '1900-01-01 00:00:00.000' and d.userid in(" + table["userids"] + @") and d.Deleted=0
                    //                             and DeviceID not in ( select top (" + rowCount + "*(" + current + @"-1)) DeviceID from Devices d inner join Users u on d.UserID=u.UserID where HireExpireDate < GETDATE() and HireExpireDate > '1900-01-01 00:00:00.000' and d.userid in(" + table["userids"] + @") and d.Deleted=0 " + where + " order by DeviceID asc )" + where + @"
                    //                              order by DeviceID asc";
                    //                    countSql = "select count(*) from Devices d inner join Users u on d.UserID=u.UserID  where HireExpireDate < GETDATE() and HireExpireDate > '1900-01-01 00:00:00.000' and d.userid in(" + table["userids"] + ") and d.Deleted=0 " + where;
                }
                #endregion
            }
            #region 执行SQL

            StringBuilder rows = new StringBuilder();
            DataTable dt = new DataTable();
            if (where != "")
            {
                count = Dao.Select(countSql, new SqlParameter[] { new SqlParameter("@searchPhrase", "%" + searchPhrase.Trim() + "%") })[""].ToString();
                dt = Dao.Selects(strSql, new SqlParameter[] { new SqlParameter("@searchPhrase", "%" + searchPhrase.Trim() + "%") });
            }
            else
            {
                count = Dao.Select(countSql)[0].ToString();
                dt = Dao.Selects(strSql);
            }
            StringBuilder jsonStr = new StringBuilder();
            jsonStr.Append("{");
            jsonStr.Append("\"current\": " + current + " ,");
            jsonStr.Append("\"rows\": [");
            foreach (DataRow dr in dt.Rows)
            {
                dr["DeviceName"] = dr["DeviceName"].ToString() == "" ? dr["SerialNumber"] : dr["DeviceName"];
                jsonStr.Append("{\"rowid\":\"" + dr["rowid"] + "\",\"DeviceID\":\"" + dr["DeviceID"] + "\",\"DeviceName\":\"" + dr["DeviceName"] + "\",\"CarNum\":\"" + dr["CarNum"] +
                    "\",\"SerialNumber\":\"" + dr["SerialNumber"] + "\",\"Created\":\"" + dr["Created"] + "\",\"ActiveDate\":\"" + dr["ActiveDate"] + "\",\"HireExpireDate\":\"" + dr["HireExpireDate"] + "\",\"UserName\":\"" + dr["UserName"] + "\",\"UserID\":\"" + dr["UserID"] +
                    "\",\"Model\":\"" + dr["DataText"] + "\",\"CellPhone\":\"" + dr["CellPhone"] + "\",\"PhoneNum\":\"" + dr["PhoneNum"] + "\",\"UserCellPhone\":\"" + dr["UserCellPhone"] + "\",\"LastCommunication\":\"" + dr["LastCommunication"] + "\"},");
            }
            if (jsonStr.Length > 30) {
                jsonStr.Remove(jsonStr.Length - 1, 1);
            }
            //jsonStr.Append(jsonStr);
            jsonStr.Append("],");
            jsonStr.Append(" \"total\": " + count + "");
            jsonStr.Append("}");
            #endregion
            return jsonStr.ToString(); //Dao.Selects(strSql,  new SqlParameter[] { new SqlParameter("@searchPhrase", "%" + searchPhrase + "%")}   );
        }

        /// <summary>
        /// 查询离线设备列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="DeviceModel"></param>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        /// <returns></returns>
        public static DataTable GetOfflineDeviceList(String userid, String DeviceModel, String sTime, String eTime, String ModelType, string current, string rowCount, string sort, string searchPhrase)
        {
            String where = "";
            Dictionary<string, object> dic = Dao.ToDict(DeviceModel);
            if (dic["model"] != null)
            {
                where += "  and DataValue in( ";// " + DeviceModel;
                ArrayList arr = (ArrayList)dic["model"];
                for (int i = 0; i < arr.Count; i++)
                {
                    where += arr[i] + ",";
                }
                where = where.Substring(0, where.Length - 1) + ")";
            }
            Hashtable ht = GetDeviceNumber(userid);
            String strSql = "";
            switch (ModelType)
            {
                case "0": //查询离线设备
                    strSql = @" select top " + rowCount + @" *  from 
                                (
                                 select row_number() over(order by d.DeviceID) as rowid,d.DeviceID,d.SerialNumber,DeviceName,CarNum,PhoneNum,CarUserName,d.CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText 
                                  from devices d inner join users u on d.userid=u.userid  inner join LKLocation l on l.deviceid=d.DeviceID inner join Dictionary di on di.DataValue=d.Model where DATEDIFF(MI, l.LastCommunication ,GETDATE()) > 10 and d.Deleted=0 and l.LastCommunication > '" + sTime + "' and l.LastCommunication < '" + eTime + "' and d.UserID=" + userid + where + @" order by offTime desc
                                )  OfflineDevices where rowid > (" + rowCount + "*(" + current + "-1))";
                    strSql = @"select d.DeviceID,d.SerialNumber,DeviceName,CarNum,PhoneNum,CarUserName,d.CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText  from devices d inner join users u on d.userid=u.userid  inner join LKLocation l on l.deviceid=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
//                               where DATEDIFF(MI, l.LastCommunication ,GETDATE()) > 10 and d.Deleted=0 and l.LastCommunication > '" + sTime + "' and l.LastCommunication < '" + eTime + "' and d.UserID=" + userid + where + " order by offTime desc";
                    break;
                case "1":   //查询设备统计的所有设备列表 ROW_NUMBER() OVER (ORDER BY d.deviceid)num,
                    strSql = @" select top " + rowCount + @" * from (
	                            select ROW_NUMBER() over(order by d.DeviceID) as rowid, DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText 
	                            from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model where d.UserID in(" + ht["userids"] + @") and d.Deleted=0 
                                ) allDevices where rowid > (" + rowCount + "*(" + current + "-1))";
                    // strSql = @"select DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model where d.UserID in(" + ht["userids"] + @")
                    //             and d.Deleted=0 order by d.DeviceID";
                    break;
                case "2":  //当前在线的设备
                    strSql = @"select top " + rowCount + @" * from (
                                select ROW_NUMBER() over(order by d.DeviceID) rowid, DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                                where d.UserID in(" + ht["userids"] + @")
                                and d.Deleted=0 and  DATEDIFF(MI,  l.LastCommunication ,getdate()) < " + Utils.offLineMinute + @"  
                                  ) onLineDevices where rowid > (" + rowCount + "*(" + current + "-1))";
                    break;
                case "3": //使用超过七天的设备
                    strSql = @"select top " + rowCount + @" * from (
                                select ROW_NUMBER() over(order by d.DeviceID) rowid, DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText from Devices d inner join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                                where d.UserID in(" + ht["userids"] + @")
                                and d.Deleted=0 and datediff(DAY,  HireStartDate , GETDATE()) >= 7 and ActiveDate > '1900-01-01 00:00:00.000' 
                                ) use7Day where rowid>(" + rowCount + "*(" + current + "-1))";
                    strSql = @"select DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText from Devices d inner join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                                where d.UserID in(" + ht["userids"] + @")
                                and d.Deleted=0 and   datediff(DAY,  HireStartDate , GETDATE()) >= 7  and ActiveDate > '1900-01-01 00:00:00.000' order by d.DeviceID";
                    break;
                case "4": //离线设备数
                    strSql = @" select top " + rowCount + @" * from (
                             select ROW_NUMBER() over (order by DATEDIFF(MI,l.LastCommunication,GETDATE()) desc ) rowid,DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText
                             from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                            where d.UserID in(" + ht["userids"] + @") and d.Deleted=0 and datediff(MI,ISNULL(LastCommunication,'1900-01-01 00:00:00.000'),GETDATE()) >= " + Utils.offLineMinute + @" 
                            ) offLine30Day where rowid>(" + rowCount + "*(" + current + "-1))";
                    break;
                case "5": //使用设备数，上线即算使用
                    strSql = @"select top " + rowCount + @" * from (
                                select ROW_NUMBER()over(order by d.DeviceID) rowid,DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText
                               from Devices d right join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                               where d.UserID in(6)and d.Deleted=0 and HireStartDate != '1900-01-01 00:00:00.000'
                               ) useCountDevices where rowid>(" + rowCount + "*(" + current + "-1))";
                    break;
                case "6": //欠费设备数

                    break;
                case "7": //未启用设备数
                    strSql = @"select top " + rowCount + @" * from (     
                               select ROW_NUMBER() over(order by d.DeviceID) rowid, DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText
                               from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model where d.UserID in(6) and d.Deleted=0 and lastcommunication is null
                               ) InactiveDevices where rowid>(" + rowCount + "*(" + current + "-1))";
                    //                    strSql = @"select DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText
                    //                               from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model where d.UserID in(" + ht["userids"] + ") and d.Deleted=0 and lastcommunication is null";
                    break;
                default:
                    break;
            }
            return Dao.Selects(strSql);
        }

        public static string GetOfflineDeviceList(String userid, String sTime, String eTime, String ModelType, string current, string rowCount, string sort, string searchPhrase)
        {

            Hashtable ht = GetDeviceNumber(userid);
            String strSql = "";
            switch (ModelType)
            {
                case "0": //查询离线设备
                    strSql = @" select top " + rowCount + @" *  from 
                                (
                                 select row_number() over(order by d.DeviceID) as rowid,d.DeviceID,d.SerialNumber,DeviceName,CarNum,PhoneNum,CarUserName,d.CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText 
                                  from devices d inner join users u on d.userid=u.userid  inner join LKLocation l on l.deviceid=d.DeviceID inner join Dictionary di on di.DataValue=d.Model where DATEDIFF(MI, l.LastCommunication ,GETDATE()) > 10 and d.Deleted=0 and l.LastCommunication > '" + sTime + "' and l.LastCommunication < '" + eTime + "' and d.UserID=" + userid + @" order by offTime desc
                                )  OfflineDevices where rowid > (" + rowCount + "*(" + current + "-1))";
                    //                    strSql = @"select d.DeviceID,d.SerialNumber,DeviceName,CarNum,PhoneNum,CarUserName,d.CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText  from devices d inner join users u on d.userid=u.userid  inner join LKLocation l on l.deviceid=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                    //                               where DATEDIFF(MI, l.LastCommunication ,GETDATE()) > 10 and d.Deleted=0 and l.LastCommunication > '" + sTime + "' and l.LastCommunication < '" + eTime + "' and d.UserID=" + userid + where + " order by offTime desc";
                    break;
                case "1":   //查询设备统计的所有设备列表 ROW_NUMBER() OVER (ORDER BY d.deviceid)num,
                    strSql = @" select top " + rowCount + @" * from (
	                            select ROW_NUMBER() over(order by d.DeviceID) as rowid, DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText 
	                            from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model where d.UserID in(" + ht["userids"] + @") and d.Deleted=0 
                                ) allDevices where rowid > (" + rowCount + "*(" + current + "-1))";
                    // strSql = @"select DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model where d.UserID in(" + ht["userids"] + @")
                    //             and d.Deleted=0 order by d.DeviceID";
                    break;
                case "2":  //当前在线的设备
                    strSql = @"select top " + rowCount + @" * from (
                                select ROW_NUMBER() over(order by d.DeviceID) rowid, DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                                where d.UserID in(" + ht["userids"] + @")
                                and d.Deleted=0 and  DATEDIFF(MI,  l.LastCommunication ,getdate()) < " + Utils.offLineMinute + @" 
                                  ) onLineDevices where rowid > (" + rowCount + "*(" + current + "-1))";
                    break;
                case "3": //使用超过七天的设备
                    strSql = @"select top " + rowCount + @" * from (
                                select ROW_NUMBER() over(order by d.DeviceID) rowid, DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText from Devices d inner join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                                where d.UserID in(" + ht["userids"] + @")
                                and d.Deleted=0 and datediff(DAY,  HireStartDate , GETDATE()) >= 7 and ActiveDate > '1900-01-01 00:00:00.000' 
                                ) use7Day where rowid>(" + rowCount + "*(" + current + "-1))";
                    strSql = @"select DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText from Devices d inner join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                                where d.UserID in(" + ht["userids"] + @")
                                and d.Deleted=0 and   datediff(DAY,  HireStartDate , GETDATE()) >= 7  and ActiveDate > '1900-01-01 00:00:00.000' order by d.DeviceID";
                    break;
                case "4": //离线设备数
                    strSql = @" select top " + rowCount + @" * from (
                             select ROW_NUMBER() over (order by DATEDIFF(MI,l.LastCommunication,GETDATE()) desc ) rowid,DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText
                             from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                            where d.UserID in(" + ht["userids"] + @") and d.Deleted=0 and datediff(MI,ISNULL(LastCommunication,'1900-01-01 00:00:00.000'),GETDATE()) >= " + Utils.offLineMinute + @"
                            ) offLine30Day where rowid>(" + rowCount + "*(" + current + "-1))";
                    break;
                case "5": //使用设备数，上线即算使用
                    strSql = @"select top " + rowCount + @" * from (
                                select ROW_NUMBER()over(order by d.DeviceID) rowid,DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText
                               from Devices d right join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model
                               where d.UserID in(6)and d.Deleted=0 and HireStartDate != '1900-01-01 00:00:00.000'
                               ) useCountDevices where rowid>(" + rowCount + "*(" + current + "-1))";
                    break;
                case "6": //欠费设备数

                    break;
                case "7": //未启用设备数
                    strSql = @"select top " + rowCount + @" * from (     
                               select ROW_NUMBER() over(order by d.DeviceID) rowid, DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText
                               from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model where d.UserID in(6) and d.Deleted=0 and lastcommunication is null
                               ) InactiveDevices where rowid>(" + rowCount + "*(" + current + "-1))";
                    //                    strSql = @"select DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication,DATEDIFF(MI,l.LastCommunication,GETDATE()) offTime,di.DataText
                    //                               from Devices d left join LKLocation l on l.DeviceID=d.DeviceID inner join Dictionary di on di.DataValue=d.Model where d.UserID in(" + ht["userids"] + ") and d.Deleted=0 and lastcommunication is null";
                    break;
                default:
                    break;
            }
            return strSql;
        }

        /// <summary>
        /// 获取每个用户的离线设备数量
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static DataTable GetUsersOffLineDevicCount()
        {
            Hashtable ht = GetDeviceNumber(Utils.GetSession("UserInfo").UserID);
            string strSql = string.Format(@"select  u.UserID,u.UserName+'/'+ convert(varchar(10), COUNT(*))+'台' UserName from Devices d inner join Users u on d.UserID=u.UserID inner join LKLocation l on l.DeviceID=d.DeviceID where d.Deleted=0 and d.Deleted=0 
                             and DATEDIFF(MI,l.LastCommunication,GETDATE()) > {0} and d.userid in({1}) group by u.UserID,u.UserName", Utils.offLineMinute, ht["userids"]);
            return Dao.Selects(strSql);
        }

        /// <summary>
        /// Statistic 设备统计
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static DataTable GetDeviceStatisticByUserID(String userid, String type)
        {
            Hashtable ht = GetDeviceNumber(userid);
            ///所有设备 
            String strSql = @"select  ROW_NUMBER() OVER (ORDER BY d.deviceid)num,DeviceName,d.SerialNumber,CarNum,PhoneNum,CarUserName,CellPhone,HireExpireDate,l.LastCommunication from Devices d left join LKLocation l on l.DeviceID=d.DeviceID 
                        where d.UserID in(" + ht["userids"] + ") and d.Deleted=0 and  DATEDIFF(MI,  l.LastCommunication ,getdate()) < " + Utils.offLineMinute + " order by d.DeviceID";

            return Dao.Selects(strSql);
        }

        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="GroupName">分组名称</param>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public static String NewGroup(String GroupName, String UserID)
        {
            String strSql = "insert into groups(GroupName,UserID,Username,Description, Created,GroupType,AccountID,Deleted)values('" + GroupName + "'," + UserID + ",'','',GETDATE(),-1,-1,0) select @@IDENTITY ";
            DataRow status = Dao.Select(strSql);
            if (status != null)
                return "{\"success\":true,\"gid\":\"" + status[0] + "\"}";
            else
                return "{\"success\":false}";
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="GroupID">分组id</param>
        /// <returns></returns>
        public static String DelGroup(String GroupID)
        {
            string strSql = ""; 
            try
            {
                strSql = "update Devices set GroupID=-1 where GroupID=@GroupID";
                int count = Dao.ExecutionSQL(strSql, new SqlParameter[] { new SqlParameter("GroupID", GroupID) });
                string msg = "";
                if (count > 0)
                {
                    msg = " 该分组下的设备以移到默认组下.";
                }
                strSql = "delete from groups where groupid= " + GroupID;
                int status = Dao.ExecutionSQL(strSql);
                if (status > 0)
                {
                    return "{\"success\":true,\"msg\":\"删除分组成功！" + msg + "\"}";
                }
                else
                {
                    return "{\"success\":false,\"msg\":\"删除分组失败！\"}";
                }
            }
            catch (Exception ex)
            {
                return "{\"success\":false,\"msg\":\""+ ex .Message+ "\"}";
            } 
        }

        /// <summary>
        /// 修改分组名称
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="GroupName"></param>
        /// <returns></returns> 
        public static String UpdateGroup(String GroupID, String GroupName)
        {
            String strSql = String.Format("update groups set GroupName='{0}' where GroupID={1} ", GroupName, GroupID);
            int status = Dao.ExecutionSQL(strSql);
            if (status > 0)
            {
                return "{\"success\":true,\"msg\":\"修改分组名称成功！\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"修改分组名称失败！\"}";
            }
        }

        public static String UpdateDeviceGroupID(String toGroupID, String SerialNumber)
        {
            String strSql = String.Format("update devices set GroupID={0} where SerialNumber='{1}' and Deleted=0", toGroupID, SerialNumber);
            int status = Dao.ExecutionSQL(strSql);
            if (status > 0)
            {
                return "{\"success\":true,\"msg\":\"成功将该设备移至指定分组\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"移动分组失败\"}";
            }
        }


        public static DataTable GetDeviceInfoBySerialNumber(String DeviceID)
        {
            String strSql = String.Format(@"select DeviceID, SerialNumber, DeviceName,CarUserName, CarNum, CellPhone, PhoneNum, Model, Description,  ActiveDate, HireStartDate, HireExpireDate, SpeedLimit, Icon, OILCoefficient, CarImg ,di.DataText,ServerID
                            from Devices d inner join Dictionary di on d.Model=di.DataValue where d.Deleted=0 and DeviceID = {0}", DeviceID);
            DataTable dt = com.Dao.Selects(strSql);
            foreach (DataRow dr in dt.Rows)
            {
                dr["CarImg"] = Utils.urlconvertor(System.Web.HttpContext.Current.Server.MapPath("~") + @"Upload\" + dr["CarImg"]);
            }
            return dt;
        }

        /// <summary>
        /// 设备上传图片
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static String UploadFile(HttpContext context)
        {
            HttpFileCollection files = context.Request.Files;
            String uploadType = context.Request["uploadType"];
            if (files.Count > 0)
            {
                HttpPostedFile File = files[0];
                if (!string.IsNullOrWhiteSpace(uploadType) && uploadType == "importDevices")
                {
                    #region importDevices 
                    string userid = context.Request.QueryString["userid"];
                    string fileName = File.FileName;
                    int count = 0;
                    if (fileName.IndexOf('.') > 0 && (fileName.Substring(fileName.IndexOf('.'), fileName.Length - fileName.IndexOf('.')) == ".xls" || fileName.Substring(fileName.IndexOf('.'), fileName.Length - fileName.IndexOf('.')) == ".xlsx"))
                    {
                        #region 批量导入设备 
                        fileName = Utils.urlconvertorlocal("") + "/Upload/" + DateTime.Now.Ticks + "-" + fileName;
                        File.SaveAs(fileName);
                        DataSet ds = Utils.LoadDataFromExcel(fileName);
                        if (Utils.checkDataSet(ds))
                        {
                            if (!ds.Tables[0].Columns.Contains("型号") || !ds.Tables[0].Columns.Contains("IMEI号"))
                            {
                                return "{\"success\":true,\"msg\":\"没找到IMEI号和型号，请检查数据是否正确。\"}"; ;
                            }
                            int sum = 0; count = ds.Tables[0].Rows.Count;
                            List<string> HasImeiList = new List<string>();
                            DataTable HasImei = Dao.Selects(" select SerialNumber from devices where Deleted=0 ");//new List<Dictionary<string, object>>();
                            foreach (DataRow dr in HasImei.Rows)
                                HasImeiList.Add(dr["SerialNumber"].ToString());
                            Hashtable dictionaryHt = Utils.dsTransToHashtable(GetDictionaryList());
                            string strSql = "";
                            string ServerID = "1";
                            SqlParameter[] sqlParameter = null;
                            for (int i = 0; i < count; i++)
                            {
                                DataRow row = ds.Tables[0].Rows[i];
                                if (HasImeiList.Contains(row["IMEI号"].ToString().Trim()))
                                    continue;
                                Hashtable insertData = new Hashtable(); insertData["SpeedLimit"] = "";
                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    #region 获取列名及数据 
                                    string columnName = ds.Tables[0].Columns[j].ColumnName;
                                    switch (columnName)
                                    {
                                        case "设备名字":
                                            insertData["DeviceName"] = row[columnName].ToString().Trim();
                                            break;
                                        case "型号":
                                            if (!dictionaryHt.ContainsKey(row["型号"].ToString().Trim()))
                                                continue;
                                            insertData["Model"] = dictionaryHt[row["型号"].ToString().Trim()];
                                            if (insertData["Model"].ToString() == "123" || insertData["Model"].ToString() == "122" || insertData["Model"].ToString() == "124")
                                                ServerID = "0";
                                            break;
                                        case "IMEI号":
                                            if (row[columnName].ToString().Trim() == "")
                                                continue;
                                            insertData["Imei"] = row[columnName].ToString().Trim();
                                            break;
                                        case "超速":
                                            insertData["SpeedLimit"] = row[columnName].ToString().Trim();
                                            break;
                                        case "车牌号":
                                            insertData["carNum"] = row[columnName].ToString().Trim();
                                            break;
                                        case "设备号码":
                                            insertData["DevicePhone"] = row[columnName].ToString().Trim();
                                            break;
                                        case "联系号码":
                                            insertData["userPhone"] = row[columnName].ToString().Trim();
                                            break;
                                        case "VC码":
                                            string vc = row[columnName].ToString().Trim();
                                            if (vc == "123456")
                                                continue;
                                            insertData["vc"] = string.IsNullOrEmpty(vc) ? "123456" : vc;
                                            break;
                                    }
                                    #endregion
                                }
                                if (!insertData.ContainsKey("Imei")||!insertData.ContainsKey("Model")  || string.IsNullOrEmpty(insertData["Imei"].ToString()) || string.IsNullOrEmpty(insertData["Model"].ToString()))
                                {
                                    continue;
                                }
                                strSql = @"insert into Devices(SerialNumber,DeviceName,DevicePassword,CarUserName, CarNum, CellPhone,Status,PhoneNum,Model, Description,Created,Deleted ,ActiveDate,HireStartDate,HireExpireDate,SpeedLimit,UserID,GroupID,Icon,AddHireDay,OILCoefficient,BSJIP,ServerID,CarImg)
                                values(@SerialNumber,@DeviceName,@DevicePassword,@CarUserName, @CarNum, @CellPhone,@Status,@PhoneNum,@Model,@Description,@Created,@Deleted ,@ActiveDate,@HireStartDate,@HireExpireDate,@SpeedLimit,@UserID,@GroupID,@Icon,@AddHireDay,@OILCoefficient,@BSJIP,@ServerID,@CarImg)";
                                sqlParameter = new SqlParameter[] {
                                             new SqlParameter("@SerialNumber", insertData["Imei"]),
                                             new SqlParameter("@DeviceName",string.IsNullOrEmpty( insertData["DeviceName"].ToString()) ?"":insertData["DeviceName"]),
                                             new SqlParameter("@DevicePassword", insertData["vc"]),
                                             new SqlParameter("@CarUserName",""),
                                             new SqlParameter("@CarNum", insertData["carNum"]),
                                             new SqlParameter("@CellPhone", insertData["userPhone"]),
                                             new SqlParameter("@Status", 1),
                                             new SqlParameter("@PhoneNum", insertData["DevicePhone"]),
                                             new SqlParameter("@Model", insertData["Model"]),
                                             new SqlParameter("@Description",""),
                                             new SqlParameter("@Created",DateTime.Now.AddHours(-8)),
                                             new SqlParameter("@Deleted", "0"),
                                             new SqlParameter("@ActiveDate", "1900-01-01 00:00:00.000"),
                                             new SqlParameter("@HireStartDate",DateTime.Now.AddHours(-8)),
                                             new SqlParameter("@HireExpireDate", "1900-01-01 00:00:00.000"),
                                             new SqlParameter("@SpeedLimit", insertData["SpeedLimit"].ToString() ==  "" ? "0.00" : insertData["SpeedLimit"]),
                                             new SqlParameter("@UserID", userid),
                                             new SqlParameter("@GroupID", "-1"),
                                             new SqlParameter("@Icon", "1"),
                                             new SqlParameter("@AddHireDay", "0"),
                                             new SqlParameter("@OILCoefficient","0"),
                                             new SqlParameter("@BSJIP",""),
                                             new SqlParameter("@ServerID",ServerID),
                                             new SqlParameter("@CarImg","")};

                               // Utils.log(string.Format("imei号:{0} \t 设备名字:{1} \t 型号:{2} \t 超速：{3}\t 车牌号:{4} \t 联系号码:{5} \t 设备号码:{6}",
                               //                       insertData["Imei"], insertData["DeviceName"], insertData["Model"], insertData["SpeedLimit"], insertData["carNum"], insertData["userPhone"], insertData["DevicePhone"]));
                                //Utils.log("SQL:" + strSql);
                                int status = Dao.ExecutionSQL(strSql, sqlParameter);
                                Utils.log("导入状态：" + status);
                                if (status > 0)
                                {
                                    HasImeiList.Add(insertData["Imei"].ToString());
                                    sum++;
                                }
                            }
                            return "{\"success\":true,\"msg\":\"一共" + count + "条,成功" + sum + "条\"}";
                          
                        }
                        else
                        {
                            return "{\"success\":false,\"msg\":\"请选择有效的Excel文件！\"}";
                        }
                        #endregion
                    }
                    else
                    {
                        return "{\"success\":false,\"msg\":\"请选择一个Excel文件！\"}";
                    }
                    #endregion
                }

                else if (!string.IsNullOrWhiteSpace(uploadType) && uploadType == "importupdatesim")
                {
                    #region 批量导入SIM卡

                    string fileName = File.FileName;
                    int count = 0;
                    int sum = 0;
                    List<string> lst = new List<string>();
                    if (fileName.EndsWith(".xls")  || fileName.EndsWith(".xlsx"))
                    {
                        fileName = Utils.urlconvertorlocal("") + "/Upload/" + DateTime.Now.Ticks + "-" + fileName;
                        File.SaveAs(fileName);
                        DataSet ds = Utils.LoadDataFromExcel(fileName);
                        if (Utils.checkDataSet(ds))
                        {
                            count = ds.Tables[0].Rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                DataRow dr = ds.Tables[0].Rows[i];//获取一行数据
                                if (dr[0].ToString() == "" || dr[1].ToString() == "")
                                {
                                    continue;
                                }
                                string sql = "update Devices set PhoneNum='" + dr[0] + "' where SerialNumber='" + dr[1] + "' and deleted=0";
                                lst.Add(sql);
                            }
                            Task.Run(() => 
                            {
                                sum += Dao.ExecutionSQL(lst, false);
                            }); 
                            return "{\"success\":true,\"msg\":\"后台正在进行批量修改,请稍后自行查看数据！\"}";
                            //  return "{\"success\":true,\"msg\":\"一共" + count + "条,成功" + sum + "条\"}";
                        }
                        else
                        {
                            return "{\"success\":false,\"msg\":\"请选择有效的Excel文件！\"}";
                        }
                    }
                    else
                    {
                        return "{\"success\":false,\"msg\":\"请选择有效的Excel文件！\"}";
                    }
                    #endregion
                }
                else if (!string.IsNullOrEmpty(uploadType) && uploadType == "fileuploadsearch")
                {
                    #region 上传Excel批量查询 
                    string fileName = File.FileName;
                    if (fileName.EndsWith(".xls") || fileName.EndsWith("xlsx"))
                    {
                        string dir = Utils.urlconvertorlocal("") + "/Upload/";
                        fileName = dir + DateTime.Now.Ticks + "-" + fileName;
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        File.SaveAs(fileName);
                        DataSet ds = Utils.LoadDataFromExcel(fileName);
                        int count = 0;
                        if (Utils.checkDataSet(ds))
                        {
                            DataRow tempRow;
                            try
                            {
                                count = ds.Tables[0].Rows.Count;
                                if (count < 0)
                                {
                                    return "{\"success\":false,\"msg\":\"未解析到数据。\"}";
                                }
                                string columnName = ds.Tables[0].Columns[0].ColumnName;
                                if (columnName.IndexOf("流量") >= 0)
                                {
                                    columnName = "PhoneNum";
                                }
                                else
                                {
                                    columnName = "SerialNumber";
                                }
                                StringBuilder sb = new StringBuilder();
                                for (int i = 0; i < count; i++)
                                {
                                    DataRow dr = ds.Tables[0].Rows[i];//获取一行数据
                                    if (string.IsNullOrEmpty(dr[0].ToString()))
                                    {
                                        continue;
                                    }
                                    sb.Append("'" + dr[0].ToString() + "',");
                                    //string sql = "update Devices set PhoneNum=" + dr[0] + " where SerialNUmber='" + dr[1] + "' and deleted=0"; 
                                }
                                String path = System.Web.HttpContext.Current.Server.MapPath(".") + "\\Log";
                                fileName = "批量查询(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                                string title = @" <h4>批量查询</h4>
                            <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称 </td><td>IMEI</td><td>型号</td><td>流量卡号</td><td>创建时间</td><td>激活时间</td><td>到期时间</td><td>通信时间</td><td>所属用户</td><td>最后在线地址</td><td>纬度</td><td>经度</td></tr>";
                                if (sb.Length > 0)
                                {
                                    Stopwatch sw = new Stopwatch();
                                    sw.Start();
                                    sb = sb.Remove(sb.Length - 1, 1);
                                    string strSql = @"select ROW_NUMBER() OVER( order by d.Created) rowIndex, DeviceName, d.SerialNumber,di.DataText, PhoneNum ,d.Created,
                                             case when d.ActiveDate = '1900-01-01 00:00:00.000' then '未激活' else CONVERT(varchar, d.ActiveDate,120) end ActiveDate,
                                             case when d.HireExpireDate = '1900-01-01 00:00:00.000' then '未激活' else CONVERT(varchar, d.HireExpireDate,120) end HireExpireDate,
                                             l.LastCommunication, u.UserName,Address,l.OLat,l.OLng
                                             from Devices d inner join Dictionary di on d.Model = di.DataValue inner
                                             join Users u on d.UserID = u.UserID left
                                             join lklocation l on l.deviceid = d.deviceid
                                             where d.Deleted = 0 and u.deleted = 0  and d." + columnName + " in( " + sb.ToString() + ") order by d.Created";
                                    DataTable dt = Dao.Selects(strSql);
                                     
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        tempRow = row;
                                        double bd_lat = 0, bd_lng = 0;
                                        if (!string.IsNullOrEmpty( row["OLat"].ToString()) && !string.IsNullOrEmpty(row["OLng"].ToString()))
                                        {
                                            EvilTransform.bd_encrypt(Convert.ToDouble(row["OLat"]), Convert.ToDouble(row["OLng"]), ref bd_lat, ref bd_lng);
                                            Task<string> adr = GetAddressByLatlngAsync(bd_lat.ToString(), bd_lng.ToString());
                                            row["Address"] = adr.Result;
                                        } 
                                        //row["Address"] = GetAddressByLatlng(bd_lat.ToString(), bd_lng.ToString());
                                    }
                                    sw.Stop();
                                    var mis = sw.Elapsed.TotalMilliseconds;
                                    Utils.log(string.Format("批量查询:一共{0}条,用时{1}分:", dt.Rows.Count, (mis / 1000 / 60)));
                                    Utils.DataSetToExcel(dt, path += "\\" + fileName, title);
                                }
                                else
                                {
                                    Utils.DataSetToExcel(new DataTable(), path += "\\" + fileName, title);
                                }
                                return "{\"success\":true,\"FileName\":\"" + fileName + "\"}";
                            }
                            catch (Exception ex)
                            {
                                Utils.log("批量查询 Error:"+ex.Message); 
                                return "{\"success\":false,\"FileName\":\"\",\"msg\":\""+ex.Message+"\"}";
                            } 
                        }
                    }
                    #endregion
                }
                else
                {
                    if (File.ContentLength > 2097152)
                    {
                        return "{\"success\":false,\"msg\":\"不能上传大于2M的图片。\"}";
                    }
                    String filepath = HttpContext.Current.Server.MapPath("~") + @"Upload\";
                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    String fileName = DateTime.Now.ToString("yyMMddhhmmss") + ".png";
                    File.SaveAs(filepath + fileName);
                    return Utils.GetThumbnail(filepath + fileName, filepath + "S-" + fileName, 100, 110, "S-" + fileName);
                }
            }
            return "{\"success\":false,\"msg\":\"请选择至少一个文件！\"}";
        }

        /// <summary>
        /// 修改设备信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static String UpdateDevicInfo(String data)
        {
            Dictionary<String, Object> dic = Dao.ToDict(data);
            Dictionary<String, object> NewDic = new Dictionary<string, object>();

            NewDic["DeviceName"] = dic["DeviceName"];
            NewDic["DeviceID"] = dic["DeviceID"];
            NewDic["ServerID"] = Boolean.Parse(dic["LBS"].ToString()) ? "1" : "0";
            if (Utils.GetSession().SuperAdmin == "1")
            {
                NewDic["PhoneNum"] = dic["DevicePhoneNum"];
            } 
            NewDic["SpeedLimit"] = dic["SpeedLimit"];
            NewDic["CarNum"] = dic["DeviceCarNum"];
            NewDic["CellPhone"] = dic["DeviceCallPhone"];
            NewDic["CarUserName"] = dic["DeviceCarUserName"];
            NewDic["OILCoefficient"] = dic["Device100KM"];
            NewDic["Icon"] = dic["Icon"];
            NewDic["CarImg"] = dic["DeviceCarImg"];
            NewDic["Description"] = dic["DeviceDescription"];

            int count = Dao.Update(NewDic, "Devices", "DeviceID");
            if (count > 0)
            {
                return "{\"success\":true,\"msg\":\"修改设备信息成功！ \"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"修改设备信息失败！ \"}";
            }
        }

        /// <summary>
        ///  给发送命令
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static String SendCommand(String data)
        {
            try
            {
                //MG-X21,MG-X20，MG-X10 --  S200/远程恢复油电， S201/远程断油电， SCF1/终端撤防 ， SCF0/终端设防 ， S712/上传间隔
                //MG-300, CheckLocation/查询定位， HFYD/远程恢复油电 ，DYD/断油电
                //MG-X81,MG-X50  S712/上传间隔
                //MG-X83 209CDW/打开GPS时间
                //MG-X11D  撤防 设防 刹车 取消刹车
                Dictionary<String, Object> dic = Dao.ToDict(data);
                string CommandType = dic["CommandType"].ToString();
                string imei = dic["SN"].ToString();
                string model = dic["Model"].ToString();
                string DeviceID = dic["DeviceID"].ToString();
                string pwd  = dic["pwd"].ToString(); //客户端传过来的数据，不一定是密码
                if (dic["CommandType"].ToString() == "S7122" || dic["CommandType"].ToString() == "S7123")
                {
                    int temp = 0;
                    if (int.TryParse(dic["pwd"].ToString(), out temp) || temp < 5)
                    {
                        return Utils.SendCommand(imei, Convert.ToInt32(DeviceID), dic["CommandType"].ToString(), Convert.ToInt32(dic["Model"]), Phone: temp.ToString());
                    }
                    else
                    {
                        return "{\"success\":false,\"msg\":\"请输入有效的数字！\"}";
                    }
                    //  return Utils.SendCommand(dic["CommandType"].ToString(), Convert.ToInt32(dic["DeviceID"]), Convert.ToInt32(dic["Model"]), dic["SN"].ToString(), dic["TrueOrFalse"].ToString(), dic["pwd"].ToString());
                }
                else if (CommandType.IndexOf("X11D") >= 0)
                {
                    #region  

                    string[] cmds = CommandType.Split(',');
                    if (cmds.Length == 2)
                    {
                        string strSql = "";
                        var cmd = cmds[1];
                        string UserID = Utils.GetSession("UserInfo").UserID;
                        if (cmd == "DZWL-ON")
                        {
                            SqlParameter[] pars = new SqlParameter[] { new SqlParameter("DeviceID", DeviceID) };
                            strSql = "delete from GeoFence where  FenceName='一键围栏' and  DeviceID=@DeviceID";
                            Dao.ExecutionSQL(strSql, pars);
                            strSql = "  select OLat,OLng from  LKLocation where DeviceID=@DeviceID";
                            Hashtable table = Dao.Select(strSql, pars);
                            if (table.Count > 0)
                            {
                                bool b = AddGeoFence("一键围栏", UserID, DeviceID, table["OLat"].ToString(), table["OLng"].ToString(), dic["pwd"].ToString(), "");// dic["pwd"] 存的半径
                                if (b)
                                {
                                    return "{\"success\":true,\"msg\":\"发送成功！\"}";
                                }
                            }
                        }
                        else if (cmd == "DZWL-OFF")
                        {
                            strSql = "delete from GeoFence where  FenceName='一键围栏' and  DeviceID=@DeviceID";
                            SqlParameter[] pars = new SqlParameter[] { new SqlParameter("DeviceID", DeviceID) };
                            int count = Dao.ExecutionSQL(strSql, pars);
                            if (count > 0)
                            {
                                MG_BLL. Utils.SendTcpCmd("VTR-DZWL-" + DeviceID);
                                return "{\"success\":true,\"msg\":\"发送成功！\"}";
                            }
                        }
                        else
                        {
                            strSql = " select Password from Users where UserID = @UserID";
                            string password = Dao.Select(strSql, new SqlParameter[] { new SqlParameter("UserID", UserID) })["Password"].ToString();
                            password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                            string p = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
                            if (password == p)
                            {
                                string command = "VTR-Command-" + imei + "-" + cmd;
                                string res = MG_BLL. Utils.SendTcpCmd(command);
                                if (res == "1")
                                {
                                    return "{\"success\":true,\"msg\":\"发送成功！\"}";
                                }
                                else
                                {
                                    return "{\"success\":false,\"msg\":\"发送失败！\"}";
                                }
                            }
                        }
                    }
                    #endregion

                }
                else if (CommandType == "209CDW")
                {
                    //int status = cmdByPhone.SendCommandByPhone("4209801819", 11078, "209CDW", 123, "10");
                    return Utils.SendCommand(imei, Convert.ToInt32(DeviceID), dic["CommandType"].ToString(), Convert.ToInt32(dic["Model"]), Phone: dic["pwd"].ToString());
                }
                else if (CommandType == "VTR")
                {
                    #region 定时唤醒
                    string command = dic["pwd"].ToString();
                    string[] sps = command.Split(',');
                    if (sps.Length != 3)
                        return "{\"success\":false,\"msg\":\"发送失败！\"}";
                    if ((Convert.ToInt32(sps[0]) < 1 || Convert.ToInt32(sps[0]) > 24) && Convert.ToInt32(sps[0]) != -1) //定时唤醒，1点-24点
                    {
                        return "{\"success\":false,\"msg\":\"发送失败！\"}";
                    }
                    if (Convert.ToInt32(sps[1]) < 1 || Convert.ToInt32(sps[1]) > 48) //唤醒间隔  定时唤醒为-1的话就是1-240， 定时唤醒是1-24的话就是1-24
                    {
                        return "{\"success\":false,\"msg\":\"发送失败！\"}";
                    }
                    if ((Convert.ToInt32(sps[2]) < 5 || Convert.ToInt32(sps[2]) > 360) && Convert.ToInt32(sps[2]) != 999) // 工作时间 
                    {
                        return "{\"success\":false,\"msg\":\"发送失败！\"}";
                    }
                    // DateTime date =Convert.ToDateTime( DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + " " + sps[0] + ":00:00");
                    // aaa,唤醒工作时间，005～360，单位分钟，aaa = 999表示一直工作
                    // bb, 唤醒间隔，01～24(01-240)，单位小时，bb = 0时，唤醒间隔30分钟
                    // hhmm, 每天定时唤醒时间，根据设备对应时区转换为当地时间，非0时区时间，bb小于或等于24小时有效
                    //VTR-Command-4210015245-OFF,aaa,bb,hh00
                    if (sps[2] == "999" || sps[2] == "005")
                    {
                        command = string.Format("VTR-Command-{0}-OFF,{1}", imei, sps[2]);
                    }
                    else
                    {
                        if (sps[0] == "-1")
                        {
                            if (Convert.ToInt32(sps[1]) < 0 || Convert.ToInt32(sps[1]) > 240)
                            {
                                return "{\"success\":false,\"msg\":\"发送失败！\"}";
                            }
                            command = string.Format("VTR-Command-{0}-OFF,{1},{2} ", dic["SN"].ToString(), sps[2].PadLeft(3, '0'), sps[1].PadLeft(2, '0'));
                        }
                        else
                        {
                            if (Convert.ToInt32(sps[1]) < 0 || Convert.ToInt32(sps[1]) > 24)
                            {
                                return "{\"success\":false,\"msg\":\"发送失败！\"}";
                            }
                            command = string.Format("VTR-Command-{0}-OFF,{1},{2},{3}", imei, sps[2].PadLeft(3, '0'), sps[1].PadLeft(2, '0'), sps[0].PadLeft(2, '0') + "00");
                        }
                    }
                    string res = MG_BLL. Utils.SendTcpCmd(command);
                    if (res == "1")
                    {
                        return "{\"success\":true,\"msg\":\"发送成功！\"}";
                    }
                    else
                    {
                        return "{\"success\":false,\"msg\":\"发送失败！\"}";
                    }
                    #endregion 
                }
                else if (model == "213")//MG-X30B
                {
                    string cmd = "VTR-Command-" + imei + "-" + CommandType;
                    var res = MG_BLL.Utils.SendTcpCmd(cmd);
                    if (res == "1")
                    {
                        return "{\"success\":true,\"msg\":\"发送成功！\"}";
                    }
                    else
                    {
                        return "{\"success\":false,\"msg\":\"发送失败！\"}";
                    }
                }
                else if (CommandType == "83BF")
                {
                    var userID = Utils.GetSession().UserID;
                    if (userID.Equals("3437") || userID.Equals("2"))
                    {
                        int gpsdate = 0;
                        if (int.TryParse(pwd, out gpsdate) && gpsdate >= 0 && gpsdate < 1000)
                        {
                            pwd = pwd.PadLeft(3,'0');
                            var cmd = "VTR-Command-" + imei + "-OFF," + pwd;
                            var res = MG_BLL.Utils.SendTcpCmd(cmd);
                            if (res == "1")
                            {
                                return "{\"success\":true,\"msg\":\"发送成功！\"}";
                            }
                            else
                            {
                                return "{\"success\":false,\"msg\":\"发送失败！\"}";
                            }
                        } 
                   }
                }
                else
                {
                    //String CommandType,String DeviceID,String Model,String SN,String TrueOrFalse,String Pwd
                    UsersAjax.UsersAjaxSoapClient user = new UsersAjax.UsersAjaxSoapClient();
                    int Status = user.ValidPassword(Convert.ToInt32(dic["UserID"]), dic["pwd"].ToString());
                    if (Status == 1)
                    {
                        return Utils.SendCommand(imei, Convert.ToInt32(DeviceID), dic["CommandType"].ToString(), Convert.ToInt32(dic["Model"]), TrueOrFalse: dic["TrueOrFalse"].ToString());
                    }
                    return "{\"success\":false,\"msg\":\"密码错误，请重新输入!\"}";
                }
                return "{\"success\":false,\"msg\":\"发送失败！\"}";
            }
            catch (Exception)                                                                                                                                                    
            {
                return "{\"success\":false,\"msg\":\"参数错误！\"}";
            }

        }

        /// <summary>
        /// 获取发送密令的返回状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static String GetCommandResponse(String id, String TimeZones)
        {
            CommandQueue.CommandQueueAjaxSoapClient cmdQueue = new CommandQueue.CommandQueueAjaxSoapClient();
            return cmdQueue.GetResponse(Convert.ToInt32(id), TimeZones);
           
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static DataTable SearchUserInfo(String UserID)
        {
            string strSql = string.Format(" select UserId,UserName,LoginName,FirstName,Address1,UserType,CellPhone from Users where Deleted=0 and UserID={0}", UserID);
            return Dao.Selects(strSql);
        }

        /// <summary>
        /// 修改设备信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static String UpdateDeviceUserInfo(String data)
        {
            Dictionary<String, Object> dic = Dao.ToDict(data);
            if (Dao.Update(dic, "Users", "userid") > 0)
            {
                return "{\"success\":true,\"msg\":\"修改信息成功！\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"修改信息失败。\"}";
            }
        }

        /// <summary>
        /// 重置密码（默认密码为123456）
        /// </summary>
        /// <param name="DeviceID"></param>
        /// <returns></returns>
        public static String ResetDevicePassword(String DeviceID)
        {
            String strSql = string.Format("update devices set DevicePassword='123456' where deviceID=@DeviceID", DeviceID);
            int status = Dao.ExecutionSQL(strSql,new SqlParameter[] {new SqlParameter("DeviceID", DeviceID) });
            if (status > 0)
            {
                return "{\"success\":true,\"msg\":\"重置密码成功！\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"重置密码失败！\"}";
            }
        }
        /// <summary>
        /// 删除设备(伪删除)(可批量删除)
        /// </summary>
        /// <param name="DeviceIDs">设备的ID</param>
        /// <param name="Destroy">是否彻底销毁</param>
        /// <returns></returns>
        public static String DeleteDevice(String DeviceIDs, String Destroy)
        {
            if (!Utils.GetSession().SuperAdmin.Equals("1"))
            {
                return "{\"success\":true,\"msg\":\"无权限!\"}";
            }
            List<Dictionary<string, object>> list = Dao.ToList(DeviceIDs);
            int status = 0, count = 0;
            try
            { 
                String strSql = "";
                bool des = false;
                Boolean.TryParse(Destroy, out des);
                DataRow drData = null;
                int hireDay = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    strSql = string.Format("select DATEDIFF(day, HireExpireDate, getdate()) HireExpireDate,UserID from Devices where deviceid={0}", list[i]["DeviceID"]);
                    drData = Dao.Select(strSql);
                    DevicesAjax.DevicesAjaxSoapClient delDevice = new DevicesAjax.DevicesAjaxSoapClient();
                    hireDay = 0;
                    if (Convert.ToInt32(drData["HireExpireDate"]) < 0)
                    {
                        hireDay = Convert.ToInt32(drData["HireExpireDate"]);
                    }
                    delDevice.UpdateHireExpriDateDays(Convert.ToInt32(drData["UserID"]), list[i]["DeviceID"].ToString(), hireDay); // 删除设备时，设置设备过期时间为 已过期，否则再添加该设备不能激活
                    //delDevice.DelDevice(Convert.ToInt32(list[i]["DeviceID"])); //调用老平台接口删除设备，否则再添加设备就不能激活
                    if (des) //彻底删除：删除Devices、LKLocation、ExceptionMessage、DevicesReport表的数据，以及删除ZLocation_DeviceID表
                    {
                        strSql = string.Format("delete from devices where deviceid={0} and deleted=1", list[i]["DeviceID"]);
                        ClearHistoryData(list[i]["DeviceID"].ToString());
                    }
                    else
                        strSql = string.Format("update devices set Deleted=1 where deviceID={0}", list[i]["DeviceID"]);
                    status = Dao.ExecutionSQL(strSql);
                    if (status > 0)
                        count++;
                }
            }
            catch (Exception ex)
            {
                Utils.log(string.Format("删除设备出错：{0},DeviceIDs:{1}", ex.Message, DeviceIDs));
            }
            if (count <= list.Count)
            {
                return "{\"success\":true,\"msg\":\"\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"\"}";
            }
        }

        /// <summary>
        /// 恢复已删除(伪删除)的设备
        /// </summary>
        /// <param name="deviceid"></param>
        /// <returns></returns>
        public static string RecoveryDevices(string DeviceIDs, string isClaerAll)
        {
            if (!Utils.GetSession().SuperAdmin.Equals("1"))
            {
                return "{\"success\":true,\"msg\":\"无权限!\"}";
            }
            List<Dictionary<string, object>> list = Dao.ToList(DeviceIDs);
            string strSql = "";
            bool ClaerAll = false;
            int count = 0, status = 0;
            Boolean.TryParse(isClaerAll, out ClaerAll);
            for (int i = 0; i < list.Count; i++)
            {
                strSql = string.Format("select Deviceid,SerialNumber,UserID from Devices where SerialNumber =(select SerialNumber from devices where DeviceID = {0}) and Deleted=0", list[i]["DeviceID"]);
                DataTable dt = Dao.Selects(strSql);
                if (Utils.checkDataTable(dt))
                {
                    continue;
                }

                if (ClaerAll)
                {
                    strSql = string.Format("update Devices set Deleted=0,DevicePassword='123456',Status=1,ActiveDate='1900-01-01 00:00:00.000',HireStartDate='{0}',HireExpireDate='1900-01-01 00:00:00.000',GroupID=-1,Icon=1,AddHireDay=0 where DeviceID= {1}", DateTime.Now.AddHours(-8), list[i]["DeviceID"]);
                    DevicesAjax.DevicesAjaxSoapClient deviceOper = new DevicesAjax.DevicesAjaxSoapClient();
                    deviceOper.UpdateHireExpriDateDays(Convert.ToInt32(Dao.Select("select UserID from devices where deviceid=32965")["UserID"]), list[i]["DeviceID"].ToString(), 1); ///过期时间加一天是为了激活设备。
                    ClearHistoryData(list[i]["DeviceID"].ToString());
                }
                else
                {
                    strSql = string.Format("update Devices set Deleted=0 where DeviceID={0}", list[i]["DeviceID"]);
                }
                status = Dao.ExecutionSQL(strSql);
                if (status > 0)
                    count++;
            }
            if (count > 0)
            {
                return "{\"success\":true,\"msg\":\"一共" + list.Count + "条数据，成功" + count + "条数据\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"恢复失败！！\"}";
            }
        }

        /// <summary>
        /// 设备转移和设备到期时间设定  
        /// </summary>
        /// <param name="devices"></param>
        /// <param name="years"></param>
        /// <param name="toUserID"></param>
        /// <returns></returns>
        public static String DeviceShiftOrExpire(String devices, String day = null, String toUserID = null)
        { 
            List<Dictionary<string, object>> list = Dao.ToList(devices);
            List<string> sqlList = new List<string>();
            string strSql = "";
            if (day != null && day != "")
            {
                if (!Utils.GetSession().SuperAdmin.Equals("1"))
                {
                    return "{\"success\":false,\"msg\":\"无权限！\"}";
                }
                StringBuilder DevicesSB = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    Dictionary<string, object> dic = list[i];
                    //strSql = @"update devices set  AddHireDay = (case when HireExpireDate='1900-01-01 00:00:00.000' and activeDate='1900-01-01 00:00:00.000' then AddHireDay+" + day + @" else AddHireDay end),
                    //       HireExpireDate = (case when HireExpireDate!='1900-01-01 00:00:00.000' and activeDate!='1900-01-01 00:00:00.000' then dateadd(day," + day + @",HireExpireDate) else HireExpireDate  end) 
                    //       where deviceid= " + dic["DeviceID"];
                    DevicesSB.Append(dic["DeviceID"] + ",");

                    //sqlList.Add(strSql);
                }
                DevicesSB.Remove(DevicesSB.Length - 1, DevicesSB.Length > 0 ? 1 : 0);
                DevicesAjax.DevicesAjaxSoapClient devicesOper = new DevicesAjax.DevicesAjaxSoapClient();
                int state = devicesOper.UpdateHireExpriDateDays(Convert.ToInt32(Utils.GetSession("UserInfo").UserID), DevicesSB.ToString(), Convert.ToInt32(day));
                if (state == 0)
                {
                    return "{\"success\":true,\"Refresh\":false,\"msg\":\"共" + list.Count + "台设备," + list.Count + "台设备到期时间修改成功！\"}";
                }
                else
                {
                    return "{\"success\":false,\"msg\":\"到期时间设置失败！\"}";
                }
            }
            if (toUserID != null && toUserID != "" && Convert.ToInt32(toUserID) > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Dictionary<string, object> dic = list[i];
                    strSql = "update Devices set UserID = " + toUserID + ", GroupID=-1 where DeviceID=  " + dic["DeviceID"];
                    sqlList.Add(strSql);
                }
                int count = Dao.ExecutionSQL(sqlList);
                return "{\"success\":true,\"Refresh\":true,\"msg\":\"共" + list.Count + "台设备," + count + "台设备转移成功！\"}";
            }
            return "{\"success\":false,\"msg\":\"参数错误\"}";
        }

        /// <summary>
        /// 删除用户(伪删除)
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static String DeleteUsers(String UserID)
        {
            if (string.IsNullOrEmpty(UserID))
            {
                return "{\"success\":true,\"msg\":\"参数错误！\"}";
            }
            string strSql = "select COUNT(*) Count from Devices where deleted=0 and userid = @UserID";

            int count = Convert.ToInt32(Dao.Select(strSql, new SqlParameter[] { new SqlParameter("UserID", UserID) })["Count"].ToString());
            if (count > 0)
            {
                return "{\"success\":false,\"msg\":\"该用户下还有设备,不能删除.\"}";
            }

            strSql = string.Format("update users set deleted=1 where userid={0}", UserID);
            count = Dao.ExecutionSQL(strSql);
            if (count > 0)
            {
                return "{\"success\":true,\"msg\":\"删除成功！\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"删除用户失败！\"}";
            }
        }

        /// <summary>
        /// 获取设备型号
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDictionaryList(string UserID=null)
        {
            string strSql = "";
            if (UserID == null)
            {
                strSql = "select  DataText,DataValue from Dictionary";
            }
            else
            {
                strSql = @"with temp(UserID,ParentID) as
                            (
                                select UserID, ParentID from Users where UserID = @UserID 
                                union all 
                                select Users.UserID, Users.ParentID from Users, temp
                                where Users.ParentID = temp.UserID and users.Deleted = 0
                            )
                            select DataText, DataValue from devices d inner
                            join temp t on t.UserID = d.UserID
                            inner join Dictionary di on di.DataValue = d.Model
                            group by DataText,DataValue ";
            }
            return Dao.Selects(strSql, new SqlParameter[] { new SqlParameter("UserID", Utils.GetSession().UserID) });
        }

        public static DataTable GetMessageTypeList()
        {
            try
            {
                //string strSql = @"select NotificationType,Message from ExceptionMessage em inner join Devices d on d.DeviceID=em.DeviceID 
                //            where UserID = @UserID group by NotificationType,Message";
                string strSql = "select NotificationType,Message from ExceptionMessage group by NotificationType,Message";

                return Dao.Selects(strSql, new SqlParameter[] { new SqlParameter("UserID", Utils.GetSession().UserID) });
            }
            catch (Exception)
            {
                return new DataTable();
            }
         
        }
        /// <summary>
        /// 添加新设备
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static String AddDevice(String data)
        {
            Dictionary<string, object> dic = Dao.ToDict(data);
            double speedLimit = 0.00;
            if (dic.ContainsKey("SpeedLimit") && dic["SpeedLimit"].ToString() != "")
            {
                if ((!double.TryParse(dic["SpeedLimit"].ToString(), out speedLimit)))
                {
                    return "{\"success\":false,\"msg\":\"请输入有效的超速速度\"}";
                }
                else
                {
                    if (speedLimit > 200 || speedLimit <= 60)
                    {
                        return "{\"success\":false,\"msg\":\"超速速度必须为60到200之间\"}";
                    }
                }
            }
            string[] imeis = dic["Imei"].ToString().Split('\n');
            if (imeis.Length == 0 || dic["Imei"].ToString().Trim() == "")
            {
                return "{\"success\":false,\"msg\":\"IMEI号最少为一个\"}";
            }
            List<string> HasImeiList = new List<string>();
            DataTable HasImei = Dao.Selects(" select SerialNumber from devices where Deleted=0 ");//new List<Dictionary<string, object>>();
            foreach (DataRow dr in HasImei.Rows)
                HasImeiList.Add(dr["SerialNumber"].ToString());

            String strSql = "";
            StringBuilder errorImei = new StringBuilder();
            errorImei.Append("[");

            List<string> listImei = new List<string>();
            for (int i = 0; i < imeis.Length; i++)
            {
                if (HasImeiList.Contains(imeis[i]))
                {
                    errorImei.Append("{\"imei\":\"" + imeis[i] + " - 该IMEI号已存在\"},"); continue;
                }
                if (!listImei.Contains(imeis[i]))
                    listImei.Add(imeis[i]);
                else
                    errorImei.Append("{\"imei\":\"" + imeis[i] + " - 列表出现重复IMEI\"},");
            }
            int count = 0;
            string ServerID = "1";
            string model = dic["Model"].ToString();
            if (model == "123" || model == "122" || model == "124")
            {
                ServerID = "0";
            }
            for (int i = 0; i < listImei.Count; i++)
            {
                if (string.IsNullOrEmpty( listImei[i].Trim()))
                    continue;
                if (listImei[i].Length > 20)
                {
                    errorImei.Append("{\"imei\":\"" + listImei[i] + " - 长度大于20\"},");
                    continue;
                }

                strSql = @"insert into Devices(SerialNumber,DeviceName,DevicePassword,CarUserName, CarNum, CellPhone,Status,PhoneNum,Model, Description,Created,Deleted ,ActiveDate,HireStartDate,HireExpireDate,SpeedLimit,UserID,GroupID,Icon,AddHireDay,OILCoefficient,BSJIP,ServerID,CarImg)
                            values(@SerialNumber,@DeviceName,@DevicePassword,@CarUserName, @CarNum, @CellPhone,@Status,@PhoneNum,@Model,@Description,@Created,@Deleted ,@ActiveDate,@HireStartDate,@HireExpireDate,@SpeedLimit,@UserID,@GroupID,@Icon,@AddHireDay,@OILCoefficient,@BSJIP,@ServerID,@CarImg);select @@identity";
                SqlParameter[] sqlParameter = new SqlParameter[] {
                     new SqlParameter("@SerialNumber", listImei[i]),
                     new SqlParameter("@DeviceName",""),
                     new SqlParameter("@DevicePassword", 123456),
                     new SqlParameter("@CarUserName",""),
                     new SqlParameter("@CarNum",""),
                     new SqlParameter("@CellPhone",""),
                     new SqlParameter("@Status", 1),
                     new SqlParameter("@PhoneNum",""),
                     new SqlParameter("@Model", model),
                     new SqlParameter("@Description",""),
                     new SqlParameter("@Created",DateTime.Now.AddHours(-8)),
                     new SqlParameter("@Deleted", "0"),
                     new SqlParameter("@ActiveDate", "1900-01-01 00:00:00.000"),
                     new SqlParameter("@HireStartDate",DateTime.Now.AddHours(-8)),
                     new SqlParameter("@HireExpireDate", "1900-01-01 00:00:00.000"),
                     new SqlParameter("@SpeedLimit",  speedLimit),
                     new SqlParameter("@UserID", dic["UserID"]),
                     new SqlParameter("@GroupID", "-1"),
                     new SqlParameter("@Icon", "3"),
                     new SqlParameter("@AddHireDay", "0"),
                     new SqlParameter("@OILCoefficient","0"),
                     new SqlParameter("@BSJIP",""),
                     new SqlParameter("@ServerID", ServerID),
                     new SqlParameter("@CarImg","")
                };
                Hashtable tb = Dao.Select(strSql, sqlParameter);
                if (Convert.ToInt32(tb[""]) <= 0)
                    errorImei.Append("{\"imei\":\"" + listImei[i] + " - IEMI号出现错误\"},");
                else
                {
                    count++;
                    strSql = "select COUNT(*) COUNT from Devices where SerialNumber=@SerialNumber and Deleted=1";
                    sqlParameter = new SqlParameter[] { new SqlParameter("@SerialNumber", listImei[i]) };
                    if (Convert.ToInt32(Dao.Select(strSql, sqlParameter)["COUNT"].ToString()) > 0)
                    {
                        DevicesAjax.DevicesAjaxSoapClient devicesOper = new DevicesAjax.DevicesAjaxSoapClient();
                        devicesOper.UpdateHireExpriDateDays(Convert.ToInt32(dic["UserID"]), tb[""].ToString(), 1); //如果该设备之前进行过删除的操作，要进行添加到底天数的 操作，否则无法激活！
                    }
                }
            }
            string reg = errorImei.ToString();
            if (reg.Length > 2)
            {
                reg = reg.Substring(0, reg.Length - 1);
            }
            if (count == 0)
            {
                return "{\"success\":false,\"SumLength\":\"" + imeis.Length + "\",\"successLength\":\"" + count + "\",\"errorImei\":" + reg + "]" + "}";
            }
            return "{\"success\":true,\"SumLength\":\"" + imeis.Length + "\",\"successLength\":\"" + count + "\",\"errorImei\":" + reg + "]" + "}";
        }

        public static string AddDevicesList(string carnum, string carusername, string cellphone,string description, string devicelist)
        {
            string msg = "";
            if (string.IsNullOrEmpty(devicelist))
                return "{\"success\":false,\"msg\":\"请至少添加一台GPS设备.\"}"; 
            List<Dictionary<string, object>> list = Dao.ToList(devicelist);
            if (list.Count <= 0)
                msg = "请至少添加一台GPS设备."; 
            if (string.IsNullOrEmpty(carnum))
                msg = "车牌号不能为空."; 
            if (string.IsNullOrEmpty(carusername))
                msg = "联系人不能为空."; 
            if (string.IsNullOrEmpty(cellphone) || !System.Text.RegularExpressions.Regex.IsMatch(cellphone, @"^[1]+[8,3,5,7]+\d{9}"))
                msg = "号码格式错误."; 
            if (!string.IsNullOrEmpty(msg))
            {
                 return "{\"success\":false,\"msg\":\""+ msg + "\"}";
            }
           
            string strSql = "";
            //List<string> sqlList = new List<string>();
            //List<SqlParameter[]> parsList = new List<SqlParameter[]>();
            string GroupName = carusername + "-" + carnum;
            string UserID= Utils.GetSession("UserInfo").UserID;
            strSql = "insert into groups(GroupName, UserID, Username, Description, Created, GroupType, AccountID, Deleted)values( @groupname, @userid, '', '', GETDATE(), -1, -1, 0)  select @@IDENTITY GroupID";
            Hashtable row = Dao.Select(strSql,new SqlParameter[] {new SqlParameter("groupname", GroupName),new SqlParameter("userid",UserID) });
            if (row != null)
            {
                string GroupID = row["GroupID"].ToString(); 
                strSql = "update devices set GroupID=@GroupID,CarNum=@CarNum,CarUserName=@CarUserName,Description=@Description,CellPhone=@CellPhone,DeviceName=" +
                    string.Format("CASE WHEN (select di.DataText from Devices d inner join Dictionary di on di.DataValue = d.Model where SerialNumber=@SerialNumber and deleted = 0) like'MG-X8%' THEN '{0}'+'-无线'ELSE '{0}'+'-有线'END", carusername); 
                int count = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    string where = " where SerialNumber=@SerialNumber and UserID=@UserID and deleted=0";
                    SqlParameter[] pars = new SqlParameter[] {
                        new SqlParameter("GroupID",GroupID),
                        new SqlParameter("CarNum",carnum),
                        new SqlParameter("CarUserName",carusername),
                        new SqlParameter("CellPhone",cellphone),
                        new SqlParameter("SerialNumber",list[i]["imei"]),
                        new SqlParameter("UserID",UserID),
                        new SqlParameter("Description",description)
                    };
                    count += Dao.ExecutionSQL(strSql + where, pars);
                }
                if (count == list.Count)
                {
                    return "{\"success\":true,\"msg\":\"添加成功!\"}";
                }
                else {
                       return "{\"success\":false,\"msg\":\"共"+list.Count+"台,成功" + count + "台\"}";
                }
            } 
            return "";
        }

        /// <summary>
        /// 清空所有报警消息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static String ClearAllMessage(String UserID)
        {
            String strSql = string.Format("update ExceptionMessage set deleted=1 ,clearby={1},clearDate = getdate() where deviceid in (select DeviceID from devices where userid={0}) ", UserID, Utils.GetSession("UserInfo").UserID);
            int count = Dao.ExecutionSQL(strSql);
            if (count > 0)
            {
                return "{\"success\":true,\"msg\":\"清除成功！\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"清除失败！\"}";
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="NewPassword">新的密码</param>
        /// <param name="OldPassword">旧密码</param>
        /// <returns></returns>
        public static String UpdatePassword(String NewPassword, String OldPassword)
        {
            if (!string.IsNullOrEmpty(NewPassword) || NewPassword  .Length <=3 || NewPassword.Length >=15)
            {
                return "{\"success\":false,\"msg\":\"请输入3-15位数的密码!\"}"; 
            }
            if (!string.IsNullOrEmpty(OldPassword) || OldPassword == NewPassword)
            {
                return "{\"success\":false,\"msg\":\"新密码不能跟旧密码一样\"}";
            }
            string password = "", strSql = "";
            int count = 0;
            if (Utils.GetSession("UserInfo").LoginType == "1")
            {
                string DeviceID = Utils.GetSession("UserInfo").DeviceID;
                strSql = string.Format("select DevicePassword from devices where deviceid = {0}", DeviceID);
                DataRow dr = Dao.Select(strSql);
                password = dr[0].ToString();
                if (password != OldPassword)
                    return "{\"success\":false,\"msg\":\"旧密码输入错误，请重新输入！\"}";
                strSql = "update Devices set DevicePassword = @Password where DeviceID=@DeviceID";
                SqlParameter[] par = { new SqlParameter("@Password", NewPassword), new SqlParameter("@DeviceID", DeviceID) };
                count = Dao.ExecutionSQL(strSql, par);
            }
            else
            {
                String UserID = Utils.GetSession("UserInfo").UserID;
                strSql = string.Format(" select password from users where userid= {0}", UserID);
                DataRow dr = Dao.Select(strSql);
                password = dr[0].ToString();
                if (password != OldPassword)
                    return "{\"success\":false,\"msg\":\"旧密码输入错误，请重新输入！\"}";

                strSql = "update users set Password=@Password where UserID=@UserID";
                SqlParameter[] par = { new SqlParameter("@Password", NewPassword), new SqlParameter("@UserID", UserID) };
                count = Dao.ExecutionSQL(strSql, par);
            }

            if (count > 0)
            {
                return "{\"success\":true,\"msg\":\"当前用户密码修改成功！\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"修改密码失败！\"}";
            }
        }

        /// <summary>
        /// 全局搜索 根据设备的IMEI、DeviceName、CarNum模糊查询设备
        /// </summary>
        /// <returns></returns>
        public static DataTable SearchDevices(String SearchText)
        {
            String strSql = string.Format(@"select ROW_NUMBER() OVER (ORDER BY d.created asc) num ,DeviceID,case when DeviceName='' then SerialNumber else DeviceName end DeviceName,SerialNumber,u.UserName,u.UserID,PhoneNum, DATEADD(HH,8,d.Created) Created,HireExpireDate,CarUserName from devices d inner join Users u on d.userid=u.userid 
                                where d.deleted=0 and d.userid in(" + GetDeviceNumber(Utils.GetSession("UserInfo").UserID)["userids"] + ") and (serialnumber like @SearchText or devicename like @SearchText or CarNum like @SearchText or PhoneNum like @SearchText or CarUserName like @SearchText) order by d.created asc");
            DataTable dt = Dao.Selects(strSql, new SqlParameter[] { new SqlParameter("@SearchText", "%" + SearchText + "%") });
            return dt;
        }

        /// <summary>
        /// 全局搜索，根据UserName或者LoginName进行搜索
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public static DataTable SearchUsers(String SearchText)
        {
            String strSql = string.Format(@"with subqry(UserID,UserName,LoginName,ParentID ,FirstName,CellPhone) as (
                              select UserID,UserName,LoginName,ParentID,FirstName,CellPhone from Users where UserID = {0}
                              union all
                              select Users.UserID,Users.UserName,Users.LoginName,Users.ParentID,Users.FirstName,Users.CellPhone from Users,subqry
                              where Users.ParentID = subqry.UserID and users.Deleted=0
                              )
                              select * from subqry where UserName like @SearchText or LoginName like @SearchText order by UserName collate Chinese_PRC_CS_AS_KS_WS", Utils.GetSession("UserInfo").UserID);

            DataTable dt = Dao.Selects(strSql, new SqlParameter[] { new SqlParameter("@SearchText", "%" + SearchText + "%") });
            return dt;
        }

        /// <summary>
        /// 设置用户只查看那些报警类型
        /// </summary>
        /// <param name="msgType">选择要查看的报警类型的报警ＩＤ，最后一个为是否过滤掉无线设备的离线报警信息(-1为过滤,0为不过滤)</param>
        /// <param name="UserID"></param> 
        /// <returns></returns>
        public static String SettingShowExceptionMsgType(String msgType, String UserID)
        {
            String strSql = string.Format(" update users set address2='{0}' where userid={1}", msgType, UserID);
            int count = Dao.ExecutionSQL(strSql);
            if (count > 0)
            {
                return "{\"success\":true,\"msg\":\"保存成功！\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"保存失败！\"}";
            }
        }

        /// <summary>
        /// 设置默认只查询哪几种类型的报警消息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static String GetSettingShowExceptionMsgType(String UserID)
        {
            String strSql = string.Format("select UserID,Address2 ExceptionMessageType from users where userid={0}", UserID);
            return Dao.Select(strSql)["ExceptionMessageType"].ToString();
        }

        /// <summary>
        /// 设置是否播放报警声音
        /// </summary>
        /// <param name="play">true or false</param>
        public static void SetIsPlay(String play)
        {
            Boolean.TryParse(play, out Utils.isPlayAudio);
            string isPlay = "1";
            if (Utils.isPlayAudio)
            {
                isPlay = "";
            }
            String strSql = string.Format(" update users set LastName='{0}' where userid={1} ", isPlay, Utils.GetSession("UserInfo").UserID);
            Dao.ExecutionSQL(strSql);
        }

        /// <summary>
        /// 获取该用户是否播放报警声音
        /// </summary>
        /// <returns></returns>
        public static bool GetIsPlay()
        {
            String strSql = string.Format("select LastName from users where userID={0}", Utils.GetSession("UserInfo").UserID);
            DataRow dr = Dao.Select(strSql);
            if (dr["LastName"] != null && dr["LastName"].ToString().Trim() == "1")
            {
                return false;
            }
            else
                return true;
        }
        /// <summary>
        /// 设置当前用户是否查询下级用户的报警信息
        /// </summary>
        /// <param name="LowerMsg">true 或 false</param>
        public static void setLowerMsg(String LowerMsg)
        {
            Boolean.TryParse(LowerMsg, out Utils.LowerMsg);
            string Lower = "1";
            if (Utils.LowerMsg)
            {
                Lower = "";
            }
            String strSql = string.Format(" update users set MiddleName='{0}' where userid={1} ", Lower, Utils.GetSession("UserInfo").UserID);
            Dao.ExecutionSQL(strSql);
        }

        /// <summary>
        /// 获取当前用户是否查询下级用户的报警信息
        /// </summary>
        /// <returns></returns>
        public static bool GetLowerMsg()
        {
            String strSql = string.Format("select MiddleName from users where userID={0}", Utils.GetSession("UserInfo").UserID);
            DataRow dr = Dao.Select(strSql);
            if (dr["MiddleName"] != null && dr["MiddleName"].ToString().Trim() == "1")
            {
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// 根据经纬度获取详细地址
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public static String GetAddressByLatlng(string lat, string lng)
        {
            try
            {
                
                decimal BaiduLat = 0;
                decimal BaiduLng = 0;
                decimal.TryParse(lat, out BaiduLat);
                decimal.TryParse(lng, out BaiduLng);
                string key = "address_" + BaiduLat.ToString("0.00000") + "-" + BaiduLng.ToString("0.00000");
                string cache = MG_BLL.Utils.GetCache<string>(key);
                if (!string.IsNullOrEmpty(cache))
                {
                    return cache;
                }
                POIService.POIServiceSoapClient poi = new POIService.POIServiceSoapClient();
                string address = poi.GetAddressByLatlng(BaiduLat, BaiduLng, "BAIDU", "ZH-CN");
                MG_BLL.Utils.SetCache(key, address);
                return address;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public async static Task<string> GetAddressByLatlngAsync(string lat, string lng)
        {
            try
            {
                return await Task.Run(() =>
                {
                    decimal BaiduLat = 0;
                    decimal BaiduLng = 0;
                    decimal.TryParse(lat, out BaiduLat);
                    decimal.TryParse(lng, out BaiduLng);
                    POIService.POIServiceSoapClient poi = new POIService.POIServiceSoapClient(); 
                    return poi.GetAddressByLatlng(BaiduLat, BaiduLng, "BAIDU", "ZH-CN");
                });
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 生成google kml文件
        /// </summary>
        /// <param name="DeviceID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static String DownloadLocation(string DeviceID, string date)
        {
            String path = System.Web.HttpContext.Current.Server.MapPath("../") + "\\kml";
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();
            StringBuilder sb = getPlayBack(DeviceID, date, DateTime.Now.ToString());
            string[] strs = sb.ToString().Split(';');
            path += "\\" + DeviceID + "_" + DateTime.Now.Ticks + ".kml";
            StringBuilder LatLng = new StringBuilder();
            for (int i = 0; i < strs.Length - 1; i++)
            {
                string[] latlng = strs[i].Split(',');
                LatLng.Append(latlng[0] + "," + latlng[1] + ",0 ");
            }
            XmlDocument doc = new XmlDocument();
            #region 创建kml文件
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", "no");
            doc.AppendChild(dec);
            XmlElement kml = doc.CreateElement("kml");
            kml.SetAttribute("xmlns", "http://www.opengis.net/kml/2.2"); kml.SetAttribute("xmlns:atom", "http://www.w3.org/2005/Atom");
            kml.SetAttribute("xmlns:gx", "http://www.google.com/kml/ext/2.2"); kml.SetAttribute("xmlns:kml", "http://www.opengis.net/kml/2.2");
            XmlElement Document = doc.CreateElement("Documen");
            XmlElement name = doc.CreateElement("NAME");
            name.InnerText = "我的爱车-2015-06-06";
            Document.AppendChild(name);
            XmlElement style = doc.CreateElement("Style");
            style.SetAttribute("id", "sn_ylw-pushpin0");
            XmlElement IconStyle = doc.CreateElement("IconStyle");
            XmlElement scale = doc.CreateElement("scale");
            scale.InnerText = "1.1";
            XmlElement Icon = doc.CreateElement("Icon");
            XmlElement href = doc.CreateElement("href");
            href.InnerText = "http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png";
            Icon.AppendChild(href);
            IconStyle.AppendChild(Icon);
            XmlElement hotSpot = doc.CreateElement("hotSpot");
            hotSpot.SetAttribute("x", "20"); hotSpot.SetAttribute("xunits", "pixels"); hotSpot.SetAttribute("yunits", "pixels");
            IconStyle.AppendChild(hotSpot);
            IconStyle.AppendChild(scale);
            style.AppendChild(IconStyle);
            XmlElement LineStyle = doc.CreateElement("LineStyle");
            XmlElement color = doc.CreateElement("color");
            color.InnerText = "ff0000ff";
            XmlElement width = doc.CreateElement("width");
            width.InnerText = "2";
            LineStyle.AppendChild(color);
            LineStyle.AppendChild(width);
            style.AppendChild(LineStyle);
            Document.AppendChild(style);
            XmlElement Style1 = doc.CreateElement("Style");
            Style1.SetAttribute("id", "sn_ylw-pushpin1");
            XmlElement IconStyle1 = doc.CreateElement("IconStyle");
            scale.InnerText = "1.3";
            IconStyle1.AppendChild(scale);
            XmlElement Icon1 = doc.CreateElement("Icon");
            href.InnerText = "http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png";
            Icon1.AppendChild(href);
            IconStyle1.AppendChild(Icon1);
            IconStyle1.AppendChild(hotSpot);

            Style1.AppendChild(IconStyle1);
            Style1.AppendChild(LineStyle);
            Document.AppendChild(Style1);

            XmlElement StyleMap = doc.CreateElement("StyleMap");
            StyleMap.SetAttribute("id", "msn_ylw-pushpin");
            XmlElement Pair = doc.CreateElement("Pair");
            XmlElement key = doc.CreateElement("key");
            key.InnerText = "normal";
            XmlElement styleUrl = doc.CreateElement("styleUrl");
            styleUrl.InnerText = "#sn_ylw-pushpin1";
            Pair.AppendChild(key);
            Pair.AppendChild(styleUrl);
            StyleMap.AppendChild(Pair);
            XmlElement Pair1 = doc.CreateElement("Pair");
            key.InnerText = "highlight";
            styleUrl.InnerText = "#sn_ylw-pushpin1";
            Pair1.AppendChild(key);
            Pair1.AppendChild(styleUrl);
            StyleMap.AppendChild(Pair1);
            Document.AppendChild(StyleMap);

            XmlElement Placemark = doc.CreateElement("Placemark");
            Placemark.AppendChild(name);
            styleUrl.InnerText = "#msn_ylw-pushpin";
            Placemark.AppendChild(styleUrl);
            Document.AppendChild(Placemark);
            XmlElement LineString = doc.CreateElement("LineString");
            XmlElement tessellate = doc.CreateElement("tessellate");
            tessellate.InnerText = "1";
            LineString.AppendChild(tessellate);
            XmlElement coordinates = doc.CreateElement("coordinates");
            coordinates.InnerText = LatLng.ToString();
            LineString.AppendChild(coordinates);
            Document.AppendChild(LineString);
            kml.AppendChild(Document);
            doc.AppendChild(kml);

            doc.Save(path);
            #endregion
            // FileStream fs = new FileStream(path + "\\1.xml", FileMode.OpenOrCreate);
            // StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.UTF8);

            // XmlDocument xml = new XmlDocument();
            // XmlElement node = xml.CreateElement("xml");
            // node.SetAttribute("version", "1.0"); node.SetAttribute("encoding", "UTF-8"); node.SetAttribute("standalone", "no");

            //// xml.Save(path+"\\"+DateTime.Now.ToString("yyyyMMddHHmmss"));
            // fs.Close();
            // sw.Close();
            return "success";
        }


        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string addUsers(HttpContext context)
        {
            //UserID, ParentID, UserName, LoginName, Password, UserType, , FirstName, , , , , , , , , , ,  , , , , , , , , , , 
            string ParentID = context.Request.Form["ParentUserID"];
            string UserName = context.Request.Form["UserName"];
            string LoginName = context.Request.Form["LoginName"];
            string Password = context.Request.Form["Password"];
            string UserType = context.Request.Form["UserType"];
            string Phone = context.Request.Form["Phone"];
            string Address = context.Request.Form["Address"];
            string Contacts = context.Request.Form["Contacts"];
            string strSql; 

            if (string.IsNullOrEmpty(ParentID) || string.IsNullOrEmpty(LoginName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(UserType) || string.IsNullOrEmpty(UserName))
            {
                return "{\"success\":false,\"msg\":\"请检查数据书否输入正确.\"}";
            }
            if (string.IsNullOrEmpty(Password) || Password.Length > 20)
            {
                return "{\"success\":false,\"msg\":\"密码不能为空且不能超过20个字符.\"}";
            }
            if (string.IsNullOrEmpty(LoginName) || LoginName.Length > 20)
            {
                return "{\"success\":false,\"msg\":\"密码不能为空且不能超过.\"}";
            }

            strSql = @" if exists(select u.UserID from Users u right join devices d on u.userid=d.userid where LoginName=@LoginName or d.SerialNumber=@LoginName)
                            begin
                                select -2 [state]
                            end
                         else
                            begin
                                insert into Users(ParentID, UserName, LoginName, Password, UserType, Gender, FirstName, MiddleName, LastName, TimeZone, Address1, Address2, Country, State, HomePhone, WorkPhone, CellPhone, SMSEmail, PrimaryEmail, SecondaryEmail, Status, UpdateTime, Created, Deleted, SuperAdmin, AllDeviceCount, ActivationCount, MoneyCount)
                                values(@ParentID, @UserName, @LoginName, @Password, @UserType, @Gender, @FirstName, @MiddleName, @LastName, @TimeZone, @Address1, @Address2, @Country, @State, @HomePhone, @WorkPhone, @CellPhone, @SMSEmail, @PrimaryEmail, @SecondaryEmail, @Status, @UpdateTime, @Created, @Deleted, @SuperAdmin, @AllDeviceCount, @ActivationCount, @MoneyCount)
                                select @@identity UserID
                            end";
            SqlParameter[] parms = new SqlParameter[] {
               new SqlParameter("@ParentID",ParentID),new SqlParameter("@UserName",UserName),new SqlParameter("LoginName",LoginName),new SqlParameter("@Password",Password),
               new SqlParameter("@UserType",UserType),new SqlParameter("@Gender","0"),new SqlParameter("@FirstName",Contacts),new SqlParameter("@MiddleName","") ,new SqlParameter("@LastName","") ,new SqlParameter("@TimeZone","China Standard Time") ,
               new SqlParameter("@Address1",Address), new SqlParameter("@Address2",""), new SqlParameter("@Country","-1"), new SqlParameter("@State","-1"), new SqlParameter("@HomePhone",""), new SqlParameter("@WorkPhone",""), new SqlParameter("@CellPhone",Phone),
               new SqlParameter("@SMSEmail",""), new SqlParameter("@PrimaryEmail",""), new SqlParameter("@SecondaryEmail",""), new SqlParameter("@Status","-1"), new SqlParameter("@UpdateTime", DateTime.Now), new SqlParameter("@Created",DateTime.Now), new SqlParameter("@Deleted","0"),
               new SqlParameter("@SuperAdmin","0"),  new SqlParameter("@AllDeviceCount","0"),  new SqlParameter("@ActivationCount","0"),  new SqlParameter("@MoneyCount","0")
            };
            int status = Convert.ToInt32(Dao.ExecuteScalar(strSql, parms));


            //strSql = string.Format("select COUNT(*) from Users u right join devices d on u.userid=d.userid where LoginName='{0}' or d.SerialNumber='{0}' ", LoginName);
            //DataRow dr = Dao.Select(strSql);
            //if (Convert.ToInt32(dr[0]) > 0)
            //{
            //    return "{\"success\":false,\"msg\":\"添加失败，登录名已存在！\"}";
            //}


            //strSql = "insert into Users values(@ParentID, @UserName, @LoginName, @Password, @UserType, @Gender, @FirstName, @MiddleName, @LastName, @TimeZone, @Address1, @Address2, @Country, @State, @HomePhone, @WorkPhone, @CellPhone, @SMSEmail, @PrimaryEmail, @SecondaryEmail, @Status, @UpdateTime, @Created, @Deleted, @SuperAdmin, @AllDeviceCount, @ActivationCount, @MoneyCount)";
            //int status = Dao.ExecutionSQL(strSql, parms);
            if (status > 0)
            {
                return "{\"success\":true,\"msg\":\"用户添加成功！\"}";
            }
            else if (status == -2)
            {
                return "{\"success\":false,\"msg\":\"登录账号已存在.\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"用户添加失败！\"}";
            }
        }

        /// <summary>
        /// 转移用户
        /// </summary>
        /// <param name="UserID">要转移的用户的ID</param>
        /// <param name="toUserID">目标用户的ＩＤ</param>
        /// <returns></returns>
        public static string ShiftUsers(String UserID,String toUserID)
        {
            List<Dictionary<string, object>> list = Dao.ToList(UserID);
            StringBuilder userids =  new StringBuilder ();
            for (int i = 0; i < list.Count; i++)
            {
                Dictionary<string, object> dic = list[i];
                userids.Append(dic["UserID"]+",");
            }
            string strSql = string.Format("update Users set ParentID = {0} where UserID in ({1})", toUserID, userids.ToString().Substring(0,userids.ToString().Length-1 ));
            int status = Dao.ExecutionSQL(strSql);
            if (status > 0)
            {
                return "{\"success\":true,\"msg\":\"用户转移成功！\"}";
            }
            else
            {
                return "{\"success\":false,\"msg\":\"用户转移失败！\"}";
            }
        }

        /// <summary>
        /// 查询设备指令发送记录
        /// </summary> 
        /// <param name="imei">设备的IMEI号</param>
        /// <returns></returns>
        public static DataTable GetCommandList(string DeviceID)
        {
            string strSql = string.Format("select ROW_NUMBER() OVER (ORDER BY sendDate desc) num,CommandName,DeviceID as IMEI,dateadd(HH,8 ,SendDate)sendDate,ResponseText,dateadd(HH,8 ,ResponseDate)ResponseDate,IsResponse,isSend from  dbo.CarCommandQueue where DeviceID=( select SerialNumber from devices where DeviceID={0})  ", DeviceID);
            return Dao.Selects(strSql); 
        }

         /// <summary>
         ///  获取已删除设备
         /// </summary>
         /// <param name="current">当前页</param>
         /// <param name="rowCount">一页显示多少行</param>
         /// <param name="sort">排序列</param>
         /// <param name="searchPhrase">搜索内容</param>
         /// <returns></returns>
        public static String GetDeletedDevices(string current, string rowCount, string sort, string searchPhrase)
        {
            //http://localhost:1619/AjaxService/AjaxService.ashx?action=test&current=10&rowCount=5&sort%5Breceived%5D=desc&searchPhrase=
            string where = "",sortStr="";
            if (searchPhrase.Trim() != "")
            {
                where = " and ( DeviceName like '%" + searchPhrase + "%' or SerialNumber like '%" + searchPhrase + "%' or UserName like '%" + searchPhrase + "%')";
            }
            if (!string.IsNullOrWhiteSpace(sort))
            {
                sortStr = " order by " + sort;
               sortStr= sortStr.Replace("rowid", "DeviceID");
            }
            string strSql = @"select top " + rowCount + " * from (select ROW_NUMBER() over(" + sortStr + ") rowid,d.DeviceID,d.DeviceName,d.SerialNumber,u.UserID,u.UserName  from Devices d inner join Users u on d.userid=u.userid where d.deleted=1 " + where + @"
                        ) 已删除 where rowid>(" + rowCount + "*(" + current + @"-1))" + sortStr;
            DataTable dt = Dao.Selects(strSql); 
            StringBuilder jsonStr = new StringBuilder(); 
            foreach (DataRow dr in dt.Rows)
            {
                  jsonStr.Append("{\"rowid\":\"" + dr["rowid"] + "\",\"DeviceID\":\"" + dr["DeviceID"] + "\",\"DeviceName\":\"" + (dr["DeviceName"].ToString() == "" ? dr["SerialNumber"].ToString() : dr["DeviceName"]) + "\",\"SerialNumber\":\"" + dr["SerialNumber"] + "\",\"UserID\":\"" + dr["UserID"] + "\",\"UserName\":\"" + dr["UserName"] + "\"},"); 
            }
            if (jsonStr.Length>0) {
                jsonStr.Remove(jsonStr.Length - 1, 1);
            }
           
            strSql = "select count(*) from Devices d inner join Users u on d.userid=u.userid where d.deleted=1 " + where;
            string Count = Dao.Select(strSql)[0].ToString();
            StringBuilder sb = new StringBuilder();

            sb.Append("{");
            sb.Append("\"current\": " + current + " ,");
            sb.Append("\"rows\": [");
            sb.Append(jsonStr);
            sb.Append("],");
            sb.Append(" \"total\": " + Count + "");
            sb.Append("}"); 
            return sb.ToString();
        }

        public static String GetRenewalsExport(String UserID, String StartTime, String EndTime, int current, string rowCount, string sort, string searchPhrase)
        {
            #region MyRegion


            //  string where = "", sortStr = "";
            //  if (UserID == "")
            //      UserID = Utils.GetSession().UserID;
            //  if (searchPhrase.Trim() != "")
            //  {
            //      where = " and ( DeviceName like '%" + searchPhrase + "%' or d.SerialNumber like '%" + searchPhrase + "%' or UserName like '%" + searchPhrase + "%')";
            //  }
            //  if (!string.IsNullOrWhiteSpace(sort))
            //  {
            //      sortStr = " order by " + sort;
            //      sortStr = sortStr.Replace("rowid", "d.DeviceID");
            //  }
            //  string strSql = string.Format(@"with subqry(UserID) as (select UserID from Users where UserID = {0} union all select Users.UserID from Users,subqry
            //                        where Users.ParentID = subqry.UserID and users.Deleted = 0)   
            //    	select top {1} * from(select row_number() over({5}) rowid,d.DeviceID, u.UserID, u.UserName, d.ActiveDate, d.devicename
            //                        , d.SerialNumber, d.HireExpireDate, phonenum, l.lastcommunication 
            //                         from devices d inner
            //                         join users u on u.userid = d.userid inner
            //                         join lklocation l  on l.deviceid = d.deviceid 
            //                          where d.deleted = 0 and d.ActiveDate >= '{2}' and d.ActiveDate <= '{3}' and d.userid in (select Userid from subqry)" + where + @"
            //) d where d.rowid > ({1} * ({4} - 1)) {5}", UserID, rowCount, StartTime, EndTime, current, sortStr);
            //  DataTable dt = Dao.Selects(strSql);
            //  StringBuilder jsonStr = new StringBuilder();
            //  foreach (DataRow dr in dt.Rows)
            //  {
            //      jsonStr.Append("{\"rowid\":\"" + dr["rowid"] + "\",\"DeviceID\":\"" + dr["DeviceID"] + "\",\"DeviceName\":\"" + (dr["DeviceName"].ToString() == "" ? dr["SerialNumber"].ToString() : dr["DeviceName"]) + "\"," +
            //                     "\"SerialNumber\":\"" + dr["SerialNumber"] + "\",\"UserID\":\"" + dr["UserID"] + "\",\"UserName\":\"" + dr["UserName"] + "\",\"ActiveDate\":\"" + dr["ActiveDate"] + "\",\"PhoneNum\":\"" + dr["phonenum"] + "\",\"LastCommunication\":\"" + dr["lastcommunication"] + "\"},");
            //  }
            //  if (jsonStr.Length > 0)
            //      jsonStr.Remove(jsonStr.Length - 1, 1);
            //  strSql = string.Format(@" with subqry(UserID) as (select UserID from Users where UserID = {0} union all select Users.UserID from Users,subqry
            //                         where Users.ParentID = subqry.UserID and users.Deleted = 0)   
            //       select count(*) from devices d inner
            //                         join users u on u.userid = d.userid inner
            //                         join lklocation l  on l.deviceid = d.deviceid
            //                         where d.deleted = 0 and d.ActiveDate >= '{1}' and d.ActiveDate <= '{2}' and d.userid in (select Userid from subqry) {3}", UserID, StartTime, EndTime, where);
            //  string Count = Dao.Select(strSql)[0].ToString();
            //  StringBuilder sb = new StringBuilder();

            //  sb.Append("{");
            //  sb.Append("\"current\": " + current + " ,");
            //  sb.Append("\"rows\": [");
            //  sb.Append(jsonStr);
            //  sb.Append("],");
            //  sb.Append(" \"total\": " + Count + "");
            //  sb.Append("}");
            //  return sb.ToString();
            #endregion
            #region task


            string where = "", sortStr = "";
            if (string.IsNullOrEmpty(UserID))
                UserID = Utils.GetSession().UserID;
            if (searchPhrase.Trim() != "")
            {
                where = " and ( DeviceName like '%" + searchPhrase + "%' or d.SerialNumber like '%" + searchPhrase + "%' or UserName like '%" + searchPhrase + "%')";
            }
            if (!string.IsNullOrWhiteSpace(sort))
            {
                sortStr = " order by " + sort;
                sortStr = sortStr.Replace("rowid", "d.DeviceID");
            }
            Task<List<Dictionary<string, string>>> task1 = Task.Factory.StartNew(() =>
            {
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                string strSql = string.Format(@"with subqry(UserID) as (select UserID from Users where UserID = {0} union all select Users.UserID from Users,subqry
                                    where Users.ParentID = subqry.UserID and users.Deleted = 0)   
                	select top {1} * from(select row_number() over({5}) rowid,d.DeviceID, u.UserID, u.UserName, d.ActiveDate, d.devicename
                                    , d.SerialNumber, d.HireExpireDate, PhoneNum, l.lastcommunication 
                                     from devices d inner
                                     join users u on u.userid = d.userid inner
                                     join lklocation l  on l.deviceid = d.deviceid 
                                      where d.deleted = 0 and d.HireExpireDate >= '{2}' and d.HireExpireDate <= '{3}' and d.userid in (select Userid from subqry)" + where + @"
            ) d where d.rowid > ({1} * ({4} - 1)) {5}", UserID, rowCount, StartTime, EndTime, current, sortStr);
                DataTable dt = Dao.Selects(strSql);
                foreach (DataRow dr in dt.Rows)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic["rowid"] = dr["rowid"].ToString();
                    dic["DeviceID"] = dr["DeviceID"].ToString();
                    dic["DeviceName"] = dr["DeviceName"].ToString();
                    dic["SerialNumber"] = dr["SerialNumber"].ToString();
                    dic["UserID"] = dr["UserID"].ToString();
                    dic["UserName"] = dr["UserName"].ToString();
                    dic["ActiveDate"] = dr["ActiveDate"].ToString();
                    dic["PhoneNum"] = dr["PhoneNum"].ToString();
                    dic["LastCommunication"] = dr["LastCommunication"].ToString();
                    dic["HireExpireDate"] = dr["HireExpireDate"].ToString();
                    list.Add(dic);
                }
                return list;
            });
             
            Task<int> task2 = Task.Factory.StartNew<int>(() =>
           { 
                  string strSql = string.Format(@" with subqry(UserID) as (select UserID from Users where UserID = {0} union all select Users.UserID from Users,subqry
                                     where Users.ParentID = subqry.UserID and users.Deleted = 0)   
                   select count(*) from devices d inner
                                     join users u on u.userid = d.userid inner
                                     join lklocation l  on l.deviceid = d.deviceid
                                     where d.deleted = 0 and d.ActiveDate >= '{1}' and d.ActiveDate <= '{2}' and d.userid in (select Userid from subqry) {3}", UserID, StartTime, EndTime, where);
               return Convert.ToInt32(Dao.Select(strSql)[0].ToString());
           
              });
            
            List<Dictionary<string, string>> resList = task1.Result;
            int resCount = task2.Result;
            Dictionary<string, object> resDic = new Dictionary<string, object>();
            resDic["current"] = current;
            resDic["total"] = resCount;
            resDic["rows"] = resList;
          
            var res = new
            {
                current = current,
                total = resCount,
                rows = resList
            };
            
            return   Dao.ToJson(resDic);
            #endregion
        }

        /// <summary>
        /// 获取设备数为0的分组列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static DataTable GetGroups(String UserID)
        {
            string strSql = string.Format("select GroupID,GroupName,UserID from  Groups where UserID ={0}", UserID);
            return Dao.Selects(strSql);
        }

        /// <summary>
        /// 根据设备ID查询一个时间段内的报警数
        /// </summary>
        /// <param name="DeviceID"></param>
        /// <returns></returns>
        public static DataTable GetExceptionMessageCount(String DeviceID,string sTime,string eTime)
        {
            string strSql = string.Format("select CONVERT(varchar(10), Created, 120) msgDate,COUNT(*) msgCount from ExceptionMessage where DeviceID={0} and Created >= '{1}' and Created <= '{2}' group by CONVERT(varchar(10), Created, 120) ", DeviceID,sTime,eTime);
            return Dao.Selects(strSql);
        }

        /// <summary>
        /// 清空设备数据（报警、历史轨迹等）
        /// </summary>
        /// <param name="DeviceID"></param>
        /// <returns></returns>
        public static void ClearHistoryData(String DeviceID)
        {
            DataRow drData = Dao.Select("select ActiveDate,SerialNumber from devices where DeviceID = " + DeviceID);
            if (drData["ActiveDate"].ToString() == "1900-01-01 00:00:00.000")
            {
                return;
            }
            DateTime activeDate = Convert.ToDateTime(drData["ActiveDate"]);
            DateTime spDate = Convert.ToDateTime("2015-11-04");
            string strSql = string.Format(@" delete from CarCommandQueue where DeviceID='{0}';
                                             delete from LKLocation where DeviceID={1};
                                             delete from ExceptionMessage where deviceid={1};
                                             delete from DevicesReport where DeviceID={1};
                                             delete from SpeedReport where DeviceID={1};
                                             if exists(select * from sysobjects where id= object_id('ZLocation_{1}')and type='U') drop table ZLocation_{1}; ", drData["SerialNumber"].ToString(), DeviceID);
            Dao.ExecutionSQL(strSql); 
            if (activeDate >= spDate)
            {
                double day = (DateTime.Now - activeDate).TotalDays +1 ;
                SqlConnection conn = Dao.CreateConn(activeDate);
                for (int i = 0; i < day; i++)
                {
                    if (i > 0 && activeDate.AddDays(i).Month != activeDate.AddDays(i - 1).Month) 
                        conn = Dao.CreateConn(activeDate.AddDays(i)); 
                    strSql = string.Format("delete from Location{0} where DeviceID={1}",activeDate.AddDays(i).Day, DeviceID);
                    Dao.ExecutionSQL(strSql, conn);
                }
            } 
        }

        public static string IsExistFence(string DeviceID  )
        {
            try
            {
                if (string.IsNullOrEmpty(DeviceID))
                {
                    return "{\"success\":false,\"msg\":\"参数错误！\"}";
                }
                string strSql = "  select Radius from GeoFence where FenceName='一键围栏' and  DeviceID=@DeviceID";
                Hashtable ht = Dao.Select(strSql, new SqlParameter[] { new SqlParameter("DeviceID", DeviceID) });
                if (ht!= null && ht.Count >0)
                {
                    string radius = Convert.ToDouble(ht["Radius"]).ToString("0");  
                    return "{\"success\":true,\"msg\":\""+ radius + "\"}"; 
                } 
            }
            catch (Exception ex)
            {
                Utils.log("IsExistFence Error:"+ex.Message);
            }
            return "{\"success\":false,\"msg\":\"\"}";
        }

        public static bool DeleteGeoFence(string FenceID)
        {
            try
            {
                string strSql = "delete from GeoFence where GeofenceID=@GeofenceID ";
              
                return Dao.ExecutionSQL(strSql, new SqlParameter[] { new SqlParameter("GeofenceID", FenceID)  }) > 0;
              
            }
            catch (Exception ex)
            {
                Utils.log("DeleteGeoFence Error:"+ex.Message);
                return false;
            }
        }

        public static bool AddGeoFence(string FenceName, string UserID, string Deviceid, string Latitude, string Longitude, string Radius, string Description)
        {
            try
            {
                if (string.IsNullOrEmpty(FenceName) || string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(Deviceid) ||
                    string.IsNullOrEmpty(Latitude) || string.IsNullOrEmpty(Longitude) || string.IsNullOrEmpty(Radius))
                {
                    return false;
                }
                string strSql = @"insert into GeoFence(FenceName, Latitude, Longitude, [Entry], [Exit], Radius, IsInclusion, Created, Deleted, Lat1, Lng1, FenceType, Width, UserID, DeviceID, [Description])
                              values(@FenceName, @Latitude, @Longitude, 0, 0, @Radius, -1, GETDATE(), 0, -1.00000000000000000000, -1.00000000000000000000, 0, -1, 
                             @UserID, @Deviceid, @Description) select @@IDENTITY as gid ";

                string gid = Dao.Select(strSql, new SqlParameter[] {
                    new SqlParameter("FenceName", FenceName),
                    new SqlParameter("Latitude",Latitude),
                    new SqlParameter("Longitude",Longitude),
                    new SqlParameter ("UserID",UserID),
                    new SqlParameter ("Deviceid",Deviceid),
                    new SqlParameter ("Radius",Radius),
                    new SqlParameter ("Description",Description),
                })["gid"].ToString();
                if (!gid.Equals(string.Empty))
                {
                    Task.Run(() => MG_BLL.Utils.SendTcpCmd("VTR-DZWL-" + Deviceid)  ); 
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Utils.log("AddGeoFence Error:" + ex.Message);
                return false;
            }
        }
    } 
}
