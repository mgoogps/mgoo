using MG_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Data;
using MG_BLL.Common;
using MG_BLL.Pay.WeixinPay.business;
using MG_BLL.Pay.WeixinPay.lib;
using MG_BLL.Weixin;
using System.Net.Mail;
using MG_BLL;

namespace MG_BLL.Pay.MgooOrders
{
    public class Orders
    {
        public AuthHeader myHeader = new AuthHeader();
        public Orders(AuthHeader _myHeader)
        {
            myHeader = _myHeader;
        }

        public Orders() { }

        public void UnifiedOrder(string deviceid, string tariffid, string tradetype)
        {
            int tariff_id = int.Parse(tariffid);
            SQLServerOperating s = new SQLServerOperating();
            DataTable dt = s.Selects(" select ID, TariffName, Price, OldPrice, BuyCount, Type  from TariffPackages where ID=@tariff_id", new SqlParameter[] { new SqlParameter("tariff_id", tariff_id) });
            if (dt.Rows.Count <= 0)
            {
                return;
            }
            s.ExecuteSql("update TariffPackages set BuyCount=BuyCount+cast( ceiling(rand()*100) as int) where ID=@ID", new SqlParameter[] { new SqlParameter("ID", tariff_id) });
            s.Selects(" select ID, TariffName, Price, OldPrice, BuyCount, Type  from TariffPackages where ID=@tariff_id", new SqlParameter[] { new SqlParameter("tariff_id", tariff_id) });
            string total_fee = dt.Rows[0]["Price"].toStringEmpty();
            string tariff_name = dt.Rows[0]["TariffName"].toStringEmpty();
            string device_name = s.Select("select case when DeviceName='' then SerialNumber else DeviceName end from Devices where deviceid=@deviceid", new SqlParameter[] { new SqlParameter("deviceid", deviceid) });
            if (string.IsNullOrEmpty(device_name))
            {
                return;
            }
            tariff_name = "GPS移动流量-" + device_name + " 充值" + tariff_name;
            JsApiPay jsApiPay = new JsApiPay();
            jsApiPay.user_id = int.Parse(myHeader.UserID);
            jsApiPay.openid = "";
            jsApiPay.total_fee = jsApiPay.user_id == 6 || jsApiPay.user_id == 7 ? new Random().Next(1, 10) : int.Parse(total_fee);
            jsApiPay.device_id = int.Parse(deviceid);
            jsApiPay.tariff_id = tariff_id;

            jsApiPay.product_body = tariff_name;
            WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
            jsApiPay.InsertMgooOrder();
            // AddOrder(myHeader.UserID,deviceid,"",);
        }

