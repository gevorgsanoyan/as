using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASFront.Models;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data.Entity;
using ASFront.Classes;
using ASFront.ModelsView;
using Westwind.Web.Mvc;

using System.Data.Entity.Validation;

namespace ASFront.Controllers
{
    [Authorize]
    public class acraController : Controller
    {
        ApplicationDbContext db;
        acramonEntities acraDB;

        public acraController()
        {
            db = new Models.ApplicationDbContext();
            acraDB = new ASFront.acramonEntities();
        }




        [HttpPost]
        public async Task<ActionResult> GetClientsGroupsAcra(List<long> GMList, long EditClientId = 0)
        {

            Response.StatusCode = 200; // OK

            long groupId = 0;

            if ( EditClientId > 0 )
            {

                groupId = db.clientsGroup.Where(c => c.clientId == EditClientId).OrderByDescending(p => p.clientsGroupId).Select(g => g.groupId).FirstOrDefault();
                if (groupId > 0 && GMList != null)
                {
                    var clGroup = db.clientsGroup.Where(gr => gr.groupId == groupId && GMList.Contains(gr.clientId)).ToList();

                    foreach (var grm in clGroup)
                    {
                        try
                        {
                            long membId = grm.clientId;
                            int rt = await GetGroupMemberACRA(membId);
                        }
                        catch { }
                    }

                }//if(groupId > 0)
            }//if(EditClientId != "0")



            List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();


            cgList = clientsGroupDetView.GetClientsGroupDetViewList(groupId);

            ViewBag.EditClientId = EditClientId;
            return PartialView("~/views/clientsGroups/_ClientsGroupTable.cshtml", cgList);



            //if (Request.IsAjaxRequest())
            //    return PartialView("~/views/clientsGroups/_ClientsGroupTable.cshtml", cgList);
            //return View(cgList);

            //return Json(new { Url = Url.Action("GetClientsGroupsAcra", cgList) });

        }







        // GET: acras
        public ActionResult Index()
        {
            return View();
        }



