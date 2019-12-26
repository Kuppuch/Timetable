using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TimeTable.Models;
using TimeTable.DAO;

namespace TimeTable.Controllers {
    public class AccountController : Controller {
        // GET: Account
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model) {

            if (ModelState.IsValid) {
                if (UserContext.CheckUser(model.Email, model.Password)) {
                    FormsAuthentication.SetAuthCookie(model.Email, true);

                    return RedirectToAction("Index", "Home", model);
                } else {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff() {
            FormsAuthentication.SignOut();
            Session.Abandon(); // Очистит сессию
            return RedirectToAction("Index", "Home");
        }
    }
}