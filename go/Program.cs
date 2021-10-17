using System;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using MySql.Data.MySqlClient;






namespace go
{
    class MainClass
    {
        


        static void getHTML(string url)
        {
            //WebRequest request = WebRequest.Create(url);
            //WebResponse response = request.GetResponse();
            //Stream stream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(stream);
            //string result = reader.ReadToEnd();
            //string res = XElement.Parse(result).ToString();
            //StreamWriter writer = new StreamWriter("EmptyXmlFile.xml", false);
            //writer.WriteLine(res);
            using (WebClient web1 = new WebClient())
            {
                string text = web1.DownloadString(url);
                
                

                string result = XElement.Parse(text).ToString();
                using (StreamWriter writer = File.CreateText("EmptyXmlFile.xml"))
                {
                    writer.WriteLine(result);
                }
                
                
            }

        }
        
        public static void Main(string[] args)
        {
            

            string url = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=21.08.2019";
            getHTML(url);

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("EmptyXmlFile.xml");
            string connect = "server=localhost;user=root;database=kurse;password='';";
            MySqlConnection conn = new MySqlConnection(connect);
            conn.Open();
            string a = "12";
            string sql = "if exists (select 1 from dmy where den="+a+") begin insert into (den) values( "+a+" ) end; else create table"+ a+";";

            //for (int i = 0; i < 34; i++)
            //{
            //    string el = xDoc.GetElementsByTagName("NumCode")[i].InnerText;
            //    string ef = xDoc.GetElementsByTagName("Value")[i].InnerText;

            //    string sql = "insert into tist (name,val) values ('" +ef+ "',"+el+");";
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.ExecuteNonQuery();

            //}
            conn.Close();

        }

    }
}
