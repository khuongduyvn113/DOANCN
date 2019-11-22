using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TT_IT.Models;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
namespace Blog_IT.Controllers
{
    public class PostController : Controller
    {
        TinTucEntities db = new TinTucEntities();

        [ChildActionOnly]
        public ActionResult Featured_ArticlesPartial()
        {
            IEnumerable<Post> featured_Articles = db.Posts.Where(m => m.Show == true).OrderByDescending(m => m.Views).Take(5);
            return PartialView(featured_Articles);
        }

        [ChildActionOnly]
        public ActionResult Featured_ArticlesPartial2()
        {
            IEnumerable<Post> featured_Articles = db.Posts.Where(m => m.Show == true).OrderByDescending(m => m.Views).Take(5);
            return PartialView(featured_Articles);
        }
        // GET: Post
        [Route("{alias}.html")]
        public async Task<ActionResult> Detail(string alias, string reportMessage, string errorMessage)
        {
            if (alias == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.SingleOrDefault(m => m.Alias == alias);
            if (post == null || post.Show == false)
            {
                return RedirectToAction("PageNotFound", "StaticContent", new { area = "" });
            }
            if (Session["read_post" + post.PostID.ToString()] == null)
            {
                post.Views++;
                db.Entry(post).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                Session["read_post" + post.PostID.ToString()] = "readed";
            }
            ViewBag.ReportMessage = reportMessage;
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.RelatedPost = db.Posts.Where(m => m.SubCategoryID == post.SubCategoryID && m.PostID != post.PostID && m.Show == true).Take(5).AsEnumerable();
            return View(post);
        }
        [Authorize]
        [Route("Demo/{alias}.html")]
        public ActionResult Demo(string alias)
        {
            if (alias == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.SingleOrDefault(m => m.Alias == alias);
            if (post == null)
            {
                return RedirectToAction("PageNotFound", "StaticContent", new { area = "" });
            }
            if (User.IsInRole("Admin") == false)
            {
                if (post.UserID != User.Identity.GetUserId())
                {
                    return RedirectToAction("PageNotFound", "StaticContent", new { area = "" });
                }
            }
            ViewBag.RelatedPost = db.Posts.Where(m => m.SubCategoryID == post.SubCategoryID && m.PostID != post.PostID && m.Show == true).Take(5).AsEnumerable();
            return View("Detail", post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Route("category/{alias}")]
        public ActionResult PostByCategory(string alias, int? page)
        {
            Category category = db.Categories.SingleOrDefault(m => m.Alias == alias);
            if (category == null)
            {
                return RedirectToAction("PageNotFound", "StaticContent");
            }
            ViewBag.CategoryName = category.Name;
            var model = category.Posts.Where(m => m.Show == true).OrderByDescending(m => m.PostID);
            return View(model.ToPagedList(page ?? 1, 10));
        }
        [Route("subcategory/{alias}")]
        public ActionResult PostBySubCategory(string alias, int? page)
        {
            SubCategory subCategory = db.SubCategories.SingleOrDefault(m => m.Alias == alias);
            if (subCategory == null)
            {
                return RedirectToAction("PageNotFound", "StaticContent");
            }
            ViewBag.SubCategoryName = subCategory.Name;
            var model = subCategory.Posts.Where(m => m.Show == true).OrderByDescending(m => m.PostID);
            return View(model.ToPagedList(page ?? 1, 10));
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