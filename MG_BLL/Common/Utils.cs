using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.SessionState;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Data.OleDb;
using System.Web.Script.Serialization;
using MG_BLL.Entity;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Security.Cryptography;

namespace MG_BLL
{
    public class Utils : System.Web.UI.Page, IHttpHandler, IRequiresSessionState
    {

        //bool IHttpHandler.IsReusable
        //{
        //    get { throw new NotImplementedException(); }
        //}

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 当前使用的语言
        /// </summary>
        public static string language = "zh-cn";

        /// <summary>
        /// 速度过滤
        /// </summary>
        private static double _speedFilter = 7.5;

        /// <summary>
        /// 
        /// </summary>
        private static int _offLineMinute = 20;

        private static int _stopSeconds = 600;

        private static string loginType  ;
        /// <summary>
        /// 过多少分钟算离线
        /// </summary>
        public static int OffLineMinute
        {
            get
            {
                return _offLineMinute;
            }
        }
        /// <summary>
        /// 过滤速度
        /// </summary>
        public static double SpeedFilter
        {
            get
            {
                return _speedFilter;
            }
        }
        /// <summary>
        /// 两点之间的时间相差600秒就算停留
        /// </summary>
        public static int StopSeconds
        {
            get
            {
                return _stopSeconds;
            }


        }

        public static string LoginType
        {
            get
            {
                return loginType;
            }

            set
            {
                loginType = value;
            }
        }

