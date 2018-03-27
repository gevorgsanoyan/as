using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;
using ASFront.ModelsView;

namespace ASFront.Controllers
{
    public class AgroAssetIncomeNormativesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AgroAssetIncomeNormatives
        public ActionResult Index()
        {
            //var agroAssetIncomeNormative = db.AgroAssetIncomeNormative.Include(a => a.AgroAssetTypes).Include(a => a.Branches);
            //return View(agroAssetIncomeNormative.ToList());

            AgroAssetIncomeNormativesIntroGroupViewModel item = new AgroAssetIncomeNormativesIntroGroupViewModel();

            ViewBag.AgroAssetTypesId = new SelectList(db.AgroAssetTypes, "Id", "Name");
            ViewBag.BrancheId = new SelectList(db.Branches, "Id", "Branch");

            return View(item);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AgroAssetIncomeNormativesIntroGroupViewModel item)
        {
            if (ModelState.IsValid)
            {
                if (db.AgroAssetIncomeNormative.Any(p => (p.BrancheId == item.BrancheId && p.AgroAssetTypesId == p.AgroAssetTypesId)))
                {
                    return RedirectToAction("Edit", new { BrancheId = item.BrancheId, AgroAssetTypeId = item.AgroAssetTypesId });
                }
                else
                {
                    return RedirectToAction("Create", new { BrancheId = item.BrancheId, AgroAssetTypeId = item.AgroAssetTypesId });
                }


            }

            ViewBag.AgroAssetTypesId = new SelectList(db.AgroAssetTypes, "Id", "Name", item.AgroAssetTypesId);
            ViewBag.BrancheId = new SelectList(db.Branches, "Id", "Branch", item.BrancheId);
            return View(item);
        }



        // GET: AgroAssetIncomeNormatives/Create
        public ActionResult Create()
        {



            int BrancheId = 0; int AgroAssetTypeId = 0;




            string AgroAssetTypeIdStr = Request.QueryString["AgroAssetTypeId"];
            string BrancheIdStr = Request.QueryString["BrancheId"];

            if (!string.IsNullOrWhiteSpace(AgroAssetTypeIdStr))
                Int32.TryParse(AgroAssetTypeIdStr, out AgroAssetTypeId);




            if (!string.IsNullOrWhiteSpace(BrancheIdStr))
                Int32.TryParse(BrancheIdStr, out BrancheId);


            ViewBag.Branche = db.Branches.Where(p => p.Id == BrancheId).Select(p => p.Branch).FirstOrDefault() ?? string.Empty;
            ViewBag.AgroAssetType = db.AgroAssetTypes.Where(p => p.Id == AgroAssetTypeId).Select(p => p.Name).FirstOrDefault() ?? string.Empty;

            AgroAssetIncomeNormativesListViewModel item = new AgroAssetIncomeNormativesListViewModel(BrancheId, AgroAssetTypeId);

            return View(item);
        }

        // POST: AgroAssetIncomeNormatives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AgroAssetIncomeNormativesListViewModel agroAssetIncomeNormative)
        {
            if (ModelState.IsValid)
            {


                foreach (var it in agroAssetIncomeNormative.AgroNormatives)
                {
                    db.AgroAssetIncomeNormative.Add(it);

                }


                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.Branche = db.Branches.Where(p => p.Id == agroAssetIncomeNormative.BrancheId).Select(p => p.Branch).FirstOrDefault() ?? string.Empty;
            ViewBag.AgroAssetType = db.AgroAssetTypes.Where(p => p.Id == agroAssetIncomeNormative.AgroAssetTypesId).Select(p => p.Name).FirstOrDefault() ?? string.Empty;

            return View(agroAssetIncomeNormative);
        }

        // GET: AgroAssetIncomeNormatives/Edit/5
        public ActionResult Edit()
        {


            int BrancheId = 0; int AgroAssetTypeId = 0;




            string AgroAssetTypeIdStr = Request.QueryString["AgroAssetTypeId"];
            string BrancheIdStr = Request.QueryString["BrancheId"];

            if (!string.IsNullOrWhiteSpace(AgroAssetTypeIdStr))
                Int32.TryParse(AgroAssetTypeIdStr, out AgroAssetTypeId);




            if (!string.IsNullOrWhiteSpace(BrancheIdStr))
                Int32.TryParse(BrancheIdStr, out BrancheId);




            AgroAssetIncomeNormativesListViewModel item = new AgroAssetIncomeNormativesListViewModel(BrancheId, AgroAssetTypeId);

            if (item == null)
                return RedirectToAction("Create", new { BrancheId = item.BrancheId, AgroAssetTypeId = item.AgroAssetTypesId });


            ViewBag.Branche = db.Branches.Where(p => p.Id == BrancheId).Select(p => p.Branch).FirstOrDefault() ?? string.Empty;
            ViewBag.AgroAssetType = db.AgroAssetTypes.Where(p => p.Id == AgroAssetTypeId).Select(p => p.Name).FirstOrDefault() ?? string.Empty;

            return View(item);
        }

        // POST: AgroAssetIncomeNormatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AgroAssetIncomeNormativesListViewModel agroAssetIncomeNormative)
        {
            if (ModelState.IsValid)
            {
                List<int> agrList = new List<int>();
                List<AgroAssetIncomeNormative> agrDelList = new List<AgroAssetIncomeNormative>();



                foreach (var it in agroAssetIncomeNormative.AgroNormatives)
                {
                    if (it.Id > 0)
                    {
                        db.Entry(it).State = EntityState.Modified;
                        agrList.Add(it.Id);

                    }

                    else
                        db.AgroAssetIncomeNormative.Add(it);
                }



                List<AgroAssetIncomeNormative> normList = db.AgroAssetIncomeNormative.Where(p => (p.BrancheId == agroAssetIncomeNormative.BrancheId && p.AgroAssetTypesId == agroAssetIncomeNormative.AgroAssetTypesId)).ToList();



                agrDelList = normList.Where(p => !agrList.Contains(p.Id)).ToList();
                db.AgroAssetIncomeNormative.RemoveRange(agrDelList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.Branche = db.Branches.Where(p => p.Id == agroAssetIncomeNormative.BrancheId).Select(p => p.Branch).FirstOrDefault() ?? string.Empty;
            ViewBag.AgroAssetType = db.AgroAssetTypes.Where(p => p.Id == agroAssetIncomeNormative.AgroAssetTypesId).Select(p => p.Name).FirstOrDefault() ?? string.Empty;

            return View(agroAssetIncomeNormative);
        }

        // GET: AgroAssetIncomeNormatives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgroAssetIncomeNormative agroAssetIncomeNormative = db.AgroAssetIncomeNormative.Find(id);
            if (agroAssetIncomeNormative == null)
            {
                return HttpNotFound();
            }
            return View(agroAssetIncomeNormative);
        }

        // POST: AgroAssetIncomeNormatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgroAssetIncomeNormative agroAssetIncomeNormative = db.AgroAssetIncomeNormative.Find(id);
            db.AgroAssetIncomeNormative.Remove(agroAssetIncomeNormative);
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
