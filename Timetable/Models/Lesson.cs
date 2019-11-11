using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Models {
    public class Lesson {

        private int id;
        private string group;
        private string discipline;
        private string user;

        public int Id { get; set; }
        public int Group { get; set; }
        public string Discipline { get; set; }
        public string User { get; set; }

    }
}