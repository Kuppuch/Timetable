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
            List<string> years = new List<string>();
            DateTime now = DateTime.Now;

            for (int i = 0; i < 5; i++) {
                years.Add(now.ToString("yy"));
                now = now.AddYears(1);
            }

            return years;
        }

        public bool InsertGroup(Group g) {
            CheckConnection();

            try {
                (new MySqlCommand("INSERT INTO `timetable`.`group` (`name`, `year`) VALUES ('" + g.Name + "','" + DateTime.Now.ToString("yy") + "');", connection))
                .ExecuteNonQuery();

                return true;
            }
            catch (Exception ex) {
                Logger.Logger.Log.Info("Добавить группу не удалось: " + ex.Message);
                return false;
            }

        }

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

        public static Group GetGroup(int Id) {
            CheckConnection();
            Group group = new Group();

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
            CheckConnection();

            try {
                (new MySqlCommand("DELETE FROM `group` WHERE id = " + Id, connection)).ExecuteNonQuery();
                return true;
            } catch (Exception ex) {
                Logger.Logger.Log.Info("Удалить группу не удалось: " + ex.Message);
            }

            return false;
        }

    }
}