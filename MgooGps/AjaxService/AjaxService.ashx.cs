using System;
using System.Collections.Generic;
using System.Web;
using MgooGps.com;
using System.Web.SessionState;
using System.Web.Services;
using System.IO;
using System.Data;

namespace MgooGps
{
    /// <summary>
    /// AjaxService 的摘要说明
    /// </summary>
    public class AjaxService : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        { 
            context.Response.ContentType = "text/plain";
            if (context.Request.Params["action"] == null || context.Request.Params["action"].Trim() == "")
                context.Response.Redirect("~/login.aspx");
            HttpRequest request = context.Request;
            switch (request.Params["action"].ToLower())
            {
                default:
                    context.Response.Redirect("~/login.aspx");
                    break; 
                case "login":
                    context.Response.Write(com.Utils.login(request.Form["loginname"], request.Form["pwd"], request.Form["language"]));
                    break;
                //case "getmyteamlist":
                //    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetMyTeamList()));
                //    break;
                case "gettree":
                        context.Response.Write(Dao.DataTableToJSON(MyTeam.GetMyTeamList(request.Form["UserType"])));
                    break;
                case "getdevicesbyuserid":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.getDevicesList(request.Form["UserID"], request.Form["imei"],request.Form["LowerDevice"])));
                    break;
                //case "getdevicesbyserialnumber":
                //    context.Response.Write(Dao.DataTableToJSON(MyTeam.getDeviceBySerialNumber(request.Form["SerialNumber"])));
                //    break;
                case "tracking": //实时跟踪
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.Tracking(request.Form["deviceid"], request.Form["userid"])));
                    break;
                case "addgeofences":
                    context.Response.Write(MyTeam.addGeofences(request.Form["pointData"], request.Form["name"], request.Form["userid"], request.Form["deviceid"],request.Form["south_west"],request.Form["north_east"],request.Form["type"]));
                    break;
                case "getgeofencepolygon": 
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetPolygonDetail(request.Params["GID"])));
                    break;
                case "getdeviceinfo":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.getDeviceByDeviceID(request.Form["DeviceID"])));
                    break;
                case "deletegeofencesbyid":
                    context.Response.Write(MyTeam.DeleteGeofences(request.Form["GeofenceID"]));
                    break;
                case "getplay":
                    String call = request.Params["callback"];
                    String callBack = call + "(\"" + MyTeam.getPlayBack(request.Params["deviceid"], request.Params["startDate"], request.Params["endDate"]) + "\")";
                    context.Response.Write(callBack);
                    break;
                case "getlushu":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.getPlayBack(request.Form["deviceid"], request.Form["startDate"], request.Form["endDate"], "")));
                    break;
                case "alarmtime": //定时刷新报警信息
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.alarmTime(request.Form["userid"], request.Form["lower"])));
                    break;
                case "alarm": // 报警信息列表
                    context.Response.Write(com.Dao.DataTableToJSON(MyTeam.getAlarmList(request.Form["userid"], request.Form["lower"], request.Form["status"])));
                    break;
                case "allreadonly":
                    context.Response.Write(com.MyTeam.AlarmAllReadonly(request.Form["userid"], request.Form["ExceptionID"]));
                    break;
                case "remainview":   //运行总览
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetRemainView(request.Form["u"], request.Form["st"], request.Form["et"])));
                    break;
                case "get_statistics_alarmlist": //统计报表》报警消息 
                    string UserID = request.Form["userid"];
                    string Model = request.Form["model"];
                    string MsgType = request.Form["msgtype"];
                    string StartTime = request.Form["starttime"];
                    string EndTime = request.Form["endtime"];
                    context.Response.Write(Dao.DataTableToJSON(com.MyTeam.GetMessageList(UserID, Model, MsgType, StartTime, EndTime)));
                    break;
                case "getdevices":
                    context.Response.Write(MyTeam.GetDevicesList(request.Form["u"]));
                    break;
                case "searchuserinfo":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.SearchUserInfo(request.Form["userid"])));
                    break;
                case "savedeviceuserinfo":
                    context.Response.Write(MyTeam.UpdateDeviceUserInfo(request.Form["data"]));
                    break;
                case "isplay": //历史轨迹
                    MyTeam.SetIsPlay(request.Form["play"]);
                    context.Response.Write("success");
                    break;
                case "isexistfence":
                    context.Response.Write(MyTeam.IsExistFence(request.Form["deviceid"]));
                    break;
                case "lowermsg":
                    MyTeam.setLowerMsg(request.Form["LowerMsg"]);
                    context.Response.Write("success");
                    break;
                //case "getdevicesdetailbydeviceid":
                //    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetDevicesDetailByDeviceID(request.Form["d"])));
                //    break;
                case "getmileage":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetMileage(request.Form["yh"], request.Form["dn"], request.Form["st"], request.Form["et"])));
                    break;
                case "getspeedreport": //停留列表，查询SpeedReport表的数据
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetSpeedReport(request.Form["st"], request.Form["et"], request.Form["dn"])));
                    break;
                case "getspeedreportecharts": // 停留折线图，查询历史轨迹数据，然后算出停留点
                    #region 停留详单
                    MyTeam myteam = new MyTeam();
                    string deviceID = "";
                    DataTable dt = MyTeam.GetSpeedReport(request.Form["st"], request.Form["et"], request.Form["dn"],ref deviceID, request.Form["filter"] , " and Speed >= 10 order by DeviceTime");
                    if (string.IsNullOrWhiteSpace(request.Form["et"])) // 结束时间为空就是查询停留折线图数据。
                    { 
                        context.Response.Write(Dao.DataTableToJSON(dt));
                        break;
                    }
                    else
                    {
                        context.Response.Write(Dao.DataTableToJSON(dt));
                    }
                    #endregion 
                    break;
                case "getstopdetail"://停留详单
                    context.Response.Write(Dao.ToJson( MyTeam.GetStopDetail(request.Form["userid"], request.Form["deviceid"],request.Form["starttime"],request.Form["endtime"],request.Form["stopday"],request.Form["wireless"],request.Form["laststop"] )));
                    break;
                case "getexception":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetExceptionAll(request.Form["st"], request.Form["et"], request.Form["dn"])));
                    break;
                case "exceptioncount":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetExceptionView(request.Form["st"], request.Form["et"], request.Form["dn"])));
                    break;
                case "getexceptiontype":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetExceptionType()));
                    break;
                case "getexctptiondetail":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetExctptionDetail(request.Form["st"], request.Form["et"], request.Form["t"], request.Form["dn"])));
                    break;
                case "getfencesview":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetFencesView(request.Form["st"], request.Form["et"], request.Form["dn"])));
                    break;
                case "getcurrentuserdevicesname":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetCurrentUserDeviceName(request.Form["userid"])));
                    break;
                case "getuserinfobyuserid":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetUserInfoByUserID(request.Form["UserID"])));
                    break;
                case "updateuserinfo":
                    context.Response.Write(MyTeam.UpdateUserInfo(request.Form["data"]));
                    break;
                case "importexcel": //导出excel
                    //context.Response.Write(MyTeam.ImportExcel(request.Form["type"], request.Form["st"], request.Form["et"], request.Form["dn"], request.Form["emtype"], request.Form["yh"], request.Form["userid"]));
                    context.Response.Write(MyTeam.ImportExcel(context));
                    break;
                case "alldevicemodel":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetDeviceModel()));
                    break;
                case "getuserdeviceinfo": //设备统计
                    context.Response.Write(MyTeam.GetUserDeviceInfo(request.Params["model"], request.Params["st"], request.Params["et"]));
                    break;
                case "getonlinedevicelist": //离线设备统计
                    context.Response.Write(Dao.ToJson( MyTeam.GetOnlineDeviceList(request.Form["userid"],request.Form["starttime"],request.Form["endtime"],request.Form["hour"])));
                    break;
                case "getdevicehireexpire": 
                    context.Response.Write(MyTeam.GetDeviceHireExpire(request.Params["day"], request.Params["uid"], request.Params["t"], request.Params["current"], request.Params["rowCount"], request.Form["sortBy"] +" "+ request.Params["sortDir"], request.Params["searchPhrase"], request.Params["byUser"]));
                    break;
                case "getofflinedevice":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetOfflineDeviceList(request.Form["uid"], request.Form["model"], request.Form["stime"], request.Form["et"], request.Form["t"], request.Params["current"], request.Params["rowCount"], request.Params[""], request.Params["searchPhrase"])));
                    break;
                case "getusersofflinedeviccount":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetUsersOffLineDevicCount()));
                    break;
                case "newgroup":
                    context.Response.Write(MyTeam.NewGroup(request.Form["groupName"], request.Form["userid"]));
                    break;
                case "delgroup":
                    context.Response.Write(MyTeam.DelGroup(request.Form["GroupID"]));
                    break;
                case "updategroup":
                    context.Response.Write(MyTeam.UpdateGroup(request.Form["GroupID"], request.Form["GroupName"]));
                    break;
                case "updatedevicegroupid":
                    context.Response.Write(MyTeam.UpdateDeviceGroupID(request.Form["toGroupID"], request.Form["SerialNumber"]));
                    break;
                case "getdeviceinfobyserialnumber":  // 根据deviceid获取设备信息
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetDeviceInfoBySerialNumber(request.Form["DeviceID"])));
                    break;
                case "upload": //导入excel文件(导入设备，导入SIM卡等)
                    context.Response.Write(MyTeam.UploadFile(context)); 
                    break;
                case "savedeviceinfo":
                    context.Response.Write(MyTeam.UpdateDevicInfo(request.Form["UpdateData"]));
                    break;
                case "sendcommand":  //发送指令
                    context.Response.Write(MyTeam.SendCommand(request.Form["data"]));
                    break;
                case "getresponse": //获取发送密令的返回结果
                    context.Response.Write(MyTeam.GetCommandResponse(request.Form["CommandID"], request.Form["TimeZones"]));
                    break; 
                case "resetpwd":
                    context.Response.Write(MyTeam.ResetDevicePassword(request.Form["deviceid"]));
                    break;
                case "deletedevice":  //删除设备 
                    context.Response.Write(MyTeam.DeleteDevice(request.Form["deviceid"], request.Form["Destroy"]));
                    break;
                case "recoverydevices": //恢复已删除的设备
                    context.Response.Write(MyTeam.RecoveryDevices(request.Form["data"], request.Form["isClaerAll"]));
                    break;
                case "deviceshiftorexpire":
                    if (request.Form["operType"] == "shift")
                        context.Response.Write(MyTeam.DeviceShiftOrExpire(request.Form["devices"], toUserID: request.Form["toUserID"]));
                    else
                        context.Response.Write(MyTeam.DeviceShiftOrExpire(request.Form["devices"], day: request.Form["day"]));
                    break;
                case "deleteuser":
                    context.Response.Write(MyTeam.DeleteUsers(request.Form["UserID"]));
                    break;
                case "adddevice":
                    context.Response.Write(MyTeam.AddDevice(request.Form["data"]));
                    break;
                case "clearallmessage":
                    context.Response.Write(MyTeam.ClearAllMessage(request.Form["UserID"]));
                    break;
                case "updatepassword":
                    context.Response.Write(MyTeam.UpdatePassword(request.Form["NewPassword"], request.Form["OldPassword"]));
                    break;
                case "searchdevices":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.SearchDevices(request.Form["SearchText"])));
                    break;
                case "searchusers":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.SearchUsers(request.Form["SearchText"])));
                    break;
                case "settingmsgtype":
                    context.Response.Write(MyTeam.SettingShowExceptionMsgType(request.Form["MsgID"], request.Form["UserID"]));
                    break;
                case "getsettingmsgtype":
                    context.Response.Write(MyTeam.GetSettingShowExceptionMsgType(request.Form["UserID"]));
                    break;
                case "getaddressbylatlng":
                    context.Response.Write(MyTeam.GetAddressByLatlng(request.Form["lat"], request.Form["lng"]));
                    break;
                case "downloadgooglekml":
                    context.Response.Write(MyTeam.DownloadLocation(request.Form["DeviceID"], request.Form["Date"]));
                    break;
                case "addusers":
                    context.Response.Write(MyTeam.addUsers(context));
                    break;
                case "shiftusers":
                    context.Response.Write(MyTeam.ShiftUsers(request.Form["UserID"], request.Form["toUserID"]));
                    break;
                case "getcommandlist":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetCommandList(request.Form["DeviceID"])));
                    break;
                case "imeibatch":
                    Utils.strImeiBatch = request.Form["Imeis"];
                    context.Response.Write("");
                    break;
                case "getdeleteddevices":
                    context.Response.Write(MyTeam.GetDeletedDevices(request["current"], request["rowCount"], request.Form["sortBy"] + " " + request.Params["sortDir"], request["searchPhrase"]));
                    break;
                case "renewalsexport": 
                    string date = request["date"];  
                    context.Response.Write(MyTeam.GetRenewalsExport(request["userid"], date.Split('~')[0].Trim(), date.Split('~')[1].Trim(), Convert.ToInt32( request["current"]), request["rowcount"], request.Form["sortBy"] + " " + request.Params["sortDir"],request["searchphrase"]));
                    break;
                case "getgroups":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetGroups(request.Form["uid"])));
                    break;
                case "getexceptionmessagecount":
                    context.Response.Write(Dao.DataTableToJSON(MyTeam.GetExceptionMessageCount(request.Form["deviceid"],request.Form["st"],request.Form["et"])));
                    break;
                case "adddevicegps":
                    //设备列表上面的添加设备功能(实际上是把这次添加的几台设备快速分到新建的分组里面)
                    context.Response.Write(MyTeam.AddDevicesList(request["carnum"],request["carusername"],request["cellphone"],request["description"],request["devicelist"]));
                    break;
                case "mgoologin": //江西赣州登录接口
                    callBack = request.Params["callback"];
                    string loginname = request.Params["loginname"];
                    string pwd = request.Params["pwd"];
                    string language = request.Params["language"]; 
                    string reulst =  callBack + "('" + com.Utils.mgooLogin(loginname, pwd, language) + "')";
                    context.Response.Write(reulst); 
                    break; 
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