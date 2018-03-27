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
using ASFront.Classes;

using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;

namespace ASFront.Controllers
{
    public class groupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: groups
        public ActionResult Index(long groupId = 0)
        {           

            var group = db.group.ToList();
            if (groupId > 0)
                group = db.group.Where(g => g.groupId == groupId).ToList();

            return View(group);
        }

        // GET: groups/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            group group = db.group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }


        public ActionResult ClientsGroupMembersInput(long clientId)
        {


            string errMsg = TempData["errMsg"] as string;

            if (errMsg != null)
            {

                ViewBag.errMsg = errMsg;
            }

            string OtherGroupMemberMessage = TempData["OtherGroupMemberMessage"] as string;
            if (OtherGroupMemberMessage != null)
            {

                ViewBag.OtherGroupMemberMessage = OtherGroupMemberMessage;
            }



            //ViewBag.errMsg = "";
            var client = db.clients.Where(c => c.clientId == clientId).ToList();
            string lastName = "";
            string Address = "";

            foreach (var cl in client)
            {
                lastName = cl.clientLastName;
                Address = cl.rRegion + " " + cl.rCity + " " + cl.rStreet + " " + cl.rBuilding + " " + cl.rApartment;
            }


            ViewBag.rType = new SelectList(db.releationType, "releationTypeId", "relType");

            group group = GroupCreate(lastName, Address);

            var cg = db.clientsGroup.Where(cgi => cgi.clientId == clientId).ToList();
            if (cg.Count > 0)
            {
            }
            else
            {
                clientsGroup newClientsGroup = new clientsGroup();
                newClientsGroup.clientId = clientId;
                newClientsGroup.groupId = group.groupId;
                newClientsGroup.relType = 1;
                //newClientsGroup.AcraLastRequestDate = DateTime.Now;
                db.clientsGroup.Add(newClientsGroup);
                db.SaveChanges();
            }

            ViewBag.grName = group.gruopFullName;

            clientsGroupView gw = new ModelsView.clientsGroupView();
            gw.groupId = group.groupId;

            cgTotalView cgTotal = new ModelsView.cgTotalView();
            cgTotal.groupName = group.gruopFullName;
            cgTotal.groupId = group.groupId;
            cgTotal.gMmebers = new List<ModelsView.clientsGroupView>();
            cgTotal.SingleClientID = clientId;

            for (int i = 0; i < 10; i++)
                cgTotal.gMmebers.Add(gw);



            return View(cgTotal);
        }

