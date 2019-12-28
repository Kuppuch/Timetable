using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;
using TimeTable.Models;

namespace TimeTable.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        [Authorize(Roles = "Спец. по кадрам, Преподаватель, Студент")]
        public ActionResult Index() {
            var group_id = Convert.ToInt32(Request.QueryString["group_id"] ?? "1");

            ViewBag.ActiveGroup = group_id;
            ViewBag.Group = DAOGroup.GetGroups();
            ViewBag.Pairs = DAOSchedule.GetPairs(group_id);

            return View(DAOSchedule.GetTimetable());
        }

    }
}