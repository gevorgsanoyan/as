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
    public class BusinessInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BusinessInfoes
        public ActionResult Index()
        {
            var businessInfo = db.BusinessInfo.Include(b => b.BusinessSector).Include(b => b.BusinessType).Include(b => b.clients).Include(b => b.clientSexes).Include(b => b.NameofBanks).Include(b => b.OwnershipType);
            return View(businessInfo.ToList());
        }

        // GET: BusinessInfoes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessInfo businessInfo = db.BusinessInfo.Find(id);
            if (businessInfo == null)
            {
                return HttpNotFound();
            }
            return View(businessInfo);
        }

        // GET: BusinessInfoes/Create
        public ActionResult Create()
        {
            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name");
            ViewBag.BusinessTypeId = new SelectList(db.BusinessType, "Id", "Name");
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName");
            ViewBag.GenderId = new SelectList(db.clientSexes, "clientSexId", "sex");
            ViewBag.NameofBanksId = new SelectList(db.NameofBanks, "Id", "Name");
            ViewBag.OwnershipTypeId = new SelectList(db.OwnershipType, "Id", "Name");
            return View();
        }

        // POST: BusinessInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clientId,BusinessSectorId,BusinessTypeId,OwnershipTypeId,NameofBanksId,GenderId,businessDescription,NumberofMonths,BankAccount,FirstName,LastName,MidName,DayofBirth,SocialNumber,tel,Mobile,DirEmail,note1,note2")] BusinessInfo businessInfo)
        {
            if (ModelState.IsValid)
            {
                db.BusinessInfo.Add(businessInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name", businessInfo.BusinessSectorId);
            ViewBag.BusinessTypeId = new SelectList(db.BusinessType, "Id", "Name", businessInfo.BusinessTypeId);
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", businessInfo.clientId);
            ViewBag.GenderId = new SelectList(db.clientSexes, "clientSexId", "sex", businessInfo.GenderId);
            ViewBag.NameofBanksId = new SelectList(db.NameofBanks, "Id", "Name", businessInfo.NameofBanksId);
            ViewBag.OwnershipTypeId = new SelectList(db.OwnershipType, "Id", "Name", businessInfo.OwnershipTypeId);
            return View(businessInfo);
        }

        // GET: BusinessInfoes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessInfo businessInfo = db.BusinessInfo.Find(id);
            if (businessInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name", businessInfo.BusinessSectorId);
            ViewBag.BusinessTypeId = new SelectList(db.BusinessType, "Id", "Name", businessInfo.BusinessTypeId);
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", businessInfo.clientId);
            ViewBag.GenderId = new SelectList(db.clientSexes, "clientSexId", "sex", businessInfo.GenderId);
            ViewBag.NameofBanksId = new SelectList(db.NameofBanks, "Id", "Name", businessInfo.NameofBanksId);
            ViewBag.OwnershipTypeId = new SelectList(db.OwnershipType, "Id", "Name", businessInfo.OwnershipTypeId);
            return View(businessInfo);
        }

        // POST: BusinessInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clientId,BusinessSectorId,BusinessTypeId,OwnershipTypeId,NameofBanksId,GenderId,businessDescription,NumberofMonths,BankAccount,FirstName,LastName,MidName,DayofBirth,SocialNumber,tel,Mobile,DirEmail,note1,note2")] BusinessInfo businessInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name", businessInfo.BusinessSectorId);
            ViewBag.BusinessTypeId = new SelectList(db.BusinessType, "Id", "Name", businessInfo.BusinessTypeId);
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", businessInfo.clientId);
            ViewBag.GenderId = new SelectList(db.clientSexes, "clientSexId", "sex", businessInfo.GenderId);
            ViewBag.NameofBanksId = new SelectList(db.NameofBanks, "Id", "Name", businessInfo.NameofBanksId);
            ViewBag.OwnershipTypeId = new SelectList(db.OwnershipType, "Id", "Name", businessInfo.OwnershipTypeId);
            return View(businessInfo);
        }

        // GET: BusinessInfoes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessInfo businessInfo = db.BusinessInfo.Find(id);
            if (businessInfo == null)
            {
                return HttpNotFound();
            }
            return View(businessInfo);
        }

        // POST: BusinessInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BusinessInfo businessInfo = db.BusinessInfo.Find(id);
            db.BusinessInfo.Remove(businessInfo);
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
