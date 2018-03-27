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
    [Authorize(Roles ="Admin")]
    public class ProductLimitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductLimits
        public ActionResult Index(int page = 1)
        {
            ViewBag.pr = db.Products;
            ViewBag.prGroup = new SelectList(db.productGroups, "productGroupId", "prodGroupName");
            ViewBag.prName = new SelectList(db.Products, "productId", "productName");

            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(db.ProductLimits.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string prGroupName, string prName, int page = 1)
        {
            List<ASFront.Models.ProductLimits> plist = new List<ProductLimits>();
            int prGr = 0;
            var prLimits = (from pl in db.ProductLimits
                            join pr in db.Products on pl.ProductID equals pr.productId
                            select new
                            {
                                Id = pl.Id,
                                AmountLimit = pl.AmountLimit,
                                App1 = pl.App1,
                                App2 = pl.App2,
                                ProductID = pl.ProductID,
                                Products = pl.Products,
                                Scoring = pl.Scoring
                            }).ToList();

            if (prGroupName != "")
            {
                prGr = int.Parse(prGroupName);
                if (prName != "")
                {
                    int prId = int.Parse(prName);
                    prLimits = (from pl in db.ProductLimits
                                join pr in db.Products on pl.ProductID equals pr.productId
                                where pl.ProductID == prId && pr.productGroupId == prGr
                                select new
                                {
                                    Id = pl.Id,
                                    AmountLimit = pl.AmountLimit,
                                    App1 = pl.App1,
                                    App2 = pl.App2,
                                    ProductID = pl.ProductID,
                                    Products = pl.Products,
                                    Scoring = pl.Scoring
                                }).ToList();

                }
                else
                {
                    prLimits = (from pl in db.ProductLimits
                                join pr in db.Products on pl.ProductID equals pr.productId
                                where pr.productGroupId == prGr
                                select new
                                {
                                    Id = pl.Id,
                                    AmountLimit = pl.AmountLimit,
                                    App1 = pl.App1,
                                    App2 = pl.App2,
                                    ProductID = pl.ProductID,
                                    Products = pl.Products,
                                    Scoring = pl.Scoring
                                }).ToList();
                } //if(prName != "")
            }
            else
            {
                if (prName != "")
                {
                    int prId = int.Parse(prName);
                    prLimits = (from pl in db.ProductLimits
                                join pr in db.Products on pl.ProductID equals pr.productId
                                where pl.ProductID == prId
                                select new
                                {
                                    Id = pl.Id,
                                    AmountLimit = pl.AmountLimit,
                                    App1 = pl.App1,
                                    App2 = pl.App2,
                                    ProductID = pl.ProductID,
                                    Products = pl.Products,
                                    Scoring = pl.Scoring
                                }).ToList();
                                
                } //if(prName != "")
            }//if(prGroupName != "")


            foreach(var p in prLimits)
            {
                Models.ProductLimits pl = new ProductLimits();
                pl.AmountLimit = p.AmountLimit;
                pl.App1 = p.App1;
                pl.App2 = p.App2;
                pl.Id = p.Id;
                pl.ProductID = pl.ProductID;
                pl.Products = p.Products;
                pl.Scoring = p.Scoring;
                plist.Add(pl);
            }


            //ViewBag.pr = db.Products;
            ViewBag.prGroup = new SelectList(db.productGroups, "productGroupId", "prodGroupName");
            if (prGroupName != "")
                ViewBag.prName = new SelectList(db.Products.Where(p=>p.productGroupId == prGr).ToList(), "productId", "productName");
            else
                ViewBag.prName = new SelectList(db.Products, "productId", "productName");

            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(plist.ToPagedList(pageNumber, pageSize));
        }


        // GET: ProductLimits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLimits productLimits = db.ProductLimits.Find(id);
            if (productLimits == null)
            {
                return HttpNotFound();
            }
            return View(productLimits);
        }

        public long prLimitCheck(ProductLimits plim)
        {
            long prlimId = 0;

            prlimId = db.ProductLimits.Where(  p =>  p.Id!=plim.Id &&   p.AmountLimit == plim.AmountLimit && p.App1 == plim.App1 && p.App2 == plim.App2 && p.ProductID == plim.ProductID && p.Scoring == plim.Scoring).Select(pid => pid.Id).SingleOrDefault();

            return prlimId;
        }

        // GET: ProductLimits/Create
        public ActionResult Create()
        {
            ViewBag.pr = new SelectList(db.Products, "productId", "productName");
            ProductLimits p = new ProductLimits();
            p.Scoring = true;
            p.App1 = true;
            p.App2 = true;
            return View(p);
        }

        // POST: ProductLimits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductID,AmountLimit,Scoring,App1,App2")] ProductLimits productLimits)
        {
            ViewBag.pr = new SelectList(db.Products, "productId", "productName");
            if (ModelState.IsValid)
            {
                long prlimId = prLimitCheck(productLimits);
                if (prlimId == 0)
                {
                    db.ProductLimits.Add(productLimits);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.err = "Նման սահմանափակում գոյություն ունի:";                
            }

            return View(productLimits);
        }

        // GET: ProductLimits/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.pr = new SelectList(db.Products, "productId", "productName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLimits productLimits = db.ProductLimits.Find(id);
            if (productLimits == null)
            {
                return HttpNotFound();
            }
            return View(productLimits);
        }

        // POST: ProductLimits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ProductLimits productLimits)
        {
            ViewBag.pr = new SelectList(db.Products, "productId", "productName");
            if (ModelState.IsValid)
            {
                long prlimId = prLimitCheck(productLimits);
                if (prlimId == 0)
                {
                    db.Entry(productLimits).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.err = "Նման սահմանափակում գոյություն ունի:";
            }
            return View(productLimits);
        }

        // GET: ProductLimits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductLimits productLimits = db.ProductLimits.Find(id);
            if (productLimits == null)
            {
                return HttpNotFound();
            }
            return View(productLimits);
        }

        // POST: ProductLimits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductLimits productLimits = db.ProductLimits.Find(id);
            db.ProductLimits.Remove(productLimits);
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
