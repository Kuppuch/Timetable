using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class Lesson {
        private int id;
        private string discipline;
        private string group;
        private int year;
        private string teacher;

        public int Id { get; set; }
        public string Discipline { get; set; }
        public string Group { get; set; }
        public int Year { get; set; }
        public string Teacher { get; set; }
    }
}