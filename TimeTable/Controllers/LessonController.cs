using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;
using TimeTable.Models;

namespace TimeTable.Controllers {
    public class LessonController : Controller {
        DAOLesson daoLesson = new DAOLesson();


        // GET: Lesson
        public ActionResult Index() {
            return View(daoLesson.GetLesson());
        }

        public ActionResult Disc() {
            return View(DAODiscipline.GetDisciplines());
        }

        public ActionResult Year() {
            return View(DAOGroup.GetGroups());
        }

        public ActionResult Create() {
            ViewBag.Message = daoLesson.GetLesson();
            return View(new Lesson());
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Lesson dl) {
            try {
                if (daoLesson.InsertLesson(dl)) 
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index");
            } catch {
                Console.WriteLine("Сюда бы не забыть добавить Log4Net!");
                return RedirectToAction("Index");
            }
        }

    }
}