using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace AttendanceAPISync {
    class Program {

        static INISettings settings;
        public static MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=timetable;");

        static void Main(string[] args) {

            settings = new INISettings(BaseDir() + "settings.ini");
            if (args.Length == 0 || args.Length == 1 && args[0] == "-updateDB") {
                UpdateDatabase();
            } else if (args.Length == 1 && args[0] == "-sendTT") {
                SendTimetable();
            }
            Console.ReadKey();

            /*
             * /api/session/create
                master      - преп-ль (id'шник)
                userType    - тип конечных польз (id'шник)
                groups      - список групп конечных польз (через запятую, id'шники)
                activeTime  - время активности в секундах (стандартное - 20)
                activeAt    - дата начала активности (YYYY.MM.DD HH:MM:SS)
             */

        }

        static string BaseDir() {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        static void UpdateDatabase() {
            using(WebClient wc = new WebClient()) {
                var url = settings.IniReadValue("url", "usersAside");
                var studentUserType = settings.IniReadValue("common", "studentUserType");
                var json = wc.DownloadString(url);

                dynamic asideObject = JObject.Parse(json);

                dynamic users = asideObject["users"];
                dynamic types = asideObject["types"];
                dynamic groups = asideObject["groups"];

                User u;
                List<User> lUsers = new List<User>();
                foreach (dynamic user in users) {
                    u = new User() {
                        Id = Convert.ToInt32(user["id"]),
                        Name = user["name_short"],
                        Group = Convert.ToInt32(user["group_id"] == null ? 0 : user["group_id"]),
                        Email = user["email"],
                        UserType = user["user_type_id"]
                    };
                    lUsers.Add(u);
                    //Console.WriteLine(user["name"]);
                }

                UserType ut;
                List<UserType> lUsersType = new List<UserType>();
                foreach (dynamic type in types) {
                    ut = new UserType() {
                        Id = Convert.ToInt32(type["id"]),
                        Name = type["name"]
                    };
                    lUsersType.Add(ut);
                    //Console.WriteLine(type["name"]);
                }

                Group g;
                List<Group> lGroup = new List<Group>();
                foreach (dynamic group in groups) {
                    g = new Group() {
                        Id = Convert.ToInt32(group["id"]),
                        Name = group["name"],
                        Year = Convert.ToInt32(group["year"])
                    };
                    lGroup.Add(g);
                }

                // Для пользователей, групп и типов пользователей отдельно:
                // Получаешь список id из БД
                // находишь разницу между списком id из БД и списком из API (по id'шникам)
                /*
                
                API:   DB:

                ..           -- для тех, которые есть в API, но нет в БД. -> Добавление в БД
                ..      
                ..     ..    -- для тех, которые есть и там и там -> Обновление в БД по id'шникам
                ..     ..
                ..     ..
                ..     ..
                       ..    -- для тех, которые есть в БД, но нет в API. -> Удаление из БД
                       ..

                 */


                List<int> usersFromDB = new List<int>();
                CheckConnection();

                using (var reader = (new MySqlCommand("SELECT `id` from `users`", connection)).ExecuteReader()) {
                    while (reader.Read()) {
                        usersFromDB.Add(reader.GetInt32("id"));
                    }
                }

                List<User> notInDBU = lUsers.FindAll(item => !usersFromDB.Contains(item.Id)); // польз-и
                List<User> intersectsU = lUsers.FindAll(item => usersFromDB.Contains(item.Id)); // польз-и
                List<int> notInAPIU = usersFromDB.FindAll(item => lUsers.Find(u => (u.Id == item)) == null); // id'шники

                //Console.WriteLine("notInDBU count: " + notInDBU.Count);
                //Console.WriteLine("intersectsU count: " + intersectsU.Count);
                //Console.WriteLine("notInAPIU count: " + notInAPIU.Count);

                List<int> groupFromDB = new List<int>();

                using (var reader = (new MySqlCommand("SELECT `id` FROM `group`", connection)).ExecuteReader()) {
                    while (reader.Read()) {
                        groupFromDB.Add(reader.GetInt32("id"));
                    }
                }

                List<Group> notInDBG = lGroup.FindAll(item => !groupFromDB.Contains(item.Id)); // группы
                List<Group> intersectsG = lGroup.FindAll(item => groupFromDB.Contains(item.Id)); // группы
                List<int> notInAPIG = groupFromDB.FindAll(item => lGroup.Find(u => (u.Id == item)) == null); // id'шники

                //Console.WriteLine("notInDBG count: " + notInDBG.Count);
                //Console.WriteLine("intersectsG count: " + intersectsG.Count);
                //Console.WriteLine("notInAPIG count: " + notInAPIG.Count);

                List<int> usersTypesFromDB = new List<int>();

                using (var reader = (new MySqlCommand("SELECT `id` from `user_type`", connection)).ExecuteReader()) {
                    while (reader.Read()) {
                        usersTypesFromDB.Add(reader.GetInt32("id"));
                    }
                }

                List<UserType> notInDBT = lUsersType.FindAll(item => !usersTypesFromDB.Contains(item.Id)); // типы
                List<UserType> intersectsT = lUsersType.FindAll(item => usersTypesFromDB.Contains(item.Id)); // типы
                List<int> notInAPIT = usersTypesFromDB.FindAll(item => lUsersType.Find(u => (u.Id == item)) == null); // id'шники

                //Console.WriteLine("notInDBG count: " + notInDBT.Count);
                //Console.WriteLine("intersectsG count: " + intersectsT.Count);
                //Console.WriteLine("notInAPIG count: " + notInAPIT.Count);

                try {
                    foreach (var group in notInDBG) {
                        (new MySqlCommand("INSERT INTO `group` (`id`, `name`, `year`) VALUES " +
                            "('" + group.Id + "', '" + group.Name + "', '" + group.Year + "')", connection))
                        .ExecuteNonQuery();
                    }
                    foreach (var group in intersectsG) {
                        (new MySqlCommand("UPDATE `group` SET " +
                          "`name` = '" + group.Name + "', `year` = '" + group.Year + "' " +
                           "WHERE `id` ='" + group.Id + "'", connection))
                       .ExecuteNonQuery();
                    }
                    if (notInAPIG.Count > 0) {
                        (new MySqlCommand("DELETE FROM `group` WHERE `id` IN '" + string.Join(",", notInAPIG) + "'", connection))
                        .ExecuteNonQuery();
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }

                try {
                    foreach (var usertype in notInDBT) {
                        (new MySqlCommand("INSERT INTO `user_type` (`id`, `name`) VALUES " +
                            "('" + usertype.Id + "', '" + usertype.Name + "')", connection))
                        .ExecuteNonQuery();
                    }
                    foreach (var usertype in intersectsT) {
                        (new MySqlCommand("UPDATE `user_type` SET " +
                          "`name` = '" + usertype.Name + "' " +
                           "WHERE `id` ='" + usertype.Id + "'", connection))
                       .ExecuteNonQuery();
                    }
                    if (notInAPIT.Count > 0) {
                        (new MySqlCommand("DELETE FROM `user_type` WHERE `id` IN '" + string.Join(",", notInAPIT) + "'", connection))
                        .ExecuteNonQuery();
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }

                try {
                    foreach (var user in notInDBU) {
                        (new MySqlCommand("INSERT INTO `users` (`id`, `name`, `group`, `type`, `email`) VALUES " +
                            "('" + user.Id + "', '" + user.Name + "', " + (user.Group == 0 ? "null" : "'" + user.Group + "'") + ", '" + user.UserType + "', '" + user.Email + "')", connection))
                        .ExecuteNonQuery();
                    }
                    foreach (var user in intersectsU) {
                        (new MySqlCommand("UPDATE `users` SET " +
                           "`name` = '" + user.Name + "', `group` = " + (user.Group == 0 ? "null" : "'" + user.Group + "'") + ", `type` = '" + user.UserType + "', `email` = '" + user.Email + "' " +
                           "WHERE `id` ='" + user.Id + "'", connection))
                       .ExecuteNonQuery();
                    }
                    Console.WriteLine(string.Join(",", notInAPIU));
                    if (notInAPIU.Count > 0) {
                        (new MySqlCommand("DELETE FROM `users` WHERE `id` IN '" + string.Join(",", notInAPIU) + "'", connection))
                        .ExecuteNonQuery();
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }

                Console.WriteLine("База данных обновлена");
            }
        }


        public static void SendTimetable() {

            var now = DateTime.Now;
            var hours = now.Hour;
            var minutes = now.Minute;
            // hours = 10;
            // minutes = 30;
            minutes = hours * 60 + minutes - (8 * 60 + 30);
            var pair = minutes / (1 * 60 + 50);
            if (minutes % (1 * 60 + 50) >= (1 * 60 + 30)) {
                return;
            }

            var weekDay = (int)now.DayOfWeek;

            CultureInfo myCI = new CultureInfo("ru-RU");
            Calendar myCal = myCI.Calendar;

            var week = myCal.GetWeekOfYear(now, myCI.DateTimeFormat.CalendarWeekRule, myCI.DateTimeFormat.FirstDayOfWeek);

            var weekType = week % 2 == 0;

            var tts = GetTimetable(weekType, weekDay, pair);
            
            // Создание списка пар на основе одинаковых преподавателей

            PairContainer pc;
            IDictionary<int, PairContainer> pcs = new Dictionary<int, PairContainer>();

            foreach (var tt in tts) {
                if (pcs.ContainsKey(tt.Teacher)) {
                    pcs[tt.Teacher].groups.Add(tt.Group);
                } else {
                    pc = new PairContainer() {
                        groups = new List<int>() { tt.Group },
                        teacher = tt.Teacher
                    };
                    pcs.Add(tt.Teacher, pc);
                }
            }

            using (WebClient wc = new WebClient()) {
                var url = settings.IniReadValue("url", "sessionCreate");
                var studentUserType = settings.IniReadValue("common", "studentUserType");
                var activeTime = settings.IniReadValue("session", "activeTime");

                try {
                    foreach (var one in pcs) {
                        var json = wc.DownloadString(url + "&userType=" + studentUserType + "&activeTime=" + activeTime + "&groups=" + string.Join(',', one.Value.groups) + "&master=" + one.Value.teacher);
                        dynamic responseObject = JObject.Parse(json);
                        if (responseObject["success"] == "true") {
                            Console.WriteLine("Сеанс был успешно создан для: групп: " + string.Join(',', one.Value.groups) + ", преподавателя: " + one.Value.teacher + ", сеанс начинается: " + responseObject["session"]["activeDateTime"]);
                        } else {
                            Console.WriteLine("Сеанс не был успешно создан! Причина: " + responseObject["msg"]);
                        }
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Сеанс не был успешно создан! Причина: " + ex.Message);
                }
                
            }

        }

        public static List<Timetable> GetTimetable(bool weekType, int weekDay, int pair) {
            CheckConnection();
            List<Timetable> ttList = new List<Timetable>();

            using (var reader = (new MySqlCommand("SELECT * FROM timetable_view WHERE numerator = " + (weekType ? 1 : 0) +
                " AND weekday = " + (weekDay) + " AND number = " + (pair + 1), connection)).ExecuteReader()) {
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

        public static bool CheckConnection() {
            if (connection.State != System.Data.ConnectionState.Open) {
                connection.Open();

                return true;
            }

            return false;
        }

    }

    public class PairContainer {

        public int teacher;
        public List<int> groups;
        public Timetable pair;

    }
   
}
