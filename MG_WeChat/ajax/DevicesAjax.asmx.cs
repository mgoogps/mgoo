using MG_BLL;
using MG_BLL.Common;
using MG_BLL.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace MG_WeChat.ajax
{
    /// <summary>
    /// DevicesAjax 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class DevicesAjax : System.Web.Services.WebService
    {
        public MG_BLL.Common.AuthHeader myHeader = new AuthHeader();
        
        [SoapHeader("myHeader")]
        [WebMethod( Description ="根据用户id获取分组列表,然后根据分组ID获取该组下的设备列表")] 
        public string GetGroupList(string userid)
        {
            string valid = myHeader.isValid(userid);
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return Utils.ToJson(d.GetGroupList(userid));
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据用户id获取设备列表")] 
        public string GetDevicesList(string userid)
        {
            string valid = myHeader.isValid(userid);
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader); 
            return Utils.ToJson(d.GetDevicesList(userid));
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据设备id获取设备实时数据.")] 
        public string GetMonitorByDeviceID(string deviceid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return Utils.ToJson(d.GetMonitorByDeviceID(deviceid));
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "新增分组.")]
        public string AddGroups(string userid, string groupname)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Groups d = new Groups(myHeader);
            return d.AddGroups(userid, groupname);
           
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "修改分组.")]
        public string UpdateGroups(string groupid, string groupname)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Groups g = new Groups(myHeader);
            return g.UpdateGroups(groupid, groupname);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "删除分组.")]
        public string DeleteGroups(string groupid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Groups g = new Groups(myHeader);
            return g.DeleteGroups(groupid); 
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据设备ID获取设备的信息.")]
        public string GetDeviceInfoByID(string deviceid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return Utils.ToJson(d.GetDeviceInfoByID(deviceid));
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据设备ID修改设备的信息.")]
        public string UpdateDeviceInfoByID(string deviceid, string devicename, string carusername, string cellphone, string carnum,string groupid,string description,string sens)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return d.UpdateDeviceInfoByID(deviceid, devicename, carnum, carusername, cellphone, groupid, description, sens);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据设备ID获取该设备的历史轨迹，一次只能查看一天的轨迹.")]
        public string GetHistoryLocus(string deviceid, string date,string maptype)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            if (string.IsNullOrEmpty(maptype))
            {
                maptype = "GAODE";
            }
            return Utils.ToJson(d.GetHistoryLocus(deviceid, date,maptype)); 
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据设备ID查询该设备某个时间段的历史轨迹,speedfilter:速度过滤,默认为7.5 . ")] 
        public string GetHistoryLocusList(string deviceid,string startdate,string enddate,string speedfilter)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader); 
            return Utils.ToJson(d.GetHistoryLocus(deviceid, startdate, enddate, speedfilter));
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "添加设备")]
        public string AddDevice(string imei,string vccode,string userid,string groupid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return d.AddDevice(imei,vccode,userid,groupid);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "批量添加设备")]
        public string AddDeviceBatch(string carnum,string carusername,string cellphone, string devicelist)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return d.AddDeviceBatch(carnum, carusername, cellphone, devicelist);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "删除设备")]
        public string DeleteDevice(string deviceid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            return Utils.GetResult("您无权删除此设备.", statusCode.Code.failure);
            //Devices d = new Devices(myHeader);
            //return d.DeleteDeviceByID(deviceid);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "获取所有设备型号")]
        public string GetModelList()
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return Utils.ToJson( d.GetModelList());
        } 

        [SoapHeader("myHeader")]
        [WebMethod(Description = "添加电子围栏")]
        public string AddGeoFence(string fencename,string userid, string deviceid, string latitude, string longitude, string radius,string description)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return d.AddGeoFence(fencename, userid, deviceid, latitude, longitude, radius, description);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "修改电子围栏")]
        public string UpdateGeoFence(string fenceid, string fencename, string latitude, string longitude, string radius, string description)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return d.UpdateGeoFence(fenceid, fencename, latitude, longitude, radius, description);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "删除电子围栏")]
        public string DeleteGeoFence(string fenceid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return d.DeleteGeoFence(fenceid);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据用户获取该用户下的电子围栏列表")]
        public string GetGeoFenceList(string userid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return Utils.ToJson( d.GetGeoFenceList(userid));
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据设备ID设置该设备的围栏信息")]
        public string SetGeoFenceByDeviceID(string deviceid, string radius, string lng, string lat) {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return d.SetGeoFenceByDeviceID(deviceid,radius,lng,lat);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据ID查询该电子围栏的详细信息")]
        public string GetGeoFenceInfoByID(string fenceid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return Utils.ToJson(d.GetGeoFenceInfoByID(fenceid));
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据设备ID查询该设备下的电子围栏")]
        public string GetGeoFenceListByDeviceID(string deviceid)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return Utils.ToJson(d.GetGeoFenceListByDeviceID(deviceid));
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "发送指令.")]
        public string SendCommand(string command)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            return d.SendCommand(command);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "根据imei号查询该设备是否存在VC码.")]
        public string VCCodeExist(string serialnumber)
        {
            string valid = myHeader.isValid();
            if (!valid.Equals(string.Empty))
            {
                return valid;
            }
            Devices d = new Devices(myHeader);
            Dictionary<string,string> dic = d.VCCodeExist(serialnumber);
            if (dic.Count > 0)
            {
                string vccode1 = dic["DevicePassword"];
                DateTime created = dic["Created"].toDateTime();
                if (!string.IsNullOrEmpty(vccode1) && created > "2016-10-22 00:00:28".toDateTime() && vccode1!= "123456")
                {
                    return Utils.GetResult("该IMEI有VC码.", statusCode.Code.success, vccode1);
                }
                else
                {
                    return Utils.GetResult("该IMEI没有VC码.", statusCode.Code.success, "");
                }
            }
            else
            {
                return Utils.GetResult("未找到该设备.", statusCode.Code.failure, "");
            }
          
        }
    }
}
