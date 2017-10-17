using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.Entity
{
    public class WeChatPush
    {
        private string _touser;
        private string _template_id;
        private string _url;
        private data _data;
        private remark _remark;

        public string touser
        {
            get
            {
                return _touser;
            }

            set
            {
                _touser = value;
            }
        }

        public string template_id
        {
            get
            {
                return _template_id;
            }

            set
            {
                _template_id = value;
            }
        }

        public string url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
            }
        }

        internal data data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
            }
        }

        internal remark remark
        {
            get
            {
                return _remark;
            }

            set
            {
                _remark = value;
            }
        }
    }
    class data
    {
        private first _first;
        private keyword1 _keyword1;
        private keyword2 _keyword2;
        private keyword3 _keyword3;

        internal first first
        {
            get
            {
                return _first;
            }

            set
            {
                _first = value;
            }
        }

        internal keyword1 keyword1
        {
            get
            {
                return _keyword1;
            }

            set
            {
                _keyword1 = value;
            }
        }

        internal keyword2 keyword2
        {
            get
            {
                return _keyword2;
            }

            set
            {
                _keyword2 = value;
            }
        }

        internal keyword3 keyword3
        {
            get
            {
                return _keyword3;
            }

            set
            {
                _keyword3 = value;
            }
        }
    }
    class first
    {
        private string _color;
        private string _value;

        public string color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }

        public string value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }
    }
    class keyword1
    {
        private string _color;
        private string _value;
        public string color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }

        public string value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }
    }
    class keyword2
    {
        private string _color;
        private string _value;
        public string color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }

        public string value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }
    }
    class keyword3
    {
        private string _color;
        private string _value;
        public string color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }

        public string value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }
    }

    class remark {
        private string _value;
        private string _color;

        public string value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        public string color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }
    }
}