        [HttpPost]
        public ActionResult ClientsGroupMembersInput(cgTotalView cgTotal)
        {
            string errMsg = "";
            TempData["errMsg"] = errMsg;
            ViewBag.rType = new SelectList(db.releationType, "releationTypeId", "relType");
            clientsGroupView cn;
            List<clientsGroupView> cnList = new List<ModelsView.clientsGroupView>();
            cgTotal.gMmebers = new List<clientsGroupView>();

            for (int i = 0; i < 10; i++)
            {
                cn = new clientsGroupView();
                cn.groupId = cgTotal.groupId;
                cn.rpFirstName = Request.Form["fName" + i.ToString()];
                cn.rpLastName = Request.Form["lName" + i.ToString()];
                cn.rpSoc = Request.Form["Soc" + i.ToString()];
                if (Request.Form["rType" + i.ToString()].Length > 0)
                {
                    cn.relType = int.Parse(Request.Form["rType" + i.ToString()]);
                }






                cnList.Add(cn);
                cgTotal.gMmebers.Add(cn);
            }





            string isInOtherGroupErrMsg = "Նշված անձիք արդեն իսկ գտնվում են այլ խմբերում: ";

            bool isInOtherGroupMember = false;

            foreach (var c in cnList)
            {
                if (c.rpSoc.Length > 0)
                {
                    clientsGroup newclientsGroup = new clientsGroup();
                    newclientsGroup.groupId = c.groupId;
                    var exClient = db.clients.Where(e => e.socNumb == c.rpSoc).ToList();
                    if (exClient.Count > 0)
                    {
                        newclientsGroup.clientId = exClient[0].clientId;
                    }
                    else
                    {
                        if (c.rpSoc.Length == 10)
                        {
                            var t = CommonFunction.GetInfroFromSoc(c.rpSoc);
                            clients newClient = new clients();
                            newClient.clientName = c.rpFirstName;
                            newClient.clientLastName = c.rpLastName;
                            newClient.clientMidName = "_";
                            newClient.socNumb = c.rpSoc;
                            newClient.dob = t.Item2;
                            newClient.sex_clientSexId = t.Item1;
                            newClient.passpNumb = "1";
                            newClient.passpDate = DateTime.Now;
                            newClient.passpAuth = "001";
                            newClient.rRegion = "R";
                            newClient.rCity = "C";
                            newClient.rStreet = "S";
                            newClient.rBuilding = "B";
                            newClient.mob1 = "091000000";
                            newClient.edu_educationId = 1;
                            newClient.isSameAddress = false;
                            newClient.isRented = false;




                            

                            int BranchtId = 0;
                            int UserASProfileId = 0;
                            string UserId = User.Identity.GetUserId(); ;

                            UserASProfileId = db.UserASProfiles.Where(p => p.UserId == UserId).Select(p => p.UserASProfileId).FirstOrDefault();
                            BranchtId = db.BranchUsers.Where(p => p.UserASProfileId == UserASProfileId).Select(p => p.BrancheId).FirstOrDefault();

                            if (BranchtId == 0)
                                BranchtId = 1;

                            newClient.BranchtId = BranchtId;

                            try
                            {
                                db.clients.Add(newClient);
                                db.SaveChanges();
                                newclientsGroup.clientId = newClient.clientId;
                            }
                            catch (DbEntityValidationException dbEx)
                            {
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                                    }
                                }
                            }





                        }
                        else
                        {
                            errMsg += "Սխալ մուտքագրված սոց.քարտի համար: # " + c.rpSoc + " # Խնդրում եմ մուտքագրել կրկին: ";
                            break;
                        }//soc check
                    }
                    if (c.relType > 0)
                        newclientsGroup.relType = c.relType;
                    else
                        newclientsGroup.relType = 6;


                    var tmpclGr = db.clientsGroup.Where(t => t.clientId == newclientsGroup.clientId).ToList();
                    if (tmpclGr.Count > 0)
                    {
                        isInOtherGroupMember = true;
                        foreach (var tg in tmpclGr)
                        {
                            string clientFullName = db.clients.Where(cl => cl.clientId == tg.clientId).Select(n => n.clientName + " " + n.clientLastName).SingleOrDefault();
                            string exGroupName = db.group.Where(gr => gr.groupId == tg.groupId).Select(gn => gn.gruopFullName).SingleOrDefault();
                            isInOtherGroupErrMsg += clientFullName + ", խումբ - " + exGroupName;
                        }
                    }



                    else
                    {
                        //newclientsGroup.AcraLastRequestDate = DateTime.Now;//.AddYears(-1000);
                        db.clientsGroup.Add(newclientsGroup);
                        db.SaveChanges();
                    }
                }//if(c.rpSoc.Length > 0)
            }



            if (errMsg.Length > 0)
            {
                if (isInOtherGroupMember)
                {

                    ViewBag.OtherGroupMemberMessage = isInOtherGroupErrMsg;
                    TempData["OtherGroupMemberMessage"] = isInOtherGroupErrMsg;
                }
                  

                ViewBag.errMsg = errMsg;   
                TempData["errMsg"] = errMsg;


                return RedirectToAction("ClientsGroupMembersInput", "groups", new { clientID = cgTotal.SingleClientID });
            }
            else
            {

                if (!isInOtherGroupMember)
                {
                    return RedirectToAction("Index", "clientsGroups", new { groupId = cgTotal.groupId });
                }
                else
                {
                    //ViewBag.errMsg = "";;
                    ViewBag.OtherGroupMemberMessage = isInOtherGroupErrMsg;
                    //return View(cgTotal);
                    TempData["OtherGroupMemberMessage"] = isInOtherGroupErrMsg;
                }

                return RedirectToAction("ClientsGroupMembersInput", "groups", new { clientID = cgTotal.SingleClientID });
            }

        }


        public ActionResult ClientInputToGroup(long clientId, long groupId, long mclientId)
        {
            string errMsg = "";
            long clGroupId = 0;


            ViewBag.clientId = mclientId;

            clGroupId = db.clientsGroup.Where(g => g.clientId == clientId).Select(s => s.groupId).SingleOrDefault();
            if (clGroupId > 0)
            {
                errMsg = "Տվյալ անձը գտնվում է հետևյալ խմբում. " + db.group.Where(g => g.groupId == clGroupId).Select(s => s.gruopFullName).SingleOrDefault();
            }
            else
            {
                long mclGroupId = db.clientsGroup.Where(g => g.clientId == mclientId).Select(s => s.groupId).SingleOrDefault();
                if (mclGroupId == 0)
                {
                    group gr = new group();
                    var mclient = db.clients.Where(c => c.clientId == mclientId).ToList();
                    foreach (var mcl in mclient)
                    {
                        gr.gruopName = mcl.clientLastName;
                        gr.gruopFullName = mcl.rRegion + " " + mcl.rCity + " " + mcl.rBuilding + " " + mcl.rBuilding + " " + mcl.clientLastName;
                        gr.gruopAddress = mcl.rRegion + " " + mcl.rCity + " " + mcl.rBuilding + " " + mcl.rBuilding;
                        db.group.Add(gr);
                        try
                        {
                            db.SaveChanges();
                            mclGroupId = gr.groupId;
                        }
                        catch (Exception ex)
                        {
                            errMsg = "DB Save error (creating group " + gr.gruopFullName + " )." + ex.Message;
                        }
                    }//foreach(var mcl in mclient)
                }

                /////////////////////////////////////////////////////////////////////////
                clientsGroup mcGroup = new clientsGroup();
                mcGroup.groupId = mclGroupId;
                mcGroup.clientId = clientId;
                mcGroup.relType = 6;
                db.clientsGroup.Add(mcGroup);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    errMsg = "DB Save error (saving client  (id)#" + clientId.ToString() + " to group (id#)" + mclGroupId.ToString() + " )." + ex.Message;
                }
                mcGroup.groupId = mclGroupId;
                mcGroup.clientId = mclientId;
                mcGroup.relType = 6;
                db.clientsGroup.Add(mcGroup);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    errMsg = "DB Save error (saving client  (id)#" + clientId.ToString() + " to group (id#)" + mclGroupId.ToString() + " )." + ex.Message;
                }
                groupId = mclGroupId;
                /////////////////////////////////////////////////////////////////////////

            }//if (clGroupId > 0)


            if (errMsg == "")
                return RedirectToAction("Index", "clientsGroups", new { groupId = groupId, mclientId = mclientId });
            else
            {
                ViewBag.errMsg = errMsg;
                return View();
            }
                
        }


        public group GroupCreate(string gLastName, string gAddress)
        {
            var gr = db.group.Where(g => g.gruopAddress == gAddress).ToList();
            group grp = new Models.group();

            if (gr.Count > 0)
            {
                foreach (var g in gr)
                {
                    grp.groupId = g.groupId;
                    grp.gruopAddress = g.gruopAddress;
                    grp.gruopFullName = g.gruopFullName;
                    grp.gruopName = g.gruopName;
                    grp.note1 = g.note1;
                    grp.note2 = g.note2;
                    grp.note3 = g.note3;
                }
            }
            else
            {
                grp.gruopAddress = gAddress;
                grp.gruopName = gLastName;
                grp.gruopFullName = gAddress + " " + gLastName;
                db.group.Add(grp);
                db.SaveChanges();
            }

            return grp;
        }

        // GET: groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "groupId,gruopName,gruopAddress,gruopFullName,note1,note2,note3")] group group)
        {
            if (ModelState.IsValid)
            {
                var gr = db.group.Where(g => g.gruopAddress == group.gruopAddress).ToList();
                if (gr.Count > 0)
                {
                    ViewBag.isExist = "Խումբն արդեն գոյություն ունի:";
                }
                else
                {
                    db.group.Add(group);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(group);
        }

        // GET: groups/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            group group = db.group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "groupId,gruopName,gruopAddress,gruopFullName,note1,note2,note3")] group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: groups/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            group group = db.group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            group group = db.group.Find(id);
            db.group.Remove(group);
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
