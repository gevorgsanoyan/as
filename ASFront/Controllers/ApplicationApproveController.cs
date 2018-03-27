using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ASFront.Models;
using ASFront.ModelsView;
using Microsoft.AspNet.Identity;

using System.Threading.Tasks;
using Telegram.Bot;

using ASFront.Classes;

namespace ASFront.Controllers
{
    public class ApplicationApproveController : Controller
    {
        // GET: ApplicationApprove

        ApplicationDbContext db;

        public ApplicationApproveController()
        {
            db = new Models.ApplicationDbContext();
        }

        public ActionResult Index(long applicationId, int appGrade)
        {

            string currUserId = User.Identity.GetUserId();
            string appError = "";
            string appInfo = "";
            int agrd = 0;
            ViewBag.appStat = "";


            var app = db.applications.Where(a => a.applicationId == applicationId).ToList();
            int appStatus = app.Select(s => s.appStatus).SingleOrDefault();

                                    
            var appItems = db.Items.Where(i => i.applicationId == applicationId).ToList();
            UserAppView uapp = new ModelsView.UserAppView();
            uapp.appId = applicationId;
            foreach(var item in appItems)
            {                
                uapp.appSum += item.Sum;
                uapp.creditSum += item.ClientInvest;
                uapp.cuserId = item.userId;
            }


            var ruser = db.UserASProfiles.Where(r => r.UserId == uapp.cuserId);
            foreach (var rusr in ruser)
            {
                uapp.cuserId = rusr.FirstName + " " + rusr.LastName;
            }
            try
            {
                uapp.creditSum = app.Select(s => s.creditSum).SingleOrDefault().Value; //uapp.appSum - uapp.creditSum;
            }
            catch
            {
                uapp.creditSum = uapp.appSum - uapp.creditSum;
            }
            

            
            int credDur = app.Select(d => d.CreditTerm).SingleOrDefault();
            int branchId = app.Select(b => b.branchId).SingleOrDefault();
            int productId = app.Select(pr => pr.productId).SingleOrDefault();
            int prodLimitId = 0; 

            int currId = db.Products.Where(p => p.productId == productId).Select(c => c.productCurrency).SingleOrDefault();
            string appCurr = db.CurrencyTypes.Where(c => c.currencyTypesId == currId).Select(c => c.currencyArm).SingleOrDefault();
            uapp.appCurrency = appCurr;
            uapp.credDur = credDur;                     
            var prodlimit = (from pl in db.ProductLimits
                            where pl.ProductID == productId && pl.AmountLimit >= uapp.creditSum
                            select new { pl.Id, pl.ProductID, pl.AmountLimit, pl.Scoring, pl.App1, pl.App2 }).ToList();

            foreach(var prdl in prodlimit)
            {
                if(uapp.creditSum <= prdl.AmountLimit)                
                {
                    prodLimitId = prdl.Id;

                    if (appGrade == 0)
                    {
                        var agrdl = db.UserAppTable.Where(ug => ug.userId == currUserId && ug.isActive && ug.prodLimitId == prodLimitId && ug.maxLimit >= uapp.creditSum).ToList();
                        foreach(var ag in agrdl)
                        {
                            appGrade = ag.appTypeId ?? default(int);
                        }
                    }

                    switch (appStatus)
                    {
                        case 0:
                            appError = "Տվյալ հայտի համար չի հաշվարկվել սքորինգ:";
                            break;
                        case 1:
                            break;
                        case 2:
                            //appError = "Տվյալ հայտն արդեն հաստատված է սքորինգով:";
                            break;
                        case 3:
                            if (appGrade == 1)
                                appError = "Տվյալ հայտն արդեն հաստատված է Օղակ 1-ի կողմից:";
                            break;
                        case 4:
                            appError = "Տվյալ հայտն արդեն հաստատված է:";
                            break;
                        case 5:
                            appError = "Տվյալ հայտը մերժված է:";
                            break;
                    }//switch (appStatus)

                    switch (appGrade)
                    {
                        case 1:
                            if(prdl.App1 ==  true)
                            {
                                if (prdl.App2 == false)
                                    uapp.aprStatus = 4;
                                else
                                    uapp.aprStatus = 3;
                            }
                            else
                            {
                                appError = "Տվյալ պրոդուկտի համար նախատեսված չէ նման հաստատման իրավասություններով իրականացնել հաստատման գործառույթ:";
                            }
                            break;
                        case 2:
                            if (prdl.App2 == true)
                            {
                                if (appStatus == 3)
                                    uapp.aprStatus = 4;
                                else
                                    appError = "Տվյալ հայտը դեռ հաստատված չէ Օղակ 1-ի կողմից: Խնդրում ենք փորձել ավելի ուշ:";
                            }
                            else
                                appError = "Տվյալ պրոդուկտի համար նախատեսված չէ նման հաստատման իրավասություններով իրականացնել հաստատման գործառույթ:";
                            break;
                    }//switch(appGrade )
                    break;
                }//if(uapp.creditSum >= prdl.AmountLimit)
            }//foreach(var pl in prodlimit)

            var usrApp = db.UserAppTable.Where(u => u.userId == currUserId && u.isActive && u.prodLimitId == prodLimitId && u.maxLimit >= uapp.creditSum).ToList();

            if (appError.Length > 0)
            {
                return RedirectToAction("appErrorDisplay", new { errMsg = appError });
            }


            foreach (var usapp in usrApp)
            {
                if (usapp.BrancheId != branchId)
                {
                    appError = "Դուք չունեք հաստատման իրավասություններ տվյալ մասնաճյուղի հայտերի համար:";
                    //break;
                }
                else
                { appError = ""; }
                if(usapp.prodLimitId != prodLimitId)
                {
                    appError = "Դուք չունեք հաստատման իրավասություններ տվյալ պրոդուկտի հայտերի համար, կամ հայտի գումարը գերազանցում է սահմանաչափը:";
                    //break;
                }
                else
                { appError = ""; }
                if (usapp.appTypeId == appGrade)
                    agrd++;        
            }

            AppErrorView errDesc = new ModelsView.AppErrorView();
            if(appError.Length > 0)
            {
                errDesc.appErrorDescr = appError;
            }
            else
            {
                if (agrd == 0)
                    errDesc.appErrorDescr = "Դուք չունեք տվյալ աստիճանի հաստատման իրավասություններ:";
                else
                    errDesc.appErrorDescr = "";
            }

            if (errDesc.appErrorDescr.Length > 0)
                return RedirectToAction("appErrorDisplay", new { errMsg = errDesc.appErrorDescr });

            return View(uapp);
        }


