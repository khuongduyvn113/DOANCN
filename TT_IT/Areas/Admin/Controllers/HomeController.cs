using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TT_IT.Models;
namespace TT_IT.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        TinTucEntities db = new TinTucEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.CountPostNotShow = db.Posts.Count(m => m.Show == false);
            ViewBag.UsersRegisterToDay = db.AspNetUsers.Where(m => m.DateRegister == DateTime.Today).ToList();

            return View();
        }

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