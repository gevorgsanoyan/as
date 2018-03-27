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
    public class TurnoversController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Turnovers
        public ActionResult Index()
        {

            string EditMode = Request.QueryString["EditMode"] ?? "";

            long ApplicationID = 0;
            long TurnoversID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];


            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);



            long ClientID = 0;
            string ClientIDStr = Request.QueryString["ClientID"];
            string TurnoversIDStr = Request.QueryString["TurnoversID"];


            if (!string.IsNullOrWhiteSpace(ClientIDStr))
                Int64.TryParse(ClientIDStr, out ClientID);



            if (!string.IsNullOrWhiteSpace(TurnoversIDStr))
                Int64.TryParse(TurnoversIDStr, out TurnoversID);



            var turnovers = db.Turnovers.Include(t => t.applications).Include(t => t.MeasurementUnits).Where(p => (p.applicationId == ApplicationID)).ToList() ?? new List<Turnovers>();



            Turnovers InputForm = new Turnovers();
            InputForm.applicationId = ApplicationID;

            if (TurnoversID > 0)
                InputForm = db.Turnovers.Find(TurnoversID);


            if (EditMode == "Copy")
            {
                Turnovers CopyForm = InputForm.ShallowCopy();

                InputForm = CopyForm;
            }




            ViewBag.EditMode = EditMode;
            ViewBag.ApplicationID = ApplicationID;
            ViewBag.MeasurementUnitId = new SelectList(db.MeasurementUnits, "Id", "Title", InputForm.MeasurementUnitId);


            TurnoverViewModels Tr = new TurnoverViewModels();

            Tr.InputForm = InputForm;
            Tr.Table = turnovers;

            return View(Tr);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Turnovers turnovers, string EditMode)
        {



            if (turnovers.COGS != 0)
            {
                turnovers.Overgrown = Math.Round((turnovers.SalesPrice / turnovers.COGS - 1) * 100);
                
            }
            else
            {
                turnovers.Overgrown = 0;
                
            }

            turnovers.Proceeds = turnovers.MonthlySalesQuantity * turnovers.SalesPrice;
            turnovers.MoneyBalance = Math.Round(turnovers.COGS * turnovers.OutstandingQuantity);


            if (ModelState.IsValid)
            {


                if (turnovers.Id == 0)
                {
                    db.Turnovers.Add(turnovers);



                    db.SaveChanges();
                    return RedirectToAction("", new { ApplicationID = turnovers.applicationId });

                }
                else

                {

                    db.Entry(turnovers).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("", new { ApplicationID = turnovers.applicationId });
                }







            }


            var turnoverL = db.Turnovers.Include(t => t.applications).Include(t => t.MeasurementUnits).Where(p => (p.applicationId == turnovers.applicationId)).ToList() ?? new List<Turnovers>();

            ViewBag.ApplicationID = turnovers.applicationId;
            ViewBag.MeasurementUnitId = new SelectList(db.MeasurementUnits, "Id", "Title", turnovers.MeasurementUnitId);


            Turnovers InputForm = turnovers;





            ViewBag.EditMode = EditMode;

            TurnoverViewModels Tr = new TurnoverViewModels();

            Tr.InputForm = InputForm;
            Tr.Table = turnoverL;

            return View(Tr);




        }



        public ActionResult Create()
        {
            long ApplicationID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];


            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);


            if (ApplicationID == 0)
                return RedirectToAction("");








            return RedirectToAction("", new { ApplicationID = ApplicationID });



        }


        public ActionResult Edit(long id = 0)
        {

            if (id == 0)
                return RedirectToAction("");

            Turnovers turnovers = db.Turnovers.Find(id);

            if (turnovers.Id == 0)
                return RedirectToAction("", new { ApplicationID = turnovers.applicationId });


            return RedirectToAction("", new { ApplicationID = turnovers.applicationId, TurnoversID = id, EditMode = "Edit" });



        }

        public ActionResult Copy(long id = 0)
        {

            if (id == 0)
                return RedirectToAction("");

            Turnovers turnovers = db.Turnovers.Find(id);

            if (turnovers.Id == 0)
                return RedirectToAction("", new { ApplicationID = turnovers.applicationId });





            return RedirectToAction("", new { ApplicationID = turnovers.applicationId, TurnoversID = id, EditMode = "Copy" });



        }



        // POST: Turnovers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Turnovers turnovers)
        {
            if (turnovers.COGS != 0)
            {
                turnovers.Overgrown = (turnovers.SalesPrice / turnovers.COGS - 1) * 100;
               
            }
            else
            {
                turnovers.Overgrown = 0;
                
            }

            turnovers.Proceeds = turnovers.MonthlySalesQuantity * turnovers.SalesPrice;
            turnovers.MoneyBalance = Math.Round(turnovers.COGS * turnovers.OutstandingQuantity);

            if (ModelState.IsValid)
            {


                db.Turnovers.Add(turnovers);
                db.SaveChanges();
                return RedirectToAction("", new { ApplicationID = turnovers.applicationId });
            }

            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", turnovers.applicationId);
            ViewBag.MeasurementUnitId = new SelectList(db.MeasurementUnits, "Id", "Title", turnovers.MeasurementUnitId);
            return View(turnovers);
        }


        // POST: Turnovers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Turnovers turnovers, string EditMode)
        {

            if (turnovers.COGS != 0)
            {
                turnovers.Overgrown = (turnovers.SalesPrice / turnovers.COGS - 1) * 100;
                
            }
            else
            {
                turnovers.Overgrown = 0;
                
            }

            turnovers.Proceeds = turnovers.MonthlySalesQuantity * turnovers.SalesPrice;
            turnovers.MoneyBalance = Math.Round(turnovers.COGS * turnovers.OutstandingQuantity);

            if (ModelState.IsValid)
            {
                db.Entry(turnovers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }




            return RedirectToAction("", new { ApplicationID = turnovers.applicationId, TurnoversID = turnovers.Id, EditMode = EditMode });
        }

        // GET: Turnovers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnovers turnovers = db.Turnovers.Find(id);
            if (turnovers == null)
            {
                return HttpNotFound();
            }
            return View(turnovers);
        }

        // POST: Turnovers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turnovers turnovers = db.Turnovers.Find(id);
            db.Turnovers.Remove(turnovers);
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
