using MyNotes.Dao;
using MyNotes.Entity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyNotes.Controllers
{
    public class AdminController : Controller
    {
        CommonDao dao = new CommonDao();
        // GET: Admin
        public ActionResult Index(string sa, int page = 1, int pageSize = 20)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("DangNhap", "Login");
            ViewBag.Search = sa;
            var model = dao.ListAllPaging(sa, page, pageSize, true);
            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("DangNhap", "Login");
            if (id == null)
                return RedirectToAction("Index");
            var model = dao.GetNote(id.Value);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbl_Notes model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("DangNhap", "Login");
            if (dao.EditNote(model))
            {
                return RedirectToAction("Index");
            }
            else
                return View(model);
        }

        public ActionResult Delete(Guid? id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("DangNhap", "Login");
            if (id == null)
                return RedirectToAction("Index");
            dao.DeleteNote(id.Value);
            return RedirectToAction("Index");
        }
    }
}