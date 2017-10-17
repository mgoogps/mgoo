using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Collections;
using System.Web.SessionState;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Office.Interop;
using System.Configuration;
using System.Net;
using System.Net.Sockets;

namespace MgooGps.com
{
    public class Utils
    {
        /// <summary>
        /// 是否播放声音
        /// </summary>
        public static bool isPlayAudio = true;

        /// <summary>
        /// 是否显示下级的报警信息
        /// </summary>
        public static bool LowerMsg = true;

        /// <summary>
        /// IMEI号批量查询 临时存储
        /// </summary>
        public static string strImeiBatch = "";

        /// <summary>
        /// 当前使用的语言
        /// </summary>
        public static string language = "zh-cn";

        /// <summary>
        /// 过多少分钟算离线
        /// </summary>
        public static int offLineMinute
        {
            get { return 20; }
        }

        public static string logoutUrl = "";

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        public static string login(String name, String pwd, String language)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
            {
                return "no";
            } 
            SqlParameter[] list = new SqlParameter[2]{
              new SqlParameter("@LoginName",SqlDbType.VarChar),
              new SqlParameter("@Password",SqlDbType.VarChar)
            };
            Utils.language = language.Trim() == "" ? Utils.language : language;
            list[0].Value = name; 
            list[1].Value = pwd;
            string strSql = "select UserName,UserID,LoginName,UserType,SuperAdmin,PassWord,FirstName,CellPhone from users where Deleted=0 and (LoginName=@LoginName OR CellPhone=@LoginName)";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("LoginName", name) };
            Hashtable LoginTable = com.Dao.Select(strSql, parameter);
            if (LoginTable != null && LoginTable.Count > 0)
            {
                string loginPwd = LoginTable["PassWord"].ToString();
                loginPwd = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(loginPwd))).Replace("-", "").ToLower();
                if (loginPwd == pwd)
                {
                    string UserID = LoginTable["UserID"].ToString();
                    bool isSMSNotice = MG_BLL.Common.lib.Permission.IsSMSNotice(UserID);
                    var loginUser = new UserInfo(UserID, LoginTable["UserName"].ToString(), LoginTable["LoginName"].ToString(), LoginTable["FirstName"].ToString(), LoginTable["UserType"].ToString(), LoginTable["SuperAdmin"].ToString(), DateTime.Now, isSMSNotice);
                    loginUser.IsUpdateSonInfo = UserID == "" ? true : false;
                    Utils.SetSession("UserInfo", loginUser);
                    Utils.log("login : "+name +","+pwd);
                    return "main.aspx";
                }
            }
            else
            {
                LoginTable = com.Dao.Select(" select d.DeviceID,d.SerialNumber,d.DeviceName, UserID, d.DevicePassword from Devices d inner join  LKLocation l on l.DeviceID = d.DeviceID where d.SerialNumber = @LoginName and deleted=0 ", list);
                if (LoginTable != null && LoginTable.Count > 0)
                {
                    string dpwd = LoginTable["DevicePassword"].ToString();
                    dpwd = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(dpwd))).Replace("-", "").ToLower();
                    if (dpwd == pwd)
                    {
                        Utils.SetSession("UserInfo", new UserInfo(LoginTable["SerialNumber"].ToString(), LoginTable["UserID"].ToString(), LoginTable["DeviceID"].ToString(), LoginTable["DeviceName"].ToString()));
                        return "main.aspx";
                    } 
                }
            }
            return "no";
            //Hashtable userDr = com.Dao.Select("select UserID,UserName,LoginName,FirstName,CellPhone,SuperAdmin,UserType from Users where LoginName = @LoginName and Password=@Password and Deleted = 0", list);
            //if (userDr != null && userDr.Count >0)
            //{
            //    Utils.SetSession("UserInfo", new UserInfo(userDr["UserID"].ToString(), userDr["UserName"].ToString(), userDr["LoginName"].ToString(), userDr["FirstName"].ToString(), userDr["UserType"].ToString(), userDr["SuperAdmin"].ToString(), DateTime.Now));
            //    return "ok";
            //}
            //else
            //{
            //    userDr = com.Dao.Select(" select UserID,UserName,LoginName,FirstName,CellPhone,SuperAdmin,UserType from Users where CellPhone = @LoginName and Password= @Password and Deleted =0", list);
            //    if (userDr != null && userDr.Count > 0)
            //    {
            //        Utils.SetSession("UserInfo", new UserInfo(userDr["UserID"].ToString(), userDr["UserName"].ToString(), userDr["LoginName"].ToString(), userDr["FirstName"].ToString(), userDr["UserType"].ToString(), userDr["SuperAdmin"].ToString(), DateTime.Now));
            //        return "ok";
            //    }
            //    else
            //    {
            //        userDr = com.Dao.Select(" select d.DeviceID,d.SerialNumber,d.DeviceName, UserID from Devices d inner join  LKLocation l on l.DeviceID = d.DeviceID where d.SerialNumber = @LoginName and d.DevicePassword = @Password and deleted=0 ", list);
            //        if (userDr != null && userDr.Count > 0)
            //        {
            //            Utils.SetSession("UserInfo", new UserInfo(userDr["SerialNumber"].ToString(), userDr["UserID"].ToString(), userDr["DeviceID"].ToString(), userDr["DeviceName"].ToString()));
            //            return "ok";
            //        }
            //    }
            //    return "no";
            //}
        }

        /// <summary>
        /// 提供给外部的接口(江西赣州)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string mgooLogin(string name, string pwd, string language = null)
        {
            try
            { 
                SqlParameter[] list = new SqlParameter[1]{
              new SqlParameter("@LoginName",SqlDbType.VarChar),
            //  new SqlParameter("@Password",SqlDbType.VarChar)
                };
                Utils.language = language.Trim() == "" ? Utils.language : language;
                list[0].Value = name;
               // list[1].Value = pwd;
                string json = "";
                Hashtable userDr = com.Dao.Select("select UserID, LoginName, UserName, passWord, FirstName, UserType, SuperAdmin,passWord from Users where loginname=@LoginName" , list);  
                    //and password=@Password"
                if (userDr != null && userDr.Count > 0)
                {
                    byte[] result = Encoding.Default.GetBytes(userDr["passWord"].ToString());
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] output = md5.ComputeHash(result);
                    string pwdMd5 = BitConverter.ToString(output).Replace("-", ""); 
                    if (pwdMd5.ToLower() == pwd)
                    { 
                        UserInfo user = new UserInfo(userDr["UserID"].ToString(), userDr["UserName"].ToString(), userDr["LoginName"].ToString(), userDr["FirstName"].ToString(), userDr["UserType"].ToString(), userDr["SuperAdmin"].ToString(), DateTime.Now);
                       // Utils.SetSession("UserInfo", user); 
                        string token = (Guid.NewGuid().ToString().Replace("-", "") + Guid.NewGuid().ToString().Replace("-", "") + Guid.NewGuid().ToString().Replace("-", "")).ToLower();
                        long ticks = DateTime.Now.Ticks;
                        string resUrl = "http://120.24.78.26:8090/default.aspx?t=" + ticks + "&token=" + token;
                        HttpRuntime.Cache.Insert(token + ticks, user, null, DateTime.UtcNow.AddMinutes(1), TimeSpan.Zero);
                        json = "{\"success\":true,\"url\":\"" + resUrl + "\"}";
                    }
                    else
                    {
                        json = "{\"success\":false,\"url\":\"\"}";
                    }
                }
                else
                {
                    //userDr = com.Dao.Select(" select UserID,UserName,LoginName,FirstName,CellPhone,SuperAdmin,UserType from Users where CellPhone = @LoginName and Password= @Password and Deleted =0", list);
                    //if (userDr != null && userDr.Count > 0)
                    //{
                    //    Utils.SetSession("UserInfo", new UserInfo(userDr["UserID"].ToString(), userDr["UserName"].ToString(), userDr["LoginName"].ToString(), userDr["FirstName"].ToString(), userDr["UserType"].ToString(), userDr["SuperAdmin"].ToString(), DateTime.Now));
                    //    json = "{\"success\":true}";
                    //}
                    //else
                    //{
                    //    userDr = com.Dao.Select(" select d.DeviceID,d.SerialNumber,d.DeviceName, UserID from Devices d inner join  LKLocation l on l.DeviceID = d.DeviceID where d.SerialNumber = @LoginName and d.DevicePassword = @Password and deleted=0 ", list);
                    //    if (userDr != null && userDr.Count > 0)
                    //    {
                    //        Utils.SetSession("UserInfo", new UserInfo(userDr["SerialNumber"].ToString(), userDr["UserID"].ToString(), userDr["DeviceID"].ToString(), userDr["DeviceName"].ToString()));

                    //        json = "{\"success\":true}";
                    //    }
                    //}
                    json = "{\"success\":false,\"url\":\"\"}";
                }
                return json;
            }
            catch (Exception)
            {
                return "{\"success\":false,\"url\":\"\"}";
            }
        }

        /// <summary>
        /// 检查DataSet是否为真
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool checkDataTable( DataTable dt)
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
        /// DataSet 转为 Hashtable 第0个字段为键,第1个字段为值
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static Hashtable dsTransToHashtable(DataSet ds)
        {
            Hashtable hashTab = new Hashtable();
            if (checkDataSet(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!hashTab.ContainsKey(dr[0].ToString().Trim()))
                    {
                        hashTab.Add(dr[0].ToString().Trim(), dr[1].ToString().Trim());
                    }
                }
            }
            return hashTab;
        }
        /// <summary>
        ///  DataTable 转为 Hashtable 第0个字段为键,第1个字段为值
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Hashtable dsTransToHashtable(DataTable dt)
        {
            Hashtable hashTab = new Hashtable();
            if (checkDataTable(dt))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!hashTab.ContainsKey(dr[0].ToString().Trim()))
                    {
                        hashTab.Add(dr[0].ToString().Trim(), dr[1].ToString().Trim());
                    }
                }
            }
            return hashTab;
        }

        /// <summary>
        /// 根据session名获取session对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static UserInfo GetSession(String name =null)
        {
            if(name == null)
                name ="UserInfo";
            return (UserInfo)HttpContext.Current.Session[name];
        }
        /// <summary>
        /// 设置session
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="val">值</param>
        public static void SetSession(String name, object val)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[name] != null)
                HttpContext.Current.Session.Remove(name);
            HttpContext.Current.Session[name] = val;
        }

        public static void LoginOut()
        { 
            com.Utils.SetSession("UserInfo", null);
        }

        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public static void isLogin()
        {
            if (HttpContext.Current.Session["UserInfo"] == null)
            {
                HttpContext.Current.Response.Redirect(Utils.logoutUrl == "" ? "/login.aspx": Utils.logoutUrl);
            }
            else
            {
                if (((UserInfo)HttpContext.Current.Session["UserInfo"]).LoginType == "1")
                {
                    string path = HttpContext.Current.Request.Path;
                    if (path != "/main.aspx" && path != "/numerical.aspx" && path != "/RemainView.aspx" && path != "/AlarmMessage.aspx" && path != "/Mileage.aspx" && path != "/StopDetail.aspx" && path != "/ParkingEcharts.aspx"
                        && path != "/ExceptionView.aspx" && path != "/ExceptionCount.aspx" && path != "/ExceptionDetail.aspx" && path != "/GeoFencesView.aspx" && path != "/Tracking.aspx" && path != "/PlayBack.aspx" && path != "/Geofences.aspx")
                    {
                        HttpContext.Current.Response.Redirect("~/main.aspx");
                    }
                }
            }
        }

        /// <summary>
        /// 清除登录信息 
        /// </summary>
        public static void RemoveSession()
        {
            HttpContext.Current.Session.Clear();//.Remove("UserInfo");
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
        /// 将数据集中的数据导出到EXCEL文件
        /// </summary>
        /// <param name="dataSet">输入数据集</param>
        /// <param name="isShowExcle">是否显示该EXCEL文件</param>
        /// <returns></returns>
        public static bool DataSetToExcel(DataTable dt, bool isShowExcle)
        {

            if (dt == null || dt.Rows.Count == 0) return false;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                return false;
            }
            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;
                range.Font.Bold = true;
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            xlApp.Visible = isShowExcle;

            return true;
        }

        public static void DataSetToExcel(DataTable table, string file,String title)
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
                    else if (table.Columns[i].ColumnName == "DeviceStatus")
                    {
                        if (row["LastCommunication"] == null || string.IsNullOrEmpty( row["LastCommunication"].ToString()))
                        {
                            sw.Write("未激活");
                        }
                        else
                        {
                            TimeSpan ts = (DateTime.Now - Convert.ToDateTime(row["LastCommunication"].ToString()));
                            if (ts.TotalMinutes > Utils.offLineMinute  )
                            {
                                sw.Write("离线"+MinuteToHour(ts.TotalMinutes ,true));
                            }
                            else
                            {
                                sw.Write("在线");
                            }
                        }
                       // sw.Write(MinuteToHour());
                    }
                    else
                    {
                        sw.Write(row[i].ToString());
                    }
                    sw.Write("</td>");
                } 
                sw.Write("</tr>");
            }
            sw.Write("</table>");
            sw.Close(); 
            fs.Close();
        }

        public static void ListToExcel(List<List<string>> list,string file,string title)
        {
            FileStream fs = new FileStream(file, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.UTF8);
            sw.Write("<table style='font-weight:bold;'>");
            sw.Write(title);
            foreach (List<string> strs in list)
            {
                sw.Write("<tr>");
                for (int i = 0; i < strs.Count; i++)
                { 
                    sw.Write("<td style='border:1px solid #000000'>");
                    sw.Write(strs[i]);
                    sw.Write("</td>");
                }
                sw.Write("</tr>");
            }
            sw.Write("</table>");
            sw.Close();
            fs.Close();
        }

        public static String MinuteToHour(double mi, bool b = false)
        {
            try
            {
                if (mi <= 0)
                    return "0分钟";
                if (mi <= 60)
                {
                    return Math.Floor(mi) + "分钟";
                }
                var day = Math.Floor(mi / 60 / 24);
                var h = Math.Floor(mi / 60 % 24);
                var m = Math.Floor(mi % 60);
                string StrReturn = "";
                if (day > 0)
                {
                    StrReturn = day + "天";
                    if (b) 
                        return StrReturn; 
                } if (h > 0)
                {
                    StrReturn += h + "小时";
                    if (b)
                        return StrReturn;
                } if (m > 0)
                {
                    StrReturn += m + "分钟";
                    if (b)
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
        /// <param name="imagesurl1"></param>
        /// <returns></returns>
        public static string urlconvertor(string imagesurl1)
        {
            string tmpRootDir = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = imagesurl1.Replace(tmpRootDir, ""); //转换成相对路径
            imagesurl2 = imagesurl2.Replace(@"\", @"/");
            return imagesurl2;
        }
        /// <summary>
        /// 相对路径转换成服务器本地物理路径
        /// </summary>
        /// <param name="imagesurl1"></param>
        /// <returns></returns>
        public static string urlconvertorlocal(string imagesurl1)
        {
            string tmpRootDir = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = tmpRootDir + imagesurl1.Replace(@"/", @"\"); //转换成绝对路径
            return imagesurl2;
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
        public static String GetThumbnail(string serverImagePath, string thumbnailImagePath, int width, int height,String SaveFileName )
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
                    return "{\"success\":true,\"msg\":\"上传成功\",\"src\":\"" + urlconvertor(thumbnailImagePath) + "\",\"ImgName\":\"" + SaveFileName + "\"}";
                }
                catch (System.Exception e)
                {
                    return "{\"success\":false,\"msg\":\"" + e.Message + "\"}";
                }
                finally
                {
                    serverImage.Dispose();
                    bm.Dispose();
                    g.Dispose();
                    File.Delete(serverImagePath);
                }
            }
            catch (Exception ex)
            {
                File.Delete(serverImagePath);
                return "{\"success\":false,\"msg\":\"请上传有效的图片文件\"}";
            }
       
        }

       // public static String GetCommandResponse(int CommandID, String Timezones, CommandQueue.CommandQueueAjaxSoapClient send)
       // {
            //return send.GetResponse(CommandID, Timezones);
       // }
        /// <summary>
        /// 向设备发送命令
        /// </summary>
        /// <param name="CommandType"></param>
        /// <param name="DeviceID"></param>
        /// <param name="Model"></param>
        /// <param name="SN"></param>
        /// <param name="TrueOrFalse"></param>
        /// <returns></returns>
        public static String SendCommand(String SN, int DeviceID, String CommandType, int Model, String TrueOrFalse = default(String), String Phone = default(String))
        {
            CommandQueue.CommandQueueAjaxSoapClient send = new CommandQueue.CommandQueueAjaxSoapClient();
            // if (CommandType == "S7122")
            //{ 
            //    return Utils.GetCommandResponse(Convert.ToInt32(send.SendCommandByPhone (SN, DeviceID, CommandType, Model, Phone)), send); 
            // }
            // else
            // {int status = cmdByPhone.SendCommandByPhone("4209801819", 11078, "209CDW", 123, "10");
            int status = default(int);
            if (TrueOrFalse == default(String))
            {
                status = send.SendCommandByPhone(SN, DeviceID, CommandType, Model, Phone);
            }
            else
            {
                status = send.SendCommand(SN, DeviceID, CommandType, TrueOrFalse, Model);
            }

            return status.ToString(); //Utils.GetCommandResponse(Convert.ToInt32(Status), send); 
            //   }
        }

        public static DataSet LoadDataFromExcel(string filePath)
        {
            try
            {
                //备注： "HDR=yes;"是说Excel文件的第一行是列名而不是数据，"HDR=No;"正好与前面的相反。
                //       "IMEX=1 "如果列中的数据类型不一致，使用"IMEX=1"可必免数据类型冲突。 
                string strConn;
                if (filePath.Substring(filePath.IndexOf('.'), filePath.Length - filePath.IndexOf('.'))==".xls")
                {
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "data source=" + filePath + ";Extended Properties='Excel 4.0; HDR=Yes; IMEX=1'"; //此连接只能操作Excel2007之前
                }
                else
                {
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'"; //此连接可以操作.xls与.xlsx文件 (支持Excel2003 和 Excel2007 的连接字符串)
                }
             
                OleDbConnection OleConn = new OleDbConnection(strConn);
                OleConn.Open();
                DataTable schemaTable = OleConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                string tableName = schemaTable.Rows[0][2].ToString().Trim();
                String sql = "SELECT * FROM  [" + tableName + "]";//可是更改Sheet名称，比如sheet2，等等   

                OleDbDataAdapter OleDaExcel = new OleDbDataAdapter(sql, OleConn);
                DataSet OleDsExcle = new DataSet();
                OleDaExcel.Fill(OleDsExcle, tableName);

                OleConn.Close();
                return OleDsExcle;
            }
            catch (Exception err)
            { 
                Utils.log("LoadDataFromExcel error:" + err.Message);
                return null;
            }
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

        public static void log(string LogStr, string path = null)
        {
            //在网站根目录下创建日志目录
            //string appPath = HttpContext.Current.Request.PhysicalApplicationPath + "logs";
            //string p = "D://Log";
            string dir = System.AppDomain.CurrentDomain.BaseDirectory + "logs";
            string logName = DateTime.Now.ToString("yyyy-MM-dd") + "MgooGps.log";
            if (path == null)
            {
                //path = @"D:/Log/" + DateTime.Now.ToString("yyyy-MM-dd") + "MgooGps.log";
                path = dir +"/"+ logName;
            }
            else
            {
                dir = path;
                path = dir + "/" + logName;
            }
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            StreamWriter sw = null;
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                LogStr = DateTime.Now.ToLocalTime().ToString() + "  \n" + LogStr;
                sw = new StreamWriter(path, true, System.Text.Encoding.UTF8);
                sw.WriteLine(LogStr);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }

        }

        public static Hashtable insertTable(string hour ,DataTable dTable,DataTable newDataTable)
        {
            int count = dTable.Rows.Count; 
            Hashtable hhCount = new Hashtable ();
            for (int i = 1; i < count; i++)
            {
                string hh = Convert.ToDateTime(dTable.Rows[i]["DeviceTime"]).ToString("HH");
                hhCount[hh] = Convert.ToInt32(hhCount[hh]) + 1;
            }
            return hhCount;
        }

        public static void insertDataRow(ref DataTable retTable, DataRow AgoDR, DataRow RearDr, int rowIndex)
        {
            DateTime Ago = Convert.ToDateTime(AgoDR["DeviceTime"]); //上一个row
            DateTime Rear = Convert.ToDateTime(RearDr["DeviceTime"]); //当前row
            int seconds = (int)((Rear - Ago).TotalSeconds / 10);
            for (int i = 0; i < seconds; i++)
            {
                DataRow newRow = retTable.NewRow();
                newRow["DeviceTime"] = Ago.AddSeconds((i + 1) * 10);
                newRow["Speed"] = 0;
                retTable.Rows.InsertAt(newRow, rowIndex);
                rowIndex++;
            }
        }
        public static string SendTcpCmd(string cmd)
        {
            Socket clientSocket = null;
            try
            {
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["tcpPort"]); // 7700;
                string host = ConfigurationManager.AppSettings["tcpIP"].ToString();  //"120.24.78.26";//服务器端ip地址

                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);

                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(ipe);

                //send message 
                byte[] sendBytes = Encoding.ASCII.GetBytes(cmd);
                clientSocket.BeginSend(sendBytes, 0, sendBytes.Length, SocketFlags.None, null, null);

                //receive message
                string recStr = "";
                byte[] recBytes = new byte[2];
                int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
                recStr += Encoding.ASCII.GetString(recBytes, 0, bytes);
                return recStr;
            }
            catch (Exception ex)
            {
                Utils.log("SendTcpCmd Error:"+ex.Message);
                throw ex;
            }
            finally
            {
                if (clientSocket != null)
                    clientSocket.Close();
            }
        }
    }
}