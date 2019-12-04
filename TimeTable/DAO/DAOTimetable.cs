using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using TimeTable.Models;

namespace TimeTable.DAO {
    public class DAOTimetable : DAO {

        public List<Timetable> GetTimetable(string table = "Group") {
            //Connect();
            //Disconnect();
            List<Timetable> ttList = new List<Timetable>();
            connection.Open();

            using (var reader = (new MySqlCommand("SELECT * FROM timetable.group", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    Console.WriteLine(reader["id"]);

                    ttList.Add(new Timetable() { Id = (int)reader["id"], Lesson = (string)reader["lesson"], Teacher = (string)reader["teacher"], WeekDay = (string)reader["teacher"], Numerator = (int)reader["numerator"], LessonNumber = (int)reader["lessonnumber"], Location = (string)reader["location"] });
                }

            }

            return ttList;
        }

    }
}