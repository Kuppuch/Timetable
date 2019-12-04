using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class Lesson {
        public int Id { get; set; }
        public int Discipline { get; set; }
        public int Group { get; set; }
        public int Teacher { get; set; }


        public string DisciplineText { get; set; }
        public string GroupText { get; set; }
        public string TeacherText { get; set; }
    }
}