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
    public class ProductPurposeController : Controller
    {

        private ApplicationDbContext db;


        public ProductPurposeController()
        {
            db = new ApplicationDbContext();
        }

        // GET: ProductPurposes
        public ActionResult Index(int page = 1)
        {
            //ProductPurposeViewModel productPurposes = 
            //db.ProductPurposes.Distinct().ToList();
            //var pd = db.Products.Distinct().ToList();
            //var pur= db.Purposes.Distinct().ToList();

            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();

            var purp = db.Purposes.Select(p => new { Id = p.Id, p.PurposeName }).Distinct().ToList();

            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.purp = new SelectList(purp, "Id", "PurposeName");


            List<ProductPurposeViewModel> productPurposes = (

                 from pp in db.ProductPurposes
                 join pr in db.Products on pp.ProductId equals pr.productId
                 join pur in db.Purposes on pp.PurposeId equals pur.Id

                 select new ProductPurposeViewModel() { Id = pp.Id, ProductName = pr.productName, PurposeName = pur.PurposeName }

                 ).Distinct().ToList();

            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(productPurposes.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult Index(string product, string purpD, int page = 1)
        {
            //ProductPurposeViewModel productPurposes = 
            //db.ProductPurposes.Distinct().ToList();
            //var pd = db.Products.Distinct().ToList();
            //var pur= db.Purposes.Distinct().ToList();

            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();
            var purp = (from pp in db.ProductPurposes
                       join pr in db.Products on pp.ProductId equals pr.productId
                       join pur in db.Purposes on pp.PurposeId equals pur.Id
                       select new ProductPurposeViewModel() { Id = pp.Id, PurposeName = pur.PurposeName }

                     ).ToList();//db.Purposes.Select(p => new { PurposeId = p.Id, p.PurposeName }).Distinct().ToList();
            List<ProductPurposeViewModel> productPurposes = (

                 from pp in db.ProductPurposes
                 join pr in db.Products on pp.ProductId equals pr.productId
                 join pur in db.Purposes on pp.PurposeId equals pur.Id

                 select new ProductPurposeViewModel() { Id = pp.Id, ProductName = pr.productName, PurposeName = pur.PurposeName }

                 ).Distinct().ToList();

            if (product != "")
            {                
                int prId = int.Parse(product);
                prod = db.Products.Where(r=>r.productId == prId).Select(p => new { p.productId, p.productName }).Distinct().ToList();
                purp = (from pp in db.ProductPurposes
                       join pr in db.Products on pp.ProductId equals pr.productId
                       join pur in db.Purposes on pp.PurposeId equals pur.Id
                       where pp.ProductId == prId 
                       select new ProductPurposeViewModel() { Id = pp.Id, PurposeName = pur.PurposeName }

                     ).ToList();//db.Purposes.Where(r=>r.P).Select(p => new { PurposeId = p.Id, p.PurposeName }).Distinct().ToList();
                if (purpD == "")
                {
                    productPurposes = (

                     from pp in db.ProductPurposes
                     join pr in db.Products on pp.ProductId equals pr.productId
                     join pur in db.Purposes on pp.PurposeId equals pur.Id
                     where pp.ProductId == prId
                     select new ProductPurposeViewModel() { Id = pp.Id, ProductName = pr.productName, PurposeName = pur.PurposeName }

                     ).Distinct().ToList();
                }
                else
                {
                    int purpId = int.Parse(purpD);
                    productPurposes = (

                     from pp in db.ProductPurposes
                     join pr in db.Products on pp.ProductId equals pr.productId
                     join pur in db.Purposes on pp.PurposeId equals pur.Id
                     where pp.ProductId == prId && pp.PurposeId == purpId
                     select new ProductPurposeViewModel() { Id = pp.Id, ProductName = pr.productName, PurposeName = pur.PurposeName }

                     ).Distinct().ToList();
                }
            }//if (product != "")





            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.purp = new SelectList(purp, "Id", "PurposeName");


            

            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(productPurposes.ToPagedList(pageNumber, pageSize));
        }


        // GET: ProductPurposes/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ProductPurposes/Create
        public ActionResult Create()
        {
            Models.ProductPurposes item = new ProductPurposes();

            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();

            var purp = db.Purposes.Select(p => new { PurposeId = p.Id, p.PurposeName }).Distinct().ToList();

            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.purp = new SelectList(purp, "PurposeId", "PurposeName");

            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;

            return View(item);
        }

        // POST: ProductPurposes/Create
        [HttpPost]
        public ActionResult Create(Models.ProductPurposes item)
        {
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                if (!(db.ProductPurposes.Any(p => p.ProductId == item.ProductId && p.PurposeId == item.PurposeId)))
                {
                    item.userId = User.Identity.GetUserId();
                    item.LastModifDate = DateTime.Now;

                    db.ProductPurposes.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError("", Messages.ProductPurposesExist);
                }
            }

            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();

            var purp = db.Purposes.Select(p => new { PurposeId = p.Id, p.PurposeName }).Distinct().ToList();

            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.purp = new SelectList(purp, "PurposeId", "PurposeName");


            return View(item);
        }

        // GET: ProductPurposes/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Create");

            Models.ProductPurposes item = db.ProductPurposes.Where(p => p.Id == id).FirstOrDefault();

            if (item.Id == 0)
                return RedirectToAction("Create");




            var prod = db.Products.Select(p => new { p.productId, p.productName }).Distinct().ToList();

            var purp = db.Purposes.Select(p => new { PurposeId = p.Id, p.PurposeName }).Distinct().ToList();

            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.purp = new SelectList(purp, "PurposeId", "PurposeName");




            return View(item);
        }

        // POST: ProductPurposes/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.ProductPurposes item)
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

            var purp = db.Purposes.Select(p => new { PurposeId = p.Id, p.PurposeName }).Distinct().ToList();

            ViewBag.prod = new SelectList(prod, "productId", "productName");
            ViewBag.purp = new SelectList(purp, "PurposeId", "PurposeName");




            return View(item);

        }


    }
}
