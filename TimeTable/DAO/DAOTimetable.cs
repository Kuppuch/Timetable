using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using TimeTable.Models;

namespace TimeTable.DAO {
    public class DAOTimetable : DAO {

        public static List<Timetable> GetTimetable(int group_id = 0) {
            CheckConnection();
            List<Timetable> ttList = new List<Timetable>();

            using (var reader = (new MySqlCommand("SELECT * FROM timetable_view" + (group_id > 0 ? " WHERE id_group = " + group_id : ""), connection)).ExecuteReader()) {
                while (reader.Read()) {
                    ttList.Add(new Timetable() {
                        Id = (int)reader["id"],
                        Lesson = (int)reader["id_lesson"],
                        LessonText = (string)reader["discipline"],
                        WeekDay = (int)reader["weekday"],
                        Numerator = reader.GetBoolean("numerator"),
                        LessonNumber = reader.GetInt32("number") + "",
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

        public static bool GetSQLNumerator(object i) {
            return (byte)i == 1;
        }

        public bool InsertTimetable(Timetable t) {
            CheckConnection();

            if (CheckTimetable(t))
                return false;

            try {
                PairLocation pl = ParseLocation(t);
                (new MySqlCommand("INSERT INTO `timetable`.`timetable` (`lesson`, `weekday`, `numerator`, `number`, `location`) VALUES ('" + t.Lesson + "', '" + pl.weekDay + "', '" + (t.Numerator ? 1 : 0) + "','" + pl.pairNum + "','" + t.Location + "');", connection))
                .ExecuteNonQuery();

                return true;
            } catch (Exception ex) {
                // ЛОГГЕР
            }

            return false;
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

        private static string[] WeekDays = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

        public static PairLocation ParseLocation(Timetable t) {
            string[] location = t.LessonNumber.Split('-');

            return new PairLocation() { weekDay = int.Parse(location[0]), pairNum = int.Parse(location[1]) };
        }

        public int GetNumerator (Timetable t) {
            return t.Numerator ? 1 : 0;
        }

        public static List<TimeTableLine> GetPairs(int group_id = 0) {
            var pairs = GetTimetable(group_id);

            List<TimeTableLine> ttll = new List<TimeTableLine>();

            TimeTableLine ttl;

            int time = 8 * 60 + 30;

            for (int l = 0; l < 6; l++) {
                ttl = new TimeTableLine();
                ttl.time = time;
                for (int w = 0; w < 5; w++) {
                    ttl.pairs.Add(pairs.Find((item) => {
                        return item.LessonNumber == (l+1) + "" && item.WeekDay == (w+1);
                    }) ?? new Timetable());
                }
                ttll.Add(ttl);
                time += 1 * 60 + 30 + 20;
            }

            return ttll;
        }
    }
}