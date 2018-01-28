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
    public partial class PayConfirm : System.Web.UI.Page
    {
        public static string wxJsApiParam { get; set; } //H5调起JS API参数
        public string imei { get; set; }
        public string prductName { get; set; }
        public int total_fee { get; set; }

        public string order_no { get; set; }
        public string state { set; get; }
        public string callback_url { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imei = Request.QueryString["imei"];
                string vc = Request.QueryString["vc"];
                total_fee = Convert.ToInt32(Request.QueryString["p"]);
                MG_DAL.YiwenGPSEntities db = new MG_DAL.YiwenGPSEntities();
                var device = db.Devices.Where(d => d.Deleted == false && d.SerialNumber == imei && d.DevicePassword == vc).SingleOrDefault();
                prductName = "北斗定位防盗报警器-" + (string.IsNullOrEmpty(device.DeviceName) ? device.SerialNumber : device.DeviceName);

                JsApiPay jsApiPay = new JsApiPay(this);
                //JSAPI支付预处理
                try
                {
                    // total_fee = ( type == "1" ? int.Parse(total_fee) :int.Parse( total_fee) )+"";
                    jsApiPay.user_id = (int)device.UserID;
                    //jsApiPay.openid = openid;
                    jsApiPay.total_fee = jsApiPay.user_id == 6 || jsApiPay.user_id == 7 ? new Random().Next(1, 10) : Convert.ToInt32(total_fee);
                    jsApiPay.device_id = device.DeviceID;
                    jsApiPay.tariff_id = 0;
                    jsApiPay.product_body = prductName;
                    jsApiPay.device_name = "success_notify";//attach 商家数据包，原样返回

                    WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                    wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数        

                    jsApiPay.InsertMgooOrder();

                    //下单成功后的内部订单号
                    order_no = jsApiPay.order_no;
                    callback_url = "../PayActivation/payResult.html?no=" + order_no.ToString() + "&deviceid=" + device.DeviceID + "&t=" + DateTime.Now.Ticks;
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
                   // Utils.log("下单失败:userid:" + userid + ",deviceid：" + deviceid + "，total_fee：" + Convert.ToInt32(total_fee) + "，openid：" + openid + "，tariff_id：" + tariff_id + "，tariff_name：" + tariff_name + "，device_name：" + device_name + "，type：" + type);
                    //Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
                    // submit.Visible = false;
                }
            }
        }
    }
}