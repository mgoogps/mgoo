using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class Tracking : System.Web.UI.Page
    {
        public String DeviceID = "", UserID = "", DeviceName = "", SpeedLimit="";
        protected void Page_Load(object sender, EventArgs e)
        {
            com.Utils.isLogin();
            if (Request.QueryString["deviceid"] != null && Request.QueryString["deviceid"] != "" && Request.QueryString["id"] != null && Request.QueryString["id"]!="")
            {
                try
                {
                    DeviceID = int.Parse(Request.QueryString["deviceid"]).ToString();
                    UserID = int.Parse(Request.QueryString["id"]).ToString();
                    Hashtable table = com.MyTeam.GetDeviceNumber(com.Utils.GetSession("UserInfo").UserID);
                    string[] userids = table["userids"].ToString().Split(',');
                    if (!userids.Contains(UserID.ToString()))
                    {
                        Response.Write("<script>alert('参数错误！!')</script>");
                        Response.Redirect("~/main.aspx");
                    }
                    DataTable dt = com.MyTeam.getDeviceByDeviceID(DeviceID, UserID);
                    if (dt.Rows.Count > 0)
                    {
                        SpeedLimit = double.Parse(dt.Rows[0]["SpeedLimit"].ToString()) > 0 ? dt.Rows[0]["SpeedLimit"].ToString() : "";
                        DeviceName = dt.Rows[0]["DeviceName"].ToString().Trim() == "" ? dt.Rows[0]["SerialNumber"].ToString() : dt.Rows[0]["DeviceName"].ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('参数错误！');</script>");
                    }
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('参数错误！');</script>");
                }
            }
        }
    }
}