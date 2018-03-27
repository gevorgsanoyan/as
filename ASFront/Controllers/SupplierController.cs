using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using ASFront.Classes;

namespace ASFront.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: shops
        public ActionResult Index(int page = 1)
        {
            var suppliers = db.Suppliers.Where(p => p.Active == true).ToList().OrderBy(p => p.SupplierName);
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(suppliers.ToPagedList(pageNumber, pageSize));
          
        }

        // GET: shops/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers sup = db.Suppliers.Find(id);

            if (sup == null)
            {
                return HttpNotFound();
            }

            var usr = (from u in db.Users
                       where u.Id == sup.userId
                       select new { u.UserName });

            //if (!usr.Any())
            //{
            //    return HttpNotFound();
            //}
            foreach (var u in usr)
            {
                sup.userId = u.UserName;
            }
            if (sup == null)
            {
                return HttpNotFound();
            }
            return View(sup);
        }

        // GET: shops/Create
        [Authorize]
        public ActionResult Create()
        {
            Suppliers item = new Suppliers();
            return View(item);
        }

        // POST: shops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Suppliers shop)
        {
            if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                shop.userId = User.Identity.GetUserId();
                 
                db.Suppliers.Add(shop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shop);
        }

        // GET: shops/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers shop = db.Suppliers.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // POST: shops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Suppliers shop)
        {
             if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save) )
            {
                shop.userId = User.Identity.GetUserId();

                db.Entry(shop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shop);
        }

        // GET: shops/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers sup = db.Suppliers.Find(id);            
            if (sup == null)
            {
                return HttpNotFound();
            }
            return View(sup);
        }

        // POST: shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Suppliers shop = db.Suppliers.Find(id);
            db.Suppliers.Remove(shop);
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
