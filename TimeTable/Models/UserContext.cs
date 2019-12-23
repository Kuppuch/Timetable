﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using TimeTable.DAO;

namespace TimeTable.Models {
    public class UserContext : DbContext {
        public UserContext() :
        base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        public static bool CheckUser(string email, string password) {
            NameValueCollection config = WebConfigurationManager.AppSettings;
            using (WebClient wc = new WebClient()) {
                var json = wc.DownloadString(string.Format(config["api_userCheck"], email, password));
                dynamic jsonObject = JObject.Parse(json);
                InputUT();
                return jsonObject["result"] == "true";
            }
        }

        public static void InputUT() {
            List<UserType> list = new List<UserType>();
            list = DAOUserType.GetUserTypies();
        }

    }

    public class LoginModel {
        [Required]
        [DisplayName("Логин")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }


}