using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using MySql.Data.MySqlClient;

namespace AttendanceAPISync {
    class Program {

        static INISettings settings;
        public static MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=timetable;");

        static void Main(string[] args) {

            settings = new INISettings(BaseDir() + "settings.ini");
            UpdateDatabase();
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
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

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



        }

    }
}
