using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace TimeTable.DAO {
    public class DAO {
        public static MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=timetable;");

        public static MySqlConnection Connection { get; set; }

        public static void Connect() {
            try {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                Logger.Logger.Log.Info("Подключение к БД установлено");
            }
            catch (Exception e) {
                Logger.Logger.Log.Info("Подключение к БД не установлено: " + e.Message);
            }

        }

        public static void Disconnect() {
            if (connection.State == System.Data.ConnectionState.Open) {
                connection.Close();

                Logger.Logger.Log.Info("Подключение к БД разорвано");
            } else {

                Logger.Logger.Log.Info("Подключение к БД разорвать не удалось");
            }
        }

        public static bool CheckConnection() {
            if (connection.State != System.Data.ConnectionState.Open) {
                connection.Open();

                return true;
            }

            return false;
        }
    }
}