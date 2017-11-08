using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.Common
{
    public sealed class Log
    {
        public static int LOG_LEVEL = 3;

        public static string LOG_PATH { set; get; }

        private static object LOG_LOCK = new object();

        /// <summary>
        /// 禁止实例化
        /// </summary>
        private Log() { }

        /// <summary>
        /// 向日志文件写入调试信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classObj">当前类</param>
        /// <param name="content">写入内容</param>
        public static void Debug<T>(T classObj, params string[] content)
        {
            if (LOG_LEVEL >= 3)
            {
                WriteLog("Debug", classObj.GetType().ToString(), string.Join(",", content));
            }
        }
        /// <summary>
        /// 向日志文件写入运行时信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classObj">当前类</param>
        /// <param name="content">写入内容</param>
        public static void Info<T>(T classObj, params string[] content)
        {
            if (LOG_LEVEL >= 2)
            {
                WriteLog("Run", classObj.GetType().ToString(), string.Join(",", content));
            }
        }

        /// <summary>
        /// 向日志文件写入出错信息
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="err">异常对象</param>
        public static void Error<T>(T classObj, Exception err)
        {
            if (LOG_LEVEL >= 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.NewLine + " ---------------------Error Start---------------------------" + Environment.NewLine);
                sb.Append(err.Message);
                sb.Append(Environment.NewLine);
                sb.Append(err.StackTrace);
                sb.Append(Environment.NewLine);
                sb.Append(err.TargetSite);
                sb.Append(Environment.NewLine + " ---------------------Error End---------------------------" + Environment.NewLine);

                if (classObj is String)
                    WriteLog("Error", classObj.ToString(), sb.ToString());
                else
                    WriteLog("Error", classObj.GetType().ToString(), sb.ToString());
            }
        }

        /**
        * 实际的写日志操作
        * @param type 日志记录类型
        * @param className 类名
        * @param content 写入内容
        */
        private static void WriteLog(string type, string className, string content)
        {
            string filename = "";
            try
            {
                if (string.IsNullOrEmpty(LOG_PATH))
                {
                    LOG_PATH = @"D:\MgooInterface\logs";
                }
                if (!System.IO.Directory.Exists(LOG_PATH))//如果日志目录不存在就创建
                {
                    System.IO.Directory.CreateDirectory(LOG_PATH);
                }
                //在网站根目录下创建日志目录
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
                filename = LOG_PATH + "/" + DateTime.Now.ToString("yyyy-MM-dd") +"-"+ type + ".log";//用日期对日志文件命名
                lock (LOG_LOCK)
                {
                    //创建或打开日志文件，向日志文件末尾追加记录
                    StreamWriter mySw = File.AppendText(filename);

                    //向日志文件写入内容
                    string write_content = time + " " + className + ": " + content;
                    mySw.WriteLine(write_content);

                    //关闭日志文件
                    mySw.Close();
                }
            }
            catch (Exception ex)
            {
                 Utils.log("WriteLog Error:" + ex.Message + ",type：" + type + ",className：" + className + ",content:" + content + ",filename:" + filename + ",堆栈：" + ex.Source + "，" + ex.StackTrace);
            }
        }
    }
}
