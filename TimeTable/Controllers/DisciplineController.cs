using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;
using TimeTable.Models;

namespace TimeTable.Controllers {
    public class DisciplineController : Controller {
        DAODiscipline daoDiscipline = new DAODiscipline();
        DAOLesson daoLesson = new DAOLesson();

        // GET: Discipline
        [Authorize(Roles = "Спец. по кадрам, Преподаватель")]
        public ActionResult Index() {
            return View(DAODiscipline.GetDisciplines());
        }

        [Authorize(Roles = "Спец. по кадрам")]
        public ActionResult Create() {
            ViewBag.Message = DAOUser.GetTeachers();
            return View(new Discipline());
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Discipline dd) {
            try {
                if (daoDiscipline.InsertDiscipline(dd))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index");
            } catch (Exception ex){
                Logger.Logger.Log.Info("Неудалось добавить дисциплину: " + ex);
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Спец. по кадрам, Преподаватель")]
        public ActionResult Details(int id) {
            return View(DAODiscipline.GetDiscipline(id));
        }

        [Authorize(Roles = "Спец. по кадрам")]
        public ActionResult Delete(int id) {
            return View(DAODiscipline.GetDiscipline(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            if (ModelState.IsValid && DAODiscipline.DeleteDiscipline(id))
                return RedirectToAction("Index");
            return View();
        }

        [Authorize(Roles = "Спец. по кадрам")]
        public ActionResult Edit(int id) {
            ViewBag.Message = DAOUser.GetTeachers();
            return View(DAODiscipline.GetDiscipline(id));
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id, Name, User, UserText")]Discipline discipline) {
            if (ModelState.IsValid && DAODiscipline.EditDiscipline(discipline))
                return RedirectToAction("Index");
            return View();
        }
    }
}