        public async Task<string> acraRequest(string acraUri)
        {
            string acraResult = "";
            try
            {
                using (var httpclient = new HttpClient())
                {
                    var uri = new Uri(acraUri);

                    var response = await httpclient.GetAsync(uri);

                    acraResult = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex) { }

            //catch  { }

            return acraResult;
        }

        public async Task<int> GetGroupMemberACRA(long clientId)
        {
            double sefLoans = 0;
            int sefLoansCount = 0;
            double LoansTotSum = 0;
            int ovrdDaysCount = 0;

            int rtVal = 0;

            acraRequestInit acreq = new Models.acraRequestInit();
            acras am = new Models.acras();

            var client = db.clients.Where(c => c.clientId == clientId).ToList();
            am.clientId = clientId;

            if (client.Count > 0)
            {
                acreq.cFirstName = client[0].clientName.TrimEnd();
                acreq.cLastName = client[0].clientLastName.TrimEnd();
                acreq.cNumber = client[0].clientId.ToString();
                acreq.socNumb = client[0].socNumb.TrimEnd();
                string acraserUri = "http://192.168.0.117:9988/acraserv/api/acraResponse/" + acreq.cFirstName + "/" + acreq.cLastName + "/" + acreq.socNumb + "/" + clientId.ToString() + "/" + "/2/2/"; //"http://192.168.0.117:8888/acraserv/api/acraResponse/" + acreq.cFirstName + "/" + acreq.cLastName + "/" + acreq.socNumb;//"http://192.168.0.117:8989/acraserv/api/acraResponse/" + acreq.cFirstName + "/" + acreq.cLastName + "/" + acreq.socNumb + "/" + acreq.cNumber;
                string acraResult = await acraRequest(acraserUri); //"_OK";                

                if (acraResult.Length > 2)
                {
                    if (acraResult.Substring(1, 2) == "OK")
                    {
                        double maxLoan = 0;
                        int loansCount = 0;
                        var acraR = acraDB.ACRA_RequestsI.Where(a => a.soc_card == acreq.socNumb).OrderByDescending(asrt => asrt.r_ans_date).First();

                        am.cAddress = acraR.address;
                        am.clientId = client[0].clientId;
                        am.delayMax = acraR.delay_max.GetValueOrDefault();
                        am.delay_tot = acraR.delay_tot.GetValueOrDefault();
                        am.dob = acraR.DOB.GetValueOrDefault();
                        am.employm = acraR.employer;
                        am.firstName = acraR.first_name.TrimEnd();
                        if (acraR.income_update_date.GetValueOrDefault().Year > 200)
                            am.incUpdateDate = acraR.income_update_date.GetValueOrDefault();
                        else
                            am.incUpdateDate = new DateTime(1900, 1, 1);
                        am.lastName = acraR.last_name.TrimEnd();
                        am.passp = acraR.passport;
                        am.req30Count = acraR.req30_count.GetValueOrDefault();
                        am.reqCount = acraR.req_count.GetValueOrDefault();
                        am.resultId = acraR.result_ID;
                        am.socNumb = acraR.soc_card;
                        am.userId = User.Identity.GetUserId();
                        am.reqDate = DateTime.Now;
                        am.ReportNumber = acraR.ReportNumber;
                        am.IdCardNumber = acraR.IdCardNumber;
                        am.PersonBankruptIncome = acraR.PersonBankruptIncome;
                        am.SwitchisClassQuantity = acraR.SwitchisClassQuantity;
                        am.InformationReviewDate = acraR.InformationReviewDate;
                        am.TheWorstClassLoan = acraR.TheWorstClassLoan;
                        am.TheWorsClassGuarantee = acraR.TheWorsClassGuarantee;
                        am.SelfEnquiryQuantity30 = acraR.SelfEnquiryQuantity30;
                        am.SelfEnquiryQuantity = acraR.SelfEnquiryQuantity;
                        am.DelayPaymentQuantity = acraR.DelayPaymentQuantity;
                        //------Total Liabil------
                        var totLiab = acraDB.ACRA_R_TotalLiabilities.Where(t => t.fk_result_ID == am.resultId).ToList();
                        if (totLiab.Count > 0)
                        {
                            foreach (var tots in totLiab)
                            {

                                switch (tots.l_type)
                                {
                                    case 1:
                                        if (tots.currID == 1)
                                            am.totLiabAMD += tots.Amount.GetValueOrDefault();
                                        else
                                            am.totLiabUSD += tots.Amount.GetValueOrDefault();
                                        break;
                                    case 2:
                                        if (tots.currID == 1)
                                            am.totGuarAMD += tots.Amount.GetValueOrDefault();
                                        else
                                            am.totGuarUSD += tots.Amount.GetValueOrDefault();
                                        break;
                                }//switch(tots.l_type)

                            }//foreach(var tots in totLiab)
                        }//if(totLiab.Count > 0)
                        //------Total Liabil------
                        //-------------Total Uverdues--------
                        var ovrd = acraDB.ACRA_R_CurrentOverdue.Where(o => o.fk_result_ID == am.resultId).ToList();
                        foreach (var ovr in ovrd)
                        {
                            switch (ovr.l_type)
                            {
                                case 1:
                                    if (ovr.currID == 1)
                                        am.totOverdueAMD += ovr.Amount.GetValueOrDefault();
                                    else
                                        am.totOverdueUSD += ovr.Amount.GetValueOrDefault();
                                    break;
                                case 2:
                                    if (ovr.currID == 1)
                                        am.totOverdueGAMD += ovr.Amount.GetValueOrDefault();
                                    else
                                        am.totOverdueGUSD += ovr.Amount.GetValueOrDefault();
                                    break;
                            }//switch(ovr.l_type)
                        }
                        //-------------Total Uverdues--------

                        db.acras.Add(am);
                        db.SaveChanges();

                        //-------------Request info---------------
                        var reqInfo = acraDB.ACRA_RequestesInfo.Where(r => r.resultId == am.resultId);
                        acraRequestesInfo rInfo = new acraRequestesInfo();

                        foreach (var rq in reqInfo)
                        {
                            rInfo.resultId = am.resultId;
                            rInfo.acraId = am.acraId;
                            rInfo.vBankName = rq.vBankName;
                            rInfo.vDate = rq.vDate;
                            rInfo.vNumber = rq.vNumber;
                            rInfo.vReason = rq.vReason;
                            rInfo.vSubReason = rq.vSubReason;
                            try
                            {
                                db.acraRequestesInfo.Add(rInfo);
                                db.SaveChanges();
                            }
                            catch (DbEntityValidationException saveEx)
                            {
                                foreach (var eve in saveEx.EntityValidationErrors)
                                {
                                    acraResult += " Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State.ToString() + " has the following validation errors:";
                                    foreach (var ve in eve.ValidationErrors)
                                    {
                                        acraResult += $"- Property:" + ve.PropertyName + " , Error:" + ve.ErrorMessage;
                                    }
                                }
                            }
                            catch (Exception clnex)
                            {
                                acraResult = "ACRA requestes data input error_" + clnex.Message + " # " + acraResult;
                            }
                        }//foreach(var rq in reqInfo)

                        //----------------------------

                        //---------------Interrel-------------

                        var inrel = acraDB.ACRA_Interrelated.Where(r => r.resultId == am.resultId);
                        acraInterrelated interRel = new acraInterrelated();

                        foreach (var ir in inrel)
                        {
                            interRel.acraId = am.acraId;
                            interRel.iAmountDue = ir.iAmountDue;
                            interRel.iAmountOverdue = ir.iAmountOverdue;
                            interRel.iContractAmount = ir.iContractAmount;
                            interRel.iCreditClassification = ir.iCreditClassification;
                            interRel.iCreditStart = ir.iCreditStart;
                            interRel.iCurrency = ir.iCurrency;
                            interRel.iLastInstallment = ir.iLastInstallment;
                            interRel.iNumber = ir.iNumber;
                            interRel.iOutstandingDate = ir.iOutstandingDate;
                            interRel.iOutstandingPercent = ir.iOutstandingPercent;
                            interRel.resultId = am.resultId;
                            interRel.vDebtorNum = ir.vDebtorNum;
                            interRel.vInterrelatedSourceName = ir.vInterrelatedSourceName;
                            try
                            {
                                db.acraInterrelated.Add(interRel);
                                db.SaveChanges();
                            }
                            catch (DbEntityValidationException saveEx)
                            {
                                foreach (var eve in saveEx.EntityValidationErrors)
                                {
                                    acraResult += " Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State.ToString() + " has the following validation errors:";
                                    foreach (var ve in eve.ValidationErrors)
                                    {
                                        acraResult += $"- Property:" + ve.PropertyName + " , Error:" + ve.ErrorMessage;
                                    }
                                }
                            }
                            catch (Exception clnex)
                            {
                                acraResult = "ACRA Interreleated loans data input error_" + clnex.Message + " # " + acraResult;
                            }

                        }//foreach(var ir in inrel)

                        //----------------------------


                        var loans = acraDB.ACRA_LoansGetDetEx(am.resultId).ToList();

                        if (loans.Count > 0)
                        {
                            var dloans = db.acraLoans.Where(d => d.clientId == clientId).ToList();
                            foreach(var dl in dloans)
                            {
                                db.acraLoanosDays.RemoveRange(db.acraLoanosDays.Where(od=>od.loanId == dl.acraLoansId));
                            }

                            db.acraLoans.RemoveRange(db.acraLoans.Where(l => l.clientId == clientId));
                            acraLoans aloans = new Models.acraLoans();

                            for (int i = 0; i < loans.Count; i++)
                            {
                                aloans.acraId = am.acraId;
                                aloans.balance = loans[i].Balance.GetValueOrDefault();
                                aloans.cCreditSource = loans[i].SourceName;
                                aloans.clientId = clientId;
                                aloans.collateralAmount = loans[i].CollateralAmount.GetValueOrDefault();
                                aloans.collateralCurrencyId = loans[i].CollateralCurrency.GetValueOrDefault();
                                aloans.collateralNotes = loans[i].CollateralNotes;
                                aloans.contractAmount = loans[i].ContractAmount.GetValueOrDefault();
                                aloans.creditAmount = loans[i].Amount.GetValueOrDefault();
                                aloans.creditCloseDate = loans[i].CloseDate.GetValueOrDefault();
                                aloans.creditID = loans[i].CreditID;
                                aloans.creditingDate = loans[i].CreditingDate.GetValueOrDefault();
                                aloans.creditScope = loans[i].Scope;
                                aloans.creditStatus = loans[i].CStatus;
                                aloans.currencyId = loans[i].currID.GetValueOrDefault();
                                aloans.DelayPaymentFrequency13_24 = loans[i].DelayPaymentFrequency13_24.GetValueOrDefault();
                                aloans.DelayPaymentFrequency1_12 = loans[i].DelayPaymentFrequency1_12.GetValueOrDefault();
                                aloans.DelayPaymentQuantity13_24 = loans[i].DelayPaymentQuantity13_24.GetValueOrDefault();
                                aloans.DelayPaymentQuantity1_12 = loans[i].DelayPaymentQuantity1_12.GetValueOrDefault();
                                aloans.iterest = loans[i].Interest.GetValueOrDefault();
                                aloans.lastPaymentDate = loans[i].LastPaymentDate.GetValueOrDefault();
                                aloans.loanClass = loans[i].LClass;
                                aloans.loanDBId = loans[i].loan_ID;
                                if (loans[i].l_type.GetValueOrDefault() == 1)
                                {
                                    aloans.lType = "Վարկ";
                                    loansCount++;
                                    double tmpmaxLoan = 0;

                                    if (aloans.currencyId == 1)
                                        tmpmaxLoan = aloans.creditAmount;
                                    else
                                        tmpmaxLoan = aloans.creditAmount * Classes.CommonFunction.GetExchRate(2, DateTime.Now);


                                    if (maxLoan < tmpmaxLoan)
                                    {
                                        maxLoan = tmpmaxLoan;
                                    }//if (maxLoan < aloans.creditAmount)
                                }
                                else
                                    aloans.lType = "Երաշխավորություն";

                                aloans.pledgeSubject = loans[i].PledgeSubject;
                                aloans.CreditUsePlace = loans[i].CreditUsePlaceText;
                                aloans.PersonCount = loans[i].PersonCount.ToString();
                                aloans.GuarantorCount = loans[i].GuarantorCount.ToString();
                                aloans.pmt = loans[i].PMT.GetValueOrDefault();
                                aloans.IncomingDate = loans[i].IncomingDate;
                                aloans.AmountOverdue = loans[i].AmountOverdue;
                                aloans.CreditType = loans[i].CreditSortName;
                                aloans.OutstandingPercent = loans[i].OutstandingPercent;
                                aloans.resultId = am.resultId;
                                db.acraLoans.Add(aloans);
                                db.SaveChanges();
                            }//for(int i = 0; i < loans.Count; i++)

                            var totLoans = db.acraLoans.Where(l => l.acraId == am.acraId).ToList();


                            foreach (var tl in totLoans)
                            {
                                tl.DelayPaymentQuantity1_12 = tl.DelayPaymentQuantity1_12 + tl.DelayPaymentQuantity13_24;
                                ovrdDaysCount += Convert.ToInt32(tl.OverdueDays);
                                if (tl.creditStatus == "գործող")
                                {
                                    LoansTotSum += (tl.balance * Classes.CommonFunction.GetExchRate(tl.currencyId, DateTime.Now));
                                    if (tl.cCreditSource == "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ")
                                    {
                                        sefLoans += (tl.creditAmount * Classes.CommonFunction.GetExchRate(tl.currencyId, DateTime.Now));
                                        sefLoansCount++;
                                    }//if(l.cCreditSource == "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ")
                                }//if(l.creditStatus == "գործող")
                                var osDays = acraDB.ACRA_osDays.Where(d => d.loanId == tl.loanDBId).ToList();
                                acraLoanosDays losDays = new acraLoanosDays();
                                foreach (var d in osDays)
                                {
                                    losDays.loanId = tl.acraLoansId;
                                    losDays.osMoth = d.osMoth;
                                    losDays.osValue = d.osValue;
                                    losDays.osYear = d.osYear;
                                    db.acraLoanosDays.Add(losDays);
                                    db.SaveChanges();
                                }//foreach(var d in osDays)
                            }//foreach(var l in totLoans)

                            ViewBag.loans = totLoans;
                            double Loans = 0;

                            if (LoansTotSum != 0 && sefLoans != 0)
                                Loans = Math.Round(((sefLoans / LoansTotSum) * 100), 1);

                            ViewBag.sefLoans = Loans;

                        }//if(loans.Count > 0)

                        //am.maxLoan = maxLoan;
                        //am.rcvLoansCount = loansCount;
                        var en = db.acras.Find(am.acraId);
                        en.maxLoan = maxLoan;
                        en.rcvLoansCount = loansCount;
                        en.sefLoansSum = sefLoans;
                        en.sefLoansCount = sefLoansCount;

                        db.SaveChanges();

                        var grTots = db.clientsGroup.Where(c => c.clientId == clientId).FirstOrDefault();
                        grTots.AcraLastRequestDate = en.reqDate;
                        grTots.CreditLoadAcra = LoansTotSum;
                        grTots.OverdueMoney = en.totOverdueAMD + Classes.CommonFunction.GetExchRate(2, DateTime.Now) * en.totOverdueUSD;
                        grTots.OverdueDaysCount = ovrdDaysCount;
                        grTots.Income = db.clientWorkDatas.Where(w => w.clientId == clientId).Select(s => s.salary).SingleOrDefault()
                            + db.clientWorkDatas.Where(w => w.clientId == clientId).Select(s => s.otherIncome).SingleOrDefault();



                        db.SaveChanges();
                        rtVal = 1;
                    }
                    else
                    {
                        var grTots = db.clientsGroup.Where(c => c.clientId == clientId).FirstOrDefault();
                        grTots.AcraLastRequestDate = DateTime.Now;
                        grTots.Income = db.clientWorkDatas.Where(w => w.clientId == clientId).Select(s => s.salary).SingleOrDefault()
                              + db.clientWorkDatas.Where(w => w.clientId == clientId).Select(s => s.otherIncome).SingleOrDefault();
                        db.SaveChanges();
                        rtVal = 2;
                    }//if (acraResult.Substring(1, 2) == "OK")

                }
                else
                { rtVal = -1; }//if (acraResult.Length > 2)

            }
            else { rtVal = -2; }//if (client.Count > 0)

            return rtVal;
        }

        public async Task<ActionResult> GetACRA(long clientId = 0, long appId = 0, string acraReqType = "2")
        {
            ViewBag.appId = appId;
            double sefLoans = 0;
            int sefLoansCount = 0;
            double LoansTotSum = 0;
            //int ovrdDaysCount = 0;
            int dbInsertErr = 0;

            double maxLoan = 0;
            int loansCount = 0;
            int actLoansCount = 0;
            int closedLoansCount = 0;
            int actGuarantCount = 0;
            int closedGuarantCount = 0;

            acraRequestInit acreq = new Models.acraRequestInit();
            acras am = new Models.acras();

            var client = db.clients.Where(c => c.clientId == clientId).ToList();
            am.clientId = clientId;
            if (client.Count > 0)
            {
                acreq.cFirstName = client[0].clientName.TrimEnd();
                acreq.cLastName = client[0].clientLastName.TrimEnd();
                acreq.cNumber = client[0].clientId.ToString();
                acreq.socNumb = client[0].socNumb.TrimEnd();
                string acraserUri = "http://192.168.0.117:9988/acraserv/api/acraResponse/" + acreq.cFirstName + "/" + acreq.cLastName + "/" + acreq.socNumb + "/" + clientId.ToString() + "/" + "/2/" + acraReqType  +"/"; //"http://192.168.0.117:8888/acraserv/api/acraResponse/" + acreq.cFirstName + "/" + acreq.cLastName + "/" + acreq.socNumb;//"http://192.168.0.117:8989/acraserv/api/acraResponse/" + acreq.cFirstName + "/" + acreq.cLastName + "/" + acreq.socNumb + "/" + acreq.cNumber;
                string acraResult = await acraRequest(acraserUri); //"_OK";                

                if (acraResult.Length > 2)
                {
                    if (acraResult.Substring(1, 2) == "OK")
                    {                        
                        var acraR = acraDB.ACRA_RequestsI.Where(a => a.soc_card == acreq.socNumb).OrderByDescending(asrt => asrt.r_ans_date).First();

                        am.cAddress = acraR.address;
                        am.clientId = client[0].clientId;
                        am.delayMax = acraR.delay_max.GetValueOrDefault();
                        am.delay_tot = acraR.delay_tot.GetValueOrDefault();
                        am.dob = acraR.DOB.GetValueOrDefault();
                        am.employm = acraR.employer;
                        am.firstName = acraR.first_name.TrimEnd();
                        if (acraR.income_update_date.GetValueOrDefault().Year > 200)
                            am.incUpdateDate = acraR.income_update_date.GetValueOrDefault();
                        else
                            am.incUpdateDate = new DateTime(1900, 1, 1);
                        am.lastName = acraR.last_name.TrimEnd();
                        am.passp = acraR.passport;
                        am.req30Count = acraR.req30_count.GetValueOrDefault();
                        am.reqCount = acraR.req_count.GetValueOrDefault();
                        am.resultId = acraR.result_ID;
                        am.socNumb = acraR.soc_card;
                        am.userId = User.Identity.GetUserId();
                        am.reqDate = DateTime.Now;
                        am.ReportNumber = acraR.ReportNumber;
                        am.IdCardNumber = acraR.IdCardNumber;
                        am.PersonBankruptIncome = acraR.PersonBankruptIncome;
                        am.SwitchisClassQuantity = acraR.SwitchisClassQuantity;
                        am.InformationReviewDate = acraR.InformationReviewDate;
                        am.TheWorstClassLoan = acraR.TheWorstClassLoan;
                        am.TheWorsClassGuarantee = acraR.TheWorsClassGuarantee;
                        am.SelfEnquiryQuantity30 = acraR.SelfEnquiryQuantity30;
                        am.SelfEnquiryQuantity = acraR.SelfEnquiryQuantity;
                        am.DelayPaymentQuantity = acraR.DelayPaymentQuantity;                        
                        //------Total Liabil------
                        var totLiab = acraDB.ACRA_R_TotalLiabilities.Where(t => t.fk_result_ID == am.resultId).ToList();
                        if (totLiab.Count > 0)
                        {
                            foreach (var tots in totLiab)
                            {

                                switch (tots.l_type)
                                {
                                    case 1:
                                        if (tots.currID == 1)
                                            am.totLiabAMD += tots.Amount.GetValueOrDefault();
                                        else
                                            am.totLiabUSD += tots.Amount.GetValueOrDefault();
                                        break;
                                    case 2:
                                        if (tots.currID == 1)
                                            am.totGuarAMD += tots.Amount.GetValueOrDefault();
                                        else
                                            am.totGuarUSD += tots.Amount.GetValueOrDefault();
                                        break;
                                }//switch(tots.l_type)

                            }//foreach(var tots in totLiab)
                        }//if(totLiab.Count > 0)
                        //------Total Liabil------
                        //-------------Total Uverdues--------
                        var ovrd = acraDB.ACRA_R_CurrentOverdue.Where(o => o.fk_result_ID == am.resultId).ToList();
                        foreach (var ovr in ovrd)
                        {
                            switch (ovr.l_type)
                            {
                                case 1:
                                    if (ovr.currID == 1)
                                        am.totOverdueAMD += ovr.Amount.GetValueOrDefault();
                                    else
                                        am.totOverdueUSD += ovr.Amount.GetValueOrDefault();
                                    break;
                                case 2:
                                    if (ovr.currID == 1)
                                        am.totOverdueGAMD += ovr.Amount.GetValueOrDefault();
                                    else
                                        am.totOverdueGUSD += ovr.Amount.GetValueOrDefault();
                                    break;
                            }//switch(ovr.l_type)
                        }
                        //-------------Total Uverdues--------
                        try
                        {
                            db.acras.Add(am);
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException saveEx)
                        {
                            dbInsertErr++;
                            foreach (var eve in saveEx.EntityValidationErrors)
                            {
                                acraResult += " Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State.ToString() + " has the following validation errors:";
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    acraResult += $"- Property:" + ve.PropertyName + " , Error:" + ve.ErrorMessage;
                                }
                            }
                        }
                        catch (Exception clnex)
                        {
                            dbInsertErr++;
                            acraResult = "ACRA Request main data input error_" + clnex.Message + " # " + acraResult;
                        }
                        

                        //-------------Request info---------------
                        var reqInfo = acraDB.ACRA_RequestesInfo.Where(r => r.resultId == am.resultId);
                        acraRequestesInfo rInfo = new acraRequestesInfo();

                        List<acraRequestesInfo> reqInfoList = new List<acraRequestesInfo>(); 
                        foreach (var rq in reqInfo)
                        {                            
                            rInfo.resultId = am.resultId;
                            rInfo.acraId = am.acraId;
                            rInfo.vBankName = rq.vBankName;
                            rInfo.vDate = rq.vDate;
                            rInfo.vNumber = rq.vNumber;
                            rInfo.vReason = rq.vReason;
                            rInfo.vSubReason = rq.vSubReason;
                            reqInfoList.Add(rInfo);
                            try
                            {
                                db.acraRequestesInfo.Add(rInfo);
                                db.SaveChanges();
                            }
                            catch (DbEntityValidationException saveEx)
                            {
                                dbInsertErr++;
                                foreach (var eve in saveEx.EntityValidationErrors)
                                {
                                    acraResult += " Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State.ToString() + " has the following validation errors:";
                                    foreach (var ve in eve.ValidationErrors)
                                    {
                                        acraResult += $"- Property:" + ve.PropertyName + " , Error:" + ve.ErrorMessage;
                                    }
                                }
                            }
                            catch (Exception clnex)
                            {
                                dbInsertErr++;
                                acraResult = "ACRA requestes data input error_" + clnex.Message + " # " + acraResult;
                            }
                            rInfo = new acraRequestesInfo();
                        }//foreach(var rq in reqInfo)

                        ViewBag.reqInfoList = reqInfoList;
                        //----------------------------

                        //---------------Interrel-------------

                        var inrel = acraDB.ACRA_Interrelated.Where(r => r.resultId == am.resultId);
                        acraInterrelated interRel = new acraInterrelated();
                        List<acraInterrelated> interList = new List<acraInterrelated>();
                        foreach (var ir in inrel)
                        {
                            interRel.acraId = am.acraId;
                            interRel.iAmountDue = ir.iAmountDue;
                            interRel.iAmountOverdue = ir.iAmountOverdue;
                            interRel.iContractAmount = ir.iContractAmount;
                            interRel.iCreditClassification = ir.iCreditClassification;
                            interRel.iCreditStart = ir.iCreditStart;
                            interRel.iCurrency = ir.iCurrency;
                            interRel.iLastInstallment = ir.iLastInstallment;
                            interRel.iNumber = ir.iNumber;
                            interRel.iOutstandingDate = ir.iOutstandingDate;
                            interRel.iOutstandingPercent = ir.iOutstandingPercent;
                            interRel.resultId = am.resultId;
                            interRel.vDebtorNum = ir.vDebtorNum;
                            interRel.vInterrelatedSourceName = ir.vInterrelatedSourceName;
                            interList.Add(interRel);
                            try
                            {
                                db.acraInterrelated.Add(interRel);
                                db.SaveChanges();
                            }
                            catch (DbEntityValidationException saveEx)
                            {
                                dbInsertErr++;
                                foreach (var eve in saveEx.EntityValidationErrors)
                                {
                                    acraResult += " Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State.ToString() + " has the following validation errors:";
                                    foreach (var ve in eve.ValidationErrors)
                                    {
                                        acraResult += $"- Property:" + ve.PropertyName + " , Error:" + ve.ErrorMessage;
                                    }
                                }
                            }
                            catch (Exception clnex)
                            {
                                dbInsertErr++;
                                acraResult = "ACRA Interreleated loans data input error_" + clnex.Message + " # " + acraResult;
                            }
                            interRel = new acraInterrelated();
                        }//foreach(var ir in inrel)

                        ViewBag.interList = interList;
                        //----------------------------


                        var loans = acraDB.ACRA_LoansGetDetEx(am.resultId).ToList();

                        if (loans.Count > 0)
                        {
                            var dloans = db.acraLoans.Where(d => d.clientId == clientId).ToList();
                            foreach (var dl in dloans)
                            {
                                db.acraLoanosDays.RemoveRange(db.acraLoanosDays.Where(od => od.loanId == dl.acraLoansId));
                            }

                            db.acraLoans.RemoveRange(db.acraLoans.Where(l => l.clientId == clientId));
                            acraLoans aloans = new Models.acraLoans();

                            for (int i = 0; i < loans.Count; i++)
                            {
                                aloans.acraId = am.acraId;
                                aloans.balance = loans[i].Balance.GetValueOrDefault();
                                aloans.cCreditSource = loans[i].SourceName;
                                aloans.clientId = clientId;
                                aloans.collateralAmount = loans[i].CollateralAmount.GetValueOrDefault();
                                aloans.collateralCurrencyId = loans[i].CollateralCurrency.GetValueOrDefault();
                                aloans.collateralNotes = loans[i].CollateralNotes;
                                aloans.contractAmount = loans[i].ContractAmount.GetValueOrDefault();
                                aloans.creditAmount = loans[i].Amount.GetValueOrDefault();
                                aloans.creditCloseDate = loans[i].CloseDate.GetValueOrDefault();
                                aloans.creditID = loans[i].CreditID;
                                aloans.creditingDate = loans[i].CreditingDate.GetValueOrDefault();
                                aloans.creditScope = loans[i].Scope;
                                aloans.creditStatus = loans[i].CStatus;
                                aloans.currencyId = loans[i].currID.GetValueOrDefault();
                                aloans.DelayPaymentFrequency13_24 = loans[i].DelayPaymentFrequency13_24.GetValueOrDefault();
                                aloans.DelayPaymentFrequency1_12 = loans[i].DelayPaymentFrequency1_12.GetValueOrDefault();
                                aloans.DelayPaymentQuantity13_24 = loans[i].DelayPaymentQuantity13_24.GetValueOrDefault();
                                aloans.DelayPaymentQuantity1_12 = loans[i].DelayPaymentQuantity1_12.GetValueOrDefault();
                                aloans.iterest = loans[i].Interest.GetValueOrDefault();
                                aloans.lastPaymentDate = loans[i].LastPaymentDate.GetValueOrDefault();
                                aloans.loanClass = loans[i].LClass;
                                aloans.loanDBId = loans[i].loan_ID;
                                if (loans[i].l_type.GetValueOrDefault() == 1)
                                {
                                    aloans.lType = "Վարկ";
                                    loansCount++;                                    
                                    double tmpmaxLoan = 0;

                                    if (aloans.currencyId == 1)
                                        tmpmaxLoan = aloans.creditAmount;
                                    else
                                        tmpmaxLoan = aloans.creditAmount * Classes.CommonFunction.GetExchRate(2, DateTime.Now);


                                    if (maxLoan < tmpmaxLoan)
                                    {
                                        maxLoan = tmpmaxLoan;
                                    }//if (maxLoan < aloans.creditAmount)
                                }
                                else
                                {
                                    aloans.lType = "Երաշխավորություն";
                                }

                                aloans.pledgeSubject = loans[i].PledgeSubject;
                                aloans.CreditUsePlace = loans[i].CreditUsePlaceText;
                                aloans.PersonCount = loans[i].PersonCount.ToString();
                                aloans.GuarantorCount = loans[i].GuarantorCount.ToString();
                                aloans.pmt = loans[i].PMT.GetValueOrDefault();
                                aloans.IncomingDate = loans[i].IncomingDate;
                                aloans.AmountOverdue = loans[i].AmountOverdue;
                                aloans.CreditType = loans[i].CreditSortName;
                                aloans.OutstandingPercent = loans[i].OutstandingPercent;
                                aloans.resultId = am.resultId;                                
                                db.acraLoans.Add(aloans);
                                db.SaveChanges();                               

                            }//for(int i = 0; i < loans.Count; i++)

                            var totLoans = db.acraLoans.Where(l => l.acraId == am.acraId).ToList();


                            foreach (var tl in totLoans)
                            {
                                tl.DelayPaymentQuantity1_12 = tl.DelayPaymentQuantity1_12 + tl.DelayPaymentQuantity13_24;
                                if (tl.creditStatus == "գործող")
                                {
                                    LoansTotSum += (tl.balance * Classes.CommonFunction.GetExchRate(tl.currencyId, DateTime.Now));
                                    if (tl.cCreditSource == "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ")
                                    {
                                        sefLoans += (tl.balance * Classes.CommonFunction.GetExchRate(tl.currencyId, DateTime.Now));
                                        sefLoansCount++;
                                    }//if(l.cCreditSource == "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ")
                                }//if(l.creditStatus == "գործող")

                                if(tl.lType == "Վարկ")
                                {
                                    switch (tl.creditStatus)
                                    {
                                        case "մարված":
                                            closedLoansCount++;
                                            break;
                                        case "գործող":
                                            actLoansCount++;
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (tl.creditStatus)
                                    {
                                        case "մարված":
                                            closedGuarantCount++;
                                            break;
                                        case "գործող":
                                            actGuarantCount++;
                                            break;
                                    }
                                }//if(tl.lType == "Վարկ")

                                var osDays = acraDB.ACRA_osDays.Where(d => d.loanId == tl.loanDBId).ToList();
                                acraLoanosDays losDays = new acraLoanosDays();
                                foreach (var d in osDays)
                                {
                                    losDays.loanId = tl.acraLoansId;
                                    losDays.osMoth = d.osMoth;
                                    losDays.osValue = d.osValue;
                                    losDays.osYear = d.osYear;
                                    db.acraLoanosDays.Add(losDays);
                                    db.SaveChanges();
                                }//foreach(var d in osDays)

                            }//foreach(var l in totLoans)

                            ViewBag.loans = totLoans;
                            
                            double Loans = 0;

                            if (LoansTotSum != 0 && sefLoans != 0)
                                Loans = Math.Round(((sefLoans / LoansTotSum) * 100), 1);

                            ViewBag.sefLoans = Loans;

                        }//if(loans.Count > 0)

                        //am.maxLoan = maxLoan;
                        //am.rcvLoansCount = loansCount;
                        

                        try
                        {
                            var en = db.acras.Find(am.acraId);
                            en.maxLoan = maxLoan;
                            en.rcvLoansCount = loansCount;
                            en.sefLoansSum = sefLoans;
                            en.sefLoansCount = sefLoansCount;

                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException saveEx)
                        {
                            dbInsertErr++;
                            foreach (var eve in saveEx.EntityValidationErrors)
                            {
                                acraResult += " Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State.ToString() + " has the following validation errors:";
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    acraResult += $"- Property:" + ve.PropertyName + " , Error:" + ve.ErrorMessage;
                                }
                            }
                        }
                        catch (Exception clnex)
                        {
                            dbInsertErr++;
                            
                        }

                        if (dbInsertErr > 0)
                            ViewBag.acraErr = acraResult;

                        //var grTots = db.clientsGroup.Where(c => c.clientId == clientId).FirstOrDefault();
                        //grTots.AcraLastRequestDate = en.reqDate;
                        //grTots.CreditLoadAcra = LoansTotSum;
                        //grTots.OverdueMoney = en.totOverdueAMD + Classes.CommonFunction.GetExchRate(2, DateTime.Now) * en.totOverdueUSD;
                        //grTots.OverdueDaysCount = ovrdDaysCount;
                        //grTots.Income = db.clientWorkDatas.Where(w => w.clientId == clientId).Select(s => s.salary).SingleOrDefault();
                        //db.SaveChanges();
                    }
                    else
                    {
                        ViewBag.acraErr = acraResult;
                    }//if(acraResult.Substring(0,2) == "OK")
                }
                else
                {
                    ViewBag.acraErr = acraResult;
                }//if(acraResult.Length > 2)

            }//if(client.Count > 0)

            ViewBag.over_debt =  am.totOverdueAMD + am.totOverdueUSD * CommonFunction.GetExchRate(2, DateTime.Now) + am.totOverdueGAMD + am.totOverdueGUSD * CommonFunction.GetExchRate(2, DateTime.Now);

            ViewBag.number_of_current_loan = actLoansCount;
            ViewBag.number_of_current_guaranty = actGuarantCount;
            ViewBag.number_of_closed_loan = closedLoansCount;
            ViewBag.number_of_closed_guaranty = closedGuarantCount;


            return View(am);
        }//public async Task<ActionResult> GetACRA(long clientId)

