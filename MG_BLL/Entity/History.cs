using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MG_BLL.Entity
{
    public class History
    {
        private string _IMEI;
        private string _command;
        private string _connectTime;
        private string _deviceTime;
        private string _lat;
        private string _lon;
        private string _direction;
        private int _speed;
        private int _battery;
        private string _status;
        private int _gsm;
        private int _gps;
        private int _kmtotal;
        private string _latitude;
        private string _longitude;
        /// <summary>
        /// 设备的 IMEI 码
        /// </summary>
        public string IMEI
        {
            get
            {
                return _IMEI;
            }

            set
            {
                _IMEI = value;
            }
        }

        public string Command
        {
            get
            {
                return _command;
            }

            set
            {
                _command = value ?? "";
            }
        }
        /// <summary>
        /// 心跳时间，设备与服务器连接时间
        /// </summary>
        public string ConnectTime
        {
            get
            {
                return _connectTime;
            }

            set
            {
                _connectTime = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        /// <summary>
        /// 设备定位时间
        /// </summary>
        public string DeviceTime
        {
            get
            {
                return _deviceTime;
            }

            set
            {
                _deviceTime = Convert.ToDateTime(value) .ToString("yyyy-MM-dd HH:mm:ss"); ;
            }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Lat
        {
            get
            {
                return _lat;
            }

            set
            {
                _lat = value ?? "-1";
            }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public string Lon
        {
            get
            {
                return _lon;
            }

            set
            {
                _lon = value ?? "-1";
            }
        }
        /// <summary>
        /// 方向角
        /// </summary>
        public string Direction
        {
            get
            {
                return _direction;
            }

            set
            {
                _direction = value;
            }
        }
        /// <summary>
        /// 速度（单位：km/h）
        /// </summary>
        public int Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
            }
        }
        /// <summary>
        /// 电池电量，最大 6
        /// </summary>
        public int Battery
        {
            get
            {
                return _battery;
            }

            set
            {
                _battery = value;
            }
        }
        /// <summary>
        /// ACC 等信息
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
            }
        }
        /// <summary>
        /// GSM 信号，最大 31
        /// </summary>
        public int Gsm
        {
            get
            {
                return _gsm;
            }

            set
            {
                _gsm = value;
            }
        }
        /// <summary>
        /// 当前连接的 GPS 卫星数量
        /// </summary>
        public int Gps
        {
            get
            {
                return _gps;
            }

            set
            {
                _gps = value;
            }
        }
        /// <summary>
        /// 设备里程总数
        /// </summary>
        public int Kmtotal
        {
            get
            {
                return _kmtotal;
            }

            set
            {
                _kmtotal = value;
            }
        }

        public string Latitude
        {
            get
            {
                return _latitude;
            }

            set
            {
                Lat = value;
                _latitude = value;
            }
        }

        public string Longitude
        {
            get
            {
                return _longitude;
            }

            set
            {
                Lon = value;
                _longitude = value;
            }
        }
    }
}
