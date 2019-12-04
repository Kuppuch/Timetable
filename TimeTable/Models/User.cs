using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Group { get; set; }
        public int UserType { get; set; }
        public string GroupText { get; set; }
        public string UserTypeText { get; set; }
    }
}