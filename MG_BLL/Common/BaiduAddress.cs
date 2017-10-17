using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.Common
{ 
        public class BaiduAddress
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
