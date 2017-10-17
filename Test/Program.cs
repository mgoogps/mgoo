using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MG_DAL;
using MG_BLL;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Test
{
    class Program
    {
        static SQLServerOperating s = new SQLServerOperating();
        static Dictionary<string, DataTable> cacheTable = new Dictionary<string, DataTable>();
        public static Dictionary<string, string> cacheAddress = new Dictionary<string, string>();

        static string path = System.AppDomain.CurrentDomain.BaseDirectory + "logs";
       static int index = 0;
        static int index1 = 0;
        static void Main(string[] args)
        {
            //TaskDemo td = new Test.TaskDemo();
            //td.action();
            List<Task> listTask = new List<Task>();
            for (int i = 0; i < 1000; i++)
            {
               Task task = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        string str = ""; // Android---渭南市华县北拾村基站渭南市华县北拾村基站:
                        index++;
                        log(index.ToString() + "-----" + str);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("0" + ex.Message);
                    }

                });
                listTask.Add(task);
                task = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        string str = ""; // Android---渭南市华县北拾村基站渭南市华县北拾村基站:
                        index1++;
                        log1(index1.ToString() + "-----" + str);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("1" + ex.Message);
                    }
                });
                listTask.Add(task);
            } 
            Console.WriteLine(path);
            Task.WaitAll(listTask.ToArray());
            string[] dirs = Directory.GetFiles(path);

            for (int i = 0; i < dirs.Length; i++)
            {
                string[] filesLins = File.ReadAllLines(dirs[i]);
                Console.WriteLine(dirs[i]);
                Console.WriteLine(filesLins.Length);
            }
            Console.WriteLine("完成.");
            Console.ReadKey();

        }
        /// <summary>
        /// 导出停留列表
        /// </summary>
        public static void 导出Excel()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string sql = @"with temp(UserID,ParentID) as
                        (
                            select UserID, ParentID from Users where UserID = 3437 
                            union all 
                            select Users.UserID, Users.ParentID from Users, temp

                            where Users.ParentID = temp.UserID and users.Deleted = 0
                        )
                        select DeviceID from devices where userid in(select UserID from temp) and deleted = 0";

            DataTable dt = s.Selects(sql);
            DateTime startTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00").toDateTime();
            DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd 23:59:59").toDateTime();

            DataTable table = new DataTable();
            table.Columns.Add("rowid");
            table.Columns.Add("DeviceName");
            table.Columns.Add("imei");
            table.Columns.Add("UserName");
            table.Columns.Add("startTime");
            table.Columns.Add("endTime");
            table.Columns.Add("lng");
            table.Columns.Add("lat");
            table.Columns.Add("time");
            table.Columns.Add("address");

            try
            {
                int tableIndex = 1;
                for (int i = 10; i < 17; i++)
                {
                    Console.WriteLine("i:" + i);

                    foreach (DataRow row in dt.Rows)
                    {
                        int DeviceID = row.Field<int>("DeviceID");
                        Dictionary<string, string> dic = GetDeviceName(DeviceID + "");
                        DataTable temp = new DataTable();
                        sql = "select DeviceID,Speed,DATEADD(HH,8,DeviceUTCTime) DeviceTime,OLat Latitude,OLng Longitude from [YWData201706].[dbo].[Location" + i + "] where DeviceID=" + DeviceID + " and  DeviceUTCTime >= '" + startTime + "' and  DeviceUTCTime <= '" + endTime + "' and Speed >= 10 order by DeviceTime asc";

                        temp = s.Selects(sql);
                        for (int j = 1; j < temp.Rows.Count; j++)
                        {
                            #region 填充table

                            DataRow lastRow = temp.Rows[j - 1]; DataRow currentRow = temp.Rows[j];
                            DateTime start = Convert.ToDateTime(lastRow["DeviceTime"]);
                            DateTime end = Convert.ToDateTime(currentRow["DeviceTime"]);
                            TimeSpan ts = start.Subtract(end).Duration();
                            if (ts.TotalSeconds > 600)
                            {
                                Console.WriteLine(i + "-停留点:" + tableIndex + ",DeviceID:" + DeviceID + ",UserName:" + dic["UserName"]);
                                DataRow r = table.NewRow();
                                r["rowid"] = tableIndex++;
                                r["DeviceName"] = dic["DeviceName"];
                                r["UserName"] = dic["UserName"];
                                r["startTime"] = start.ToString("yyyy-MM-dd HH:mm:ss");
                                r["endTime"] = end.ToString("yyyy-MM-dd HH:mm:ss");
                                r["lng"] = Convert.ToDouble(lastRow["Longitude"]).ToString("0.00000");
                                r["lat"] = Convert.ToDouble(lastRow["Latitude"]).ToString("0.00000");
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
                                r["time"] = time;
                                string addressKey = "";
                                string address = "";
                                if (cacheAddress.ContainsKey(addressKey))
                                {
                                    address = cacheAddress[addressKey];
                                }
                                else
                                {
                                    address = Utils.GetAddressByLatlng(lastRow["Latitude"].ToString(), lastRow["Longitude"].ToString());
                                }
                                r["address"] = address;
                                r["imei"] = dic["SerialNumber"];
                                table.Rows.Add(r);
                                j++;
                            }
                            #endregion
                        }
                    }
                }
                string FileName = "停留详单(" + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";
                string title = @" <h4>时间：" + startTime + "-" + endTime + @"</h4>
                         <tr style='border:1px solid #000000'><td>序号</td><td> 设备名称 </td> <td> IEMI号 </td> <td> 所属用户 </td> <td> 开始时间  </td><td> 结束时间  </td><td>  经度 </td><td> 纬度 </td><td> 持续时间 </td><td> 地址 </td></tr>";

                if (table.Rows.Count > 0)
                {
                    Utils.DataSetToExcel(table, FileName, title);
                }
                sw.Stop();
                Console.WriteLine("执行完成,共用时" + Utils.MinuteToHour(sw.ElapsedMilliseconds / 1000));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        public static Dictionary<string, string> GetDeviceName(string deviceid)
        {
            Dictionary<string, string> ieDic = list.SingleOrDefault(o => o["DeviceID"] == deviceid);
            if (ieDic != null && ieDic.Count() > 0)
            {
                return ieDic;
            }
            else
            {
                string sql = "select case when DeviceName='' then SerialNumber else DeviceName end DeviceName,SerialNumber,u.UserName from Devices d inner join users u on u.userid=d.UserID where deviceid=@deviceid";
                Dictionary<string, string> dic = s.Selects(sql, new SqlParameter[] { new SqlParameter("deviceid", deviceid) }).toDictionary();
                dic["DeviceID"] = deviceid;
                list.Add(dic);
                return dic;
            }

        }
        public void test1()
        {

        }

        private static object lock_log1 = new object();
        public static void log(string LogStr, string Name = null)
        {

            string ProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            string FilePath = "";
           //  p = "D://Log";
           // string p = System.AppDomain.CurrentDomain.BaseDirectory + "logs";  //HttpContext.Current.Request.PhysicalApplicationPath + "logs";
            string fileName = "";
            if (Name == null)
                fileName = DateTime.Now.ToString("yyyy-MM-dd") + "-MG_WeChat-" + ProcessName + ".log";
            else
                fileName = Name;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FilePath = path + "/" + fileName;
            try
            {
                  lock (lock_log1)
                 {
                   using (FileStream fs = new FileStream(FilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        LogStr = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff") + "  \t" + LogStr;
                        using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                        {
                            sw.WriteLine(LogStr);
                        }
                    }
                 } 
            }
            catch (Exception ex)
            {
                throw ex;
                //Pay.WeixinPay.lib.Log.Info("Utils.Log", "写入Log出错" + ex.Message + ",内容:" + LogStr);
            } 
        }

        private static object lock_log = new object();

        public static void log1(string LogStr, string Name = null)
        {
            string ProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            string FilePath = ""; 
            //string p = System.AppDomain.CurrentDomain.BaseDirectory + "logs";  //HttpContext.Current.Request.PhysicalApplicationPath + "logs";
            string fileName = "";
            if (Name == null)
                fileName = DateTime.Now.ToString("yyyy-MM-dd") + "-MG_WeChat1-" + ProcessName + ".log";
            else
                fileName = Name;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            FilePath = path + "/" + fileName;

            try
            {
                lock (lock_log)
                {
                    LogStr = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff") + "  \t" + LogStr;
                    using (StreamWriter sw = new StreamWriter(FilePath, true, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(LogStr);
                         
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Pay.WeixinPay.lib.Log.Info("Utils.Log", "写入Log出错" + ex.Message + ",内容:" + LogStr);
            }
        }
    }
}
