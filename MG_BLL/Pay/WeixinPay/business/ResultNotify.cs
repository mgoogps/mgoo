using MG_BLL.Pay.WeixinPay.lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace MG_BLL.Pay.WeixinPay.business
{
    public class ResultNotify : Notify
    {
        /// <summary>
        /// 商户系统内部订单号
        /// </summary>
        private string trade_no { set; get; }
        /// <summary>
        /// 微信的订单号 
        /// </summary>
        private string transaction_id { set; get; }
        /// <summary>
        /// 银行类型，采用字符串类型的银行标识; 详细信息见：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_2
        /// </summary>
        private string bank_type { set; get; } 
        /// <summary>
        /// 货币种类	CNY-人民币
        /// </summary>
        private string fee_type { set ; get ; }
        /// <summary>
        /// 支付方式 JSAPI--公众号支付、NATIVE--原生扫码支付、APP--app支付
        /// </summary>
        private string trade_type { set; get; }
        /// <summary>
        /// 充值套餐的ID
        /// </summary>
        private string tariff_id { set; get; }
        private string device_name { set; get; }

        public string time_end
        {
            get
            {
                return _time_end;
            }

            set
            {
                _time_end = DateTime.ParseExact(value , "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss");
               // _timeEnd = value;
            }
        }



        /// <summary>
        /// 订单支付时间
        /// </summary>
        private string _time_end;  

        public ResultNotify(Page page) : base(page)
        {
        }
        public override void ProcessNotify()
        {
            WxPayData notifyData = GetNotifyData();

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                Log.Error(this.GetType().ToString(), "The Pay result is error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            this.transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(this.transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                Log.Error(this.GetType().ToString(), "Order query failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
            //查询订单成功
            else
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                Log.Info(this.GetType().ToString(), "order query success : " + res.ToXml());
                //result_code等于SUCCESS，代表支付成功
                Log.Info(this.GetType().ToString(), "-----result_code:" + notifyData.GetValue("result_code") +",微信订单号" + this.transaction_id);
                if (notifyData.GetValue("result_code").ToString() == "SUCCESS")
                {
                    this.trade_no = notifyData.GetValue("out_trade_no").toStringEmpty();
                    this.time_end = notifyData.GetValue("time_end").toStringEmpty();
                    this.trade_type = notifyData.GetValue("trade_type").ToString();
                    this.fee_type = notifyData.GetValue("fee_type").toStringEmpty();
                    this.bank_type = notifyData.GetValue("bank_type").toStringEmpty();
                    this.device_name = notifyData.GetValue("attach").ToString();
                    string openid = notifyData.GetValue("openid").ToString();
                    string total_fee = notifyData.GetValue("total_fee").ToString();

                  
                    MgooOrders.Orders o = new MgooOrders.Orders();
                  
                    bool success = o.ModifyOrderStatus(transaction_id,fee_type,time_end,bank_type,trade_type,trade_no, !device_name.StartsWith("success_notify"));
                 
                    if (success)
                    {
                        if (device_name.StartsWith("success_notify"))
                        {
                            Task.Run(()=> {
                                try
                                {
                                    MG_DAL.YiwenGPSEntities db = new MG_DAL.YiwenGPSEntities();
                                    //Common.Log.Info(this, device_name);
                                    var dev = db.Devices.Find(Convert.ToInt32( device_name.Split(',')[1]));
                                    var urserid = device_name.Split(',')[2];
                                    ///用户在代理商线下用微信支付 扫码支付 激活设备
                                    Weixin.Devices wd = new Weixin.Devices(new Common.AuthHeader() { UserID = urserid });
                                    wd.AddDevice(dev.SerialNumber, dev.DevicePassword, urserid, "-1");
                                }
                                catch (Exception ex)
                                {
                                    Common.Log.Error(this,ex);
                                }
                              
                            }); 
                        }
                        else
                        {
                            Task.Run(() => o.SendMail(trade_no));
                            Task.Run(() => o.PaySuccessPush(openid, device_name, total_fee, time_end, o.GetBankName(bank_type), trade_no));
                        } 
                    }
                }
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
        }

        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       

    }
}
