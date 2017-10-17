using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MG_DAL
{
    public static class Utils_Dal
    {
        public static string ToStringEmpty(this Object str)
        {
            if (str == null)
            {
                return "";
            }
            else
            {
                return str.ToString();
            }
        }
    }
}
