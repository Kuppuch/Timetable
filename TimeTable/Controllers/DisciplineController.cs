using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;
using TimeTable.Models;

namespace TimeTable.Controllers
{
    public class DisciplineController : Controller
    {
        DAODiscipline daoDiscipline = new DAODiscipline();
        DAOLesson daoLesson = new DAOLesson();

        // GET: Discipline
        [Authorize]
        public ActionResult Index()
        {
            return View(DAODiscipline.GetDisciplines());
        }

        [Authorize]
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
            }
            catch {
                Console.WriteLine("Сюда бы не забыть добавить Log4Net!");
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        public ActionResult Details(int id) {
            return View(DAODiscipline.GetDiscipline(id));
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
            ViewBag.Message = DAOUser.GetTeachers();
            return View(DAODiscipline.GetDiscipline(id));
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Name, Group")]Discipline discipline) {
            if (ModelState.IsValid && DAODiscipline.EditDiscipline(discipline))
                return RedirectToAction("Index");
            return View();
        }
    }
}