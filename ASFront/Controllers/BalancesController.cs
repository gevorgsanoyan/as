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
    public class BalancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        [HttpPost]
        public ActionResult CalculateShortTermLoans(long ApplicationID)
        {

            CommonFunction.CalculateShortTermLoans(ApplicationID);

            Balance balance = db.Balance.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();


            return PartialView("~/views/Balances/Balance/_four_one.cshtml", balance);

        }


        [HttpPost]
        public ActionResult CalculateInventory(long ApplicationID)
        {

            CommonFunction.CalculateInventory(ApplicationID);

            Balance balance = db.Balance.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();


            return PartialView("~/views/Balances/Balance/_two_four.cshtml", balance);

        }



        [HttpPost]
        public ActionResult CalculateMediumLongTermLoans(long ApplicationID)
        {

            CommonFunction.CalculateMediumLongTermLoans(ApplicationID);

            Balance balance = db.Balance.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();


            return PartialView("~/views/Balances/Balance/_five _one.cshtml", balance);

        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Balance balance, long ApplicationID, bool Edit)
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
                //    return RedirectToAction("Edit", "Clients", new { ClientID = balance.clientId });
                //}




            }

            if (ModelState.IsValid)

            {

                if (balance.Id > 0 && db.Balance.Any(p => p.Id == balance.Id))
                {
                    db.Entry(balance).State = EntityState.Modified;

                }
                else
                {
                    db.Balance.Add(balance);
                }



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
                  
                //    return RedirectToAction("Edit", "Clients", new { ClientID = balance.clientId });
                //}


            }
            else
            {
                ViewBag.ErrorText = Resources.Messages.RequiredFields;
            }



            ViewBag.ApplicationID = ApplicationID;
            ViewBag.Edit = Edit;
            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", balance.clientId);
            return View(balance);
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

            if (ApplicationID == 0)
                return RedirectToAction("", "Application");

            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName");


            Balance balance = new Balance();
            balance = db.Balance.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();

            string EditStr = Request.QueryString["Edit"];
            bool EditBool = false;

            if (EditStr == true.ToString())
                EditBool = true;

            if (balance == null)
            {
                balance = new Balance();
                balance.clientId = ClientID;
                balance.applicationId = ApplicationID;
                db.Balance.Add(balance);
                db.SaveChanges();

                db = new ApplicationDbContext();
                balance = db.Balance.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();
            }
            @ViewBag.ApplicationID = ApplicationID;
            @ViewBag.Edit = EditBool;

            return View(balance);
        }

        // GET: Balances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Balance balance = db.Balance.Find(id);
            if (balance == null)
            {
                return HttpNotFound();
            }
            return View(balance);
        }

        // GET: Balances/Create
        public ActionResult Create()
        {
            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName");
            return View();
        }

        // POST: Balances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,clientId,PreparationDate,note1,note2,note3,Cash,BankAccount,AccountReceivable,Inventory,OtherCurrentAssets,Equipments,Vehicles,PropertyPlantes,OtherNonCurrentAssets,ShortTermLoans,AccountPayable,OtherCurrentLiabilities,MediumLongTermLoans,OtherNonCurrentLiabilitiues")] Balance balance)
        {
            if (ModelState.IsValid)
            {
                db.Balance.Add(balance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", balance.clientId);
            return View(balance);
        }

        // GET: Balances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Balance balance = db.Balance.Find(id);
            if (balance == null)
            {
                return HttpNotFound();
            }
            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", balance.clientId);
            return View(balance);
        }

        // POST: Balances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,clientId,PreparationDate,note1,note2,note3,Cash,BankAccount,AccountReceivable,Inventory,OtherCurrentAssets,Equipments,Vehicles,PropertyPlantes,OtherNonCurrentAssets,ShortTermLoans,AccountPayable,OtherCurrentLiabilities,MediumLongTermLoans,OtherNonCurrentLiabilitiues")] Balance balance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(balance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", balance.clientId);
            return View(balance);
        }

        // GET: Balances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Balance balance = db.Balance.Find(id);
            if (balance == null)
            {
                return HttpNotFound();
            }
            return View(balance);
        }

        // POST: Balances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Balance balance = db.Balance.Find(id);
            db.Balance.Remove(balance);
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
