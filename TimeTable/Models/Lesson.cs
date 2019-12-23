using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class Lesson {
        public int Id { get; set; }
        [DisplayName("id дисциплины")]
        public int Discipline { get; set; }
        [DisplayName("id группы")]
        public int Group { get; set; }
        [DisplayName("Преподаватель")]
        public int Teacher { get; set; }

        [DisplayName("Дисциплина")]
        public string DisciplineText { get; set; }
        [DisplayName("Группа")]
        public string GroupText { get; set; }
        [DisplayName("Преподаватель")]
        public string TeacherText { get; set; }
    }
}