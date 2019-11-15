using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Timetable.DAO {
    public class DAOUser : DAO {
        public bool InsertUser(string name, int year) {

            Connect();

            try {

                Debug.WriteLine("INSERT INTO group (name, year) VALUES (" + name + ", " + year + ")");
                (new MySqlCommand("INSERT INTO [Table] (Movie, Room, Name, Row, Place) VALUES (" + name + ", " + year + ")", Connection))
                .ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);

                return false;
            }

        }


    }
}