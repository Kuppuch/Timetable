using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTable.Models.Context {
    [Table("user_type")]
    public class UserTypeData {

        public int id { get; set; }
        public string name { get; set; }

    }
}