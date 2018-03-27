using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;
using ASFront.ModelsView;
using Microsoft.AspNet.Identity;

namespace ASFront.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserAppTablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserAppTables
        public ActionResult Index(string id)
        {
            var userAppTable = db.UserAppTable.Where(u=>u.userId == id).OrderBy(p=>p.prodLimitId).OrderBy(s=>s.maxLimit).Include(u => u.ApplicationUser).Include(u => u.Branches).Include(t=>t.AppTypes).ToList();

            UserAppTableDetView uAppD;
            List<UserAppTableDetView> uAppDList = new List<Models.UserAppTableDetView>();

            foreach(var uapp in userAppTable)
            {
                uAppD = new UserAppTableDetView();
                uAppD.appTypeId = uapp.AppTypes.appType;
                uAppD.BrancheId = uapp.Branches.Branch;
                uAppD.changeDate = uapp.changeDate;
                uAppD.createDate = uapp.createDate;
                uAppD.isActive = uapp.isActive;
                uAppD.maxLimit = uapp.maxLimit;
                int prodId = db.ProductLimits.Where(p => p.Id == uapp.prodLimitId).Select(s => s.ProductID).SingleOrDefault();
                int currId = db.Products.Where(p => p.productId == prodId).Select(s => s.productCurrency).SingleOrDefault();
                uAppD.prodCurrency = db.CurrencyTypes.Where(c => c.currencyTypesId == currId).Select(s => s.currencyArm).SingleOrDefault();
                uAppD.prodLimitId = uapp.prodLimitId.ToString();
                uAppD.productName = db.Products.Where(p => p.productId == prodId).Select(s => s.productName).SingleOrDefault();
                uAppD.UserAppTableId = uapp.UserAppTableId;
                uAppD.userNameFull = db.UserASProfiles.Where(u => u.UserId == uapp.userId).Select(s => s.FirstName + " " + s.LastName).SingleOrDefault();
                uAppD.userId = uapp.userId;
                uAppDList.Add(uAppD);
            }

            if (userAppTable.Count() < 1)
                return RedirectToAction("Create", new { id = id });            

            return View(uAppDList);
        }

        public ActionResult IndexEx()
        {            
            var branches = db.Branches.OrderBy(bo=>bo.Branch).ToList();
            ViewBag.br = new SelectList(branches, "Id", "Branch");
            ViewBag.brt = branches;

            var products = db.Products.ToList();
            ViewBag.pr = new SelectList(products, "productId", "productName");

            var prlim = (from pl in db.ProductLimits
                         join pr in db.Products on pl.ProductID equals pr.productId into prods
                         from z in prods
                         select new { pl.Id, z.productId, pname = z.productName + " " + pl.AmountLimit + " " + pl.Scoring + " " + pl.App1 + " " + pl.App2 }).ToList();

            ViewBag.prlimitId = new SelectList(prlim, "Id", "pname");

            ViewBag.prlimitIdt = prlim;

            var users = from usr in db.Users
                        join pusr in db.UserASProfiles on usr.Id equals pusr.UserId into fusr
                        from fu in fusr
                        select new { usr.Id, usr.Email, fullName = (fu.FirstName + " " + fu.LastName + " " + usr.Email), fu.asUserName };

            ViewBag.usrs = new SelectList(users, "Id", "fullName");

            ViewBag.usrst = users;
            var appT = db.AppTypes;
            ViewBag.appTt = appT;

            UserAppTableView appTable = new ModelsView.UserAppTableView();

            var appTbl = db.UserAppTable.Take(15);
            foreach(var at in appTbl)
            {
                UserAppTable t = new Models.UserAppTable();
                t.appTypeId = at.appTypeId;                
                t.BrancheId = at.BrancheId;
                t.changeDate = at.changeDate;
                t.createDate = at.createDate;
                t.cUserId = at.cUserId;
                t.isActive = at.isActive;
                t.maxLimit = at.maxLimit;
                t.prodLimitId = at.prodLimitId;
                t.UserAppTableId = at.UserAppTableId;
                t.userId = at.userId;
                appTable.usrAppTbl.Add(t);
            }

            return View(appTable);
        }

        [HttpPost]
        public ActionResult IndexEx(UserAppTableView appTable)
        {

            string sqlQ = "";
            string tmpsqlQ = "";

            var branches = db.Branches.OrderBy(bo => bo.Branch).ToList();
            var users = from usr in db.Users
                        join pusr in db.UserASProfiles on usr.Id equals pusr.UserId into fusr
                        from fu in fusr
                        select new { usr.Id, usr.Email, fullName = (fu.FirstName + " " + fu.LastName + " " + usr.Email), fu.asUserName };

            if (appTable.branchId != null)
            {
                ViewBag.br = new SelectList(branches, "Id", "Branch", appTable.branchId);

                users = from usr in db.Users                        
                        join pusr in db.UserASProfiles on usr.Id equals pusr.UserId into fusr
                        from fu in fusr
                        where fu.BrancheId == appTable.branchId
                        select new { usr.Id, usr.Email, fullName = (fu.FirstName + " " + fu.LastName + " " + usr.Email), fu.asUserName };
                sqlQ += " BrancheId = " + appTable.branchId.ToString();
            }
            else
            {
                ViewBag.br = new SelectList(branches, "Id", "Branch");
            }

            ViewBag.usrs = new SelectList(users, "Id", "fullName");

            var products = db.Products.ToList();
            var prlim = (from pl in db.ProductLimits
                         join pr in db.Products on pl.ProductID equals pr.productId into prods
                         from z in prods
                         select new { pl.Id, z.productId, pname = z.productName + " " + pl.AmountLimit + " " + pl.Scoring + " " + pl.App1 + " " + pl.App2 }).ToList();            

            if (appTable.prodoctId != null)
            {
                ViewBag.pr = new SelectList(products, "productId", "productName", appTable.prodoctId);

                prlim = (from pl in db.ProductLimits
                         where pl.ProductID == appTable.prodoctId
                         join pr in db.Products on pl.ProductID equals pr.productId into prods
                         from z in prods
                         select new { pl.Id, z.productId, pname = z.productName + " " + pl.AmountLimit + " " + pl.Scoring + " " + pl.App1 + " " + pl.App2 }).ToList();
            }
            else
            {
                ViewBag.pr = new SelectList(products, "productId", "productName");
            }

            ViewBag.prlimitId = new SelectList(prlim, "Id", "pname");


            if(appTable.prodoctId != null && appTable.prodLimitId == null)
            {
                
                var plms = db.ProductLimits.Where(pl => pl.ProductID == appTable.prodoctId).Select(sp => sp.Id).ToList();
                foreach(var pt in plms)
                {
                    if (tmpsqlQ.Length > 0)
                        tmpsqlQ += " OR prodLimitId = " + pt.ToString();
                    else
                        tmpsqlQ += " (prodLimitId = " + pt.ToString();
                }
                if (tmpsqlQ.Length > 0)
                    tmpsqlQ += ")";
                else
                    tmpsqlQ += " (prodLimitId = 0) ";
            }//if(appTable.prodoctId != null)


            if (appTable.prodLimitId != null)
            {
                if(sqlQ.Length > 0)
                    sqlQ += " AND prodLimitId = " + appTable.prodLimitId.ToString();
                else
                    sqlQ += " prodLimitId = " + appTable.prodLimitId.ToString();
            }

            if(appTable.userId != null)
            {
                if (sqlQ.Length > 0)
                    sqlQ += " AND userId = '" + appTable.userId + "'";
                else
                    sqlQ += " userId = '" + appTable.userId + "'";
            }

            //--------------------------------------------------------------------------------------------------------------------------------
            ViewBag.brt = branches;

            var prlimt = (from pl in db.ProductLimits
                         join pr in db.Products on pl.ProductID equals pr.productId into prods
                         from z in prods
                         select new { pl.Id, z.productId, pname = z.productName + " " + pl.AmountLimit + " " + pl.Scoring + " " + pl.App1 + " " + pl.App2 }).ToList();

            ViewBag.prlimitIdt = prlimt;

            var userst = from usr in db.Users
                        join pusr in db.UserASProfiles on usr.Id equals pusr.UserId into fusr
                        from fu in fusr
                        select new { usr.Id, usr.Email, fullName = (fu.FirstName + " " + fu.LastName + " " + usr.Email), fu.asUserName };

            ViewBag.usrst = userst;
            var appT = db.AppTypes;
            ViewBag.appTt = appT;

            //--------------------------------------------------------------------------------------------------------------------------------

            if (sqlQ.Length > 0)
            {
                sqlQ = "SELECT * FROM UserAppTables WHERE " + sqlQ;
                if (tmpsqlQ.Length > 0)
                    sqlQ += " AND " + tmpsqlQ;
            }
            else
            {
                sqlQ = "SELECT * FROM UserAppTables";
                if (tmpsqlQ.Length > 0)
                    sqlQ += " WHERE " + tmpsqlQ;
            }
                

            var appTbl = db.UserAppTable.SqlQuery(sqlQ);


            foreach (var at in appTbl)
            {
                UserAppTable t = new Models.UserAppTable();
                t.appTypeId = at.appTypeId;
                t.BrancheId = at.BrancheId;
                t.changeDate = at.changeDate;
                t.createDate = at.createDate;
                t.cUserId = at.cUserId;
                t.isActive = at.isActive;
                t.maxLimit = at.maxLimit;
                t.prodLimitId = at.prodLimitId;
                t.UserAppTableId = at.UserAppTableId;
                t.userId = at.userId;
                appTable.usrAppTbl.Add(t);
            }


            return View(appTable);
        }


        // GET: UserAppTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAppTable userAppTable = db.UserAppTable.Find(id);
            if (userAppTable == null)
            {
                return HttpNotFound();
            }
            return View(userAppTable);
        }

        // GET: UserAppTables/Create
        public ActionResult Create(string id)
        {
            var prlim = (from pl in db.ProductLimits
                         join pr in db.Products on pl.ProductID equals pr.productId into prods
                         from z in prods
                         select new {pl.Id, z.productId, pname =  z.productName + " " + pl.AmountLimit + " " + pl.Scoring + " " + pl.App1 + " " + pl.App2 }).ToList();                        

            ViewBag.prlimitId = new SelectList(prlim, "Id", "pname");
            ViewBag.userId = new SelectList(db.Users.Where(u=>u.Id == id).ToList(), "Id", "UserName");
            ViewBag.BrancheId = new SelectList(db.Branches, "Id", "Branch");
            ViewBag.appTypes = new SelectList(db.AppTypes, "AppTypesId", "appType");
            ViewBag.uId = id;

            var users = from usr in db.Users
                        join pusr in db.UserASProfiles on usr.Id equals pusr.UserId into fusr
                        from fu in fusr
                        select new { Id = usr.Id, fullName = (fu.FirstName + " " + fu.LastName + " " + usr.Email + " " + fu.asUserName) };
            ViewBag.users = new SelectList(users, "Id", "fullName");


            UserAppTable userAppTable = new UserAppTable();
            return View(userAppTable);
        }

        // POST: UserAppTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAppTable userAppTable)
        {
            userAppTable.createDate = DateTime.Now;
            userAppTable.changeDate = DateTime.Now;
            userAppTable.cUserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                userAppTable.isActive = true;
                db.UserAppTable.Add(userAppTable);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = userAppTable.userId });
            }

            var prlim = (from pl in db.ProductLimits
                         join pr in db.Products on pl.ProductID equals pr.productId into prods
                         from z in prods
                         select new { pl.Id, z.productId, pname = z.productName + " " + pl.AmountLimit + " " + pl.Scoring + " " + pl.App1 + " " + pl.App2 }).ToList();

            ViewBag.prlimitId = new SelectList(prlim, "Id", "pname");

            //ViewBag.userId = new SelectList(db.Users, "Id", "UserName", userAppTable.userId);

            var users = from usr in db.Users
                        join pusr in db.UserASProfiles on usr.Id equals pusr.UserId into fusr
                        from fu in fusr
                        select new { Id = usr.Id, fullName = (fu.FirstName + " " + fu.LastName + " " + usr.Email + " " + fu.asUserName) };

            ViewBag.users = new SelectList(users, "Id", "fullName", userAppTable.userId);

            ViewBag.BrancheId = new SelectList(db.Branches, "Id", "Branch", userAppTable.BrancheId);
            ViewBag.appTypes = new SelectList(db.AppTypes, "AppTypesId", "appType");
            return View(userAppTable);
        }

        // GET: UserAppTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAppTable userAppTable = db.UserAppTable.Find(id);
            if (userAppTable == null)
            {
                return HttpNotFound();
            }

            var prlim = (from pl in db.ProductLimits
                         join pr in db.Products on pl.ProductID equals pr.productId into prods
                         from z in prods
                         select new { pl.Id, z.productId, pname = z.productName + " " + pl.AmountLimit + " " + pl.Scoring + " " + pl.App1 + " " + pl.App2 }).ToList();

            ViewBag.prlimitId = new SelectList(prlim, "Id", "pname");
            //ViewBag.userId = new SelectList(db.Users.Where(u => u.Id == userAppTable.userId).ToList(), "Id", "UserName");

            var users = from usr in db.Users
                        join pusr in db.UserASProfiles on usr.Id equals pusr.UserId into fusr
                        from fu in fusr
                        select new { Id=usr.Id,  fullName = (fu.FirstName + " " + fu.LastName + " " + usr.Email + " " + fu.asUserName) };

            ViewBag.users = new SelectList(users, "Id", "fullName", userAppTable.userId);

            ViewBag.BrancheId = new SelectList(db.Branches, "Id", "Branch", userAppTable.BrancheId);
            ViewBag.appTypes = new SelectList(db.AppTypes, "AppTypesId", "appType");
            return View(userAppTable);
        }

        // POST: UserAppTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserAppTable userAppTable)
        {
            userAppTable.changeDate = DateTime.Now;
            userAppTable.cUserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(userAppTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexEx");//, new { id= userAppTable.userId }
            }
            //ViewBag.userId = new SelectList(db.Users, "Id", "UserName", userAppTable.userId);

            var users = from usr in db.Users
                        join pusr in db.UserASProfiles on usr.Id equals pusr.UserId into fusr
                        from fu in fusr
                        select new { Id = usr.Id, fullName = (fu.FirstName + " " + fu.LastName + " " + usr.Email + " " + fu.asUserName) };

            ViewBag.users = new SelectList(users, "Id", "fullName", userAppTable.userId);

            ViewBag.BrancheId = new SelectList(db.Branches, "Id", "Branch", userAppTable.BrancheId);
            ViewBag.appTypes = new SelectList(db.AppTypes, "AppTypesId", "appType");
            return View(userAppTable);
        }

        // GET: UserAppTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAppTable userAppTable = db.UserAppTable.Find(id);
            if (userAppTable == null)
            {
                return HttpNotFound();
            }
            return View(userAppTable);
        }

        // POST: UserAppTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAppTable userAppTable = db.UserAppTable.Find(id);
            db.UserAppTable.Remove(userAppTable);
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
