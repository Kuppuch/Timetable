using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Models {
    public class Group {

        private int id;
        private string name;
        private DateTime year;
        private string fullname;

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Year { get; set; }
        public string Fullname { get; set; }

        private string GetFullName() {
            fullname = name + year;
            Console.WriteLine(fullname);

            return fullname;
        }

    }
}