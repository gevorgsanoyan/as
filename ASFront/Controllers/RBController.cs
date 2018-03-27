using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using PagedList;
using ASFront.Classes;
using System.Data.Entity;

namespace ASFront.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RBController : Controller
    {
        ApplicationDbContext db;
        
        public RBController()
        {
            db = new Models.ApplicationDbContext();
        }

        [Authorize(Roles ="Admin")]        
        public ActionResult Index(int page = 1)
        {
             var regs = db.Regions.ToList();
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(regs.ToPagedList(pageNumber, pageSize));

           
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Branches()
        {
            var brs = db.Branches.ToList();
            return View(brs);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult CreateRegion()
        {
            var reg = new Regions();
            return View(reg);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Create");

            Branches item = db.Branches.Where(p => p.Id == id).FirstOrDefault();

            if (item.Id == 0)
                return RedirectToAction("Create");

             

             
            return View(item);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Branches item)
        {

            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
              

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Branches");
            }
            return View(item);

        }


        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Create");

            Models.Branches item = db.Branches.Where(p => p.Id == id).FirstOrDefault();

            if (item.Id == 0)
                return RedirectToAction("Create");




            return View(item);
        }

        [HttpPost]
        [HandleError()]
        public ActionResult CreateRegion(Regions reg)
        {
            db.Regions.Add(reg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateBranch()
        {
            var br = new Branches();
            return View(br);
        }

        [HttpPost]
        [HandleError()]
        public ActionResult CreateBranch(Branches br)
        {
            db.Branches.Add(br);
            db.SaveChanges();
            return RedirectToAction("Branches");
        }
        
        public ActionResult BrByRegsList()
        {
            var rbs = db.BrbyRegs.Join(db.Branches,
                r => r.BranchesId,
                b => b.Id,
                (r, b) => new
                {
                    brId = r.BranchesId,
                    rgID = r.RegionsId,
                    BranchName = b.Branch,
                    lastupdate = r.rDate,
                    currStat = r.current
                }).Join(db.Regions,
                n => n.rgID,
                g => g.RegionsId,
                (n, g) => new
                {
                    bID = n.brId,
                    BrName = n.BranchName,
                    lupdate = n.lastupdate,
                    cStatus = n.currStat,
                    regID = g.RegionsId,
                    RgName = g.Region
                });



            BranchRegionsView brv;
            List<BranchRegionsView> brList = new List<BranchRegionsView>();

            foreach (var b in rbs)
            {
                if (b.cStatus == 1)
                {
                    brv = new Models.BranchRegionsView();
                    brv.BranchId = b.bID;
                    brv.Branch = b.BrName;
                    brv.RegionId = b.regID;
                    brv.Region = b.RgName;
                    brv.rDate = b.lupdate;
                    brList.Add(brv);
                }//if (b.cStatus == 1)
            }//foreach(var b in rbs)

            return View(brList);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult BrByRegsCreate()
        {
            BrbyRegs item = new BrbyRegs();
            ViewBag.Br = new SelectList(db.Branches.ToList(), "Id", "Branch");
            ViewBag.Rg = new SelectList(db.Regions.ToList(), "RegionsId", "Region");
            return View(item);
        }

        [HttpPost]
        public ActionResult BrByRegsCreate(BrbyRegs item)
        {

            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {

                //var user = UserManager.FindById(User.Identity.GetUserId());
                string currentUserId = User.Identity.GetUserId();

                BrbyRegs br = new Models.BrbyRegs();
                br.BranchesId = item.BranchesId;
                br.RegionsId = item.RegionsId;
                br.current = 1;
                br.userId = currentUserId;
                br.rDate = DateTime.Now;

                var r = db.BrbyRegs.Where(bbr => bbr.BranchesId == item.BranchesId).ToList();  //from bbr in db.BrbyRegs
                                                                                            //where (bbr.BranchesId == m.BranchesId)
                                                                                            //select bbr.BrbyRegsId;

                if (r.Count() > 0)
                {
                    foreach (var it in r)
                    {
                        BrbyRegs brbrg = db.BrbyRegs.SingleOrDefault(id => id.BrbyRegsId == it.BrbyRegsId);
                        brbrg.current = 0;
                        db.SaveChanges();
                    }
                }

                db.BrbyRegs.Add(br);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Br = new SelectList(db.Branches.ToList(), "Id", "Branch");
            ViewBag.Rg = new SelectList(db.Regions.ToList(), "RegionsId", "Region");
            return View(item);
        }

    }//public class RBController : Controller
}