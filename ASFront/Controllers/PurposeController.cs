using ASFront.Classes;
using ASFront.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASFront.Controllers
{
    [Authorize]
    public class PurposeController : Controller
    {

        ApplicationDbContext db;

        public PurposeController()
        {
            db = new Models.ApplicationDbContext();
        }


        // GET: Purposes
        public ActionResult Index(int page = 1)
        {


            var items = db.Purposes.Distinct().ToList();


            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        //// GET: Purposes/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Purposes/Create
        public ActionResult Create()
        {
            Models.Purposes item = new Purposes();
            item.userId = User.Identity.GetUserId();
            item.LastModifDate = DateTime.Now;

            return View(item);
        }

        // POST: Purposes/Create
        [HttpPost]
        public ActionResult Create(Models.Purposes item)
        {
            if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;

                db.Purposes.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }  

            return View(item);
        }

        // GET: Purposes/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Create");

            Models.Purposes item = db.Purposes.Where(p => p.Id == id).FirstOrDefault();

            if (item.Id == 0)
                return RedirectToAction("Create");     
            

            return View(item);
        }

        // POST: Purposes/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Purposes item)
        {

             if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save) )
            {
                item.userId = User.Identity.GetUserId();
                item.LastModifDate = DateTime.Now;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }         

            return View(item);

        }



    }
}
