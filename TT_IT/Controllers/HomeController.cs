using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TT_IT.Models;
using PagedList;
using PagedList.Mvc;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;
using System.Text;

namespace TT_IT.Controllers
{
    public class HomeController : Controller
    {
        TinTucEntities db = new TinTucEntities();
        

        public ActionResult Index(int? page)
        {
            var newPosts = db.Posts.Where(m => m.Show == true).OrderByDescending(p => p.PostID);
            return View(newPosts.ToPagedList(page ?? 1, 10));
        }

        public ActionResult About()
        {
            return View();
        }

        public PartialViewResult LayNoiDungBangTin()
        {
            return PartialView(db.Popups.First());
        }
        //[ChildActionOnly]
        //public CaptchaImageResult ShowCaptchaImage()
        //{
        //    return new CaptchaImageResult();
        //}
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Mailbox model)
        {
            model.SendDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Mailboxes.Add(model);
                db.SaveChanges();
                //ViewBag.MessageSuccess = "Gửi thành công.";
                return RedirectToAction("ContactSuccess");
            }
            return View(model);
        }
        [ChildActionOnly]
        public PartialViewResult _TopMenuPartial()
        {
            return PartialView("_TopMenuPartial", db.Categories.OrderBy(m => m.STT).AsEnumerable());
        }

        public ViewResult ContactSuccess()
        {
            return View();
        }
        [ChildActionOnly]
        [OutputCache(Duration = 86400)]
        public PartialViewResult _HinhGanDayPartial()
        {
            return PartialView(db.Posts.Where(m => m.Show == true).OrderByDescending(m => m.PostID).Take(9).Select(m => m.Image));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}