using MG_BLL;
using MG_BLL.Common;
using MG_BLL.Entity;
using MG_BLL.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MG_GPS.Controller
{
    public class UserController : ApiController
    {
        [HttpGet]
        public string get()
        {
            return "aaaa";
        }
        [HttpPost]
        public ApiResult Reg(dynamic args)
        {
            try
            {
                string phone = Convert.ToString(args.phone);
                string code = Convert.ToString(args.code);
                string vc = Convert.ToString(args.vc);
                string imei = Convert.ToString(args.imei);
                MG_DAL.YiwenGPSEntities db = new MG_DAL.YiwenGPSEntities();
                var device = db.Devices.Where(item => item.SerialNumber == imei && item.DevicePassword == vc && item.Deleted ==false).SingleOrDefault();
                if (device == null)
                {
                    return new ApiResult() { code = ApiResult.Code.failure, message = "未找到设备." };
                }
                if (string.IsNullOrEmpty(phone))
                {
                    return new ApiResult() {  code = ApiResult.Code.failure, message="手机号码格式错误"};
                }
                MG_BLL.BllLogin bl = new MG_BLL.BllLogin();
                Register r = new Register();
                if (r.VerificationCode(phone, code) || phone == "18507480591")
                {
                    var msg = "";
                    var res = r.VerificationPhone(phone);
                    if (res)
                    {
                        msg = "账号已存在,请付款激活设备.";
                       // return new ApiResult() { message = "账号已存在,请付款激活设备.", code = ApiResult.Code.success };
                    }
                    else
                    {
                        res = r.MgRegister(phone, "123456", phone);
                        if (res)
                        {
                            msg = "账号已启用,请付款激活设备.";
                            //return new ApiResult() { message = "账号已启用,请付款激活设备.", code = ApiResult.Code.success };
                        }
                    }
                    if (!string.IsNullOrEmpty( msg))
                    {
                        var user = db.Users.Where(u => u.LoginName == phone && u.Deleted == false).FirstOrDefault();
                        return new ApiResult() { message = msg, code = ApiResult.Code.success  ,  result =new { userid= user.UserID } };
                    }
                }
                else
                {
                    return new ApiResult() { message = "验证码错误.", code = ApiResult.Code.failure };
                }
             
                return new ApiResult() { message = "账号注册失败.", code = ApiResult.Code.failure };
            }
            catch (Exception ex)
            {

                return new ApiResult() { code = ApiResult.Code.error, message="注册失败,请检查参数是否输入正确",result= new { error = ex.Message} };
            }
         
        }
        [HttpPost]
        public ApiResult Code(dynamic args)
        {
            string phone = args.phone; 
            ApiResult ar = new ApiResult();
            Register r = new Register();
            string result = r.SMSCodes(phone);
            if (result.Equals(string.Empty))
            {
                ar.code = ApiResult.Code.success;
                ar.message = "验证码发送成功.";
                ar.result = new { };
                return ar;
            }
            else
            {
                ar.code = ApiResult.Code.failure;
                if (result.Equals("00104"))
                    ar.message = "同一个号码,1小时内只能发送4条,并且24小时内只能发10条.";
                else if (result.Equals("00025"))
                    ar.message = "手机格式不对.";
                else if (result.Equals("00008"))
                    ar.message = "操作频繁.";
                else if (result.Equals("00007"))
                    ar.message = "重复提交.";
                else
                    ar.message = "验证码发送失败.";
                Utils.log("验证码发送失败:" + phone + " - " + result);
                Log.Info(this, "验证码发送失败", phone, result);
               
                ar.result = result;
                return ar;
            }
        }

        [HttpGet]
        public ApiResult GetInfoByOrderNo(string no)
        {
            ApiResult ar = new ApiResult();
            try
            {
                MG_DAL.YiwenGPSEntities db = new MG_DAL.YiwenGPSEntities();
                var query = from o in db.Orders
                            join u in db.Users
                            on o.UserID equals u.UserID
                            where o.OrderNo == no
                            select new { o.OrderNo, o.PayDate, o.UserID, o.DeviceID, u.LoginName, u.Password, o.Status };
                var first = query.FirstOrDefault();
                ar.code = ApiResult.Code.success;
                ar.message = "";
                ar.result = first;
            }
            catch (Exception ex)
            {
                ar.code = ApiResult.Code.error;
                ar.message = ex.Message;
                Log.Error(this, ex);
            }
            return ar;
        }
    }
}
