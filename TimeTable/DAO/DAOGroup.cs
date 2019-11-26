using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using MySql.Data.MySqlClient;

namespace TimeTable.DAO {
    public class DAOGroup : DAO {

        public List<Group> GetGroups(string table = "Group") {
            //Connect();
            //Disconnect();
            List<Group> groupList = new List<Group>();
            connection.Open();

            using (var reader = (new MySqlCommand("SELECT * FROM timetable.group", connection)).ExecuteReader()) {
                while (reader.Read()) {
                    Console.WriteLine(reader["id"]);

                    groupList.Add(new Group() { Id = (int)reader["id"], Name = (string)reader["name"], Year = (int)reader["year"] });
                }

            }

            return groupList;
        }

    }
}