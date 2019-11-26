using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class User {
        private int id;
        private string name;
        private int group;
        private int userType;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Group { get; set; }
        public int UserType { get; set; }
    }
}