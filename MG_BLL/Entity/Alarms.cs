using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MG_BLL.Entity
{
    public class Alarms
    {
        private string _id;
        private string _imei;
        private string _alarmtime;
        private string _content;
        private string _lat;
        private string _lon;
        private string _userID;
        private string _alarmType;
        /// <summary>
        /// 报警消息的ＩＤ
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
        /// 报警设备的ＩＭＥＩ
        /// </summary>
        public string Imei
        {
            get
            {
                return _imei;
            }

            set
            {
                _imei = value;
            }
        }
        /// <summary>
        /// 报警的时间
        /// </summary>
        public string Alarmtime
        {
            get
            {
                return _alarmtime;
            }

            set
            {
                _alarmtime = value;
            }
        }
        /// <summary>
        /// 报警的类容
        /// </summary>
        public string Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;
            }
        }
        /// <summary>
        /// 报警时设备的纬度
        /// </summary>
        public string Lat
        {
            get
            {
                return _lat;
            }

            set
            {
                
                _lat =  value  ;
            }
        }
        /// <summary>
        /// 报警时设备的精度
        /// </summary>
        public string Lon
        {
            get
            {
                return _lon;
            }

            set
            {
                _lon = value ;
            }
        }
        /// <summary>
        /// 设备所属用户
        /// </summary>
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

        public string AlarmType
        {
            get
            {
                return _alarmType;
            }

            set
            {
                _alarmType = value;
            }
        }
    }
}
