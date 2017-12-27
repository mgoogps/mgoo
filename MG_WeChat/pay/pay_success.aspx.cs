using MG_BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MG_WeChat.pay
{
    public partial class pay_success : System.Web.UI.Page
    {
        public string order_no { get; set; }
        public string deviceid { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["no"]))
            {
               // Response.Redirect("../device/DeviceList.html");
            }
            deviceid = Request.QueryString["deviceid"];
            order_no = Request.QueryString["no"];
            //Log.Info(this,"订单号："+order_no);
        }
    }
}