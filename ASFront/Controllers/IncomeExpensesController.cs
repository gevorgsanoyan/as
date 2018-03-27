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

namespace ASFront.Controllers
{
    public class IncomeExpensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();




        [HttpPost]
        public ActionResult CalculateFamilyCost(long ApplicationID)
        {

            CommonFunction.CalculateFamilyCost(ApplicationID);

            IncomeExpenses incomeExpenses = db.IncomeExpenses.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();


            return PartialView("~/views/IncomeExpenses/IncomeExpenses/_four.cshtml", incomeExpenses);

        }


        [HttpPost]
        public ActionResult CalculateAgro(long ApplicationID)
        {

            CommonFunction.CalculateAgriculturalData(ApplicationID);

            IncomeExpenses incomeExpenses = db.IncomeExpenses.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();


            return PartialView("~/views/IncomeExpenses/IncomeExpenses/_three.cshtml", incomeExpenses);

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IncomeExpenses incomeExpenses, long ApplicationID, bool Edit)
        {
         

            if (Request.Form["Cancel"] != null && Request.Form["Cancel"].Equals(Resources.Page.Cancel))
            {


                //if (ApplicationID > 0)
                {
                    if (!Edit)
                    {
                        return RedirectToAction("ApplicationSummary", "Application", new { ApplicationID = ApplicationID });
                    }
                    else
                    {
                        return RedirectToAction($"Edit/{ApplicationID}", "Application");
                    }
                }
                //else
                //{
                //    return RedirectToAction("Edit", "Clients", new { ClientID = incomeExpenses.clientId });
                //}




            }

            if (ModelState.IsValid)

            {

                //if (incomeExpenses.Id > 0 && db.IncomeExpenses.Any(p => p.Id == incomeExpenses.Id))
                //{
                //    db.Entry(incomeExpenses).State = EntityState.Modified;

                //}
                //else
                //{
                //    db.IncomeExpenses.Add(incomeExpenses);
                //}


                db.Entry(incomeExpenses).State = EntityState.Modified;
                ViewBag.cSaved = Resources.Messages.Saved;


                db.SaveChanges();



                //if (ApplicationID > 0)
                {
                    if (!Edit)
                    {
                        return RedirectToAction("ApplicationSummary", "Application", new { ApplicationID = ApplicationID });
                    }
                    else
                    {
                        return RedirectToAction($"Edit/{ApplicationID}", "Application");
                    }
                }
                //else
                //{

                //    return RedirectToAction("Edit", "Clients", new { ClientID = incomeExpenses.clientId });
                //}


            }
            else
            {
                ViewBag.ErrorText = Resources.Messages.RequiredFields;
            }



            ViewBag.ApplicationID = ApplicationID;
            ViewBag.Edit = Edit;
            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", incomeExpenses.clientId);
            return View(incomeExpenses);
        }


        // GET: Balances
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

               if(ApplicationID == 0)
                return RedirectToAction("", "ApplicationID");



            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName");


            IncomeExpenses incomeExpenses = new IncomeExpenses();
            incomeExpenses = db.IncomeExpenses.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();

            string EditStr = Request.QueryString["Edit"];
            bool EditBool = false;

            if (EditStr == true.ToString())
                EditBool = true;

            if (incomeExpenses == null)
            {
                incomeExpenses = new IncomeExpenses();
                incomeExpenses.clientId = ClientID;
                incomeExpenses.applicationId = ApplicationID;
                db.IncomeExpenses.Add(incomeExpenses);
                db.SaveChanges();

                CommonFunction.CalculateIncomeExpenses(ApplicationID);
                // Redirect(Request.UrlReferrer.ToString());
                db = new ApplicationDbContext();
                incomeExpenses = db.IncomeExpenses.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();
               
            }
            else
            {
                CommonFunction.CalculateIncomeExpenses(ApplicationID);
            }//if (incomeExpenses == null)
            ViewBag.ApplicationID = ApplicationID;
            ViewBag.Edit = EditBool;

            return View(incomeExpenses);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(IncomeExpenses incomeExpenses, long applicationId)
        {
            if (ModelState.IsValid)
            {

                if (incomeExpenses.Id > 0 && db.IncomeExpenses.Any(p => p.Id == incomeExpenses.Id))
                {
                    db.Entry(incomeExpenses).State = EntityState.Modified;

                }
                else
                {
                    db.IncomeExpenses.Add(incomeExpenses);
                }



                ViewBag.cSaved = Resources.Messages.Saved;


                db.SaveChanges();

            }
            else
            {
                ViewBag.ErrorText = Resources.Messages.RequiredFields;
            }

            //long ApplicationIDLong = 0;

            //Int64.TryParse(ApplicationID.ToString(), out ApplicationIDLong);

            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", incomeExpenses.clientId);
            return RedirectToAction("ApplicationSummary", "Application", new { ApplicationID = applicationId });
        }


     

        // GET: IncomeExpenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeExpenses incomeExpenses = db.IncomeExpenses.Find(id);
            if (incomeExpenses == null)
            {
                return HttpNotFound();
            }
            return View(incomeExpenses);
        }

        // GET: IncomeExpenses/Create
        public ActionResult Create()
        {
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName");
            return View();
        }

        // POST: IncomeExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clientId,ClientsWage,LoanInterestExpenses,FamilysLoanInterestExpenses,note1,note2,note3,Revenue,CostOfGoodsSold,Rent,TransportationCosts,UtilityExpenses,Wage,OtherExpenses,Taxes,AgroIncome,AgroExpenses,FamilyMembersWages,OtherFamilyIncome,FamilyExpenses")] IncomeExpenses incomeExpenses)
        {
            if (ModelState.IsValid)
            {
                db.IncomeExpenses.Add(incomeExpenses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", incomeExpenses.clientId);
            return View(incomeExpenses);
        }

        // GET: IncomeExpenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeExpenses incomeExpenses = db.IncomeExpenses.Find(id);
            if (incomeExpenses == null)
            {
                return HttpNotFound();
            }
            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", incomeExpenses.clientId);
            return View(incomeExpenses);
        }

        // POST: IncomeExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clientId,ClientsWage,LoanInterestExpenses,FamilysLoanInterestExpenses,note1,note2,note3,Revenue,CostOfGoodsSold,Rent,TransportationCosts,UtilityExpenses,Wage,OtherExpenses,Taxes,AgroIncome,AgroExpenses,FamilyMembersWages,OtherFamilyIncome,FamilyExpenses")] IncomeExpenses incomeExpenses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incomeExpenses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", incomeExpenses.clientId);
            return View(incomeExpenses);
        }

        // GET: IncomeExpenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeExpenses incomeExpenses = db.IncomeExpenses.Find(id);
            if (incomeExpenses == null)
            {
                return HttpNotFound();
            }
            return View(incomeExpenses);
        }

        // POST: IncomeExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncomeExpenses incomeExpenses = db.IncomeExpenses.Find(id);
            db.IncomeExpenses.Remove(incomeExpenses);
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