        public ActionResult ACRAProfile(long clientId = 0, long appId = 0)
        {
            int loansCount = 0;
            int actLoansCount = 0;
            int closedLoansCount = 0;
            int actGuarantCount = 0;
            int closedGuarantCount = 0;

            ViewBag.appId = appId;

            acras am = new Models.acras();
            am.clientId = clientId;
            var acra = db.acras.Where(c => c.clientId == clientId).ToList();

            foreach (var a in acra)
            {
                am.acraId = a.acraId;
                am.cAddress = a.cAddress;
                am.clientId = a.clientId;
                am.delayMax = a.delayMax;
                am.delay_tot = a.delay_tot;
                am.dob = a.dob;
                am.employm = a.employm;
                am.firstName = a.firstName;
                am.incUpdateDate = a.incUpdateDate;
                am.lastName = a.lastName;
                am.maxLoan = a.maxLoan;
                am.passp = a.passp;
                am.rcvLoansCount = a.rcvLoansCount;
                am.req30Count = a.req30Count;
                am.reqCount = a.reqCount;
                am.resultId = a.resultId;
                am.socNumb = a.socNumb;
                am.totGuarAMD = a.totGuarAMD;
                am.totGuarUSD = a.totGuarUSD;
                am.totLiabAMD = a.totLiabAMD;
                am.totLiabUSD = a.totLiabUSD;
                am.totOverdueAMD = a.totOverdueAMD;
                am.totOverdueGAMD = a.totOverdueGAMD;
                am.totOverdueGUSD = a.totOverdueGUSD;
                am.totOverdueUSD = a.totOverdueUSD;
                am.userId = a.userId;
                am.reqDate = a.reqDate;
                am.ReportNumber = a.ReportNumber;
                am.IdCardNumber = a.IdCardNumber;
                am.PersonBankruptIncome = a.PersonBankruptIncome;
                am.SwitchisClassQuantity = a.SwitchisClassQuantity;
                am.InformationReviewDate = a.InformationReviewDate;
                am.TheWorstClassLoan = a.TheWorstClassLoan;
                am.TheWorsClassGuarantee = a.TheWorsClassGuarantee;
                am.SelfEnquiryQuantity30 = a.SelfEnquiryQuantity30;
                am.SelfEnquiryQuantity = a.SelfEnquiryQuantity;
                am.DelayPaymentQuantity = a.DelayPaymentQuantity;                
            }

            var reqInfo = db.acraRequestesInfo.Where(r => r.resultId == am.resultId).ToList();
            acraRequestesInfo rInfo = new Models.acraRequestesInfo();
            List<acraRequestesInfo> reqInfoList = new List<acraRequestesInfo>();
            foreach (var rq in reqInfo)
            {
                rInfo.resultId = am.resultId;
                rInfo.acraId = am.acraId;
                rInfo.vBankName = rq.vBankName;
                rInfo.vDate = rq.vDate;
                rInfo.vNumber = rq.vNumber;
                rInfo.vReason = rq.vReason;
                rInfo.vSubReason = rq.vSubReason;
                reqInfoList.Add(rInfo);
                rInfo = new Models.acraRequestesInfo();
            }

            ViewBag.reqInfoList = reqInfoList;
            //----------------------------

            //---------------Interrel-------------

            var inrel = db.acraInterrelated.Where(r => r.resultId == am.resultId);
            acraInterrelated interRel = new acraInterrelated();
            List<acraInterrelated> interList = new List<acraInterrelated>();
            foreach (var ir in inrel)
            {
                interRel.acraId = am.acraId;
                interRel.iAmountDue = ir.iAmountDue;
                interRel.iAmountOverdue = ir.iAmountOverdue;
                interRel.iContractAmount = ir.iContractAmount;
                interRel.iCreditClassification = ir.iCreditClassification;
                interRel.iCreditStart = ir.iCreditStart;
                interRel.iCurrency = ir.iCurrency;
                interRel.iLastInstallment = ir.iLastInstallment;
                interRel.iNumber = ir.iNumber;
                interRel.iOutstandingDate = ir.iOutstandingDate;
                interRel.iOutstandingPercent = ir.iOutstandingPercent;
                interRel.resultId = am.resultId;
                interRel.vDebtorNum = ir.vDebtorNum;
                interRel.vInterrelatedSourceName = ir.vInterrelatedSourceName;
                interList.Add(interRel);
                interRel = new acraInterrelated();
            }//foreach(var ir in inrel)

            ViewBag.interList = interList;


            var totLoans = db.acraLoans.Where(l => l.acraId == am.acraId).OrderBy(t => t.lType).OrderBy(s => s.creditStatus).ToList();

            double sefLoans = 0;
            double LoansTotSum = 0;
            foreach (var l in totLoans)
            {
                if (l.lType == "Վարկ")
                {
                    switch (l.creditStatus)
                    {
                        case "մարված":
                            closedLoansCount++;
                            break;
                        case "գործող":
                            actLoansCount++;
                            break;
                    }
                }
                else
                {
                    switch (l.creditStatus)
                    {
                        case "մարված":
                            closedGuarantCount++;
                            break;
                        case "գործող":
                            actGuarantCount++;
                            break;
                    }
                }//if(tl.lType == "Վարկ")

                l.DelayPaymentQuantity1_12 = l.DelayPaymentQuantity1_12 + l.DelayPaymentQuantity13_24;
                if (l.creditStatus == "գործող")
                {
                    LoansTotSum += (l.creditAmount * Classes.CommonFunction.GetExchRate(l.currencyId, DateTime.Now));
                    if (l.cCreditSource == "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ")
                    {
                        sefLoans += (l.creditAmount * Classes.CommonFunction.GetExchRate(l.currencyId, DateTime.Now));
                    }//if(l.cCreditSource == "ՍԵՖ Ինտերնեյշնլ ՈւՎԿ ՍՊԸ")
                }//if(l.creditStatus == "գործող")
            }//foreach(var l in totLoans)

            ViewBag.loans = totLoans;
             
            double Loans = 0;

            if(LoansTotSum != 0 && sefLoans !=0)
            Loans = Math.Round(((sefLoans / LoansTotSum) * 100), 1);

            ViewBag.sefLoans = Loans;
            
            ViewBag.over_debt = am.totOverdueAMD + am.totOverdueUSD * CommonFunction.GetExchRate(2, DateTime.Now) + am.totOverdueGAMD + am.totOverdueGUSD * CommonFunction.GetExchRate(2, DateTime.Now);

            ViewBag.number_of_current_loan = actLoansCount;
            ViewBag.number_of_current_guaranty = actGuarantCount;
            ViewBag.number_of_closed_loan = closedLoansCount;
            ViewBag.number_of_closed_guaranty = closedGuarantCount;


            return View("GetACRA", am);
        }


