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
    
    public partial class Users
    {
        public int UserID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public Nullable<int> UserType { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string TimeZone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public Nullable<short> Country { get; set; }
        public Nullable<short> State { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string SMSEmail { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public Nullable<short> Status { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> SuperAdmin { get; set; }
        public Nullable<int> AllDeviceCount { get; set; }
        public Nullable<int> ActivationCount { get; set; }
        public Nullable<int> MoneyCount { get; set; }
    }
}
