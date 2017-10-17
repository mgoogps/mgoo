using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using MG_BLL;
using System.Web.Script.Services;

namespace MG_GPS.AjaxService
{
    /// <summary>
    /// LoginAjax 的摘要说明
    /// </summary>
    [WebService(Namespace = "mgoogps")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class LoginAjax : System.Web.Services.WebService
    {

        [WebMethod(EnableSession=true,Description="登录方法")] 
        [ScriptMethod(UseHttpGet = false)]
        public string Login(String loginname, String loginpassword)
        {
            string pwdMd5 = loginpassword.Substring(8, 16);
            MG_BLL.BllLogin login = new MG_BLL.BllLogin();
            bool loginBool = login.WebSystemLogin_Bll(loginname, loginpassword ,"System");
            if (loginBool )
            {
                return "success";
            }
            else
            {
                return "error";
            }
            //    SqlParameter[] list = new SqlParameter[2]{
            //      new SqlParameter("@LoginName",SqlDbType.VarChar),
            //      new SqlParameter("@Password",SqlDbType.VarChar)
            //    };
            //    list[0].Value = loginname;
            //    list[1].Value = pwd;
            //    //Utils.language = language.Trim() == "" ? Utils.language : language;
            //   // MG_BLL.UserInfo users = new MG_BLL.UserInfo();
            //    SQLHelper _sqlHelper = new SQLHelper();
            //    Hashtable userDr = _sqlHelper.Select("select UserID,UserName,LoginName,FirstName,CellPhone,SuperAdmin,UserType from Users where LoginName = @LoginName and Password=@Password and Deleted = 0", list);
            //    if (userDr != null && userDr.Count > 0)
            //    {

            //        Utils.SetSession("UserInfo", new LoginUserInfo(userDr["UserID"].ToString(), userDr["UserName"].ToString(), userDr["LoginName"].ToString(), userDr["FirstName"].ToString(), userDr["UserType"].ToString(), userDr["SuperAdmin"].ToString(), DateTime.Now));
            //        return "success";
            //    }
            //    else
            //    {
            //        userDr = _sqlHelper.Select(" select UserID,UserName,LoginName,FirstName,CellPhone,SuperAdmin,UserType from Users where CellPhone = @LoginName and Password= @Password and Deleted =0", list);
            //        if (userDr != null && userDr.Count > 0)
            //        {
            //           Utils.SetSession("UserInfo", new LoginUserInfo(userDr["UserID"].ToString(), userDr["UserName"].ToString(), userDr["LoginName"].ToString(), userDr["FirstName"].ToString(), userDr["UserType"].ToString(), userDr["SuperAdmin"].ToString(), DateTime.Now));
            //            return "success";
            //        }
            //        else
            //        {
            //            userDr = _sqlHelper.Select(" select d.DeviceID,d.SerialNumber,d.DeviceName, UserID from Devices d inner join  LKLocation l on l.DeviceID = d.DeviceID where d.SerialNumber = @LoginName and d.DevicePassword = @Password and deleted=0 ", list);
            //            if (userDr != null && userDr.Count > 0)
            //            {
            //                 Utils.SetSession("UserInfo", new LoginUserInfo(userDr["SerialNumber"].ToString(), userDr["UserID"].ToString(), userDr["DeviceID"].ToString(), userDr["DeviceName"].ToString()));
            //                return "success";
            //            }
            //        }
           // return "{\"aa\":\"11\"}";
           // } 
       }
    }
}
