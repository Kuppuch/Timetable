using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable {
    public class TimeTableLine {

        public int time = 0;

        public string timeStart {
            get {
                var h = Math.Floor((double)time / 60);
                var m = (double)time % 60;
                return (h < 10 ? "0" + h : "" + h) + ":" + (m < 10 ? "0" + m : "" + m);
            }
        }
        public string timeEnd {
            get {
                var t = time + 1 * 60 + 30;
                var h = Math.Floor((double)t / 60);
                var m = (double)t % 60;
                return (h < 10 ? "0" + h : "" + h) + ":" + (m < 10 ? "0" + m : "" + m);
            }
        }

        public List<Models.Timetable> pairs = new List<Models.Timetable>();

    }
}