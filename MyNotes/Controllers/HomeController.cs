using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MyNotes.Dao;
using MyNotes.Entity;

namespace MyNotes.Controllers
{
    public class HomeController : Controller
    {
        CommonDao dao = new CommonDao();
        public ActionResult Index(Guid? id)
        {
            if (id != null)
            {
                ViewBag.Model = dao.GetNote(id.Value);
            }
            Session["CAPTCHA"] = GetRandomText();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(tbl_Notes model, string clientCaptcha)
        {
            string serverCaptcha = Session["CAPTCHA"].ToString();
            if (!clientCaptcha.Equals(serverCaptcha))
            {
                ViewBag.ShowCAPTCHA = serverCaptcha;

                ViewBag.CaptchaError = "Wrong captcha !!!";
                Session["CAPTCHA"] = GetRandomText();
                return View(model);
            }
            model.ID = Guid.NewGuid();
            //model.Content = model.Content.Replace(Environment.NewLine, "<br/>");
            if (dao.AddNote(model))
            {
                return RedirectToAction("Index", new { id = model.ID });
            }
            else
                return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Son Nguyen";

            return View();
        }

        public ActionResult Recent(string s, int page=1, int pageSize = 10)
        {
            ViewBag.Search = s;
            var model = dao.ListAllPaging(s, page, pageSize);
            return View(model);
        }

        public ActionResult Raw(Guid? id)
        {
            if(id != null)
            {
                var model = dao.GetNote(id.Value);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Download(Guid? id)
        {
            if (id != null)
            {
                var model = dao.GetNote(id.Value);
                return File(Encoding.UTF8.GetBytes(model.Content),
                 "text/plain",
                  string.Format("{0}.txt", model.Name));
            }
            return RedirectToAction("Index");
        }

        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 7; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }

        public FileResult CaptchaImg()
        {
            string text = Session["CAPTCHA"].ToString();

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 15);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
            drawing = Graphics.FromImage(img);

            Color backColor = Color.DarkGray;
            Color textColor = Color.Red;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }
    }
}