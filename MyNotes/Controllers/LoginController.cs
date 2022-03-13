using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNotes.Entity;
using MyNotes.Dao;
using System.Web.Security;

namespace MyNotes.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult DangNhap()
        {
            //Comment if u dont want to create demo user
            //new CommonDao().CreateFirstUser();
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(tbl_User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else if( new CommonDao().CheckUser(model.Username, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, true);
                return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult DangXuat()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}