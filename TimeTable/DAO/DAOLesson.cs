using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using MySql.Data.MySqlClient;

namespace TimeTable.DAO {
    public class DAOLesson : DAO {
        public List<Lesson> GetLesson(string table = "Discipline") {
            List<Lesson> lessonList = new List<Lesson>();
            connection.Open();

            //using (var reader = (new MySqlCommand("SELECT users.fullname, group.name, user_type.type FROM users i", connection)).ExecuteReader()) {
            using (var reader = (new MySqlCommand("SELECT * FROM less;", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    Console.WriteLine(reader["id"]);

                    lessonList.Add(new Lesson() { Id = (int)reader["id"], Discipline = (string)reader["discipline"], Group = (string)(reader["group"]), Year = (int)(reader["year"]), Teacher = (string)(reader["teacher"]) });
                }

            }

            return lessonList;
        }
    }
}