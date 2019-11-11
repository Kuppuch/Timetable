using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.DAO {
    public class DAO {

        public MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=timetable;");


        public void Connect() {
            if (connection.State == System.Data.ConnectionState.Closed) 
                connection.Open();
        }

        public void Disconnect() {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }


    }
}