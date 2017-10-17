using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.Entity
{
    public class SMSResult
    {
        private string respCode;
        private string failCount;
        private object failList;
        private string smsId;

        public string RespCode
        {
            get
            {
                return respCode;
            }

            set
            {
                respCode = value;
            }
        }

        public string FailCount
        {
            get
            {
                return failCount;
            }

            set
            {
                failCount = value;
            }
        }

        public object FailList
        {
            get
            {
                return failList;
            }

            set
            {
                failList = value;
            }
        }

        public string SmsId
        {
            get
            {
                return smsId;
            }

            set
            {
                smsId = value;
            }
        }
    }


}
