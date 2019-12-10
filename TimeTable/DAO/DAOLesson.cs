using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace TimeTable.DAO {
    public class DAOLesson : DAO {

        public LessionContainer GetLessonContainer(string table = "Discipline") {
            List<Lesson> lessonList = new List<Lesson>();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            //using (var reader = (new MySqlCommand("SELECT users.fullname, group.name, user_type.type FROM users i", connection)).ExecuteReader()) {
            using (var reader = (new MySqlCommand("SELECT * FROM less;", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    lessonList.Add(new Lesson() {
                        Id = (int)reader["id"],
                        Discipline = (int)reader["discipline_id"],
                        Group = (int)reader["group_id"],
                        Teacher = (int)reader["teacher_id"],
                        DisciplineText = (string)reader["discipline"],
                        GroupText = (string)(reader["group"]) + (int)(reader["year"]),
                        TeacherText = (string)reader["teacher"]
                    });
                }

            }

            return new LessionContainer() { lessons = lessonList, disciplines = DAODiscipline.GetDisciplines(), groups = DAOGroup.GetGroups(), users = DAOUser.GetTeachers() };
        }

        public bool InsertLesson(Lesson l) {

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            try {
                (new MySqlCommand("INSERT INTO `timetable`.`lesson` (`group`, `discipline`, `teacher`) VALUES ('" + l.Group + "', '" + l.Discipline + "', '" + l.Teacher + "');", connection))
                .ExecuteNonQuery();

                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);

                return false;
            }

        }

        public static List<Lesson> GetLessons() {
            List<Lesson> lessonList = new List<Lesson>();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            //using (var reader = (new MySqlCommand("SELECT users.fullname, group.name, user_type.type FROM users i", connection)).ExecuteReader()) {
            using (var reader = (new MySqlCommand("SELECT * FROM less ORDER BY `discipline`;", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    lessonList.Add(new Lesson() {
                        Id = (int)reader["id"],
                        Discipline = (int)reader["discipline_id"],
                        Group = (int)reader["group_id"],
                        Teacher = (int)reader["teacher_id"],
                        DisciplineText = (string)reader["discipline"],
                        GroupText = (string)(reader["group"]) + (int)(reader["year"]),
                        TeacherText = (string)reader["teacher"]
                    });
                }
            }
            return lessonList;
        }
    }
}