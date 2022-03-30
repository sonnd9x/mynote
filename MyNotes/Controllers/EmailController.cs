using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MyNotes.Dao;
using MyNotes.Entity;
using MyNotes.Models;
using Newtonsoft.Json;

namespace MyNotes.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        private readonly CommonDao dao = new CommonDao();
        public ActionResult Index(string s, int page = 1, int pageSize = 20)
        {
            ViewBag.Search = s;
            var model = dao.ListEmailAllPaging(s, page, pageSize);
            return View(model);
        }
        [HttpPost()]
        public async Task Receive(EmailModel model)
        {
            try
            {
                var email = new tbl_Emails
                {
                    Date = model.Date,
                    From = model.From.Text,
                    To = model.To.Text,
                    Text = model.Text,
                    Html = model.TextAsHtml,
                    Subject = model.Subject
                };

                await dao.InsertEmail(email);
            }
            catch (Exception ex)
            {

            }
        }
    }
}