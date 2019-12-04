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

        public List<User> GetUsers(string selectCondition = "") {
            //Connect();
            //Disconnect();
            List<User> userList = new List<User>();
            connection.Open();

            //using (var reader = (new MySqlCommand("SELECT users.fullname, group.name, user_type.type FROM users i", connection)).ExecuteReader()) {
            using (var reader = (new MySqlCommand("SELECT * from users_view" + (selectCondition != "" ? " " + selectCondition : ""), connection)).ExecuteReader()) {
                while (reader.Read()) {
                    userList.Add(new User() {
                        Id = (int)reader["id"],
                        Name = (string)reader["fullname"],
                        Group = (int)(reader["group_id"] == DBNull.Value ? 0 : reader["group_id"]),
                        GroupText = (string)(reader["group"] == DBNull.Value ? "-" : reader["group"]) + (string)(reader["year"] == DBNull.Value ? "" : reader["year"] + ""),
                        UserType = (int)reader["type_id"],
                        UserTypeText = (string)reader["type"]
                    });
                }

            }

            return userList;
        }
    }
}