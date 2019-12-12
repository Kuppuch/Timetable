using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;
using TimeTable.Models;

namespace TimeTable.Controllers {
    public class HomeController : Controller {

        DAOUser daoUser = new DAOUser();
        DAOGroup daoGroup = new DAOGroup();
        DAODiscipline daoDiscipline = new DAODiscipline();
        DAOLesson daoLesson = new DAOLesson();
        DAOTimetable daoTimetable = new DAOTimetable();


        public ActionResult Index() {
            ViewBag.Group = DAOGroup.GetGroups();
            ViewBag.Pairs = DAOTimetable.GetPairs();
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

        //public ActionResult User() {
        //    ViewBag.Message = "Your users page.";

        //    return View(daoUser.GetUsers());
        //}

        //public ActionResult Group() {
        //    ViewBag.Message = "Your group page.";

        //    return View(DAOGroup.GetGroups());
        //}

        //public ActionResult Discipline() {
        //    ViewBag.Message = "Your discipline page.";

        //    return View(DAODiscipline.GetDisciplines());
        //}

        //public ActionResult Lesson() {
        //    ViewBag.Message = "Your lesson page.";

        //    return View(daoLesson.GetLesson());
        //}

        //public ActionResult Disc() {
        //    return View(DAODiscipline.GetDisciplines());
        //}

        //public ActionResult Year() {
        //    return View(DAOGroup.GetGroups());
        //}

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