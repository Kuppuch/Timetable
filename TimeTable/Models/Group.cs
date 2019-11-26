using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class Group {
        private int id;
        private string name;
        private int year;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}