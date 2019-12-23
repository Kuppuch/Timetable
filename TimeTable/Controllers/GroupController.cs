﻿using System;
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
        [Authorize]
        public ActionResult Index()
        {
            return View(DAOGroup.GetGroups());
        }

        [Authorize]
        public ActionResult Create() {
            ViewBag.Message = DAOGroup.GetYears();
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

        [Authorize]
        public ActionResult Delete(int id) {
            return View(DAOGroup.GetGroup(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            if (ModelState.IsValid && DAOGroup.DeleteGroup(id))
                return RedirectToAction("Index");
            return View();
        }
    }
}