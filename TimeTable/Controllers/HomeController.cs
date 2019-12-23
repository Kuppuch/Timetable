using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;
using TimeTable.Models;
using TimeTable.Providers;

namespace TimeTable.Controllers {
    public class HomeController : Controller {

        DAOTimetable daoTimetable = new DAOTimetable();

        //[Authorize(Roles= "Спец. по кадрам, Преподаватель, Студент")]
        public ActionResult Index(LoginModel model) {

            //User user;
            //user = DAOUser.GetUser(model.Email);
            ////var userID = DAOUser.GetUser(model.Email).Id; /*User.Identity.Id ?? 0+"");*/
            //var userID = user.Id;
            //if (!CustomRoleProvider.HasRole(userID, new string[] { "Спец. по кадрам", "Преподаватель", "Студент" })) {
            //    return Redirect("/Account/Login");
            //}

            var group_id = Convert.ToInt32(Request.QueryString["group_id"]);
            ViewBag.ActiveGroup = group_id;
            ViewBag.Group = DAOGroup.GetGroups();
            ViewBag.Pairs = DAOTimetable.GetPairs(group_id);
            return View(DAOTimetable.GetTimetable());
           
        }

        public ActionResult Timetable() {
            return View(DAOTimetable.GetTimetable());
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Create() {
            ViewBag.Message = DAOLesson.GetLessons();
            ViewBag.Group = DAOGroup.GetGroups();
            return View(new Timetable() { Numerator = true });
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Timetable dt) {
            try {
                if (daoTimetable.InsertTimetable(dt))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index");
            }
            catch {
                Console.WriteLine("Сюда бы не забыть добавить Log4Net!");
                return RedirectToAction("Index");
            }
        }
    }
}