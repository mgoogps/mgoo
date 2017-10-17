
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class AlarmMessage : System.Web.UI.Page
    {
        public DataTable DictionaryTable = new DataTable();
        public DataTable MessageTypeList = new DataTable();
        public String date = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            com.Utils.isLogin();
            date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 到 " + DateTime.Now.ToString("yyyy-MM-dd");
            DictionaryTable = com.MyTeam.GetDictionaryList(com.Utils.GetSession().UserID);

            MessageTypeList = com.MyTeam.GetMessageTypeList();

        }
    }
}