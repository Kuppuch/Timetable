using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace TimeTable.Models {
    public class Group {
        private int id;
        private string name;
        private int year;

        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Год ")]
        public int Year { get; set; }
    }
}