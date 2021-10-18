using System;
using System.Xml;
using MySql.Data.MySqlClient;


namespace go
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            DateTime today = DateTime.Today;
            string data = today.ToString("dd.MM.yyyy");
            string url = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + data;
            GetHTML.getHTML(url);
            
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("DailyXmlFile.xml");
            Console.WriteLine("Введите хост: ");
            string host = Console.ReadLine().ToString();
            Console.WriteLine("Введите пользователя: ");
            string usr = Console.ReadLine().ToString();
            Console.WriteLine("Введите базу данных: ");
            string db = Console.ReadLine().ToString();
            Console.WriteLine("Введите пароль: ");
            string psw = Console.ReadLine().ToString();

            string connect = "server=" + host + ";user=" + usr + ";database=" + db + ";password=" + psw + ";";
            MySqlConnection conn = new MySqlConnection(connect);
            conn.Open();

            string sqldata = today.ToString("yyyy-MM-dd");
            for (int i = 0; i < 34; i++)
            {
                string name = xDoc.GetElementsByTagName("CharCode")[i].InnerText;
                double val = Convert.ToDouble(xDoc.GetElementsByTagName("Value")[i].InnerText);
                double nom = Convert.ToDouble(xDoc.GetElementsByTagName("Nominal")[i].InnerText);
                double r = val / nom;

                string sql = "use " + db + ";" + "create table if not exists Exchange_Rates (id int NOT NULL AUTO_INCREMENT,Date date DEFAULT NULL, CharCode varchar(45) DEFAULT NULL,Val double DEFAULT NULL,PRIMARY KEY(id)) ; "+
                    " insert into Exchange_Rates (Date,CharCode,Val) values ('" + sqldata + "','" + name + "'," + r.ToString(System.Globalization.CultureInfo.InvariantCulture) + ");";
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.ExecuteNonQuery();

            }
            conn.Close();
            VarExch.VariableExch();
            Console.ReadKey();
        }

    }
}
