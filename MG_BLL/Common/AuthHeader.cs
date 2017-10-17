using MG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Protocols;

namespace MG_BLL.Common
{
    public class AuthHeader : SoapHeader
    {
        private string _userID;
        private string _token;
        private string _identifies;
        private MapType _mapType;
        private LoginType _loginType;

        public string UserID
        {
            get
            {
                return _userID;
            }

            set
            {
                _userID = value;
            }
        }

        public string Token
        {
            get
            {
                return _token;
            }

            set
            {
                _token = value;
            }
        }

        public string Identifies
        {
            get
            {
                return _identifies;
            }

            set
            {
                _identifies = value;
            }
        }

        public MapType MapType
        {
            get
            {
                return _mapType;
            }

            set
            {
                _mapType = value;
            }
        }

        public LoginType LoginType
        {
            get
            {
                return _loginType;
            }

            set
            {
                _loginType = value;
            }
        }

       

        //public string Username;
        //public string Password;

        public AuthHeader()
        {
        }

        public AuthHeader(string identifies, string userid ,string token)
        {
            this._userID = userid;
            this._token = token;
            this._identifies = identifies;
        }
        /// <summary>
        /// 验证用户是否有权访问内部接口  
        /// </summary>
        /// <param name="identifies">设备唯一标识</param>
        /// <param name="userid">用户id</param>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        private bool isValid(string identifies,string userid, string token)
        {
            try
            {
                LoginUserInfo lu = HttpRuntime.Cache.Get(identifies + userid + token) as LoginUserInfo;

                if (!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(identifies)
                    && userid.Equals(lu.UserID) && token.Equals(lu.ToKen) && identifies.Equals(lu.Identifies))
                {
                    //if (lu.LoginType.ToLower() == "weixin")
                   // {
                       // Utils.LoginType = "weixin";
                  //  }
                    HttpRuntime.Cache.Insert(identifies+userid+token, lu ,null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 验证用户是否有权访问外部接口  
        /// </summary>
        /// <param name="userid">访问的用户ID</param>
        /// <returns></returns>
        public string isValid(string userid = null)
        {
            ajaxResult ar = new ajaxResult();
            if (userid != null && userid != _userID)
            {
                ar.Message = "对不起！您无权访问此用户的数据！";
                ar.StatusCode = statusCode.Code.failure;
                return Utils.ToJson(ar);
            }
            if (!isValid(_identifies, _userID, _token))
            {
                ar.Message= "登陆信息已失效,请重新登录.";
                ar.StatusCode = statusCode.Code.tokenFail;
                return Utils.ToJson(ar);
            }
            return string.Empty;
        }

        public LoginUserInfo GetUserInfo()
        {
            LoginUserInfo lu = HttpRuntime.Cache.Get(Identifies + UserID + Token) as LoginUserInfo;
            return lu;
        }
    } 
}
