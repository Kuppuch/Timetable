using Newtonsoft.Json.Linq;
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
using TimeTable.Models.Context;

namespace TimeTable.Models {
    public class UserContext : DbContext {
        public UserContext() : base("DefaultConnection") { }

        public DbSet<UserData> Users { get; set; }
        public DbSet<UserTypeData> UserTypes { get; set; }

        public static bool CheckUser(string email, string password) {
            NameValueCollection config = WebConfigurationManager.AppSettings;
            using (WebClient wc = new WebClient()) {
                var json = wc.DownloadString(string.Format(config["api_userCheck"], email, password));
                dynamic jsonObject = JObject.Parse(json);
                return jsonObject["result"] == "true";
            }
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