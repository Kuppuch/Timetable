using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using MySql.Data.MySqlClient;

namespace TimeTable.DAO {
    public class DAOUserType : DAO {

        public static List<UserType> GetUserTypies() {
            CheckConnection();
            List<UserType> groupList = new List<UserType>();

            using (var reader = (new MySqlCommand("SELECT * FROM timetable.user_type", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    groupList.Add(new UserType() { Id = (int)(reader["id"] == DBNull.Value ? 0 : reader["id"]), Name = (string)reader["name"] });
                }
            }

            return groupList;
        }

    }
}