using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MG_BLL.Entity
{
    public class Users
    {
        private String _id;
        private String _userName;
        private String _cellPhone;
        private String _address;
        private String _parent;

        /// <summary>
        /// 用户ID(登录名)
        /// </summary>
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
         
        /// <summary>
        /// 用户联系号码
        /// </summary>
        public string CellPhone
        {
            get
            {
                return _cellPhone;
            }

            set
            {
                _cellPhone = value;
            }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
            }
        }
        /// <summary>
        /// 父级账号
        /// </summary>
        public string Parent
        {
            get
            {
                return _parent;
            }

            set
            {
                _parent = value;
            }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
            }
        }
    }
}
