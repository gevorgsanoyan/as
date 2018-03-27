using ASFront.Classes;
using ASFront.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASFront.Controllers
{
    [Authorize(Roles ="Admin")]
    public class productGroupController : Controller
    {

        private ApplicationDbContext db;


        public productGroupController()
        {
            db = new ApplicationDbContext();
        }

        // GET: productGroups
        public ActionResult Index(int page = 1)
        {
            var items = db.productGroups.Distinct().ToList();


            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: productGroups/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: productGroups/Create
        public ActionResult Create()
        {
            productGroups pg = new productGroups();
           
            return View(pg);
        }

        // POST: productGroups/Create
        [HttpPost]
        public ActionResult Create(Models.productGroups pg)
        {
            if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save))
            {

                db.productGroups.Add(pg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pg);
           
        }

        // GET: productGroups/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
                return RedirectToAction("Create");


            Models.productGroups pg = db.productGroups.Where(p => p.productGroupId == id).FirstOrDefault();

            if (pg.productGroupId == 0)
                return RedirectToAction("Create");


            return View(pg);
        }

        // POST: productGroups/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.productGroups pg)
        {
             if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save) )
            {

                db.Entry(pg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pg);
        }

        // GET: productGroups/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: productGroups/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
