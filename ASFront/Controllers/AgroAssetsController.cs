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
    public class AgroAssetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AgroAssets
      

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


            List<AgroAsset> agroAsset = db.AgroAsset.Include(a => a.AgroAssetTypes).Where(p => (p.applicationId == ApplicationID)).ToList() ?? new List<AgroAsset>();

        


            return View(agroAsset.ToList());
        }



        // GET: AgroAssets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgroAsset agroAsset = db.AgroAsset.Find(id);
            if (agroAsset == null)
            {
                return HttpNotFound();
            }
            return View(agroAsset);
        }

        // GET: AgroAssets/Create
        public ActionResult Create()
        {

            long ApplicationID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];


            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);   

            @ViewBag.ApplicationID = ApplicationID;

            AgroAsset agroAsset = new AgroAsset();
            agroAsset.applicationId = ApplicationID;

            if (ApplicationID == 0)
                return RedirectToAction("", "Application");

            ViewBag.AgroAssetTypesId = new SelectList(db.AgroAssetTypes, "Id", "Name");
            return View(agroAsset);
        }

        // POST: AgroAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AgroAsset agroAsset)
        {
            if (ModelState.IsValid && agroAsset.AgroAssetTypesId>0)
            {
                db.AgroAsset.Add(agroAsset);
                db.SaveChanges();



                return RedirectToAction("", new { ApplicationID = agroAsset.applicationId });


            }
         
            ViewBag.AgroAssetTypesId = new SelectList(db.AgroAssetTypes, "Id", "Name", agroAsset.AgroAssetTypesId);
            return View(agroAsset);
        }

        // GET: AgroAssets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgroAsset agroAsset = db.AgroAsset.Find(id);
            if (agroAsset == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgroAssetTypesId = new SelectList(db.AgroAssetTypes, "Id", "Name", agroAsset.AgroAssetTypesId);
            return View(agroAsset);
        }

        // POST: AgroAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,applicationId,AgroAssetTypesId,Quantity,Description,note1,note2,note3,note4")] AgroAsset agroAsset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agroAsset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("", new { ApplicationID = agroAsset.applicationId });
            }
            ViewBag.AgroAssetTypesId = new SelectList(db.AgroAssetTypes, "Id", "Name", agroAsset.AgroAssetTypesId);
            return View(agroAsset);
        }

        // GET: AgroAssets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgroAsset agroAsset = db.AgroAsset.Find(id);
            if (agroAsset == null)
            {
                return HttpNotFound();
            }
            return View(agroAsset);
        }

        // POST: AgroAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgroAsset agroAsset = db.AgroAsset.Find(id);
            db.AgroAsset.Remove(agroAsset);
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
