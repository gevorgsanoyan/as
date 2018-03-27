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
using PagedList;
using ASFront.Classes;

namespace ASFront.Controllers
{
    [Authorize]
    public class ScoringFormulaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult Create(CreateFormulaPartialViewModel item)
        {
             if (ModelState.IsValid && Request.Form["Save"] !=null && Request.Form["Save"].Equals(Resources.Page.Save) )
            {
                 
                int IndicatorID = item.IndicatorID;

                try
                {
                    if (IndicatorID > 0)
                    {


                        var si = db.ScoringIndicators.Where(p => p.ID == IndicatorID).FirstOrDefault();
                        si.FormulaText = item.FormulaText;
                        si.FormulaTextPriorityFixed = CommonFunction.FormulaFix(item.FormulaText);

                    }

                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(ex.HResult);
                }
                db.SaveChanges();

                CommonFunction.FixValue();


            }



            return RedirectToAction("Index", new { IndicatorID = item.IndicatorID });

        }


        [HttpPost]
        public ActionResult DropDownSelchange(string IndicatorID)
        {



            return RedirectToAction("Index", new { IndicatorID = IndicatorID });


        }
        //public ActionResult ScoringParametersPartial(int page = 1)
        //{

        //    var items = (from sp in db.ScoringParameters
        //                 select new ScoringParametersViewModels() { ID = sp.ID, InputParameterName = sp.InputParameterName, InputParameterValue = sp.InputParameterValue }).ToList();

        //    int pageSize = 1;
        //    int pageNumber = (page);
        //    return PartialView(items.ToPagedList(pageNumber, pageSize));
        //}


        // GET: Scoring
        public ActionResult Index(int pagep = 1, int pagef = 1)
        {
            string IndicatorIDStr = Request.QueryString["IndicatorID"];
            int IndicatorID = 0;

            if (!string.IsNullOrWhiteSpace(IndicatorIDStr))
                Int32.TryParse(IndicatorIDStr, out IndicatorID);

            string FormulaTextStr = string.Empty;
            List<ScoringParametersViewModels> spList = new List<ScoringParametersViewModels>();

         


            if (IndicatorID > 0)
            {
                spList = (
                    from s in  db.ScoringIndicatorsParameters
                     join p in db.ScoringParameters on s.ParameterID equals p.ID
                    where s.IndicatorID== IndicatorID
                    select new ScoringParametersViewModels() { ID = s.ID, InputParameterName = p.InputParameterName, InputParameterValue = p.InputParameterValue }
                       
                    ).ToList();

                FormulaTextStr = db.ScoringIndicators.Where(p => p.ID == IndicatorID).Select(p => p.FormulaText).FirstOrDefault();
            }


            //Where IndicatorID == IndicatorID Select
            //     sp => (new ScoringParametersViewModels() { ID = sp.ID, InputParameterName = sp., InputParameterValue = sp.InputParameterValue })

            int pageSize = ApplicationSettings.PageSize;
            int pageNumberP = (pagep);
            int pageNumberF = (pagef);



            var ind = db.ScoringIndicators.ToList();
            ViewBag.ind = new SelectList(ind, "ID", "IndicatorName");



            FormulaViewModel item = new FormulaViewModel();
            item.ParametersList = spList;
            item.CreateFormula = new CreateFormulaPartialViewModel { FormulaText = FormulaTextStr, IndicatorID= IndicatorID };
            item.IndicatorID = IndicatorID;

            return View(item);
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
