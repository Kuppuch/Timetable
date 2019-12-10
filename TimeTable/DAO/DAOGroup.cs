using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace TimeTable.DAO {
    public class DAOGroup : DAO {

        public static List<string> GetYears() {
            List<string> year00 = new List<string>();
            for (int i = 0; i < 5; i++) {
                year00.Add(DateTime.Now.ToString("yy"));
            }
            return (year00);
        }

        public bool InsertGroup(Group g) {

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            try {
                (new MySqlCommand("INSERT INTO `timetable`.`group` (`name`, `year`) VALUES ('" + g.Name + "','" + DateTime.Now.ToString("yy") + "');", connection))
                .ExecuteNonQuery();

                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);

                return false;
            }

        }

        public static List<Group> GetGroups() {
            List<Group> groupList = new List<Group>();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            using (var reader = (new MySqlCommand("SELECT * FROM timetable.group", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    groupList.Add(new Group() { Id = (int)(reader["id"] == DBNull.Value ? 0 : reader["id"]), Name = (string)reader["name"], Year = (int)reader["year"] });
                }
            }
            return groupList;
        }

        public static Group GetGroup(int Id) {
            Group group = new Group();

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            using (var reader = (new MySqlCommand("SELECT * FROM `group` WHERE id = " + Id, connection)).ExecuteReader()) {
                while (reader.Read())
                    group = (new Group() {
                        Id = (int)reader["Id"],
                        Name = (string)reader["name"],
                        Year = (int)reader["year"]
                    });
            }
            return group;
        }

        public static bool DeleteGroup(int Id) {
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            try {
                (new MySqlCommand("DELETE FROM `group` WHERE id = " + Id, connection)).ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                return false;
            }
        }

    }
}