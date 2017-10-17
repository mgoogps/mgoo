using MG_BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MG_GPS
{
    public partial class Unlock : System.Web.UI.Page
    {
        public Dictionary<string, string> dic = new Dictionary<string, string>();
        public string status = "";
        public int DeviceID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) )
            {
                string action = Request.QueryString["action"];
                switch (action)
                {
                    case "unlock":
                        SendUnlockCommand();
                        break;
                    case "gettracking":
                        DeviceID = Convert.ToInt32( Request.Params["DeviceID"]);
                        GetDeviceTracking(DeviceID);
                        break;
                    default:
                        break;
                }
             
            }
            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["deviceid"]))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["status"]))
                {
                    status = Request.QueryString["status"];
                }
                DeviceID = int.Parse(Request.QueryString["deviceid"]);
                // if (DeviceID == 75539)
                // {
                //    DeviceID = 74324;
                // }
                //GetDeviceTracking(DeviceID);
            }
        }


        private void GetDeviceTracking(int DeviceID)
        {
            MG_DAL.SQLServerOperating sqlHelper = new MG_DAL.SQLServerOperating();
            string strSql = "select d.DeviceID,d.SerialNumber,DeviceName,l.OLat,l.OLng,l.LastCommunication DeviceDate,l.Speed,l.Course,l.DataContext from devices d inner join LKLocation l on l.DeviceID=d.DeviceiD where d.DeviceID =@DeviceID and d.deleted=0";// model=213 and 

            dic = sqlHelper.Selects(strSql, new SqlParameter[] { new SqlParameter("DeviceID", DeviceID) }).toDictionary();

            Geocoding geo = new Amap();
            Gps gps = geo.Translate(dic["OLat"], dic["OLng"]);
            //Gps gps = Utils.gps84_To_Gcj02(dic["OLat"], dic["OLng"]);
            dic["OLat"] = gps.getWgLat().ToString();
            dic["OLng"] = gps.getWgLon().ToString();
            dic["Address"] = gps.Address;
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
            Response.Write( Utils.ToJson(dic));
            Response.End();
        }

        private void SendUnlockCommand()
        {
            var DeviceID = int.Parse(Request.Form["DeviceID"]);
            var SerialNumber = Request.Form["SerialNumber"];
            var Password = Request.Form["Password"];
            var status = "3";
            // if (DeviceID == 75539)
            //  {
            //     DeviceID = 74324;
            // }
            if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(SerialNumber) && DeviceID > 0 && Password.Length > 0 && Password.Length < 10)
            {
                string strSql = "select DevicePassword from devices where deviceid=@DeviceID and deleted=0";
                MG_DAL.SQLServerOperating sqlHelper = new MG_DAL.SQLServerOperating();
                string devicePassword = sqlHelper.Select(strSql, new SqlParameter[] { new SqlParameter("DeviceID", DeviceID) });
                if (devicePassword.Equals(Password))
                {
                    status = Utils.SendTcpCmd("VTR-Command-" + SerialNumber + "-KM");
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
                    status = "2";
                }
            }
            Response.Redirect("Unlock.aspx?DeviceID=" + DeviceID + "&status=" + status);
        }
    }
}