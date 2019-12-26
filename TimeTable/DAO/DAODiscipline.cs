using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace TimeTable.DAO {
    public class DAODiscipline : DAO {

        public static List<Discipline> GetDisciplines() {
            List<Discipline> disciplineList = new List<Discipline>();
            CheckConnection();

            using (var reader = (new MySqlCommand("SELECT * FROM `discipline_view` ORDER BY `name`", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    disciplineList.Add(new Discipline() {
                        Id = (int)(reader["id"] == DBNull.Value ? 0 : reader["id"]),
                        Name = (string)reader["name"],
                        UserText = (string)reader["user"] });
                }
            }
            return disciplineList;
        }

        public bool InsertDiscipline(Discipline d) {
            CheckConnection();

            try {
                (new MySqlCommand("INSERT INTO `timetable`.`discipline` (`name`, `user`) VALUES ('" + d.Name + "', '" + d.User + "');", connection))
                .ExecuteNonQuery();

                return true;
            }
            catch (Exception ex) {
                Logger.Logger.Log.Info("Вставка дисциплины прошла неудачно. " + ex.Message);
                return false;
            }

        }

        public static Discipline GetDiscipline(int Id) {
            CheckConnection();
            Discipline discipline = new Discipline();

            using (var reader = (new MySqlCommand("SELECT * FROM discipline_view WHERE id = " + Id, connection)).ExecuteReader()) {
                while (reader.Read())
                    discipline = (new Discipline() {
                        Id = (int)reader["id"],
                        Name = (string)reader["name"],
                        User = (int)reader["user_id"],
                        UserText = (string)reader["user"]
                    });
            }
            return discipline;
        }

        public static bool DeleteDiscipline(int Id) {
            CheckConnection();

            try {
                (new MySqlCommand("DELETE FROM `timetable`.`discipline` WHERE `id` = " + Id + ";", connection)).ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                Logger.Logger.Log.Info("Удаление дисциплины прошло неудачно. " + ex.Message);
                return false;
            }
        }

        public static bool EditDiscipline(Discipline d) {
            CheckConnection();

            try {
                (new MySqlCommand("UPDATE `timetable`.`discipline` SET `name` = '" + d.Name + "', `user` = '" + d.User + "'  WHERE `id` ='" + d.Id + "'", connection))
                .ExecuteNonQuery();

                return true;
            }
            catch (Exception ex) {
                Logger.Logger.Log.Info("Редактирование дисциплины прошло неудачно. " + ex.Message);
                return false;
            }
        }
    }
}