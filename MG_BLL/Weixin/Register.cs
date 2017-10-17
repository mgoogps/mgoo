using MG_DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MG_BLL.Weixin
{
    public class Register
    {

        public bool MgRegister(string phone, string password,string username)
        {
            SqlParameter[] parms = new SqlParameter[] {
               new SqlParameter("@ParentID","1391"),
               new SqlParameter("@UserName",string.IsNullOrEmpty(username) ? phone:username),
               new SqlParameter("@LoginName",phone),
                new SqlParameter("@Password",password),
               new SqlParameter("@UserType","1"),
                new SqlParameter("@Gender","0"),
                new SqlParameter("@TimeZone","China Standard Time") ,
              //new SqlParameter("@Address1",""),
              // new SqlParameter("@Address2",""),
              // new SqlParameter("@Country","-1"),
              // new SqlParameter("@State","-1"),
              // new SqlParameter("@HomePhone",""),
              // new SqlParameter("@WorkPhone",""),
              // new SqlParameter("@CellPhone",phone),
              //new SqlParameter("@SMSEmail",""),
              // new SqlParameter("@PrimaryEmail",""),
              // new SqlParameter("@SecondaryEmail",""),
              // new SqlParameter("@Status","-1"),
              // new SqlParameter("@UpdateTime", DateTime.Now),
              // new SqlParameter("@Created",DateTime.Now),
              // new SqlParameter("@Deleted","0"),
              //new SqlParameter("@SuperAdmin","0"),
              // new SqlParameter("@AllDeviceCount","0"),
              // new SqlParameter("@ActivationCount","0"),
              // new SqlParameter("@MoneyCount","0")
            };
            string ParentLoginName = "zzzc"+DateTime.Now.ToString("yyyyMM");
            string ParentUserName  = "自主注册"+ DateTime.Now.ToString("yyyyMM");
            string ParentPassword = new Random().Next(100000,999999).ToString();
            string strSql = @"declare @UserID int 
                select @UserID = UserID from users where loginname = '"+ ParentLoginName + @"' 
                if  (@UserID IS NULL)
                begin 
                   insert into Users(ParentID, UserName, LoginName, Password, UserType, Gender , TimeZone,  Country, State, Status, UpdateTime, Created, Deleted, SuperAdmin, AllDeviceCount, ActivationCount, MoneyCount) 
                   values (1391,'" + ParentUserName + "','"+ ParentLoginName + "','"+ ParentPassword + @"',2,0,'China Standard Time',-1,-1,-1,getdate(),getdate(),0,0,0,0,0) select @UserID = @@identity
                end
                insert into Users(ParentID, UserName, LoginName, Password, UserType, Gender , TimeZone,  Country, State, Status, UpdateTime, Created, Deleted, SuperAdmin, AllDeviceCount, ActivationCount, MoneyCount) 
	values (@UserID,@UserName,@LoginName,@Password,@UserType,@Gender,@TimeZone,-1,-1,-1,getdate(),getdate(),0,0,0,0,0) 
                  ";
            //strSql = "insert into Users values(@ParentID, @UserName, @LoginName, @Password, @UserType, @Gender, @FirstName, @MiddleName, @LastName, @TimeZone, @Address1, @Address2, @Country, @State, @HomePhone, @WorkPhone, @CellPhone, @SMSEmail, @PrimaryEmail, @SecondaryEmail, @Status, @UpdateTime, @Created, @Deleted, @SuperAdmin, @AllDeviceCount, @ActivationCount, @MoneyCount)";
            SQLServerOperating s = new SQLServerOperating();
            bool success = s.ExecuteSql(strSql, parms) > 0; 
            return success;
        }

        public string SMSCodes(string phone)
        {
            try
            {
                string exist = Utils.GetCache<object>("SMS" + phone).toStringEmpty();
                if (!exist.Equals(string.Empty))
                {
                    // HttpRuntime.Cache.Insert("SMS" + phone, exist, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
                    //  return string.Empty;
                }

                MG_DAL.MgoogpsWebClient mwc = new MG_DAL.MgoogpsWebClient();
                mwc.RequestUrl = "https://api.miaodiyun.com/20150822/industrySMS/sendSMS";
                //ACCOUNT SID +AUTH TOKEN + timestamp
                string ACCOUNTSID = "dba0b636a1b74391bb1503beaff97bfc";
                string AUTHTOKEN = "76a183e571014f3ea04138bdb5387c24";
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                int SecurityCode = new Random().Next(100000, 999999);
                string smsContent = string.Format("【美谷科技】您的验证码是{0} ，{1}分钟输入有效。", SecurityCode, 10);

                string sig = FormsAuthentication.HashPasswordForStoringInConfigFile(ACCOUNTSID + AUTHTOKEN + timestamp, "MD5").ToLower();

                byte[] postdata = Encoding.UTF8.GetBytes(string.Format("accountSid={0}&smsContent={1}&to={2}&timestamp={3}&sig={4}&respDataType=JSON", ACCOUNTSID, smsContent, phone, timestamp, sig));
                mwc.RequestContentType = "application/x-www-form-urlencoded";

                mwc.RequestPostData = postdata;

                string reulst = mwc.RequestSend();
                JavaScriptSerializer js = new JavaScriptSerializer();
                Entity.SMSResult smsresult = js.Deserialize<Entity.SMSResult>(reulst);
                if (smsresult.RespCode.Equals("00000"))
                {
                    HttpRuntime.Cache.Insert("SMS" + phone, SecurityCode, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
                    return "";
                }
                return smsresult.RespCode;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool VerificationPhone(string phone)
        {
            MG_DAL.SQLServerOperating s = new MG_DAL.SQLServerOperating();
            string exist = s.Select("select count(*) from Users where deleted=0 and loginname=@loginname", new SqlParameter[] { new SqlParameter("loginname", phone) });
            return Convert.ToInt32(exist) > 0;
        }

        public bool VerificationCode(string phone, string code)
        {
            string SecurityCode = HttpRuntime.Cache.Get("SMS" + phone).toStringEmpty();
            if (SecurityCode.Equals(code) || SecurityCode.Equals(string.Empty))
            {
                HttpRuntime.Cache.Remove("SMS" + phone);
            }
            return SecurityCode.Equals(code);
        }

        public ajaxResult RetrievePassword(string phone, string password)
        {
            try
            {

                string strSql = "select COUNT(UserID) from users where deleted = 0 and loginname=@loginname";

                strSql = @"if exists(select UserID from Users where deleted=0 and loginname=@loginname)
                            begin
                                update Users set password=@password where deleted=0 and loginname=@loginname select 1 
                            end
                           else
                             select -2";
                SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("loginname", phone), new SqlParameter("password", password) };
                ajaxResult ar = new MG_BLL.ajaxResult();
                SQLServerOperating s = new SQLServerOperating();
                string count = s.Select(strSql, parameter);
                if (count.Equals("1"))
                {
                    ar.StatusCode = statusCode.Code.success;
                    ar.Message = "密码重设成功.";
                    ar.Result = "";
                    //strSql = "update Users set password=@password where loginname=@loginname";
                    //parameter = new SqlParameter[] { new SqlParameter("loginname", phone), new SqlParameter("password", password) };
                    //int status = s.ExecuteSql(strSql, parameter);
                    //if (status > 0)
                    //{
                    //    return true;
                    //}
                }
                else
                {
                    ar.StatusCode = statusCode.Code.failure;
                    ar.Message = "密码重设失败.";
                    ar.Result = ""; 
                }
                return ar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
