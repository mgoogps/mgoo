using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MG_DAL
{
    public class ILog
    {
        //public static string path = HttpContext.Current.Request.PhysicalApplicationPath + "logs";
       // public static string path =  "D://Log";
        public static string path = System.AppDomain.CurrentDomain.BaseDirectory + "logs";
        public static void WriteLog(string content, string type = "", string className = "")
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
            string filename = path + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "_DAL" + ".log";//用日期对日志文件命名
            //创建或打开日志文件，向日志文件末尾追加记录
            StreamWriter mySw = File.AppendText(filename);
            try
            {
                if (!Directory.Exists(path))//如果日志目录不存在就创建
                {
                    Directory.CreateDirectory(path);
                } 
                //向日志文件写入内容
                string write_content = time + " " + type + " " + className + ": " + content;
                mySw.WriteLine(write_content); 
            }
            catch (Exception ex)
            {
                //WriteLog("WriteLog Error:");
            }
            finally
            {
                //关闭日志文件
                mySw.Close();
            }
           
        }
    }
}
 