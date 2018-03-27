using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using ASFront.Classes;

namespace ASFront.Controllers
{
    [Authorize]
    public class productsController : Controller
    {
        private ApplicationDbContext db;


        public productsController()
        {
            db = new ApplicationDbContext();
        }
        // GET: products
        public ActionResult Index(int page = 1)
        {

            ViewBag.prGroup = new SelectList(db.productGroups, "productGroupId", "prodGroupName");
            SelectListItem itm = new SelectListItem();
            List<SelectListItem> sList = new List<SelectListItem>();
            itm.Value = "1";
            itm.Text = "Գործող";
            sList.Add(itm);
            itm = new SelectListItem();
            itm.Value = "2";
            itm.Text = "Չգործող";
            sList.Add(itm);
            //itm = new SelectListItem();
            //itm.Value = "3";
            //itm.Text = "Բոլորը";
            //sList.Add(itm);

            ViewBag.grStatus = new SelectList(sList, "Value", "Text");


            var products = db.Products.Include(p=>p.productGroups).Distinct().ToList();


            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string prGroupName, string prStatus, int page = 1)
        {

            bool stVal = true;
            int prGrdVal = 0;
            var products = db.Products.Include(p => p.productGroups).Distinct().ToList();

            if (prStatus != "")
            {
                switch(prStatus)
                {
                    case "1":
                        stVal = true;
                        break;
                    case "2":
                        stVal = false;
                        break;
                }

                if(prGroupName != "")
                {
                    prGrdVal = int.Parse(prGroupName);
                    products = db.Products.Where(f => f.productGroupId == prGrdVal && f.prodStatus == stVal).Include(p => p.productGroups).Distinct().ToList();
                }                    
                else
                    products = db.Products.Where(f => f.prodStatus == stVal).Include(p => p.productGroups).Distinct().ToList();
            }
            else
            {
                if (prGroupName != "")
                {
                    prGrdVal = int.Parse(prGroupName);
                    products = db.Products.Where(f => f.productGroupId == prGrdVal).Include(p => p.productGroups).Distinct().ToList();
                }                    
            }

            ViewBag.prGroup = new SelectList(db.productGroups, "productGroupId", "prodGroupName");
            SelectListItem itm = new SelectListItem();
            List<SelectListItem> sList = new List<SelectListItem>();
            itm.Value = "1";
            itm.Text = "Գործող";
            sList.Add(itm);
            itm = new SelectListItem();
            itm.Value = "2";
            itm.Text = "Չգործող";
            sList.Add(itm);
            itm = new SelectListItem();
            itm.Value = "3";
            itm.Text = "Բոլորը";
            sList.Add(itm);

            ViewBag.grStatus = new SelectList(sList, "Value", "Text");



            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: products/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products pr = db.Products.Find(id);
            if (pr.productId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usr = (from u in db.Users
                       where u.Id == pr.userId
                       select new { u.UserName });
            foreach (var u in usr)
            {
                pr.userId = u.UserName;
            }
            if (pr == null)
            {
                return HttpNotFound();
            }
            return View(pr);
        }

        [Authorize]
        // GET: products/Create
        public ActionResult Create()
        {
            Products pr = new Products();
            var curr = (from c in db.CurrencyTypes select new { c.currencyTypesId, c.exchCode, c.currencyArm, c.currencyEng });
            ViewBag.cr = new SelectList(curr, "currencyTypesId", "exchCode");



            var pgrs = (from c in db.productGroups select new { c.productGroupId, c.prodGroupName });
            ViewBag.pgr = new SelectList(pgrs, "productGroupId", "prodGroupName");

            pr.userId = User.Identity.GetUserId();
            pr.LastModifDate = DateTime.Now;
            return View(pr);
        }

        // POST: products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products pr)
        {
            if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                pr.userId = User.Identity.GetUserId();
                pr.LastModifDate = DateTime.Now;
                db.Products.Add(pr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var curr = (from c in db.CurrencyTypes select new { c.currencyTypesId, c.exchCode, c.currencyArm, c.currencyEng });
            ViewBag.cr = new SelectList(curr, "currencyTypesId", "exchCode");


            var pgrs = (from c in db.productGroups select new { c.productGroupId, c.prodGroupName });
            ViewBag.pgr = new SelectList(pgrs, "productGroupId", "prodGroupName");

            return View(pr);
        }

        // GET: products/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products pr = db.Products.Find(id);
            var curr = (from c in db.CurrencyTypes select new { c.currencyTypesId, c.exchCode, c.currencyArm, c.currencyEng });
            ViewBag.cr = new SelectList(curr, "currencyTypesId", "exchCode");

          


            var pgrs = (from c in db.productGroups select new { c.productGroupId, c.prodGroupName });
            ViewBag.pgr = new SelectList(pgrs, "productGroupId", "prodGroupName");

            if (pr == null)
            {
                return HttpNotFound();
            }
            return View(pr);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products product)
        {
             if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save) )
            {
                product.userId = User.Identity.GetUserId();
                product.LastModifDate = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var curr = (from c in db.CurrencyTypes select new { c.currencyTypesId, c.exchCode, c.currencyArm, c.currencyEng });
            ViewBag.cr = new SelectList(curr, "currencyTypesId", "exchCode");


            var pgrs = (from c in db.productGroups select new { c.productGroupId, c.prodGroupName });
            ViewBag.pgr = new SelectList(pgrs, "productGroupId", "prodGroupName");

            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products product = db.Products.Find(id);
            db.Products.Remove(product);
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
