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
using PagedList;

namespace ASFront.Controllers
{
    [Authorize]
    public class ScoringApplicationScoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScoringApplicationScores

        public ActionResult Index(int page = 1)
        {

            string ApplicationIDStr = Request.QueryString["ApplicationID"];
            long ApplicationID = 0;

            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);

            var items = new List<ScoringApplicationScoresView>();

            if (ApplicationID > 0)

            {
                var cl = db.applications.Where(c => c.applicationId == ApplicationID).FirstOrDefault().clientId;
                ViewBag.ClientId = Convert.ChangeType(cl, typeof(long));

                            items = (
                          from sa in db.ScoringApplicationScores
                          join i in db.ScoringIndicators on sa.IndicatorID equals i.ID
                          join a in db.applications on sa.ApplicationID equals a.applicationId

                          where sa.ApplicationID == ApplicationID
                          select new ScoringApplicationScoresView
                          {
                              IndicatorName = i.IndicatorName,
                              ApplicationName = a.applicationId.ToString(),
                              Coefficient = sa.Coefficient,
                              Score = sa.Score,
                              Value = sa.Value,
                              ID = sa.ID
                          }


                          ).Distinct().ToList();

                        }
                        else
                        {

                            items = (
                from sa in db.ScoringApplicationScores
                join i in db.ScoringIndicators on sa.IndicatorID equals i.ID
                join a in db.applications on sa.ApplicationID equals a.applicationId
                select new ScoringApplicationScoresView
                {
                    IndicatorName = i.IndicatorName,
                    ApplicationName = a.applicationId.ToString(),
                    Coefficient = sa.Coefficient,
                    Score = sa.Score,
                    Value = sa.Value,
                    ID = sa.ID
                }


                ).Distinct().ToList();

            }
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = (page);
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult CalculateScores(long ApplicationID, Int64 clientId)
        {
            try
            {

                //if (!db.ScoringApplicationScores.Any(p => p.ApplicationID == ApplicationID))
                //{
                    var prID = db.applications.Where(p => p.applicationId == ApplicationID).Select(p => p.productId).SingleOrDefault();


                    var inds = (
                        from i in db.ScoringIndicators
                        join pi in db.ScoringProductIndicators on i.ID equals pi.IndicatorID
                        where pi.ProductID == prID
                        select i

                                 ).ToList();
                    var appScores = new List<ScoringApplicationScores>();



                    foreach (var Item in inds)
                    {
                        string formulatext = Item.FormulaTextPriorityFixed;

                        formulatext = CommonFunction.FormulaInsertValue(formulatext, Item.ID, clientId);
                        double value = CommonFunction.CalculateValue(formulatext);

                        var score = db.ScoringScores.Where(p => (p.indicatorID == Item.ID && p.maxValue >= value && p.minValue <= value)).FirstOrDefault();

                        var appScoreitem = new ScoringApplicationScores();

                        appScoreitem.ApplicationID = ApplicationID;
                        appScoreitem.Coefficient = score.Coefficient;

                        appScoreitem.IndicatorID = Item.ID;
                        appScoreitem.Value = value;
                        appScoreitem.Score = score.Score;
                        appScoreitem.note1 = score.note1;
                        appScoreitem.note2 = score.note2;
                        appScoreitem.note3 = score.note3;
                        

                        appScores.Add(appScoreitem);

                    }

                    var dappScor = db.ScoringApplicationScores.Where(dp => dp.ApplicationID == ApplicationID);
                    db.ScoringApplicationScores.RemoveRange(dappScor);

                    db.ScoringApplicationScores.AddRange(appScores);
                    db.SaveChanges();

                double totalScore = 0;
                foreach (var ap in appScores)
                {
                    totalScore += ap.Score * ap.Coefficient;                    
                }

                var scorDes = (from sd in db.ScoringScoreDecisions
                               where sd.ProductID == prID && sd.minValue <= totalScore && sd.maxValue >= totalScore
                               select sd.DecisionID).FirstOrDefault();

                if(scorDes < 1)
                {
                    scorDes = (from sd in db.ScoringScoreDecisions
                               where sd.ProductID == prID && sd.maxValue < totalScore
                               orderby sd.maxValue descending
                               select sd.DecisionID).FirstOrDefault();
                }
                

                var dtotscore = db.ApplicationSummary.Where(aps => aps.HaytID == ApplicationID);
                db.ApplicationSummary.RemoveRange(dtotscore);

                var totscore = new ApplicationSummary();
                totscore.HaytID = ApplicationID;
                totscore.ScoreValue = totalScore;
                totscore.ScoreDate = DateTime.Now;
                totscore.ScoreDecisionID = scorDes;
                db.ApplicationSummary.Add(totscore);
                db.SaveChanges();
                //}



            }

            catch (Exception ex)
            {                
                ;
            }

            return RedirectToAction("Index", new { ApplicationID = ApplicationID });
        }
        // GET: ScoringApplicationScores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ScoringApplicationScores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ApplicationID,IndicatorID,Value,Score,Coefficient,note1,note2,note3,CreatedDate,LastModifDate")] ScoringApplicationScores scoringApplicationScores)
        {
            if (ModelState.IsValid)
            {
                db.ScoringApplicationScores.Add(scoringApplicationScores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scoringApplicationScores);
        }

        // GET: ScoringApplicationScores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoringApplicationScores scoringApplicationScores = db.ScoringApplicationScores.Find(id);
            if (scoringApplicationScores == null)
            {
                return HttpNotFound();
            }
            return View(scoringApplicationScores);
        }

        // POST: ScoringApplicationScores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ApplicationID,IndicatorID,Value,Score,Coefficient,note1,note2,note3,CreatedDate,LastModifDate")] ScoringApplicationScores scoringApplicationScores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scoringApplicationScores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scoringApplicationScores);
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
