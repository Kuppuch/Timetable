using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TimeTable.Models;
using TimeTable.Models.Context;

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
            using (UserContext db = new UserContext()) {
                var user = db.Users.FirstOrDefault(u => u.email == username);
                if (user != null) {
                    UserTypeData usertype = db.UserTypes.Find(user.type);
                    if (usertype != null)
                        return new string[] { usertype.name };
                }
            }

            return new string[] { };

        }

        public override string[] GetUsersInRole(string roleName) {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string email, string roleName) {

            bool outputResult = false;
            using (UserContext db = new UserContext()) {
                UserData user = db.Users.FirstOrDefault(u => u.email == email);
                if (user != null) {
                    UserTypeData usertype = db.UserTypes.Find(user.type);
                    if (usertype != null && usertype.name == roleName)
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