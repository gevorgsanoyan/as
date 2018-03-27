using ASFront.Classes;
using ASFront.Models;
using ASFront.ModelsView;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;
using Telegram.Bot;



namespace ASFront.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        ApplicationDbContext db;

        public ApplicationController()
        {
            db = new Models.ApplicationDbContext();
        }


        public ActionResult ApplicationSummary()
        {
            ApplicationSummaryViewModel sum = new ApplicationSummaryViewModel();

            sum.AppSumBriefDataViewModel = new AppSumBriefDataViewModel();
            sum.AppSumContractViewModel = new AppSumContractViewModel();
            sum.AppSumDecisionMakingViewModel = new List<AppSumDecisionMakingViewModel>();
            sum.AppSumDetailPurposeViewModel = new AppSumDetailPurposeViewModel();
            sum.AppSumProductDescriptionViewModel = new List<AppSumProductDescriptionViewModel>();
            sum.AppSumScoringScoreDecisionViewModel = new AppSumScoringScoreDecisionViewModel();
            sum.AppSumScoringScoreViewModel = new List<AppSumScoringScoreViewModel>();

            sum.Balance = new BalanceViewModel();

            sum.Turnover = new TurnoverAppSumViewModels();

            sum.AgroAssets = new List<AgroAsset>();

            sum.IncomeExpenses = new IncomeExpensesViewModel();
            sum.LoanInsurance = new LoanInsuranceViewModel();





            long ApplicationID = 0;
            string ApplicationIDStr = Request.QueryString["ApplicationID"];


            if (!string.IsNullOrWhiteSpace(ApplicationIDStr))
                Int64.TryParse(ApplicationIDStr, out ApplicationID);





            ApplicationViewModel Application = new ApplicationViewModel();
            var items = new List<ItemViewModel>();

            if (ApplicationID > 0)
            {
                var app = db.applications.Find(ApplicationID);

                if (app == null)
                {
                    return RedirectToAction("Index");
                }



                if (TempData["Score"] != null)
                {
                    ViewBag.Score = TempData["Score"];
                }


                ViewBag.FileTable = db.DocsApllications.Where(p => p.ApplicationId == ApplicationID).ToList();



                sum.AgroAssets = db.AgroAsset.Where(p => p.applicationId == ApplicationID).OrderBy(p => p.AgroAssetTypes.Name).ThenBy(p => p.Description).ToList();




                IncomeExpensesViewModel ivm = new IncomeExpensesViewModel();

                IncomeExpenses Inc = db.IncomeExpenses.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();


                if (Inc != null)
                {


                    ivm = new IncomeExpensesViewModel(Inc);



                }


                sum.Turnover = new TurnoverAppSumViewModels(ApplicationID);

                sum.IncomeExpenses = ivm;


                Balance balance = db.Balance.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();


                if (balance != null)
                {


                    BalanceViewModel bvm = new BalanceViewModel(balance);


                    sum.Balance = bvm;


                }





                LoanInsuranceViewModel LoanInsurance = new LoanInsuranceViewModel();

                LoanInsurance.applicationId = ApplicationID;
                LoanInsurance.GuarantorsList = GuarantorsViewModel.GetGuarantorsViewModelList(ApplicationID);


                

                double MovableEstateSum = db.MovableEstate.Where(p => p.applicationId == ApplicationID).Sum(p => (double ? )p.EstimatedTotalCostAM) ?? 0;
                double RealtyEstateSum = db.RealtyEstate.Where(p => p.applicationId == ApplicationID).Sum(p => (double?)p.TotRatedPrice) ?? 0;
                double GoldCollateralSum = db.GoldCollateral.Where(p => p.applicationId == ApplicationID).Sum(p => (double?)p.EstimatedTotalCostAMD) ?? 0;

                LoanInsurance.MovableEstateSum = MovableEstateSum;
                LoanInsurance.RealtyEstateSum = RealtyEstateSum;
                LoanInsurance.GoldCollateralSum = GoldCollateralSum;

                LoanInsurance.MovableEstateSumStr = MovableEstateSum.ToString("N0");
                LoanInsurance.RealtyEstateSumStr = RealtyEstateSum.ToString("N0");
                LoanInsurance.GoldCollateralSumStr = GoldCollateralSum.ToString("N0");

                double lnCltrl = 0;
                if (app.creditSumAMD != null)
                {
                    try
                    {
                        lnCltrl = (double)((app.creditSumAMD / (GoldCollateralSum + RealtyEstateSum + MovableEstateSum)) * 100);                        
                    }
                    catch
                    {
                        lnCltrl = 0;
                    }
                }
                LoanInsurance.loanCollateral = lnCltrl.ToString("N0");

                sum.LoanInsurance = LoanInsurance;










                var clientName = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList().Select(p => p.clientName).FirstOrDefault();

                var branchName =

                    db.Branches.Where(p => p.Id == app.branchId).Select(p => p.Branch).FirstOrDefault();


                var product = db.Products.Where(p => p.productId == app.productId).FirstOrDefault();
                var productName = product.productName;
                var Currency = db.CurrencyTypes.Where(p => p.currencyTypesId == product.productCurrency).FirstOrDefault();
                string CurrencyText = " " + Currency.currencyArm;
                string CurrencySign = "";


                var SellerName = db.Sellers.Where(p => p.Id == app.SellerId).Select(p => p.FirstName + " " + p.LastName + " " + p.Patronymic).FirstOrDefault();

                var userFullName = db.UserASProfiles.Where(p => p.UserId == app.userId).Select(p => p.FirstName + " " + p.LastName).FirstOrDefault();
                var appuserFullName =
                db.UserASProfiles.Where(p => p.UserASProfileId == app.appuserId).Select(p => p.FirstName + " " + p.LastName).FirstOrDefault();

                var SupplierID = db.SupplierBranches.Where(p => p.BrancheId == app.branchId).Select(p => p.SupplierId).FirstOrDefault();
                var SupplierName = db.Suppliers.Where(p => p.SupplierId == SupplierID).Select(p => p.SupplierName).FirstOrDefault();
                var appStatusName = db.appStatus.Where(p => p.appStatusId == app.appStatus).Select(p => p.appStatusArm).FirstOrDefault();

                var up = db.UserASProfiles.Where(p => p.UserId == app.userId).FirstOrDefault();

                Application = new ApplicationViewModel
                {
                    agrNumb = app.agrNumb,
                    agrNumbP = app.agrNumbP,
                    appDate = app.appDate,
                    appDescr = app.appDescr,
                    applicationId = app.applicationId,
                    appStatus = app.appStatus,
                    appuserId = appuserFullName,
                    aprDate = app.aprDate,
                    branchId = app.branchId,
                    clientId = app.clientId,


                    branchName = branchName,
                    clientName = clientName,
                    productName = productName,
                    SellerName = SellerName,
                    userName = userFullName,
                    SupplierName = SupplierName,
                    appStatusName = appStatusName,


                    CreditTerm = app.CreditTerm,
                    note1 = app.note1,
                    note2 = app.agrNumb,
                    note3 = app.agrNumb,
                    note4 = app.agrNumb,
                    note5 = app.agrNumb,
                    productId = app.productId,

                    SellerId = app.SellerId,

                    userId = app.agrNumb

                };



                sum.AppSumDetailPurposeViewModel.appDate = app.appDate;
                sum.AppSumDetailPurposeViewModel.appDescr = app.appDescr;
                sum.AppSumDetailPurposeViewModel.appStatus = appStatusName;
                sum.AppSumDetailPurposeViewModel.aprDate = app.aprDate;
                sum.AppSumDetailPurposeViewModel.userName = userFullName;
                sum.AppSumDetailPurposeViewModel.Branch = branchName;



                sum.AppSumDetailPurposeViewModel.asCode = up?.asUserId;


                List<ItemViewModel> Items = (
                       from i in db.Items
                       join a in db.applications on i.applicationId equals a.applicationId
                       join c in db.clients on i.clientId equals c.clientId
                       join s in db.Suppliers on i.SupplierId equals s.SupplierId
                       join pp in db.ProductPurposes on i.FKProductPurposeId equals pp.Id
                       join pr in db.Products on pp.ProductId equals pr.productId
                       join pur in db.Purposes on pp.PurposeId equals pur.Id

                       where i.applicationId == ApplicationID

                       select new ItemViewModel()
                       {
                           Id = i.Id,

                           ItemName = i.ItemName,
                           ItemDescr = i.ItemDescr,
                           ClientInvest = i.ClientInvest,
                           Count = i.Count,
                           Price = i.Price,
                           Sum = i.Sum,

                           applicationName = a.agrNumb,
                           ProductPurposeName = pr.productName + " - " + pur.PurposeName,
                           clientName = c.clientName + " " + c.clientLastName + " " + c.clientMidName ?? "",
                           SupplierName = s.SupplierName
                       }


                    ).Distinct().ToList();

                double credTotSum = 0;
                double clInvest = 0;
                double MonthlyPaymentSize = 0;
                try
                {
                    credTotSum = app.creditSum.Value;
                }
                catch
                {
                    foreach (var itm in Items)
                    {
                        credTotSum += itm.Sum;
                    }
                }                
                foreach (var itm in Items)
                {
                    clInvest += itm.ClientInvest;
                }

                sum.AppSumBriefDataViewModel.applicationId = ApplicationID.ToString();
                sum.AppSumBriefDataViewModel.CreditTotalAmount = CurrencySign + credTotSum.ToString("N0") + CurrencyText;
                sum.AppSumBriefDataViewModel.Investment = CurrencySign + clInvest.ToString("N0") + CurrencyText;
                sum.AppSumBriefDataViewModel.CreditTerm = app.CreditTerm.ToString() + " " + "Ամիս";





                sum.AppSumBriefDataViewModel.productName = product.productName;
                sum.AppSumBriefDataViewModel.productCurrency = CurrencySign + CurrencyText;
                sum.AppSumBriefDataViewModel.anualRate = (product.anualRate * 100).ToString("G2") + "%";
                sum.AppSumBriefDataViewModel.appFee = CurrencySign +
                   (product.appFee).ToString("N2") + CurrencyText;
                sum.AppSumBriefDataViewModel.mothFee = CurrencySign +
                    (product.mothFee * credTotSum + product.mothFeeFlat).ToString("N2") + CurrencyText;
                sum.AppSumBriefDataViewModel.upfronFee = CurrencySign +
                    (product.upfronFee * credTotSum).ToString("N2") + CurrencyText;



                MonthlyPaymentSize = CommonFunction.PMTCalc(app.applicationId, credTotSum);



                sum.AppSumBriefDataViewModel.MonthlyPaymentSize = CurrencySign + MonthlyPaymentSize.ToString("N0") + CurrencyText;


                List<AppSumProductDescriptionViewModel> AppSumProductDescriptionViewModel = (
                   from i in db.Items
                   join a in db.applications on i.applicationId equals a.applicationId
                   join c in db.clients on i.clientId equals c.clientId
                   join s in db.Suppliers on i.SupplierId equals s.SupplierId
                   join pp in db.ProductPurposes on i.FKProductPurposeId equals pp.Id
                   join pr in db.Products on pp.ProductId equals pr.productId
                   join pur in db.Purposes on pp.PurposeId equals pur.Id

                   where i.applicationId == ApplicationID

                   select new AppSumProductDescriptionViewModel()
                   {


                       ItemName = i.ItemName,

                       ClientInvest = i.ClientInvest,
                       Count = i.Count,
                       Price = i.Price,
                       Sum = i.Sum


                   }


                ).Distinct().ToList();



                sum.AppSumProductDescriptionViewModel = AppSumProductDescriptionViewModel;





                var appsum = db.ApplicationSummary.Where(h => h.HaytID == ApplicationID).FirstOrDefault();
                AppSumScoringScoreDecisionViewModel sd = new ModelsView.AppSumScoringScoreDecisionViewModel();
                string DecisionText = string.Empty;
                int ScoringDecisionID = 0;
                if (appsum != null)
                {
                    //sd.Id = apps.Id;
                    sd.Score = appsum.ScoreValue;
                    sd.ScoreDate = appsum.ScoreDate;
                    var scDec = db.ScoringDecisions.Where(dt => dt.ID == appsum.ScoreDecisionID).FirstOrDefault();
                    if (scDec != null)
                    {
                        DecisionText = scDec.Decision;
                        ScoringDecisionID = scDec.ID;
                    }

                    sd.Decision = DecisionText;
                }


                sum.AppSumScoringScoreDecisionViewModel = sd;

                ViewBag.ApplicationID = ApplicationID;
                ViewBag.ClientID = Application.clientId;

                ViewBag.appuserFullName = appuserFullName;



                sum.Application = Application;
                sum.clientId = Application.clientId;
                sum.ApplicationID = Application.applicationId;



                List<AppSumScoringScoreViewModel> AppSumScoringScoreViewModelList = new List<ModelsView.AppSumScoringScoreViewModel>();

                AppSumScoringScoreViewModelList = (
              from sa in db.ScoringApplicationScores
              join i in db.ScoringIndicators on sa.IndicatorID equals i.ID


              where sa.ApplicationID == ApplicationID
              select new AppSumScoringScoreViewModel
              {
                  IndicatorName = i.IndicatorName,


                  Score = sa.Score,
                  Value = sa.Value

              }


              ).Distinct().ToList();

                sum.AppSumScoringScoreViewModel = AppSumScoringScoreViewModelList;


                List<AppSumScoringScoreDecisionViewModel> AppSumScoringScoreDecisionViewModel = new List<AppSumScoringScoreDecisionViewModel>();



                ProductLimits pl = db.ProductLimits.Where(p => p.ProductID == app.productId && p.AmountLimit >= credTotSum).FirstOrDefault();

                ScoringDecisions Dis = new ScoringDecisions();

                List<string> usersIDListR1 = new List<string>();
                List<string> usersIDListR2 = new List<string>();
                if (pl != null)
                {
                    usersIDListR1 = db.UserAppTable.Where(u => u.prodLimitId == pl.Id && u.maxLimit >= credTotSum && u.appTypeId == 1).Distinct().Take(3).Select(p => p.userId).ToList();
                    usersIDListR2 = db.UserAppTable.Where(u => u.prodLimitId == pl.Id && u.maxLimit >= credTotSum && u.appTypeId == 2).Distinct().Take(3).Select(p => p.userId).ToList();


                }


                List<AppSumDecisionMakingViewModel> AppSumDecisionMakingViewModelList = new List<AppSumDecisionMakingViewModel>();

                AppSumDecisionMakingViewModel appDes = new AppSumDecisionMakingViewModel();

                if (pl == null)
                    ViewBag.ErrorText = Resources.Messages.ApprovalRequirementNull;

                var k = db.Users.ToList();

                List<UserASProfiles> usersListR1 = new List<UserASProfiles>();
                List<UserASProfiles> usersListR2 = new List<UserASProfiles>();
                if (usersIDListR1.Count > 0)
                {
                    usersListR1 = db.UserASProfiles.Where(p => usersIDListR1.Contains(p.UserId)).ToList();
                }

                if (usersIDListR2.Count > 0)
                {
                    usersListR2 = db.UserASProfiles.Where(p => usersIDListR2.Contains(p.UserId)).ToList();
                }


                List<NotificationInfo> NotificationInfoR1 = new List<NotificationInfo>();
                List<NotificationInfo> NotificationInfoR2 = new List<NotificationInfo>();

                foreach (var u in usersListR1)
                {
                    NotificationInfo nat = new NotificationInfo();
                    nat.UserFullName = $"{u.FirstName} {u.LastName}";
                    nat.UserID = u.UserId;
                    NotificationInfoR1.Add(nat);
                }

                foreach (var u in usersListR2)
                {
                    NotificationInfo nat = new NotificationInfo();
                    nat.UserFullName = $"{u.FirstName} {u.LastName}";
                    nat.UserID = u.UserId;
                    NotificationInfoR2.Add(nat);
                }


               
                var ApplicationAppruvesLast = db.ApplicationAppruves.Where(p => p.ApplicationAppruvesId == (db.ApplicationAppruves.Where(m => m.appId == app.applicationId).Max(m => m.ApplicationAppruvesId))).FirstOrDefault();


                ApplicationAppruves ApplicationAppruvesR1 = new ApplicationAppruves();
                ApplicationAppruves ApplicationAppruvesR2 = new ApplicationAppruves();

                if (usersIDListR1.Count > 0)
                {
                    ApplicationAppruvesR1 = db.ApplicationAppruves.Where(p => p.appId == app.applicationId && usersIDListR1.Contains(p.appUserId)).OrderByDescending(p => p.appId).FirstOrDefault();
                }

                if (usersIDListR2.Count > 0)
                {
                    ApplicationAppruvesR2 = db.ApplicationAppruves.Where(p => p.appId == app.applicationId && usersIDListR2.Contains(p.appUserId)).OrderByDescending(p => p.appId).FirstOrDefault();
                }


                

                appDes.RowName = "Գնահատական";

                if (pl != null)
                    appDes.ApprovalRequirement = pl.Scoring;



                if (appsum != null)
                {
                    string CurrentState = appsum.ScoreValue.ToString("N0");

                    if (!string.IsNullOrWhiteSpace(DecisionText))
                        CurrentState += $"/{DecisionText}";
                    appDes.CurrentState = CurrentState;
                    Dis = db.ScoringDecisions.Where(p => p.ID == appsum.ScoreDecisionID).FirstOrDefault();
                }
                else
                {
                    appDes.CurrentState = appStatusName;
                }
                
                appDes.Verifying = "";
                appDes.Notification = "";


                

                AppSumDecisionMakingViewModelList.Add(appDes);
                appDes = new AppSumDecisionMakingViewModel();



                appDes.RowName = "Օղակ 1";

                if (pl != null)
                    appDes.ApprovalRequirement = pl.App1;
                appDes.AppGrade = 1;

                if (ApplicationAppruvesR1 != null)//&& ApplicationAppruvesR1.aprStatus == 4
                {

                    appDes.CurrentState = ApplicationAppruvesR1.creditSum.ToString("N0");


                }
                if (pl != null && (pl.App1 ?? false))
                    appDes.Verifying = "Օղակ 1";
                if (usersListR1.Count > 0)
                {
                    appDes.NotificationInfo = NotificationInfoR1;

                }

                
                AppSumDecisionMakingViewModelList.Add(appDes);
                appDes = new AppSumDecisionMakingViewModel();


                appDes.RowName = "Օղակ 2";

                if (pl != null)
                    appDes.ApprovalRequirement = pl.App2;
                appDes.AppGrade = 2;

                if (ApplicationAppruvesR2 != null && ApplicationAppruvesR2.aprStatus == 4)
                {

                    appDes.CurrentState = ApplicationAppruvesR2.creditSum.ToString("N0");


                }

                if (pl != null && (pl.App2 ?? false))
                    appDes.Verifying = "Օղակ 2";

                if (usersListR2.Count > 0)
                {
                    appDes.NotificationInfo = NotificationInfoR2;


                }


                if (pl != null && pl.App2.HasValue && pl.App2.Value)
                {
                    if (appsum != null)
                    {
                        appDes.FinalDecision = Dis.Decision;


                        if (appsum.ScoreDecisionID > 0 && CommonFields.ButtonClassesList.Count - 1 >= appsum.ScoreDecisionID)
                        {
                            appDes.btnClass = CommonFields.ButtonClassesList[appsum.ScoreDecisionID];
                        }
                        else
                        {
                            appDes.btnClass = CommonFields.ButtonClassesList[0];
                        }
                    }
                }

                AppSumDecisionMakingViewModelList.Add(appDes);
                appDes = new AppSumDecisionMakingViewModel();

                appDes.RowName = "Հաստատված վերջնական գումար";

                appDes.ApprovalRequirement = null;

                bool isApproved = false;
                bool isShowApproveButton = true;
                if (app.appStatus == 4)
                {
                    isApproved = true;
                    isShowApproveButton = false;

                    appDes.CurrentState = credTotSum.ToString("N0");
                    appDes.FinalDecision = credTotSum.ToString();
                    appDes.btnClass = CommonFields.ButtonClassesList[1];
                }
                if (app.appStatus == 4 || app.appStatus == 5)
                {
                    isShowApproveButton = false;
                }
                else
                {
                    isShowApproveButton = true;
                }

                ViewBag.Approved = isApproved;
                ViewBag.NotRejected = isShowApproveButton;
                appDes.Verifying = "";
                appDes.Notification = "";
                ViewBag.isShowApproveButton = isShowApproveButton;
                appsum = new Models.ApplicationSummary();
                
                if (app.appStatus == 4)
                    AppSumDecisionMakingViewModelList.Add(appDes);
                appDes = new AppSumDecisionMakingViewModel();



                ViewBag.grPeriod = "";
                if (isApproved && ApplicationAppruvesLast != null)
                {
                    ViewBag.grPeriod = ApplicationAppruvesLast.grPeriod.ToString("N0");

                    if (appsum.Appfinal != null && ApplicationAppruvesLast.appUserId != appsum.Appfinal)
                    {
                        ApplicationAppruvesLast.appUserId = appsum.Appfinal;

                    }


                    ViewBag.Appruves = new ApplicationAppruvesViewModels(ApplicationAppruvesLast);
                }

                sum.AppSumDecisionMakingViewModel = AppSumDecisionMakingViewModelList;


            }
            else
            {
                return RedirectToAction("Index");
            }
            ViewBag.Client = db.clients.Distinct().Where(p => p.clientId == Application.clientId).Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList().Select(p => p.clientName).FirstOrDefault();

            ViewBag.socNumb = db.clients.Distinct().Where(p => p.clientId == Application.clientId).Select(p => p.socNumb).FirstOrDefault();


            ViewBag.AppInfo = Application.appDate.ToString();
            ViewBag.appDate = Application.appDate.ToString();
            ViewBag.AppNumber = Application.applicationId;

            ViewBag.ApplicationID = ApplicationID.ToString();




            long groupId = 0;

            #region ClientGroup


            var clientsGroupItem = db.clientsGroup.Where(g => g.clientId == Application.clientId).FirstOrDefault();

            if (clientsGroupItem != null && clientsGroupItem.groupId > 0)
            {
                ViewBag.IsGroupId = true;
                ViewBag.GroupId = clientsGroupItem.groupId;
                groupId = clientsGroupItem.groupId;
            }
            else
            {
                ViewBag.IsGroupId = false;
                ViewBag.GroupId = 0;
                groupId = 0;
            }






            #endregion //ClientGroup








            List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();
            cgList = clientsGroupDetView.GetClientsGroupDetViewList(groupId);
            ViewBag.ClientsGroup = cgList;

            ViewBag.EditClientId = Application.clientId;





            return View(sum);
        }


        public ActionResult CalculateScores(long ApplicationID, Int64 clientId)
        {
            try
            {


                
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

                if (scorDes < 1)
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

                var app = db.applications.Find(ApplicationID);

                ApplicationSummary appSummry = new Models.ApplicationSummary();

                if (scorDes == 1)
                {
                    app.appStatus = 5;
                    db.SaveChanges();
                }
                else
                {
                    if (app.appStatus == 1)
                    {
                        double appSum = 0;
                        var appItems = db.Items.Where(i => i.applicationId == ApplicationID).ToList();
                        foreach (var apps in appItems)
                        {
                            appSum += (apps.Sum - apps.ClientInvest);
                        }

                        int credDur = app.CreditTerm; 
                        int branchId = app.branchId; 
                        int productId = app.productId; 

                        var prlimits = db.ProductLimits.Where(pl => pl.ProductID == prID).ToList();
                        foreach (var prl in prlimits)
                        {
                            if (prl.AmountLimit >= appSum && prl.App1 == false && prl.App2 == false)
                            {
                                app.appStatus = 4;
                                
                                ApplicationAppruves aprv = new ApplicationAppruves();
                                aprv.appUserId = User.Identity.GetUserId();
                                aprv.appDate = DateTime.Now;
                                aprv.appId = ApplicationID;
                                aprv.appSum = appSum;
                                aprv.collateral = "";
                                aprv.credDur = credDur;
                                aprv.creditSum = appSum;
                                aprv.grPeriod = 0;
                                aprv.insurance = "";
                                aprv.note1 = "Հաստատված է սքորինգով";
                                aprv.note2 = "";
                                aprv.note3 = "";
                                aprv.preCondit = "";
                                aprv.aprStatus = 4;

                                db.ApplicationAppruves.Add(aprv);
                                db.SaveChanges();

                            }
                            else
                            {                               
                                app.appStatus = 2;
                                db.SaveChanges();
                            }
                        }
                    }//if(app.appStatus == 1)
                }//if (scorDes == 5)


              
                appSummry.HaytID = app.applicationId;
                appSummry.ScoreDate = DateTime.Now;
                appSummry.ScoreDecisionID = scorDes;
                appSummry.ScoreValue = scorDes;
                var extAppSummry = db.ApplicationSummary.Where(s => s.HaytID == app.applicationId).SingleOrDefault();
                if (extAppSummry != null)
                {
                    extAppSummry.ScoreDate = appSummry.ScoreDate;
                    extAppSummry.ScoreDecisionID = appSummry.ScoreDecisionID;
                    extAppSummry.ScoreValue = appSummry.ScoreValue;
                }
                else
                {
                    db.ApplicationSummary.Add(appSummry);
                }

                db.SaveChanges();


            }
            catch (Exception ex)
            {
                ;
            }

            TempData["Score"] = 1;//scorDes

            return RedirectToAction("ApplicationSummary", new { ApplicationID = ApplicationID });
        }

        
        // GET: Application
        public ActionResult Index(int page = 1)
        {

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "adminPanel");

            long ClientID = 0;
            string ClientIDStr = Request.QueryString["ClientID"];
            List<applications> app = new List<applications>();

            if (!string.IsNullOrWhiteSpace(ClientIDStr))
                Int64.TryParse(ClientIDStr, out ClientID);



            string SQLStat = "SELECT * FROM dbo.applications WHERE (";
            string currUserId = User.Identity.GetUserId();
            int userASProfilId = db.UserASProfiles.Where(a => a.UserId == currUserId).Select(s => s.UserASProfileId).SingleOrDefault();
            var userBranches = db.BranchUsers.Where(b => b.UserASProfileId == userASProfilId).ToList();
            for(int i = 0; i < userBranches.Count; i++)
            {
                if (i > 0)
                    SQLStat += " OR ";

                SQLStat += " branchId = " + userBranches[i].BrancheId.ToString();
            }

            SQLStat += ") ";


            int StatusID = 0;
            string StatusIDStr = Request.QueryString["StatusID"];

            if (!string.IsNullOrWhiteSpace(StatusIDStr))
                Int32.TryParse(StatusIDStr, out StatusID);


            if (ClientID > 0)
                ViewBag.ClientID = ClientID;

            if (StatusID > 0)
                ViewBag.StatusID = StatusID;

            if (ClientID > 0 && StatusID > 0)
            {
                app = db.applications.Where(p => p.clientId == ClientID).ToList();
            }
            else
            {
                if (ClientID > 0)
                    app = db.applications.Where(p => p.clientId == ClientID).ToList();
                else if (StatusID > 0)
                {
                    SQLStat += " AND appStatus = " + StatusID.ToString();
                    app = db.applications.SqlQuery(SQLStat).ToList(); 
                }
                else
                    app = db.applications.SqlQuery(SQLStat).ToList();

            }



            List<ApplicationViewModel> items = new List<ApplicationViewModel>();

            foreach (var appCl in app)
            {


                var clientName = db.clients.Distinct().Where(p => p.clientId == appCl.clientId).Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList().Select(p => p.clientName).FirstOrDefault();



                var productName = db.Products.Where(p => p.productId == appCl.productId).Select(p => p.productName).FirstOrDefault();

                var SellerName = db.Sellers.Where(p => p.Id == appCl.SellerId).Select(p => p.FirstName + " " + p.LastName + " " + p.Patronymic).FirstOrDefault();

                var Selller = db.Sellers.Where(p => p.Id == appCl.SellerId).FirstOrDefault();
                var SupplierID = db.SupplierBranches.Where(p => p.BrancheId == Selller.BrancheId).Select(p => p.SupplierId).FirstOrDefault();


                var branchName = db.SupplierBranches.Where(p => p.BrancheId == Selller.BrancheId).Select(p => p.BrancheName).FirstOrDefault();
                var SupplierName = db.Suppliers.Where(p => p.SupplierId == SupplierID).Select(p => p.SupplierName).FirstOrDefault();

                var userName = db.Users.Where(p => p.Id == appCl.userId).Select(p => p.UserName).FirstOrDefault();

                var appuserFullName =
               db.UserASProfiles.Where(p => p.UserASProfileId == appCl.appuserId).Select(p => p.FirstName + " " + p.LastName).FirstOrDefault();




                var appStatusName = db.appStatus.Where(p => p.appStatusId == appCl.appStatus).Select(p => p.appStatusArm).FirstOrDefault();


                ApplicationViewModel item = new ApplicationViewModel
                {
                    agrNumb = appCl.agrNumb,
                    agrNumbP = appCl.agrNumbP,
                    appDate = appCl.appDate,
                    appDescr = appCl.appDescr,
                    applicationId = appCl.applicationId,
                    appStatus = appCl.appStatus,
                    appuserId = appuserFullName,
                    aprDate = appCl.aprDate,
                    branchId = appCl.branchId,
                    clientId = appCl.clientId,


                    branchName = branchName,
                    clientName = clientName,
                    productName = productName,
                    SellerName = SellerName,
                    userName = userName,
                    SupplierName = SupplierName,
                    appStatusName = appStatusName,



                    CreditTerm = appCl.CreditTerm,
                    note1 = appCl.note1,
                    note2 = appCl.agrNumb,
                    note3 = appCl.agrNumb,
                    note4 = appCl.agrNumb,
                    note5 = appCl.agrNumb,
                    productId = appCl.productId,

                    SellerId = appCl.SellerId,

                    userId = appCl.agrNumb


                };




                items.Add(item);
            }

            items = items.OrderByDescending(p => p.applicationId).ToList();
            
            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = page;
            return View(items.ToPagedList(pageNumber, pageSize));
        }



        // GET: Application/Create
        public ActionResult Create()
        {
            long ClientID = 0;
            string ClientIDStr = Request.QueryString["ClientID"];
            Models.applications app = new applications();

            if (!string.IsNullOrWhiteSpace(ClientIDStr))
                Int64.TryParse(ClientIDStr, out ClientID);

            var clients = db.clients.Distinct().Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList();


            if (ClientID > 0)
            {
                app.clientId = ClientID;
                ViewBag.ClintFixed = true;


                ViewBag.Client = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => new { clientId = p.clientId, clientName = ((p.clientName ?? "") + " " + (p.clientLastName ?? "") + " " + (p.clientMidName ?? "")) }).ToList().Select(p => p.clientName).FirstOrDefault();

                ViewBag.AppInfo = app.appDate.ToString();
                ViewBag.AppNumber = app.applicationId;
            }


            string RoleId = db.Roles.Where(p => p.Name == "Manager").Select(p => p.Id).FirstOrDefault();


            List<Branches> branches = db.Branches.Distinct().ToList();
            List<appStatus> statusesList = db.appStatus.Distinct().ToList();
            List<Products> products = db.Products.Where(p => p.prodStatus == true).Distinct().ToList();


            var usersIDList = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(RoleId)).Select(p => p.Id).ToList();

            List<UserASProfiles> usersSign = db.UserASProfiles.Where(p => usersIDList.Contains(p.asUserId)).ToList();

            ViewBag.cl = new SelectList(clients, "clientId", "clientName");
            ViewBag.br = new SelectList(branches, "BranchesId", "Branch");
            ViewBag.st = new SelectList(statusesList, "appStatusId", "appStatusArm");
            ViewBag.pr = new SelectList(products, "productId", "productName");
            ViewBag.sp = new SelectList(usersSign, "UserASProfileId", "asUserName");

            ViewBag.prGroup = new SelectList(db.productGroups, "productGroupId", "prodGroupName");

            app.userId = User.Identity.GetUserId();


            var sel = db.Sellers.Include(p => p.SupplierBranchs).Include(p => p.Suppliers).Select(p => new { Id = p.Id, Name = p.FirstName + " " + p.LastName + " " + p.Patronymic + " - " + p.Suppliers.SupplierName + ", " + p.SupplierBranchs.BrancheName }).OrderBy(p => p.Name).ToList();

            ViewBag.Sellers = new SelectList(sel, "Id", "Name");


            ViewBag.brpdGroupId = 0;

            return View(app);
        }

        // POST: Application/Create
        [HttpPost]
        public ActionResult Create(Models.applications app, string prGroup)
        {


            ViewBag.brpdGroupId = 0;
            app.appDate = DateTime.Now;
            app.userId = User.Identity.GetUserId();
            var up = db.UserASProfiles.Where(p => p.UserId == app.userId).FirstOrDefault();
            int userBrancheId = db.BranchUsers.Where(b => b.UserASProfileId == up.UserASProfileId).Select(s => s.BrancheId).FirstOrDefault();
            app.branchId = userBrancheId;//up.BrancheId;

            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {
                int prodMinMatur = db.Products.Where(p => p.productId == app.productId).Select(s => s.minMaturtity).SingleOrDefault();
                int prodMaxMatur = db.Products.Where(p => p.productId == app.productId).Select(s => s.maxMaturity).SingleOrDefault();

                if (app.CreditTerm >= prodMinMatur)
                {
                    if (app.CreditTerm <= prodMaxMatur)
                    {
                        db.applications.Add(app);
                        db.SaveChanges();

                        return RedirectToAction("Create", "Items", new { ApplicationID = app.applicationId });
                    }
                    else
                        ViewBag.maturErr = "Նշված ժամկետը չի համապատասխանում տվյալ պրոդուկտի մաքսիմալ տևողությանը:";
                }
                else
                    ViewBag.maturErr = "Նշված ժամկետը չի համապատասխանում տվյալ պրոդուկտի մինիմալ տևողությանը:";
                


            }


            long ClientID = 0;
            string ClientIDStr = Request.QueryString["ClientID"];



            Int64.TryParse(ClientIDStr, out ClientID);



            var clients = db.clients.Distinct().Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList();
            if (ClientID > 0)
            {

                ViewBag.ClintFixed = true;

                ViewBag.Client = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => new { clientId = p.clientId, clientName = ((p.clientName ?? "") + " " + (p.clientLastName ?? "") + " " + (p.clientMidName ?? "")) }).ToList().Select(p => p.clientName).FirstOrDefault();
                ViewBag.AppInfo = app.appDate.ToString();
                ViewBag.AppNumber = app.applicationId;
            }


            string RoleId = db.Roles.Where(p => p.Name == "Manager").Select(p => p.Id).FirstOrDefault();


            List<Branches> branches = db.Branches.Distinct().ToList();
            List<appStatus> statusesList = db.appStatus.Distinct().ToList();
            List<Products> products = db.Products.Where(p=>p.prodStatus == true).Distinct().ToList();


            var usersIDList = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(RoleId)).Select(p => p.Id).ToList();

            List<UserASProfiles> usersSign = db.UserASProfiles.Where(p => usersIDList.Contains(p.asUserId)).ToList();

            ViewBag.cl = new SelectList(clients, "clientId", "clientName");
            ViewBag.br = new SelectList(branches, "BranchesId", "Branch");
            ViewBag.st = new SelectList(statusesList, "appStatusId", "appStatusArm");            
            ViewBag.sp = new SelectList(usersSign, "UserASProfileId", "asUserName");

            ViewBag.prGroup = new SelectList(db.productGroups, "productGroupId", "prodGroupName");

            if(prGroup != "")
            {
                int prGr = int.Parse(prGroup);
                ViewBag.brpdGroupId = prGr;
                ViewBag.pr = new SelectList(products.Where(p => p.productGroupId == prGr && p.prodStatus == true), "productId", "productName");
            }                
            else
                ViewBag.pr = new SelectList(products.Where(p => p.prodStatus == true), "productId", "productName");


            var sel = db.Sellers.Include(p => p.SupplierBranchs).Include(p => p.Suppliers).Select(p => new { Id = p.Id, Name = p.FirstName + " " + p.LastName + " " + p.Patronymic + " - " + p.Suppliers.SupplierName + ", " + p.SupplierBranchs.BrancheName }).OrderBy(p => p.Name).ToList();

            ViewBag.Sellers = new SelectList(sel, "Id", "Name");


            return View(app);

        }

        // GET: Application/Edit/5

        public ActionResult Edit(long id = 0)
        {            
            if (id == 0)
                return RedirectToAction("Create");

            ViewBag.isEdirable = CommonFunction.isApplicationEditable(id, User.Identity.GetUserId(), CommonFunction.GetRolesForEditing());

            Models.applications app = db.applications.Where(p => p.applicationId == id).FirstOrDefault();

            if (app.applicationId == 0)
                return RedirectToAction("Create");


            string RoleId = db.Roles.Where(p => p.Name == "Manager").Select(p => p.Id).FirstOrDefault();

            List<clients> clients = db.clients.Distinct().ToList();
            List<Branches> branches = db.Branches.Distinct().ToList();
            List<appStatus> statusesList = db.appStatus.Distinct().ToList();
            List<Products> products = db.Products.Where(p => p.prodStatus == true).Distinct().ToList();


            var usersIDList = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(RoleId)).Select(p => p.Id).ToList();

            List<UserASProfiles> usersSign = db.UserASProfiles.Where(p => usersIDList.Contains(p.asUserId)).ToList();

            ViewBag.cl = new SelectList(clients, "clientId", "clientName");
            ViewBag.br = new SelectList(branches, "BranchesId", "Branch");
            ViewBag.st = new SelectList(statusesList, "appStatusId", "appStatusArm");
            ViewBag.pr = new SelectList(products, "productId", "productName");
            ViewBag.sp = new SelectList(usersSign, "UserASProfileId", "asUserName");
            var sel = db.Sellers.Include(p => p.SupplierBranchs).Include(p => p.Suppliers).Select(p => new { Id = p.Id, Name = p.FirstName + " " + p.LastName + " " + p.Patronymic + " - " + p.Suppliers.SupplierName + ", " + p.SupplierBranchs.BrancheName }).OrderBy(p => p.Name).ToList();

            ViewBag.Sellers = new SelectList(sel, "Id", "Name");


            ViewBag.Client = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => new { clientId = p.clientId, clientName = ((p.clientName ?? "") + " " + (p.clientLastName ?? "") + " " + (p.clientMidName ?? "")) }).ToList().Select(p => p.clientName).FirstOrDefault();

            ViewBag.AppInfo = app.appDate.ToString();
            ViewBag.AppNumber = app.applicationId;


            BalanceViewModel bvm = new BalanceViewModel();
            Balance balance = db.Balance.Where(p => p.applicationId == id).OrderByDescending(p => p.Id).FirstOrDefault();


            if (balance != null)
            {
                bvm = new BalanceViewModel(balance);

            }



            IncomeExpensesViewModel ivm = new IncomeExpensesViewModel();

            IncomeExpenses Inc = db.IncomeExpenses.Where(p => p.applicationId == id).OrderByDescending(p => p.Id).FirstOrDefault();


            if (Inc != null)
            {


                ivm = new IncomeExpensesViewModel(Inc);



            }


            ViewBag.Items = (
          from i in db.Items
          join a in db.applications on i.applicationId equals a.applicationId
          join c in db.clients on i.clientId equals c.clientId
          join s in db.Suppliers on i.SupplierId equals s.SupplierId
          join pp in db.ProductPurposes on i.FKProductPurposeId equals pp.Id
          join pr in db.Products on pp.ProductId equals pr.productId
          join pur in db.Purposes on pp.PurposeId equals pur.Id

          where i.applicationId == app.applicationId

          select new ItemViewModel()
          {
              Id = i.Id,

              ItemName = i.ItemName,
              ItemDescr = i.ItemDescr,
              ClientInvest = i.ClientInvest,
              Count = i.Count,
              Price = i.Price,
              Sum = i.Sum,

              applicationName = a.agrNumb,
              ProductPurposeName = pr.productName + " - " + pur.PurposeName,
              clientName = c.clientName + " " + c.clientLastName + " " + c.clientMidName ?? "",
              SupplierName = s.SupplierName
          }


       ).Distinct().ToList();

            ViewBag.FileTable = db.DocsApllications.Where(p => p.ApplicationId == app.applicationId).ToList();

            ViewBag.socNumb = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => p.socNumb).FirstOrDefault();


            ViewBag.ApplicationID = app.applicationId;

            ViewBag.Balance = bvm;
            ViewBag.IncomeExpenses = ivm;

            string prGroupName = db.productGroups.Where(p => p.productGroupId == app.productId).Select(g => g.prodGroupName).SingleOrDefault();
            ViewBag.prGroupName = prGroupName;

            return View(app);
        }

        // POST: Application/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.applications app)
        {
            ViewBag.isEdirable = CommonFunction.isApplicationEditable(app.applicationId, User.Identity.GetUserId(), CommonFunction.GetRolesForEditing());
            if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save))
            {

                int prodMinMatur = db.Products.Where(p => p.productId == app.productId).Select(s => s.minMaturtity).SingleOrDefault();
                int prodMaxMatur = db.Products.Where(p => p.productId == app.productId).Select(s => s.maxMaturity).SingleOrDefault();

                if (app.CreditTerm >= prodMinMatur)
                {
                    if (app.CreditTerm <= prodMaxMatur)
                    {
                        db.Entry(app).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                        ViewBag.maturErr = "Նշված ժամկետը չի համապատասխանում տվյալ պրոդուկտի մաքսիմալ տևողությանը:";
                }
                else
                    ViewBag.maturErr = "Նշված ժամկետը չի համապատասխանում տվյալ պրոդուկտի մինիմալ տևողությանը:";                


            }

            ViewBag.FileTable = db.DocsApllications.Where(p => p.ApplicationId == app.applicationId).ToList();



            string RoleId = db.Roles.Where(p => p.Name == "Manager").Select(p => p.Id).FirstOrDefault();

            List<clients> clients = db.clients.Distinct().ToList();
            List<Branches> branches = db.Branches.Distinct().ToList();
            List<appStatus> statusesList = db.appStatus.Distinct().ToList();
            List<Products> products = db.Products.Distinct().ToList();


            var usersIDList = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(RoleId)).Select(p => p.Id).ToList();

            List<UserASProfiles> usersSign = db.UserASProfiles.Where(p => usersIDList.Contains(p.asUserId)).ToList();

            ViewBag.cl = new SelectList(clients, "clientId", "clientName");
            ViewBag.br = new SelectList(branches, "BranchesId", "Branch");
            ViewBag.st = new SelectList(statusesList, "appStatusId", "appStatusArm");
            ViewBag.pr = new SelectList(products, "productId", "productName");
            ViewBag.sp = new SelectList(usersSign, "UserASProfileId", "asUserName");
            var sel = db.Sellers.Include(p => p.SupplierBranchs).Include(p => p.Suppliers).Select(p => new { Id = p.Id, Name = p.FirstName + " " + p.LastName + " " + p.Patronymic + " - " + p.Suppliers.SupplierName + ", " + p.SupplierBranchs.BrancheName }).OrderBy(p => p.Name).ToList();

            ViewBag.Sellers = new SelectList(sel, "Id", "Name");



            ViewBag.Client = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => new { clientId = p.clientId, clientName = ((p.clientName ?? "") + " " + (p.clientLastName ?? "") + " " + (p.clientMidName ?? "")) }).ToList().Select(p => p.clientName).FirstOrDefault();


            ViewBag.AppInfo = app.appDate.ToString();
            ViewBag.AppNumber = app.applicationId;

            BalanceViewModel bvm = new BalanceViewModel();


            Balance balance = db.Balance.Where(p => p.applicationId == app.applicationId).OrderByDescending(p => p.Id).FirstOrDefault();


            if (balance != null)
            {
                bvm = new BalanceViewModel(balance);

            }



            IncomeExpensesViewModel ivm = new IncomeExpensesViewModel();

            IncomeExpenses Inc = db.IncomeExpenses.Where(p => p.applicationId == app.applicationId).OrderByDescending(p => p.Id).FirstOrDefault();


            if (Inc != null)
            {


                ivm = new IncomeExpensesViewModel(Inc);



            }



            ViewBag.Items = (
       from i in db.Items
       join a in db.applications on i.applicationId equals a.applicationId
       join c in db.clients on i.clientId equals c.clientId
       join s in db.Suppliers on i.SupplierId equals s.SupplierId
       join pp in db.ProductPurposes on i.FKProductPurposeId equals pp.Id
       join pr in db.Products on pp.ProductId equals pr.productId
       join pur in db.Purposes on pp.PurposeId equals pur.Id

       where i.applicationId == app.applicationId

       select new ItemViewModel()
       {
           Id = i.Id,

           ItemName = i.ItemName,
           ItemDescr = i.ItemDescr,
           ClientInvest = i.ClientInvest,
           Count = i.Count,
           Price = i.Price,
           Sum = i.Sum,

           applicationName = a.agrNumb,
           ProductPurposeName = pr.productName + " - " + pur.PurposeName,
           clientName = c.clientName + " " + c.clientLastName + " " + c.clientMidName ?? "",
           SupplierName = s.SupplierName
       }


    ).Distinct().ToList();


            ViewBag.ApplicationID = app.applicationId;

            ViewBag.socNumb = db.clients.Distinct().Where(p => p.clientId == app.clientId).Select(p => p.socNumb).FirstOrDefault();

            ViewBag.Balance = bvm;
            ViewBag.IncomeExpenses = ivm;

            string prGroupName = db.productGroups.Where(p => p.productGroupId == app.productId).Select(g => g.prodGroupName).SingleOrDefault();
            ViewBag.prGroupName = prGroupName;

            return View(app);

        }

        //---------------------------------------------------------------

        public async Task<ActionResult> SendTelegramMessage(long? appId, string userId)
        {
            string appURL = Url.RequestContext.HttpContext.Request.Url.Scheme.ToString() + "://" + Url.RequestContext.HttpContext.Request.Url.Authority.ToString() + "/asfront/Application/ApplicationSummary?ApplicationID=" + appId.ToString();
            string txtMsg = "Տվյալ օգտատիրոջ համար նշված չէ Telegram հասցե:";

            int appStatus = db.applications.Where(a => a.applicationId == appId).Select(s => s.appStatus).SingleOrDefault();

            var ruserTelegramId = db.UserASProfiles.Where(r => r.UserId == userId).Select(ru => ru.telegramId).SingleOrDefault();

            if (appStatus > 3)
            {
                txtMsg = "Հայտն արդեն իսկ հաստատված է, և չի կարող ուղղարկվել հաստատման:";
            }

            
            if (ruserTelegramId != null && appStatus < 4)
            {
                string userFullName = "";
                string suserFullName = "";
                string suserId = User.Identity.GetUserId();
                var ruser = db.UserASProfiles.Where(r => r.UserId == userId);
                foreach (var rusr in ruser)
                {
                    userFullName = rusr.FirstName + " " + rusr.LastName;
                }

                var suser = db.UserASProfiles.Where(r => r.UserId == suserId);
                foreach (var susr in suser)
                {
                    suserFullName = susr.FirstName + " " + susr.LastName;
                }

                txtMsg = "Հարգելի " + userFullName + ", " + suserFullName + " աշխատակցի կողմից, ձեր հաստատմանն է ուղարկվել թիվ#" + appId.ToString() + " հայտը: ";


                var Bot = new Telegram.Bot.TelegramBotClient("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

                await Bot.SetWebhookAsync();

                var t = await Bot.SendTextMessageAsync(ruserTelegramId, txtMsg);
                t = await Bot.SendTextMessageAsync(ruserTelegramId, appURL);

                ApplicationsForApprove apMs = new Models.ApplicationsForApprove();

                apMs.appId = appId.Value;
                apMs.sendDate = DateTime.Now;
                apMs.sendUserId = suserId;
                apMs.appUserId = userId;
                apMs.apprStatus = "Ուղարկված";
                db.ApplicationsForApprove.Add(apMs);
                db.SaveChanges();

                txtMsg = "Հաստատման մասին հաղորդագրությունն ուղարկված է " + userFullName + " աշխատակցի Telegram հասցեին:";
            }//if (ruserTelegramId != null)

            ViewBag.appMsg = txtMsg;

            return View();
        }


        public ActionResult GetApplicationsForAppruve(int page = 1)
        {
            string userId = User.Identity.GetUserId();

            UserASProfiles userASProf = db.UserASProfiles.Where(u => u.UserId == userId).SingleOrDefault();

            var appForAprv = db.ApplicationsForApprove.Where(a => a.appUserId == userId && a.apprStatus == "Ուղարկված").Select(s=>s.appId).ToList();
            

            var appList = db.applications.Where(a => a.appStatus == 1 && appForAprv.Contains(a.applicationId)).ToList();


            List<ApplicationViewModel> items = new List<ApplicationViewModel>();

            foreach (var appCl in appList)
            {






                var clientName = db.clients.Distinct().Where(p => p.clientId == appCl.clientId).Select(p => new { clientId = p.clientId, clientName = (p.clientName + " " + p.clientLastName + " " + p.clientMidName ?? "") }).ToList().Select(p => p.clientName).FirstOrDefault();



                var productName = db.Products.Where(p => p.productId == appCl.productId).Select(p => p.productName).FirstOrDefault();

                var SellerName = db.Sellers.Where(p => p.Id == appCl.SellerId).Select(p => p.FirstName + " " + p.LastName + " " + p.Patronymic).FirstOrDefault();

                var Selller = db.Sellers.Where(p => p.Id == appCl.SellerId).FirstOrDefault();
                var SupplierID = db.SupplierBranches.Where(p => p.BrancheId == Selller.BrancheId).Select(p => p.SupplierId).FirstOrDefault();


                var branchName = db.SupplierBranches.Where(p => p.BrancheId == Selller.BrancheId).Select(p => p.BrancheName).FirstOrDefault();
                var SupplierName = db.Suppliers.Where(p => p.SupplierId == SupplierID).Select(p => p.SupplierName).FirstOrDefault();

                var userName = db.Users.Where(p => p.Id == appCl.userId).Select(p => p.UserName).FirstOrDefault();

                var appuserFullName =
               db.UserASProfiles.Where(p => p.UserASProfileId == appCl.appuserId).Select(p => p.FirstName + " " + p.LastName).FirstOrDefault();




                var appStatusName = db.appStatus.Where(p => p.appStatusId == appCl.appStatus).Select(p => p.appStatusArm).FirstOrDefault();


                ApplicationViewModel item = new ApplicationViewModel
                {
                    agrNumb = appCl.agrNumb,
                    agrNumbP = appCl.agrNumbP,
                    appDate = appCl.appDate,
                    appDescr = appCl.appDescr,
                    applicationId = appCl.applicationId,
                    appStatus = appCl.appStatus,
                    appuserId = appuserFullName,
                    aprDate = appCl.aprDate,
                    branchId = appCl.branchId,
                    clientId = appCl.clientId,


                    branchName = branchName,
                    clientName = clientName,
                    productName = productName,
                    SellerName = SellerName,
                    userName = userName,
                    SupplierName = SupplierName,
                    appStatusName = appStatusName,



                    CreditTerm = appCl.CreditTerm,
                    note1 = appCl.note1,
                    note2 = appCl.agrNumb,
                    note3 = appCl.agrNumb,
                    note4 = appCl.agrNumb,
                    note5 = appCl.agrNumb,
                    productId = appCl.productId,

                    SellerId = appCl.SellerId,

                    userId = appCl.agrNumb



                };




                items.Add(item);
            }

            items = items.OrderByDescending(p => p.applicationId).ToList();

            int pageSize = ApplicationSettings.PageSize;
            int pageNumber = page;
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        

        //---------------------------------------------------------------


    }
}
