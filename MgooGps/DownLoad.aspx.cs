using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MgooGps
{
    public partial class DownLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String path = @"E:\MgooGps\MgooGps\MgooGps\AjaxService\Log\1.xls";
            FileInfo Fi = new FileInfo(path);
            if (Fi.Exists)
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length]; 
                fs.Read(bytes, 0, bytes.Length);
                fs.Close(); 
                Response.ContentType = "application/octet-stream";//通知浏览器下载文件而不是打开
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(path, System.Text.Encoding.UTF8));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }
    }
}