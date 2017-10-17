using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MG_BLL;
using System.IO;

namespace MG_GPS
{
    public partial class Index : MG_BLL.BasePage
    {
        public List<Dictionary<string,string>> zTree = new List<Dictionary<string, string>>();
        protected void Page_Load(object sender, EventArgs e)
        { 
            try
            {
                BllMonitor mo = new BllMonitor();
                zTree = mo.GetUserZTree(); 
            }
            catch {   }
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFile File = Request.Files[0];
                    if (File.ContentLength > 2097152)
                    {
                        Response.Write("{\"success\":\"false\"}" );
                    }   
                    String filepath = HttpContext.Current.Server.MapPath("~") + @"Upload\";
                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    String fileName = DateTime.Now.ToString("yyMMddhhmmss") + ".png";
                    File.SaveAs(filepath + fileName);
                    Response.Write(new Utils().GetThumbnail(filepath + fileName, filepath + "MG-" + fileName, 130, 140, "MG-" + fileName));
                }
            }
            catch
            {
                Response.Write("{\"success\":\"false\"}" );
            }
            finally
            {
                if (Request.Files.Count > 0)
                {
                    Response.End();
                }
            }
         
        }
    }
}