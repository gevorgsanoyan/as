using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;
using ASFront.Classes;
using PagedList;

namespace ASFront.Controllers
{
    public class DocTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DocTypes
      
        public ActionResult Index(int page = 1)
        {


            var items = db.DocType.Distinct().ToList();


            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }


        // GET: DocTypes/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DocType docType = db.DocType.Find(id);
        //    if (docType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(docType);
        //}

        // GET: DocTypes/Create
        public ActionResult Create()
        {
            Models.DocType item = new DocType();
            //item.userId = User.Identity.GetUserId();
            //item.LastModifDate = DateTime.Now;

            return View(item);
        }

        // POST: DocTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( DocType docType)
        {
           
                if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
                {
                    db.DocType.Add(docType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(docType);
        }

        // GET: DocTypes/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Create");

            DocType docType = db.DocType.Find(id);
            if (docType.Id == 0)
                return RedirectToAction("Create");

            return View(docType);
        }

        // POST: DocTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] DocType docType)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                db.Entry(docType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(docType);
        }

        // GET: DocTypes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DocType docType = db.DocType.Find(id);
        //    if (docType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(docType);
        //}

        //// POST: DocTypes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    DocType docType = db.DocType.Find(id);
        //    db.DocType.Remove(docType);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
