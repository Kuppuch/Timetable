﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class User {
        private int id;
        private string name;
        private string group;
        private int year;
        private string userType;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public int Year { get; set; }
        public string UserType { get; set; }
    }
}