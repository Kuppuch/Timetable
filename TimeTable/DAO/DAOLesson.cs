using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using MySql.Data.MySqlClient;

namespace TimeTable.DAO {
    public class DAOLesson : DAO {
        public LessionContainer GetLesson(string table = "Discipline") {
            List<Lesson> lessonList = new List<Lesson>();
            connection.Open();

            //using (var reader = (new MySqlCommand("SELECT users.fullname, group.name, user_type.type FROM users i", connection)).ExecuteReader()) {
            using (var reader = (new MySqlCommand("SELECT * FROM less;", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    lessonList.Add(new Lesson() { Id = (int)reader["id"], Discipline = (string)reader["discipline"], Group = (string)(reader["group"]), Year = (int)(reader["year"]), Teacher = (string)(reader["teacher"]) });
                }

            }

            return new LessionContainer() { lessons = lessonList, disciplines = GetDisciplines(), groups = GetGroups(), users = GetUsers() };
        }

        public List<Discipline> GetDisciplines() {
            List<Discipline> disciplineList = new List<Discipline>();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            using (var reader = (new MySqlCommand("SELECT id, name FROM discipline ORDER BY name", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    disciplineList.Add(new Discipline() { Id = (int)(reader["id"] == DBNull.Value ? 0 : reader["id"]), Name = (string)reader["name"] });
                }
            }
            return disciplineList;
        }

        public List<Group> GetGroups() {
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

        public List<User> GetUsers() {
            List<User> userList = new List<User>();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            using (var reader = (new MySqlCommand("SELECT * FROM timetable.users where `user_type` = 2 ORDER BY fullname;", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    userList.Add(new User() { Id = (int)reader["id"], Name = (string)reader["fullname"], Group = (string)(reader["group"] == DBNull.Value ? "0" : reader["group"]) });
                }
            }
            return userList;
        }

    }
}