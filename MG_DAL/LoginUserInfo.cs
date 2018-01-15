using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.SessionState;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection; 
namespace MG_DAL
{

    public class LoginUserInfo:  IRequiresSessionState
    {
        /// <summary>
        /// 当前用户ＩＤ
        /// </summary>
        private String userID;
        public string UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        /// <summary>
        /// 当前用户账号
        /// </summary>
        private String userName;

        /// <summary>
        /// 当前用户登录名
        /// </summary>
        private String loginName;

        /// <summary>
        /// 登录时间
        /// </summary>
        private DateTime loginTime;

        private String firstName;

        /// <summary>
        /// 2:经销商,有管理后台的  1:普通用户,没有管理后台的
        /// </summary>
        private String userType;

        /// <summary>
        /// 1 超级管理员   0 普通用户
        /// </summary>
        private String superAdmin;

        /// <summary>
        /// 用IMEI号登录的IMEI
        /// </summary>
        private String serialNumber;

        /// <summary>
        /// 当前登录所使用的地图(百度、高德)
        /// </summary>
        private MapType mapType;
        /// <summary>
        /// 登录方式，是IMEI号登录还是用户账号登录
        /// 0 是用户账号
        /// 1 是IMEI号登录
        /// </summary>
        private LoginType loginType;


        private String deviceName;

        private String deviceID;

        private String parentID;

        private String cellPhone;

        private String address;

        private String eMail;

        private String identifies;

        
        public string ToKen
        {
            get
            {
                return toKen;
            }
            set
            {
                toKen = value;
            }
        }

        private String toKen;
        /// <summary>
        /// 用户账号登录
        /// </summary>
        /// <param name="_UserID"></param>
        /// <param name="_UserName"></param>
        /// <param name="_LoginName"></param>
        /// <param name="_FirstName"></param>
        /// <param name="_UserType"></param>
        /// <param name="_SuperAdmin"></param>
        /// <param name="_LoginTime"></param>
        public LoginUserInfo(String _UserID, String _UserName, String _LoginName, String _FirstName, String _UserType, String _SuperAdmin, DateTime _LoginTime)
        {
            this.UserID = _UserID;
            this.UserName = _UserName;
            this.LoginName = _LoginName;
            this.FirstName = _FirstName;
            this.UserType = _UserType;
            this.SuperAdmin = _SuperAdmin;
            this.LoginTime = _LoginTime;
            this.LoginType =  LoginType.User;
        }

        /// <summary>
        /// 用IMEI号登录的
        /// </summary>
        /// <param name="_SerialNumber"></param>
        /// <param name="_UserID"></param>
        public LoginUserInfo(String _SerialNumber, String _UserID, String _DeviceID, String _UserName)
        {
            this.SuperAdmin = "0";
            this.UserType = "1";
            this.LoginType =  LoginType.Imei;
            this.SerialNumber = _SerialNumber;
            this.UserID = _UserID;
            this.DeviceID = _DeviceID;
            if (_UserName.Trim() == "")
                this.UserName = _SerialNumber;
            else
                this.UserName = _UserName;
        }

        public LoginUserInfo()
        {

        }

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <returns></returns>
        public LoginUserInfo GetSession()
        {
            HttpSessionState _session = HttpContext.Current.Session;
            return _session[this.SessionName] as LoginUserInfo;
        }
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        private string sessionName;
        public string SessionName
        {
            get
            {
                if (sessionName == null)
                {
                    sessionName = "LoginUserInfo";
                }
                return sessionName;
            }

            set
            {
                sessionName = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public string LoginName
        {
            get
            {
                return loginName;
            }

            set
            {
                loginName = value;
            }
        }

        public DateTime LoginTime
        {
            get
            {
                return loginTime;
            }

            set
            {
                loginTime = value;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        /// <summary>
        /// 2 是经销商，1是用户，3是IMEI登录
        /// </summary>
        public string UserType
        {
            get
            {
                return userType;
            }

            set
            {
                userType = value;
            }
        }

        public string SuperAdmin
        {
            get
            {
                return superAdmin;
            }

            set
            {
                superAdmin = value;
            }
        }

        public string SerialNumber
        {
            get
            {
                return serialNumber;
            }

            set
            {
                serialNumber = value;
            }
        }

        public LoginType LoginType
        {
            get
            {
                return loginType;
            }

            set
            {
                loginType = value;
            }
        }

        public string DeviceID
        {
            get
            {
                return deviceID;
            }

            set
            {
                deviceID = value;
            }
        }

        public string ParentID
        {
            get
            {
                if (parentID == null)
                {
                    return "0";
                }
                return parentID;
            }

            set
            {
                parentID = value;
            }
        }

        public string CellPhone
        {
            get
            {
                return cellPhone;
            }

            set
            {
                cellPhone = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public string EMail
        {
            get
            {
                return eMail;
            }

            set
            {
                eMail = value;
            }
        }

        public string Identifies
        {
            get
            {
                return identifies;
            }

            set
            {
                identifies = value;
            }
        }

        public MapType MapType
        {
            get
            {
                return mapType;
            }

            set
            {
                mapType = value;
            }
        }

        public string DeviceName
        {
            get
            {
                return deviceName;
            }

            set
            {
                deviceName = value;
            }
        }
    }
    public enum MapType
    {
        BAIDU = 0,
        AMAP = 1,
       // GOOGLE =2 
    }
    public enum LoginType
    {
        User = 0,
        Imei =1
    }
}
