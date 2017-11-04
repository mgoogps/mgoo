using MG_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_BLL.Weixin
{
    public class Message
    {
        private Common.AuthHeader myHeader = new Common.AuthHeader();
        private string cacheKey = null;
        public Message (Common.AuthHeader header) {
            this.myHeader = header;
            cacheKey = myHeader.Identifies + myHeader.UserID + myHeader.Token;
        }

        public List<Dictionary<string, string>> GetMessageList(string currentIndex, string pageCount, string userid, string type)
        {
            try
            {
                if (string.IsNullOrEmpty(type))
                {
                    type = "2";
                }
                string where = "";
                if (type.Equals("0")) //查看未删除的
                {
                    where = " and e.Deleted = 0 ";
                }
                else if (type.Equals("1")) //查看已删除的
                {
                    where = " and e.Deleted = 1 ";
                }
                else if (type.Equals("2"))//查看全部报警信息
                {   }
                else
                {
                    return new List<Dictionary<string, string>>();
                }
                string strSql = string.Format(@"select top {0} * from(
                             select row_number() over(order by e.[Created] desc) rowIndex,
                             e.[ExceptionID], case when geo.FenceName is null then e.Message else e.Message+':'+geo.FenceName end Message , dateadd(HH,8, e.[Created])[Created], d.DeviceName, d.SerialNumber, e.ClearDate, e.ClearBy 
                             from ExceptionMessage e inner join Devices d on e.DeviceID = d.DeviceID
                             left join GeoFence geo on geo.GeofenceID=e.GeoFenceID 
                             where d.deleted =0 and d.userid = @userid " + where+@"
	                         ) t where rowIndex > {0} * ({1} - 1) order by  Created desc", pageCount, currentIndex);
                SQLServerOperating s = new SQLServerOperating();
                return s.Selects(strSql, new SqlParameter[] {new SqlParameter("userid", userid) }).toListDictionary();
            }
            catch (Exception ex)
            {
                Utils.log("GetMessageList Error:" + ex.Message);
                return new List<Dictionary<string, string>>();
            }
        }

        public string DeleteMessage(string userid, string ExceptionID )
        {
            try
            {
                if (string.IsNullOrEmpty(ExceptionID))
                {
                    return  Utils.GetResult("至少选择一条报警消息.", statusCode.Code.failure);
                } 
                string[] msgid = ExceptionID.Split(',');
                List<int> ids = new List<int>();
                for (int i = 0; i < msgid.Length; i++)
                {
                    int temp;
                    if (int.TryParse(msgid[i], out temp))
                    {
                        ids.Add(temp);
                    }
                }
                string strSql = "update ExceptionMessage set Deleted=1,ClearDate=GETDATE(),ClearBy=@UserID where ExceptionID in(" + string.Join(",", ids.ToArray()) + ")  and Deleted=0";
                if (myHeader.Identifies == "MG_XCX@AMAP")
                {
                    Utils.log(strSql);
                }
                SQLServerOperating s = new SQLServerOperating();
                int count = s.ExecuteSql(strSql,new SqlParameter[] { new SqlParameter ("UserID", userid) });
                return Utils.GetResult("清除成功" + count + "条", statusCode.Code.success);
            }
            catch (Exception ex)
            {
                Utils.log("Message>DeleteMessage ERROR:" + ex.Message);
                return Utils.GetResult(ex.Message, statusCode.Code.error);
            }
        }

        public Dictionary<string, string> GetMessageInfoByID(string eid)
        {
            try
            {
                SQLServerOperating s = new SQLServerOperating();
                string strSql = @"select CASE when d.devicename='' then d.SerialNumber else d.DeviceName end DeviceName,dateadd(HH,8, em.created) Created,em.OLat,em.OLng ,
                                case when geo.FenceName is null then em.Message else em.Message+':'+geo.FenceName end Message
                                from ExceptionMessage em inner join Devices d on d.DeviceID=em.DeviceID 
                                left join GeoFence geo on geo.GeofenceID=em.GeoFenceID 
                                where ExceptionID=@ExceptionID";
                DataTable table = s.Selects(strSql, new SqlParameter[] { new SqlParameter("ExceptionID", eid) });
                Dictionary<string, string> dic = new Dictionary<string, string>();
                
                Geocoding geo = GetCurrentMapType();
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn dc in table.Columns)
                    {
                        dic[dc.ColumnName] = row[dc.ColumnName].toStringEmpty();
                    }
                    //Gps gps = Utils.gps84_To_Gcj02(dic["OLat"], dic["OLng"],key);
                    Gps gps = geo.Translate(dic["OLat"], dic["OLng"]);
                    dic["OLat"] = gps.getWgLat().toStringEmpty();
                    dic["OLng"] = gps.getWgLon().toStringEmpty();
                    dic["Address"] = gps.Address.toStringEmpty();
                }
                return dic;
            }
            catch (Exception ex)
            {
                Utils.log("Message>GetMessageInfoByID ERROR:"+ex.Message);
                return new Dictionary<string, string>();
            }

        }

        public List<Dictionary<string, string>> GetMessageByDeviceID(string currentIndex, string pageCount, string deviceid,string type)
        {
            try
            {
                if (string.IsNullOrEmpty(currentIndex) || currentIndex == "undefined")
                {
                    return new List<Dictionary<string, string>>();
                }
                if (string.IsNullOrEmpty(type))
                {
                    type = "2";
                }
                string where = "";
                if (type.Equals("0")) //查看未删除的
                {
                    where = " and ex.Deleted = 0 ";
                }
                else if (type.Equals("1")) //查看已删除的
                {
                    where = " and ex.Deleted = 1 ";
                }
                else if (type.Equals("2"))//查看全部报警信息
                { }
                else
                {
                    return new List<Dictionary<string, string>>();
                }
                string strSql = string.Format(@"select top {0} * from(
                                    select row_number() over(order by ex.[Created] desc) rowIndex, ex.ExceptionID, CASE when d.devicename = '' then d.SerialNumber else d.DeviceName end DeviceName,
                                    d.SerialNumber, d.DeviceID,case when geo.FenceName is null then ex.Message else ex.Message+':'+geo.FenceName end Message, dateadd(HH, 8, ex.created) Created ,ex.ClearBy,ex.ClearDate
                                    from ExceptionMessage ex inner join devices d on d.deviceid = ex.deviceid
                                    left join GeoFence geo on geo.GeofenceID=ex.GeoFenceID 
                                    where d.DeviceID = @DeviceID " + where+ @"
                                ) t where rowIndex > {0} * ({1} - 1) order by  Created desc", pageCount, currentIndex);

                SQLServerOperating s = new SQLServerOperating();
                return s.Selects(strSql, new SqlParameter[] { new SqlParameter("DeviceID", deviceid) }).toListDictionary();
    
           }
            catch (Exception ex)
            {
                Utils.log("Message>GetMessageByDeviceID ERROR:" + ex.Message);
                return new List<Dictionary<string, string>>();
            }
         
        }

        /// <summary>
        /// 获取所有的报警类型
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, string>> GetExceptionType()
        {
            SQLServerOperating s = new SQLServerOperating();
            String strSql = "select Message,NotificationType as ID from ExceptionMessage group by Message,NotificationType";
            return s.Selects(strSql).toListDictionary();
        }
        /// <summary>
        /// 获取微信推送通知的报警类型
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GetPushMsgByUserID(string UserID)
        {
            string strSql = "select PushMsgType from UsersConfig where UserID=@UserID";
            SQLServerOperating s = new SQLServerOperating();
            return s.Select(strSql, new SqlParameter[]{new SqlParameter("UserID", UserID) });
        }
        /// <summary>
        /// 设置微信推送通知的报警类型
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string SetPushMsgType(string msgType)
        {
            try
            {
                string[] mt = msgType.Split(',');
                int[] ls = new int[mt.Length];
                for (int i = 0; i < mt.Length; i++)
                {
                    int t;
                    if (int.TryParse(mt[i], out t))
                    {
                        ls[i] = t;
                    }
                }
                string mstType = ","+string.Join(",", ls)+",";
                string strSql = " update UsersConfig set PushMsgType=@PushMsgType where UserID=@UserID";
                SQLServerOperating s = new SQLServerOperating();
                int count = s.ExecuteSql(strSql, new SqlParameter[] { new SqlParameter("PushMsgType", mstType), new SqlParameter("UserID", myHeader.UserID) });
                if (count > 0)
                {
                    return Utils.GetResult("设置成功.", statusCode.Code.success); ;
                }
                else
                {
                    Utils.log("SetPushMsgType 操作失败:" + strSql + ", UserID:" + myHeader.UserID + ",mstType:" + msgType);
                    return Utils.GetResult("设置失败.", statusCode.Code.failure);
                }
            }
            catch (Exception ex)
            {
                Utils.log("Message>SetPushMsgType ERROR:" + ex.Message);
                throw;
            }
         
            
        }

        public List<Dictionary<string, string>> GetMessageTypeList()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            try
            {
                string strSql = " select [Message],[NotificationType] as ID from ExceptionMessage e inner join Devices d on d.DeviceID=e.DeviceID where d.UserID=@UserID group by [Message],[NotificationType] order by ID";
                SQLServerOperating s = new SQLServerOperating();
                DataTable msgTypeDataTable = s.Selects(strSql, new SqlParameter[] { new SqlParameter("UserID", myHeader.UserID) });
                string pushMsgType = s.Select("select PushMsgType from [UsersConfig] where UserID=@UserID", new SqlParameter[] { new SqlParameter("UserID", myHeader.UserID) });
                string[] msgList = pushMsgType.Split(','); 
                foreach (DataRow row in msgTypeDataTable.Rows)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach (DataColumn col in msgTypeDataTable.Columns)
                    {
                        dic[col.ColumnName] = row[col.ColumnName].ToString();
                    }
                    if (msgList.Contains(row["ID"].ToString()))
                    {
                        dic["IsPush"] = "0";//推送
                    }
                    else
                    {
                        dic["IsPush"] = "1";//不推送
                    }
                    list.Add(dic);
                }
            }
            catch (Exception e)
            {
                Utils.log("GetMessageTypeList Error:" + e.Message);
            }
            return list;
        }

        public Dictionary<string, string> GetUsersConfig()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                string strSql = @"select ISNULL(uc.PushAudio,0) Audio,ISNULL(uc.PushShock,0) Shock,ISNULL(ShockSens,0) ShockSens ,ISNULL(PushPeriod,1) Period
                             from users u left join UsersConfig uc on uc.UserID=u.UserID
                             where u.userid = @userid";
                SQLServerOperating s = new SQLServerOperating();
                List<Dictionary<string, string>> list = s.Selects(strSql, new SqlParameter[] { new SqlParameter("userid", myHeader.UserID) }).toListDictionary();
                dic = list[0];
            }
            catch (Exception ex)
            {
                Utils.log( "GetUsersConfig Error:"+ex.Message);
              
            }
            return dic;
        }

        public string SetUsersConfig(string ConfigData)
        {
            Dictionary<string,string> dic = Utils.ToDictionary(ConfigData);
            List<string> whereList = new List<string>();
            Dictionary<string, string> par = new Dictionary<string, string>();
            if (dic.ContainsKey("audio"))
            {
                whereList.Add(" PushAudio=@PushAudio "); 
                par["PushAudio"] = dic["audio"];
            }
            if (dic.ContainsKey("shock"))
            {
                whereList.Add(" PushShock=@PushShock "); 
                par["PushShock"] = dic["shock"];
            }
            if (dic.ContainsKey("period"))
            {
                whereList.Add(" PushPeriod=@PushPeriod ");
                par["PushPeriod"] = dic["period"];
            }
            whereList.Add(" UpdateTime=GETDATE() ");
            string pars = string.Join(",",whereList);
            string strSql = "update UsersConfig set "+ pars + " where UserID=@UserID";
            SqlParameter[] parsList = new SqlParameter[par.Count+1];
            parsList[0] = new SqlParameter("UserID", myHeader.UserID);
            int index = 1;
            foreach (KeyValuePair<string,string> item in par)
            {
                parsList[index] = new SqlParameter(item.Key,item.Value);
                index++;
            }
            SQLServerOperating s = new SQLServerOperating();
            if (s.ExecuteSql(strSql, parsList) > 0)
            {
                return Utils.GetResult("设置成功.", statusCode.Code.success);
            }
            else
            {
                return Utils.GetResult("设置失败.", statusCode.Code.failure);
            }
        }

        public List<Dictionary<string, string>> GetMessageByDeviceID(int DeviceID, int second)
        {
            try
            {
                string strSql = $"select top 100 DeviceID,Message,Created from [ExceptionMessage] where DeviceID=@DeviceID and created > dateadd(ss,@second,dateadd(HH,-8, getdate()))";
                SQLServerOperating s = new SQLServerOperating();
                SqlParameter[] par = new SqlParameter[] { new SqlParameter("DeviceID", DeviceID), new SqlParameter("second", second - second * 2) };
                List<Dictionary<string, string>> list = s.Selects(strSql, par).toListDictionary();
                return list;
            }
            catch (Exception ex)
            {
                Utils.log("GetMessageByDeviceID:"+ex.Message);
                return new List<Dictionary<string, string>>();
            } 
        }

        public Geocoding GetCurrentMapType() 
        {
            Geocoding geo;
            if (Utils.GetCache<LoginUserInfo>(cacheKey).MapType == MapType.BAIDU)
            {
                geo = new Baidu();
                geo.key = Utils.GetBaiDuKey();
            }
            else
            {
                geo = new Amap();
                geo.key = Utils.GetAmapKey();
            }
            return geo;
        }
    }
}
