using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
namespace MG_BLL
{
    public class XmlOperate
    {
        private XmlDocument xDoc  ; 
        private XmlElement xElement;
        private string path ;
        private string filename;
        private string deviceid;
        public XmlOperate(string deviceid,string userid)
        {
            this.deviceid = deviceid; 
            path = System.Web.HttpContext.Current.Server.MapPath("../") + "/CommandXml/" + userid;
            filename = path + "/" + deviceid + ".xml";
        }

        public void CreateXml(string command,string deviceid ,string phone = null,string mode = null,string sens=null)
        {
            xDoc = new XmlDocument();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                if (File.Exists(filename))
                {
                    xDoc.Load(filename);
                }
            }
            if (xDoc.ChildNodes.Count != 0)
            {
                XmlNode xmlNode;
                switch (command)
                {
                    case "1":
                        xmlNode = xDoc.GetElementsByTagName("Phone")[0];
                        xmlNode.InnerText = phone;
                        break;
                    case "2":
                        xmlNode = xDoc.GetElementsByTagName("Phone")[0];
                        xmlNode.InnerText = "";
                        break;
                    case "3":
                        xmlNode = xDoc.GetElementsByTagName("Mode")[0];
                        xmlNode.InnerText = mode;
                        xmlNode = xDoc.GetElementsByTagName("Sens")[0];
                        xmlNode.InnerText = sens;
                        break;
                }
                xDoc.Save(filename);
                return;
            }
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlDeclaration xmldecl;
            xmldecl = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xDoc.AppendChild(xmldecl);
            //加入一个根元素
            xElement = xDoc.CreateElement("", "MgooGps", "");
            xDoc.AppendChild(xElement);
            XmlNode root = xDoc.SelectSingleNode("MgooGps");//查找<Employees> 
            XmlElement node = xDoc.CreateElement("Phone");
            node.InnerText = phone;
            root.AppendChild(node);
            node = xDoc.CreateElement("Mode");
            node.InnerText = mode;
            root.AppendChild(node);
            node = xDoc.CreateElement("Sens");
            node.InnerText = sens;
            root.AppendChild(node); 
            xDoc.Save(filename);
        }

        public string GetCommandXmlNode()
        { 
            xDoc = new XmlDocument();
       
            if (!File.Exists(filename))
            {
                return "";
            }
             xDoc.Load(filename);
             XmlNode node = xDoc.GetElementsByTagName("MgooGps")[0];

            XmlNodeList nodeList = node.ChildNodes;
           
            Dictionary<string, string> relDic = new Dictionary<string, string>();
            for (int i = 0; i < nodeList.Count; i++)
            { 
                relDic[nodeList[i].Name] = nodeList[i].InnerText;
            }
             //return node.InnerText;
           return Utils.ToJson(relDic);
        }


    }
}
