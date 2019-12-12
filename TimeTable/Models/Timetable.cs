using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class Timetable {
        public int Id { get; set; }
        [DisplayName("Занятие")]
        public int Lesson { get; set; }
        [DisplayName("Занятие")]
        public string LessonText { get; set; }
        [DisplayName("Преподаватель")]
        public int Teacher { get; set; }
        [DisplayName("Преподаватель")]
        public string TeacherText { get; set; }
        [DisplayName("День недели")]
        public int WeekDay { get; set; }

        [DisplayName("Числитель")]
        public bool Numerator { get; set; }
        [DisplayName("Номер занятия")]
        public string LessonNumber { get; set; }
        [DisplayName("Место проведения")]
        public string Location { get; set; }
        [DisplayName("Группа")]
        public int Group { get; set; }
        [DisplayName("Группа")]
        public string GroupText { get; set; }
        public int GroupYear { get; set; }
    }
}