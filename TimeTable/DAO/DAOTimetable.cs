using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            using (var reader = (new MySqlCommand("SELECT * FROM timetable_view", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    Console.WriteLine(reader["id"]);

                    ttList.Add(new Timetable() {
                        Id = (int)reader["id"],
                        Lesson = (int)reader["id_lesson"],
                        LessonText = (string)reader["discipline"],
                        WeekDay = (string)reader["weekday"],
                        Numerator = (bool)reader["numerator"],
                        LessonNumber = (string)reader["number"],
                        Location = (string)reader["location"],
                        Teacher = (int)reader["id_user"],
                        TeacherText = (string)reader["teacher"],
                        Group = (int)reader["id_group"],
                        GroupText = (string)reader["group_name"],
                        GroupYear = (int)reader["group_year"]
                    });
                }
            }
            return ttList;
        }

        public bool InsertTimetable(Timetable t) {

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            if (CheckTimetable(t))
                return false;
            try {
                PairLocation pl = ParseLocation(t);
                (new MySqlCommand("INSERT INTO `timetable`.`timetable` (`lesson`, `weekday`, `numerator`, `number`, `location`) VALUES ('" + t.Lesson + "', '" + pl.weekDay + "', '" + (t.Numerator ? 1 : 0) + "','" + pl.pairNum + "','" + t.Location + "');", connection))
                .ExecuteNonQuery();

                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);

                return false;
            }

        }

        public bool CheckTimetable(Timetable t) {
            long count = 0;

            PairLocation pl = ParseLocation(t);
            using (var reader = (new MySqlCommand("SELECT COUNT(*) as `count` FROM timetable_view where weekday = '" + pl.weekDay + "' and numerator = " + GetNumerator(t) + " and `number` = " + pl.pairNum + " and location = '" + t.Location + "'", connection)).ExecuteReader()) {
                if (reader.Read()) {
                    count = (long)reader["count"];
                }
            }
            return count > 0; 
        }

        private string[] WeekDays = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

        public PairLocation ParseLocation(Timetable t) {
            string[] location = t.LessonNumber.Split('-');

            return new PairLocation() { weekDay = WeekDays[int.Parse(location[0]) - 1], pairNum = int.Parse(location[1]) };
        }

        public int GetNumerator (Timetable t) {
            return t.Numerator ? 1 : 0;
        }
    }
}