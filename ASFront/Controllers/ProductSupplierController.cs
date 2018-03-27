using ASFront.Classes;
using ASFront.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASFront.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductSupplierController : Controller
    {
        private ApplicationDbContext db;


        public ProductSupplierController()
        {
            db = new ApplicationDbContext();
        }
        // GET: ProductSuppliers
        public ActionResult Index(int page = 1)
        {
            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();
            var supp = db.Suppliers.Select(p => new { p.SupplierId, p.SupplierName }).Distinct().ToList();

            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.supp = new SelectList(supp, "SupplierId", "SupplierName");

            List<ProductSupplierViewModel> productSuppliers = (

                 from ps in db.ProductSuppliers
                 join pr in db.Products on ps.ProductId equals pr.productId
                 join sup in db.Suppliers on ps.SupplierId equals sup.SupplierId

                 select new ProductSupplierViewModel() { Id = ps.Id, ProductName = pr.productName, SupplierName = sup.SupplierName }

                 ).ToList();

            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(productSuppliers.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string product, string supplier, int page = 1)
        {
            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();
            var supp = db.Suppliers.Select(p => new { p.SupplierId, p.SupplierName }).Distinct().ToList();

            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.supp = new SelectList(supp, "SupplierId", "SupplierName");

            List<ProductSupplierViewModel> productSuppliers = (

                 from ps in db.ProductSuppliers
                 join pr in db.Products on ps.ProductId equals pr.productId
                 join sup in db.Suppliers on ps.SupplierId equals sup.SupplierId

                 select new ProductSupplierViewModel() { Id = ps.Id, ProductName = pr.productName, SupplierName = sup.SupplierName }

                 ).ToList();

            int prodId = 0;
            int suppId = 0;

            if (product != "")
            {
                prodId = int.Parse(product);

                if (supplier != "")
                {
                    suppId = int.Parse(supplier);
                    productSuppliers = (

                     from ps in db.ProductSuppliers
                     join pr in db.Products on ps.ProductId equals pr.productId
                     join sup in db.Suppliers on ps.SupplierId equals sup.SupplierId
                     where ps.ProductId == prodId && ps.SupplierId == suppId
                     select new ProductSupplierViewModel() { Id = ps.Id, ProductName = pr.productName, SupplierName = sup.SupplierName }

                     ).ToList();
                }
                else
                {
                    productSuppliers = (

                     from ps in db.ProductSuppliers
                     join pr in db.Products on ps.ProductId equals pr.productId
                     join sup in db.Suppliers on ps.SupplierId equals sup.SupplierId
                     where ps.ProductId == prodId
                     select new ProductSupplierViewModel() { Id = ps.Id, ProductName = pr.productName, SupplierName = sup.SupplierName }

                     ).ToList();
                }
            }
            else
            {
                if (supplier != "")
                {
                    suppId = int.Parse(supplier);
                    productSuppliers = (

                     from ps in db.ProductSuppliers
                     join pr in db.Products on ps.ProductId equals pr.productId
                     join sup in db.Suppliers on ps.SupplierId equals sup.SupplierId
                     where ps.ProductId == prodId && ps.SupplierId == suppId
                     select new ProductSupplierViewModel() { Id = ps.Id, ProductName = pr.productName, SupplierName = sup.SupplierName }

                     ).ToList();
                }
            }//if(product != "")

            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(productSuppliers.ToPagedList(pageNumber, pageSize));
        }


        //// GET: ProductSuppliers/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ProductSuppliers/Create
        public ActionResult Create()
        {
            Models.ProductSuppliers item = new ProductSuppliers();

            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();
            var supp = db.Suppliers.Select(p => new { p.SupplierId, p.SupplierName }).Distinct().ToList();

            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.supp = new SelectList(supp, "SupplierId", "SupplierName");

            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;

            return View(item);
        }

        // POST: ProductSuppliers/Create
        [HttpPost]
        public ActionResult Create(Models.ProductSuppliers item)
        {


            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                if (!(db.ProductSuppliers.Any(p => p.ProductId == item.ProductId && p.SupplierId == item.SupplierId)))
                {

                    item.userId = User.Identity.GetUserId();
                    item.LastModifDate = DateTime.Now;

                    db.ProductSuppliers.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", Messages.ProductSupplierExist);
                }

            }

            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();
            var supp = db.Suppliers.Select(p => new { p.SupplierId, p.SupplierName }).Distinct().ToList();

            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.supp = new SelectList(supp, "SupplierId", "SupplierName");

            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;

            return View(item);
        }

        // GET: ProductSuppliers/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Create");

            Models.ProductSuppliers item = db.ProductSuppliers.Where(p => p.Id == id).FirstOrDefault();

            if (item.Id == 0)
                return RedirectToAction("Create");




            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();
            var supp = db.Suppliers.Select(p => new { p.SupplierId, p.SupplierName }).Distinct().ToList();

            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.supp = new SelectList(supp, "SupplierId", "SupplierName");



            return View(item);
        }

        // POST: ProductSuppliers/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.ProductSuppliers item)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();
            var supp = db.Suppliers.Select(p => new { p.SupplierId, p.SupplierName }).Distinct().ToList();

            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.supp = new SelectList(supp, "SupplierId", "SupplierName");



            return View(item);
        }


    }
}
