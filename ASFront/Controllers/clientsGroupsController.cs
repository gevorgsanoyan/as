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
using System.Threading.Tasks;

namespace ASFront.Controllers
{
    public class clientsGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: clientsGroups

  

        public ActionResult ClientsGroupByEditClientId(long EditClientId = 0)
        {

           


            List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();

            long groupId = 0;
           
            if (EditClientId > 0)
            {
                 
                groupId = db.clientsGroup.Where(c => c.clientId == EditClientId).Select(g => g.groupId).SingleOrDefault();
                cgList = clientsGroupDetView.GetClientsGroupDetViewList(groupId);
            }

            return View(cgList);
        }


        public ActionResult ClientsGroup(long groupId=0)
        {            

            //var clientsGroup = (from cg in db.clientsGroup
            //                       join r in db.releationType on cg.relType equals r.releationTypeId
            //                       join c in db.clients on cg.clientId equals c.clientId
            //                       join g in db.@group on cg.groupId equals g.groupId
            //                       select new {
            //                           cg.clientsGroupId, cg.groupId, cg.clientId, cg.note1, cg.note2, cg.note3,
            //                           cg.relType, rt = r.relType, c.clientName , c.clientLastName,
            //                           g.gruopFullName 
            //                   }).ToList();
                
                
                //db.clientsGroup.Include(c => c.releationType);

            //if (groupId > 0)
            //    clientsGroup =(from cg in db.clientsGroup
            //     where cg.groupId == groupId
            //     join r in db.releationType on cg.relType equals r.releationTypeId
            //     join c in db.clients on cg.clientId equals c.clientId
            //     join g in db.@group on cg.groupId equals g.groupId
            //     select new
            //     {
            //         cg.clientsGroupId,
            //         cg.groupId,
            //         cg.clientId,
            //         cg.note1,
            //         cg.note2,
            //         cg.note3,
            //         cg.relType,
            //         rt = r.relType,
            //         c.clientName, c.clientLastName,
            //         g.gruopFullName
            //     }).ToList();


            //clientsGroupDetView cgView;
            //List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();

            //foreach(var cg in clientsGroup )
            //{
            //    cgView = new ModelsView.clientsGroupDetView();
            //    cgView.clientsGroupId = cg.clientsGroupId;
            //    cgView.groupId = cg.groupId;
            //    cgView.groupName = cg.gruopFullName;
            //    cgView.relType = cg.relType;
            //    cgView.rpFirstName = cg.clientName;
            //    cgView.rpLastName = cg.clientLastName;
            //    cgView.rTypeName = cg.rt;
            //    cgView.clientId = cg.clientId;
            //    cgList.Add(cgView);                
            //}





            List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();


            cgList = clientsGroupDetView.GetClientsGroupDetViewList(groupId);


            return View(cgList);
        }
         public ActionResult Index(long groupId=0)
        {

            //var clientsGroup = (from cg in db.clientsGroup
            //                       join r in db.releationType on cg.relType equals r.releationTypeId
            //                       join c in db.clients on cg.clientId equals c.clientId
            //                       join g in db.@group on cg.groupId equals g.groupId
            //                       select new {
            //                           cg.clientsGroupId, cg.groupId, cg.clientId, cg.note1, cg.note2, cg.note3,
            //                           cg.relType, rt = r.relType, c.clientName , c.clientLastName,
            //                           g.gruopFullName 
            //                   }).ToList();


            //    //db.clientsGroup.Include(c => c.releationType);

            //if (groupId > 0)
            //    clientsGroup =(from cg in db.clientsGroup
            //     where cg.groupId == groupId
            //     join r in db.releationType on cg.relType equals r.releationTypeId
            //     join c in db.clients on cg.clientId equals c.clientId
            //     join g in db.@group on cg.groupId equals g.groupId
            //     select new
            //     {
            //         cg.clientsGroupId,
            //         cg.groupId,
            //         cg.clientId,
            //         cg.note1,
            //         cg.note2,
            //         cg.note3,
            //         cg.relType,


            //         rt = r.relType,
            //         c.clientName,
            //         c.clientLastName,
            //         g.gruopFullName


            //     }).ToList();


            //clientsGroupDetView cgView;
            //List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();

            //foreach(var cg in clientsGroup )
            //{
            //    cgView = new ModelsView.clientsGroupDetView();
            //    cgView.clientsGroupId = cg.clientsGroupId;
            //    cgView.groupId = cg.groupId;
            //    cgView.groupName = cg.gruopFullName;
            //    cgView.relType = cg.relType;
            //    cgView.rpFirstName = cg.clientName;
            //    cgView.rpLastName = cg.clientLastName;
            //    cgView.rTypeName = cg.rt;
            //    cgView.clientId = cg.clientId;
            //    cgList.Add(cgView);                
            //}


            long clientId = 0;
            string clientIdStr = Request.QueryString["mclientId"];


            if (!string.IsNullOrWhiteSpace(clientIdStr))
                Int64.TryParse(clientIdStr, out clientId);

            ViewBag.clientId = clientId;

            List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();


            cgList= clientsGroupDetView.GetClientsGroupDetViewList(groupId);

            return View(cgList);
        }

