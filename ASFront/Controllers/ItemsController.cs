using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using ASFront.ModelsView;
using ASFront.Classes;

namespace ASFront.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index(int page = 1)
        {


            long ApplicationID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];
           

            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);



            ViewBag.isEdirable = CommonFunction.isApplicationEditable(ApplicationID, User.Identity.GetUserId(), CommonFunction.GetRolesForEditing());


            var items = new List<ItemViewModel>();

            if (ApplicationID > 0)
            {
                var app = db.applications.Find(ApplicationID);
                ViewBag.ApplicationID = ApplicationID;
                ViewBag.Client = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList().Select(p => p.clientName).FirstOrDefault();
                ViewBag.AppInfo = app.appDate.ToString();
                ViewBag.AppNumber = app.applicationId;


                items = (
                       from i in db.Items
                       join a in db.applications on i.applicationId equals a.applicationId
                       join c in db.clients on i.clientId equals c.clientId
                       join s in db.Suppliers on i.SupplierId equals s.SupplierId
                       join pp in db.ProductPurposes on i.FKProductPurposeId equals pp.Id
                       join pr in db.Products on pp.ProductId equals pr.productId
                       join pur in db.Purposes on pp.PurposeId equals pur.Id

                       where i.applicationId == ApplicationID

                       select new ItemViewModel()
                       {
                           Id = i.Id,

                           ItemName = i.ItemName,
                           ItemDescr = i.ItemDescr,
                           ClientInvest = i.ClientInvest,
                           Count = i.Count,
                           Price = i.Price,
                           Sum = i.Sum,

                           applicationName = a.agrNumb,
                           ProductPurposeName = pr.productName + " - " + pur.PurposeName,
                           clientName = c.clientName + " " + c.clientLastName + " " + c.clientMidName ?? "",
                           SupplierName = s.SupplierName
                       }


                    ).Distinct().ToList();

            }
            else
            {
                items = (
                     from i in db.Items
                     join a in db.applications on i.applicationId equals a.applicationId
                     join c in db.clients on i.clientId equals c.clientId
                     join s in db.Suppliers on i.SupplierId equals s.SupplierId
                     join pp in db.ProductPurposes on i.FKProductPurposeId equals pp.Id
                     join pr in db.Products on pp.ProductId equals pr.productId
                     join pur in db.Purposes on pp.PurposeId equals pur.Id

                   

                     select new ItemViewModel()
                     {
                         Id = i.Id,

                         ItemName = i.ItemName,
                         ItemDescr = i.ItemDescr,
                         ClientInvest = i.ClientInvest,
                         Count = i.Count,
                         Price = i.Price,
                         Sum = i.Sum,

                         applicationName = a.agrNumb,
                         ProductPurposeName = pr.productName + " - " + pur.PurposeName,
                         clientName = c.clientName + " " + c.clientLastName + " " + c.clientMidName ?? "",
                         SupplierName = s.SupplierName
                     }


                  ).Distinct().ToList();

            }


          

            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Items/Details/5


        // GET: Items/Create
        public ActionResult Create()
        {
            Items item = new Items();

            long ApplicationID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];


            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);

       

             if(ApplicationID < 1  )
            {
                return RedirectToAction("Index");
            }

            var app = db.applications.Where(p => p.applicationId == ApplicationID).FirstOrDefault();


            if (app==null)
            {
                return RedirectToAction("Index");
            }


            item.applicationId = ApplicationID;
            item.clientId = app.clientId;

            var SupplierId = db.Sellers.Where(p => p.Id == app.SellerId).Select(p => p.SupplierId).FirstOrDefault();

            item.SupplierId = SupplierId;

            ViewBag.ApplicationID = ApplicationID;
            ViewBag.Client = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList().Select(p => p.clientName).FirstOrDefault();
            ViewBag.AppInfo = app.appDate.ToString();
            ViewBag.AppNumber = app.applicationId;

            //var app = (from c in db.applications select new { c.applicationId, c.agrNumb });

            var appPrID = db.applications.Where(p => p.applicationId == item.applicationId).FirstOrDefault().productId;

            var prodPr = (
                 from pp in db.ProductPurposes
                 join pr in db.Products on pp.ProductId equals pr.productId
                 join pur in db.Purposes on pp.PurposeId equals pur.Id
                  where pr.productId==appPrID
                 select new ProductPurposeDropBoxViewModel() { Id = pp.Id, ProductPurposeName = pr.productName + " - " + pur.PurposeName }


                ).Distinct().ToList();

            var supp = db.Suppliers.Select(p => new { p.SupplierId, p.SupplierName }).Distinct().ToList();

            //var clients = db.clients.Distinct().Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList();
            //var pgrs = (from c in db.productGroups select new { c.productGroupId, c.prodGroupName }).Distinct().ToList();

            int prGrId = db.Products.Where(p => p.productId == app.productId).Select(g => g.productGroupId).SingleOrDefault();
            string prGroupName = db.productGroups.Where(p => p.productGroupId == prGrId).Select(g => g.prodGroupName).SingleOrDefault();
            ViewBag.prGroupName = prGroupName;


            ViewBag.prodPr = new SelectList(prodPr, "Id", "ProductPurposeName");
            ViewBag.supp = new SelectList(supp, "SupplierId", "SupplierName");
            //ViewBag.app = new SelectList(app, "applicationId", "agrNumb");


            //ViewBag.pgr = new SelectList(pgrs, "productGroupId", "prodGroupName",app.productId);
            //ViewBag.cl = new SelectList(clients, "clientId", "clientName");


            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;
            return View(item);
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Items item)
        {

            if (item.applicationId < 1)
            {
                return RedirectToAction("Index", "Application");
            }


            var appPrID = db.applications.Where(p => p.applicationId == item.applicationId).FirstOrDefault().productId;

            var prodPr = (
                 from pp in db.ProductPurposes
                 join pr in db.Products on pp.ProductId equals pr.productId
                 join pur in db.Purposes on pp.PurposeId equals pur.Id
                 where pr.productId == appPrID
                 select new ProductPurposeDropBoxViewModel() { Id = pp.Id, ProductPurposeName = pr.productName + " - " + pur.PurposeName }


                ).Distinct().ToList();

            double prodLimitMin = db.Products.Where(p => p.productId == appPrID).Select(s => s.minAmount).SingleOrDefault(); 
            double prodLimitMax = db.Products.Where(p => p.productId == appPrID).Select(s => s.maxAmount).SingleOrDefault();            

            var oldItems = db.Items.Where(i => i.applicationId == item.applicationId).ToList();
            double itemsTotalSum = 0;
            foreach (var oldi in oldItems)
            {
                itemsTotalSum += oldi.Sum - oldi.ClientInvest;
            }

            if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save))
            {                                               
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;


                item.Sum = item.Price * item.Count;

                itemsTotalSum += item.Sum - item.ClientInvest;

                if (itemsTotalSum <= prodLimitMax)
                {
                    if(itemsTotalSum >= prodLimitMin)
                    {                        
                        db.Items.Add(item);
                        db.SaveChanges();

                        return RedirectToAction("ApplicationSummary", "Application", new { ApplicationID = item.applicationId });
                    }
                    else
                        ViewBag.sumErr = "Հայտի ընդհանուր գումարը (" + itemsTotalSum.ToString() + ") փոքր է պրոդուկտի մինիմալ սահմանաչափից (" + prodLimitMin.ToString("N0") + "):";


                    //return RedirectToAction($"Edit/{item.applicationId}", "Application");

                    
                }
                else
                    ViewBag.sumErr = "Հայտի ընդհանուր գումարը (" + itemsTotalSum.ToString() +") գերազանցում է պրոդուկտի մաքսիմալ սահմանաչափը (" + prodLimitMax.ToString("N0") + "):";
            }
            //var app = (from c in db.applications select new { c.applicationId, c.agrNumb });

            

            var supp = db.Suppliers.Select(p => new { p.SupplierId, p.SupplierName }).Distinct().ToList();

            //var clients = db.clients.Distinct().Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList();
            //var pgrs = (from c in db.productGroups select new { c.productGroupId, c.prodGroupName }).Distinct().ToList();

            int prGrId = db.Products.Where(p => p.productId == appPrID).Select(g => g.productGroupId).SingleOrDefault();
            string prGroupName = db.productGroups.Where(p => p.productGroupId == prGrId).Select(g => g.prodGroupName).SingleOrDefault();
            ViewBag.prGroupName = prGroupName;

            ViewBag.prodPr = new SelectList(prodPr, "Id", "ProductPurposeName");
            ViewBag.supp = new SelectList(supp, "SupplierId", "SupplierName");
            //ViewBag.app = new SelectList(app, "applicationId", "agrNumb");


            //ViewBag.pgr = new SelectList(pgrs, "productGroupId", "prodGroupName");
            //ViewBag.cl = new SelectList(clients, "clientId", "clientName");


            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;

            


                var app = db.applications.Find(item.applicationId);
                ViewBag.ApplicationID = item.applicationId;
                @ViewBag.Client = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList().Select(p => p.clientName).FirstOrDefault();
                @ViewBag.AppInfo = app.appDate.ToString();
            

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int id = 0)
        {


            if (id == 0)
                return RedirectToAction("Create");

            Models.Items item = db.Items.Where(p => p.Id == id).FirstOrDefault();

            if (item.Id == 0)
                return RedirectToAction("Create");

            if (item.applicationId < 1)
            {
                return RedirectToAction("Index");
            }


           

            //var app = (from c in db.applications select new { c.applicationId, c.agrNumb });

            var appPrID = db.applications.Where(p => p.applicationId == item.applicationId).FirstOrDefault().productId;

            var prodPr = (
                 from pp in db.ProductPurposes
                 join pr in db.Products on pp.ProductId equals pr.productId
                 join pur in db.Purposes on pp.PurposeId equals pur.Id
                 where pr.productId == appPrID
                 select new ProductPurposeDropBoxViewModel() { Id = pp.Id, ProductPurposeName = pr.productName + " - " + pur.PurposeName }


                ).Distinct().ToList();

            var supp = db.Suppliers.Select(p => new { p.SupplierId, p.SupplierName }).Distinct().ToList();

            //var clients = db.clients.Distinct().Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList();
            var pgrs = (from c in db.productGroups select new { c.productGroupId, c.prodGroupName }).Distinct().ToList();


            ViewBag.prodPr = new SelectList(prodPr, "Id", "ProductPurposeName");
            ViewBag.supp = new SelectList(supp, "SupplierId", "SupplierName");
            //ViewBag.app = new SelectList(app, "applicationId", "agrNumb");


            ViewBag.pgr = new SelectList(pgrs, "productGroupId", "prodGroupName");
            //ViewBag.cl = new SelectList(clients, "clientId", "clientName");



            var app = db.applications.Find(item.applicationId);
            ViewBag.ApplicationID = item.applicationId;
            @ViewBag.Client = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList().Select(p => p.clientName).FirstOrDefault();
            @ViewBag.AppInfo = app.appDate.ToString();

            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Items item)
        {

            if (item.applicationId < 1)
            {
                return RedirectToAction("Index");
            }

            var appPrID = db.applications.Where(p => p.applicationId == item.applicationId).FirstOrDefault().productId;

            double prodLimitMin = db.Products.Where(p => p.productId == appPrID).Select(s => s.minAmount).SingleOrDefault();
            double prodLimitMax = db.Products.Where(p => p.productId == appPrID).Select(s => s.maxAmount).SingleOrDefault();

            var oldItems = db.Items.Where(i => i.applicationId == item.applicationId).ToList();
            double itemsTotalSum = 0;
            foreach (var oldi in oldItems)
            {
                itemsTotalSum += oldi.Sum - oldi.ClientInvest;
            }

            if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save) )
            {
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;

                item.Sum = item.Price * item.Count;

                if (itemsTotalSum <= prodLimitMax)
                {
                    if (itemsTotalSum >= prodLimitMin)
                    {
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction($"Edit/{item.applicationId}", "Application");
                    }
                    else
                        ViewBag.sumErr = "Հայտի ընդհանուր գումարը (" + itemsTotalSum.ToString() + ") փոքր է պրոդուկտի մինիմալ սահմանաչափից (" + prodLimitMin.ToString("N0") + "):";
                }
                else
                    ViewBag.sumErr = "Հայտի ընդհանուր գումարը (" + itemsTotalSum.ToString() + ") գերազանցում է պրոդուկտի մաքսիմալ սահմանաչափը (" + prodLimitMax.ToString("N0") + "):";                
                

            }






            //var app = (from c in db.applications select new { c.applicationId, c.agrNumb });

            

            var prodPr = (
                 from pp in db.ProductPurposes
                 join pr in db.Products on pp.ProductId equals pr.productId
                 join pur in db.Purposes on pp.PurposeId equals pur.Id
                 where pr.productId == appPrID
                 select new ProductPurposeDropBoxViewModel() { Id = pp.Id, ProductPurposeName = pr.productName + " - " + pur.PurposeName }


                ).Distinct().ToList();

            var supp = db.Suppliers.Select(p => new { p.SupplierId, p.SupplierName }).Distinct().ToList();

            var clients = db.clients.Distinct().Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList();
            var pgrs = (from c in db.productGroups select new { c.productGroupId, c.prodGroupName }).Distinct().ToList();


            ViewBag.prodPr = new SelectList(prodPr, "Id", "ProductPurposeName");
            ViewBag.supp = new SelectList(supp, "SupplierId", "SupplierName");
            //ViewBag.app = new SelectList(app, "applicationId", "agrNumb");


            ViewBag.pgr = new SelectList(pgrs, "productGroupId", "prodGroupName");
            ViewBag.cl = new SelectList(clients, "clientId", "clientName");


            var app = db.applications.Find(item.applicationId);
            ViewBag.ApplicationID = item.applicationId;
            @ViewBag.Client = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList().Select(p => p.clientName).FirstOrDefault();
            @ViewBag.AppInfo = app.appDate.ToString();


            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;
            return View(item);
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
