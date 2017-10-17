using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MgooGps.com
{
    public class UserInfo
    {
        /// <summary>
        /// 当前用户ＩＤ
        /// </summary>
        public String UserID { private set; get; }

        /// <summary>
        /// 当前用户账号
        /// </summary>
        public String UserName { private set; get; }

        /// <summary>
        /// 当前用户登录名
        /// </summary>
        public String LoginName { private set; get; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { private set; get; }

        public String FirstName { private set; get;}

        /// <summary>
        /// 2:经销商,有管理后台的  1:普通用户,没有管理后台的
        /// </summary>
        public String UserType { private set; get; }

        /// <summary>
        /// 1 超级管理员   0 普通用户
        /// </summary>
        public String SuperAdmin { private set; get; }

        /// <summary>
        /// 用IMEI号登录的IMEI
        /// </summary>
        public String SerialNumber { private set; get; }

        /// <summary>
        /// 登录方式，是IMEI号登录还是用户账号登录
        /// 0 是用户账号
        /// 1 是IMEI号登录
        /// </summary>
        public String LoginType { private set; get; }

        public String DeviceID { private set; get; }

        /// <summary>
        /// 是否有报警短信通知的权限
        /// </summary>
        public bool IsSMSNoticePermission { private set; get; }

        /// <summary>
        /// 是否可以修改子用户的联系号码(用做短信通知用)
        /// </summary>
        public bool IsUpdateSonInfo { set; get; }
        /// </summary>
        /// 用户账号登录
        /// </summary>
        /// <param name="_UserID"></param>
        /// <param name="_UserName"></param>
        /// <param name="_LoginName"></param>
        /// <param name="_FirstName"></param>
        /// <param name="_UserType"></param>
        /// <param name="_SuperAdmin"></param>
        /// <param name="_LoginTime"></param>
        /// <param name="_LoginTime"></param>
        /// <param name="_IsSMSNoticePermission">是否有设置短信通知的权限</param>
        public UserInfo(String _UserID, String _UserName, String _LoginName, String _FirstName, String _UserType, String _SuperAdmin, DateTime _LoginTime,bool _IsSMSNoticePermission = false)
        { 
            this.UserID = _UserID;
            this.UserName = _UserName;
            this.LoginName = _LoginName;
            this.FirstName = _FirstName;
            this.UserType = _UserType;
            this.SuperAdmin = _SuperAdmin;
            this.LoginTime = _LoginTime;
            this.LoginType = "0";
            this.IsSMSNoticePermission = _IsSMSNoticePermission ;
        }

        /// <summary>
        /// 用IMEI号登录的
        /// </summary>
        /// <param name="_SerialNumber"></param>
        /// <param name="_UserID"></param>
        public UserInfo(String _SerialNumber, String _UserID, String _DeviceID, String _UserName)
        {
            this.SuperAdmin = "0";
            this.UserType = "1";
            this.LoginType = "1";
            this.SerialNumber = _SerialNumber;
            this.UserID = _UserID;
            this.DeviceID = _DeviceID;
            if (_UserName.Trim() == "")
                this.UserName = _SerialNumber;
            else
                this.UserName = _UserName;
        }

        public UserInfo()
        {

        }
    }
}