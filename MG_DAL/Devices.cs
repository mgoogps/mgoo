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
    
    public partial class Devices
    {
        public int DeviceID { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceName { get; set; }
        public string DevicePassword { get; set; }
        public string CarUserName { get; set; }
        public string CarNum { get; set; }
        public string CellPhone { get; set; }
        public Nullable<int> Status { get; set; }
        public string PhoneNum { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.DateTime> ActiveDate { get; set; }
        public Nullable<System.DateTime> HireStartDate { get; set; }
        public Nullable<System.DateTime> HireExpireDate { get; set; }
        public Nullable<decimal> SpeedLimit { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> GroupID { get; set; }
        public string Icon { get; set; }
        public Nullable<double> OILCoefficient { get; set; }
        public string BSJIP { get; set; }
        public Nullable<int> AddHireDay { get; set; }
        public Nullable<int> ServerID { get; set; }
        public Nullable<decimal> OilPrice { get; set; }
        public Nullable<System.DateTime> CreatedByUser { get; set; }
        public Nullable<System.DateTime> ExpireByUser { get; set; }
        public Nullable<decimal> OilVolume { get; set; }
        public Nullable<decimal> OilLow { get; set; }
        public Nullable<decimal> OilHigh { get; set; }
        public string CarImg { get; set; }
        public Nullable<int> ServerID2 { get; set; }
        public Nullable<decimal> ByDistance { get; set; }
        public Nullable<decimal> LastByDistance { get; set; }
    }
}