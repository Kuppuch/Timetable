using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;
using TimeTable.Models;

namespace TimeTable.Controllers {
    public class GroupController : Controller {

        // GET: Group
        [Authorize(Roles = "Спец. по кадрам, Преподаватель")]
        public ActionResult Index() {
            return View(DAOGroup.GetGroups());
        }
    }
}