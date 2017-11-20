﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace Chapter34_XmlReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReadXml();
            //WriteXml();
            //XMLDocument();
            WriteXmlDocument();
            Console.Read();
        }
        static void ReadXml()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            XmlReader reader = XmlReader.Create("config.xml");
            
            //while(reader.Read())
            //{
            //    if (reader.NodeType == XmlNodeType.Text)
            //    {
            //        sb.Append(reader.Value + "\r\n");
            //    }
            //    if (reader.NodeType == XmlNodeType.Element)
            //    {
            //        if (reader.HasAttributes)
            //        {
            //            sb2.Append(reader.Name + "\r\n");
            //        }
            //    }
            //}

            //ReadElementString
            while(!reader.EOF)
            {
                if(reader.MoveToContent()==XmlNodeType.Element&&reader.Name.ToLower()=="item")
                {
                    sb.Append(reader.ReadElementContentAsString() + "\r\n");
                }
                else
                {
                    reader.Read();
                }
            }
            Console.Write(sb.ToString());
           // Console.ForegroundColor = ConsoleColor.Red;
          //  Console.WriteLine(sb2.ToString());
        }

        static void WriteXml()
        {
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.IndentChars = "\t";
            setting.NewLineOnAttributes = true;
            XmlWriter writer = XmlWriter.Create("newbook.xml", setting);
            writer.WriteStartDocument();
            writer.WriteStartElement("book");
            writer.WriteAttributeString("genre", "Mystery");
            writer.WriteAttributeString("publicationdate", "2001");
            writer.WriteAttributeString("ISBN", "1234567890");
            writer.WriteElementString("title", "书的名字");
            writer.WriteStartElement("author");
            writer.WriteElementString("name", "我的");
            writer.WriteEndElement();
            writer.WriteElementString("price", "21");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        static void XMLDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("newbook.xml");
            XmlNodeList nodeList = doc.GetElementsByTagName("author");
            foreach(XmlNode node in nodeList)
            {
                Console.WriteLine(node.InnerText + "\r\n");
            }
        }

        static void WriteXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("newbook.xml");
            XmlElement newbook = doc.CreateElement("book2");
            newbook.SetAttribute("genre2", "Mystery");
            newbook.SetAttribute("publicationdate2", "2001");
            newbook.SetAttribute("ISBN2", "1234567890");
            XmlElement title = doc.CreateElement("title");
            title.InnerText = "书的名字2";
            newbook.AppendChild(title);
            XmlElement author = doc.CreateElement("author");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = "我的2";
            author.AppendChild(name);
            newbook.AppendChild(author);
            XmlElement price = doc.CreateElement("price");
            price.InnerText = "21";
            newbook.AppendChild(price);
            doc.DocumentElement.AppendChild(newbook);

            XmlTextWriter writer = new XmlTextWriter("booksEdit.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.WriteContentTo(writer);
            writer.Close();

            XmlNodeList nodeList = doc.GetElementsByTagName("author");
            foreach (XmlNode node in nodeList)
            {
                Console.WriteLine(node.InnerText + "\r\n");
            }
        }

        static void XPathRead()
        {
            XPathDocument doc = new XPathDocument("booksEdit.xml");
            //创建XPath navigator
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator iter =  nav.Select("/bookstore/book[@genre='Mystery']");
            while(iter.MoveNext())
            {
                //XPathNodeIterator iter.Current.SelectDescendants(XPathNodeType.Element, false);
            }

        }
    }
}