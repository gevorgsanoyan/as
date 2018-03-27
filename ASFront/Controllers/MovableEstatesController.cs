using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;

namespace ASFront.Controllers
{
    public class MovableEstatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MovableEstates
        public ActionResult Index()
        {
            long ApplicationID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];


            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);



            long ClientID = 0;
            string ClientIDStr = Request.QueryString["ClientID"];


            if (!string.IsNullOrWhiteSpace(ClientIDStr))
                Int64.TryParse(ClientIDStr, out ClientID);





            ViewBag.ApplicationID = ApplicationID;


            var movableEstate = db.MovableEstate.Include(m => m.applications).Include(m => m.MovableEstateTypes).Where(p => (p.applicationId == ApplicationID)).ToList() ?? new List<MovableEstates>();
            return View(movableEstate.ToList());
        }

        // GET: MovableEstates/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovableEstates movableEstates = db.MovableEstate.Find(id);
            if (movableEstates == null)
            {
                return HttpNotFound();
            }
            return View(movableEstates);
        }

        // GET: MovableEstates/Create
        public ActionResult Create()
        {

            long ApplicationID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];


            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);

   if (ApplicationID == 0)
                return RedirectToAction("");


            MovableEstates movableEstates = new MovableEstates();

            movableEstates.applicationId = ApplicationID;

         

           
            ViewBag.MovableEstateTypeId = new SelectList(db.MovableEstateTypes, "Id", "Title");
            return View(movableEstates);
        }

        // POST: MovableEstates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovableEstates movableEstates)
        {
            if (ModelState.IsValid)
            {
                db.MovableEstate.Add(movableEstates);
                db.SaveChanges();
                return RedirectToAction("", new { ApplicationID = movableEstates.applicationId });
            }

            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", movableEstates.applicationId);
            ViewBag.MovableEstateTypeId = new SelectList(db.MovableEstateTypes, "Id", "Title", movableEstates.MovableEstateTypeId);
            return View(movableEstates);
        }

        // GET: MovableEstates/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovableEstates movableEstates = db.MovableEstate.Find(id);
            if (movableEstates == null)
            {
                return HttpNotFound();
            }
           
            ViewBag.MovableEstateTypeId = new SelectList(db.MovableEstateTypes, "Id", "Title", movableEstates.MovableEstateTypeId);
            return View(movableEstates);
        }

        // POST: MovableEstates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( MovableEstates movableEstates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movableEstates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("", new { ApplicationID = movableEstates.applicationId });
            }
            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", movableEstates.applicationId);
            ViewBag.MovableEstateTypeId = new SelectList(db.MovableEstateTypes, "Id", "Title", movableEstates.MovableEstateTypeId);
            return View(movableEstates);
        }

        // GET: MovableEstates/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovableEstates movableEstates = db.MovableEstate.Find(id);
            if (movableEstates == null)
            {
                return HttpNotFound();
            }
            return View(movableEstates);
        }

        // POST: MovableEstates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MovableEstates movableEstates = db.MovableEstate.Find(id);
            db.MovableEstate.Remove(movableEstates);
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
