using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using TimeTable.Models.Context;

namespace TimeTable.Models {
    public class User {
        public int Id { get; set; }
        [DisplayName("Имя")]
        public string Name { get; set; }
        public int Group { get; set; }
        public int UserType { get; set; }
        [DisplayName("Группа")]
        public string GroupText { get; set; }
        [DisplayName("Статус")]
        public string UserTypeText { get; set; }
        public string Email { get; set; }


        public bool HasRole(string[] roles) {
            using (UserContext db = new UserContext()) {
                UserData user = db.Users.FirstOrDefault(u => u.id == Id);
                if (user != null) {
                    UserTypeData usertype = db.UserTypes.Find(user.type);
                    if (usertype != null)
                        foreach (var role in roles)
                            if (usertype.name.ToLower() == role.ToLower())
                                return true;
                }
            }
            return false;
        }

        public bool HasRole(int[] roles) {
            using (UserContext db = new UserContext()) {
                UserData user = db.Users.FirstOrDefault(u => u.id == Id);
                if (user != null) {
                    UserTypeData usertype = db.UserTypes.Find(user.type);
                    if (usertype != null)
                        foreach (var role in roles)
                            if (usertype.id == role)
                                return true;
                }
            }
            return false;
        }

        public bool HasRole(string role) {
            return HasRole(new string[] { role });
        }
        public bool HasRole(int role) {
            return HasRole(new int[] { role });
        }

    }
}