using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TimeTable.Models;

namespace TimeTable.Providers {
    public class CustomRoleProvider : RoleProvider {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName) {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles() {
            throw new NotImplementedException();
        }


        public override string[] GetRolesForUser(string username) {
            string[] roles = new string[] { };

            using (UserContext db = new UserContext()) {
                User user = db.Users.FirstOrDefault(u => u.Email == username);
                if (user != null) {
                    UserType usertype = db.UserTypes.Find(user.UserType);
                    if (usertype != null)
                        roles = new string[] { usertype.Name };
                }
            }
            return roles;

        }

        public static bool HasRole(int id, string[] roles) {
            using (UserContext db = new UserContext()) {
                User user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null) {
                    UserType usertype = db.UserTypes.Find(user.UserType);
                    if (usertype != null) {
                        foreach (var role in roles) {
                            if (usertype.Name.ToLower() == role.ToLower()) {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;

        }

        public static bool HasRole(int id, string role) {
            return HasRole(id, new string[] { role });
        }

        public override string[] GetUsersInRole(string roleName) {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName) {

            bool outputResult = false;
            using (UserContext db = new UserContext()) {
                User user = db.Users.FirstOrDefault(u => u.Email == username);
                if (user != null) {
                    UserType usertype = db.UserTypes.Find(user.UserType);
                    if (usertype != null && usertype.Name == roleName)
                        return outputResult = true;

                }
            }
            return outputResult;

        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName) {
            throw new NotImplementedException();
        }
    }
}