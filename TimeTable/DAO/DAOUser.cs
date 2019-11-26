using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using TimeTable.Models;

namespace TimeTable.DAO {
    public class DAOUser : DAO {
        public bool InsertUser(string name, int year) { 

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

        public List<User> GetUsers(string table = "Users") {
            //Connect();
            //Disconnect();
            List<User> userList = new List<User>();
            connection.Open();
           
            using (var reader = (new MySqlCommand("SELECT * FROM users", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    Console.WriteLine(reader["id"]);

                    userList.Add(new User() { Id = (int)reader["id"], Name = (string)reader["fullname"], Group = (int)(reader["group"] == DBNull.Value ? 0 : reader["group"]), UserType = (int)reader["user_type"] });
                }

            }

            return userList;
        }
    }
}