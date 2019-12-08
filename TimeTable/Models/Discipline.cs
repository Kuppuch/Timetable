using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class Discipline {
        //private int id;
        //private string name;
        //private string user;

        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Преподаватель")]
        public string UserText { get; set; }
        public int User { get; set; }
    }
}