        public ActionResult ACRALoanDetails(long loanId = 0)
        {

            acraLoanDetails ld = new Models.acraLoanDetails();

            var loan = db.acraLoans.Where(l => l.acraLoansId == loanId).ToList();

            foreach(var l in loan)
            {
                ld.contract_amount = l.contractAmount.ToString("N0");
                ld.Amount = l.creditAmount.ToString("N0");
                ld.AmountBalance = (l.contractAmount - l.balance).ToString("N0");
                ld.amount_overdue = (l.AmountOverdue != null) ? Convert.ToString(l.AmountOverdue) : "0";
                ld.balance = l.balance.ToString("N2");
                ld.classification_last_date = String.Format("{0:d}", l.ClassificationLastDate);
                ld.clCurrency = (l.collateralAmount > 0) ? "ՀՀ Դրամ" : "";// (l.collateralCurrencyId != 0) ? ((l.collateralCurrencyId == 1) ?  "ՀՀ Դրամ" : "ԱՄՆ Դոլար") : "";
                ld.clientName = db.clients.Where(c => c.clientId == l.clientId).Select(s => s.clientName + " " + s.clientLastName).SingleOrDefault();
                ld.close_date = String.Format("{0:d}", l.creditCloseDate);
                ld.collateral_note = l.collateralNotes;
                ld.collateral_value = (l.collateralAmount > 0) ? l.collateralAmount.ToString("N0") : "";
                ld.credit_date = String.Format("{0:d}", l.creditingDate);
                ld.credit_id = l.creditID;
                ld.credit_notes = l.CreditNotes;
                ld.credit_scope = l.creditScope;
                ld.credit_start = String.Format("{0:d}", l.creditingDate);
                ld.credit_type = l.CreditType;
                ld.currency = (l.currencyId == 1) ? "ՀՀ Դրամ" : "ԱՄՆ Դոլար";
                ld.guarantor_amount = l.GuarantorAmount.ToString();
                ld.guarantor_count = l.GuarantorCount;
                ld.incoming_date = String.Format("{0:d}", l.IncomingDate);
                ld.Interest = l.iterest.ToString("N2");
                ld.last_pmt_date = String.Format("{0:d}", l.lastPaymentDate);
                ld.lender = l.cCreditSource;
                ld.loan_class = l.loanClass;
                ld.l_type = l.lType;
                ld.outstanding_date = String.Format("{0:d}", l.OutstandingDate);
                ld.outstanding_percent = l.OutstandingPercent.ToString();
                ld.overdue_days = l.OverdueDays.ToString();
                ld.payment_amount = l.PaymentAmount.ToString();
                ld.pledge = l.pledgeSubject;
                ld.status = l.creditStatus;
                int usePlId = 0;
                try {
                    usePlId = int.Parse(l.CreditUsePlace);
                }
                catch { usePlId = 1; }

                ld.use_place = acraDB.ACRA_CreditUsePlacesGet(usePlId).Select(s => s.CreditUsePlace).SingleOrDefault();// l.CreditUsePlace;

                var lods = db.acraLoanosDays.Where(o => o.loanId == l.acraLoansId).OrderByDescending(r=>r.osYear).ToList();
                int yearsCount = db.acraLoanosDays.Where(o => o.loanId == l.acraLoansId).Select(y => y.osYear).Distinct().Count();

                if(yearsCount > 0)
                {
                    ld.lOSdays = new string [yearsCount, 13];
                }

                string tmpYear = "";
                int yRow = 0;
                int cntr = 0;
                foreach (var lo in lods)
                {                    
                    if (lo.osYear != tmpYear)
                    {
                        if (cntr > 0)
                            yRow++;

                        tmpYear = lo.osYear;                        
                        ld.lOSdays[yRow, 0] = lo.osYear;
                        
                    }//if (lo.osYear != tmpYear)

                    ld.lOSdays[yRow, int.Parse(lo.osMoth)] = lo.osValue;

                    cntr++;
                    //switch(lo.osMoth)
                    //{
                    //    case "01":
                    //        ld.lOSdays[yRow, 1] = lo.osValue;
                    //        break;
                    //    case "02":
                    //        ld.lOSdays[yRow, 2] = lo.osValue;
                    //        break;
                    //    case "03":
                    //        ld.lOSdays[yRow, 3] = lo.osValue;
                    //        break;
                    //    case "04":
                    //        ld.lOSdays[yRow, 4] = lo.osValue;
                    //        break;
                    //}

                }//foreach (var lo in lods)

                if(cntr > 0)
                {
                    for(int i = 0; i < yearsCount; i++)
                    {
                        for(int j = 0; j < 13; j++)
                        {
                            if (ld.lOSdays[i, j] == null)
                                ld.lOSdays[i, j] = "  ";
                        }
                    }//for(int i = 0; i < yearsCount; i++)
                }//if(cntr > 0)

            }//foreach(var l in loan)

            

            return View(ld);
        }

    }
}