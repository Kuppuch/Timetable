using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TimeTable.Models {
    public class Discipline {

        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Преподаватель")]
        public string UserText { get; set; }
        [DisplayName("id преподавателя")]
        public int User { get; set; }
    }
}