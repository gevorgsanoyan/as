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
using PagedList;

namespace ASFront.Controllers
{
    public class SellersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sellers



        public ActionResult Index(int page = 1)
        {
             List<Sellers> items = new List<Sellers>();


            long SupplierId = 0;
            long BrancheId = 0; 

            string SupplierIdStr = Request.QueryString["SupplierId"];
            string BrancheIdStr = Request.QueryString["BrancheId"];
          

            if (!string.IsNullOrWhiteSpace(SupplierIdStr))
                Int64.TryParse(SupplierIdStr, out SupplierId);   

            if (!string.IsNullOrWhiteSpace(BrancheIdStr))
                Int64.TryParse(BrancheIdStr, out BrancheId);



            if (SupplierId > 0 )
            {
                if (SupplierId > 0 && BrancheId > 0)
                {
                    items = db.Sellers.Include(s => s.Suppliers).Include(b=> b.SupplierBranchs).Where(p => p.BrancheId == BrancheId && p.SupplierId == SupplierId).Distinct().ToList();

                }
                else
                    items = db.Sellers.Include(s => s.Suppliers).Include(b => b.SupplierBranchs).Where(p => p.SupplierId == SupplierId).Distinct().ToList();

            }
            else
                items = db.Sellers.Include(s => s.Suppliers).Include(b => b.SupplierBranchs).Distinct().ToList();







            


            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }





        // GET: Sellers/Create
        public ActionResult Create()
        {
            Sellers sellers = new Sellers();
            long SupplierId = 0;
            long BrancheId = 0;

            string SupplierIdStr = Request.QueryString["SupplierId"];
            string BrancheIdStr = Request.QueryString["BrancheId"];


            if (!string.IsNullOrWhiteSpace(SupplierIdStr))
                Int64.TryParse(SupplierIdStr, out SupplierId);

            if (!string.IsNullOrWhiteSpace(BrancheIdStr))
                Int64.TryParse(BrancheIdStr, out BrancheId);

            List<Suppliers> Supp = db.Suppliers.Where(p => p.Active != false).OrderBy(p => p.SupplierName).ToList();



            List<SupplierBranches> SuppBr = new List<SupplierBranches>();

           

            if (SupplierId > 0 )
            {
                SuppBr = db.SupplierBranches.Where(p => p.Active != false && p.SupplierId== SupplierId).OrderBy(p => p.BrancheName).ToList();
                sellers.SupplierId = SupplierId;
            }
           

            if ( BrancheId > 0)
            {
               sellers.BrancheId = BrancheId;
            }
           



           
            ViewBag.Suppl = new SelectList(Supp, "SupplierId", "SupplierName");
            ViewBag.br = new SelectList(SuppBr, "BrancheId", "BrancheName");
            return View(sellers);
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sellers sellers)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                db.Sellers.Add(sellers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            List<Suppliers> Supp = db.Suppliers.Where(p => p.Active != false).OrderBy(p => p.SupplierName).ToList();



            List<SupplierBranches> SuppBr = new List<SupplierBranches>();



            if (sellers.SupplierId > 0)
            {
                SuppBr = db.SupplierBranches.Where(p => p.Active != false && p.SupplierId == sellers.SupplierId).OrderBy(p => p.BrancheName).ToList();
              
            }





            ViewBag.Suppl = new SelectList(Supp, "SupplierId", "SupplierName", sellers.SupplierId);
            ViewBag.br = new SelectList(SuppBr, "BrancheId", "BrancheName");
            return View(sellers);
        }

        // GET: Sellers/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sellers sellers = db.Sellers.Find(id);
            if (sellers == null)
            {
                return HttpNotFound();
            }


            List<Suppliers> Supp = db.Suppliers.Where(p => p.Active != false).OrderBy(p => p.SupplierName).ToList();



            List<SupplierBranches> SuppBr = new List<SupplierBranches>();



            if (sellers.SupplierId > 0)
            {
                SuppBr = db.SupplierBranches.Where(p => p.Active != false && p.SupplierId == sellers.SupplierId).OrderBy(p => p.BrancheName).ToList();

            }





            ViewBag.Suppl = new SelectList(Supp, "SupplierId", "SupplierName", sellers.SupplierId);
            ViewBag.br = new SelectList(SuppBr, "BrancheId", "BrancheName");

            return View(sellers);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sellers sellers)
        {

            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                db.Entry(sellers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            List<Suppliers> Supp = db.Suppliers.Where(p => p.Active != false).OrderBy(p => p.SupplierName).ToList();



            List<SupplierBranches> SuppBr = new List<SupplierBranches>();



            if (sellers.SupplierId > 0)
            {
                SuppBr = db.SupplierBranches.Where(p => p.Active != false && p.SupplierId == sellers.SupplierId).OrderBy(p => p.BrancheName).ToList();

            }





            ViewBag.Suppl = new SelectList(Supp, "SupplierId", "SupplierName", sellers.SupplierId);
            ViewBag.br = new SelectList(SuppBr, "BrancheId", "BrancheName");
            return View(sellers);
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
