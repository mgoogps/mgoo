using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml;

namespace MG_BLL.DeviceManager
{
    public class CommandOper
    {
        public string SendCommand(string CommandType,string userid, string deviceid, string Phone, string mode = null, string sens = null)
        {
            //http://api.mgoogps.com/service/sendcommand/?param=id,4210033800&model,X&com,BD&phone,13800000000
            string url = string.Format("/service/sendcommand/?param=id,{0}&model,{1}", deviceid, "X");

            XmlOperate xml = new XmlOperate(deviceid,userid);
            switch (CommandType)
            {
                case "1":
                    url += string.Format("&com,BD&phone,{0}", Phone); //绑定手机 
                    mode = "";  sens = "";
                    break;
                case "2":
                    url += string.Format("&com,BDR");//解绑手机 
                    Phone = "";  mode = "";  sens = "";
                    break;
                case "3":
                    url += string.Format("&com,ZD&mode,{0}&sens,{1}", mode, sens); // 设置震动报警方式
                    Phone = "";
                    break;
                case "4":
                    url += string.Format("&com,SF"); //  	设防
                    break;
                case "5":
                    url += string.Format("&com,CF"); //  	撤防
                    break;
                case "6":
                    url += string.Format("&com,DY"); //  断油电 
                    break;
                case "7":
                    url += string.Format("&com,TY"); //  通油电
                    break;
                default:
                    break;
            }
            MG_DAL.MgoogpsWebClient mwc = new MG_DAL.MgoogpsWebClient();
            mwc.RequestMethodName = url;
            string json = mwc.RequestSend();
            if (json.Equals("\"ok\""))
            {
                xml.CreateXml(CommandType,deviceid, Phone, mode,sens);
              // string ph = xml.GetXml("Phone");
                return "success";
            }
            return "error";
        }

        public string SendCommand(string deviceid)
        {
            try
            {
                MG_DAL.SQLServerOperating s = new MG_DAL.SQLServerOperating();
                string imei = s.Selects("select SerialNumber from devices where deviceid=@deviceid and Deleted=0",
                    new SqlParameter[] { new SqlParameter("deviceid", deviceid) }).Rows[0]["SerialNumber"].toStringEmpty();
                string cmd = "VTR-SCommand-" + imei+"-TY";
                string res = Utils.SendTcpCmd(cmd);
                Utils.log(" MG_BLL.DeviceManager.SendCommand:" + cmd + "," + res);
                if (res == "1")
                {
                    return "success";
                }
                {
                    return "error";
                }
            }
            catch (Exception e)
            {
                Utils.log("MG_BLL.DeviceManager.SendCommand Error:"+e.Message+"-"+e.StackTrace+"-"+e.Source);
                return "error";
            }
        }
    }
    public enum Command
    {
        /// <summary>
        /// 绑定手机
        /// </summary>
        BindPhone,
        /// <summary>
        /// 解绑手机
        /// </summary>
        UnbundPhone,  
        /// <summary>
        /// 设置震动报警方式
        /// </summary>
        VibrationMode ,
        /// <summary>
        /// 设防
        /// </summary>
        Protect,
        /// <summary>
        /// 撤防
        /// </summary>
        Disarm,
        /// <summary>
        /// 断油电
        /// </summary>
        OffPetrolAndElectricity,
        /// <summary>
        /// 通油电
        /// </summary>
        OnPetrolAndElectricity,
       
    }
}
