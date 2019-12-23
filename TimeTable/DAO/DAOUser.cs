using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using TimeTable.Models;

namespace TimeTable.DAO {
    public class DAOUser : DAO {

        public List<User> GetUsers(string selectCondition = "") {
            //Connect();
            //Disconnect();
            List<User> userList = new List<User>();
            if(connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            //using (var reader = (new MySqlCommand("SELECT users.fullname, group.name, user_type.type FROM users i", connection)).ExecuteReader()) {
            using (var reader = (new MySqlCommand("SELECT * from users_view" + (selectCondition != "" ? " " + selectCondition : ""), connection)).ExecuteReader()) {
                while (reader.Read()) {
                    userList.Add(new User() {
                        Id = (int)reader["id"],
                        Name = (string)reader["name"],
                        Group = (int)(reader["group_id"] == DBNull.Value ? 0 : reader["group_id"]),
                        GroupText = (string)(reader["group"] == DBNull.Value ? "-" : reader["group"]) + (string)(reader["year"] == DBNull.Value ? "" : reader["year"] + ""),
                        UserType = (int)reader["type_id"],
                        UserTypeText = (string)reader["type"]
                    });
                }

            }

            return userList;
        }

        public static List<User> GetTeachers() {
            return new DAOUser().GetUsers("WHERE `type_id` = 2 ORDER BY name");
        }

        public static User GetUser(string Email) {
            User user = new User();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            using (var reader = (new MySqlCommand("SELECT * from `users_view` where email = '" + Email + "'", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    user.Id = (int)reader["id"];
                    user.UserType = (int)reader["type_id"];
                    user.Email = (string)reader["email"];
                }

            }
        return user;
        }
    }
}