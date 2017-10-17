using System.Data;

namespace MG_BLL.Common.lib
{
    public class Permission
    {
        /// <summary>
        /// 根据用户ＩＤ判断该用户是否在含短信通知功能用户账号体系下的
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static bool IsSMSNotice(string UserID)
        {
            //var ids = System.Configuration.ConfigurationManager.AppSettings["SMSNotice"].ToString();
            //if (string.IsNullOrEmpty(ids))
            //{
            //    return false;
            //}
            // UserID 是4927 的表示 该用户下的账号有 报警短信通知功能
            string strSql =  "with temp(UserID,ParentID) as"+
                                "(  select UserID, ParentID from Users where UserID = " + lib.Config.SMSNoticeUserID+
                                    "union all  select Users.UserID, Users.ParentID from Users, temp  where Users.ParentID = temp.UserID and users.Deleted = 0"+
                                 ")select UserID from temp where UserID= "+UserID;
            MG_DAL.SQLServerOperating s = new MG_DAL.SQLServerOperating();
            DataTable dt=  s.Selects(strSql);
            if (dt.Rows.Count <= 0)
            {
                return false;
            }
            //List<Dictionary<string, string>> listDic = s.Selects(strSql).toListDictionary();

            ////目前只有澳博旗下所有用户 才会 发送 报警短信通知  3437 澳博美谷 总账号的UserID  870是xietong
            //if (listDic.Where(l => l.ContainsValue(UserID)).Count() <= 0) //
            //{
            //    return false;
            //}
            return true;
        }
    }
}
