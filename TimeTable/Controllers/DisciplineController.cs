using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;

namespace TimeTable.Controllers
{
    public class DisciplineController : Controller
    {
        DAODiscipline daoDiscipline = new DAODiscipline();

        // GET: Discipline
        public ActionResult Index()
        {
            return View(daoDiscipline.GetDiscipline());
        }
    }
}