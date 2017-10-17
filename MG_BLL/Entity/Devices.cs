using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MG_BLL.Entity
{
    public class Devices
    { 
        private string _id;
        private string _name;
        private string _speed;
        private string _connectTime;
        private string _deviceTime;
        private string _lat;
        private string _lon;
        private string _lbs;
        private string _direction;  
        private string _status; 
        private bool _onLine;
        private string _endTime;
        private string _deviceStatus;

        /// <summary>
        /// 设备IMEI
        /// </summary>
        public string Id
        {  
            get
            {
                return _id.Trim();
            }

            set
            {
                _id = value;
            }
        } 
        /// <summary>
        /// 速度（单位:KM/H）
        /// </summary>
        public string Speed
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
        /// 设备心跳时间，设备与服务器连接的时间
        /// </summary>
        public string ConnectTime
        {
            get
            {
                return _connectTime;
            }

            set
            {
                 DateTime ct = Convert.ToDateTime(value);
                // if ((DateTime.Now - ct).TotalMinutes > Utils.offLineMinute)
                // {
                // this.OnLine = Enum.GetName(typeof(DeviceStatus) , DeviceStatus.速度) ;
                // int enumValueThree = Convert.ToInt32(DeviceStatus.速度);
                //  }
                //.AddHours(8)
                _connectTime = ct.ToString("yyyy-MM-dd HH:mm:ss");
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
                DateTime d = Convert.ToDateTime(value);
                //.AddHours(8)
                _deviceTime = d.ToString("yyyy-MM-dd HH:mm:ss");
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
                _lat = value ?? "0.00";
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
                _lon = value ?? "0.00";
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
        /// ACC等信息
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
        /// 设备是否在线
        /// </summary>
        /// 
        public bool OnLine
        {
            get
            {
                return _onLine;
            }

            set
            {
                _onLine = value;
            }
        }

        /// <summary>
        /// 设备名字
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Lbs
        {
            get
            {
                return _lbs;
            }

            set
            {
                _lbs = value;
            }
        }

        /// <summary>
        /// 设备过期时间
        /// </summary>
        public string EndTime
        {
            get
            {
                return _endTime;
            }

            set
            {
                _endTime = value;
            }
        }

        public string DeviceStatus
        {
            get
            {
                return _deviceStatus;
            }

            set
            {
                _deviceStatus = value;
            }
        }
    }

    public class DeviceDetail {
        private string _id;
        private string _model;
        private string _name;
        private string _connectTime;
        private string _simNumber;
        private string _deviceTime;
        private string _lat;
        private string _lon;
        private string _speed;
        private string _battery;
        private string _gsm;
        private string _gps;
        private string _status;
        private object _lbs;
        private string _userID;
        private string _deviceStatus;
        private string _address;
        private string _direction;
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value.Trim();
            }
        }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string Model
        {
            get
            {
                return _model;
            }

            set
            {
                _model = value;
            }
        }
        /// <summary>
        /// SIM号码
        /// </summary>
        public string SimNumber
        {
            get
            {
                return _simNumber;
            }

            set
            {
                _simNumber = value ?? this.Id;
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
   

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string ConnectTime
        {
            get
            {
                return _connectTime;
            }

            set
            {
                DateTime ct = Convert.ToDateTime(value);
                //.AddHours(8)
                _connectTime = ct.ToString("yyyy-MM-dd HH:mm:ss"); 
            }
        }

        public string DeviceTime
        {
            get
            {
                return _deviceTime;
            }

            set
            {
                DateTime d = Convert.ToDateTime(value);
                //AddHours(8).
                _deviceTime = d.ToString("yyyy-MM-dd HH:mm:ss"); 
            }
        }

        public string Lat
        {
            get
            {
                return _lat;
            }

            set
            {
                _lat = value ?? "0.00";
            }
        }

        public string Lon
        {
            get
            {
                return _lon;
            }

            set
            {
                _lon = value ?? "0.00";
            }
        }

        public string Speed
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

        public string Battery
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

        public string Gps
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

        public object Lbs
        {
            get
            {
                return _lbs;
            }

            set
            {
                _lbs = value;
            }
        }
         
        public string Gsm
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

        public string DeviceStatus
        {
            get
            {
                return _deviceStatus;
            }

            set
            {
                _deviceStatus = value;
            }
        }

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
    }

    public class DeviceInfo {
        private string _id;
        private string _name;
        private string _model;
        private string _simNumber;
        
        private string _carNumber;
        private string _contact;
        private string _creatTime;
        private string _cellPhone;

        private string _activeTime;
        private string _endTime;
        private string _userID;
        private string _image;
        private bool _islbs;

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber
        {
            get
            {
                return _carNumber;
            }

            set
            {
                _carNumber = value ?? "";
            }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact
        {
            get
            {
                return _contact;
            }

            set
            {
                _contact = value ?? ""; 
            }
        }


        /// <summary>
        /// 联系人电话
        /// </summary>
        public string CellPhone
        {
            get
            {
                return _cellPhone;
            }

            set
            {
                _cellPhone = value ?? ""; 
            }
        }

        /// <summary>
        /// 出厂时间
        /// </summary>
        public string CreatTime
        {
            get
            {
                return _creatTime;
            }

            set
            {
                _creatTime = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        /// <summary>
        /// GPS 设备的 IMEI 码
        /// </summary>
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value ?? "";  
            }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string Model
        {
            get
            {
                return _model;
            }

            set
            {
                _model = value ?? "";
            }
        }
        /// <summary>
        /// SIM号码
        /// </summary>
        public string SimNumber
        {
            get
            {
                return _simNumber;
            }

            set
            {
                _simNumber = value ?? "";
            }
        }
        /// <summary>
        /// 安装激活时间
        /// </summary>
        public string ActiveTime
        {
            get
            {
                return _activeTime;
            }

            set
            {
                _activeTime = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"); ;
            }
        }
        /// <summary>
        /// 到期时间
        /// </summary>
        public string EndTime
        {
            get
            {
                return _endTime;
            }

            set
            {
                _endTime = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"); ;
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
                _userID = value ?? "";
            }
        }
        /// <summary>
        /// 汽车图片路径
        /// </summary>
        public string Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value ?? "";
            }
        }
        /// <summary>
        /// 是否启用网络定位
        /// </summary>
        public bool Islbs
        {
            get
            {
                return _islbs;
            }

            set
            { 
                _islbs = value;
            }
        }
    }
    /// <summary>
    /// 设备的状态
    /// </summary>
    enum DeviceStatus {
       离线  = 1,
       未激活 = 0 ,
       行驶 = 2,
       停止 =3
        ///离线 、未激活 、停止、行驶
    }
}
