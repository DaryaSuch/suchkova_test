using System;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;

namespace go
{
    public class VarExch
    {
        public static void VariableExch()
        {

            string Nominal = "";
            string Value = "";
            Console.WriteLine("Введите дату в формате dd.MM.yyyy :");
            string date = Console.ReadLine().ToString();
            string url = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + date;
            GetHTML.getHTML(url);
            Console.WriteLine("Введите дату в формате dd.MM.yyyy :");
            string name = Console.ReadLine().ToString();
            string charcode = "";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("ListValute.xml");
            for (int i = 0; i < 34; i++)
            {
                string Name = xDoc.GetElementsByTagName("Name")[i].InnerText;
                if (Name == name)
                {
                    charcode = xDoc.GetElementsByTagName("CharCode")[i].InnerText;
                    Console.WriteLine(name);
                    Console.WriteLine(charcode);
                }
            }
            
            xDoc.Load("DailyXmlFile.xml");
            for (int i = 0; i < 34; i++)
            {
                string CharCode = xDoc.GetElementsByTagName("CharCode")[i].InnerText;
                if (CharCode == charcode)
                {
                    Nominal = xDoc.GetElementsByTagName("Nominal")[i].InnerText;
                    Value = xDoc.GetElementsByTagName("Value")[i].InnerText;
                    Console.WriteLine(Nominal);
                    Console.WriteLine(Value);
                }
            }

            


        }
    }
}