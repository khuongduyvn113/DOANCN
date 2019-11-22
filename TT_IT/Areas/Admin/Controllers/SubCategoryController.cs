using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TT_IT.Models;


namespace TT_IT.Areas.Admin.Controllers
{

    public class SubCategoryController : Controller
    {
        private TinTucEntities db = new TinTucEntities();

        // GET: Admin/Tags
        public ActionResult Index()
        {
            return View(db.SubCategories.Include(c => c.Category).ToList());
        }

        // GET: Admin/Tags/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Admin/Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CategoryID,STT,Alias")] SubCategory subcategory)
        {
            if (ModelState.IsValid)
            {
                db.SubCategories.Add(subcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subcategory);
        }



        // GET: Admin/Tags/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = db.SubCategories.Find(id);
            if (subcategory == null)
            {
                return RedirectToAction("PageNotFound", "StaticContent", new { area = "" });
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View(subcategory);
        }

        // POST: Admin/Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CategoryID,STT,Alias")] SubCategory subcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View(subcategory);
        }

        // GET: Admin/Tags/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = db.SubCategories.Find(id);
            if (subcategory == null)
            {
                return RedirectToAction("PageNotFound", "StaticContent", new { area = "" });
            }
            return View(subcategory);
        }

        // POST: Admin/Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SubCategory subcategory = db.SubCategories.Find(id);
            db.SubCategories.Remove(subcategory);
            db.SaveChanges();
            return RedirectToAction("Index");
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
