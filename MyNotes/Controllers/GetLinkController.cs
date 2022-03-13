using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Bitly.Net;

namespace MyNotes.Controllers
{
    public class GetLinkController : Controller
    {
        // GET: GetLink
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string link)
        {
            if (string.IsNullOrEmpty(link))
                return View();
            Regex pattern = null;
            Match m = null;
            string result = string.Empty;
            if (link.Contains("https://drive.google.com"))
            {
                pattern = new Regex(@"https?:\/\/(?:drive\.google\.com\/file\/d\/)([\w-]{28}).*");
                if (pattern.IsMatch(link))
                {
                    m = pattern.Match(link);
                    result = @"https://drive.google.com/uc?export=download&id=" + m.Groups[1].Value;
                }
                else
                {
                    pattern = new Regex(@"https?:\/\/(?:drive\.google\.com\/open\?id=)([\w-]{28}).*");
                    if (pattern.IsMatch(link))
                    {
                        m = pattern.Match(link);
                        result = @"https://drive.google.com/uc?export=download&id=" + m.Groups[1].Value;
                    }
                }
            }
            else if (link.Contains("dropbox.com"))
            {
                //pattern = new Regex(@"https://www.dropbox.com/s/jh7dq4uh4k26d0b/Huong%20dan.txt?dl=0");
                //if (pattern.IsMatch(link))
                //{
                //    m = pattern.Match(link);
                //    result = @"https://dl.dropboxusercontent.com/s/" + m.Groups[1].Value;
                //}
                result = link.Replace("www.dropbox.com", "dl.dropboxusercontent.com").Replace("dl=0", "dl=1");
            }
            else if (link.Contains(@"https://onedrive.live.com/embed"))
            {
                result = link.Replace("embed", "download");
            }
            if (!string.IsNullOrEmpty(result))
            {
                string accesstoken = "19ef39ce4a13e2448b98ac7beed5556552b05bd5";
                var bitly = new BitlyAPI(accesstoken);
                var shortlink = await bitly.ShortenAsync(result);
                ViewBag.Short = string.IsNullOrEmpty(shortlink)? "Lỗi ! Không rút gọn link." : shortlink;
            }
            ViewBag.Result = string.IsNullOrEmpty(result) ? "Lỗi ! Không lấy được link." : result;
            return View();
        }
    }
}