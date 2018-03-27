using ASFront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

namespace ASFront.Controllers
{
    public class ListsController : Controller
    {

        private ApplicationDbContext db;


        public ListsController()
        {
            db = new ApplicationDbContext();
        }


        public JsonResult Gender()

        {
            var RolesList = db.clientSexes.Select(p => new { Id = p.clientSexId, Name = p.sex }).Where(p=> p.Id<3).Distinct().ToList();



            Newtonsoft.Json.JsonSerializerSettings set = new Newtonsoft.Json.JsonSerializerSettings();

            return this.Json(RolesList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Region( )

        {
            var RolesList = db.comunities.Select(p => new {Id= p.reg, Name=p.reg }).Distinct().ToList();



            Newtonsoft.Json.JsonSerializerSettings set = new Newtonsoft.Json.JsonSerializerSettings();

            return this.Json(RolesList, JsonRequestBehavior.AllowGet);
        }



        //public JsonResult City()

        //{

        //    var RolesList = db.comunities.Select(p => new { Id = p.cName, Name = p.cName }).Distinct().ToList();

        //    Newtonsoft.Json.JsonSerializerSettings set = new Newtonsoft.Json.JsonSerializerSettings();

        //    return this.Json(RolesList, JsonRequestBehavior.AllowGet);
        //}
        //[Route("ListCity/{regName}", Name = "c_rCity")]
        public JsonResult City(string regName = "")
        {
            //string rn = Request.QueryString["regName"];
            var RolesList = db.comunities.Select(p => new { Id = p.cName, Name = p.cName }).Distinct().ToList(); ;
            if (regName != "")
                RolesList = db.comunities.Where(r=>r.reg == regName).Select(p => new { Id = p.cName, Name = p.cName }).Distinct().ToList();
            

            Newtonsoft.Json.JsonSerializerSettings set = new Newtonsoft.Json.JsonSerializerSettings();

            return this.Json(RolesList, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Street(string cityName = "")

        {
            var RolesList = db.Streets.Select(p => new { Id = p.Street, Name = p.Street }).Distinct().ToList();

            if(cityName != "")
                RolesList = db.Streets.Where(c=>c.cName == cityName).Select(p => new { Id = p.Street, Name = p.Street }).Distinct().ToList();

            Newtonsoft.Json.JsonSerializerSettings set = new Newtonsoft.Json.JsonSerializerSettings();

            return this.Json(RolesList, JsonRequestBehavior.AllowGet);
        }







        public JsonResult Roles()

        {
            var RolesList = db.Roles.Select(p=> new { p.Id, p.Name }).ToList();



            Newtonsoft.Json.JsonSerializerSettings set = new Newtonsoft.Json.JsonSerializerSettings();

            return this.Json(RolesList, JsonRequestBehavior.AllowGet);
        }







        public JsonResult UserBranches()

        {
            var BranchesList = db.Branches.Select(p => new { p.Id, Name=p.Branch }).ToList();
           


            Newtonsoft.Json.JsonSerializerSettings set = new Newtonsoft.Json.JsonSerializerSettings();

            return this.Json(BranchesList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppNotifications()
        {
            var rtVal = 1;

            string userId = User.Identity.GetUserId();
            int appCount = db.ApplicationsForApprove.Where(u => u.appUserId == userId && u.apprStatus == "Ուղարկված").Select(s => s.ApplicationsForApproveId).Count();

            rtVal = appCount;

            return Json(rtVal, JsonRequestBehavior.AllowGet);
        }



        // GET: Lists
        public ActionResult Index()
        {
            return View();
        }





    }
}