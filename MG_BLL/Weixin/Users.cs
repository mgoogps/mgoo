using MG_DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.Weixin
{
    public class Users
    {
        private Common.AuthHeader myHeader = new Common.AuthHeader();
        public Users(Common.AuthHeader header)
        {
            this.myHeader = header;
        }
        public string GetUsersInfoByID(string userid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                if (myHeader.UserID.Equals(userid))
                {
                    string strSql = "select UserName,LoginName,FirstName,CellPhone,PrimaryEmail,Address1 [Address] from users where userid=@userid";
                    SQLServerOperating s = new SQLServerOperating();
                    dic = s.Selects(strSql, new SqlParameter[] { new SqlParameter("userid", userid) }).toDictionary();
                }
                return Utils.ToJson(dic);
            }
            catch (Exception)
            {
                return Utils.ToJson(dic);
            }
        }

        public string UpdateUsersInfoByID(string firstName, string callphone, string primaryemail, string address, string userid)
        {
            try
            {
                string strSql = "update users set FirstName=@FirstName,Address1=@Address,CellPhone=@CellPhone,PrimaryEmail=@PrimaryEmail where deleted=0 and userid=@userid";
                SQLServerOperating s = new SQLServerOperating();
                int status = s.ExecuteSql(strSql, new SqlParameter[] {
                                                    new SqlParameter("FirstName", firstName), new SqlParameter("Address", address),
                                                    new SqlParameter("CellPhone", callphone), new SqlParameter("PrimaryEmail", primaryemail),
                                                    new SqlParameter ("userid",userid)
                                                    });
                if (status > 0)
                {
                    return Utils.GetResult("修改成功.", statusCode.Code.success);
                }
                else
                {
                    return Utils.GetResult("修改失败.", statusCode.Code.failure);
                }
            }
            catch (Exception ex)
            {
                return Utils.GetResult(ex.Message, statusCode.Code.error);
            }
        }

        public string UpdatePassword(string userid, string oldpwd, string newpwd)
        {
            string strSql = "update users set Password=@newPwd where UserID=@UserID and Password=@oldPwd";
            SQLServerOperating s = new SQLServerOperating();
            int c = s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("UserID", userid), new SqlParameter("newPwd", newpwd), new SqlParameter("oldPwd", oldpwd) });
            if (c > 0)
            {
                return Utils.GetResult("修改成功,下次登录请用新密码.", statusCode.Code.success);
            }
            else
            {
                return Utils.GetResult("修改密码失败.", statusCode.Code.failure);
            }
        }
    }
}
