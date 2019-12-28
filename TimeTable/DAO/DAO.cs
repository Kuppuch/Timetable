using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace TimeTable.DAO {
    public class DAO {
        public static MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=timetable;");

        public static MySqlConnection Connection { get; set; }

        public static bool CheckConnection() {
            if (connection.State != System.Data.ConnectionState.Open) {
                connection.Open();

                return true;
            }

            return false;
        }
    }
}