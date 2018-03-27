using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using ASFront.Models;
using PagedList;
using ASFront.Classes;

namespace ASFront.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        ApplicationDbContext context;   
        public RolesController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Roles
        [Authorize(Roles ="Admin")]
        public ActionResult Index(int page = 1)
        {
              var Roles = context.Roles.ToList();
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(Roles.ToPagedList(pageNumber, pageSize));   
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ApplicationEditRoles(int page = 1)
        {
            var appEditRoles = context.applicationEditRoles.ToList();
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(appEditRoles.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CreateRoleEdit()
        {
            applicationEditRoles appEditRole = new Models.applicationEditRoles();

            var Roles = context.Roles.ToList();
            ViewBag.lRoles = new SelectList(Roles, "Name", "Name");

            return View(appEditRole);
        }

        [HttpPost]
        public ActionResult CreateRoleEdit(applicationEditRoles appEditRole)
        {
            ViewBag.errMsg = "";
            if (ModelState.IsValid)
            {
                context.applicationEditRoles.Add(appEditRole);
                context.SaveChanges();
                ViewBag.errMsg = "Պահպանված է";
            }

            var Roles = context.Roles.ToList();
            ViewBag.lRoles = new SelectList(Roles, "Name", "Name");

            return View(appEditRole);
        }

        public ActionResult EditRoleEdit(int appEditRoleId)
        {
            applicationEditRoles appRoleEdit = context.applicationEditRoles.Where(e => e.applicationEditRolesId == appEditRoleId).SingleOrDefault();

            return View(appRoleEdit);
        }

        [HttpPost]
        public ActionResult EditRoleEdit(applicationEditRoles appEditRole)
        {
            var appRoleEdit = context.applicationEditRoles.Where(e => e.applicationEditRolesId == appEditRole.applicationEditRolesId).SingleOrDefault();
            ViewBag.errMsg = "";
            if (ModelState.IsValid)
            {
                appRoleEdit.canEdit = appEditRole.canEdit;
                appRoleEdit.RoleName = appRoleEdit.RoleName;
                context.SaveChanges();
                ViewBag.errMsg = "Պահպանված է";
            }
                
            return View(appRoleEdit);
        }

    }
}