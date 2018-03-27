using ASFront.Models;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using ASFront.ModelsView;
using ASFront.Classes;

namespace ASFront.Controllers
{
    [Authorize]
    public class ToPDFController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ToPDF
        public ActionResult Index()
        {
            return View();
        }

        #region GoldAcceptanceAct
        public ActionResult GoldAcceptanceActPDF(GoldAcceptanceActViewModels item)
        {
            if (item == null)
            {
                item = new GoldAcceptanceActViewModels();
                item.GoldCollaterals = new List<GoldCollaterals>();
            }




            if (item.GoldCollaterals == null)
                item.GoldCollaterals = db.GoldCollateral.ToList();







            string ApplicationIDStr = Request.QueryString["ApplicationID"];


            if (Request.QueryString["ApplicationID"] != null)
            {
                long ApplicationID = 0;
                if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                    Int64.TryParse(ApplicationIDStr, out ApplicationID);

                if (ApplicationID > 0)
                {
                    var app = db.applications.Where(p => p.applicationId == ApplicationID).FirstOrDefault();

                    var cl = db.clients.Where(p => p.clientId == app.clientId).FirstOrDefault();

                    var goldCollateral = db.GoldCollateral.Include(g => g.applications).Include(g => g.GoldAssayes).Include(g => g.GoldTypes).Where(p => (p.applicationId == ApplicationID)).ToList() ?? new List<GoldCollaterals>();

                    item.GoldCollaterals = goldCollateral;


                    item.QuantitySum = goldCollateral.Sum(p => p.Quantity);
                    item.TotalWeightWithJewelsSum = goldCollateral.Sum(p => p.TotalWeightWithJewels);
                    item.NetWeightSum = goldCollateral.Sum(p => p.NetWeight);
                    item.EstimatedTotalCostAMDSum = goldCollateral.Sum(p => p.EstimatedTotalCostAMD);
                    
                    item.aprDate = app.aprDate;


                    item.CreditSpecialistName = db.UserASProfiles.Where(p => p.UserId == app.userId).Select(p => (p.FirstName + " " + p.LastName )).FirstOrDefault() ?? string.Empty;

                    item.ClientName = cl.clientName + " " + cl.clientLastName   ;

                    item.ClientLastName =  cl.clientLastName  ;
                    item.ClientFirstName = cl.clientName ;


                    List<Items> Items =
                        db.Items.Where(p=> p.applicationId == ApplicationID     ).Distinct().ToList();

                    double credTotSum = 0;
                    double clInvest = 0;
                    

                    foreach (var itm in Items)
                    {
                        credTotSum += itm.Sum;
                        clInvest += itm.ClientInvest;
                    }

                    var product = db.Products.Where(p => p.productId == app.productId).FirstOrDefault();

                    var Currency = db.CurrencyTypes.Where(p => p.currencyTypesId == product.productCurrency).FirstOrDefault();

                    string CurrencyText = " " + Currency.currencyArm;
                    item.CreditTotalAmount =  credTotSum.ToString("N0") + CurrencyText;
                    item.EstimatedTotalCostAMDSumStr = item.EstimatedTotalCostAMDSum.ToString("N0") + CurrencyText;

                    if (item.EstimatedTotalCostAMDSum > 0)
                        item.LoanCollateralRatio = (credTotSum / item.EstimatedTotalCostAMDSum).ToString("N0") + "%";
                    else
                    {
                        item.LoanCollateralRatio = "0%";
                    }



                    item.Currency = CommonFunction.CurrencyIndexDic[2].ToString("N0");
                    item.DocVesion = "version "+"29.12.2017";

                }


            }





            return View(item);
        }

        public ActionResult GoldAcceptanceAct()
        {
            long ApplicationID = 0;



            string ApplicationIDStr = Request.QueryString["ApplicationID"];





            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);





            GoldAcceptanceActViewModels item = new GoldAcceptanceActViewModels(ApplicationID);




            //var app = db.applications.Where(p => p.applicationId == ApplicationID).FirstOrDefault();

            //var cl = db.clients.Where(p => p.clientId == app.clientId).FirstOrDefault();


            //item.ApplicationID = app.applicationId;



            //var goldCollateral = db.GoldCollateral.Include(g => g.applications).Include(g => g.GoldAssayes).Include(g => g.GoldTypes).Where(p => (p.applicationId == ApplicationID)).ToList() ?? new List<GoldCollaterals>();

            //item.GoldCollaterals = goldCollateral;