        public ActionResult appErrorDisplay(string errMsg)
        {
            AppErrorView errDescr = new ModelsView.AppErrorView();
            errDescr.appErrorDescr = errMsg;
            return View(errDescr);
        }

        [HttpPost]
        public async Task<ActionResult> Index(UserAppView userApp, string submit)
        {
            string appStatus = "";
            string appMsg = ": Ձեր կողմից հաստատման ուղարկված թիվ#" + userApp.appId.ToString() + " հայտը ";

            if (userApp.creditSum > userApp.appSum)
                userApp.creditSum = userApp.appSum;

            switch (submit)
            {
                case "Մերժել":
                    Decline(userApp);
                    appStatus = "Մերժված է:";                                        
                    break;
                case "Հաստատել":
                    Approve(userApp);
                    appStatus = "Հաստատված է:";
                    break;
            }            

            appMsg += appStatus + " Հաստատման նշումներ: ";
            if(userApp.preCondit != null)
            {
                appMsg += userApp.preCondit;
            }
            if (userApp.insurance!= null)
            {
                appMsg += ", " + userApp.insurance;
            }
            if (userApp.collateral!=null)
            {
                appMsg += ", " + userApp.collateral;
            }
            if (userApp.note1!=null)
            {
                appMsg += ", " + userApp.note1;
            }

            var apMs = db.ApplicationsForApprove.Where(a => a.appId == userApp.appId).OrderByDescending(o=>o.ApplicationsForApproveId).Take(1).SingleOrDefault();
            apMs.apprDate = DateTime.Now;
            apMs.apprStatus = appStatus;
            apMs.note1 = "Stb";            
            db.SaveChanges();

            await SendAppruveMessageToTelegram(userApp.appId, appMsg);

            ViewBag.appStat = appStatus;
            return RedirectToAction("ApplicationSummary", "Application", new { ApplicationID = userApp.appId}); //View(userApp);
        }

