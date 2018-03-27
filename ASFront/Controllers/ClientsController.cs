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
using System.Data.Entity;
using ASFront.ModelsView;
using System.Text.RegularExpressions;

namespace ASFront.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        ApplicationDbContext db;
        public ClientsController()
        {
            db = new Models.ApplicationDbContext();
        }



     
        public ActionResult NewClient()
        {

            ClientTypeViewModel item = new ClientTypeViewModel();
            return View(item);
        }


        [HttpPost]
        public ActionResult NewClient(ClientTypeViewModel item)
        {
            if(item.ClientType== "Client")
                return RedirectToAction("Create");
            return View(item);
        }


        [Authorize]
        public ActionResult Table(int page = 1)
        {
            var clients = new List<clients>();
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);

            if (TempData["cl"] != null)
            {
                ClientShearchViewModel item = TempData["cl"] as ClientShearchViewModel;
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);


                item.NameStr = item.NameStr?.Trim();
                item.PassOrSocNumtStr = item.PassOrSocNumtStr?.Trim();
                item.PhoneStr = item.PhoneStr?.Trim();

               


             
                item.PassOrSocNumtStr = item.PassOrSocNumtStr?.ToLower();
                item.NameStr = item.NameStr?.ToLower();




                if (!string.IsNullOrWhiteSpace(item.PassOrSocNumtStr))
                {
                    clients = db.clients.Where(p => p.socNumb.Trim().ToLower() == item.PassOrSocNumtStr ||
                                    p.passpNumb.Trim().ToLower() == item.PassOrSocNumtStr).ToList();

                }


                if (!(clients?.Count > 0))
                    if (!string.IsNullOrWhiteSpace(item.PhoneStr))
                    {
                        clients = db.clients.Where(p => p.mob1.Trim() == item.PhoneStr || p.mob2.Trim() == item.PhoneStr || p.mob3.Trim() == item.PhoneStr || p.mob4.Trim() == item.PhoneStr).ToList();
                    }




               





                if (!(clients?.Count > 0))
                    if (!string.IsNullOrWhiteSpace(item.NameStr))
                    {

                 
                        item.NameStr = regex.Replace(item.NameStr, " ");



                        string[] Names = item.NameStr.Split(' ');

                        if (Names.Length > 1)
                        {
                            string FName = Names[0];
                            string LName = Names[1];
                            clients = db.clients.Where(p => p.clientName.Trim().ToLower().StartsWith(FName) &&
                                 p.clientLastName.Trim().ToLower().StartsWith(LName)).ToList();

                        }
                        else if (Names.Length == 1)
                        {
                            string FLName = Names[0];

                            clients = db.clients.Where(p => p.clientName.Trim().ToLower().StartsWith(FLName) ||
                                 p.clientLastName.Trim().ToLower().StartsWith(FLName)).ToList();
                        }


                        if (!string.IsNullOrWhiteSpace(item.Region) || !string.IsNullOrWhiteSpace(item.City) || !string.IsNullOrWhiteSpace(item.Street))
                        {
                            if (!string.IsNullOrWhiteSpace(item.Region))
                                clients = clients.Where(p => p.rRegion == item.Region).ToList();


                            if (!string.IsNullOrWhiteSpace(item.City))
                                clients = clients.Where(p => p.rCity == item.City).ToList();


                            if (!string.IsNullOrWhiteSpace(item.Street))
                                clients = clients.Where(p => p.rStreet == item.Street).ToList();

                        }


                    }





                if (!(clients?.Count > 0))
                    if (!string.IsNullOrWhiteSpace(item.Region) || !string.IsNullOrWhiteSpace(item.City) || !string.IsNullOrWhiteSpace(item.Street))
                    {


                        if (!string.IsNullOrWhiteSpace(item.Region) || !string.IsNullOrWhiteSpace(item.City) || !string.IsNullOrWhiteSpace(item.Street))
                        {
                            clients = db.clients.ToList();

                            if (!string.IsNullOrWhiteSpace(item.Region))
                                clients = clients.Where(p => p.rRegion == item.Region).ToList();


                            if (!string.IsNullOrWhiteSpace(item.City))
                                clients = clients.Where(p => p.rCity == item.City).ToList();


                            if (!string.IsNullOrWhiteSpace(item.Street))
                                clients = clients.Where(p => p.rStreet == item.Street).ToList();

                        }


                    }




            }


            return View(clients.ToPagedList(pageNumber, pageSize));





        }






        // GET: Clients
        [Authorize]
        public ActionResult Index()
        {
            //var clients = db.clients.OrderByDescending(p => p.clientId).ToList();
            //int pageSize = ApplicationSettings.PageSize;
            //int pageNumber = (page);



            //return View(clients.ToPagedList(pageNumber, pageSize));


            ClientShearchViewModel item = new ClientShearchViewModel();
            return View(item);


        }







        [Authorize]
        [HttpPost]
        //public ActionResult Index(string NameStr, string PhoneStr, string PassOrSocNumtStr,
        //    string Region, string City, string Street,
        //    string searchBtn, string dob)
        public ActionResult Index(ClientShearchViewModel item)

        {



            TempData["cl"] = item;


            return RedirectToAction("Table");



            ////string SearchStr = Request.Form["SearchStr"].Trim();
            ////string searchBtn = Request.Form["searchBtn"].Trim();

            //List<clients> clients = new List<clients>();
            //clients client = null;

            //string NameStr = ""; ; string PhoneStr = ""; string PassOrSocNumtStr = ""; string dob = "";
            //string searchBtn = "որոնել";


            //PassOrSocNumtStr = PassOrSocNumtStr.Trim();
            //NameStr = NameStr.Trim();

            //PhoneStr = PhoneStr.Trim();


            //RegexOptions options = RegexOptions.None;
            //Regex regex = new Regex("[ ]{2,}", options);
            //NameStr = regex.Replace(NameStr, " ");


            //if (searchBtn == "որոնել")
            //{


            //    if (!string.IsNullOrWhiteSpace(PassOrSocNumtStr))
            //    {
            //        client = db.clients.Where(p => p.socNumb == PassOrSocNumtStr ||
            //        p.passpNumb.Trim() == PassOrSocNumtStr || p.tel.Trim() == PassOrSocNumtStr
            //         || p.mob1.Trim() == PassOrSocNumtStr || p.mob2.Trim() == PassOrSocNumtStr || p.mob3.Trim() == PassOrSocNumtStr || p.mob4.Trim() == PassOrSocNumtStr
            //        ).FirstOrDefault();


            //        if (client == null && !string.IsNullOrWhiteSpace(PhoneStr))
            //        {
            //            client = db.clients.Where(p =>
            //             p.mob1.Trim() == PhoneStr || p.mob2.Trim() == PhoneStr || p.mob3.Trim() == PhoneStr || p.mob4.Trim() == PhoneStr
            //            ).FirstOrDefault();




            //        }


            //        if (client == null && !string.IsNullOrWhiteSpace(NameStr))
            //        {
            //            clients = db.clients.Where(p => (
            //             (p.clientName.Trim() + " " + p.clientLastName.Trim()) == NameStr
            //              )
            //            ).ToList();

            //            if (!string.IsNullOrWhiteSpace(dob))

            //                clients = clients.Where(p => (p.dob.ToShortDateString()) == dob).ToList();
            //            if (clients.Count == 1)
            //                client = clients.FirstOrDefault();

            //            else client = null;
            //        }
            //    }

            //    else
            //    {



            //        if (!string.IsNullOrWhiteSpace(PhoneStr))
            //        {
            //            client = db.clients.Where(p =>
            //             p.mob1.Trim() == PhoneStr || p.mob2.Trim() == PhoneStr || p.mob3.Trim() == PhoneStr || p.mob4.Trim() == PhoneStr
            //            ).FirstOrDefault();




            //        }


            //        //////



            //        if (client == null && !string.IsNullOrWhiteSpace(NameStr))
            //        {
            //            clients = db.clients.Where(p => (
            //             (p.clientName.Trim() + " " + p.clientLastName.Trim()) == NameStr
            //              )
            //            ).ToList();

            //            if (!string.IsNullOrWhiteSpace(dob))
            //                clients = clients.Where(p => (p.dob.ToShortDateString()) == dob).ToList();

            //            if (clients.Count == 1)
            //                client = clients.FirstOrDefault();

            //            else client = null;



            //        }


            //    }





            //    if (client == null)
            //    {
            //        ViewBag.ErrorMessage = "Չի գտնվել";
            //    }
            //    else
            //    {
            //        return RedirectToAction("Edit", "Clients", new { clientId = client.clientId });

            //    }


            //}
            //////else
            ////{
            ////    ModelState.Clear();
            ////    clients = db.clients.ToList();
            ////}




            //int pageSize = ApplicationSettings.PageSize;
            //int pageNumber = 1;






            //return View(clients.ToPagedList(pageNumber, pageSize));
        }


        [Authorize]
        public ActionResult CreateCompany()
        {
            //Models.client client = new client();
            Models.CompanyFullViewModel client = new Models.CompanyFullViewModel();

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



            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name");
            ViewBag.BusinessTypeId = new SelectList(db.BusinessType, "Id", "Name");
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName");
            ViewBag.GenderId = new SelectList(db.clientSexes, "clientSexId", "sex");
            ViewBag.NameofBanksId = new SelectList(db.NameofBanks, "Id", "Name");
            ViewBag.OwnershipTypeId = new SelectList(db.OwnershipType, "Id", "Name");



            client.c = new clients();
            client.c.isSameAddress = true;
            //client.cw = new clientWorkDatas();
            return View(client);
        }


        [Authorize]
        [HttpPost]
        public ActionResult CreateCompany(Models.CompanyFullViewModel client)
        {
            ViewBag.ErrorText = string.Empty;

            if (db.clients.Any(p => (p.passpNumb == client.c.passpNumb && p.clientId != client.c.clientId)))
            {
                ModelState.AddModelError("passpNumb", "Անձնագրի համարը կրկնվում է:"); ViewBag.ErrorText += "Անձնագրի համարը կրկնվում է:" + " ";
            }



            if (db.clients.Any(p => (p.socNumb == client.c.socNumb && p.clientId != client.c.clientId)))
            {
                ModelState.AddModelError("socNumb", "Սոց. քարտի համարը կրկնվում է:"); ViewBag.ErrorText += "Սոց. քարտի համարը կրկնվում է:" + " ";
            }




            
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                client.c.regionId = CommonFunction.RegionIdGetFromName(client.c.rRegion.Trim());
                //client.cw.userId = User.Identity.GetUserId();

                int BranchtId = 0;
                int UserASProfileId = 0;
                string currUserId = User.Identity.GetUserId();

                UserASProfileId = db.UserASProfiles.Where(p => p.UserId == currUserId).Select(p => p.UserASProfileId).FirstOrDefault();
                BranchtId = db.BranchUsers.Where(p => p.UserASProfileId == UserASProfileId).Select(p => p.BrancheId).FirstOrDefault();

                if (BranchtId == 0)
                    BranchtId = 1;

                client.c.BranchtId = BranchtId;

                db.clients.Add(client.c);
                db.SaveChanges();
                client.bi.clientId = client.c.clientId;
                db.BusinessInfo.Add(client.bi);
                //client.cw.clientId = client.c.clientId;
                //client.cw.CreatedDate = DateTime.Now;
                //db.clientWorkDatas.Add(client.cw);
                db.SaveChanges();
                return RedirectToAction("EditCompany", "Clients", new { clientId = client.c.clientId });
            }//if(ModelState.IsValid)


            //var errors = ModelState.Select(x => x.Value.Errors)
            //               .Where(y => y.Count > 0)
            //               .ToList();

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
                          select new { ct.companyTypeID, ct.companyTypeName });

            ViewBag.ctyp = new SelectList(cmpTyp, "companyTypeID", "companyTypeName");
            ViewBag.Streets = new SelectList(Streets, "rStreet", "rStreet");
            ViewBag.cStreets = new SelectList(cStreets, "rStreet", "rStreet");


            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name");
            ViewBag.BusinessTypeId = new SelectList(db.BusinessType, "Id", "Name");
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName");
            ViewBag.GenderId = new SelectList(db.clientSexes, "clientSexId", "sex");
            ViewBag.NameofBanksId = new SelectList(db.NameofBanks, "Id", "Name");
            ViewBag.OwnershipTypeId = new SelectList(db.OwnershipType, "Id", "Name");



            return View(client);
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
            client.cw = new clientWorkDatas();
            return View(client);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Create(Models.clientFullViewModel client)
        {
            ViewBag.ErrorText = string.Empty;

            if (db.clients.Any(p => (p.passpNumb == client.c.passpNumb && p.clientId != client.c.clientId)))
            {
                ModelState.AddModelError("passpNumb", "Անձնագրի համարը կրկնվում է:"); ViewBag.ErrorText += "Անձնագրի համարը կրկնվում է:" + " ";
                ViewBag.passpErr = "Անձնագրի համարը կրկնվում է:";
            }



            if (db.clients.Any(p => (p.socNumb == client.c.socNumb && p.clientId != client.c.clientId)))
            {
                ModelState.AddModelError("socNumb", "Սոց. քարտի համարը կրկնվում է:"); ViewBag.ErrorText += "Սոց. քարտի համարը կրկնվում է:" + " ";
                ViewBag.socErr = "Սոց. քարտի համարը կրկնվում է:";
            }






            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                client.c.regionId = CommonFunction.RegionIdGetFromName(client.c.rRegion.Trim());
                client.cw.userId = User.Identity.GetUserId();

                int BranchtId = 0;
                int UserASProfileId = 0;

                UserASProfileId = db.UserASProfiles.Where(p => p.UserId == client.cw.userId).Select(p => p.UserASProfileId).FirstOrDefault();
                BranchtId = db.BranchUsers.Where(p => p.UserASProfileId == UserASProfileId).Select(p => p.BrancheId).FirstOrDefault();

                if (client.c.fMemb < (client.c.fMemb + client.c.fTenMemb))
                    client.c.fMemb = (client.c.fMemb + client.c.fTenMemb);

                if (BranchtId == 0)
                    BranchtId = 1;

                client.c.BranchtId = BranchtId;

                db.clients.Add(client.c);
                db.SaveChanges();
                client.cw.clientId = client.c.clientId;
                client.cw.CreatedDate = DateTime.Now;
                db.clientWorkDatas.Add(client.cw);
                db.SaveChanges();

                string regAddresFull = client.c.rRegion + " " + client.c.rCity + " " + client.c.rStreet + " " + client.c.rBuilding + " " + client.c.rApartment;
                string curAddresFull = client.c.cRegion + " " + client.c.cCity + " " + client.c.cStreet + " " + client.c.cBuilding + " " + client.c.cApartment;
                clientsGroup cgroup = new clientsGroup();
                long groupId = db.group.Where(g => g.gruopAddress == regAddresFull).Select(i => i.groupId).SingleOrDefault();
                if(groupId < 1)
                {
                    if(curAddresFull.Length > 4)
                    {
                        groupId = db.group.Where(g => g.gruopAddress == curAddresFull).Select(i => i.groupId).SingleOrDefault();
                        if (groupId > 0)
                        {
                            cgroup.groupId = groupId;
                            cgroup.clientId = client.c.clientId;
                            cgroup.relType = 6;
                            db.clientsGroup.Add(cgroup);
                            db.SaveChanges();
                        }
                    }
                   
                }
                else
                {
                    
                    cgroup.groupId = groupId;
                    cgroup.clientId = client.c.clientId;
                    cgroup.relType = 6;
                    db.clientsGroup.Add(cgroup);
                    db.SaveChanges();
                }//if(groupId < 1)

                if(groupId < 1)
                {
                    int isExist = 0;
                    var oldclients = db.clients.Where(a => a.rRegion == client.c.rRegion && a.rCity == client.c.rCity && a.rStreet == client.c.rStreet && a.rBuilding == client.c.rBuilding && a.rApartment == client.c.rApartment && a.clientId != client.c.clientId && (a.rRegion != "R" && a.mob1 != "091000000")).ToList();
                    foreach(var oc in oldclients)
                    {
                        if(isExist == 0)
                        {
                            group gr = new group();
                            gr.gruopName = client.c.clientLastName;
                            gr.gruopFullName = regAddresFull + " " + gr.gruopName;
                            gr.gruopAddress = regAddresFull;
                            gr.note3 = "Մուտքագրման ժամանակ հայտնաբերված համընկնում ըստ գրանցման հացեի (Հաճախորդ ID " + client.c.clientId.ToString() + ")";
                            db.group.Add(gr);
                            db.SaveChanges();
                            cgroup.groupId = gr.groupId;
                            cgroup.clientId = client.c.clientId;
                            cgroup.relType = 6;
                            db.clientsGroup.Add(cgroup);
                            db.SaveChanges();
                            groupId = gr.groupId;
                        }

                        cgroup.groupId = groupId;
                        cgroup.clientId = oc.clientId;
                        cgroup.relType = 6;
                        db.clientsGroup.Add(cgroup);
                        db.SaveChanges();

                        isExist++;
                    }

                    if(isExist < 1)
                    {
                        oldclients = db.clients.Where(a => a.rRegion == client.c.cRegion && a.cCity == client.c.cCity && a.rStreet == client.c.cStreet && a.rBuilding == client.c.cBuilding && a.rApartment == client.c.cApartment && a.clientId != client.c.clientId && (a.rRegion != "R" && a.mob1 != "091000000")).ToList();
                        foreach (var oc in oldclients)
                        {
                            if (isExist == 0)
                            {
                                group gr = new group();
                                gr.gruopName = client.c.clientLastName;
                                gr.gruopFullName = regAddresFull + " " + gr.gruopName;
                                gr.gruopAddress = regAddresFull;
                                gr.note3 = "Մուտքագրման ժամանակ հայտնաբերված համընկնում ըստ փաստացի հացեի (Հաճախորդ ID " + client.c.clientId.ToString() + ")";
                                db.group.Add(gr);
                                db.SaveChanges();
                                cgroup.groupId = gr.groupId;
                                cgroup.clientId = client.c.clientId;
                                cgroup.relType = 6;
                                db.clientsGroup.Add(cgroup);
                                db.SaveChanges();
                                groupId = gr.groupId;
                            }

                            cgroup.groupId = groupId;
                            cgroup.clientId = oc.clientId;
                            cgroup.relType = 6;
                            db.clientsGroup.Add(cgroup);
                            db.SaveChanges();

                            isExist++;
                        }
                    }//if(isExist < 1)

                    if (isExist < 1)
                    {
                        oldclients = db.clients.Where(a => a.cRegion == client.c.rRegion && a.cCity == client.c.rCity && a.cStreet == client.c.rStreet && a.cBuilding == client.c.rBuilding && a.cApartment == client.c.rApartment && a.clientId != client.c.clientId && (a.rRegion != "R" && a.cRegion != null && a.mob1 != "091000000")).ToList();
                        foreach (var oc in oldclients)
                        {
                            if (isExist == 0)
                            {
                                group gr = new group();
                                gr.gruopName = oc.clientLastName;
                                gr.gruopAddress = oc.cRegion + " " + oc.cCity + " " + oc.cStreet + " " + oc.cBuilding + " " + oc.cApartment;
                                gr.gruopFullName = gr.gruopAddress + " " + gr.gruopName;
                                gr.note3 = "Մուտքագրման ժամանակ հայտնաբերված համընկնում ըստ փաստացի հացեի (Հաճախորդ ID " + oc.clientId.ToString() + ")";
                                db.group.Add(gr);
                                db.SaveChanges();
                                cgroup.groupId = gr.groupId;
                                cgroup.clientId = client.c.clientId;
                                cgroup.relType = 6;
                                db.clientsGroup.Add(cgroup);
                                db.SaveChanges();
                                groupId = gr.groupId;
                            }

                            cgroup.groupId = groupId;
                            cgroup.clientId = oc.clientId;
                            cgroup.relType = 6;
                            db.clientsGroup.Add(cgroup);
                            db.SaveChanges();

                            isExist++;
                        }//foreach (var oc in oldclients)
                    }
                    if (isExist < 1)
                    {
                        oldclients = db.clients.Where(a => a.rRegion == client.c.rRegion && a.rCity == client.c.rCity && a.rStreet == client.c.rStreet && a.rBuilding == client.c.rBuilding && a.rApartment == client.c.rApartment && a.clientId != client.c.clientId && (a.rRegion != "R" && a.mob1 != "091000000")).ToList();
                        foreach (var oc in oldclients)
                        {
                            if (isExist == 0)
                            {
                                group gr = new group();
                                gr.gruopName = oc.clientLastName;
                                gr.gruopAddress = oc.rRegion + " " + oc.rCity + " " + oc.rStreet + " " + oc.rBuilding + " " + oc.rApartment;
                                gr.gruopFullName = gr.gruopAddress + " " + gr.gruopName;
                                gr.note3 = "Մուտքագրման ժամանակ հայտնաբերված համընկնում ըստ գրանցման հացեի (Հաճախորդ ID " + oc.clientId.ToString() + ")";
                                db.group.Add(gr);
                                db.SaveChanges();
                                cgroup.groupId = gr.groupId;
                                cgroup.clientId = client.c.clientId;
                                cgroup.relType = 6;
                                db.clientsGroup.Add(cgroup);
                                db.SaveChanges();
                                groupId = gr.groupId;
                            }

                            cgroup.groupId = groupId;
                            cgroup.clientId = oc.clientId;
                            cgroup.relType = 6;
                            db.clientsGroup.Add(cgroup);
                            db.SaveChanges();

                            isExist++;
                        }//foreach (var oc in oldclients)
                    }
                    if (isExist < 1)
                    {
                        oldclients = db.clients.Where(a => a.cRegion == client.c.cRegion && a.cCity == client.c.cCity && a.cStreet == client.c.cStreet && a.cBuilding == client.c.cBuilding && a.cApartment == client.c.cApartment && a.clientId != client.c.clientId && (a.rRegion != "R" && a.cRegion != null && a.mob1 != "091000000")).ToList();
                        foreach (var oc in oldclients)
                        {
                            if (isExist == 0)
                            {
                                group gr = new group();
                                gr.gruopName = oc.clientLastName;
                                gr.gruopAddress = oc.cRegion + " " + oc.cCity + " " + oc.cStreet + " " + oc.cBuilding + " " + oc.cApartment;
                                gr.gruopFullName = gr.gruopAddress + " " + gr.gruopName;
                                gr.note3 = "Մուտքագրման ժամանակ հայտնաբերված համընկնում ըստ փաստացի հացեի (Հաճախորդ ID " + oc.clientId.ToString() + ")";
                                db.group.Add(gr);
                                db.SaveChanges();
                                cgroup.groupId = gr.groupId;
                                cgroup.clientId = client.c.clientId;
                                cgroup.relType = 6;
                                db.clientsGroup.Add(cgroup);
                                db.SaveChanges();
                                groupId = gr.groupId;
                            }

                            cgroup.groupId = groupId;
                            cgroup.clientId = oc.clientId;
                            cgroup.relType = 6;
                            db.clientsGroup.Add(cgroup);
                            db.SaveChanges();

                            isExist++;
                        }//foreach (var oc in oldclients)
                    }
                    if (isExist < 1)
                    {
                        oldclients = db.clients.Where(a => a.rRegion == client.c.cRegion && a.rCity == client.c.cCity && a.rStreet == client.c.cStreet && a.rBuilding == client.c.cBuilding && a.rApartment == client.c.cApartment && a.clientId != client.c.clientId && (a.rRegion != "R" && a.mob1 != "091000000")).ToList();
                        foreach (var oc in oldclients)
                        {
                            if (isExist == 0)
                            {
                                group gr = new group();
                                gr.gruopName = oc.clientLastName;
                                gr.gruopAddress = oc.rRegion + " " + oc.rCity + " " + oc.rStreet + " " + oc.rBuilding + " " + oc.rApartment;
                                gr.gruopFullName = gr.gruopAddress + " " + gr.gruopName;
                                gr.note3 = "Մուտքագրման ժամանակ հայտնաբերված համընկնում ըստ գրանցման հացեի (Հաճախորդ ID " + oc.clientId.ToString() + ")";
                                db.group.Add(gr);
                                db.SaveChanges();
                                cgroup.groupId = gr.groupId;
                                cgroup.clientId = client.c.clientId;
                                cgroup.relType = 6;
                                db.clientsGroup.Add(cgroup);
                                db.SaveChanges();
                                groupId = gr.groupId;
                            }

                            cgroup.groupId = groupId;
                            cgroup.clientId = oc.clientId;
                            cgroup.relType = 6;
                            db.clientsGroup.Add(cgroup);
                            db.SaveChanges();

                            isExist++;
                        }//foreach (var oc in oldclients)
                    }
                }// if(groupId < 1)

                return RedirectToAction("Edit", "Clients", new { clientId = client.c.clientId });
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
            //Models.clientWorkDatas cwModel = new clientWorkDatas();
            //var cw = (from w in db.clientWorkDatas
            //          where w.clientId == ClientID

            //          select new
            //          {
            //              w.clientId,
            //              w.companyAddress,
            //              w.companyName,
            //              w.CompanyTel,
            //              w.companyTypeId,
            //              w.CreatedDate,
            //              w.employmentTypeId,
            //              w.empRegDate,
            //              w.Id,
            //              w.jobTitle,
            //              w.LastModifDate,
            //              w.note1,
            //              w.note2,
            //              w.note3,
            //              w.note4,
            //              w.note5,
            //              w.otherIncome,
            //              w.otherIncomeDescr,
            //              w.salary,
            //              w.taxNumber,
            //              w.userId

            //          }).ToList();

            //foreach (var t in cw)
            //{
            //    cwModel.clientId = t.clientId;
            //    cwModel.companyAddress = t.companyAddress;
            //    cwModel.companyName = t.companyName;
            //    cwModel.CompanyTel = t.CompanyTel;
            //    cwModel.companyTypeId = t.companyTypeId;
            //    cwModel.CreatedDate = t.CreatedDate;
            //    cwModel.employmentTypeId = t.employmentTypeId;
            //    cwModel.empRegDate = t.empRegDate;
            //    cwModel.Id = t.Id;
            //    cwModel.jobTitle = t.jobTitle;
            //    cwModel.LastModifDate = t.LastModifDate;
            //    cwModel.note1 = t.note1;
            //    cwModel.note2 = t.note2;
            //    cwModel.note3 = t.note3;
            //    cwModel.note4 = t.note4;
            //    cwModel.note5 = t.note5;
            //    cwModel.otherIncome = t.otherIncome;
            //    cwModel.otherIncomeDescr = t.otherIncomeDescr;
            //    cwModel.salary = t.salary;
            //    cwModel.taxNumber = t.taxNumber;
            //    cwModel.userId = t.userId;
            //}//foreach(var t in cw)



            Models.clientWorkDatas cwModel = db.clientWorkDatas.Where(p => p.clientId == ClientID).OrderByDescending(p => p.Id).FirstOrDefault();


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

                //var uCWD = db.clientWorkDatas.Where(c => c.Id == cw.Id).FirstOrDefault();
                //if(uCWD==null || uCWD?.Id == 0)
                //{
                //    uCWD = new clientWorkDatas();
                //    db.clientWorkDatas.Add(uCWD);
                //    db.SaveChanges();
                //} else
                //{
                //    db.Entry(uCWD).CurrentValues.SetValues(cw);
                //}



                if (cw.Id > 0)
                {
                    db.Entry(cw).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.clientWorkDatas.Add(cw);
                    db.SaveChanges();

                }



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
        public ActionResult EditCompany(long? clientId)
        {
            if (clientId == null)
                return RedirectToAction("Create");



            ViewBag.EditClientId = clientId;

            Models.clientFullViewModel clientFull = new Models.clientFullViewModel();


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

            //Models.clientWorkDatas cwModel = new clientWorkDatas();
            //var cw = (from w in db.clientWorkDatas
            //          where w.clientId == clientIdL
            //          select new
            //          {
            //              w.clientId,
            //              w.companyAddress,
            //              w.companyName,
            //              w.CompanyTel,
            //              w.companyTypeId,
            //              w.CreatedDate,
            //              w.employmentTypeId,
            //              w.empRegDate,
            //              w.Id,
            //              w.jobTitle,
            //              w.LastModifDate,
            //              w.note1,
            //              w.note2,
            //              w.note3,
            //              w.note4,
            //              w.note5,
            //              w.otherIncome,
            //              w.otherIncomeDescr,
            //              w.salary,
            //              w.taxNumber,
            //              w.userId
            //          });

            //foreach (var t in cw)
            //{
            //    cwModel.clientId = t.clientId;
            //    cwModel.companyAddress = t.companyAddress;
            //    cwModel.companyName = t.companyName;
            //    cwModel.CompanyTel = t.CompanyTel;
            //    cwModel.companyTypeId = t.companyTypeId;
            //    cwModel.CreatedDate = t.CreatedDate;
            //    cwModel.employmentTypeId = t.employmentTypeId;
            //    cwModel.empRegDate = t.empRegDate;
            //    cwModel.Id = t.Id;
            //    cwModel.jobTitle = t.jobTitle;
            //    cwModel.LastModifDate = t.LastModifDate;
            //    cwModel.note1 = t.note1;
            //    cwModel.note2 = t.note2;
            //    cwModel.note3 = t.note3;
            //    cwModel.note4 = t.note4;
            //    cwModel.note5 = t.note5;
            //    cwModel.otherIncome = t.otherIncome;
            //    cwModel.otherIncomeDescr = t.otherIncomeDescr;
            //    cwModel.salary = t.salary;
            //    cwModel.taxNumber = t.taxNumber;
            //    cwModel.userId = t.userId;
            //}//foreach(var t in cw)


            Models.clientWorkDatas cwModel = db.clientWorkDatas.Where(p => p.clientId == clientIdL).OrderByDescending(p => p.Id).FirstOrDefault();

            if (cwModel == null)
            {
                cwModel = new clientWorkDatas();
                cwModel.clientId = clientIdL;
            }





            //BalanceViewModel bvm = new BalanceViewModel();
            //Balance balance = db.Balance.Where(p => p.clientId == clientIdL).OrderByDescending(p => p.Id).FirstOrDefault();


            //if (balance != null)
            //{
            //    bvm = new BalanceViewModel(balance);

            //}



            IncomeExpensesViewModel ivm = new IncomeExpensesViewModel();

            IncomeExpenses Inc = db.IncomeExpenses.Where(p => p.applicationId == clientIdL).OrderByDescending(p => p.Id).FirstOrDefault();


            if (Inc != null)
            {


                ivm = new IncomeExpensesViewModel(Inc);



            }

            long groupId = 0;

            #region ClientGroup


            var clientsGroupItem = db.clientsGroup.Where(g => g.clientId == clientIdL).FirstOrDefault();

            if (clientsGroupItem != null && clientsGroupItem.groupId > 0)
            {
                ViewBag.IsGroupId = true;
                ViewBag.GroupId = clientsGroupItem.groupId;
                groupId = clientsGroupItem.groupId;
            }
            else
            {
                ViewBag.IsGroupId = false;
                ViewBag.GroupId = 0;
                groupId = 0;
            }







            //var clientsGroup = (from cg in db.clientsGroup
            //                    join r in db.releationType on cg.relType equals r.releationTypeId
            //                    join c in db.clients on cg.clientId equals c.clientId
            //                    join g in db.@group on cg.groupId equals g.groupId
            //                    select new
            //                    {
            //                        cg.clientsGroupId,
            //                        cg.groupId,
            //                        cg.clientId,
            //                        cg.note1,
            //                        cg.note2,
            //                        cg.note3,
            //                        cg.relType,
            //                        rt = r.relType,
            //                        c.clientName,
            //                        c.clientLastName,
            //                        g.gruopFullName
            //                    }).ToList();


            //db.clientsGroup.Include(c => c.releationType);
            //if (groupId > 0)

            //{
            //    clientsGroup = (from cg in db.clientsGroup
            //                    where cg.groupId == groupId
            //                    join r in db.releationType on cg.relType equals r.releationTypeId
            //                    join c in db.clients on cg.clientId equals c.clientId
            //                    join g in db.@group on cg.groupId equals g.groupId
            //                    select new
            //                    {
            //                        cg.clientsGroupId,
            //                        cg.groupId,
            //                        cg.clientId,
            //                        cg.note1,
            //                        cg.note2,
            //                        cg.note3,
            //                        cg.relType,
            //                        rt = r.relType,
            //                        c.clientName,
            //                        c.clientLastName,
            //                        g.gruopFullName
            //                    }).ToList();


            //    clientsGroupDetView cgView;


            //    foreach (var cg in clientsGroup)
            //    {
            //        if (cg.clientId ==clientId.Value)
            //            continue;
            //        cgView = new ModelsView.clientsGroupDetView();
            //        cgView.clientsGroupId = cg.clientsGroupId;
            //        cgView.groupId = cg.groupId;
            //        cgView.groupName = cg.gruopFullName;
            //        cgView.relType = cg.relType;
            //        cgView.rpFirstName = cg.clientName;
            //        cgView.rpLastName = cg.clientLastName;
            //        cgView.rTypeName = cg.rt;

            //        cgView.clientId = cg.clientId;

            //        cgList.Add(cgView);
            //    }



            //}
            #endregion //ClientGroup





            //ViewBag.Balance = bvm;
            //ViewBag.IncomeExpenses = ivm;





            List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();
            cgList = clientsGroupDetView.GetClientsGroupDetViewList(groupId);
            ViewBag.ClientsGroup = cgList;






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




            clientFull.c = clientM;
            clientFull.cw = cwModel;

            var appList = db.applications.Where(p => p.clientId == clientFull.c.clientId).Select(p => p.applicationId).ToList();

            //ViewBag.FileTable = db.DocsApllications.Where(p => appList.Contains(p.ApplicationId)).ToList();



            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name");
            ViewBag.BusinessTypeId = new SelectList(db.BusinessType, "Id", "Name");
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName");
            ViewBag.GenderId = new SelectList(db.clientSexes, "clientSexId", "sex");
            ViewBag.NameofBanksId = new SelectList(db.NameofBanks, "Id", "Name");
            ViewBag.OwnershipTypeId = new SelectList(db.OwnershipType, "Id", "Name");




            return View(clientFull);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditCompany(clientFullViewModel clientFull)
        {
            Models.clients client = clientFull.c;


            Models.clientWorkDatas cw = clientFull.cw;

            var appList = db.applications.Where(p => p.clientId == clientFull.c.clientId).Select(p => p.applicationId).ToList();

            //ViewBag.FileTable = db.DocsApllications.Where(p => appList.Contains(p.ApplicationId)).ToList();

            ViewBag.EditClientId = clientFull.c.clientId;

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

                ViewBag.cSaved = "Փոփոխությունները պահպանված են:1";






                cw.userId = User.Identity.GetUserId();
                cw.LastModifDate = DateTime.Now;

                //



                if (cw.Id > 0)
                {
                    db.Entry(cw).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.clientWorkDatas.Add(cw);
                    db.SaveChanges();

                }

                //var uCWD = db.clientWorkDatas.Where(c => c.Id == cw.Id).FirstOrDefault();
                //if (uCWD == null || uCWD?.Id == 0)
                //{
                //    uCWD = new clientWorkDatas();

                //}
                //else
                //{
                //    db.Entry(uCWD).CurrentValues.SetValues(cw);
                //}







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





            //BalanceViewModel bvm = new BalanceViewModel();
            //Balance balance = db.Balance.Where(p => p.clientId == client.clientId).OrderByDescending(p => p.Id).FirstOrDefault();


            //if (balance != null)
            //{
            //    bvm = new BalanceViewModel(balance);

            //}






            long groupId = 0;

            #region ClientGroup


            var clientsGroupItem = db.clientsGroup.Where(g => g.clientId == client.clientId).FirstOrDefault();

            if (clientsGroupItem != null && clientsGroupItem.groupId > 0)
            {
                ViewBag.IsGroupId = true;
                ViewBag.GroupId = clientsGroupItem.groupId;
                groupId = clientsGroupItem.groupId;
            }
            else
            {
                ViewBag.IsGroupId = false;
                ViewBag.GroupId = 0;
                groupId = 0;
            }






            //var clientsGroup = (from cg in db.clientsGroup
            //                    join r in db.releationType on cg.relType equals r.releationTypeId
            //                    join c in db.clients on cg.clientId equals c.clientId
            //                    join g in db.@group on cg.groupId equals g.groupId
            //                    select new
            //                    {
            //                        cg.clientsGroupId,
            //                        cg.groupId,
            //                        cg.clientId,
            //                        cg.note1,
            //                        cg.note2,
            //                        cg.note3,
            //                        cg.relType,
            //                        rt = r.relType,
            //                        c.clientName,
            //                        c.clientLastName,
            //                        g.gruopFullName
            //                    }).ToList();


            //db.clientsGroup.Include(c => c.releationType);


            //clientsGroupDetView cgView = new clientsGroupDetView();
            //List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();

            //if (groupId > 0)
            //{
            //    var clientsGroup = (from cg in db.clientsGroup
            //                        where cg.groupId == groupId
            //                        join r in db.releationType on cg.relType equals r.releationTypeId
            //                        join c in db.clients on cg.clientId equals c.clientId
            //                        join g in db.@group on cg.groupId equals g.groupId
            //                        select new
            //                        {
            //                            cg.clientsGroupId,
            //                            cg.groupId,
            //                            cg.clientId,
            //                            cg.note1,
            //                            cg.note2,
            //                            cg.note3,
            //                            cg.relType,

            //                            rt = r.relType,
            //                            c.clientName,
            //                            c.clientLastName,
            //                            g.gruopFullName
            //                        }).ToList();




            //    foreach (var cg in clientsGroup)
            //    {
            //        if (cg.clientId == clientFull.c.clientId)
            //            continue;

            //        cgView = new ModelsView.clientsGroupDetView();
            //        cgView.clientsGroupId = cg.clientsGroupId;
            //        cgView.groupId = cg.groupId;
            //        cgView.groupName = cg.gruopFullName;
            //        cgView.relType = cg.relType;
            //        cgView.rpFirstName = cg.clientName;
            //        cgView.rpLastName = cg.clientLastName;
            //        cgView.rTypeName = cg.rt;

            //        cgView.clientId = cg.clientId;

            //        cgList.Add(cgView);
            //    }


            //}

            #endregion //ClientGroup





            //ViewBag.Balance = bvm;
            //ViewBag.IncomeExpenses = ivm;



            List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();
            cgList = clientsGroupDetView.GetClientsGroupDetViewList(groupId);
            ViewBag.ClientsGroup = cgList;










            ViewBag.rg = new SelectList(regs, "rRegion", "rRegion");
            ViewBag.ct = new SelectList(cityes, "rCity", "rCity");
            ViewBag.cct = new SelectList(ccityes, "rCity", "rCity");

            var sex = (from s in db.clientSexes select new { s.clientSexId, s.sex }).ToList();
            var edu = (from e in db.educations select new { e.educationId, e.Education }).ToList();
            ViewBag.sx = new SelectList(sex, "clientSexId", "sex");
            ViewBag.ed = new SelectList(edu, "educationId", "Education");

            ViewBag.Streets = new SelectList(Streets, "rStreet", "rStreet");
            ViewBag.cStreets = new SelectList(cStreets, "rStreet", "rStreet");





            //Models.clientWorkDatas cwModel = new clientWorkDatas();
            //var cw = (from w in db.clientWorkDatas
            //          where w.clientId == client.clientId
            //          select new
            //          {
            //              w.clientId,
            //              w.companyAddress,
            //              w.companyName,
            //              w.CompanyTel,
            //              w.companyTypeId,
            //              w.CreatedDate,
            //              w.employmentTypeId,
            //              w.empRegDate,
            //              w.Id,
            //              w.jobTitle,
            //              w.LastModifDate,
            //              w.note1,
            //              w.note2,
            //              w.note3,
            //              w.note4,
            //              w.note5,
            //              w.otherIncome,
            //              w.otherIncomeDescr,
            //              w.salary,
            //              w.taxNumber,
            //              w.userId
            //          });

            //foreach (var t in cw)
            //{
            //    cwModel.clientId = t.clientId;
            //    cwModel.companyAddress = t.companyAddress;
            //    cwModel.companyName = t.companyName;
            //    cwModel.CompanyTel = t.CompanyTel;
            //    cwModel.companyTypeId = t.companyTypeId;
            //    cwModel.CreatedDate = t.CreatedDate;
            //    cwModel.employmentTypeId = t.employmentTypeId;
            //    cwModel.empRegDate = t.empRegDate;
            //    cwModel.Id = t.Id;
            //    cwModel.jobTitle = t.jobTitle;
            //    cwModel.LastModifDate = t.LastModifDate;
            //    cwModel.note1 = t.note1;
            //    cwModel.note2 = t.note2;
            //    cwModel.note3 = t.note3;
            //    cwModel.note4 = t.note4;
            //    cwModel.note5 = t.note5;
            //    cwModel.otherIncome = t.otherIncome;
            //    cwModel.otherIncomeDescr = t.otherIncomeDescr;
            //    cwModel.salary = t.salary;
            //    cwModel.taxNumber = t.taxNumber;
            //    cwModel.userId = t.userId;
            //}//foreach(var t in cw)

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



            ViewBag.BusinessSectorId = new SelectList(db.BusinessSector, "Id", "Name");
            ViewBag.BusinessTypeId = new SelectList(db.BusinessType, "Id", "Name");
            ViewBag.clientId = new SelectList(db.clients, "clientId", "clientName");
            ViewBag.GenderId = new SelectList(db.clientSexes, "clientSexId", "sex");
            ViewBag.NameofBanksId = new SelectList(db.NameofBanks, "Id", "Name");
            ViewBag.OwnershipTypeId = new SelectList(db.OwnershipType, "Id", "Name");






            clientFull.c = client;
            clientFull.cw = cw;

            return View(clientFull);
        }





        [Authorize]
        public ActionResult Edit(long? clientId)
        {
            if (clientId == null)
                return RedirectToAction("Create");



            ViewBag.EditClientId = clientId;

            ViewBag.FileTable = db.DocsApllications.Where(p => p.clientId == clientId).ToList();

            Models.clientFullViewModel clientFull = new Models.clientFullViewModel();


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
                              tel = c.tel,
                              email = c.Email
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
                clientM.Email = c.email;
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

            //Models.clientWorkDatas cwModel = new clientWorkDatas();
            //var cw = (from w in db.clientWorkDatas
            //          where w.clientId == clientIdL
            //          select new
            //          {
            //              w.clientId,
            //              w.companyAddress,
            //              w.companyName,
            //              w.CompanyTel,
            //              w.companyTypeId,
            //              w.CreatedDate,
            //              w.employmentTypeId,
            //              w.empRegDate,
            //              w.Id,
            //              w.jobTitle,
            //              w.LastModifDate,
            //              w.note1,
            //              w.note2,
            //              w.note3,
            //              w.note4,
            //              w.note5,
            //              w.otherIncome,
            //              w.otherIncomeDescr,
            //              w.salary,
            //              w.taxNumber,
            //              w.userId
            //          });

            //foreach (var t in cw)
            //{
            //    cwModel.clientId = t.clientId;
            //    cwModel.companyAddress = t.companyAddress;
            //    cwModel.companyName = t.companyName;
            //    cwModel.CompanyTel = t.CompanyTel;
            //    cwModel.companyTypeId = t.companyTypeId;
            //    cwModel.CreatedDate = t.CreatedDate;
            //    cwModel.employmentTypeId = t.employmentTypeId;
            //    cwModel.empRegDate = t.empRegDate;
            //    cwModel.Id = t.Id;
            //    cwModel.jobTitle = t.jobTitle;
            //    cwModel.LastModifDate = t.LastModifDate;
            //    cwModel.note1 = t.note1;
            //    cwModel.note2 = t.note2;
            //    cwModel.note3 = t.note3;
            //    cwModel.note4 = t.note4;
            //    cwModel.note5 = t.note5;
            //    cwModel.otherIncome = t.otherIncome;
            //    cwModel.otherIncomeDescr = t.otherIncomeDescr;
            //    cwModel.salary = t.salary;
            //    cwModel.taxNumber = t.taxNumber;
            //    cwModel.userId = t.userId;
            //}//foreach(var t in cw)


            Models.clientWorkDatas cwModel = db.clientWorkDatas.Where(p => p.clientId == clientIdL).OrderByDescending(p => p.Id).FirstOrDefault();

            if (cwModel == null)
            {
                cwModel = new clientWorkDatas();
                cwModel.clientId = clientIdL;
            }





            //BalanceViewModel bvm = new BalanceViewModel();
            //Balance balance = db.Balance.Where(p => p.clientId == clientIdL).OrderByDescending(p => p.Id).FirstOrDefault();


            //if (balance != null)
            //{
            //    bvm = new BalanceViewModel(balance);

            //}



            IncomeExpensesViewModel ivm = new IncomeExpensesViewModel();

            IncomeExpenses Inc = db.IncomeExpenses.Where(p => p.applicationId == clientIdL).OrderByDescending(p => p.Id).FirstOrDefault();


            if (Inc != null)
            {


                ivm = new IncomeExpensesViewModel(Inc);



            }

            long groupId = 0;

            #region ClientGroup


            var clientsGroupItem = db.clientsGroup.Where(g => g.clientId == clientIdL).FirstOrDefault();

            if (clientsGroupItem != null && clientsGroupItem.groupId > 0)
            {
                ViewBag.IsGroupId = true;
                ViewBag.GroupId = clientsGroupItem.groupId;
                groupId = clientsGroupItem.groupId;
            }
            else
            {
                ViewBag.IsGroupId = false;
                ViewBag.GroupId = 0;
                groupId = 0;
            }







            //var clientsGroup = (from cg in db.clientsGroup
            //                    join r in db.releationType on cg.relType equals r.releationTypeId
            //                    join c in db.clients on cg.clientId equals c.clientId
            //                    join g in db.@group on cg.groupId equals g.groupId
            //                    select new
            //                    {
            //                        cg.clientsGroupId,
            //                        cg.groupId,
            //                        cg.clientId,
            //                        cg.note1,
            //                        cg.note2,
            //                        cg.note3,
            //                        cg.relType,
            //                        rt = r.relType,
            //                        c.clientName,
            //                        c.clientLastName,
            //                        g.gruopFullName
            //                    }).ToList();


            //db.clientsGroup.Include(c => c.releationType);
            //if (groupId > 0)

            //{
            //    clientsGroup = (from cg in db.clientsGroup
            //                    where cg.groupId == groupId
            //                    join r in db.releationType on cg.relType equals r.releationTypeId
            //                    join c in db.clients on cg.clientId equals c.clientId
            //                    join g in db.@group on cg.groupId equals g.groupId
            //                    select new
            //                    {
            //                        cg.clientsGroupId,
            //                        cg.groupId,
            //                        cg.clientId,
            //                        cg.note1,
            //                        cg.note2,
            //                        cg.note3,
            //                        cg.relType,
            //                        rt = r.relType,
            //                        c.clientName,
            //                        c.clientLastName,
            //                        g.gruopFullName
            //                    }).ToList();


            //    clientsGroupDetView cgView;


            //    foreach (var cg in clientsGroup)
            //    {
            //        if (cg.clientId ==clientId.Value)
            //            continue;
            //        cgView = new ModelsView.clientsGroupDetView();
            //        cgView.clientsGroupId = cg.clientsGroupId;
            //        cgView.groupId = cg.groupId;
            //        cgView.groupName = cg.gruopFullName;
            //        cgView.relType = cg.relType;
            //        cgView.rpFirstName = cg.clientName;
            //        cgView.rpLastName = cg.clientLastName;
            //        cgView.rTypeName = cg.rt;

            //        cgView.clientId = cg.clientId;

            //        cgList.Add(cgView);
            //    }



            //}
            #endregion //ClientGroup





            //ViewBag.Balance = bvm;
            //ViewBag.IncomeExpenses = ivm;





            List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();
            cgList = clientsGroupDetView.GetClientsGroupDetViewList(groupId);
            ViewBag.ClientsGroup = cgList;






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




            clientFull.c = clientM;
            clientFull.cw = cwModel;
            clientFull.matchingClients = new List<Models.MatchingClientViewModel>();

            var mtclients = (from mc in db.clients
                            where ((mc.rRegion == clientFull.c.rRegion && mc.rCity == clientFull.c.rCity && mc.rBuilding == clientFull.c.rBuilding && mc.rApartment == clientFull.c.rApartment && mc.clientId != clientFull.c.clientId) ||
                                  (mc.cRegion == clientFull.c.rRegion && mc.cCity == clientFull.c.rCity && mc.cBuilding == clientFull.c.rBuilding && mc.cApartment == clientFull.c.rApartment && mc.clientId != clientFull.c.clientId)) && (mc.rRegion != "R" && mc.mob1 != "091000000")
                             select new { mc.clientId, mc.clientName, mc.clientLastName, mc.socNumb }).ToList();


            MatchingClientViewModel mclient;
            foreach(var mc in mtclients)
            {
                mclient = new MatchingClientViewModel();
                mclient.clientId = mc.clientId;
                mclient.firstName = mc.clientName;
                mclient.lastName = mc.clientLastName;
                mclient.socN = mc.socNumb;
                mclient.matchingMode = "Գրանցման հասցե";
                mclient.sefLoanSum = CommonFunction.GetSefLoansSumFromACRAProfile(mc.clientId, "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ");
                mclient.mClientGroupId = groupId;
                mclient.mClientId = clientM.clientId;
                clientFull.matchingClients.Add(mclient);
            }//foreach(var mc in mtclients)

            if (clientFull.c.cRegion != null)
            {
                if (clientFull.c.cRegion.Length > 0)
                {
                    mtclients = (from mc in db.clients
                                 where ((mc.rRegion == clientFull.c.cRegion && mc.rCity == clientFull.c.cCity && mc.rBuilding == clientFull.c.cBuilding && mc.rApartment == clientFull.c.cApartment && mc.clientId != clientFull.c.clientId) ||
                                  (mc.cRegion == clientFull.c.cRegion && mc.cCity == clientFull.c.cCity && mc.cBuilding == clientFull.c.cBuilding && mc.cApartment == clientFull.c.cApartment && mc.clientId != clientFull.c.clientId )) && (mc.rRegion != "R" && mc.mob1 != "091000000")
                                 select new { mc.clientId, mc.clientName, mc.clientLastName, mc.socNumb }).ToList();

                    foreach (var mc in mtclients)
                    {
                        mclient = new MatchingClientViewModel();
                        mclient.clientId = mc.clientId;
                        mclient.firstName = mc.clientName;
                        mclient.lastName = mc.clientLastName;
                        mclient.socN = mc.socNumb;
                        mclient.matchingMode = "Փաստացի հասցե";
                        mclient.sefLoanSum = CommonFunction.GetSefLoansSumFromACRAProfile(mc.clientId, "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ");
                        mclient.mClientGroupId = groupId;
                        mclient.mClientId = clientM.clientId;
                        clientFull.matchingClients.Add(mclient);
                    }//foreach(var mc in mtclients)
                }//if(clientFull.c.cRegion.Length > 0)
            }//if (clientFull.c.cRegion != null)


            mtclients = (from mc in db.clients
                         where (mc.mob1 == clientFull.c.mob1 || mc.mob2 == clientFull.c.mob1 || mc.mob3 == clientFull.c.mob1 || mc.mob4 == clientFull.c.mob1) && mc.clientId != clientFull.c.clientId && (mc.rRegion != "R" && mc.mob1 != "091000000")
                         select new { mc.clientId, mc.clientName, mc.clientLastName, mc.socNumb }).ToList();

            foreach (var mc in mtclients)
            {
                mclient = new MatchingClientViewModel();
                mclient.clientId = mc.clientId;
                mclient.firstName = mc.clientName;
                mclient.lastName = mc.clientLastName;
                mclient.socN = mc.socNumb;
                mclient.matchingMode = "Բջջ. " + clientFull.c.mob1;
                mclient.sefLoanSum = CommonFunction.GetSefLoansSumFromACRAProfile(mc.clientId, "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ");
                mclient.mClientGroupId = groupId;
                mclient.mClientId = clientM.clientId;
                clientFull.matchingClients.Add(mclient);
            }//foreach(var mc in mtclients)


            if(clientFull.c.Email != null)
            {
                if (clientFull.c.Email.Length > 0)
                {
                    mtclients = (from mc in db.clients
                                 where (mc.Email == clientFull.c.Email) && mc.clientId != clientFull.c.clientId && (mc.rRegion != "R" && mc.mob1 != "091000000")
                                 select new { mc.clientId, mc.clientName, mc.clientLastName, mc.socNumb }).ToList();

                    foreach (var mc in mtclients)
                    {
                        mclient = new MatchingClientViewModel();
                        mclient.clientId = mc.clientId;
                        mclient.firstName = mc.clientName;
                        mclient.lastName = mc.clientLastName;
                        mclient.socN = mc.socNumb;
                        mclient.matchingMode = "Էլ. փոստ " + clientFull.c.Email;
                        mclient.sefLoanSum = CommonFunction.GetSefLoansSumFromACRAProfile(mc.clientId, "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ");
                        mclient.mClientGroupId = groupId;
                        mclient.mClientId = clientM.clientId;
                        clientFull.matchingClients.Add(mclient);
                    }//foreach(var mc in mtclients)
                }//if (clientFull.c.Email.Length > 0)
            }//if(clientFull.c.Email != null)



            if (clientFull.cw.companyName != null)
            {
                if (clientFull.cw.companyName.Length > 0)
                {
                    mtclients = (from mc in db.clients
                                 join wp in db.clientWorkDatas on mc.clientId equals wp.clientId
                                 where wp.companyName == clientFull.cw.companyName && mc.clientId != clientFull.cw.clientId && (mc.rRegion != "R" && mc.mob1 != "091000000")
                                 select new { mc.clientId, mc.clientName, mc.clientLastName, mc.socNumb }).ToList();

                    foreach (var mc in mtclients)
                    {
                        mclient = new MatchingClientViewModel();
                        mclient.clientId = mc.clientId;
                        mclient.firstName = mc.clientName;
                        mclient.lastName = mc.clientLastName;
                        mclient.socN = mc.socNumb;
                        mclient.matchingMode = "Աշխատավայր";
                        mclient.sefLoanSum = CommonFunction.GetSefLoansSumFromACRAProfile(mc.clientId, "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ");
                        mclient.mClientGroupId = groupId;
                        mclient.mClientId = clientM.clientId;
                        clientFull.matchingClients.Add(mclient);
                    }//foreach(var mc in mtclients)
                }//if (clientFull.cw.companyName.Length > 0)
            }//if(clientFull.cw.companyName != null)



            //var appList = db.applications.Where(p => p.clientId == clientFull.c.clientId).Select(p => p.applicationId).ToList();

            //ViewBag.FileTable = db.DocsApllications.Where(p => appList.Contains(p.ApplicationId)).ToList();




            return View(clientFull);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(clientFullViewModel clientFull)
        {
            Models.clients client = clientFull.c;


            Models.clientWorkDatas cw = clientFull.cw;

            //var appList = db.applications.Where(p => p.clientId == clientFull.c.clientId).Select(p => p.applicationId).ToList();

            //ViewBag.FileTable = db.DocsApllications.Where(p => appList.Contains(p.ApplicationId)).ToList();

            ViewBag.EditClientId = clientFull.c.clientId;

            ViewBag.FileTable = db.DocsApllications.Where(p => p.clientId == clientFull.c.clientId).ToList();

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
                if (clientFull.c.fMemb < (clientFull.c.fMemb + clientFull.c.fTenMemb))
                    clientFull.c.fMemb = (clientFull.c.fMemb + clientFull.c.fTenMemb);

                client.regionId = CommonFunction.RegionIdGetFromName(client.rRegion.Trim());
                var cl = db.clients.Where(c => c.clientId == client.clientId).FirstOrDefault();
                cl.fMemb = clientFull.c.fMemb;
                db.Entry(cl).CurrentValues.SetValues(client);
                db.SaveChanges();

                ViewBag.cSaved = "Փոփոխությունները պահպանված են:1";






                cw.userId = User.Identity.GetUserId();
                cw.LastModifDate = DateTime.Now;

                //



                if (cw.Id > 0)
                {
                    db.Entry(cw).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.clientWorkDatas.Add(cw);
                    db.SaveChanges();

                }

                //var uCWD = db.clientWorkDatas.Where(c => c.Id == cw.Id).FirstOrDefault();
                //if (uCWD == null || uCWD?.Id == 0)
                //{
                //    uCWD = new clientWorkDatas();

                //}
                //else
                //{
                //    db.Entry(uCWD).CurrentValues.SetValues(cw);
                //}







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





            //BalanceViewModel bvm = new BalanceViewModel();
            //Balance balance = db.Balance.Where(p => p.clientId == client.clientId).OrderByDescending(p => p.Id).FirstOrDefault();


            //if (balance != null)
            //{
            //    bvm = new BalanceViewModel(balance);

            //}






            long groupId = 0;

            #region ClientGroup


            var clientsGroupItem = db.clientsGroup.Where(g => g.clientId == client.clientId).FirstOrDefault();

            if (clientsGroupItem != null && clientsGroupItem.groupId > 0)
            {
                ViewBag.IsGroupId = true;
                ViewBag.GroupId = clientsGroupItem.groupId;
                groupId = clientsGroupItem.groupId;
            }
            else
            {
                ViewBag.IsGroupId = false;
                ViewBag.GroupId = 0;
                groupId = 0;
            }






            //var clientsGroup = (from cg in db.clientsGroup
            //                    join r in db.releationType on cg.relType equals r.releationTypeId
            //                    join c in db.clients on cg.clientId equals c.clientId
            //                    join g in db.@group on cg.groupId equals g.groupId
            //                    select new
            //                    {
            //                        cg.clientsGroupId,
            //                        cg.groupId,
            //                        cg.clientId,
            //                        cg.note1,
            //                        cg.note2,
            //                        cg.note3,
            //                        cg.relType,
            //                        rt = r.relType,
            //                        c.clientName,
            //                        c.clientLastName,
            //                        g.gruopFullName
            //                    }).ToList();


            //db.clientsGroup.Include(c => c.releationType);


            //clientsGroupDetView cgView = new clientsGroupDetView();
            //List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();

            //if (groupId > 0)
            //{
            //    var clientsGroup = (from cg in db.clientsGroup
            //                        where cg.groupId == groupId
            //                        join r in db.releationType on cg.relType equals r.releationTypeId
            //                        join c in db.clients on cg.clientId equals c.clientId
            //                        join g in db.@group on cg.groupId equals g.groupId
            //                        select new
            //                        {
            //                            cg.clientsGroupId,
            //                            cg.groupId,
            //                            cg.clientId,
            //                            cg.note1,
            //                            cg.note2,
            //                            cg.note3,
            //                            cg.relType,

            //                            rt = r.relType,
            //                            c.clientName,
            //                            c.clientLastName,
            //                            g.gruopFullName
            //                        }).ToList();




            //    foreach (var cg in clientsGroup)
            //    {
            //        if (cg.clientId == clientFull.c.clientId)
            //            continue;

            //        cgView = new ModelsView.clientsGroupDetView();
            //        cgView.clientsGroupId = cg.clientsGroupId;
            //        cgView.groupId = cg.groupId;
            //        cgView.groupName = cg.gruopFullName;
            //        cgView.relType = cg.relType;
            //        cgView.rpFirstName = cg.clientName;
            //        cgView.rpLastName = cg.clientLastName;
            //        cgView.rTypeName = cg.rt;

            //        cgView.clientId = cg.clientId;

            //        cgList.Add(cgView);
            //    }


            //}

            #endregion //ClientGroup





            //ViewBag.Balance = bvm;
            //ViewBag.IncomeExpenses = ivm;



            List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();
            cgList = clientsGroupDetView.GetClientsGroupDetViewList(groupId);
            ViewBag.ClientsGroup = cgList;










            ViewBag.rg = new SelectList(regs, "rRegion", "rRegion");
            ViewBag.ct = new SelectList(cityes, "rCity", "rCity");
            ViewBag.cct = new SelectList(ccityes, "rCity", "rCity");

            var sex = (from s in db.clientSexes select new { s.clientSexId, s.sex }).ToList();
            var edu = (from e in db.educations select new { e.educationId, e.Education }).ToList();
            ViewBag.sx = new SelectList(sex, "clientSexId", "sex");
            ViewBag.ed = new SelectList(edu, "educationId", "Education");

            ViewBag.Streets = new SelectList(Streets, "rStreet", "rStreet");
            ViewBag.cStreets = new SelectList(cStreets, "rStreet", "rStreet");





            //Models.clientWorkDatas cwModel = new clientWorkDatas();
            //var cw = (from w in db.clientWorkDatas
            //          where w.clientId == client.clientId
            //          select new
            //          {
            //              w.clientId,
            //              w.companyAddress,
            //              w.companyName,
            //              w.CompanyTel,
            //              w.companyTypeId,
            //              w.CreatedDate,
            //              w.employmentTypeId,
            //              w.empRegDate,
            //              w.Id,
            //              w.jobTitle,
            //              w.LastModifDate,
            //              w.note1,
            //              w.note2,
            //              w.note3,
            //              w.note4,
            //              w.note5,
            //              w.otherIncome,
            //              w.otherIncomeDescr,
            //              w.salary,
            //              w.taxNumber,
            //              w.userId
            //          });

            //foreach (var t in cw)
            //{
            //    cwModel.clientId = t.clientId;
            //    cwModel.companyAddress = t.companyAddress;
            //    cwModel.companyName = t.companyName;
            //    cwModel.CompanyTel = t.CompanyTel;
            //    cwModel.companyTypeId = t.companyTypeId;
            //    cwModel.CreatedDate = t.CreatedDate;
            //    cwModel.employmentTypeId = t.employmentTypeId;
            //    cwModel.empRegDate = t.empRegDate;
            //    cwModel.Id = t.Id;
            //    cwModel.jobTitle = t.jobTitle;
            //    cwModel.LastModifDate = t.LastModifDate;
            //    cwModel.note1 = t.note1;
            //    cwModel.note2 = t.note2;
            //    cwModel.note3 = t.note3;
            //    cwModel.note4 = t.note4;
            //    cwModel.note5 = t.note5;
            //    cwModel.otherIncome = t.otherIncome;
            //    cwModel.otherIncomeDescr = t.otherIncomeDescr;
            //    cwModel.salary = t.salary;
            //    cwModel.taxNumber = t.taxNumber;
            //    cwModel.userId = t.userId;
            //}//foreach(var t in cw)

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




            clientFull.c = client;
            clientFull.cw = cw;

            clientFull.matchingClients = new List<Models.MatchingClientViewModel>();

            var mtclients = (from mc in db.clients
                             where ((mc.rRegion == clientFull.c.rRegion && mc.rCity == clientFull.c.rCity && mc.rBuilding == clientFull.c.rBuilding && mc.rApartment == clientFull.c.rApartment && mc.clientId != clientFull.c.clientId) ||
                                   (mc.cRegion == clientFull.c.rRegion && mc.cCity == clientFull.c.rCity && mc.cBuilding == clientFull.c.rBuilding && mc.cApartment == clientFull.c.rApartment && mc.clientId != clientFull.c.clientId)) && (mc.rRegion != "R" && mc.mob1 != "091000000")
                             select new { mc.clientId, mc.clientName, mc.clientLastName, mc.socNumb }).ToList();


            MatchingClientViewModel mclient;
            foreach (var mc in mtclients)
            {
                mclient = new MatchingClientViewModel();
                mclient.clientId = mc.clientId;
                mclient.firstName = mc.clientName;
                mclient.lastName = mc.clientLastName;
                mclient.socN = mc.socNumb;
                mclient.matchingMode = "Գրանցման հասցե";
                mclient.sefLoanSum = CommonFunction.GetSefLoansSumFromACRAProfile(mc.clientId, "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ");
                mclient.mClientGroupId = groupId;
                mclient.mClientId = client.clientId;
                clientFull.matchingClients.Add(mclient);
            }//foreach(var mc in mtclients)

            if (clientFull.c.cRegion != null)
            {
                if (clientFull.c.cRegion.Length > 0)
                {
                    mtclients = (from mc in db.clients
                                 where ((mc.rRegion == clientFull.c.cRegion && mc.rCity == clientFull.c.cCity && mc.rBuilding == clientFull.c.cBuilding && mc.rApartment == clientFull.c.cApartment && mc.clientId != clientFull.c.clientId) ||
                                  (mc.cRegion == clientFull.c.cRegion && mc.cCity == clientFull.c.cCity && mc.cBuilding == clientFull.c.cBuilding && mc.cApartment == clientFull.c.cApartment && mc.clientId != clientFull.c.clientId)) && (mc.rRegion != "R" && mc.mob1 != "091000000")
                                 select new { mc.clientId, mc.clientName, mc.clientLastName, mc.socNumb }).ToList();

                    foreach (var mc in mtclients)
                    {
                        mclient = new MatchingClientViewModel();
                        mclient.clientId = mc.clientId;
                        mclient.firstName = mc.clientName;
                        mclient.lastName = mc.clientLastName;
                        mclient.socN = mc.socNumb;
                        mclient.matchingMode = "Փաստացի հասցե";
                        mclient.sefLoanSum = CommonFunction.GetSefLoansSumFromACRAProfile(mc.clientId, "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ");
                        mclient.mClientGroupId = groupId;
                        mclient.mClientId = client.clientId;
                        clientFull.matchingClients.Add(mclient);
                    }//foreach(var mc in mtclients)
                }//if(clientFull.c.cRegion.Length > 0)
            }//if (clientFull.c.cRegion != null)


            mtclients = (from mc in db.clients
                         where (mc.mob1 == clientFull.c.mob1 || mc.mob2 == clientFull.c.mob1 || mc.mob3 == clientFull.c.mob1 || mc.mob4 == clientFull.c.mob1) && mc.clientId != clientFull.c.clientId && (mc.rRegion != "R" && mc.mob1 != "091000000")
                         select new { mc.clientId, mc.clientName, mc.clientLastName, mc.socNumb }).ToList();

            foreach (var mc in mtclients)
            {
                mclient = new MatchingClientViewModel();
                mclient.clientId = mc.clientId;
                mclient.firstName = mc.clientName;
                mclient.lastName = mc.clientLastName;
                mclient.socN = mc.socNumb;
                mclient.matchingMode = "Բջջ. " + clientFull.c.mob1;
                mclient.sefLoanSum = CommonFunction.GetSefLoansSumFromACRAProfile(mc.clientId, "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ");
                mclient.mClientGroupId = groupId;
                mclient.mClientId = client.clientId;
                clientFull.matchingClients.Add(mclient);
            }//foreach(var mc in mtclients)


            if (clientFull.c.Email != null)
            {
                if (clientFull.c.Email.Length > 0)
                {
                    mtclients = (from mc in db.clients
                                 where (mc.Email == clientFull.c.Email) && mc.clientId != clientFull.c.clientId && (mc.rRegion != "R" && mc.mob1 != "091000000")
                                 select new { mc.clientId, mc.clientName, mc.clientLastName, mc.socNumb }).ToList();

                    foreach (var mc in mtclients)
                    {
                        mclient = new MatchingClientViewModel();
                        mclient.clientId = mc.clientId;
                        mclient.firstName = mc.clientName;
                        mclient.lastName = mc.clientLastName;
                        mclient.socN = mc.socNumb;
                        mclient.matchingMode = "Էլ. փոստ " + clientFull.c.Email;
                        mclient.sefLoanSum = CommonFunction.GetSefLoansSumFromACRAProfile(mc.clientId, "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ");
                        mclient.mClientGroupId = groupId;
                        mclient.mClientId = client.clientId;
                        clientFull.matchingClients.Add(mclient);
                    }//foreach(var mc in mtclients)
                }//if (clientFull.c.Email.Length > 0)
            }//if(clientFull.c.Email != null)



            if (clientFull.cw.companyName != null)
            {
                if (clientFull.cw.companyName.Length > 0)
                {
                    mtclients = (from mc in db.clients
                                 join wp in db.clientWorkDatas on mc.clientId equals wp.clientId
                                 where wp.companyName == clientFull.cw.companyName && mc.clientId != clientFull.cw.clientId && (mc.rRegion != "R" && mc.mob1 != "091000000")
                                 select new { mc.clientId, mc.clientName, mc.clientLastName, mc.socNumb }).ToList();

                    foreach (var mc in mtclients)
                    {
                        mclient = new MatchingClientViewModel();
                        mclient.clientId = mc.clientId;
                        mclient.firstName = mc.clientName;
                        mclient.lastName = mc.clientLastName;
                        mclient.socN = mc.socNumb;
                        mclient.matchingMode = "Աշխատավայր";
                        mclient.sefLoanSum = CommonFunction.GetSefLoansSumFromACRAProfile(mc.clientId, "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ");
                        mclient.mClientGroupId = groupId;
                        mclient.mClientId = client.clientId;
                        clientFull.matchingClients.Add(mclient);
                    }//foreach(var mc in mtclients)
                }//if (clientFull.cw.companyName.Length > 0)
            }//if(clientFull.cw.companyName != null)

            return View(clientFull);
        }


    }//public class ClientsController : Controller
}//namespace ASFront.Controllers