            //item.QuantitySum = goldCollateral.Sum(p => p.Quantity);
            //item.TotalWeightWithJewelsSum = goldCollateral.Sum(p => p.TotalWeightWithJewels);
            //item.NetWeightSum = goldCollateral.Sum(p => p.NetWeight);
            //item.EstimatedTotalCostAMDSum = goldCollateral.Sum(p => p.EstimatedTotalCostAMD);

            //item.aprDate = app.aprDate;


            //item.CreditSpecialistName = db.UserASProfiles.Where(p => p.UserId == app.userId).Select(p => (p.FirstName + " " + p.LastName )).FirstOrDefault() ?? string.Empty;


            //item.ClientName = cl.clientName + " " + cl.clientLastName ;

            //item.ClientLastName = cl.clientLastName;
            //item.ClientFirstName = cl.clientName;


            //List<Items> Items =
            //            db.Items.Where(p => p.applicationId == ApplicationID).Distinct().ToList();

            //double credTotSum = 0;
            //double clInvest = 0;


            //foreach (var itm in Items)
            //{
            //    credTotSum += itm.Sum;
            //    clInvest += itm.ClientInvest;
            //}

            //var product = db.Products.Where(p => p.productId == app.productId).FirstOrDefault();

            //var Currency = db.CurrencyTypes.Where(p => p.currencyTypesId == product.productCurrency).FirstOrDefault();

            //string CurrencyText = " " + Currency.currencyArm;
            //item.CreditTotalAmount = credTotSum.ToString("N0") + CurrencyText;
            //item.EstimatedTotalCostAMDSumStr = item.EstimatedTotalCostAMDSum.ToString("N0") + CurrencyText;

            //if (item.EstimatedTotalCostAMDSum>0)
            //item.LoanCollateralRatio = (credTotSum/ item.EstimatedTotalCostAMDSum).ToString("N0")+"%";
            //else
            //{
            //    item.LoanCollateralRatio = "0%";
            //}


            //item.Currency = CommonFunction.CurrencyIndexDic[2].ToString("N0");
            //item.DocVesion = "version " + "29.12.2017";

            var model = item;





            var CustomSwitches =
                string.Format(" --no-stop-slow-scripts --javascript-delay 1000   --print-media-type  " +
         " --footer-spacing 0 " +
         " --header-spacing 0 " +

         "	--header-html  \"{0}\" " +
         "	--footer-html  \"{1}\" ",
           Url.Action("GoldAcceptanceActHeader", "ToPDF", new { ApplicationID = ApplicationID }, Request.Url.Scheme),
           Url.Action("GoldAcceptanceActFooter", "ToPDF", null, Request.Url.Scheme)
           );


            string FileNameStr = $"GoldAcceptanceAct-{ApplicationID}.pdf";

