using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TimeTable.DAO;
using TimeTable.Models;
using TimeTable.Providers;
using TimeTable.Logger;

namespace TimeTable.Controllers {
    public class HomeController : Controller {

        DAOTimetable daoTimetable = new DAOTimetable();

        [Authorize(Roles = "Спец. по кадрам, Преподаватель")]
        public ActionResult Index() {
            var group_id = Convert.ToInt32(Request.QueryString["group_id"] ?? "1");

            ViewBag.ActiveGroup = group_id;
            ViewBag.Group = DAOGroup.GetGroups();
            ViewBag.Pairs = DAOTimetable.GetPairs(group_id);

            return View(DAOTimetable.GetTimetable());
        }

        public ActionResult Timetable() {
            return View(DAOTimetable.GetTimetable());
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Спец. по кадрам")]
        public ActionResult Create() {
            ViewBag.Message = DAOLesson.GetLessons();
            ViewBag.Group = DAOGroup.GetGroups();

            return View(new Timetable() { Numerator = true });
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Timetable dt) {
            try {
                if (daoTimetable.InsertTimetable(dt))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index");
            } catch {
                Logger.Logger.Log.Info("Не удалось добавить занятие в таблицу расписания");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Index(int Group, string Submit) {
            try {
                if (daoTimetable.Publish(Submit != "Отозвать"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index");
            }
            catch (Exception ex) {
                Logger.Logger.Log.Info("Не опкбликовать расписание: " + ex);
                return RedirectToAction("Index");
            }
        }
    }
}