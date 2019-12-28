using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace TimeTable.DAO {
    public class DAOLesson : DAO {

        public LessionContainer GetLessonContainer() {
            CheckConnection();
            List<Lesson> lessonList = new List<Lesson>();

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
            CheckConnection();

            try {
                (new MySqlCommand("INSERT INTO `timetable`.`lesson` (`group`, `discipline`, `teacher`) VALUES ('" + l.Group + "', '" + l.Discipline + "', '" + l.Teacher + "');", connection))
                .ExecuteNonQuery();

                return true;
            } catch (Exception ex) {
                Logger.Logger.Log.Info("Не удалось добавить занятие. " + ex);
            }

            return false;
        }

        public static List<Lesson> GetLessons() {
            CheckConnection();
            List<Lesson> lessonList = new List<Lesson>();

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

        public static Lesson GetLesson(int Id) {
            CheckConnection();
            Lesson lesson = new Lesson();

            using (var reader = (new MySqlCommand("SELECT * FROM less WHERE id = " + Id, connection)).ExecuteReader()) {
                while (reader.Read())
                    lesson = (new Lesson() {
                        Id = (int)reader["id"],
                        Discipline = (int)reader["discipline_id"],
                        Group = (int)reader["group_id"],
                        Teacher = (int)reader["teacher_id"],
                        DisciplineText = (string)reader["discipline"],
                        GroupText = (string)(reader["group"]) + (int)(reader["year"]),
                        TeacherText = (string)reader["teacher"]
                    });
            }
            return lesson;
        }

        public static bool DeleteLesson(int Id) {
            CheckConnection();

            try {
                (new MySqlCommand("DELETE FROM `lesson` WHERE id = " + Id, connection)).ExecuteNonQuery();
                return true;
            } catch (Exception ex) {
                Logger.Logger.Log.Info("Не удалось удалить занятие. " + ex);
            }
            return false;
        }

        public static bool EditLesson(Lesson l) {
            CheckConnection();

            try {
                (new MySqlCommand("UPDATE `timetable`.`lesson` SET `group` = '" + l.Group + "', `discipline` = '" + l.Discipline + "', `teacher` = '" + l.Teacher + "' WHERE `id` ='" + l.Id + "'", connection))
                .ExecuteNonQuery();

                return true;
            } catch (Exception ex) {
                Logger.Logger.Log.Info("Не удалось редактировать занятие. " + ex);
            }

            return false;
        }
    }
}