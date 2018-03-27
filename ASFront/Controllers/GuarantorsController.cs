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
using ASFront.Classes;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;

namespace ASFront.Controllers
{
    public class GuarantorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Guarantors
        public ActionResult Index()
        {
            var guarantors = db.Guarantors.Include(g => g.applications).Include(g => g.clients);
            return View(guarantors.ToList());
        }

        // GET: Guarantors/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guarantors guarantors = db.Guarantors.Find(id);
            if (guarantors == null)
            {
                return HttpNotFound();
            }
            return View(guarantors);
        }




        public ActionResult GuarantorsInput(long ApplicationID)
        {


            string errMsg = TempData["errMsg"] as string;
            if (errMsg != null)
            {

                ViewBag.errMsg = errMsg;
            }

            string OtherGroupMemberMessage = TempData["OtherGroupMemberMessage"] as string;
            if (OtherGroupMemberMessage != null)
            {

                ViewBag.OtherGroupMemberMessage = OtherGroupMemberMessage;
            }



            ViewBag.errMsg = "";
            long clientId = 0;

            var app = db.applications.Where(p => p.applicationId == ApplicationID).FirstOrDefault();

            if (app != null)
            {
                clientId = app.clientId;

                string clientName = "";

                var cl = db.clients.Where(p => p.clientId == app.clientId).FirstOrDefault();
                if (cl != null)
                    clientName = $"{cl.clientName}  {cl.clientLastName}  {cl.clientMidName}";
                ViewBag.TableHeader = $"Հայտ №{app.applicationId }, Հաճախորդ - {clientName} ";

            }







            clientsGuarantorView gw = new ModelsView.clientsGuarantorView();


            GuarantorsTotalView cgTotal = new ModelsView.GuarantorsTotalView();

            cgTotal.gMmebers = new List<clientsGuarantorView>();
            cgTotal.SingleClientID = clientId;

            for (int i = 0; i < 10; i++)
                cgTotal.gMmebers.Add(gw);



            return View(cgTotal);
        }




        [HttpPost]
        public ActionResult ClientsGroupMembersInput(GuarantorsTotalView cgTotal)
        {
            string errMsg = "";

            clientsGuarantorView cn;
            List<clientsGuarantorView> cnList = new List<ModelsView.clientsGuarantorView>();
            cgTotal.gMmebers = new List<clientsGuarantorView>();

            for (int i = 0; i < 10; i++)
            {
                cn = new clientsGuarantorView();

                cn.rpFirstName = Request.Form["fName" + i.ToString()];
                cn.rpLastName = Request.Form["lName" + i.ToString()];
                cn.rpSoc = Request.Form["Soc" + i.ToString()];

                double Income = 0;
                if (Request.Form["Income" + i.ToString()].Length > 0)
                {
                    double.TryParse(Request.Form["Income" + i.ToString()], out Income);
                    cn.Income = Income;
                       
                }



                cnList.Add(cn);
                cgTotal.gMmebers.Add(cn);
            }





            string isExistErrMsg = "Նշված անձիք արդեն իսկ Երաշխավոր են նույն Հայտի համար` ";

            bool isMember = false;

            foreach (var c in cnList)
            {
                if (c.rpSoc.Length > 0   && c.Income > 0)
                {
                    Guarantors guarantor = new Guarantors();

                   

                    if (db.clients.Any(e => e.socNumb == c.rpSoc) )
                    {
                        var exClient = db.clients.Where(e => e.socNumb == c.rpSoc).FirstOrDefault();

                        guarantor.clientId = exClient.clientId;
                        guarantor.applicationId = cgTotal.ApplicationID;

                        if (!db.Guarantors.Any(p => p.clientId == exClient.clientId && p.applicationId == cgTotal.ApplicationID))
                        {
                            db.Guarantors.Add(guarantor);
                            db.SaveChanges();

                            clientWorkDatas cw = new clientWorkDatas();
                            cw = db.clientWorkDatas.Where(p => p.clientId == exClient.clientId).FirstOrDefault();
                            if (cw == null)
                            {
                                cw = new clientWorkDatas();
                                cw.clientId = exClient.clientId;
                                cw.salary = c.Income;

                                db.clientWorkDatas.Add(cw);
                                db.SaveChanges();
                            }
                            else
                            {
                                cw.salary = c.Income;
                                db.SaveChanges();
                            }
                            
                        }
                        else
                        {
                            isMember = true;


                            string clientFullName = db.clients.Where(cl => cl.clientId == exClient.clientId).Select(n => n.clientName + " " + n.clientLastName).SingleOrDefault();
                            isExistErrMsg += clientFullName;
                        }



                    }
                    else
                    {
                        if (c.rpSoc.Length == 10)
                        {
                            var t = CommonFunction.GetInfroFromSoc(c.rpSoc);
                            clients newClient = new clients();
                            newClient.clientName = c.rpFirstName;
                            newClient.clientLastName = c.rpLastName;
                            newClient.clientMidName = "_";
                            newClient.socNumb = c.rpSoc;
                            newClient.dob = t.Item2;
                            newClient.sex_clientSexId = t.Item1;
                            newClient.passpNumb = "1";
                            newClient.passpDate = DateTime.Now;
                            newClient.passpAuth = "001";
                            newClient.rRegion = "R";
                            newClient.rCity = "C";
                            newClient.rStreet = "S";
                            newClient.rBuilding = "B";
                            newClient.mob1 = "091000000";
                            newClient.edu_educationId = 1;
                            newClient.isSameAddress = false;
                            newClient.isRented = false;

                           




                            int BranchtId = 0;
                            int UserASProfileId = 0;
                            string UserId = User.Identity.GetUserId(); ;

                            UserASProfileId = db.UserASProfiles.Where(p => p.UserId == UserId).Select(p => p.UserASProfileId).FirstOrDefault();
                            BranchtId = db.BranchUsers.Where(p => p.UserASProfileId == UserASProfileId).Select(p => p.BrancheId).FirstOrDefault();

                            if (BranchtId == 0)
                                BranchtId = 1;

                            newClient.BranchtId = BranchtId;

                            try
                            {
                                db.clients.Add(newClient);
                                db.SaveChanges();
                                guarantor.clientId = newClient.clientId;
                                guarantor.applicationId = cgTotal.ApplicationID;

                                db.Guarantors.Add(guarantor);
                                db.SaveChanges();


                                clientWorkDatas cw = new clientWorkDatas();
                               
                              
                                 
                                    cw.clientId = newClient.clientId;
                                    cw.salary = c.Income;

                                    db.clientWorkDatas.Add(cw);
                                    db.SaveChanges();
                               


                            }
                            catch (DbEntityValidationException dbEx)
                            {
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                                    }
                                }
                            }





                        }
                        else
                        {
                            errMsg += "Սխալ մուտքագրված սոց.քարտի համար: # " + c.rpSoc + " # Խնդրում եմ մուտքագրել կրկին: ";
                            break;
                        }//soc check
                    }



                }//if(c.rpSoc.Length > 0)

                else if (c.rpSoc.Length > 0 && !(c.Income > 0))
                {
                   
                    errMsg += " Եկամուտը չպետք է լինի 0: " + " -  սոց.քարտի համար: # " + c.rpSoc + " # Խնդրում եմ մուտքագրել կրկին: ";
                }
            }



            if (errMsg.Length > 0)
            {
                if (isMember)
                {
                    ViewBag.OtherGroupMemberMessage = isExistErrMsg;
                    TempData["OtherGroupMemberMessage"] = isExistErrMsg;
                }

                ViewBag.errMsg = errMsg;

                TempData["errMsg"] = errMsg;


                return RedirectToAction("GuarantorsInput", "Guarantors", new { ApplicationID = cgTotal.ApplicationID });
            }
            else
            {

                if (!isMember)
                {
                    return RedirectToAction("ApplicationSummary", "Application", new { ApplicationID = cgTotal.ApplicationID });
                }
                else
                {
                    //ViewBag.errMsg = "";;
                    ViewBag.OtherGroupMemberMessage = isExistErrMsg;
                    //return View(cgTotal);
                    TempData["OtherGroupMemberMessage"] = isExistErrMsg;
                }

                return RedirectToAction("GuarantorsInput", "Guarantors", new { ApplicationID = cgTotal.ApplicationID });
            }

        }




        // GET: Guarantors/Create


        // POST: Guarantors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,applicationId,clientId")] Guarantors guarantors)
        {
            if (ModelState.IsValid)
            {
                db.Guarantors.Add(guarantors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", guarantors.applicationId);
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", guarantors.clientId);
            return View(guarantors);
        }

        // GET: Guarantors/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guarantors guarantors = db.Guarantors.Find(id);
            if (guarantors == null)
            {
                return HttpNotFound();
            }
            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", guarantors.applicationId);
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", guarantors.clientId);
            return View(guarantors);
        }

        // POST: Guarantors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,applicationId,clientId")] Guarantors guarantors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guarantors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.applicationId = new SelectList(db.applications, "applicationId", "userId", guarantors.applicationId);
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName", guarantors.clientId);
            return View(guarantors);
        }

        // GET: Guarantors/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guarantors guarantors = db.Guarantors.Find(id);
            if (guarantors == null)
            {
                return HttpNotFound();
            }
            return View(guarantors);
        }

        // POST: Guarantors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Guarantors guarantors = db.Guarantors.Find(id);
            db.Guarantors.Remove(guarantors);
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
