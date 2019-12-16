using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

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
    }
}