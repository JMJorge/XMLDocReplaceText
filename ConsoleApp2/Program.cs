using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Win32;
using System.Data;
using System.Xml.Serialization;
using System.Xml;


using System.IO;
using System.Xml.Linq;

namespace EditXML
{
    class Program
    {

        static void ChangeData()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Temp\teste.xml");

            //Converte GLPostingDate para um array de forma a guardar só a data
            XmlNodeList DataList = doc.GetElementsByTagName("GLPostingDate");
            for (int i = 0; i < DataList.Count; i++)
            {
                Console.WriteLine(DataList[i].InnerXml);
                string str = DataList[i].InnerXml;
                char[] arr;

                arr = str.ToCharArray(0, 10);
                Console.WriteLine(arr);
                string s = string.Join("", arr);
                Console.WriteLine("String: {0}", s);
                DataList[i].InnerXml = s;
                Console.WriteLine("Change: {0}", DataList[i].InnerXml);
                Console.WriteLine();
            }
            doc.Save(@"C:\Temp\teste2.xml");
        }
        static void ReadWrite()
        {
            XmlReader doc = XmlReader.Create(@"C:\Temp\teste.xml");

            //lê o documento e faz um replace apenas na versão
            while (doc.Read())
            {
                if (doc.NodeType == XmlNodeType.Element && doc.Name == "AuditFile")
                {
                    Console.WriteLine("Velho: " + doc.GetAttribute("xmlns"));
                    var replace = doc.GetAttribute("xmlns").Replace(doc.GetAttribute("xmlns"), "urn:OECD:StandardAuditFile-Tax:PT_1.04_01");
                    Console.WriteLine("Novo: " + replace);

                }
            }
            Console.ReadLine();


        }
        static void ChangeVersion()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Temp\teste.xml");

            //Faz um replace ao atributo <AuditFile>
            var a = doc.CreateAttribute("xmlns");
            a.Value = "urn:OECD:StandardAuditFile-Tax:PT_1.04_01";
            doc.DocumentElement.Attributes.Append(a);

            //Faz um replace ao atributo <Header>
            var b = doc.CreateAttribute("xmlns");
            b.Value = "urn:OECD:StandardAuditFile-Tax:PT_1.04_01";
            doc.DocumentElement.FirstChild.Attributes.Append(b);

            doc.Save(@"C:\Temp\teste2.xml");
        }

        static void ReadWriteSave()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Temp\RED-SAFT3-CNT.xml");

            //Faz um replace ao atributo <AuditFile>
            var a = doc.CreateAttribute("xmlns");
            a.Value = "urn:OECD:StandardAuditFile-Tax:PT_1.04_01";
            doc.DocumentElement.Attributes.Append(a);

            //Faz um replace ao atributo <Header>
            var b = doc.CreateAttribute("xmlns");
            b.Value = "urn:OECD:StandardAuditFile-Tax:PT_1.04_01";
            doc.DocumentElement.FirstChild.Attributes.Append(b);


            //Mudar a data
            XmlNodeList DataList = doc.GetElementsByTagName("GLPostingDate");
            for (int i = 0; i < DataList.Count; i++)
            {
                string str = DataList[i].InnerXml;
                char[] arr;

                arr = str.ToCharArray(0, 10);

                string s = string.Join("", arr);

                DataList[i].InnerXml = s;
            }
            doc.Save(@"C:\Temp\Convertido.xml");

        }
        static void teste()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Temp\teste.xml");

            Console.WriteLine($"Primeiro: \n{doc.InnerXml}");
            var root = doc.SelectSingleNode("Header");
            Console.WriteLine($"Segundo: \n{root.OuterXml}");


            //doc.Save(@"C:\Temp\teste2.xml");
        }
        static void Main(string[] args)
        {
            //ReadWrite();
            //ChangeVersion
            //ChangeData();
            ReadWriteSave();
            //teste();

        }
    }
}

