using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using MySql.Data.MySqlClient;

namespace TimeTable.DAO {
    public class DAODiscipline : DAO {
        public List<Discipline> GetDiscipline(string table = "Discipline") {
            List<Discipline> disciplineList = new List<Discipline>();
            connection.Open();

            //using (var reader = (new MySqlCommand("SELECT users.fullname, group.name, user_type.type FROM users i", connection)).ExecuteReader()) {
            using (var reader = (new MySqlCommand("SELECT discipline.id, name, users.fullname as user FROM timetable.discipline JOIN timetable.users where discipline.user = users.id;", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    Console.WriteLine(reader["id"]);

                    disciplineList.Add(new Discipline() { Id = (int)reader["id"], Name = (string)reader["name"], User = (string)(reader["user"]) });
                }
            }
            return disciplineList;
        }

    }
}