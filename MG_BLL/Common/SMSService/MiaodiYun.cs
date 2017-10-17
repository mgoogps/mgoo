using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MG_BLL.Common.SMSService
{
    class MiaodiYun
    {
        private void SendSMS(string phone,string content)
        {
            if (string.IsNullOrEmpty(phone.Trim()))
            {
                return;
            }
            MG_DAL.MgoogpsWebClient mwc = new MG_DAL.MgoogpsWebClient();
            mwc.RequestUrl = "https://api.miaodiyun.com/20150822/industrySMS/sendSMS";
            //ACCOUNT SID +AUTH TOKEN + timestamp
            string ACCOUNTSID = "dba0b636a1b74391bb1503beaff97bfc";
            string AUTHTOKEN = "76a183e571014f3ea04138bdb5387c24";
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
         

            string sig = FormsAuthentication.HashPasswordForStoringInConfigFile(ACCOUNTSID + AUTHTOKEN + timestamp, "MD5").ToLower();

            byte[] postdata = Encoding.UTF8.GetBytes(string.Format("accountSid={0}&smsContent={1}&to={2}&timestamp={3}&sig={4}&respDataType=JSON", ACCOUNTSID, content, phone, timestamp, sig));
            mwc.RequestContentType = "application/x-www-form-urlencoded";

            mwc.RequestPostData = postdata;

            string reulst = mwc.RequestSend();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Entity.SMSResult smsresult = js.Deserialize<Entity.SMSResult>(reulst);

            var logName = "PushedMessage" + DateTime.Now.ToString("yyyyMM") + ".log";
           // Utils.log("UserID:" + UserID + ",phone:" + phone + ",DeviceName:" + DeviceName + ",MessageType:" + MessageType + ",smsresult.RespCode" + smsresult.RespCode, logName);

            if (smsresult.RespCode.Equals("00000"))
            {

            }
            // return smsresult.RespCode;
        }
        public void SMSNotice(string UserID,string UserName, string phone, string DeviceName, string MessageDate, string MessageType)
        {
            try
            {
                if (string.IsNullOrEmpty(phone))
                {
                    //return;
                }
                string[] phones = phone.Split(',', '，');
                if (phones.Length > 0)
                {
                    phone = string.Join(",", phones);
                }
                bool isTestAccunt = Common.lib.Permission.IsSMSNotice(UserID);

                if (!isTestAccunt)
                {
                    return;
                }
                if (isTestAccunt)
                {
                    //string strSql = "select CellPhone from users where userid=3437";
                    string strSql = "with temp(UserID,ParentID,CellPhone) as " +
                            "(select UserID, ParentID,CellPhone from Users where UserID = " + UserID +
                            "union all  select Users.UserID, Users.ParentID,Users.CellPhone from Users, temp  where Users.UserID = temp.ParentID and users.Deleted = 0" +
                            ")select CellPhone from temp where ParentID =  " + lib.Config.SMSNoticeUserID;
                    MG_DAL.SQLServerOperating s = new MG_DAL.SQLServerOperating();
                    string bigUserPhone = s.Select(strSql);
                    if (!string.IsNullOrEmpty(bigUserPhone))
                    {
                        phone  = string.IsNullOrEmpty( phone) ? bigUserPhone : phone+ "," + bigUserPhone;
                    }
                }
                string smsContent = string.Format("【美谷科技】用户{0}的设备{1}，在{2}触发了{3}，请留意。", UserName, DeviceName, MessageDate.toDateTime().ToString("yyyy-MM-dd HH:mm:ss"), MessageType);
                SendSMS(phone, smsContent);
            }
            catch (Exception ex)
            {
                Utils.log("MiaodiYun>SMSNotice Error:" + ex.Message);
                //return ex.Message;0
            }
        }

        public void SMSX30B(string SerialNumber,string DeviceName,string Date,string Phone)
        {
            if (string.IsNullOrEmpty(Phone))
            {
                return;
            }
            string[] phones = Phone.Split(',', '，');
            if (phones.Length > 0)
            {
                Phone = string.Join(",", phones);
            }
            MG_DAL.SQLServerOperating sqlHelper = new MG_DAL.SQLServerOperating();
            string strSql = @" select count(*) from[dbo].[CarCommandQueue]
                             where deviceid = '"+ SerialNumber + "' and datediff(mi, ResponseDate, getdate()) <= 1";
            string res = sqlHelper.Select(strSql);
            string state = "";
            if (Convert.ToInt32( res) > 0)
            {
              
            }
            else
            {
                strSql = @" insert into CarCommandQueue (  DeviceID, CommandText, CreateDate, IsSend, SendDate, IsResponse, ResponseDate, ResponseText, CommandName, IsOfflineSend, Infos, SendCount)
                         values('" + SerialNumber + "', 'KM', getdate(), 0, getdate(), 1, getdate() , 'Success!', '物理开锁', 0, '', 0) ";

                state = "非法";
                sqlHelper.ExecuteSql(strSql);
            }

            string content = string.Format("【美谷科技】尊敬的用户，你好。{0}在{1}发生了一次{2}开箱行为，如是本人操作，请忽略此短信。", DeviceName,Date, state);
            SendSMS(Phone, content); 
        }
    }
}