        // GET: clientsGroups/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clientsGroup clientsGroup = db.clientsGroup.Find(id);
            if (clientsGroup == null)
            {
                return HttpNotFound();
            }
            return View(clientsGroup);
        }

        // GET: clientsGroups/Create
        public ActionResult Create()
        {
            ViewBag.relType = new SelectList(db.releationType, "releationTypeId", "relType");
            return View();
        }

        // POST: clientsGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clientsGroupId,groupId,clientId,relType,note1,note2,note3")] clientsGroup clientsGroup)
        {
            if (ModelState.IsValid)
            {
                db.clientsGroup.Add(clientsGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.relType = new SelectList(db.releationType, "releationTypeId", "relType", clientsGroup.relType);
            return View(clientsGroup);
        }

        // GET: clientsGroups/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clientsGroup clientsGroup = db.clientsGroup.Find(id);
            if (clientsGroup == null)
            {
                return HttpNotFound();
            }

            var cInfo = db.clients.Where(c => c.clientId == clientsGroup.clientId).Select(s => s.clientName + " " + s.clientLastName).SingleOrDefault();
            ViewBag.cName = cInfo;

            var gInfo = db.group.Where(g => g.groupId == clientsGroup.groupId).Select(f => f.gruopFullName).SingleOrDefault();
            ViewBag.gName = gInfo;

            ViewBag.relType = new SelectList(db.releationType, "releationTypeId", "relType", clientsGroup.relType);
            return View(clientsGroup);
        }

        // POST: clientsGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clientsGroupId,groupId,clientId,relType,note1,note2,note3")] clientsGroup clientsGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientsGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { groupId = clientsGroup.groupId});
            }

            var cInfo = db.clients.Where(c => c.clientId == clientsGroup.clientId).Select(s => s.clientName + " " + s.clientLastName).SingleOrDefault();
            ViewBag.cName = cInfo;

            var gInfo = db.group.Where(g => g.groupId == clientsGroup.groupId).Select(f => f.gruopFullName).SingleOrDefault();
            ViewBag.gName = gInfo;

            ViewBag.relType = new SelectList(db.releationType, "releationTypeId", "relType", clientsGroup.relType);
            return View(clientsGroup);
        }

        // GET: clientsGroups/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clientsGroup clientsGroup = db.clientsGroup.Find(id);
            if (clientsGroup == null)
            {
                return HttpNotFound();
            }
            return View(clientsGroup);
        }

        // POST: clientsGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            clientsGroup clientsGroup = db.clientsGroup.Find(id);
            db.clientsGroup.Remove(clientsGroup);
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
