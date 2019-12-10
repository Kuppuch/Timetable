using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class Lesson {
        public int Id { get; set; }
        public int Discipline { get; set; }
        public int Group { get; set; }
        public int Teacher { get; set; }

        [DisplayName("Дисциплина")]
        public string DisciplineText { get; set; }
        [DisplayName("Группа")]
        public string GroupText { get; set; }
        [DisplayName("Преподаватель")]
        public string TeacherText { get; set; }
    }
}