using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AttendanceAPISync {
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Group { get; set; }
        public int UserType { get; set; }
        public string Email { get; set; }
    }
}