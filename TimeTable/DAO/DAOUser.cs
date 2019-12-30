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
            CheckConnection();
            List<User> usersList = new List<User>();

            using (var reader = (new MySqlCommand("SELECT * from users_view" + (selectCondition != "" ? " " + selectCondition : ""), connection)).ExecuteReader()) {
                while (reader.Read()) {
                    usersList.Add(new User() {
                        Id = (int)reader["id"],
                        Name = (string)reader["name"],
                        Group = (int)(reader["group_id"] == DBNull.Value ? 0 : reader["group_id"]),
                        GroupText = (string)(reader["group"] == DBNull.Value ? "-" : reader["group"]) + (string)(reader["year"] == DBNull.Value ? "" : reader["year"] + ""),
                        UserType = (int)reader["type_id"],
                        UserTypeText = (string)reader["type"]
                    });
                }

            }

            return usersList;
        }

        public static List<User> GetTeachers() {
            return new DAOUser().GetUsers("WHERE `type_id` = 2 ORDER BY name");
        }

        //public static User GetByEmail(string email) {
        //    CheckConnection();

        //    using (var reader = (new MySqlCommand("SELECT * FROM `users_view` WHERE `email` = '" + email + "'", connection)).ExecuteReader()) {
        //        if (reader.Read()) {
        //            return new User() {
        //                Id = reader.GetInt32("id"),
        //                UserType = reader.GetInt32("type_id"),
        //                Email = (string)reader["email"]
        //            };
        //        }

        //    }

        //    return null;
        //}

        //public static User GetById(int id) {
        //    CheckConnection();

        //    using (var reader = (new MySqlCommand("SELECT * FROM `users_view` WHERE `id` = '" + id + "'", connection)).ExecuteReader()) {
        //        if (reader.Read()) {
        //            return new User() {
        //                Id = reader.GetInt32("id"),
        //                UserType = reader.GetInt32("type_id"),
        //                Email = (string)reader["email"]
        //            };
        //        }

        //    }

        //    return null;
        //}
    }
}