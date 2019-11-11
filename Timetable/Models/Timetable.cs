using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Models {
    public class Timetable {

        private int id;
        private int lesson;
        private string weekDay;
        private string numerator;
        private int lessonNumber;
        private string location;

        public int Id { get; set; }
        public int Lesson { get; set; }
        public string WeekDay { get; set; }
        public string Numerator { get; set; }
        public int LessonNumber { get; set; }
        public int Location { get; set; }

    }
}