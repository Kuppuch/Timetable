using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using TimeTable.Models;

namespace TimeTable.DAO {
    public class DAOSchedule : DAO {

        public static List<Timetable> GetTimetable(int group_id = 0) {
            CheckConnection();
            List<Timetable> ttList = new List<Timetable>();

            using (var reader = (new MySqlCommand("SELECT * FROM timetable_view WHERE" + (group_id > 0 ? " id_group = " + group_id + " AND" : "" ) + " published = 1", connection)).ExecuteReader()) {
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
                        return item.LessonNumber == (l + 1) + "" && item.WeekDay == (w + 1);
                    }) ?? new Timetable());
                }
                ttll.Add(ttl);
                time += 1 * 60 + 30 + 20;
            }

            return ttll;
        }

    }
}