using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTable.Models.Context {
    [Table("users")]
    public class UserData {

        public int id { get; set; }
        public string name { get; set; }
        public int? group { get; set; }
        public int type { get; set; }
        public string email { get; set; }

    }
}