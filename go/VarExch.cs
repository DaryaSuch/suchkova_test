using System;
using System.Xml;


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
            Console.WriteLine("Введите трёхбуквенный код валюты :");
            string charcode = Console.ReadLine().ToString().ToUpper();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("DailyXmlFile.xml");
            for (int i = 0; i < 34; i++)
            {
                string CharCode = xDoc.GetElementsByTagName("CharCode")[i].InnerText;
                if (CharCode == charcode)
                {
                    Nominal = xDoc.GetElementsByTagName("Nominal")[i].InnerText;
                    Value = xDoc.GetElementsByTagName("Value")[i].InnerText;
                    Console.WriteLine("Номинал - " + Nominal);
                    Console.WriteLine("Курс - " + Value);
                    if (Convert.ToInt16(Nominal) > 1) 
                    {
                        Console.WriteLine("Курс / Номинал - " + (Convert.ToDouble(Value) / Convert.ToInt16(Nominal)).ToString());
                    }
                }
            }
        }
    }
}