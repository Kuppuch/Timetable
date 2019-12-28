using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceAPISync {
    public class Timetable {
        public int Id { get; set; }

        public int Lesson { get; set; }
        public string LessonText { get; set; }
        public int Teacher { get; set; }
        public string TeacherText { get; set; }
        public int WeekDay { get; set; }

        public bool Numerator { get; set; }
        public string LessonNumber { get; set; }
        public string Location { get; set; }
        public int Group { get; set; }
        public string GroupText { get; set; }
        public int GroupYear { get; set; }
    }
}
