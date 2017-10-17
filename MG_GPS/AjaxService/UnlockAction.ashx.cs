using MG_BLL;
using MG_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MG_GPS.AjaxService
{
    /// <summary>
    /// UnlockAction 的摘要说明
    /// </summary>
    public class UnlockAction : IHttpHandler
    { 
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            var req = context.Request;
            var res = context.Response;
            var action = req.QueryString["action"];
            switch (action.ToLower())
            {
                case "gettracking":
                    int DeviceID = Convert.ToInt32(req.Params["DeviceID"]);
                    res.Write(GetDeviceTracking(DeviceID));
                    break;
                case "getmessagelist":
                    DeviceID = Convert.ToInt32(req.Params["DeviceID"]);
                    res.Write(GetMessageList(req.Params["currentIndex"], req.Params["pagecount"], DeviceID, req.Params["type"]));
                    break;
                case "unlock":
                    DeviceID = Convert.ToInt32(req.Params["DeviceID"]);
                    res.Write(SendUnlockCommand(DeviceID,req.Params["password"]));
                    break;
                case "unlocklist":
                    DeviceID = Convert.ToInt32(req.Params["DeviceID"]);
                    res.Write(UnlockList(DeviceID));
                    break;
                case "getdeviceinfo":
                    DeviceID = Convert.ToInt32(req.Params["DeviceID"]);
                    res.Write( GetDeviceInfo(DeviceID));
                    break;
                case "setdeviceinfo":
                    DeviceID = Convert.ToInt32(req.Params["DeviceID"]);
                    res.Write(SetDeviceInfo(DeviceID, req.Params["devicename"], req.Params["cellphone"], req.Params["carusername"]));
                    break;
                case "addfence":
                    DeviceID = Convert.ToInt32(req.Params["DeviceID"]);
                    var Radius = Convert.ToInt32(req.Params["Radius"]);
                    res.Write( AddFence(DeviceID, Radius ));
                    break;
                case "closefence":
                    DeviceID = Convert.ToInt32(req.Params["DeviceID"]);
                    var gid = Convert.ToInt32(req.Params["FenceID"]);
                    res.Write(DeleteFence(DeviceID,gid));
                    break;
                case "gethistory":
                    DeviceID = Convert.ToInt32(req.Params["DeviceID"]);
                    res.Write(GetHistory(DeviceID,req.Params["starttime"],req.Params["endtime"]));
                    break;
                default:
                    break;
            }
        }
        private string GetDeviceTracking(int DeviceID)
        {
            MG_DAL.SQLServerOperating sqlHelper = new MG_DAL.SQLServerOperating();
            string strSql = @"select d.DeviceID,d.SerialNumber,DeviceName,l.OLat,l.OLng,l.LastCommunication DeviceDate,l.Speed,l.Course,l.DataContext ,g.Radius,g.Latitude,g.LongItude,g.GeofenceID
                from devices d inner join LKLocation l on l.DeviceID=d.DeviceiD left join GeoFence g on g.DeviceID=d.DeviceID where d.DeviceID =@DeviceID and d.deleted=0";// model=213 and 

            Dictionary<string, string> dic = sqlHelper.Selects(strSql, new SqlParameter[] { new SqlParameter("DeviceID", DeviceID) }).toDictionary();

            Geocoding geo = new Amap();
            Gps gps = geo.Translate(dic["OLat"], dic["OLng"]);
            //Gps gps = Utils.gps84_To_Gcj02(dic["OLat"], dic["OLng"]);
            dic["OLat"] = gps.getWgLat().ToString();
            dic["OLng"] = gps.getWgLon().ToString();
            dic["Address"] = gps.Address;
            gps = geo.Translate(dic["Latitude"], dic["LongItude"],false);
            dic["Latitude"] = gps.getWgLat().ToString();
            dic["LongItude"] = gps.getWgLon().ToString();
           
            dic["CourseName"] = Utils.GetCoureName(dic["Course"]);
            var dc = dic["DataContext"];
            var doorStatus = "未知";
            if (dc.Split('-').Length > 3 && !string.IsNullOrEmpty(dc.Split('-')[3]))
            {
                dc = dc.Split('-')[3];
                if (dc.Equals("0"))
                {
                    doorStatus = "打开";
                }
                else
                {
                    doorStatus = "关闭";
                }
            }
            dic["DataContext"] = doorStatus;// = dic["DataContext"].Split('-')[3]; //0--- 主电断开, 1-----主电连接
            return Utils.ToJson(dic);
        }
         
        private string GetMessageList(string currentIndex, string pageCount, int deviceid, string type)
        {
            var list = new List< Dictionary < string, string>> ();
            try
            {
                if (string.IsNullOrEmpty(type))
                {
                    type = "2";
                }
                string where = "";
                if (type.Equals("0")) //查看未删除的
                {
                    where = " and e.Deleted = 0 ";
                }
                else if (type.Equals("1")) //查看已删除的
                {
                    where = " and e.Deleted = 1 ";
                }
                else if (type.Equals("2"))//查看全部报警信息
                { }
                else
                {
                    return Utils.ToJson( list);
                }
                string strSql = string.Format(@"select top {0} * from(
                             select row_number() over(order by e.[Created] desc) rowIndex,
                             e.[ExceptionID], case when geo.FenceName is null then e.Message else e.Message+':'+geo.FenceName end Message , dateadd(HH,8, e.[Created])[Created], d.DeviceName, d.SerialNumber, e.ClearDate, e.ClearBy 
                             from ExceptionMessage e inner join Devices d on e.DeviceID = d.DeviceID
                             left join GeoFence geo on geo.GeofenceID=e.GeoFenceID 
                             where d.deleted =0 and d.DeviceID=@DeviceID " + where + @"
	                         ) t where rowIndex > {0} * ({1} - 1) order by  Created desc", pageCount, currentIndex);
                SQLServerOperating s = new SQLServerOperating();
                return Utils.ToJson(s.Selects(strSql, new SqlParameter[] { new SqlParameter("DeviceID", deviceid) }).toListDictionary());
            }
            catch (Exception ex)
            {
               // Utils.log("GetMessageList Error:" + ex.Message);
                return Utils.ToJson(list);
            }
        }

        private string SendUnlockCommand(int DeviceID,string Password)
        {

            ajaxResult ar = new ajaxResult();
            ar.Result = "3";
     
            if (!string.IsNullOrEmpty(Password) && DeviceID > 0 && Password.Length > 0 && Password.Length < 10)
            {
                string strSql = "select DevicePassword,SerialNumber from devices where deviceid=@DeviceID and deleted=0";
                MG_DAL.SQLServerOperating sqlHelper = new MG_DAL.SQLServerOperating();
                var dic = sqlHelper.Selects(strSql, new SqlParameter[] { new SqlParameter("DeviceID", DeviceID) }).toDictionary();
                if (dic["DevicePassword"].Equals(Password))
                {
                    string imei = dic["SerialNumber"];

                    List<Task> listTask = new List<Task>();
                    string cmdid = "0";
                    Task task = Task.Factory.StartNew(() =>
                    {
                        strSql = @" insert into CarCommandQueue (  DeviceID, CommandText, CreateDate, IsSend, SendDate, IsResponse, ResponseDate, ResponseText, CommandName, IsOfflineSend, Infos, SendCount)
 values(@SerialNumber, 'KM', getdate(), 0, getdate(), 0, '1900-01-01 00:00:00.000', '', '指令开锁', 0, '', 0) select @@IDENTITY CommandID";
                        cmdid = sqlHelper.Select(strSql, new SqlParameter[] { new SqlParameter("SerialNumber", imei) });
                    });
                    listTask.Add(task);

                    task = Task.Factory.StartNew(() =>
                    {
                        ar.Result = Utils.SendTcpCmd("VTR-Command-" + dic["SerialNumber"] + "-KM");
                    });
                    listTask.Add(task);
                    Task.WaitAll(listTask.ToArray());
                    if (ar.Result.Equals("1"))
                    {
                        
                        Task.Factory.StartNew(() => {
                            strSql = " update CarCommandQueue set IsResponse=1 ,ResponseDate=getdate(),ResponseText='Success!' where id="+ cmdid;
                            sqlHelper.ExecuteSql(strSql);
                        });
                    }
                    // status = Utils.SendTcpCmd("VTR-Command-" + SerialNumber + "-DY");
                    // if (status .Equals("1"))
                    //{ 
                    //Task.Run(() => {
                    //    System.Threading.Thread.Sleep(3000);
                    //    var cmdRes = Utils.SendTcpCmd("VTR-Command-" + SerialNumber + "-TY");
                    //    while (cmdRes!= "1")
                    //    {
                    //        System.Threading.Thread.Sleep(3000);
                    //        cmdRes = Utils.SendTcpCmd("VTR-Command-" + SerialNumber + "-TY");
                    //    }
                    //});
                    //  }
                }
                else
                {
                    ar.Result = "2";
                }
            }
            return Utils.ToJson(ar); 
        }

        private string UnlockList(int DeviceID)
        {
            string strSql = @" select row_number() over(order by CreateDate desc) rowIndex,DeviceID SerialNumber,CreateDate,IsResponse,CommandName from  CarCommandQueue
                                where deviceid = (select SerialNumber from devices where deviceid = @deviceid )";
            SQLServerOperating sqlHelper = new SQLServerOperating();
            var list= sqlHelper.Selects(strSql,new SqlParameter[] { new SqlParameter("deviceid", DeviceID)}).toListDictionary();
            return Utils.ToJson(list);
        }

        private string GetDeviceInfo(int DeviceID)
        {
            string strSql = @"select d.DeviceID, SerialNumber, DeviceName,CarUserName,PhoneNum, CarNum, CellPhone,ActiveDate,HireExpireDate,di.DataText  
from Devices d inner join Dictionary di on d.Model = di.DataValue where d.Deleted = 0 and d.DeviceID = @DeviceID ";
            SQLServerOperating sqlHelper = new SQLServerOperating();
            return Utils.ToJson( sqlHelper.Selects(strSql,new SqlParameter[] { new SqlParameter("DeviceID", DeviceID)}).toDictionary());
        }
        private string SetDeviceInfo(int DeviceID,string DeviceName,string Cellphone, string Contact)
        {
            try
            {
                string strSql = "update devices set devicename=@devicename,cellphone=@cellphone,carusername=@carusername where Deleted=0 and DeviceID = @DeviceID";
                SQLServerOperating sqlHelper = new SQLServerOperating();
                int state = sqlHelper.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("DeviceID", DeviceID), new SqlParameter("devicename", DeviceName), new SqlParameter("cellphone", Cellphone), new SqlParameter("carusername", Contact) });
                if (state > 0)
                {
                    return Utils.GetResult("保存成功.", statusCode.Code.success);
                }
                else
                {
                    return Utils.GetResult("保存失败.", statusCode.Code.failure);
                }
            }
            catch (Exception e)
            {
                return Utils.GetResult(e.Message, statusCode.Code.error);
            }
         
        }

        private string AddFence(int DeviceID, int Radius)
        {
            try
            {
                //添加围栏前，先把这个设备的围栏删除，一台设备只有一个围栏
                string strSql = @"delete from [GeoFence] where DeviceID=75539;
                            insert into GeoFence(FenceName, Latitude, Longitude, [Entry], [Exit], Radius, IsInclusion, Created, Deleted, Lat1, Lng1, FenceType, Width, UserID, DeviceID, [Description])
                            select @FenceName,l.OLat,l.OLng,0, 0,@Radius, -1, GETDATE(), 0, -1.00000000000000000000, -1.00000000000000000000, 0, -1,UserID, d.DeviceID, '' 
                            from Devices d inner join Lklocation l on l.DeviceID=d.DeviceID where d.DeviceID=@DeviceID
                            select @@IDENTITY as gid";

                SQLServerOperating sqlHelper = new SQLServerOperating();
                var pars = new SqlParameter[] {
                    new SqlParameter("DeviceID", DeviceID),
                    new SqlParameter("FenceName","一键围栏-"+DateTime.Now.ToString("yyyyMMdd")),
                    new SqlParameter("Radius", Radius)
                };
                int state = Convert.ToInt32( sqlHelper.Select(strSql, pars));
                if (state > 0)
                {
                    return Utils.GetResult("围栏已开启.", statusCode.Code.success, state.ToString());
                }
                else
                {
                    return Utils.GetResult("围栏开启失败.", statusCode.Code.failure);
                }
            }
            catch (Exception ex)
            {
                return Utils.GetResult(ex.Message, statusCode.Code.failure);
            }
        }

        private string DeleteFence(int DeviceID,int GeoFenceID)
        {
            try
            {
                string strSql = "delete from [GeoFence] where DeviceID=@DeviceID and GeofenceID=@GeofenceID;";
                SQLServerOperating sqlHelper = new SQLServerOperating();
                int state = sqlHelper.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("DeviceID", DeviceID), new SqlParameter("GeofenceID", GeoFenceID) });
                if (state > 0)
                {
                    return Utils.GetResult("围栏已关闭.", statusCode.Code.success);
                }
                else
                {
                    return Utils.GetResult("围栏关闭失败.", statusCode.Code.failure);
                }
            }
            catch (Exception ex) 
            {
                return Utils.GetResult(ex.Message, statusCode.Code.error);
            }
            
        }

        private string GetHistory(int deviceid, string startdate, string enddate)
        {
            var list = new List<Dictionary<string, string>>();
            string speedfilter = "2";
            if (string.IsNullOrEmpty(deviceid.ToString()) || string.IsNullOrEmpty(startdate) || string.IsNullOrEmpty(enddate))
            {
                return Utils.ToJson(list);
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
                    return Utils.ToJson(list);
                }
                TimeSpan ts = endTime - startTime;
                double days = Math.Ceiling(ts.TotalDays);
                //一次最多只能看5天的数据
                if (days > 5)
                {
                    return Utils.ToJson(list);
                }
                string DataBaseBefore = "YWData";// ConfigurationManager.AppSettings["DataBaseName"].ToStringEmpty();
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
                    if (i != days - 1)
                    {
                        strSql.Append(" union all ");
                    }
                    startTime = startTime.AddDays(1);
                }
                strSql.Append(" )t order by DeviceTime");
                SQLServerOperating s = new SQLServerOperating();
                string model = s.Select("select di.DataText from Devices d inner join Dictionary di on di.DataValue=d.Model where DeviceID=@DeviceID", new SqlParameter[] { new SqlParameter("DeviceID", deviceid) });
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
                //List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                Geocoding geo = new Amap();
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dic[dc.ColumnName] = row[dc.ColumnName].toStringEmpty();
                    }
                    Gps gps = geo.Translate(dic["OLat"], dic["OLng"], false);
                    var listWhere = list.Where(l =>  l.ContainsValue(gps.getWgLat().ToString()) && l.ContainsValue(gps.getWgLon().ToString()));
                    if (listWhere.Count() > 0 )
                    {
                        continue;
                    }
                    dic["OLat"] = gps.getWgLat().toStringEmpty();
                    dic["OLng"] = gps.getWgLon().toStringEmpty();
                    list.Add(dic);
                }
                return Utils.ToJson(list); 
            }
            catch (Exception ex)
            {
                Utils.log("GetHistoryLocus Error2:" + ex.Message + ",堆栈信息:" + ex.StackTrace + "," + deviceid + "-" + startdate + "-" + enddate);
                return Utils.ToJson(list);
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