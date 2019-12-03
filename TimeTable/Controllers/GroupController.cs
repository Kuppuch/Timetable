using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;

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
    }
}