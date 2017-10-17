using MG_BLL;
using MG_BLL.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace MG_WeChat.ajax
{
    /// <summary>
    /// GroupsAjax 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class GroupsAjax : System.Web.Services.WebService
    {

        public MG_BLL.Common.AuthHeader myHeader;

        [SoapHeader("myHeader")]
        [WebMethod(Description = "新增分组.")]
        public string AddGroups(string userid, string groupname)
        {
            Groups d = new Groups(myHeader);
            string r = d.AddGroups(userid, groupname);
            if (r != string.Empty)
            {
                return Utils.GetResult( "添加成功.",  statusCode.Code.success, r);
            }
            else
            {
                return Utils.GetResult( "添加失败", statusCode.Code.failure, r);
            }
        }
        [SoapHeader("myHeader")]
        [WebMethod(Description = "删除分组.")]
        public string UpdateGroups (string groupid, string groupname)
        { 
            Groups g = new Groups(myHeader);
            return g.UpdateGroups(groupid, groupname);
        }

        [SoapHeader("myHeader")]
        [WebMethod(Description = "删除分组.")]
        public string DeleteGroups(string groupid)
        {
            Groups g = new Groups(myHeader);
            string r = g.DeleteGroups(groupid);
            if (r.Equals(string.Empty))
            {
                return Utils.GetResult("删除分组成功.", statusCode.Code.success,"");
            }
            else
            {
                return Utils.GetResult(r, statusCode.Code.failure, "");
            }
        }
    }
}
