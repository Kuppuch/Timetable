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
        LessionContainer lc = new LessionContainer();


        // GET: Lesson
        [Authorize]
        public ActionResult Index() {
            return View(daoLesson.GetLessonContainer());
        }

        public ActionResult Disc() {
            return View(DAODiscipline.GetDisciplines());
        }

        public ActionResult Year() {
            return View(DAOGroup.GetGroups());
        }

        [Authorize]
        public ActionResult Create() {
            ViewBag.Message = daoLesson.GetLessonContainer();
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

        [Authorize]
        public ActionResult Details(int id) {
            return View(lc.GetLesson(id));
        }

        [Authorize]
        public ActionResult Delete(int id) {
            return View(DAOLesson.GetLesson(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            if (ModelState.IsValid && DAOLesson.DeleteLesson(id))
                return RedirectToAction("Index");
            return View();
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id) {
            ViewBag.Message = daoLesson.GetLessonContainer();
            return View(DAOLesson.GetLesson(id));
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Discipline, Group, Teacher")]Lesson lesson) {
            if (ModelState.IsValid && DAOLesson.EditLesson(lesson))
                return RedirectToAction("Index");
            return View();
        }

    }
}