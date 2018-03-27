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
    public class GoldCollateralsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GoldCollaterals
        public ActionResult Index()
        {
            long ApplicationID = 0;
            long GoldCollateralsID = 0;
            long ClientID = 0;


            string ApplicationIDStr = Request.QueryString["ApplicationID"];
            string ClientIDStr = Request.QueryString["ClientID"];
            string GoldCollateralsIDStr = Request.QueryString["GoldCollateralsID"];
            string EditMode = Request.QueryString["EditMode"] ?? "";


            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);







            if (!string.IsNullOrWhiteSpace(ClientIDStr))
                Int64.TryParse(ClientIDStr, out ClientID);



            if (!string.IsNullOrWhiteSpace(GoldCollateralsIDStr))
                Int64.TryParse(GoldCollateralsIDStr, out GoldCollateralsID);







            var goldCollateral = db.GoldCollateral.Include(g => g.applications).Include(g => g.GoldAssayes).Include(g => g.GoldTypes).Where(p => (p.applicationId == ApplicationID)).ToList() ?? new List<GoldCollaterals>();



           




          


           ViewBag.AssayStamp = new SelectList(
    new List<SelectListItem>
    {
       
        new SelectListItem { Selected = false, Text = "Այո", Value = (true).ToString()},
        new SelectListItem { Selected = false, Text = "Ոչ", Value = (false).ToString()},
    }, "Value", "Text", 1);




            GoldCollaterals InputForm = new GoldCollaterals();
            InputForm.applicationId = ApplicationID;

            if (GoldCollateralsID > 0)
                InputForm = db.GoldCollateral.Find(GoldCollateralsID);


            if (EditMode == "Copy")
            {
                GoldCollaterals CopyForm = InputForm.ShallowCopy();

                InputForm = CopyForm;
            }


            ViewBag.GoldAssayId = new SelectList(db.GoldAssayes, "Id", "Assay", InputForm.GoldAssayId);
            ViewBag.GoldTypeId = new SelectList(db.GoldTypes, "Id", "Title", InputForm.GoldTypeId);

            ViewBag.ApplicationID = ApplicationID;



            ViewBag.EditMode = EditMode;

            GoldCollateralViewModels gm = new GoldCollateralViewModels();

            gm.InputForm = InputForm;
            gm.Table = goldCollateral;

            return View(gm);
        }

        // GET: GoldCollaterals/Details/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(GoldCollaterals goldCollaterals, string EditMode)
        {






            if (ModelState.IsValid)
            {
                if (goldCollaterals.NetWeight > goldCollaterals.TotalWeightWithJewels)
                {
                    goldCollaterals.NetWeight = goldCollaterals.TotalWeightWithJewels;
                }
                    

                double EstimatedUnitCostAMD = db.GoldPrices.Where(p => p.GoldAssayId == goldCollaterals.GoldAssayId).OrderByDescending(p => p.Date).Select(p => p.Price).FirstOrDefault();
                goldCollaterals.EstimatedUnitCostAMD = EstimatedUnitCostAMD;

                goldCollaterals.EstimatedTotalCostAMD = goldCollaterals.EstimatedUnitCostAMD * goldCollaterals.NetWeight;

                if (goldCollaterals.Id == 0)
                {
                    db.GoldCollateral.Add(goldCollaterals);



                    db.SaveChanges();
                    return RedirectToAction("", new { ApplicationID = goldCollaterals.applicationId });

                }
                else

                {

                    db.Entry(goldCollaterals).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("", new { ApplicationID = goldCollaterals.applicationId });
                }







            }





            var goldCollateral = db.GoldCollateral.Include(g => g.applications).Include(g => g.GoldAssayes).Include(g => g.GoldTypes).Where(p => (p.applicationId == goldCollaterals.applicationId)).ToList() ?? new List<GoldCollaterals>();




            List<SelectListItem> AssayStamp = new List<SelectListItem>();

            AssayStamp.Add(new SelectListItem { Text = "Այո", Value = "1" });
            AssayStamp.Add(new SelectListItem { Text = "Ոչ", Value = "0" });







            ViewBag.AssayStamp = AssayStamp;


            GoldCollaterals InputForm = goldCollaterals;




            ViewBag.GoldAssayId = new SelectList(db.GoldAssayes, "Id", "Assay", InputForm.GoldAssayId);
            ViewBag.GoldTypeId = new SelectList(db.GoldTypes, "Id", "Title", InputForm.GoldTypeId);

            ViewBag.ApplicationID = goldCollaterals.applicationId;



            ViewBag.EditMode = EditMode;

            GoldCollateralViewModels gm = new GoldCollateralViewModels();

            gm.InputForm = InputForm;
            gm.Table = goldCollateral;

            return View(gm);
        }




        // GET: GoldCollaterals/Create
        public ActionResult Create()
        {
            long ApplicationID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];


            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);


            if (ApplicationID == 0)
                return RedirectToAction("");







            return RedirectToAction("", new { ApplicationID = ApplicationID });





            //GoldCollaterals goldCollaterals = new GoldCollaterals();
            //goldCollaterals.applicationId=ApplicationID;

            //ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId");
            //ViewBag.GoldAssayId = new SelectList(db.GoldAssayes, "Id", "Assay");
            //ViewBag.GoldTypeId = new SelectList(db.GoldTypes, "Id", "Title");
            //return View(goldCollaterals);
        }

        // POST: GoldCollaterals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GoldCollaterals goldCollaterals)
        {
            if (ModelState.IsValid)
            {
                if (goldCollaterals.NetWeight > goldCollaterals.TotalWeightWithJewels)
                {
                    goldCollaterals.NetWeight = goldCollaterals.TotalWeightWithJewels;
                }

                db.GoldCollateral.Add(goldCollaterals);

                goldCollaterals.EstimatedTotalCostAMD = goldCollaterals.EstimatedUnitCostAMD * goldCollaterals.NetWeight;

                db.SaveChanges();
                return RedirectToAction("", new { ApplicationID = goldCollaterals.applicationId });
            }


            ViewBag.GoldAssayId = new SelectList(db.GoldAssayes, "Id", "Assay", goldCollaterals.GoldAssayId);
            ViewBag.GoldTypeId = new SelectList(db.GoldTypes, "Id", "Title", goldCollaterals.GoldTypeId);
            return View(goldCollaterals);
        }

        // GET: GoldCollaterals/Edit/5
        public ActionResult Edit(long id = 0)
        {

            if (id == 0)
                return RedirectToAction("");

            GoldCollaterals goldCollaterals = db.GoldCollateral.Find(id);

            if (goldCollaterals.Id == 0)
                return RedirectToAction("", new { ApplicationID = goldCollaterals.applicationId });


            return RedirectToAction("", new { ApplicationID = goldCollaterals.applicationId, GoldCollateralsID = id, EditMode = "Edit" });


            //ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", goldCollaterals.applicationId);
            //ViewBag.GoldAssayId = new SelectList(db.GoldAssayes, "Id", "Assay", goldCollaterals.GoldAssayId);
            //ViewBag.GoldTypeId = new SelectList(db.GoldTypes, "Id", "Title", goldCollaterals.GoldTypeId);
            //return View(goldCollaterals);
        }



        public ActionResult Copy(long id = 0)
        {

            if (id == 0)
                return RedirectToAction("");

            GoldCollaterals goldCollaterals = db.GoldCollateral.Find(id);

            if (goldCollaterals.Id == 0)
                return RedirectToAction("", new { ApplicationID = goldCollaterals.applicationId });





            return RedirectToAction("", new { ApplicationID = goldCollaterals.applicationId, GoldCollateralsID = id, EditMode = "Copy" });


            //ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", goldCollaterals.applicationId);
            //ViewBag.GoldAssayId = new SelectList(db.GoldAssayes, "Id", "Assay", goldCollaterals.GoldAssayId);
            //ViewBag.GoldTypeId = new SelectList(db.GoldTypes, "Id", "Title", goldCollaterals.GoldTypeId);
            //return View(goldCollaterals);
        }



        // POST: GoldCollaterals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GoldCollaterals goldCollaterals, string EditMode)
        {
            if (ModelState.IsValid)
            {
                if (goldCollaterals.NetWeight > goldCollaterals.TotalWeightWithJewels)
                {
                    goldCollaterals.NetWeight = goldCollaterals.TotalWeightWithJewels;
                }

                db.Entry(goldCollaterals).State = EntityState.Modified;
                goldCollaterals.EstimatedTotalCostAMD = goldCollaterals.EstimatedUnitCostAMD * goldCollaterals.NetWeight;
                db.SaveChanges();
                return RedirectToAction("", new { ApplicationID = goldCollaterals.applicationId });
            }


            return RedirectToAction("", new { ApplicationID = goldCollaterals.applicationId, GoldCollateralsID = goldCollaterals.Id, EditMode = EditMode });

            //ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", goldCollaterals.applicationId);
            //ViewBag.GoldAssayId = new SelectList(db.GoldAssayes, "Id", "Assay", goldCollaterals.GoldAssayId);
            //ViewBag.GoldTypeId = new SelectList(db.GoldTypes, "Id", "Title", goldCollaterals.GoldTypeId);
            //return View(goldCollaterals);
        }

        // GET: GoldCollaterals/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldCollaterals goldCollaterals = db.GoldCollateral.Find(id);
            if (goldCollaterals == null)
            {
                return HttpNotFound();
            }
            return View(goldCollaterals);
        }

        // POST: GoldCollaterals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            GoldCollaterals goldCollaterals = db.GoldCollateral.Find(id);
            db.GoldCollateral.Remove(goldCollaterals);
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