        /// <summary>
        /// 检查DataSet是否为真
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool checkDataTable(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public static bool checkDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

          
        /// <summary>
        /// 获取当前登录用户的ID
        /// </summary>
        /// <returns></returns>
        public static string GetSessionUserID()
        {
            return MG_DAL.SessionOper.GetSessionUserID();
        }
        
        public static void SetSessionUserName(string name)
        {
            MG_DAL.SessionOper.SetSessionUserName(name);
        }

        public static void RemoveSession()
        {
            MG_DAL.SessionOper.RemoveSession();
        }


        public static bool isLogin()
        {
            return MG_DAL.SessionOper.isLogin();
        }

        /// <summary>
        /// 将时间转换成js的时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long GetTimeLikeJS(DateTime dt)
        {
            long lLeft = 621355968000000000;
            long Sticks = (dt.Ticks - lLeft) / 10000;
            return Sticks;
        }


        /// <summary>
        ///  将数据集中的数据导出到EXCEL文件
        /// </summary>
        /// <param name="table">导出的数据</param>
        /// <param name="file">路径</param>
        /// <param name="title">列表的head</param>
        public static void DataSetToExcel(DataTable table, string file, String title)
        {
            FileStream fs = new FileStream(file, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.UTF8);
            sw.Write("<table style='font-weight:bold;'>");
            sw.Write(title);
            foreach (DataRow row in table.Rows)
            {
                sw.Write("<tr>");
                for (int i = 0; i < table.Columns.Count; i++)
                {

                    sw.Write("<td style='border:1px solid #000000'>");
                    // line += row[i].ToString() + "              \t"; //内容：自动跳到下一单元格 
                    if (table.Columns[i].ColumnName == "TimediffMinute" && row[i].ToString().Trim() != "")
                    {
                        sw.Write(Utils.MinuteToHour(double.Parse(row[i].ToString())));
                    }
                    else
                        sw.Write(row[i].ToString());
                    sw.Write("</td>");
                }
                sw.Write("</tr>");
            }
            sw.Write("</table>");
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 把分钟转换成多少天多少小时
        /// </summary>
        /// <param name="mi">分钟数</param>
        /// <param name="isDetailsDate">是否显示详细时间(true:1天1分1秒，false:1天)</param>
        /// <returns></returns>
        public static String MinuteToHour(double mi,bool isDetailsDate = false)
        {
            try
            {
                if (mi <= 0)
                    return "0分";
                if (mi <= 60)
                {
                    return Math.Floor(mi) + "分";
                }
                var day = Math.Floor(mi / 60 / 24);
                var h = Math.Floor(mi / 60 % 24);
                var m = Math.Floor(mi % 60);
                string StrReturn = "";
                if (day > 0)
                {
                    StrReturn = day + "天";
                    if (isDetailsDate)
                        return StrReturn;
                }
                if (h > 0)
                {
                    StrReturn += h + "时";
                    if (isDetailsDate)
                        return StrReturn;
                }
                if (m > 0)
                {
                    StrReturn += m + "分";
                    if (isDetailsDate)
                        return StrReturn;
                }
                return StrReturn;
            }
            catch (Exception e)
            {
                return "";
            }

        }

        /// <summary>
        /// 本地路径转换成URL相对路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string urlconvertor(string url)
        {

            //  System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string tmpRootDir = "";// System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = url.Replace(tmpRootDir, ""); //转换成相对路径
            imagesurl2 = imagesurl2.Replace(@"\", @"/");
            return imagesurl2;
        }
        /// <summary>
        /// 相对路径转换成服务器本地物理路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string urlconvertorlocal(string url)
        {
            string tmpRootDir = "";// System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = tmpRootDir + url.Replace(@"/", @"\"); //转换成绝对路径
            return imagesurl2;
        }
        public static string GetIP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
            {
                return "127.0.0.1";
            }
            return result;
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="serverImagePath">图片地址</param>
        /// <param name="thumbnailImagePath">缩略图地址</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="SaveFileName">保存到数据的名称</param>
        /// <param name="imei">设备的IMEI号</param> 
        public String GetThumbnail(string serverImagePath, string thumbnailImagePath, int width, int height, String SaveFileName)
        {
            try
            {
                System.Drawing.Image serverImage = System.Drawing.Image.FromFile(serverImagePath);

                //画板大小
                int towidth = width;
                int toheight = height;
                //缩略图矩形框的像素点
                //int x = 0;
                //int y = 0;
                int ow = serverImage.Width;
                int oh = serverImage.Height;

                //if (ow > oh)
                //{
                //    toheight = serverImage.Height * width / serverImage.Width;
                //}
                //else
                //{
                //    towidth = serverImage.Width * height / serverImage.Height;
                //}
                //新建一个bmp图片
                System.Drawing.Image bm = new System.Drawing.Bitmap(width, height);
                //新建一个画板
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bm);
                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空画布并以透明背景色填充
                g.Clear(System.Drawing.Color.White);
                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(serverImage, new System.Drawing.Rectangle((width - towidth) / 2, (height - toheight) / 2, towidth, toheight), 0, 0, ow, oh, System.Drawing.GraphicsUnit.Pixel);
                try
                {
                    //以jpg格式保存缩略图
                    bm.Save(thumbnailImagePath);
                    return "{\"success\":\"" + SaveFileName + "\"}";
                }
                catch (System.Exception e)
                {
                    return "{\"success\":\"false\"}";
                }
                finally
                {
                    serverImage.Dispose();
                    bm.Dispose();
                    g.Dispose();
                    File.Delete(serverImagePath);
                }
            }
            catch (Exception)
            {
                File.Delete(serverImagePath);
                return "{\"success\":\"false\"}";
            }

        }


        private static object lock_log = new object();
        /// <summary>
        /// 写入Log
        /// </summary>
        /// <param name="LogStr">要写入的内容</param>
        /// <param name="Name">文件名(可选)</param>
        public static void log(string LogStr, string Name = null)
        {
            string ProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            string path = "";
            string p = "D://Log";
            p = System.AppDomain.CurrentDomain.BaseDirectory + "logs";  //HttpContext.Current.Request.PhysicalApplicationPath + "logs";
            string fileName = "";  
            if (Name == null)
                fileName = DateTime.Now.ToString("yyyy-MM-dd") + "-MG_WeChat-"+ ProcessName + ".log";
            else
                fileName = Name;

            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
          
            path = p + "/" + fileName; 
           
            try
            {
                lock (lock_log)
                { 
                    LogStr = DateTime.Now.ToLocalTime().ToString() + "  \n" + LogStr;
                    using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(LogStr); 
                    }
                } 
            }
            catch ( Exception ex){
                Pay.WeixinPay.lib.Log.Info("Utils.Log","写入Log出错"+ex.Message+",内容:"+LogStr);
            }
          
        }

        public static object ToObject(string json)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<object>(json);
        }
        public static Dictionary<string, string> ToDictionary(string obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<Dictionary<string, string>>(obj);
        }
        public static T ToObjects<T>(string obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<T>(obj);
        }
        public static void ToUsers(string obj, Object type)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            type = js.Deserialize<object>(obj);
        }