            return new Rotativa.ViewAsPdf("GoldAcceptanceActPDF", model)
            {
                FileName = FileNameStr,
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                PageMargins = { Left = 20, Right = 20, Top = 20, Bottom = 20 },

                CustomSwitches = CustomSwitches
            };

        }



        [AllowAnonymous]
        public ActionResult GoldAcceptanceActHeader()
        {

           


           
            return View();
        }



        [AllowAnonymous]
        public ActionResult GoldAcceptanceActFooter()
        {

            return View();
        }

        #endregion // GoldAcceptanceAct


        #region GoldLoanCreditApplication
        public ActionResult GoldLoanCreditApplicationPDF(GoldLoanCreditApplicationViewModels item)
        {

           

            if (item == null)
            {
                item = new GoldLoanCreditApplicationViewModels();
            }


            if (Request.QueryString["ApplicationID"] != null)
            {
                string ApplicationIDStr = Request.QueryString["ApplicationID"];

                long ApplicationID = 0;
                if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                    Int64.TryParse(ApplicationIDStr, out ApplicationID);

                if (ApplicationID > 0)
                {
                    item = new GoldLoanCreditApplicationViewModels(ApplicationID);

                }


            }



            return View(item);
        }

        public ActionResult GoldLoanCreditApplication()
        {
            long ApplicationID = 0;



            string ApplicationIDStr = Request.QueryString["ApplicationID"];





            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);





            GoldLoanCreditApplicationViewModels item = new GoldLoanCreditApplicationViewModels(ApplicationID);




          
            var model = item;



            string RigthCornerText = "Ձև App_002_0_v06 (30.11.17) 0";


             var CustomSwitches =
                string.Format(" --no-stop-slow-scripts --javascript-delay 1000   --print-media-type  " +
         " --footer-spacing 0 " +
         " --header-spacing 0 " +

         "	--header-html  \"{0}\" " +
         "	--footer-html  \"{1}\" ",
           Url.Action("GoldLoanCreditApplicationHeader", "ToPDF", new { ApplicationID = ApplicationID, RigthCornerText= RigthCornerText }, Request.Url.Scheme),
           Url.Action("GoldLoanCreditApplicationFooter", "ToPDF", null, Request.Url.Scheme)
           );


            string FileNameStr = $"GoldLoanCreditApplication-{ApplicationID}.pdf";

            return new Rotativa.ViewAsPdf("GoldLoanCreditApplicationPDF", model)
            {
                FileName = FileNameStr,
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                PageMargins = { Left = 10, Right = 10, Top = 10, Bottom = 10 },

                CustomSwitches = CustomSwitches
            };

        }



        [AllowAnonymous]
        public ActionResult GoldLoanCreditApplicationHeader()
        {

            if (!string.IsNullOrWhiteSpace(Request.QueryString["RigthCornerText"]))
            {
                ViewBag.RigthCornerText =
                Request.QueryString["RigthCornerText"];
            }

            return View();
        }



        [AllowAnonymous]
        public ActionResult GoldLoanCreditApplicationFooter()
        {

            return View();
        }

        #endregion // GoldLoanCreditApplication



        #region UniversalCreditApplication
        public ActionResult UniversalCreditApplicationPDF(UniversalCreditApplicationViewModels item)
        {

            if (item == null)
            {
                item = new UniversalCreditApplicationViewModels();
            }


            if (Request.QueryString["ApplicationID"] != null)
            {
                string ApplicationIDStr = Request.QueryString["ApplicationID"];

                long ApplicationID = 0;
                if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                    Int64.TryParse(ApplicationIDStr, out ApplicationID);

                if (ApplicationID > 0)
                {
                    item = new UniversalCreditApplicationViewModels(ApplicationID);

                }


            }



            return View(item);
        }

        public ActionResult CreditCommiteePDF(CreditCommiteeView item)
        {
            if (item == null)
            {
                item = new CreditCommiteeView();
            }


            if (Request.QueryString["ApplicationID"] != null)
            {
                string ApplicationIDStr = Request.QueryString["ApplicationID"];

                long ApplicationID = 0;
                if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                    Int64.TryParse(ApplicationIDStr, out ApplicationID);

                if (ApplicationID > 0)
                {
                    item = new CreditCommiteeView(ApplicationID);

                }


            }

            return View(item);
        }

        public ActionResult CreditCommitee()
        {
            long ApplicationID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];
            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);

            CreditCommiteeView ccItem = new ModelsView.CreditCommiteeView(ApplicationID);

            string FileNameStr = $"CreditCommitee-{ApplicationID}.pdf";

            return new Rotativa.ViewAsPdf("CreditCommiteePDF", ccItem)
            {
                FileName = FileNameStr,
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                PageMargins = { Left = 10, Right = 10, Top = 10, Bottom = 10 },

            };

        }



        public ActionResult UniversalCreditApplication()
        {
            long ApplicationID = 0;



            string ApplicationIDStr = Request.QueryString["ApplicationID"];





            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);





            UniversalCreditApplicationViewModels item = new UniversalCreditApplicationViewModels(ApplicationID);




            var model = item;


            string RigthCornerText = "Ձև App_001_0_v03 (30.11.17) 0";


            var CustomSwitches =
                string.Format(" --no-stop-slow-scripts --javascript-delay 1000   --print-media-type  " +
         " --footer-spacing 0 " +
         " --header-spacing 0 " +

         "	--header-html  \"{0}\" " +
         "	--footer-html  \"{1}\" ",
           Url.Action("UniversalCreditApplicationHeader", "ToPDF", new { ApplicationID = ApplicationID, RigthCornerText=RigthCornerText }, Request.Url.Scheme),
           Url.Action("UniversalCreditApplicationFooter", "ToPDF", null, Request.Url.Scheme)
           );


            string FileNameStr = $"UniversalCreditApplication-{ApplicationID}.pdf";

            return new Rotativa.ViewAsPdf("UniversalCreditApplicationPDF", model)
            {
                FileName = FileNameStr,
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                PageMargins = { Left = 10, Right = 10, Top = 10, Bottom = 10 },

                CustomSwitches = CustomSwitches
            };

        }



        [AllowAnonymous]
        public ActionResult UniversalCreditApplicationHeader()
        {

            if (!string.IsNullOrWhiteSpace(Request.QueryString["RigthCornerText"]))
            {
                ViewBag.RigthCornerText =
                Request.QueryString["RigthCornerText"];
            }

            return View();
        }



        [AllowAnonymous]
        public ActionResult UniversalCreditApplicationFooter()
        {

            return View();
        }

        #endregion // UniversalCreditApplication







    }//public class ToPDFController : Controller






}