using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;

namespace TimeTable.Controllers {
    public class HomeController : Controller {

        DAOUser daoUser = new DAOUser();
        DAOGroup daoGroup = new DAOGroup();
        DAODiscipline daoDiscipline = new DAODiscipline();
        DAOLesson daoLesson = new DAOLesson();

        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult User() {
            ViewBag.Message = "Your users page.";

            return View(daoUser.GetUsers());
        }

        public ActionResult Group() {
            ViewBag.Message = "Your group page.";

            return View(daoGroup.GetGroups());
        }

        public ActionResult Discipline() {
            ViewBag.Message = "Your discipline page.";

            return View(daoDiscipline.GetDiscipline());
        }

        public ActionResult Lesson() {
            ViewBag.Message = "Your lesson page.";

            return View(daoLesson.GetLesson());
        }

        [HttpGet]
        public ActionResult Disc() {
            ViewBag.Discipline = new SelectList(daoLesson.GetDiscipline(), "discipline", "Discipline");
            return View();
        }


    }
}