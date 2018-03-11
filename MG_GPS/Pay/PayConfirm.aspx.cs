using MG_BLL.Pay.WeixinPay.business;
using MG_BLL.Pay.WeixinPay.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public int userid { get; set; }
        public int status { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                imei = Request.QueryString["imei"];
                string vc = Request.QueryString["vc"];
                total_fee = Convert.ToInt32(Request.QueryString["p"]);
                userid = Convert.ToInt32(Request.QueryString["userid"]);
                status = Convert.ToInt32(Request.QueryString["s"]);
                var ru = HttpUtility.UrlEncode($"http://m.mgoogps.com/Pay/PayConfirm.aspx?imei={imei}&vc={vc}&p={total_fee}&userid={userid}&s={status}");
                
                var url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={WxPayConfig.APPID}&redirect_uri={ru}&response_type=code&scope=snsapi_base&state=snsapi_base#wechat_redirect";
                //https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxda27104d229a3608&redirect_uri=http://m.mgoogps.com/Pay/PayConfirm.aspx?imei=1&vc=12&p=1223&response_type=code&scope=snsapi_base&state=snsapi_base#wechat_redirect
                Response.Redirect(url);
                Response.End();
            }
            if (!IsPostBack)
            {
                imei = Request.QueryString["imei"];
                string vc = Request.QueryString["vc"];
                total_fee = Convert.ToInt32(Request.QueryString["p"]);
                var code = Request.QueryString["code"];
                userid = Convert.ToInt32(Request.QueryString["userid"]);
                status = Convert.ToInt32(Request.QueryString["s"]);
                if (userid == 0)
                {
                    state = "参数错误！";
                    return;
                }
                MG_DAL.YiwenGPSEntities db = new MG_DAL.YiwenGPSEntities();
                var device = db.Devices.Where(d => d.Deleted == false && d.SerialNumber == imei && d.DevicePassword == vc).SingleOrDefault();
                prductName = "北斗GPS流量费(2年)-" + (string.IsNullOrEmpty(device.DeviceName) ? device.SerialNumber : device.DeviceName);
                JsApiPay jsApiPay = new JsApiPay(this);
                if (device.Model.Equals("80")) //MG-X21BZ
                {
                    jsApiPay.tariff_id = 8;
                    total_fee = 240;
                }
                else
                {
                    jsApiPay.tariff_id = 9;
                    total_fee = 195;
                }
              
                //JSAPI支付预处理
                try
                {
                    var userList = new List<int>() { 6, 7, 5959, 1296, 1389, 1331, 6093 };
                    // total_fee = ( type == "1" ? int.Parse(total_fee) :int.Parse( total_fee) )+"";
                    jsApiPay.user_id = userid;// (int)device.UserID;
                    //jsApiPay.openid = openid;
                    jsApiPay.total_fee = userList.Contains(jsApiPay.user_id )? new Random().Next(1, 10) : Convert.ToInt32(total_fee)*100;
                    jsApiPay.device_id = device.DeviceID;
                  
                    jsApiPay.product_body = prductName;
                    jsApiPay.device_name = "success_notify," + device.DeviceID + "," + userid+","+status;//attach 商家数据包，原样返回, status :1 账号已存在，2是新注册的账号
                    if (total_fee < 195 && !userList.Contains(jsApiPay.user_id))
                    {
                        state = "下单失败,金额错误！";
                        return;
                    }
                    jsApiPay.GetOpenidAndAccessTokenFromCode(code);

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
                    MG_BLL.Common.Log.Error(this, ex);
                    //state = ex.Message;
                    state = "网络繁忙,请稍后再试！";
                }
                catch (Exception ex)
                {
                   state = "下单失败,请稍后再试！";
                    MG_BLL.Common.Log.Error(this,ex);
                    //state = ex.Message;
                   // Utils.log("下单失败:userid:" + userid + ",deviceid：" + deviceid + "，total_fee：" + Convert.ToInt32(total_fee) + "，openid：" + openid + "，tariff_id：" + tariff_id + "，tariff_name：" + tariff_name + "，device_name：" + device_name + "，type：" + type);
                    //Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
                    // submit.Visible = false;

                }
            }
        }
    }
}