        public static List<Dictionary<string, string>> ToList(string obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<List<Dictionary<string, string>>>(obj);
        }
        public static object JsonToList(string json)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<List<object>>(json);
        }
        /// <summary>
        /// 把集合序列化
        /// </summary>
        /// <param name="json"></param>
        public static string ToJson(object json, bool isMaxJson = false)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            if (isMaxJson)
            {
                js.MaxJsonLength = int.MaxValue;
            }
            return js.Serialize(json);
        }

      
        public static string GetCoureName(string _Course)
        {
             string name = "";
            if (string.IsNullOrEmpty(_Course))
            {
                return "未知";
            }
            int course = Convert.ToInt32(_Course);
            if ((course >= 0 && course < 22.5) || (course >= 337.5 && course < 360)) // 0
            {
                name = "正北";
            }
            else if (course >= 22.5 && course < 67.5) // 45
            {
                name = "东北";
            }
            else if (course >= 67.5 && course < 112.5) // 90
            {
                name = "正东";
            }
            else if (course >= 112.5 && course < 157.5) //135
            {
                name = "东南";
            }
            else if (course >= 157.5 && course < 202.5) //180
            {
                name = "正南";
            }
            else if (course >= 202.5 && course < 247.5) //225
            {
                name = "西南";
            }
            else if (course >= 247.5 && course < 292.5) //270
            {
                name = "正西";
            }
            else if (course >= 292.5 && course < 337.5) //315
            {
                name = "西北";
            }
            else
            {
                name = "未知";
            }
            return name;
        }
        /// <summary>
        /// 利用高德地图API把gps84坐标系转换成gcj02坐标系
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        //public static Gps amap_gps84toGcj02(string lat, string lng, bool address = true)
        //{
        //    Gps gps = null;
        //    if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lng))
        //    {
        //        gps = new Gps(-1,-1);
        //        return gps;
        //    }
        //    MG_DAL.MgoogpsWebClient mwc = new MG_DAL.MgoogpsWebClient();
        //    string key = ConfigurationManager.AppSettings["GaoDeKey"].toStringEmpty();
        //    mwc.RequestUrl = "http://restapi.amap.com/v3/assistant/coordinate/convert?key="+ key + "&locations=" + lng + "," + lat + "&coordsys=gps";
        //    mwc.RequestMethodType = "GET";
        //    //{"status":"1","info":"ok","infocode":"10000","locations":"116.487586,39.991755"}
        //    string json = mwc.RequestSend();
        //    Dictionary<string, string> dic = Utils.ToDictionary(json);
        //    string[] locations = dic["locations"].toStringEmpty().Split(',');
        //      gps = new Gps(Convert.ToDouble(locations[1]), Convert.ToDouble(locations[0]));
        //    if (address)
        //    { 
        //        gps.Address = GetAddress(gps.getWgLat(), gps.getWgLon(), key);
        //    }
        //    return gps;
        //}
        /// <summary>
        /// 根据本地算法把gps84坐标系转换成gcj02(高德)坐标系
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static Gps gps84_To_Gcj02(string lat, string lng,string key=null , bool address = true)
        { 
            double _lat = lat.toDouble();
            double _lng = lng.toDouble();
            if (key == null)
            {
                key = ConfigurationManager.AppSettings["AmapKey"].ToString();
            }
            Gps gps;
            if (string.IsNullOrEmpty(lat) || string .IsNullOrEmpty(lng) || _lat == -1.0 || _lng == -1.0)
            {
                gps = new Gps(-1.000,-1.000);
                gps.Address = "未解析到地址.";
                return gps;
            }
            gps = MG_BLL.EvilTransform.PositionUtil.gps84_To_Gcj02(lat.toDouble(), lng.toDouble());
            try
            {
                if (address)
                {
                    Geocoding geo = new Amap();
                    geo.key = key;
                    gps.Address = geo.GetAddress(gps); 
                }
                return gps;
            }
            catch ( Exception ex)
            {
                gps.Address = "未知";
                Utils.log("获取地址异常(gps84_To_Gcj02):" + ex.Message);
                return gps;
            }
        }



        /// <summary>
        /// 根据高德地图API获取地址
        /// </summary>
        /// <param name="Lat"></param>
        /// <param name="Lng"></param>
        /// <returns></returns>
        //public static string GetAddress(double Lat, double Lng, string amapkey)
        //{
        //    try
        //    {
                 
        //        //string jsonLocation = "";
        //        //MG_DAL.MgoogpsWebClient mwc = new MG_DAL.MgoogpsWebClient();
        //        //mwc.RequestMethodType = "GET";
        //        //mwc.RequestUrl = " http://restapi.amap.com/v3/geocode/regeo?output=json&location=" + Lng + "," + Lat + "&key=" + amapkey + "&radius=1000&extensions=base";
        //        //jsonLocation = mwc.RequestSend();
        //        //Dictionary<string, object> addressDic = Utils.ToObjects<Dictionary<string, object>>(jsonLocation);
        //        //if (addressDic["status"].Equals("1"))
        //        //{
        //        //    return (addressDic["regeocode"] as Dictionary<string, object>)["formatted_address"].toStringEmpty();
        //        //}
        //        //else
        //        //{
        //        //    return "未获取到地址.";
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.log("获取地址异常(GetAddress):" + ex.Message);
        //        return "未知.";
        //    }
        //}



        /// <summary>
        /// 获取配置文件中高德地图的Key
        /// </summary>
        /// <returns></returns>
        public static string GetAmapKey()
        {
            return ConfigurationManager.AppSettings["AmapKey"].toStringEmpty();
        }
        /// <summary>
        /// 获取配置文件中百度地图的Key
        /// </summary>
        /// <returns></returns>
        public static string GetBaiDuKey()
        {
            return ConfigurationManager.AppSettings["BaiduMapKey"].toStringEmpty();
        }

        public static string GetResult(ajaxResult reulst)
        {
            return ToJson(reulst);
        }

        public static string GetResult(string message, statusCode.Code code, string result = "")
        { 
            ajaxResult ar = new ajaxResult();
            ar.StatusCode = code;
            ar.Message = message;
            ar.Result = result;
            return Utils.ToJson(ar);
        }

      

        public static void SetCache(string key,object val,double absoluteExpiration = 20)
        {
            HttpRuntime.Cache.Insert(key,val,null,DateTime.Now.AddMinutes(absoluteExpiration),TimeSpan.Zero);
        }
        public static T GetCache<T>(string key)
        {
            return (T)HttpRuntime.Cache.Get(key);
        }

        public static void RemoveCache(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public static int HttpRuntimeCacheCount()
        {
            return HttpRuntime.Cache.Count;
        }

        public static string GetMD5(string str)
        {
            byte[] bytes = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public static string SendTcpCmd(string cmd,string ServerID = null)
        {
            Socket clientSocket = null;
            try
            {
                string sid = "1";
                if (string.IsNullOrEmpty(ServerID))
                {
                    string imeiOrDeviceID = cmd.Split('-')[2]; //IMEI号 或者 DeviceID
                    if (string.IsNullOrEmpty(imeiOrDeviceID))
                    {
                        sid = "1";
                        Utils.log("SendTcpCmd Error: cmd:" + cmd);
                    }
                    else
                    {
                        string strSql = " select dc.cmdid from devices d inner join devicesconfig dc on dc.deviceid=d.deviceid where d.deleted=0 and ( d.SerialNumber='" + imeiOrDeviceID + "' or d.DeviceID=" + imeiOrDeviceID+")";
                        MG_DAL.SQLServerOperating s = new MG_DAL.SQLServerOperating();
                        sid = s.Select(strSql);
                    }
                }
                else
                {
                    sid = ServerID;
                }
                string configName = "tcpIP" + sid; 
               
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["tcpPort"]); // 7700;
                string host = ConfigurationManager.AppSettings[configName].ToString();  //"120.24.78.26";//服务器端ip地址

                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                clientSocket.SendTimeout = 3000;
                clientSocket.Connect(ipe);

                //send message 
                byte[] sendBytes = Encoding.ASCII.GetBytes(cmd);
                clientSocket.BeginSend(sendBytes, 0, sendBytes.Length, SocketFlags.None, null, null);

                //receive message
                string recStr = "";
                byte[] recBytes = new byte[2];
                int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
                recStr += Encoding.ASCII.GetString(recBytes, 0, bytes);
               Utils.log( cmd + ":" + host,recStr);
                return recStr;
            }
            catch (Exception ex)
            {
                Common.Log.Error("SendTcpCmd Error", ex);
               // Utils.log("SendTcpCmd Error:"+ex.Message+ ","+ cmd);
                return "0";
            }
            finally
            {
                if (clientSocket != null)
                    clientSocket.Close();
            }
        }
    }

    public class statusCode
    {
        public enum Code
        {
            success = 200,      // 正常
            failure = 300,     //失败
            error = 500,      //错误
            tokenFail = 501,  //token失效
            loginFailure = 502  //登录信息失效
        }
    }

    public class ajaxResult
    {
        private statusCode.Code _statusCode;
        private string _message;
        private string _result;

        public statusCode.Code StatusCode
        {
            get
            {
                return _statusCode;
            }

            set
            {
                _statusCode = value;
            }
        }

        public string Message
        {
            get
            {
                return _message ?? string.Empty;
            }

            set
            {
                _message = value;
            }
        }

        public string Result
        {
            get
            {
                return _result??"";
            }

            set
            {
                _result = value;
            }
        }
    }
    
         
}
