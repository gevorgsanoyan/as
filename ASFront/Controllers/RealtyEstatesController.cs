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
    public class RealtyEstatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RealtyEstates
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





            @ViewBag.ApplicationID = ApplicationID;


            var realtyEstate = db.RealtyEstate.Include(r => r.applications).Include(r => r.RealtyTypes).Where(p => (p.applicationId == ApplicationID)).ToList() ?? new List<RealtyEstates>();
            return View(realtyEstate.ToList());
        }

        // GET: RealtyEstates/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyEstates realtyEstates = db.RealtyEstate.Find(id);
            if (realtyEstates == null)
            {
                return HttpNotFound();
            }
            return View(realtyEstates);
        }

        // GET: RealtyEstates/Create
        public ActionResult Create()
        {

            long ApplicationID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];


            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);




            RealtyEstates realtyEstate = new RealtyEstates();

            realtyEstate.applicationId = ApplicationID;

            if (ApplicationID == 0)
                return RedirectToAction("");


            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId");
            ViewBag.RealtyTypeId = new SelectList(db.RealtyTypes, "Id", "Title");
            return View(realtyEstate);
        }

        // POST: RealtyEstates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( RealtyEstates realtyEstates)
        {
            if (ModelState.IsValid)
            {
                db.RealtyEstate.Add(realtyEstates);
                db.SaveChanges();
                return RedirectToAction("", new { ApplicationID = realtyEstates.applicationId });
            }

            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", realtyEstates.applicationId);
            ViewBag.RealtyTypeId = new SelectList(db.RealtyTypes, "Id", "Title", realtyEstates.RealtyTypeId);
            return View(realtyEstates);
        }

        // GET: RealtyEstates/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyEstates realtyEstates = db.RealtyEstate.Find(id);
            if (realtyEstates == null)
            {
                return HttpNotFound();
            }
            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", realtyEstates.applicationId);
            ViewBag.RealtyTypeId = new SelectList(db.RealtyTypes, "Id", "Title", realtyEstates.RealtyTypeId);
            return View(realtyEstates);
        }

        // POST: RealtyEstates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( RealtyEstates realtyEstates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realtyEstates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("", new { ApplicationID = realtyEstates.applicationId });
            }
            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", realtyEstates.applicationId);
            ViewBag.RealtyTypeId = new SelectList(db.RealtyTypes, "Id", "Title", realtyEstates.RealtyTypeId);
            return View(realtyEstates);
        }

        // GET: RealtyEstates/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyEstates realtyEstates = db.RealtyEstate.Find(id);
            if (realtyEstates == null)
            {
                return HttpNotFound();
            }
            return View(realtyEstates);
        }

        // POST: RealtyEstates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RealtyEstates realtyEstates = db.RealtyEstate.Find(id);
            db.RealtyEstate.Remove(realtyEstates);
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
