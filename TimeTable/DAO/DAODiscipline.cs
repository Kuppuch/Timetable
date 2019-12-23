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
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

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

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            try {
                (new MySqlCommand("INSERT INTO `timetable`.`discipline` (`name`, `user`) VALUES ('" + d.Name + "', '" + d.User + "');", connection))
                .ExecuteNonQuery();

                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);

                return false;
            }

        }

        public static Discipline GetDiscipline(int Id) {
            Discipline discipline = new Discipline();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

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
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            try {
                (new MySqlCommand("DELETE FROM `discipline` WHERE id = " + Id, connection)).ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public static bool EditDiscipline(Discipline d) {

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            try {
                (new MySqlCommand("UPDATE `timetable`.`discipline` SET (`name` = '" + d.Name + "', `user` = '" + d.User + "')  WHERE `id` ='" + d.Id + "'", connection))
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