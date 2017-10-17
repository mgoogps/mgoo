using com.igetui.api.openservice;
using com.igetui.api.openservice.igetui;
using com.igetui.api.openservice.igetui.template;
using MG_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.App
{
    public class AppPush
    {
        //http的域名
        // private static String HOST = "http://sdk.open.api.igexin.com/apiex.htm";
        //https的域名
        //private String HOST = "https://api.getui.com/apiex.htm";
        //private String APPID = "y0eemtJyq0ABouBDhD7BeA";
        //private String APPKEY = "dWiFDSNfxF6zwJpTZhcRm6";
        //private String MASTERSECRET = "WJvtGnzEZl8KzaJRLS9an";

        private String Title = "";
        private String Text = "";
        private String ExceptionID = "";
        private String ExceptionDate = "";
        private String NotificationType = "";
        public SQLServerOperating sqlOper = null;
        public AppPush(string Title,string Text,string ExceptionID,string ExceptionDate,string NotificationType)
        {
            this.Title = Title;
            this.ExceptionDate = ExceptionDate.toDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            this.Text = Text + "-"+ this.ExceptionDate;
            this.ExceptionID = ExceptionID; 
            this.NotificationType = NotificationType;
        }
        public AppPush()
        { }
        private SQLServerOperating GetSQLServerOperating()
        {
            if (sqlOper!=null)
            {
                return sqlOper;
            }
            else
            {
                return new SQLServerOperating();
            }
        }
        public void push(string UserID)
        {
            try
            {
                string strSql = "select ClientID,Token,AppID,AppKey,MasterSecret,AppSecret,PackageName,m.OS,uc.PushShock,uc.PushAudio from MobileAppInfo m inner join apps a on m.AppsID=a.ID inner join usersconfig uc on uc.UserID=m.UserID where m.UserID=@userid and a.PackageName!='HBuilder' and m.LastDate>dateadd(MM,-1, getdate())";
                SQLServerOperating s = GetSQLServerOperating();
                
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("userid", UserID) });
                int len = dt.Rows.Count;
                if (len > 0)
                {
                    // GetuiPush.Push push = new GetuiPush.Push();
                    XiaoMiPush.Push push = new XiaoMiPush.Push();
                    push.PushAudio = dt.Rows[0]["PushAudio"].toStringEmpty() == "0";
                    push.PushShock = dt.Rows[0]["PushShock"].toStringEmpty() == "0"; 
                    // if (push.PushShock && push.PushShock)    
                    push.DefaulSound = -1; //声音 并且 震动  
                    if (push.PushAudio && !push.PushShock) 
                        push.DefaulSound = 1; //声音，不震动
                    else if (!push.PushAudio && !push.PushShock)
                        push.DefaulSound = 2; //只震动
                    else if (!push.PushAudio && !push.PushShock)
                        push.DefaulSound = 4; //LED灯光提示，没有声音没有震动
                    List<string> regids = new List<string>();
                    List<string> packages = new List<string>();
                    List<Task> taskList = new List<Task>();
                    TaskFactory taskFactory = new TaskFactory();
                    for (int i = 0; i < len; i++)
                    {
                        int index = i;
                        Task task = taskFactory.StartNew(() =>
                        {
                            DataRow row = dt.Rows[index];
                            regids.Add(row["ClientID"].ToString());
                            packages.Add(row["PackageName"].ToString());
                            push.PushMessageToSingle(row["ClientID"].ToString(), Title, this.Text, row["AppSecret"].ToString(), row["PackageName"].ToString(), this.NotificationType, row["OS"].ToString().ToLower(), this.ExceptionID);
                        });
                        taskList.Add(task);
                    }
                    //Task.WaitAll(taskList.ToArray()); 
                }
            }
            catch (Exception ex)
            {
               // Console.WriteLine(ex.Message);
                Utils.log("AppPush push Error:"+ex.Message+ ",Source:" + ex.Source+ ",StackTrace" + ex.StackTrace);
            }
        }

       // public void ExpiredPush(string PackageName, string AppSecret, string ClientID, string OS, string DeviceID, string DeviceName, string ExpiredDate)
       // {
           // XiaoMiPush.Push push = new XiaoMiPush.Push();
          // push.ExpiredPush(PackageName,AppSecret,ClientID,OS,DeviceID,DeviceName,ExpiredDate);
       // }

    }
}
