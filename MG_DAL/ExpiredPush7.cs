//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MG_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExpiredPush7
    {
        public Nullable<int> UserID { get; set; }
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public Nullable<System.DateTime> HireExpireDate { get; set; }
        public string ClientID { get; set; }
        public string OS { get; set; }
        public string PackageName { get; set; }
        public string AppID { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public Nullable<System.DateTime> LastPushTime { get; set; }
    }
}