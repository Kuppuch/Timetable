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
        public ActionResult Index()
        {
            return View(DAODiscipline.GetDisciplines());
        }

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
    }
}