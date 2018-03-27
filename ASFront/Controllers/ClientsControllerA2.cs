using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using PagedList.Mvc;
using PagedList;
using ASFront.Classes;

namespace ASFront.Controllers
{
    public class ClientsControllerA : Controller
    {
        ApplicationDbContext db;
        public ClientsControllerA()
        {
            db = new Models.ApplicationDbContext();
        }
        // GET: Clients
        [Authorize]
        public ActionResult Index(int page = 1)
        {
            var clients = db.clients.ToList();
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(clients.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string SearchStr, string searchBtn)
        {

            //string SearchStr = Request.Form["SearchStr"].Trim();
            //string searchBtn = Request.Form["searchBtn"].Trim();
            List<clients> clients = new List<clients>();


            SearchStr = SearchStr.Trim();

            if (searchBtn == "որոնել")
            {
                if (!string.IsNullOrWhiteSpace(SearchStr))
                {
                    clients = db.clients.Where(p => p.socNumb == SearchStr).ToList();
                }
                else
                {
                    clients = db.clients.ToList();
                }

            }
            else
            {
                ModelState.Clear();
                clients = db.clients.ToList();
            }
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = 1;






            return View(clients.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult Create()
        {
            //Models.client client = new client();
            Models.clientFullViewModel client = new Models.clientFullViewModel();

            var regs = (from r in db.comunities
                        let rRegion = r.reg
                        select new { rRegion }).Distinct().ToList();

            var cityes = (from c in db.comunities
                          let rCity = c.cName
                          select new { rCity }).Distinct().ToList();


            var Streets = (from s in db.Streets
                           let rStreet = s.Street
                           select new { rStreet }).Distinct().ToList();

            var cStreets = (from s in db.Streets
                            let rStreet = s.Street
                            select new { rStreet }).Distinct().ToList();

            ViewBag.rg = new SelectList(regs, "rRegion", "rRegion");
            ViewBag.ct = new SelectList(cityes, "rCity", "rCity");
            ViewBag.cct = new SelectList(cityes, "rCity", "rCity");

            var sex = (from s in db.clientSexes select new { s.clientSexId, s.sex }).ToList();
            var edu = (from e in db.educations select new { e.educationId, e.Education }).ToList();
            ViewBag.sx = new SelectList(sex, "clientSexId", "sex");//
            ViewBag.ed = new SelectList(edu, "educationId", "Education");//

            var empl = (from e in db.employmentTypes
                        select new { e.empTypeID, e.employment }).ToList();
            ViewBag.em = new SelectList(empl, "empTypeID", "employment");

            var cmpTyp = (from ct in db.companyTypes
                          select new { ct.companyTypeID, ct.companyTypeName });

            ViewBag.ctyp = new SelectList(cmpTyp, "companyTypeID", "companyTypeName");


            ViewBag.Streets = new SelectList(Streets, "rStreet", "rStreet");
            ViewBag.cStreets = new SelectList(cStreets, "rStreet", "rStreet");


            client.c = new clients();
            client.c.isSameAddress = true;

            return View(client);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Create(Models.clientFullViewModel client)
        {
             ViewBag.ErrorText = string.Empty;

            if (db.clients.Any(p => (p.passpNumb == client.c.passpNumb && p.clientId != client.c.clientId)))
            {
                ModelState.AddModelError("passpNumb", "Անձնագրի համարը կրկնվում է:");  ViewBag.ErrorText += "Անձնագրի համարը կրկնվում է:" + " ";
            }



            if (db.clients.Any(p => (p.socNumb == client.c.socNumb && p.clientId != client.c.clientId)))
            {
                ModelState.AddModelError("socNumb", "Սոց. քարտի համարը կրկնվում է:");   ViewBag.ErrorText += "Սոց. քարտի համարը կրկնվում է:" + " ";
            }

           

        


            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                client.c.regionId = CommonFunction.RegionIdGetFromName(client.c.rRegion.Trim());
                client.cw.userId = User.Identity.GetUserId();
                db.clients.Add(client.c);
                db.SaveChanges();
                client.cw.clientId = client.c.clientId;
                client.cw.CreatedDate = DateTime.Now;
                db.clientWorkDatas.Add(client.cw);
                db.SaveChanges();
                return RedirectToAction("Index");
            }//if(ModelState.IsValid)

            var regs = (from r in db.comunities
                        let rRegion = r.reg
                        select new { rRegion }).Distinct().ToList();

            var cityes = (from c in db.comunities
                          let rCity = c.cName
                          select new { rCity }).Distinct().ToList();


            var Streets = (from s in db.Streets
                           let rStreet = s.Street
                           select new { rStreet }).Distinct().ToList();

            var cStreets = (from s in db.Streets
                           let rStreet = s.Street
                           select new { rStreet }).Distinct().ToList();

            if (client.c.rRegion != null)
            {
                cityes = (from c in db.comunities
                          let rCity = c.cName
                          where c.reg == client.c.rRegion
                          select new { rCity }).Distinct().ToList();
            }

              var ccityes = (from c in db.comunities
                           let rCity = c.cName
                           select new { rCity }).Distinct().ToList();
            if (client.c.cRegion != null)
            {
                ccityes = (from c in db.comunities
                           let rCity = c.cName
                           where c.reg == client.c.cRegion
                           select new { rCity }).Distinct().ToList();
            }


            if (client.c.rRegion != null && client.c.rCity != null)
            {
                Streets = (from s in db.Streets
                           let rStreet = s.Street
                           where s.reg == client.c.rRegion && s.cName == client.c.rCity
                           select new { rStreet }).Distinct().ToList();
            }

            if (client.c.cRegion != null && client.c.cCity != null)
            {
                cStreets = (from s in db.Streets
                           let rStreet = s.Street
                           where s.reg == client.c.cRegion && s.cName == client.c.cCity
                           select new { rStreet }).Distinct().ToList();
            }


          
           


            ViewBag.rg = new SelectList(regs, "rRegion", "rRegion");
            ViewBag.ct = new SelectList(cityes, "rCity", "rCity");
            ViewBag.cct = new SelectList(ccityes, "rCity", "rCity");

            var sex = (from s in db.clientSexes select new { s.clientSexId, s.sex }).ToList();
            var edu = (from e in db.educations select new { e.educationId, e.Education }).ToList();
            ViewBag.sx = new SelectList(sex, "clientSexId", "sex");//
            ViewBag.ed = new SelectList(edu, "educationId", "Education");//

            var empl = (from e in db.employmentTypes
                        select new { e.empTypeID, e.employment });
            ViewBag.em = new SelectList(empl, "empTypeID", "employment");

            var cmpTyp = (from ct in db.companyTypes
                          where ct.FK_empType == client.cw.employmentTypeId
                          select new { ct.companyTypeID, ct.companyTypeName });

            ViewBag.ctyp = new SelectList(cmpTyp, "companyTypeID", "companyTypeName");
            ViewBag.Streets = new SelectList(Streets, "rStreet", "rStreet");
            ViewBag.cStreets = new SelectList(cStreets, "rStreet", "rStreet");

            return View(client);
        }

        [Authorize]
        public ActionResult ClientWorkEdit(long ClientID)
        {
            Models.clientWorkDatas cwModel = new clientWorkDatas();
            var cw = (from w in db.clientWorkDatas
                      where w.clientId == ClientID
                      select new
                      {
                          w.clientId,
                          w.companyAddress,
                          w.companyName,
                          w.CompanyTel,
                          w.companyTypeId,
                          w.CreatedDate,
                          w.employmentTypeId,
                          w.empRegDate,
                          w.Id,
                          w.jobTitle,
                          w.LastModifDate,
                          w.note1,
                          w.note2,
                          w.note3,
                          w.note4,
                          w.note5,
                          w.otherIncome,
                          w.otherIncomeDescr,
                          w.salary,
                          w.taxNumber,
                          w.userId

                      }).ToList();

            foreach (var t in cw)
            {
                cwModel.clientId = t.clientId;
                cwModel.companyAddress = t.companyAddress;
                cwModel.companyName = t.companyName;
                cwModel.CompanyTel = t.CompanyTel;
                cwModel.companyTypeId = t.companyTypeId;
                cwModel.CreatedDate = t.CreatedDate;
                cwModel.employmentTypeId = t.employmentTypeId;
                cwModel.empRegDate = t.empRegDate;
                cwModel.Id = t.Id;
                cwModel.jobTitle = t.jobTitle;
                cwModel.LastModifDate = t.LastModifDate;
                cwModel.note1 = t.note1;
                cwModel.note2 = t.note2;
                cwModel.note3 = t.note3;
                cwModel.note4 = t.note4;
                cwModel.note5 = t.note5;
                cwModel.otherIncome = t.otherIncome;
                cwModel.otherIncomeDescr = t.otherIncomeDescr;
                cwModel.salary = t.salary;
                cwModel.taxNumber = t.taxNumber;
                cwModel.userId = t.userId;
            }//foreach(var t in cw)

            var empl = (from e in db.employmentTypes
                        select new { e.empTypeID, e.employment }).ToList();
            ViewBag.em = new SelectList(empl, "empTypeID", "employment");

            var cmpTyp = (from ct in db.companyTypes
                          select new { ct.companyTypeID, ct.companyTypeName });

            ViewBag.ctyp = new SelectList(cmpTyp, "companyTypeID", "companyTypeName");

            var cname = (from cn in db.clients
                         where cn.clientId == ClientID
                         let cfullname = cn.clientName + " " + cn.clientLastName
                         select new { cn.clientId, cfullname });

            ViewBag.cfname = new SelectList(cname, "clientId", "cfullname");

            return View(cwModel);
        }


        [Authorize]
        public ActionResult ClientWorkDetails(long clientId)
        {
            Models.clientWorkDatas cwModel = new clientWorkDatas();
            var cw = (from w in db.clientWorkDatas
                      where w.clientId == clientId
                      select new
                      {
                          w.clientId,
                          w.companyAddress,
                          w.companyName,
                          w.CompanyTel,
                          w.companyTypeId,
                          w.CreatedDate,
                          w.employmentTypeId,
                          w.empRegDate,
                          w.Id,
                          w.jobTitle,
                          w.LastModifDate,
                          w.note1,
                          w.note2,
                          w.note3,
                          w.note4,
                          w.note5,
                          w.otherIncome,
                          w.otherIncomeDescr,
                          w.salary,
                          w.taxNumber,
                          w.userId
                      });

            foreach (var t in cw)
            {
                cwModel.clientId = t.clientId;
                cwModel.companyAddress = t.companyAddress;
                cwModel.companyName = t.companyName;
                cwModel.CompanyTel = t.CompanyTel;
                cwModel.companyTypeId = t.companyTypeId;
                cwModel.CreatedDate = t.CreatedDate;
                cwModel.employmentTypeId = t.employmentTypeId;
                cwModel.empRegDate = t.empRegDate;
                cwModel.Id = t.Id;
                cwModel.jobTitle = t.jobTitle;
                cwModel.LastModifDate = t.LastModifDate;
                cwModel.note1 = t.note1;
                cwModel.note2 = t.note2;
                cwModel.note3 = t.note3;
                cwModel.note4 = t.note4;
                cwModel.note5 = t.note5;
                cwModel.otherIncome = t.otherIncome;
                cwModel.otherIncomeDescr = t.otherIncomeDescr;
                cwModel.salary = t.salary;
                cwModel.taxNumber = t.taxNumber;
                cwModel.userId = t.userId;
            }//foreach(var t in cw)

            var empl = (from e in db.employmentTypes
                        select new { e.empTypeID, e.employment }).ToList();
            ViewBag.em = new SelectList(empl, "empTypeID", "employment");

            var cmpTyp = (from ct in db.companyTypes
                          select new { ct.companyTypeID, ct.companyTypeName });

            ViewBag.ctyp = new SelectList(cmpTyp, "companyTypeID", "companyTypeName");

            var cname = (from cn in db.clients
                         where cn.clientId == clientId
                         let cfullname = cn.clientName + " " + cn.clientLastName
                         select new { cn.clientId, cfullname });

            ViewBag.cfname = new SelectList(cname, "clientId", "cfullname");

            return View(cwModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ClientWorkEdit(Models.clientWorkDatas cw)
        {
            ViewBag.isSaved = "";
            if (ModelState.IsValid)
            {
                cw.userId = User.Identity.GetUserId();
                cw.LastModifDate = DateTime.Now;

                var uCWD = db.clientWorkDatas.Where(c => c.Id == cw.Id).FirstOrDefault();
                if(uCWD==null || uCWD?.Id == 0)
                {
                    uCWD = new clientWorkDatas();
                    db.clientWorkDatas.Add(uCWD);
                    db.SaveChanges();
                }
                db.Entry(uCWD).CurrentValues.SetValues(cw);

                db.SaveChanges();
                ViewBag.isSaved = "Փոփոխությունը պահպանված է:";
                //return RedirectToAction("ClientWorkDetails", new { clientId = cw.clientId });
            }//if(ModelState.IsValid)

            var empl = (from e in db.employmentTypes
                        select new { e.empTypeID, e.employment }).ToList();
            ViewBag.em = new SelectList(empl, "empTypeID", "employment");

            var cmpTyp = (from ct in db.companyTypes
                          where ct.FK_empType == cw.employmentTypeId
                          select new { ct.companyTypeID, ct.companyTypeName });

            ViewBag.ctyp = new SelectList(cmpTyp, "companyTypeID", "companyTypeName");

            var cname = (from cn in db.clients
                         where cn.clientId == cw.clientId
                         let cfullname = cn.clientName + " " + cn.clientLastName
                         select new { cn.clientId, cfullname });

            ViewBag.cfname = new SelectList(cname, "clientId", "cfullname");

            return View(cw);
        }


        [Authorize]
        public ActionResult Edit(long? clientId)
        {
            if (clientId == null)
                return RedirectToAction("Create");

            Models.clients clientM = new clients();
            var client = (from c in db.clients
                          where c.clientId == clientId
                          select new
                          {
                              clientId = c.clientId,
                              cApartment = c.cApartment,
                              cBuilding = c.cBuilding,
                              cCity = c.cCity,
                              clientLastName = c.clientLastName,
                              clientMidName = c.clientMidName,
                              clientName = c.clientName,
                              cRegion = c.cRegion,
                              cStreet = c.cStreet,
                              dob = c.dob,
                              edu = c.edu_educationId,
                              fEmpMemb = c.fEmpMemb,
                              fMemb = c.fMemb,
                              fTenMemb = c.fTenMemb,
                              isRented = c.isRented,
                              isSameAddress = c.isSameAddress,
                              mob1 = c.mob1,
                              mob2 = c.mob2,
                              mob3 = c.mob3,
                              mob4 = c.mob4,
                              note1 = c.note1,
                              note2 = c.note2,
                              note3 = c.note3,
                              passpAuth = c.passpAuth,
                              passpDate = c.passpDate,
                              passpNumb = c.passpNumb,
                              rApartment = c.rApartment,
                              rBuilding = c.rBuilding,
                              rCity = c.rCity,
                              rRegion = c.rRegion,
                              rStreet = c.rStreet,
                              sex = c.sex_clientSexId,
                              socNumb = c.socNumb,
                              tel = c.tel
                          });






            foreach (var c in client)
            {
                clientM.clientId = c.clientId;
                clientM.cApartment = c.cApartment;
                clientM.cBuilding = c.cBuilding;
                clientM.cCity = c.cCity;
                clientM.clientLastName = c.clientLastName;
                clientM.clientMidName = c.clientMidName;
                clientM.clientName = c.clientName;
                clientM.cRegion = c.cRegion;
                clientM.cStreet = c.cStreet;
                clientM.dob = c.dob;
                clientM.edu_educationId = c.edu;
                clientM.fEmpMemb = c.fEmpMemb;
                clientM.fMemb = c.fMemb;
                clientM.fTenMemb = c.fTenMemb;
                clientM.isRented = c.isRented;
                clientM.isSameAddress = c.isSameAddress;
                clientM.mob1 = c.mob1;
                clientM.mob2 = c.mob2;
                clientM.mob3 = c.mob3;
                clientM.mob4 = c.mob4;
                clientM.note1 = c.note1;
                clientM.note2 = c.note2;
                clientM.note3 = c.note3;
                clientM.passpAuth = c.passpAuth;
                clientM.passpDate = c.passpDate;
                clientM.passpNumb = c.passpNumb;
                clientM.rApartment = c.rApartment;
                clientM.rBuilding = c.rBuilding;
                clientM.rCity = c.rCity;
                clientM.rRegion = c.rRegion;
                clientM.rStreet = c.rStreet;
                clientM.sex_clientSexId = c.sex;
                clientM.socNumb = c.socNumb;
                clientM.tel = c.tel;
            }

            if (clientM.clientId == 0)
                return RedirectToAction("Create");


            var regs = (from r in db.comunities
                        let rRegion = r.reg
                        select new { rRegion }).Distinct().ToList();

            var cityes = (from c in db.comunities
                          let rCity = c.cName
                          select new { rCity }).Distinct().ToList();

            var Streets = (from s in db.Streets
                           let rStreet = s.Street
                           select new { rStreet }).Distinct().ToList();

            var cStreets = (from s in db.Streets
                            let rStreet = s.Street
                            select new { rStreet }).Distinct().ToList();

            if (clientM.rRegion != null)
            {
                cityes = (from c in db.comunities
                          let rCity = c.cName
                          where c.reg == clientM.rRegion
                          select new { rCity }).Distinct().ToList();
            }

            if (clientM.rRegion != null && clientM.rCity != null)
            {
                Streets = (from s in db.Streets
                           let rStreet = s.Street
                           where s.reg == clientM.rRegion && s.cName == clientM.rCity
                           select new { rStreet }).Distinct().ToList();
            }




            if (clientM.cRegion != null && clientM.cCity != null)
            {
                cStreets = (from s in db.Streets
                            let rStreet = s.Street
                            where s.reg == clientM.cRegion && s.cName == clientM.cCity
                            select new { rStreet }).Distinct().ToList();
            }


            var ccityes = (from c in db.comunities
                           let rCity = c.cName
                           select new { rCity }).Distinct().ToList();

            if (clientM.cRegion != null)
            {
                ccityes = (from c in db.comunities
                           let rCity = c.cName
                           where c.reg == clientM.cRegion
                           select new { rCity }).Distinct().ToList();
            }

            ViewBag.rg = new SelectList(regs, "rRegion", "rRegion");
            ViewBag.ct = new SelectList(cityes, "rCity", "rCity");
            ViewBag.cct = new SelectList(ccityes, "rCity", "rCity");

            var sex = (from s in db.clientSexes select new { s.clientSexId, s.sex }).ToList();
            var edu = (from e in db.educations select new { e.educationId, e.Education }).ToList();
            ViewBag.sx = new SelectList(sex, "clientSexId", "sex", clientM.sex_clientSexId);
            ViewBag.ed = new SelectList(edu, "educationId", "Education", clientM.edu_educationId);


            ViewBag.Streets = new SelectList(Streets, "rStreet", "rStreet");
            ViewBag.cStreets = new SelectList(cStreets, "rStreet", "rStreet");





            long clientIdL = clientId ?? 0;

            Models.clientWorkDatas cwModel = new clientWorkDatas();
            var cw = (from w in db.clientWorkDatas
                      where w.clientId == clientIdL
                      select new
                      {
                          w.clientId,
                          w.companyAddress,
                          w.companyName,
                          w.CompanyTel,
                          w.companyTypeId,
                          w.CreatedDate,
                          w.employmentTypeId,
                          w.empRegDate,
                          w.Id,
                          w.jobTitle,
                          w.LastModifDate,
                          w.note1,
                          w.note2,
                          w.note3,
                          w.note4,
                          w.note5,
                          w.otherIncome,
                          w.otherIncomeDescr,
                          w.salary,
                          w.taxNumber,
                          w.userId
                      });

            foreach (var t in cw)
            {
                cwModel.clientId = t.clientId;
                cwModel.companyAddress = t.companyAddress;
                cwModel.companyName = t.companyName;
                cwModel.CompanyTel = t.CompanyTel;
                cwModel.companyTypeId = t.companyTypeId;
                cwModel.CreatedDate = t.CreatedDate;
                cwModel.employmentTypeId = t.employmentTypeId;
                cwModel.empRegDate = t.empRegDate;
                cwModel.Id = t.Id;
                cwModel.jobTitle = t.jobTitle;
                cwModel.LastModifDate = t.LastModifDate;
                cwModel.note1 = t.note1;
                cwModel.note2 = t.note2;
                cwModel.note3 = t.note3;
                cwModel.note4 = t.note4;
                cwModel.note5 = t.note5;
                cwModel.otherIncome = t.otherIncome;
                cwModel.otherIncomeDescr = t.otherIncomeDescr;
                cwModel.salary = t.salary;
                cwModel.taxNumber = t.taxNumber;
                cwModel.userId = t.userId;
            }//foreach(var t in cw)

            var empl = (from e in db.employmentTypes
                        select new { e.empTypeID, e.employment }).ToList();
            ViewBag.em = new SelectList(empl, "empTypeID", "employment");

            var cmpTyp = (from ct in db.companyTypes
                          select new { ct.companyTypeID, ct.companyTypeName });

            ViewBag.ctyp = new SelectList(cmpTyp, "companyTypeID", "companyTypeName");

            var cname = (from cn in db.clients
                         where cn.clientId == clientIdL
                         let cfullname = cn.clientName + " " + cn.clientLastName
                         select new { cn.clientId, cfullname });

            ViewBag.cfname = new SelectList(cname, "clientId", "cfullname");

            @ViewBag.ClientWorkData = cwModel;






            return View(clientM);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Models.clients client)
        {
            ViewBag.ErrorText = string.Empty;

            if (db.clients.Any(p => (p.passpNumb == client.passpNumb && p.clientId != client.clientId)))
            {
                ModelState.AddModelError("passpNumb", "Անձնագրի համարը կրկնվում է:");
                ViewBag.ErrorText += "Անձնագրի համարը կրկնվում է:" + " ";
            }



            if (db.clients.Any(p => (p.socNumb == client.socNumb && p.clientId != client.clientId)))
            {
                ModelState.AddModelError("socNumb", "Սոց. քարտի համարը կրկնվում է:");
                ViewBag.ErrorText += "Սոց. քարտի համարը կրկնվում է:" + " ";
            }




            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                client.regionId = CommonFunction.RegionIdGetFromName(client.rRegion.Trim());
                var cl = db.clients.Where(c => c.clientId == client.clientId).FirstOrDefault();                
                db.Entry(cl).CurrentValues.SetValues(client);
                db.SaveChanges();
                ViewBag.cSaved = "Փոփոխությունները պահպանված են:";
            }


            var regs = (from r in db.comunities
                        let rRegion = r.reg
                        select new { rRegion }).Distinct().ToList();

            var cityes = (from c in db.comunities
                          let rCity = c.cName
                          select new { rCity }).Distinct().ToList();

            var Streets = (from s in db.Streets
                           let rStreet = s.Street
                           select new { rStreet }).Distinct().ToList();

            var cStreets = (from s in db.Streets
                            let rStreet = s.Street
                            select new { rStreet }).Distinct().ToList();

            if (client.rRegion != null)
            {
                cityes = (from c in db.comunities
                          let rCity = c.cName
                          where c.reg == client.rRegion
                          select new { rCity }).Distinct().ToList();
            }

            var ccityes = (from c in db.comunities
                           let rCity = c.cName
                           select new { rCity }).Distinct().ToList();

            if (client.cRegion != null)
            {
                ccityes = (from c in db.comunities
                           let rCity = c.cName
                           where c.reg == client.cRegion
                           select new { rCity }).Distinct().ToList();
            }



            if (client.rRegion != null && client.rCity != null)
            {
                Streets = (from s in db.Streets
                           let rStreet = s.Street
                           where s.reg == client.rRegion && s.cName == client.rCity
                           select new { rStreet }).Distinct().ToList();
            }




            if (client.cRegion != null && client.cCity != null)
            {
                cStreets = (from s in db.Streets
                            let rStreet = s.Street
                            where s.reg == client.cRegion && s.cName == client.cCity
                            select new { rStreet }).Distinct().ToList();
            }


           
            ViewBag.rg = new SelectList(regs, "rRegion", "rRegion");
            ViewBag.ct = new SelectList(cityes, "rCity", "rCity");
            ViewBag.cct = new SelectList(ccityes, "rCity", "rCity");

            var sex = (from s in db.clientSexes select new { s.clientSexId, s.sex }).ToList();
            var edu = (from e in db.educations select new { e.educationId, e.Education }).ToList();
            ViewBag.sx = new SelectList(sex, "clientSexId", "sex");
            ViewBag.ed = new SelectList(edu, "educationId", "Education");

            ViewBag.Streets = new SelectList(Streets, "rStreet", "rStreet");
            ViewBag.cStreets = new SelectList(cStreets, "rStreet", "rStreet");






            Models.clientWorkDatas cwModel = new clientWorkDatas();
            var cw = (from w in db.clientWorkDatas
                      where w.clientId == client.clientId
                      select new
                      {
                          w.clientId,
                          w.companyAddress,
                          w.companyName,
                          w.CompanyTel,
                          w.companyTypeId,
                          w.CreatedDate,
                          w.employmentTypeId,
                          w.empRegDate,
                          w.Id,
                          w.jobTitle,
                          w.LastModifDate,
                          w.note1,
                          w.note2,
                          w.note3,
                          w.note4,
                          w.note5,
                          w.otherIncome,
                          w.otherIncomeDescr,
                          w.salary,
                          w.taxNumber,
                          w.userId
                      });

            foreach (var t in cw)
            {
                cwModel.clientId = t.clientId;
                cwModel.companyAddress = t.companyAddress;
                cwModel.companyName = t.companyName;
                cwModel.CompanyTel = t.CompanyTel;
                cwModel.companyTypeId = t.companyTypeId;
                cwModel.CreatedDate = t.CreatedDate;
                cwModel.employmentTypeId = t.employmentTypeId;
                cwModel.empRegDate = t.empRegDate;
                cwModel.Id = t.Id;
                cwModel.jobTitle = t.jobTitle;
                cwModel.LastModifDate = t.LastModifDate;
                cwModel.note1 = t.note1;
                cwModel.note2 = t.note2;
                cwModel.note3 = t.note3;
                cwModel.note4 = t.note4;
                cwModel.note5 = t.note5;
                cwModel.otherIncome = t.otherIncome;
                cwModel.otherIncomeDescr = t.otherIncomeDescr;
                cwModel.salary = t.salary;
                cwModel.taxNumber = t.taxNumber;
                cwModel.userId = t.userId;
            }//foreach(var t in cw)

            var empl = (from e in db.employmentTypes
                        select new { e.empTypeID, e.employment }).ToList();
            ViewBag.em = new SelectList(empl, "empTypeID", "employment");

            var cmpTyp = (from ct in db.companyTypes
                          select new { ct.companyTypeID, ct.companyTypeName });

            ViewBag.ctyp = new SelectList(cmpTyp, "companyTypeID", "companyTypeName");

            var cname = (from cn in db.clients
                         where cn.clientId == client.clientId
                         let cfullname = cn.clientName + " " + cn.clientLastName
                         select new { cn.clientId, cfullname });

            ViewBag.cfname = new SelectList(cname, "clientId", "cfullname");  

            @ViewBag.ClientWorkData = cwModel;





            return View(client);
        }

    }//public class ClientsController : Controller
}//namespace ASFront.Controllers