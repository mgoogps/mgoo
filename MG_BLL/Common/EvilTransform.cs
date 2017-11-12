using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace MG_BLL
{
    public class EvilTransform
    {
        /**
         * 各地图API坐标系统比较与转换;
         * WGS84坐标系：即地球坐标系，国际上通用的坐标系。设备一般包含GPS芯片或者北斗芯片获取的经纬度为WGS84地理坐标系,
         * 谷歌地图采用的是WGS84地理坐标系（中国范围除外）;
         * GCJ02坐标系：即火星坐标系，是由中国国家测绘局制订的地理信息系统的坐标系统。由WGS84坐标系经加密后的坐标系。
         * 谷歌中国地图和搜搜中国地图采用的是GCJ02地理坐标系; BD09坐标系：即百度坐标系，GCJ02坐标系经加密后的坐标系;
         * 搜狗坐标系、图吧坐标系等，估计也是在GCJ02基础上加密而成的。 
         */
        public class PositionUtil
        {

           // private static readonly String BAIDU_LBS_TYPE = "bd09ll";

            private static double pi = 3.1415926535897932384626;
            private static double a = 6378245.0;
            private static double ee = 0.00669342162296594323;

            /// <summary>
            /// 84 to 火星坐标系 (GCJ-02) World Geodetic System ==> Mars Geodetic System
            /// </summary>
            /// <param name="lat"></param>
            /// <param name="lon"></param>
            /// <returns></returns>
            public static Gps gps84_To_Gcj02(double lat, double lon)
            {
                //if (outOfChina(lat, lon))
                //{
               //     return null;
               // }
                double dLat = transformLat(lon - 105.0, lat - 35.0);
                double dLon = transformLon(lon - 105.0, lat - 35.0);
                double radLat = lat / 180.0 * pi;
                double magic = Math.Sin(radLat);
                magic = 1 - ee * magic * magic;
                double sqrtMagic = Math.Sqrt(magic);
                dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
                dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
                double mgLat = lat + dLat;
                double mgLon = lon + dLon;
                return new Gps(mgLat, mgLon);
            }

            /// <summary>
            ///  火星坐标系 (GCJ-02) to 84 * * 
            /// </summary>
            /// <param name="lat"></param>
            /// <param name="lon"></param>
            /// <returns></returns> 
            public static Gps gcj_To_Gps84(double lat, double lon)
            {
                Gps gps = transform(lat, lon);
                double lontitude = lon * 2 - gps.getWgLon();
                double latitude = lat * 2 - gps.getWgLat();
                return new Gps(latitude, lontitude);
            }

            /// <summary>
            /// 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换算法 将 GCJ-02 坐标转换成 BD-09 坐标
            /// </summary>
            /// <param name="gg_lat"></param>
            /// <param name="gg_lon"></param>
            /// <returns></returns>
            public static Gps gcj02_To_Bd09(double gg_lat, double gg_lon)
            {
                double x = gg_lon, y = gg_lat;
                double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * pi);
                double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * pi);
                double bd_lon = z * Math.Cos(theta) + 0.0065;
                double bd_lat = z * Math.Sin(theta) + 0.006;
                return new Gps(bd_lat, bd_lon);
            }

            /// <summary>
            /// 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换算法 * * 将 BD-09 坐标转换成GCJ-02 坐标 * * @param
            /// </summary>
            /// <param name="bd_lat"></param>
            /// <param name="bd_lon"></param>
            /// <returns></returns>
            public static Gps bd09_To_Gcj02(double bd_lat, double bd_lon)
            {
                double x = bd_lon - 0.0065, y = bd_lat - 0.006;
                double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * pi);
                double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * pi);
                double gg_lon = z * Math.Cos(theta);
                double gg_lat = z * Math.Sin(theta);
                return new Gps(gg_lat, gg_lon);
            }

            /// <summary>
            /// (BD-09)-->84
            /// </summary>
            /// <param name="bd_lat"></param>
            /// <param name="bd_lon"></param>
            /// <returns></returns>
            public static Gps bd09_To_Gps84(double bd_lat, double bd_lon)
            {

                Gps gcj02 = PositionUtil.bd09_To_Gcj02(bd_lat, bd_lon);
                Gps map84 = PositionUtil.gcj_To_Gps84(gcj02.getWgLat(), gcj02.getWgLon());
                return map84;

            }
            /// <summary>
            ///  将  gps84 坐标转换成 BD-09 坐标
            /// </summary>
            /// <returns></returns>
            public static Gps gps84_To_Bd09(double lat, double lon)
            {
                double π = pi * 3000 / 180;
                double x = lon, y = lat;
                double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * π);
                double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * π);
                Gps bd09 = transform(z * Math.Sin(theta) + 0.006, z * Math.Cos(theta) + 0.0065);
                return bd09;
            }

            public static bool outOfChina(double lat, double lon)
            {
                if (lon < 72.004 || lon > 137.8347)
                    return true;
                if (lat < 0.8293 || lat > 55.8271)
                    return true;
                return false;
            }

            public static Gps transform(double lat, double lon)
            {
                //if (outOfChina(lat, lon))
                //{
                //    return new Gps(lat, lon);
                //}
                double dLat = transformLat(lon - 105.0, lat - 35.0);
                double dLon = transformLon(lon - 105.0, lat - 35.0);
                double radLat = lat / 180.0 * pi;
                double magic = Math.Sin(radLat);
                magic = 1 - ee * magic * magic;
                double sqrtMagic = Math.Sqrt(magic);
                dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
                dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
                double mgLat = lat + dLat;
                double mgLon = lon + dLon;
                return new Gps(mgLat, mgLon);
            }

            public static double transformLat(double x, double y)
            {
                double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y
                        + 0.2 * Math.Sqrt(Math.Abs(x));
                ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
                ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
                ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
                return ret;
            }

            public static double transformLon(double x, double y)
            {
                double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1
                        * Math.Sqrt(Math.Abs(x));
                ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
                ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
                ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0
                        * pi)) * 2.0 / 3.0;
                return ret;
            } 
        }

    }
    public class Gps
    {

        private double wgLat;
        private double wgLon;
        private string address;

        public string Address
        {
            get
            {
                return address ?? "未知.";
            }

            set
            {
                address = value;
            }
        }

        public Gps(double wgLat, double wgLon)
        {
            setWgLat(wgLat);
            setWgLon(wgLon);
        }

        public double getWgLat()
        {
            return wgLat;
        }

        public void setWgLat(double wgLat)
        {
            this.wgLat = wgLat;
        }

        public double getWgLon()
        {
            return wgLon;
        }

        public void setWgLon(double wgLon)
        {
            this.wgLon = wgLon;
        }
  
        public override String ToString()
        {
            return wgLat + "," + wgLon;
        }
    }

    /// <summary>
    /// 获取地址
    /// </summary>
    public interface Geocoding
    {
        string key { get; set; }
 
        /// <summary>
        /// 经纬度转换
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Gps Translate(string lat, string lng, bool address = true);

        Gps TranslateGps84(string lat, string lng);
         

        /// <summary>
        /// 根据经纬度获取中文地址
        /// </summary>
        /// <param name="gps"></param>
        /// <returns></returns>
        string GetAddress(Gps gps);

    }

    public class Baidu : Geocoding
    {
        public string _key;
        public string key
        {
            get
            {
                return _key;
            }

            set
            {
                _key = value;
            }
        }

        public string GetAddress(Gps bdGps)
        {
            try
            {
                Mgoo.Position.IGeocoding geo = new Mgoo.Position.Geocod.Baidu();
                return geo.GetAddress(new Mgoo.Position.Point (bdGps.getWgLat(),bdGps.getWgLon()));
                //POIService.POIServiceSoapClient poi = new POIService.POIServiceSoapClient();
                //return poi.GetAddressByLatlng(Convert.ToDecimal(bdGps.getWgLat()), Convert.ToDecimal(bdGps.getWgLon()), "BAIDU", "ZH-CN");

                #region 利用百度地址API获取地址 - 没用了
                /*MG_DAL.MgoogpsWebClient wc = new MG_DAL.MgoogpsWebClient();
                  wc.RequestUrl = "http://api.map.baidu.com/geocoder/v2/?ak=PFEwxiwsyv4GjEQcZrNZS0NsIkEvU8TL&location=" + bdGps.getWgLat() + "," + bdGps.getWgLon() + "&output=json&pois=0";
                  wc.RequestMethodType = "GET";
                  string jsonLocation = wc.RequestSend();
                  JavaScriptSerializer js = new JavaScriptSerializer();
                  BaiduAddress bdLocation = js.Deserialize<BaiduAddress>(jsonLocation);
                  return bdLocation.Result.Formatted_address;*/
                #endregion
            }
            catch (Exception ex)
            {
                Utils.log("EvilTransform.cs>GetAddress(baidu):" + ex.Message);
                return "未知.";
            }
          
        }

        public Gps Translate(string lat, string lng, bool address = true)
        {
            double _lat = lat.toDouble();
            double _lng = lng.toDouble();
            Gps gps;
            if (_lat == -1.0 && _lng == -1.0)
            {
                gps = new Gps(-1.000, -1.000);
                gps.Address = "未解析到地址.";
                return gps;
            } 
            if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lng))
            {
                gps = new Gps(-1.000, -1.000);
                gps.Address = "未解析到地址.";
                return gps;
            }
            gps = EvilTransform.PositionUtil.gps84_To_Bd09(_lat, _lng);
            try
            {
                if (address)
                {
                  gps.Address = GetAddress(gps);
                }
                return gps;
            }
            catch (Exception ex)
            {
                gps.Address = "未知";
                Utils.log("获取地址异常(Baidu translate):" + ex.Message);
                return gps;
            }
        }
         
        public Gps TranslateGps84(string lat, string lng)
        {
            try
            {
                return EvilTransform.PositionUtil.bd09_To_Gps84(lat.toDouble(), lng.toDouble());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class Amap : Geocoding
    {
        public string _key;
        public string key
        {
            get
            {
                return _key;
            }

            set
            {
                _key = value;
            }
        }
         
        public string GetAddress(Gps gps)
        {
            try
            {
                Mgoo.Position.IGeocoding geo = new Mgoo.Position.Geocod.Amap();
                return geo.GetAddress(new Mgoo.Position.Point(gps.getWgLat(),gps.getWgLon()));
                //if (string.IsNullOrEmpty( this.key))
                //{ 
                //    this.key = Utils.GetAmapKey(); 
                //}
                //POIService.POIServiceSoapClient poi = new POIService.POIServiceSoapClient();
                //return poi.GetAddressByLatlng(Convert.ToDecimal(gps.getWgLat()), Convert.ToDecimal(gps.getWgLon()), "AMAP", "ZH-CN");
      
                #region 利用高德地图获取中文详细地址- 没用了 
                /*string jsonLocation = "";
                MG_DAL.MgoogpsWebClient wc = new MG_DAL.MgoogpsWebClient();
                wc.RequestMethodType = "GET";
                wc.RequestUrl = " http://restapi.amap.com/v3/geocode/regeo?output=json&location=" + gps.getWgLon() + "," + gps.getWgLat() + "&key=" + _key + "&radius=1000&extensions=base";
                jsonLocation = wc.RequestSend();
                //"{\"status\":\"1\",\"info\":\"OK\",\"infocode\":\"10000\",\"regeocode\":{\"formatted_address\":\"广东省中山市南朗镇华峰路\",\"addressComponent\":{\"country\":\"中国\",\"province\":\"广东省\",\"city\":\"中山市\",\"citycode\":\"0760\",\"district\":[],\"adcode\":\"442000\",\"township\":\"南朗镇\",\"towncode\":\"442000113000\",\"neighborhood\":{\"name\":[],\"type\":[]},\"building\":{\"name\":[],\"type\":[]},\"streetNumber\":{\"street\":\"华峰路\",\"number\":\"15号\",\"location\":\"113.509635,22.5109078\",\"direction\":\"东北\",\"distance\":\"31.9644\"},\"businessAreas\":[[]]}}}"
                Dictionary<string, object> addressDic = Utils.ToObjects<Dictionary<string, object>>(jsonLocation);
                if (addressDic["status"].Equals("1"))
                {
                    return (addressDic["regeocode"] as Dictionary<string, object>)["formatted_address"].toStringEmpty();
                }
                else
                {
                    return "未知";
                }*/
                #endregion
            }
            catch (Exception ex)
            {
                Utils.log("EvilTransform.cs>GetAddress(amap):" + ex.Message);
                return "未知.";
            }
        
        }

        public Gps Translate(string lat, string lng, bool address = true)
        {
            double _lat = lat.toDouble();
            double _lng = lng.toDouble();
            Gps gps; 
            if (_lat == -1.0 && _lng == -1.0)
            {
                gps = new Gps(-1.000, -1.000);
                gps.Address = "未解析到地址.";
                return gps;
            }
            if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lng))
            {
                gps = new Gps(-1.000, -1.000);
                gps.Address = "未解析到地址.";
                return gps;
            }
            gps = EvilTransform.PositionUtil.gps84_To_Gcj02(_lat, _lng) ;
            try
            {
                if (address)
                {
                  gps.Address =  GetAddress(gps);
                }
                return gps;
            }
            catch (Exception ex)
            {
                gps.Address = "未知";
                Utils.log("获取地址异常(Baidu translate):" + ex.Message);
                return gps;
            }
        }

        public Gps TranslateGps84(string lat, string lng)
        {
            try
            {
                return EvilTransform.PositionUtil.gcj_To_Gps84(lat.toDouble(), lng.toDouble());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

   

    class BaiduAddress
    {
        private string _status;
        private Result _result;

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

        public Result Result
        {
            get
            {
                return _result;
            }

            set
            {
                _result = value;
            }
        }
    }
    public class Result
    {
        private Location _location;
        private string _formatted_address;
        private string _business;
        private addressComponent _addressComponent;
        private string _cityCode;
        private ArrayList _poiRegions;
        private ArrayList _pois;
        private string _sematic_description;

        public Location Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
            }
        }

        public string Formatted_address
        {
            get
            {
                return _formatted_address;
            }

            set
            {
                _formatted_address = value;
            }
        }

        public string Business
        {
            get
            {
                return _business;
            }

            set
            {
                _business = value;
            }
        }

        public addressComponent AddressComponent
        {
            get
            {
                return _addressComponent;
            }

            set
            {
                _addressComponent = value;
            }
        }

        public string CityCode
        {
            get
            {
                return _cityCode;
            }

            set
            {
                _cityCode = value;
            }
        }

        public ArrayList PoiRegions
        {
            get
            {
                return _poiRegions;
            }

            set
            {
                _poiRegions = value;
            }
        }

        public ArrayList Pois
        {
            get
            {
                return _pois;
            }

            set
            {
                _pois = value;
            }
        }

        public string Sematic_description
        {
            get
            {
                return _sematic_description;
            }

            set
            {
                _sematic_description = value;
            }
        }
    }

    public class addressComponent
    {
        private string _adcode;
        private string _city;
        private string _country;
        private string _country_code;
        private string _direction;
        private string _distance;
        private string _district;
        private string _province;
        private string _street;
        private string _street_number;

        public string Adcode
        {
            get
            {
                return _adcode;
            }

            set
            {
                _adcode = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                _city = value;
            }
        }

        public string Country
        {
            get
            {
                return _country;
            }

            set
            {
                _country = value;
            }
        }

        public string Country_code
        {
            get
            {
                return _country_code;
            }

            set
            {
                _country_code = value;
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

        public string Distance
        {
            get
            {
                return _distance;
            }

            set
            {
                _distance = value;
            }
        }

        public string District
        {
            get
            {
                return _district;
            }

            set
            {
                _district = value;
            }
        }

        public string Province
        {
            get
            {
                return _province;
            }

            set
            {
                _province = value;
            }
        }

        public string Street
        {
            get
            {
                return _street;
            }

            set
            {
                _street = value;
            }
        }

        public string Street_number
        {
            get
            {
                return _street_number;
            }

            set
            {
                _street_number = value;
            }
        }
    }

    public class Location
    {
        private string _lat;
        private string _lng;

        public string Lat
        {
            get
            {
                return _lat;
            }

            set
            {
                _lat = value;
            }
        }

        public string Lng
        {
            get
            {
                return _lng;
            }

            set
            {
                _lng = value;
            }
        }
    }
}
