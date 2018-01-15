using MG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using MG_BLL.Weixin;
using MG_BLL;
using System.Collections;
using System.Net;
using MG_BLL.Pay.WeixinPay.lib;
using MG_BLL.Pay.WeixinPay.business;
using System.Data.SqlClient;
using System.Data;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace MG_GPS.Pay
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class wxPay : System.Web.UI.Page
    {
        public static string wxJsApiParam { get ; set; } //H5调起JS API参数
        public string userid  { get ; set; } 
        public string deviceid   { get ; set; } 
        public string type   { get ; set; } 
        public string imei { get; set; }
        public string total_fee { get; set; }
        public string tariff_name { get; set; }
        public string device_name{ set; get; } 
        public string order_no { set; get; }
        public string callback_url   { set; get; }

        public string state { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userid = Request.QueryString["userid"];
                total_fee = Request.QueryString["total_fee"];
                deviceid = Request.QueryString["deviceid"];
                string openid = Request.QueryString["openid"];
                type = Request.QueryString["type"];
                imei = Request.QueryString["imei"];
                string tid = Request.QueryString["tariff_id"];
                if (type == "1")
                {
                    tid = "7";
                    userid = "7";
                    total_fee = "1";
                }
                else
                {
                    type = "2";
                }
                //检测是否给当前页面传递了相关参数string.IsNullOrEmpty(openid) ||
                if ( string.IsNullOrEmpty(total_fee)||string.IsNullOrEmpty(deviceid) || string.IsNullOrEmpty(tid) || string.IsNullOrEmpty(openid) || string.IsNullOrEmpty(userid))
                {
                    state = "页面传参出错,请返回重试";
                    //Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面传参出错,请返回重试" + "</span>");
                    Log.Error(this.GetType().ToString(), "This page have not get params, cannot be inited, exit...");
                    //submit.Visible = false;
                    return;
                }
                int tariff_id = int.Parse(tid);
                SQLServerOperating s = new SQLServerOperating();
               // DataTable dt= s.Selects(" select ID, TariffName, Price, OldPrice, BuyCount, Type  from TariffPackages where ID=@tariff_id", new SqlParameter[] { new SqlParameter("tariff_id", tariff_id) });
                string strSql = @"update TariffPackages set BuyCount=BuyCount+cast( ceiling(rand()*100) as int) where ID=@tariff_id;
                                  select ID, TariffName, Price, OldPrice, BuyCount, Type,
                                  (select case when DeviceName='' then SerialNumber else DeviceName end from Devices where deviceid=@deviceid) DeviceName
                                  from TariffPackages where ID=@tariff_id  ";
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("tariff_id", tariff_id), new SqlParameter("deviceid", deviceid) });
                if (dt.Rows.Count <= 0)
                {
                    state = "页面传参出错,请返回重试";
                    return;
                }
                //s.ExecuteSql("update TariffPackages set BuyCount=BuyCount+cast( ceiling(rand()*100) as int) where ID=@ID", new SqlParameter[] { new SqlParameter("ID", tariff_id) });
              
                total_fee = dt.Rows[0]["Price"].toStringEmpty();
                tariff_name = dt.Rows[0]["TariffName"].toStringEmpty();
                device_name = dt.Rows[0]["DeviceName"].toStringEmpty(); // s.Select("", new SqlParameter[] { new SqlParameter("deviceid", deviceid) });
                if (string.IsNullOrEmpty(device_name))
                {
                    return;
                }
                tariff_name = "GPS移动流量-"+device_name+" 充值"+ tariff_name;
                //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
                JsApiPay jsApiPay = new JsApiPay(this);
                //JSAPI支付预处理
                try
                {
                    // total_fee = ( type == "1" ? int.Parse(total_fee) :int.Parse( total_fee) )+"";
                    jsApiPay.user_id = int.Parse(userid);
                    jsApiPay.openid = openid;
                    jsApiPay.total_fee = jsApiPay.user_id == 6 || jsApiPay.user_id == 7 ? new Random().Next(1, 10) : Convert.ToInt32(total_fee) ;
                    jsApiPay.device_id = int.Parse(deviceid);
                    jsApiPay.tariff_id = tariff_id;
                    jsApiPay.product_body = tariff_name;
                    jsApiPay.device_name = device_name;
                    WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                    wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数        

                    jsApiPay.InsertMgooOrder();

                    //下单成功后的内部订单号
                    order_no = jsApiPay.order_no;
                    callback_url = "http://m.mgoogps.com:8070/pay/pay_success.aspx?no=" + order_no.ToString() + "&deviceid=" + deviceid + "&t=" + DateTime.Now.Ticks;
                    Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);

                    //在页面上显示订单信息
                    // Response.Write("<span style='color:#00CD00;font-size:20px' id='payRes'>订单详情：</span><br/>");
                    // Response.Write("<span style='color:#00CD00;font-size:20px'>" + unifiedOrderResult.ToPrintStr() + "</span>");
                }
                catch (System.Net.WebException ex)
                {
                    //state = ex.Message;
                    state = "网络繁忙,请稍后再试！";
                }
                catch (Exception ex)
                {
                    state = "下单失败,请稍后再试！";
                    //state = ex.Message;
                    Utils.log("下单失败:userid:" + userid + ",deviceid：" + deviceid + "，total_fee：" + Convert.ToInt32(total_fee) + "，openid：" + openid + "，tariff_id：" + tariff_id + "，tariff_name：" + tariff_name + "，device_name：" + device_name + "，type：" + type);
                    //Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
                    // submit.Visible = false;
                }
            }

        } 

        protected void btnQueryOrder_Click(object sender, EventArgs e)
        {
            //MG_BLL.Pay.MgooOrders.Orders o = new MG_BLL.Pay.MgooOrders.Orders();
            //var orderStatus = o.QueryOrder( order_no);
           // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "QueryOrder('" + orderStatus+"')", true);
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script>Ceshi();</script>", true);
        } 
    }
}