        public List<Dictionary<string, string>> GetOrderList(int userid,string orderno = null)
        {
           var list = new List<Dictionary<string, string>>();
            try
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                string where = "";
                if (!string.IsNullOrEmpty(orderno)) {
                    where = " and OrderNo=@OrderNo";
                    pars.Add(new SqlParameter("OrderNo", orderno));
                }
                string strSql = "select OrderID, o.UserID,d.DeviceName,d.SerialNumber, o.DeviceID, OpenID, OrderNo, TransactionNo, ProductBody, FeeType ," +
                          "TotalFee, o.Created, OrderDate, OrderExpire, PayDate, TradeType, o.Status, BankType, TariffID, BillCreateIP  " +
                          "  from Orders o inner join devices d on o.DeviceID=d.DeviceID " +
                          "where o.userid = @userid "+ where + " order by orderID";
                pars.Add(new SqlParameter ("userid", userid));
                SQLServerOperating s = new SQLServerOperating();
                list = s.Selects(strSql, pars.ToArray()).toListDictionary();
                return list;
            }
            catch (Exception ex)
            {
                Common.Log.Error(this,ex);
                return list;
            }
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="DeviceID">设备ID</param>
        /// <param name="OpenID">用户的OpenID</param>
        /// <param name="OrderNo">订单号</param>
        /// <param name="TransacionNo">微信支付单号，根据此单号去微信API查询订单状态（支付成功后返回回调URL里面）</param>
        /// <param name="ProductBody">商品描述</param>
        /// <param name="FeeType">货币币种</param>
        /// <param name="TotalFee">金额</param>
        /// <param name="OrderDate">订单生成时间</param>
        /// <param name="OrderExpire">订单失效时间</param>
        /// <param name="TariffID">充值的套餐ID</param>     
        public void AddOrder(int UserID, int DeviceID, string OpenID, string OrderNo, string ProductBody, string FeeType, int TotalFee, DateTime OrderDate, DateTime OrderExpire, int TariffID)
        {
            try
            {
                string strSql = @"insert into orders( UserID, DeviceID, OpenID, OrderNo,TransactionNo, ProductBody, FeeType, TotalFee, Created, OrderDate,OrderExpire, TariffID, Status, BillCreateIP) 
                         values(@UserID, @DeviceID, @OpenID, @OrderNo,@TransactionNo,@ProductBody,@FeeType, @TotalFee, GETDATE(), @OrderDate,@OrderExpire, @TariffID, @OrderStatus, '') ";
                SQLServerOperating s = new MG_DAL.SQLServerOperating();
                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("UserID",UserID),
                    new SqlParameter("DeviceID",DeviceID),
                    new SqlParameter("OpenID",OpenID),
                    new SqlParameter("OrderNo",OrderNo),
                    new SqlParameter("TransactionNo",OrderNo),/// 为了不违反TransactionNo唯一约束,在订单支付成功之前,TransactionNo值暂且用orderNo填充,在支付成功之后将改为微信的订单号
                    new SqlParameter("ProductBody",ProductBody),
                    new SqlParameter("FeeType",FeeType),
                    new SqlParameter("TotalFee",TotalFee),
                    new SqlParameter("OrderDate",OrderDate.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("OrderExpire",OrderExpire.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("TariffID",TariffID),
                    new SqlParameter("OrderStatus",OrderStatus.已提交)
                };
                s.ExecuteSql(strSql, pars);
                Task.Run(() => {
                    Utils.SendTcpCmd("VTR-UpdateHireExpireDate-" + DeviceID);
                }); 
            }
            catch (Exception ex)
            {
                Utils.log("MG_BLL.Pay.MgooOrders.Orders > AddOrder Error: " + ex.Message);
            }
        }


        public string QueryOrder(string OrderNo, string transactionid)
        {
            if (!string.IsNullOrEmpty(OrderNo))
            {
                string strSql = "select [Status] from orders where OrderNo = @OrderNo";
                SQLServerOperating s = new SQLServerOperating();
                return s.Select(strSql, new SqlParameter[] { new SqlParameter("OrderNo", OrderNo) });
            } else if (!string.IsNullOrEmpty(transactionid))
            {

            }
            return string.Empty;
        }
        public List<Dictionary<string, string>> GetPriceList(string Model)
        {
            List<Dictionary<string, string>> PriceList = new List<Dictionary<string, string>>();
            try
            {
                int type = 0;
                if (Model.StartsWith("MG-X1") || Model.StartsWith("MG-X8") || Model.StartsWith("MG-X50"))
                {
                    type = 1;
                }
                else
                {
                    type = 2;
                }
                MG_DAL.SQLServerOperating s = new MG_DAL.SQLServerOperating();
                string strSql = "select ID,TariffName,Price,OldPrice,BuyCount from TariffPackages where [Type]=@type";
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("type", type) });
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic["TariffName"] = row["TariffName"].ToString();
                    dic["Price"] = row["Price"].ToString();
                    dic["BuyCount"] = row["BuyCount"].ToString();
                    dic["OldPrice"] = row["OldPrice"].ToString();
                    dic["ID"] = row["ID"].ToString();
                    PriceList.Add(dic);
                }
            }
            catch (Exception)
            {

            }
            return PriceList;
        }
     
        /// <summary>
        /// 修改内部订单系统的订单状态
        /// </summary>
        public bool ModifyOrderStatus(string transaction_id, string fee_type, string time_end, string bank_type, string trade_type, string trade_no)
        {
            try
            {
                string strSql = "update Orders set TransactionNo=@TransactionNo,FeeType=@FeeType,PayDate=@PayDate,Status=@Status,BankType=@BankType,TradeType=@TradeType where OrderNo=@OrderNo and Status=@Status1";
                MG_DAL.SQLServerOperating s = new MG_DAL.SQLServerOperating();
                int reslut = s.ExecuteSql(strSql,
                new SqlParameter[]
                {
                    new SqlParameter("TransactionNo", transaction_id),
                    new SqlParameter("FeeType",fee_type),
                    new SqlParameter("Status", MgooOrders.OrderStatus.已付款),
                    new SqlParameter("PayDate", time_end),
                    new SqlParameter("BankType",bank_type),
                    new SqlParameter("TradeType",trade_type),
                    new SqlParameter("OrderNo",trade_no),
                    new SqlParameter("Status1", MgooOrders.OrderStatus.已提交)
                });
                if (reslut > 0)
                {
                    try
                    {
                        strSql = "update Devices set HireExpireDate=DATEADD(YEAR,(select RenewalDate from TariffPackages where id =(select TariffID from Orders where OrderNo=@OrderNo)),HireExpireDate) where DeviceID=(select deviceid from Orders where OrderNo=@OrderNo)";
                        reslut = s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("OrderNo", trade_no) });
                        if (reslut <= 0)
                        {
                            Utils.log("修改设备过期时间失败 ModifyOrderStatus Error......." + reslut);
                        }
                        return true;
                    }
                    catch (Exception e)
                    {
                        Utils.log("ModifyOrderStatus Error:" + e.Message);
                    }
                }
                else
                {
                    Utils.log("未找到该订单:" + trade_no);
                }
            }
            catch (Exception ex)
            {
                Utils.log("ModifyOrderStatus Error:" + ex.Message);
            }
            return false;
        }

        public void PaySuccessPush(string OpenID, string DeviceName, string Fee, string PayDate, string Bank, string OrderNo)
        { 
            string rulst = "";
            try
            {
                WeixinOper wo = new WeixinOper();
                string access_token = wo.AccessToken();
                MgoogpsWebClient mwc = new MgoogpsWebClient();
                mwc.RequestUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token;
                SQLServerOperating s = new SQLServerOperating();
                Dictionary<string, string> dic = s.Selects("select ProductBody,tp.RenewalDate from Orders o inner join TariffPackages tp on tp.id=o.TariffID where OrderNo = @OrderNo", new SqlParameter[] { new SqlParameter("OrderNo", OrderNo) }).toDictionary();
                string pushContent = wo.GetPaySuccessText(OpenID, DeviceName, Fee, PayDate, OrderNo, Bank, dic["ProductBody"], dic["RenewalDate"]);
                mwc.RequestPostData = Encoding.UTF8.GetBytes(pushContent);
                rulst = mwc.RequestSend();
                Dictionary<string, string> res = Utils.ToDictionary(rulst);
                if (res["errcode"].Equals("0") && res["errmsg"].Equals("ok"))
                {
                    Utils.log("充值成功提醒推送成功:" + DeviceName);
                }
                else
                {
                    Utils.log("充值成功微信推送失败" + rulst, "WeChatExpirePush.log");
                }
            }
            catch (Exception ex)
            {
                Utils.log("PaySuccessPush Error:" + ex.Message + ",StackTrace:" + ex.StackTrace + ",Source:" + ex.Source + ",rulst：" + rulst);
            }
        }

        public void SendMail(string OrderNo)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add("377108616@qq.com"); // 张飞鸿
            msg.To.Add("2996333214@qq.com"); //罗坤
            msg.To.Add("2451911874@qq.com"); //谢春丽 
            try
            {
                SQLServerOperating s = new SQLServerOperating();
                string strSql = @"select u.UserName, d.DeviceID,d.DeviceName,d.SerialNumber,o.OrderNo,o.ProductBody,o.PayDate,o.TradeType,d.PhoneNum ,d.HireExpireDate,cast(CONVERT(int, o.TotalFee)/100.0 as numeric(6,2)) TotalFee ,tp.TariffName
                                  from orders o inner join Devices d on d.DeviceID=o.DeviceID inner join Users u on u.UserID=o.UserID inner join TariffPackages tp on tp.ID=o.TariffID
                                  where o.Status = @Status and o.OrderNo = @OrderNo";
                DataTable dt = s.Selects(strSql, new SqlParameter[] { new SqlParameter("OrderNo", OrderNo), new SqlParameter("Status", OrderStatus.已付款) });
                DataRow row = dt.Rows[0];
                msg.From = new MailAddress("mgoo_service@163.com", "美谷充值", System.Text.Encoding.UTF8);
                /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
                msg.Subject = "设备充值成功";// + row["ProductBody"].ToString();//邮件标题    
                msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码    
                StringBuilder sbBody = new StringBuilder();
                sbBody.Append("用户名称：" + row["UserName"] + " <br />");
                sbBody.Append("充值设备IMEI：" + row["SerialNumber"] + " <br />");
                sbBody.Append("充值设备名称：" + row["DeviceName"] + " <br />");
                sbBody.Append("订单号：" + row["OrderNo"] + " <br />");
                sbBody.Append("充值套餐：" + row["TariffName"] + " <br />");
                sbBody.Append("充值金额：" + row["TotalFee"] + " <br />");
                sbBody.Append("下次到期时间：" + row["HireExpireDate"] + " <br />");
                sbBody.Append("交易时间：" + row["PayDate"] + " <br />");
                sbBody.Append("流量卡号：" + row["PhoneNum"] + " <br />");
                sbBody.Append("请尽快给该设备充值流量！");
                msg.Body = sbBody.ToString();//邮件内容    

                msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
                msg.Priority = MailPriority.High;//邮件优先级    
                msg.IsBodyHtml = true;//是否是HTML邮件    
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("mgoo_service@163.com", "mgoo123");//登录密码 86768770

                client.Host = "smtp.163.com";
                client.Port = 25;
                //启用ssl,也就是安全发送
                client.EnableSsl = true;
                object userState = msg;
                try
                {
                    //client.SendAsync(msg, userState);
                    client.Send(msg);
                    //简单一点儿可以client.Send(msg);   
                    Utils.log("邮件发送成功:订单号：" + OrderNo);
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    Console.WriteLine(ex.Message);
                    Utils.log("SendMailUseZj1 Error：" + ex.Message + ",body:" + sbBody.ToString() + "，副标题：" + msg.Subject);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                Utils.log("SendMailUseZj2 Error:" + ex.Message + ",发送邮件失败。。。 订单号：" + OrderNo, "PaySuccessEmailFailure.log");
            }
        }

        public string GetBankName(string code)
        {
            string name = "微信支付";
            string bankCode = @"ICBC_DEBIT,ICBC_CREDIT,ABC_DEBIT,ABC_CREDIT,PSBC_DEBIT,PSBC_CREDIT,CCB_DEBIT,CCB_CREDIT,CMB_DEBIT,CMB_CREDIT,BOC_DEBIT,BOC_CREDIT,COMM_DEBIT,COMM_CREDIT,SPDB_DEBIT,SPDB_CREDIT,GDB_DEBIT,GDB_CREDIT,CMBC_DEBIT,CMBC_CREDIT,PAB_DEBIT,PAB_CREDIT,CEB_DEBIT,CEB_CREDIT,CIB_DEBIT,CIB_CREDIT,CITIC_DEBIT,CITIC_CREDIT,BOSH_DEBIT,BOSH_CREDIT,CRB_DEBIT,HZB_DEBIT,HZB_CREDIT,BSB_DEBIT,BSB_CREDIT,CQB_DEBIT,SDEB_DEBIT,SZRCB_DEBIT,SZRCB_CREDIT,HRBB_DEBIT,BOCD_DEBIT,GDNYB_DEBIT,GDNYB_CREDIT,GZCB_DEBIT,GZCB_CREDIT,JSB_DEBIT,JSB_CREDIT,NBCB_DEBIT,NBCB_CREDIT,NJCB_DEBIT,QHNX_DEBIT,ORDOSB_CREDIT,ORDOSB_DEBIT,BJRCB_CREDIT,BHB_DEBIT,BGZB_DEBIT,BEEB_DEBIT,PZHCCB_DEBIT,QDCCB_CREDIT,QDCCB_DEBIT,SHINHAN_DEBIT,QLB_DEBIT,QSB_DEBIT,ZZB_DEBIT,CCAB_DEBIT,RZB_DEBIT,SCNX_DEBIT,BEEB_CREDIT,SDRCU_DEBIT,BCZ_DEBIT,SJB_DEBIT,LNNX_DEBIT,JUFENGB_DEBIT,ZZB_CREDIT,JXNXB_DEBIT,JZB_DEBIT,JZCB_CREDIT,JZCB_DEBIT,KLB_DEBIT,KRCB_DEBIT,KUERLECB_DEBIT,LJB_DEBIT,NYCCB_DEBIT,LSCCB_DEBIT,LUZB_DEBIT,LWB_DEBIT,LYYHB_DEBIT,LZB_DEBIT,MINTAIB_CREDIT,MINTAIB_DEBIT,NCB_DEBIT,NMGNX_DEBIT,XAB_DEBIT,WFB_CREDIT,WFB_DEBIT,WHB_CREDIT,WHB_DEBIT,WHRC_CREDIT,WHRC_DEBIT,WJRCB_DEBIT,WLMQB_DEBIT,WRCB_DEBIT,WZB_DEBIT,XAB_CREDIT,WEB_DEBIT,XIB_DEBIT,XJRCCB_DEBIT,XMCCB_DEBIT,YNRCCB_DEBIT,YRRCB_CREDIT,YRRCB_DEBIT,YTB_DEBIT,ZJB_DEBIT,ZJLXRB_DEBIT,ZJRCUB_CREDIT,AHRCUB_DEBIT,BCZ_CREDIT,SRB_DEBIT,ZYB_DEBIT,ZRCB_DEBIT,SRCB_CREDIT,SRCB_DEBIT,ZJTLCB_DEBIT,SUZB_DEBIT,SXNX_DEBIT,SXXH_DEBIT,ZJRCUB_DEBIT,AE_CREDIT,TACCB_CREDIT,TACCB_DEBIT,TCRCB_DEBIT,TJBHB_CREDIT,TJBHB_DEBIT,TJB_DEBIT,TRCB_DEBIT,TZB_DEBIT,URB_DEBIT,DYB_CREDIT,CSRCB_DEBIT,CZB_CREDIT,CZB_DEBIT,CZCB_CREDIT,CZCB_DEBIT,DANDONGB_CREDIT,DANDONGB_DEBIT,DLB_CREDIT,DLB_DEBIT,DRCB_CREDIT,DRCB_DEBIT,CSRCB_CREDIT,DYB_DEBIT,DYCCB_DEBIT,FBB_DEBIT,FDB_DEBIT,FJHXB_CREDIT,FJHXB_DEBIT,FJNX_DEBIT,FUXINB_DEBIT,BOCDB_DEBIT,JSNX_DEBIT,BOLFB_DEBIT,CCAB_CREDIT,CBHB_DEBIT,CDRCB_DEBIT,BYK_DEBIT,BOZ_DEBIT,CFT,BOTSB_DEBIT,BOSZS_DEBIT,BOSXB_DEBIT,BONX_DEBIT,BONX_CREDIT,GDHX_DEBIT,BOLB_DEBIT,BOJX_DEBIT,BOIMCB_DEBIT,BOHN_DEBIT,BOD_DEBIT,CQRCB_CREDIT,CQRCB_DEBIT,CQTGB_DEBIT,BOD_CREDIT,CSCB_DEBIT,BOB_CREDIT,GDRCU_DEBIT,BOB_DEBIT,HRXJB_DEBIT,HSBC_DEBIT,HSB_CREDIT,HSB_DEBIT,HUNNX_DEBIT,HUSRB_DEBIT,HXB_CREDIT,HXB_DEBIT,HNNX_DEBIT,BNC_DEBIT,BNC_CREDIT,BJRCB_DEBIT,JCB_DEBIT,JJCCB_DEBIT,JLB_DEBIT,JLNX_DEBIT,JNRCB_DEBIT,JRCB_DEBIT,JSHB_DEBIT,HAINNX_DEBIT,GLB_DEBIT,GRCB_CREDIT,GRCB_DEBIT,GSB_DEBIT,GSNX_DEBIT,GXNX_DEBIT,GYCB_CREDIT,GYCB_DEBIT,GZNX_DEBIT,HAINNX_CREDIT,HKB_DEBIT,HANAB_DEBIT,HBCB_CREDIT,HBCB_DEBIT,HBNX_CREDIT,HBNX_DEBIT,HDCB_DEBIT,HEBNX_DEBIT,HFB_DEBIT,HKBEA_DEBIT,JCB_CREDIT,MASTERCARD_CREDIT,VISA_CREDIT";
            string bankName = @"工商银行(借记卡),工商银行(信用卡),农业银行(借记卡),农业银行(信用卡),邮政储蓄银行(借记卡),邮政储蓄银行(信用卡),建设银行(借记卡),建设银行(信用卡),招商银行(借记卡),招商银行(信用卡),中国银行(借记卡),中国银行(信用卡),交通银行(借记卡),交通银行(信用卡),浦发银行(借记卡),浦发银行(信用卡),广发银行(借记卡),广发银行(信用卡),民生银行(借记卡),民生银行(信用卡),平安银行(借记卡),平安银行(信用卡),光大银行(借记卡),光大银行(信用卡),兴业银行(借记卡),兴业银行(信用卡),中信银行(借记卡),中信银行(信用卡),上海银行(借记卡),上海银行(信用卡),华润银行(借记卡),杭州银行(借记卡),杭州银行(信用卡),包商银行(借记卡),包商银行(信用卡),重庆银行(借记卡),顺德农商行(借记卡),深圳农商银行(借记卡),深圳农商银行(信用卡),哈尔滨银行(借记卡),成都银行(借记卡),南粤银行(借记卡),南粤银行(信用卡),广州银行(借记卡),广州银行(信用卡),江苏银行(借记卡),江苏银行(信用卡),宁波银行(借记卡),宁波银行(信用卡),南京银行(借记卡),青海农信(借记卡),鄂尔多斯银行(信用卡),鄂尔多斯银行(借记卡),北京农商(信用卡),河北银行(借记卡),贵州银行(借记卡),鄞州银行(借记卡),攀枝花银行(借记卡),青岛银行(信用卡),青岛银行(借记卡),新韩银行(借记卡),齐鲁银行(借记卡),齐商银行(借记卡),郑州银行(借记卡),长安银行(借记卡),日照银行(借记卡),四川农信(借记卡),鄞州银行(信用卡),山东农信(借记卡),沧州银行(借记卡),盛京银行(借记卡),辽宁农信(借记卡),临朐聚丰村镇银行(借记卡),郑州银行(信用卡),江西农信(借记卡),晋中银行(借记卡),锦州银行(信用卡),锦州银行(借记卡),昆仑银行(借记卡),昆山农商(借记卡),库尔勒市商业银行(借记卡),龙江银行(借记卡),南阳村镇银行(借记卡),乐山市商业银行(借记卡),柳州银行(借记卡),莱商银行(借记卡),辽阳银行(借记卡),兰州银行(借记卡),民泰银行(信用卡),民泰银行(借记卡),宁波通商银行(借记卡),内蒙古农信(借记卡),西安银行(借记卡),潍坊银行(信用卡),潍坊银行(借记卡),威海商业银行(信用卡),威海市商业银行(借记卡),武汉农商(信用卡),武汉农商行(借记卡),吴江农商行(借记卡),乌鲁木齐银行(借记卡),无锡农商(借记卡),温州银行(借记卡),西安银行(信用卡),微众银行(借记卡),厦门国际银行(借记卡),新疆农信银行(借记卡),厦门银行(借记卡),云南农信(借记卡),黄河农商银行(信用卡),黄河农商银行(借记卡),烟台银行(借记卡),紫金农商银行(借记卡),兰溪越商银行(借记卡),浙江农信(信用卡),安徽省农村信用社联合社(借记卡),沧州银行(信用卡),上饶银行(借记卡),中原银行(借记卡),张家港农商行(借记卡),上海农商银行(信用卡),上海农商银行(借记卡),浙江泰隆银行(借记卡),苏州银行(借记卡),山西农信(借记卡),陕西信合(借记卡),浙江农信(借记卡),AE(信用卡),泰安银行(信用卡),泰安银行(借记卡),太仓农商行(借记卡),天津滨海农商行(信用卡),天津滨海农商行(借记卡),天津银行(借记卡),天津农商(借记卡),台州银行(借记卡),联合村镇银行(借记卡),东营银行(信用卡),常熟农商银行(借记卡),浙商银行(信用卡),浙商银行(借记卡),稠州银行(信用卡),稠州银行(借记卡),丹东银行(信用卡),丹东银行(借记卡),大连银行(信用卡),大连银行(借记卡),东莞农商银行(信用卡),东莞农商银行(借记卡),常熟农商银行(信用卡),东营银行(借记卡),德阳银行(借记卡),富邦华一银行(借记卡),富滇银行(借记卡),福建海峡银行(信用卡),福建海峡银行(借记卡),福建农信银行(借记卡),阜新银行(借记卡),承德银行(借记卡),江苏农商行(借记卡),廊坊银行(借记卡),长安银行(信用卡),渤海银行(借记卡),成都农商银行(借记卡),营口银行(借记卡),张家口市商业银行(借记卡),零钱,唐山银行(借记卡),石嘴山银行(借记卡),绍兴银行(借记卡),宁夏银行(借记卡),宁夏银行(信用卡),广东华兴银行(借记卡),洛阳银行(借记卡),嘉兴银行(借记卡),内蒙古银行(借记卡),海南银行(借记卡),东莞银行(借记卡),重庆农商银行(信用卡),重庆农商银行(借记卡),重庆三峡银行(借记卡),东莞银行(信用卡),长沙银行(借记卡),北京银行(信用卡),广东农信银行(借记卡),北京银行(借记卡),华融湘江银行(借记卡),恒生银行(借记卡),徽商银行(信用卡),徽商银行(借记卡),湖南农信(借记卡),湖商村镇银行(借记卡),华夏银行(信用卡),华夏银行(借记卡),河南农信(借记卡),江西银行(借记卡),江西银行(信用卡),北京农商行(借记卡),晋城银行(借记卡),九江银行(借记卡),吉林银行(借记卡),吉林农信(借记卡),江南农商(借记卡),江阴农商行(借记卡),晋商银行(借记卡),海南农信(借记卡),桂林银行(借记卡),广州农商银行(信用卡),广州农商银行(借记卡),甘肃银行(借记卡),甘肃农信(借记卡),广西农信(借记卡),贵阳银行(信用卡),贵阳银行(借记卡),贵州农信(借记卡),海南农信(信用卡),汉口银行(借记卡),韩亚银行(借记卡),湖北银行(信用卡),湖北银行(借记卡),湖北农信(信用卡),湖北农信(借记卡),邯郸银行(借记卡),河北农信(借记卡),恒丰银行(借记卡),东亚银行(借记卡),JCB(信用卡),MASTERCARD(信用卡),VISA(信用卡)";

            string[] banks = bankName.Split(',');
            string[] codes = bankCode.Split(',');
            if (codes.Contains(code))
            {
                int index = Array.IndexOf(codes, code);
                name = banks[index];
            }
            return name;
        }
    }
    /// <summary>
    /// 订单状态
    /// </summary>
   public  enum OrderStatus
    {
        已提交 = 1,      
        已付款 = 2,     
        已关闭 = 3, 
        申请退款 = 4,  
        已退款 = 5,  
    }
}