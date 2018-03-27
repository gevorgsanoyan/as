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
    public class SupplierBranchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SupplierBranches
     
        public ActionResult Index(int page = 1)
        {

            var items = db.SupplierBranches.Include(s => s.Suppliers).Where(p=>p.Active==true).ToList().OrderBy(p=> p.BrancheName);
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));

        }



  
        // GET: SupplierBranches/Create
        public ActionResult Create()
        {
            SupplierBranches item = new SupplierBranches();
            var Suppliers = db.Suppliers.Where(p => p.Active != false).Select(p => new { ID = p.SupplierId, Name = p.SupplierName }).OrderBy(p => p.Name).Distinct().ToList();


            ViewBag.Suppliers = new SelectList(Suppliers, "ID", "Name");


            return View(item);
        }

        // POST: SupplierBranches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierBranches supplierBranch)
        {
            if (ModelState.IsValid)
            {
                db.SupplierBranches.Add(supplierBranch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var Suppliers = db.Suppliers.Where(p => p.Active != false).Select(p => new { ID = p.SupplierId, Name = p.SupplierName }).OrderBy(p => p.Name).Distinct().ToList();


            ViewBag.Suppliers = new SelectList(Suppliers, "ID", "Name");


            return View(supplierBranch);
        }

        // GET: SupplierBranches/Edit/5


        public ActionResult Edit(int id = 0)
        {

            if (id == 0)
                return RedirectToAction("Create");

            SupplierBranches item = db.SupplierBranches.Find(id);

            if (item == null)
                return RedirectToAction("Create");


            var Suppliers = db.Suppliers.Where(p => p.Active != false).Select(p => new { ID = p.SupplierId, Name = p.SupplierName }).OrderBy(p => p.Name).Distinct().ToList();


            ViewBag.Suppliers = new SelectList(Suppliers, "ID", "Name");


            return View(item);
        }

    

        // POST: SupplierBranches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierBranches supplierBranch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierBranch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var Suppliers = db.Suppliers.Where(p => p.Active != false).Select(p => new { ID = p.SupplierId, Name = p.SupplierName }).OrderBy(p => p.Name).Distinct().ToList();


            ViewBag.Suppliers = new SelectList(Suppliers, "ID", "Name");
            return View(supplierBranch);
        }

        // GET: SupplierBranches/Delete/5
     

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
