using System.IO;
using System.Net;
using System.Xml.Linq;


namespace go
{
    public class GetHTML
    {
        public static void getHTML(string url)
        {
            using (WebClient web1 = new WebClient())
            {
                string text = web1.DownloadString(url);
                string result = XElement.Parse(text).ToString();
                using (StreamWriter writer = File.CreateText("DailyXmlFile.xml"))
                {
                    writer.WriteLine(result);
                }
            }
        }
    }
}
