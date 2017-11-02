using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Utils
    {
        public static int offLineMinute
        {
            get { return 20; }
        }

        

        public static void DataSetToExcel(DataTable table, string file, String title)
        {

            FileStream fs = new FileStream(file, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.UTF8);
            sw.Write("<table style='font-weight:bold;'>");
            sw.Write(title);
            foreach (DataRow row in table.Rows)
            {
                sw.Write("<tr>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    sw.Write("<td style='border:1px solid #000000'>");
                    // line += row[i].ToString() + "              \t"; //内容：自动跳到下一单元格 
                    if (table.Columns[i].ColumnName == "TimediffMinute" && row[i].ToString().Trim() != "")
                    {
                        sw.Write(MinuteToHour(double.Parse(row[i].ToString())));
                    }
                    else if (table.Columns[i].ColumnName == "DeviceStatus")
                    {
                        if (row["LastCommunication"] == null || string.IsNullOrEmpty(row["LastCommunication"].ToString()))
                        {
                            sw.Write("未激活");
                        }
                        else
                        {
                            TimeSpan ts = (DateTime.Now - Convert.ToDateTime(row["LastCommunication"].ToString()));
                            if (ts.TotalMinutes > Utils.offLineMinute)
                            {
                                sw.Write("离线" + MinuteToHour(ts.TotalMinutes, true));
                            }
                            else
                            {
                                sw.Write("在线");
                            }
                        }
                        // sw.Write(MinuteToHour());
                    }
                    else
                    {
                        sw.Write(row[i].ToString());
                    }
                    sw.Write("</td>");
                }
                sw.Write("</tr>");
            }
            sw.Write("</table>");
            sw.Close();
            fs.Close();
        }

        public static String MinuteToHour(double mi, bool b = false)
        {
            try
            {
                if (mi <= 0)
                    return "0分钟";
                if (mi <= 60)
                {
                    return Math.Floor(mi) + "分钟";
                }
                var day = Math.Floor(mi / 60 / 24);
                var h = Math.Floor(mi / 60 % 24);
                var m = Math.Floor(mi % 60);
                string StrReturn = "";
                if (day > 0)
                {
                    StrReturn = day + "天";
                    if (b)
                        return StrReturn;
                }
                if (h > 0)
                {
                    StrReturn += h + "小时";
                    if (b)
                        return StrReturn;
                }
                if (m > 0)
                {
                    StrReturn += m + "分钟";
                    if (b)
                        return StrReturn;
                }
                return StrReturn;
            }
            catch (Exception e)
            {
                return "";
            }

        }

        public static String GetAddressByLatlng(string lat, string lng)
        {
            try
            {
                decimal BaiduLat = 0;
                decimal BaiduLng = 0;
                decimal.TryParse(lat, out BaiduLat);
                decimal.TryParse(lng, out BaiduLng);
                MG_BLL.POIService.POIServiceSoapClient poi = new MG_BLL.POIService.POIServiceSoapClient();
                return poi.GetAddressByLatlng(BaiduLat, BaiduLng, "BAIDU", "ZH-CN");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

    }
}
