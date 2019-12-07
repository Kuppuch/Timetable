using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace TimeTable.DAO {
    public class DAO {
        public MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=timetable;");

        public MySqlConnection Connection { get; set; }

        public void Connect() {
            try {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                Console.WriteLine("ПОДКЛЮЧЕНО");
                //Logger.InitLogger();
                //Logger.Log.Info("Подключение к БД прошло успешно");
            }
            catch (Exception e) {
                //Logger.InitLogger();
                //Logger.Log.Info(e.Message + "Подключение к БД не установлено");
                Console.WriteLine("НЕ ПОДКЛЮЧЕНО" + e);
            }

        }

        public void Disconnect() {
            if (connection.State == System.Data.ConnectionState.Open) {
                connection.Close();
                Console.WriteLine("ОТКЛЮЧЕНО");
            }
                
            Console.WriteLine("НЕ ОТКЛЮЧЕНО");
            //Logger.InitLogger();
            //Logger.Log.Info("Подключение к БД разорвано");
        }
    }
}