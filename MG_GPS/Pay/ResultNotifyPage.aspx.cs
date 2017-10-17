using MG_BLL.Pay.WeixinPay.business;
using MG_BLL.Pay.WeixinPay.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MG_GPS.Pay
{
    public partial class ResultNotifyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Info(this.GetType().ToString(),"进入回调页面.....");
            ResultNotify resultNotify = new ResultNotify(this);
            resultNotify.ProcessNotify();
           
        }
    }
}