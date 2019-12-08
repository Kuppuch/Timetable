using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;
using TimeTable.Models;

namespace TimeTable.Controllers
{
    public class GroupController : Controller
    {

        DAOGroup daoGroup = new DAOGroup();

        // GET: Group
        public ActionResult Index()
        {
            return View(daoGroup.GetGroups());
        }

        public ActionResult Create() {
            ViewBag.Message = daoGroup.GetYears();
            return View(new Group());
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Group dg) {
            try {
                if (daoGroup.InsertGroup(dg))
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