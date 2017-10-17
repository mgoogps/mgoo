using MG_DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.Weixin
{
    public class Groups
    {
        private Common.AuthHeader myHeader = new Common.AuthHeader();
        public Groups(Common.AuthHeader header)
        {
            this.myHeader = header;
        }
        public Groups()
        { }
        public string AddGroups(string userid, string groupname)
        {
            try
            {
                if (string.IsNullOrEmpty(groupname))
                {
                    return Utils.GetResult("分组名字不能为空.", statusCode.Code.failure);
                }
                string strSql = "insert into groups(GroupName, UserID, Username, Description, Created, GroupType, AccountID, Deleted)values( @groupname, @userid, '', '', GETDATE(), -1, -1, 0)  select @@IDENTITY";

                SQLServerOperating s = new SQLServerOperating();
                string status = s.Select(strSql, new SqlParameter[] { new SqlParameter("groupname", groupname), new SqlParameter("userid", userid) });
                if (status != string.Empty)
                { 
                    return Utils.GetResult("添加成功.", statusCode.Code.success, status);
                }
                else
                {
                    return Utils.GetResult("添加失败", statusCode.Code.failure, "");
                }
            }
            catch (Exception ex)
            {
                Utils.log("AddGroups ERROR:"+ex.Message);
               return Utils.GetResult("添加失败", statusCode.Code.error);
            }
         
        }

        public string UpdateGroups(string groupid,string groupname)
        {
            try
            {
                if (string.IsNullOrEmpty(groupid))
                {
                    return Utils.GetResult("分组ID不能为空.", statusCode.Code.failure);
                }
                if (string.IsNullOrEmpty(groupname))
                {
                    return Utils.GetResult("分组名字不能为空.", statusCode.Code.failure);
                }
                string strSql = "update groups set GroupName=@GroupName where GroupID=@GroupID and UserID=@UserID ";
                SQLServerOperating s = new SQLServerOperating();
                int rows = s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("GroupName", groupname), new SqlParameter("GroupID", groupid),new SqlParameter ("UserID", myHeader.UserID) });
                if (rows > 0) {
                    return Utils.GetResult("修改成功.", statusCode.Code.success, "");
                }
                else
                {
                    return Utils.GetResult("修改失败", statusCode.Code.failure, "");
                }
            }
            catch (Exception ex)
            {
                return Utils.GetResult(ex.Message, statusCode.Code.error, ""); 
            }
           
        }

        public string DeleteGroups(string groupid)
        {
            try
            {
                if (groupid.Trim() == "-1" || string.IsNullOrEmpty(groupid))
                {
                    return "该分组不能删除.";
                }
                SQLServerOperating s = new SQLServerOperating();
                string strSql = "update Devices set GroupID=-1 where GroupID=@GroupID";
                int count = s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("GroupID", groupid) });
                // strSql = "select count(*) from devices where groupid=@GroupID and UserID=@UserID";

                // string count = s.Select(strSql, new SqlParameter[] { new SqlParameter("GroupID", groupid), new SqlParameter("UserID", myHeader.UserID) });
                string msg = "";
                if (count > 0)
                {
                    msg = "该分组下的设备以移到默认组下.";
                } 
                strSql = "delete from groups where groupid= @GroupID";
                int rows = s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("GroupID", groupid) });
                return rows > 0 ? Utils.GetResult("删除成功!"+msg, statusCode.Code.success) : Utils.GetResult("删除分组失败!" + msg, statusCode.Code.failure) ;
            }
            catch (Exception ex)
            {
                Utils.log("DeleteGroups Error:"+ex.Message);
                return Utils.GetResult(ex.Message, statusCode.Code.failure);
            }
        }
    }
}
