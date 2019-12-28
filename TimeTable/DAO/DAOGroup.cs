using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace TimeTable.DAO {
    public class DAOGroup : DAO {

        public static List<Group> GetGroups() {
            CheckConnection();
            List<Group> groupList = new List<Group>();

            using (var reader = (new MySqlCommand("SELECT * FROM timetable.group", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    groupList.Add(new Group() { Id = (int)(reader["id"] == DBNull.Value ? 0 : reader["id"]), Name = (string)reader["name"], Year = (int)reader["year"] });
                }
            }
            return groupList;
        }

    }
}