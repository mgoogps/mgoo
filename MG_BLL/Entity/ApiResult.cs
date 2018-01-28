using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.Entity
{
    public class ApiResult
    {
        public Code code { get; set; }
        public string message { get; set; }
        public object result { get; set; }

        public enum Code
        {
            success = 0,
            failure = 1,
            error = 2
        }
    }
  
}
