using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class Timetable {
        public int Id { get; set; }
        public string Lesson { get; set; }
        public string Teacher { get; set; }
        public string WeekDay { get; set; }
        public int Numerator { get; set; }
        public int LessonNumber { get; set; }
        public string Location { get; set; }
    }
}