        public async Task<int> SendAppruveMessageToTelegram(long appId, string appMsg)
        {
            string appUserId = db.applications.Where(a => a.applicationId == appId).Select(u => u.userId).SingleOrDefault();
            string userFullName = "";
            var ruserTelegramId = db.UserASProfiles.Where(r => r.UserId == appUserId).Select(ru => ru.telegramId).SingleOrDefault();
            if (ruserTelegramId != null)
            {
                var ruser = db.UserASProfiles.Where(r => r.UserId == appUserId);
                foreach (var rusr in ruser)
                {
                    userFullName = "Հարգելի " + rusr.FirstName + " " + rusr.LastName;
                }

                var Bot = new Telegram.Bot.TelegramBotClient("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

                await Bot.SetWebhookAsync();

                appMsg = userFullName + " " + appMsg;

                var t = await Bot.SendTextMessageAsync(ruserTelegramId, appMsg);
                return t.MessageId;
            }
            return 0;
        }

        public void Approve(UserAppView userApp)
        {
            ApplicationAppruves aprv = new ApplicationAppruves();
            aprv.appUserId = User.Identity.GetUserId();
            aprv.appDate = DateTime.Now;
            aprv.appId = userApp.appId;
            aprv.appSum = userApp.appSum;
            aprv.collateral = userApp.collateral;
            aprv.credDur = userApp.credDur;
            aprv.creditSum = userApp.creditSum;
            aprv.grPeriod = userApp.grPeriod;
            aprv.insurance = userApp.insurance;
            aprv.note1 = userApp.note1;
            aprv.note2 = userApp.note2;
            aprv.note3 = userApp.note3;
            aprv.preCondit = userApp.preCondit;
            aprv.aprStatus = userApp.aprStatus;

            
            var app = db.applications.Find(userApp.appId);
            int currId = db.Products.Where(p => p.productId == app.productId).Select(s => s.productCurrency).SingleOrDefault();
            app.appStatus = aprv.aprStatus;
            app.creditSum = aprv.creditSum;
            app.creditSumAMD = aprv.creditSum * CommonFunction.GetExchRate(currId, app.aprDate);

            long appsummId = db.ApplicationSummary.Where(asm => asm.HaytID == userApp.appId).Select(d=>d.Id).SingleOrDefault();
            var appsumm = db.ApplicationSummary.Find(appsummId);
            switch(aprv.aprStatus)
            {
                case 3:
                    appsumm.App1 = aprv.aprStatus.ToString();
                    appsumm.App1Date = DateTime.Now;
                    appsumm.App1user = User.Identity.GetUserId();
                    break;
                case 4:
                    appsumm.Appfinal = aprv.aprStatus.ToString();
                    appsumm.AppfinalDate = DateTime.Now;
                    appsumm.Appfinaluser = User.Identity.GetUserId();
                    break;
                case 5:
                    appsumm.Appfinal = aprv.aprStatus.ToString();
                    appsumm.AppfinalDate = DateTime.Now;
                    appsumm.Appfinaluser = User.Identity.GetUserId();
                    break;
            }

            if(aprv.appSum > aprv.creditSum)
            {
                var itms = db.Items.Where(a => a.applicationId == app.applicationId).Take(1).SingleOrDefault();
                itms.ClientInvest = float.Parse((aprv.appSum - aprv.creditSum).ToString());
            }

            db.ApplicationAppruves.Add(aprv);
            db.SaveChanges();
            
        }//public ActionResult Approve(UserAppView userApp)

        public void Decline(UserAppView userApp)
        {
            ApplicationAppruves aprv = new ApplicationAppruves();
            aprv.appUserId = User.Identity.GetUserId();
            aprv.appDate = DateTime.Now;
            aprv.appId = userApp.appId;
            aprv.appSum = userApp.appSum;
            aprv.collateral = userApp.collateral;
            aprv.credDur = userApp.credDur;
            aprv.creditSum = userApp.creditSum;
            aprv.grPeriod = userApp.grPeriod;
            aprv.insurance = userApp.insurance;
            aprv.note1 = userApp.note1;
            aprv.note2 = userApp.note2;
            aprv.note3 = userApp.note3;
            aprv.preCondit = userApp.preCondit;
            aprv.aprStatus = userApp.aprStatus;


            var app = db.applications.Find(userApp.appId);
            app.appStatus = 5;

            long appsummId = db.ApplicationSummary.Where(asm => asm.HaytID == userApp.appId).Select(d => d.Id).SingleOrDefault();
            var appsumm = db.ApplicationSummary.Find(appsummId);
            switch (aprv.aprStatus)
            {
                case 3:
                    appsumm.App1 = aprv.aprStatus.ToString();
                    appsumm.App1Date = DateTime.Now;
                    appsumm.App1user = User.Identity.GetUserId();
                    break;
                case 4:
                    appsumm.Appfinal = aprv.aprStatus.ToString();
                    appsumm.AppfinalDate = DateTime.Now;
                    appsumm.Appfinaluser = User.Identity.GetUserId();
                    break;
                case 5:
                    appsumm.Appfinal = aprv.aprStatus.ToString();
                    appsumm.AppfinalDate = DateTime.Now;
                    appsumm.Appfinaluser = User.Identity.GetUserId();
                    break;
            }


            db.ApplicationAppruves.Add(aprv);
            db.SaveChanges();
        }//public ActionResult Decline(UserAppView userApp)

    }//class    

}
