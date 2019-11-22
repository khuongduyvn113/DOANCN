using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TT_IT.Models;
namespace TT_IT.Areas.Mod.Controllers
{
    [Authorize(Roles = "Mod")]
    public class HomeController : Controller
    {
        TinTucEntities db = new TinTucEntities();
        public AspNetUser user
        {
            get
            {
                string id = User.Identity.GetUserId();
                return db.AspNetUsers.SingleOrDefault(m => m.Id == id);
            }
            set
            {
                user = value;
            }
        }
        // GET: Mod/Home
        public ActionResult Index()
        {
            ViewBag.Name = user.FirstName;
            return View();
        }
    }
}