using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;

namespace TimeTable.Controllers
{
    public class LessonController : Controller
    {
        DAOLesson daoLesson = new DAOLesson();

        // GET: Lesson
        public ActionResult Index()
        {
            return View(daoLesson.GetLesson());
        }

        public ActionResult Disc() {
            return View(daoLesson.GetDisciplines());
        }

        public ActionResult Year() {
            return View(daoLesson.GetGroups());
        }

    }
}