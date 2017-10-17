using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MG_DAL;

namespace MG_BLL.Weixin
{
    public class Feedback
    {
        public string Base64ToImage(string base64)
        { 
            try
            {
                if (string.IsNullOrEmpty( base64)) 
                {
                    return string.Empty;
                }
                string filename = DateTime.Now.Ticks + new Random().Next(1, 10000) + ".png";
                byte[] arr = Convert.FromBase64String(base64);
                Utils.log(filename+"大小："+arr.Length);
                if (arr.Length > 2048*1024)
                {

                }
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(arr))
                {
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                    System.Drawing.Bitmap bmp2 = bmp;// new System.Drawing.Bitmap(500 ,500, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
              
                    System.Drawing.Graphics draw = System.Drawing.Graphics.FromImage(bmp2);
                    draw.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));
                    draw.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                    //设置高质量,低速度呈现平滑程度
                    draw.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    string path = System.AppDomain.CurrentDomain.BaseDirectory + @"/FeedbackImage/";
                   // string filename = DateTime.Now.Ticks + new Random().Next(1, 10000) + ".png";
                    DirectoryInfo dir = new DirectoryInfo(path);
                    if (!dir.Exists)
                    {
                        Utils.log("创建路径："+ path);
                        dir.Create();
                    }
                    bmp2.Save(path + filename); 
                    bmp.Dispose(); 
                    bmp2.Dispose();
                    return filename;
                }
            }
            catch (Exception ex)
            {
                Utils.log("Feedback.cs > Base64ToImage Error:" + ex.Message);
                return string.Empty;
            }
        }

        public string AddFeedback(string question, string contact, string image1, string image2, string image3, string image4)
        {
            try
            {
                if (string.IsNullOrEmpty(image1) && string.IsNullOrEmpty(image2) && string.IsNullOrEmpty(image3) && string.IsNullOrEmpty(image4))
                {
                    return Utils.GetResult("至少要有一张图片!", statusCode.Code.success);
                }
                image1 = Base64ToImage(image1);
                image2 = Base64ToImage(image2);
                image3 = Base64ToImage(image3);
                image4 = Base64ToImage(image4); 
              
                string strSql = "Insert into feedback (Content,Contact,Created,Status,Image1,Image2,Image3,Image4,Deleted) values (@Content,@Contact,@Created,@Status,@Image1,@Image2,@Image3,@Image4,@Deleted)";
                SqlParameter[] pars = new SqlParameter[] {
                    new SqlParameter("Content",question),
                    new SqlParameter("Contact", contact),
                    new SqlParameter("Created", DateTime.Now),
                    new SqlParameter("Status","已通知管理员"),
                    new SqlParameter("Image1",image1),
                    new SqlParameter("Image2",image2),
                    new SqlParameter("Image3",image3),
                    new SqlParameter("Image4",image4),
                    new SqlParameter("Deleted","0")
                };
                SQLServerOperating s = new SQLServerOperating();
                int count = s.ExecuteSql(strSql, pars);
                if (count > 0)
                {
                    return Utils.GetResult("提交成功!", statusCode.Code.success);
                }
                else
                {
                    return Utils.GetResult("提交失败!", statusCode.Code.failure);
                }
            }
            catch (Exception ex)
            {
                Utils.log("Feedback.cs > AddFeedback Error:" + ex.Message + ",堆栈:" + ex.StackTrace+",源："+ex.Source);
                return Utils.GetResult(ex.Message, statusCode.Code.error );
            }
        }
    }
}
