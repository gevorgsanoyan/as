using ASFront.Classes;
using ASFront.Models;
using ASFront.ModelsView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace ASFront.Controllers
{
    public class SettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Settings
        public ActionResult Index()
        {
            Settings settings = new Settings();

            settings.PageSize = ApplicationSettings.PageSize;
            

            return View(settings);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Settings settings)
        {

            if (ModelState.IsValid)
            {
                ApplicationSettings.PageSize = settings.PageSize;
               

                ViewBag.Info = Resources.Messages.Saved;
                ViewBag.InfoStatus = "ValidMessage";
            }
            else
            {
              ViewBag.Info = Resources.Messages.ErrorData;
                ViewBag.InfoStatus = "ErrorMessage";
            }   
            
            return View(settings);
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
