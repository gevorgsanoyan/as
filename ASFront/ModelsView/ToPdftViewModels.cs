using ASFront.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



using System.Data.Entity;



using ASFront.ModelsView;
using ASFront.Classes;



namespace ASFront.ModelsView
{
    public class GoldAcceptanceActViewModels
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public GoldAcceptanceActViewModels() { }
        public GoldAcceptanceActViewModels(long ApplicationID) : this()
        {


            var app = db.applications.Where(p => p.applicationId == ApplicationID).FirstOrDefault();

            var cl = db.clients.Where(p => p.clientId == app.clientId).FirstOrDefault();


            this.ApplicationID = app.applicationId;



            var goldCollateral = db.GoldCollateral.Include(g => g.applications).Include(g => g.GoldAssayes).Include(g => g.GoldTypes).Where(p => (p.applicationId == ApplicationID)).ToList() ?? new List<GoldCollaterals>();

            this.GoldCollaterals = goldCollateral;


            this.QuantitySum = goldCollateral.Sum(p => p.Quantity);
            this.TotalWeightWithJewelsSum = goldCollateral.Sum(p => p.TotalWeightWithJewels);
            this.NetWeightSum = goldCollateral.Sum(p => p.NetWeight);
            this.EstimatedTotalCostAMDSum = goldCollateral.Sum(p => p.EstimatedTotalCostAMD);

            this.aprDate = app.aprDate;


            this.CreditSpecialistName = db.UserASProfiles.Where(p => p.UserId == app.userId).Select(p => (p.FirstName + " " + p.LastName)).FirstOrDefault() ?? string.Empty;


            this.ClientName = cl.clientName + " " + cl.clientLastName;

            this.ClientLastName = cl.clientLastName;
            this.ClientFirstName = cl.clientName;


            List<Items> Items =
                        db.Items.Where(p => p.applicationId == ApplicationID).Distinct().ToList();

            double credTotSum = app.creditSum.Value;
            double clInvest = 0;


            //foreach (var itm in Items)
            //{
            //    credTotSum += itm.Sum;
            //    clInvest += itm.ClientInvest;
            //}

            var product = db.Products.Where(p => p.productId == app.productId).FirstOrDefault();

            var Currency = db.CurrencyTypes.Where(p => p.currencyTypesId == product.productCurrency).FirstOrDefault();

            string CurrencyText = " " + Currency.currencyArm;
            this.CreditTotalAmount = credTotSum.ToString("N0") + CurrencyText;
            this.EstimatedTotalCostAMDSumStr = this.EstimatedTotalCostAMDSum.ToString("N0") + CurrencyText;

            if (this.EstimatedTotalCostAMDSum > 0)
                this.LoanCollateralRatio = ((credTotSum / this.EstimatedTotalCostAMDSum)*100).ToString("N2") + "%";
            else
            {
                this.LoanCollateralRatio = "0%";
            }


            this.Currency = CommonFunction.CurrencyIndexDic[2].ToString("N0");
            this.DocVesion = "version " + "29.12.2017";

        }



        public List<GoldCollaterals> GoldCollaterals { get; set; } = new List<Models.GoldCollaterals>();



        [Display(Name = "Հայտ")]
        public long ApplicationID { get; set; } = 0;


        [Display(Name = "Հաճախորդ")]
        public long clientId { get; set; } = 0;




        [Display(Name = "Քանակ")]
        public int QuantitySum { get; set; } = 0;





        [Display(Name = "Ընդ.կշիռը(գր.)")]
        public double TotalWeightWithJewelsSum { get; set; } = 0;




        [Display(Name = "Մաք.կշիռը(գր.)")]
        public double NetWeightSum { get; set; } = 0;





        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public double EstimatedTotalCostAMDSum { get; set; } = 0;

        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public string EstimatedTotalCostAMDSumStr { get; set; }


        [Display(Name = "Հայտի հաստատման ամսաթիվ")]
        [DataType(DataType.Date)]
        public DateTime aprDate { get; set; }



        [Display(Name = "Վարկային մասնագետ")]
        public string CreditSpecialistName { get; set; }



        [Display(Name = "Գրավատու")]
        public string ClientName { get; set; }

        [Display(Name = "Գրավատու")]
        public string ClientFirstName { get; set; }

        [Display(Name = "Գրավատու")]
        public string ClientLastName { get; set; }


        [Display(Name = "Վարկի Ընդհանուր Գումար")]
        public string CreditTotalAmount { get; set; }


        [Display(Name = "Վարկ-գրավ հարաբերակցություն")]
        public string LoanCollateralRatio { get; set; }

        [Display(Name = "Արժույթ")]
        public string Currency { get; set; }

        [Display(Name = "DocVesion")]
        public string DocVesion { get; set; }
    }



    public class GoldLoanCreditApplicationViewModels
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public GoldLoanCreditApplicationViewModels() { }
        public GoldLoanCreditApplicationViewModels(long ApplicationID) : this()
        {


            double credTotSum = 0;
            double clInvest = 0;

            var app = db.applications.Where(p => p.applicationId == ApplicationID).FirstOrDefault();

            var cl = db.clients.Where(p => p.clientId == app.clientId).FirstOrDefault();

            var product = db.Products.Where(p => p.productId == app.productId).FirstOrDefault();

            var Currency = db.CurrencyTypes.Where(p => p.currencyTypesId == product.productCurrency).FirstOrDefault();

            List<Items> Items =
                  db.Items.Where(p => p.applicationId == ApplicationID).Distinct().ToList();



            var goldCollateral = db.GoldCollateral.Include(g => g.applications).Include(g => g.GoldAssayes).Include(g => g.GoldTypes).Where(p => (p.applicationId == ApplicationID)).ToList() ?? new List<GoldCollaterals>();



            this.EstimatedTotalCostAMDSum = goldCollateral.Sum(p => p.EstimatedTotalCostAMD);


            string CurrencyText = " " + Currency.currencyArm;


            //foreach (var itm in Items)
            //{
            //    credTotSum += itm.Sum;
            //    clInvest += itm.ClientInvest;
            //}

            credTotSum = app.creditSum.Value;

            this.ApplicationID = app.applicationId;
            this.clientId = app.clientId;

            this.aprDate = app.aprDate;
            this.aprDateStr = app.aprDate.ToShortDateString();
            this.aprDateNameStr = app.aprDate.ToLongDateString();


            this.ClientName = cl.clientName + " " + cl.clientLastName;

            this.ClientLastName = cl.clientLastName;
            this.ClientFirstName = cl.clientName;

            this.Address = "ՀՀ, " + cl.rRegion + ", " + cl.rCity + ", " + cl.rBuilding + " " + cl.rApartment;

            this.Phone = cl.tel;




            this.CreditTotalAmount = credTotSum.ToString("N0") + CurrencyText;
            this.EstimatedTotalCostAMDSumStr = this.EstimatedTotalCostAMDSum.ToString("N0") + CurrencyText;


            this.anualRate = product.anualRate.ToString("N0") + "%";

            this.fMemb = cl.fMemb.ToString("N0");

            this.fTenMemb = cl.fTenMemb.ToString("N0");


        }



        [Display(Name = "Հայտ")]
        public long ApplicationID { get; set; } = 0;


        [Display(Name = "Հաճախորդ")]
        public long clientId { get; set; } = 0;




        [Display(Name = "Հայտի հաստատման ամսաթիվ")]
        [DataType(DataType.Date)]
        public string aprDateStr { get; set; }



        [Display(Name = "Հայտի հաստատման ամսաթիվ")]
        [DataType(DataType.Date)]
        public string aprDateNameStr { get; set; }


        [Display(Name = "Գրավատու")]
        public string ClientName { get; set; }

        [Display(Name = "Գրավատու")]
        public string ClientFirstName { get; set; }

        [Display(Name = "Գրավատու")]
        public string ClientLastName { get; set; }



        [Display(Name = "Գրանցման Հասցե")]

        public string Address { get; set; }



        [Display(Name = "Phone")]

        public string Phone { get; set; }



        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public string EstimatedTotalCostAMDSumStr { get; set; }





        [Display(Name = "Վարկի Ընդհանուր Գումար")]
        public string CreditTotalAmount { get; set; }



        [Display(Name = "Վարկի ժամկետ")]
        public int CreditTerm { get; set; }



        [Display(Name = "Տարեկան տոկոսադրույք")]

        public string anualRate { get; set; }



        [Display(Name = "Ընտ. անդամների թիվը")]
        public string fMemb { get; set; }

        [Display(Name = "Ընտ. անչափահաս անդամների թիվը")]
        public string fTenMemb { get; set; }


















        /// <summary>
        /// ////////////
        /// </summary>











        [Display(Name = "Քանակ")]
        public int QuantitySum { get; set; } = 0;





        [Display(Name = "Ընդ.կշիռը(գր.)")]
        public double TotalWeightWithJewelsSum { get; set; } = 0;




        [Display(Name = "Մաք.կշիռը(գր.)")]
        public double NetWeightSum { get; set; } = 0;





        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public double EstimatedTotalCostAMDSum { get; set; } = 0;




        [Display(Name = "Հայտի հաստատման ամսաթիվ")]
        [DataType(DataType.Date)]
        public DateTime aprDate { get; set; }



        [Display(Name = "Վարկային մասնագետ")]
        public string CreditSpecialistName { get; set; }




        [Display(Name = "Վարկ-գրավ հարաբերակցություն")]
        public string LoanCollateralRatio { get; set; }

        [Display(Name = "Արժույթ")]
        public string Currency { get; set; }

        [Display(Name = "DocVesion")]
        public string DocVesion { get; set; }
    }


    public class UniversalCreditApplicationViewModels
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public UniversalCreditApplicationViewModels() { }
        public UniversalCreditApplicationViewModels(long ApplicationID) : this()
        {
            double credTotSum = 0;
            double clInvest = 0;

            var app = db.applications.Where(p => p.applicationId == ApplicationID).FirstOrDefault();

            var cl = db.clients.Where(p => p.clientId == app.clientId).FirstOrDefault();


            var br = db.Branches.Where(p => p.Id == cl.BranchtId).FirstOrDefault();

            var product = db.Products.Where(p => p.productId == app.productId).FirstOrDefault();

            var Currency = db.CurrencyTypes.Where(p => p.currencyTypesId == product.productCurrency).FirstOrDefault();

            List<Items> Items =
                  db.Items.Where(p => p.applicationId == ApplicationID).Distinct().ToList();

            var goldCollateral = db.GoldCollateral.Include(g => g.applications).Include(g => g.GoldAssayes).Include(g => g.GoldTypes).Where(p => (p.applicationId == ApplicationID)).ToList() ?? new List<GoldCollaterals>();



            this.EstimatedTotalCostAMDSum = goldCollateral.Sum(p => p.EstimatedTotalCostAMD);

            string CurrencyText = " " + Currency.currencyArm;


            //foreach (var itm in Items)
            //{
            //    credTotSum += itm.Sum;
            //    clInvest += itm.ClientInvest;
            //}
            credTotSum = app.creditSum.Value;

            this.ApplicationID = app.applicationId;
            this.clientId = app.clientId;

            this.aprDate = app.aprDate;
            this.aprDateStr = app.aprDate.ToShortDateString();
            this.aprDateNameStr = app.aprDate.ToLongDateString();


            this.ClientName = cl.clientName + " " + cl.clientLastName + " " + cl.clientMidName;

            this.ClientLastName = cl.clientLastName;
            this.ClientFirstName = cl.clientName;

            this.Address = "ՀՀ, " + cl.rRegion + ", " + cl.rCity + ", " + cl.rStreet + " փ., " + cl.rBuilding + " " + cl.rApartment;

            this.Phone = cl.mob1;

            this.CreditSpecialistName = db.UserASProfiles.Where(p => p.UserId == app.userId).Select(p => (p.FirstName + " " + p.LastName)).FirstOrDefault() ?? string.Empty;


            this.CreditTotalAmount = credTotSum.ToString("N0") + CurrencyText;
            this.EstimatedTotalCostAMDSumStr = this.EstimatedTotalCostAMDSum.ToString("N0") + CurrencyText;


            this.anualRate = (product.anualRate * 100).ToString("N2") + "%";
            if (cl.fMemb > 0)
                this.fMemb = cl.fMemb.ToString("N0");
            else
                this.fMemb = "_____";

            if(cl.fTenMemb > 0)
                this.fTenMemb = cl.fTenMemb.ToString("N0");
            else
                this.fTenMemb = "_____";

            int wm = 0;
            if(cl.fTenMemb > 0)
            {
                wm = cl.fMemb - cl.fTenMemb;
                this.fWmnMemb = wm.ToString();
            }
            else
            {
                this.fWmnMemb = "_____";
            }

            this.BrPhone = br.Phone;

            this.CreditTerm = app.CreditTerm;
            this.productName = product.productName;

        }

        
        [Display(Name = "Պրոդուկտի անվանում")]
        public string productName { get; set; }

      



        [Display(Name = "Հայտ")]
        public long ApplicationID { get; set; } = 0;


        [Display(Name = "Հաճախորդ")]
        public long clientId { get; set; } = 0;




        [Display(Name = "Հայտի հաստատման ամսաթիվ")]
        [DataType(DataType.Date)]
        public string aprDateStr { get; set; }



        [Display(Name = "Հայտի հաստատման ամսաթիվ")]
        [DataType(DataType.Date)]
        public string aprDateNameStr { get; set; }



        [Display(Name = "BrPhone")]
        [DataType(DataType.Date)]
        public string BrPhone { get; set; }



        [Display(Name = "Գրավատու")]
        public string ClientName { get; set; }

        [Display(Name = "Գրավատու")]
        public string ClientFirstName { get; set; }

        [Display(Name = "Գրավատու")]
        public string ClientLastName { get; set; }



        [Display(Name = "Գրանցման Հասցե")]

        public string Address { get; set; }



        [Display(Name = "Phone")]

        public string Phone { get; set; }



        [Display(Name = "Վարկի Ընդհանուր Գումար")]
        public string CreditTotalAmount { get; set; }



        [Display(Name = "Վարկի ժամկետ")]
        public int CreditTerm { get; set; }



        [Display(Name = "Տարեկան տոկոսադրույք")]

        public string anualRate { get; set; }



        [Display(Name = "Ընտ. անդամների թիվը")]
        public string fMemb { get; set; }

        [Display(Name = "Ընտ. անչափահաս անդամների թիվը")]
        public string fTenMemb { get; set; }

        [Display(Name = "Ընտ. կին անդամների թիվը")]
        public string fWmnMemb { get; set; }




        /// <summary>
        /// ///////////////////////
        /// </summary>




        [Display(Name = "Հայտի հաստատման ամսաթիվ")]
        [DataType(DataType.Date)]
        public DateTime aprDate { get; set; }



        [Display(Name = "Քանակ")]
        public int QuantitySum { get; set; } = 0;





        [Display(Name = "Ընդ.կշիռը(գր.)")]
        public double TotalWeightWithJewelsSum { get; set; } = 0;




        [Display(Name = "Մաք.կշիռը(գր.)")]
        public double NetWeightSum { get; set; } = 0;





        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public double EstimatedTotalCostAMDSum { get; set; } = 0;

        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public string EstimatedTotalCostAMDSumStr { get; set; }





        [Display(Name = "Վարկային մասնագետ")]
        public string CreditSpecialistName { get; set; }






        [Display(Name = "Վարկ-գրավ հարաբերակցություն")]
        public string LoanCollateralRatio { get; set; }

        [Display(Name = "Արժույթ")]
        public string Currency { get; set; }

        [Display(Name = "DocVesion")]
        public string DocVesion { get; set; }
    }

    public class CreditCommiteeView
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        [Display(Name = "Հայտ")]
        public string appId { get; set; }

        [Display(Name = "Ամսաթիվ")]
        public string appDate { get; set; }

        [Display(Name = "Պրոդուկտ")]
        public string prodName { get; set; }

        [Display(Name = "Վարկային մասնագետ")]
        public string creditSpec { get; set; }

        [Display(Name = "Հայտատու")]
        public string clientFullName { get; set; }

        [Display(Name = "Արժույթ")]
        public string appCurrency { get; set; }

        [Display(Name = "Հայտի գումար")]
        public string appSum { get; set; }

        [Display(Name = "Վարկի գումար")]
        public string creditSum { get; set; }

        [Display(Name = "Հաճախորդի ներդրում")]
        public string clientInvest { get; set; }

        [Display(Name = "Պահանջվող վարկի ժամկետ")]
        public string creditDur { get; set; }

        [Display(Name = "Տարեկան անվանական տոկոսադրույք")]
        public string interest { get; set; }

        [Display(Name = "Վարկի ամսական սպասարկման վճար")]
        public string monthFee { get; set; }

        [Display(Name = "Տրման միջնորդավճար")]
        public string upfrontFee { get; set; }

        [Display(Name = "Արտոնյալ ժամանակահատված/ամիս")]
        public string gracePeriod { get; set; }

        [Display(Name = "Գրավի պահանջ")]
        public string collatReq { get; set; }

        [Display(Name = "Ապահովագրություն")]
        public string insurance { get; set; }

        [Display(Name = "Հաստատման նախապայման")]
        public string apprPreCond { get; set; }

        [Display(Name = "Վարկի նպատակ")]
        public List<creditPurpose> crPurpose { get; set; }

        [Display(Name = "Սքորինգ")]
        public List<scoringDets> scorDet { get; set; }

        [Display(Name = "Վարկի ապահովվածությունը")]
        public List<loanCollateral> coallaterals { get; set; }

        [Display(Name = "Երաշխավորություններ")]
        public List<loanGuarantor> guarantors { get; set; }

        [Display(Name = "Վարկ/գրավ հարաբերակցություն")]
        public string credSumCollateralSum { get; set; }

        [Display(Name = "Սքորինգ գնահատական")]
        public string finalSocre { get; set; }

        [Display(Name = "Օղակ 1")]
        public string app1 { get; set; }
        [Display(Name = "Օղակ 2")]
        public string app2 { get; set; }
        

        public CreditCommiteeView() { }

        public CreditCommiteeView(long applicationId) :this()
        {
            applications app = db.applications.Where(a => a.applicationId == applicationId).SingleOrDefault();

            clients client = db.clients.Where(c => c.clientId == app.clientId).SingleOrDefault();

            UserASProfiles userProf = db.UserASProfiles.Where(u => u.UserId == app.userId).SingleOrDefault();

            Products product = db.Products.Where(p => p.productId == app.productId).SingleOrDefault();

            List <Items> itms = db.Items.Where(i => i.applicationId == app.applicationId).ToList();

            ApplicationAppruves appAppruves = db.ApplicationAppruves.Where(a => a.appId == app.applicationId).OrderByDescending(o => o.ApplicationAppruvesId).Take(1).SingleOrDefault();

            ApplicationSummary appSummry = db.ApplicationSummary.Where(a => a.HaytID == app.applicationId).OrderByDescending(o => o.Id).Take(1).SingleOrDefault();

            List<GoldCollaterals> gold = db.GoldCollateral.Where(g => g.applicationId == app.applicationId).ToList();

            List<RealtyEstates> rEstate = db.RealtyEstate.Where(r => r.applicationId == app.applicationId).ToList();

            List<MovableEstates> mEstate = db.MovableEstate.Where(m => m.applicationId == app.applicationId).ToList();

            List<Guarantors> guarant = db.Guarantors.Where(g => g.applicationId == app.applicationId).ToList();

            this.appId = app.applicationId.ToString();
            this.appDate = String.Format("{0:d}", app.appDate);
            this.appCurrency = db.CurrencyTypes.Where(c => c.currencyTypesId == product.productCurrency).Select(s => s.currencyArm).SingleOrDefault();
            this.prodName = product.productName;
            this.creditSpec = userProf.FirstName + " " + userProf.LastName;
            this.clientFullName = client.clientName + " " + client.clientLastName;
            double appSum = 0;
            double clientInvest = 0;
            crPurpose = new List<creditPurpose>();
            creditPurpose cp;
            foreach(var it in itms)
            {
                cp = new ModelsView.creditPurpose();
                appSum += it.Sum;
                clientInvest += it.ClientInvest;

                cp.clientInvest = it.ClientInvest.ToString("N0");
                int prpsId = db.ProductPurposes.Where(p => p.Id == it.FKProductPurposeId).Select(s => s.Id).SingleOrDefault();
                cp.prodPurpose = db.Purposes.Where(p => p.Id == prpsId).Select(s => s.PurposeName).SingleOrDefault();
                cp.itemSum = it.Sum.ToString("N0");
                crPurpose.Add(cp);
            }//foreach(var it in itms)

            this.appSum = appSum.ToString("N0");
            this.creditSum = app.creditSum.Value.ToString("N0");
            this.clientInvest = clientInvest.ToString("N0");
            this.creditDur = app.CreditTerm.ToString("N0");//appAppruves.credDur.ToString("N0");
            this.interest = (product.anualRate * 100).ToString("N0");
            this.monthFee = product.mothFeeFlat.ToString("N0");
            this.upfrontFee = (app.creditSum.Value * product.upfronFee).ToString("N0");

            if (appAppruves.grPeriod > 0)
                this.gracePeriod = appAppruves.grPeriod.ToString("N0");

            this.collatReq = appAppruves.collateral;
            this.insurance = appAppruves.insurance;
            this.apprPreCond = appAppruves.preCondit;

            string scoreDecis = db.ScoringDecisions.Where(d => d.ID == appSummry.ScoreDecisionID).Select(s => s.Decision).SingleOrDefault();

            var scorIndicators = (from sa in db.ScoringApplicationScores
                                  join i in db.ScoringIndicators on sa.IndicatorID equals i.ID
                                  where sa.ApplicationID == app.applicationId
                                  select new
                                  {
                                      IndicatorName = i.IndicatorName,
                                      Score = sa.Score,
                                      Value = sa.Value

                                  }).ToList();
            int iterat = 0;
            scoringDets sd;
            this.scorDet = new List<ModelsView.scoringDets>();
            foreach(var si in scorIndicators)
            {
                sd = new scoringDets();
                sd.IndicatorName = si.IndicatorName;
                sd.Value = si.Value.ToString("N0");
                sd.Score = si.Score.ToString("N0");
                if(iterat == 0)
                {
                    sd.totScore = appSummry.ScoreValue.ToString("N0");
                    sd.Decision = scoreDecis;
                }
                iterat++;
                scorDet.Add(sd);
            }//foreach(var si in scorIndicators)

            this.finalSocre = scoreDecis;
            this.app1 = db.UserASProfiles.Where(u => u.UserId == appSummry.App1user).Select(s => s.FirstName + " " + s.LastName).SingleOrDefault();
            if(appSummry.Appfinaluser != appSummry.App1user)
                this.app2 = db.UserASProfiles.Where(u => u.UserId == appSummry.Appfinaluser).Select(s => s.FirstName + " " + s.LastName).SingleOrDefault();

            double collateralSum = 0;
            loanCollateral lCollat;
            coallaterals = new List<loanCollateral>();

            foreach(var gld in gold)
            {
                lCollat = new loanCollateral();
                lCollat.collateralName = db.GoldTypes.Where(t => t.Id == gld.GoldTypeId).Select(s => s.Title).SingleOrDefault() + "( հարգ " + db.GoldAssayes.Where(a=>a.Id == gld.GoldAssayId).Select(s=>s.Assay).SingleOrDefault() + ")";
                lCollat.collateralSum = gld.EstimatedTotalCostAMD.ToString("N2");
                lCollat.collateralType = "Ոսկյա իրեր";
                lCollat.collateralDesc = gld.Description;
                collateralSum += gld.EstimatedTotalCostAMD;
                this.coallaterals.Add(lCollat);
            }//foreach(var gld in gold)

            foreach(var re in rEstate)
            {
                lCollat = new loanCollateral();
                lCollat.collateralType = "Անշարժ գույք";
                lCollat.collateralName = db.RealtyTypes.Where(t => t.Id == re.RealtyTypeId).Select(s => s.Title).SingleOrDefault();
                lCollat.collateralSum = re.TotRatedPrice.ToString("N0");
                lCollat.collateralDesc = re.Address + " (Շուկայական արժեք՝ " + re.mPrice.ToString("N0") + ")";
                collateralSum += re.mPrice;
                this.coallaterals.Add(lCollat);
            }//foreach(var re in rEstate)

            foreach(var me in mEstate)
            {
                lCollat = new loanCollateral();
                lCollat.collateralType = "Շարժական գույք";
                lCollat.collateralName = db.MovableEstateTypes.Where(t => t.Id == me.MovableEstateTypeId).Select(s => s.Title).SingleOrDefault();
                lCollat.collateralSum = me.mPrice.ToString("N0");
                lCollat.collateralDesc = me.Name + ", " + me.Color + ", տարեթի՝ " + me.Year + ", վազքը՝ " + me.Run.ToString() + ", հզորություն՝ " + me.MotorPower.ToString() + ": " + me.Description;
                collateralSum += me.mPrice;
                this.coallaterals.Add(lCollat);
            }//foreach(var me in mEstate)

            this.credSumCollateralSum = ((app.creditSumAMD.Value / collateralSum) * 100).ToString("N0");

            loanGuarantor lGuarant;
            this.guarantors = new List<loanGuarantor>();
            foreach (var gr in guarant)
            {
                lGuarant = new ModelsView.loanGuarantor();
                lGuarant.guarantFullName = db.clients.Where(g => g.clientId == gr.clientId).Select(s => s.clientName + " " + s.clientLastName).SingleOrDefault();
                lGuarant.guarantIncome = db.clientWorkDatas.Where(c => c.clientId == gr.clientId).Select(s => s.salary + s.otherIncome).SingleOrDefault().ToString("N0");
                lGuarant.guarantJob = db.clientWorkDatas.Where(c => c.clientId == gr.clientId).Select(s => s.jobTitle).SingleOrDefault();
                int delayTot = db.acras.Where(a => a.clientId == gr.clientId).Select(s => s.delay_tot).SingleOrDefault();
                double overdueSum = db.acras.Where(a => a.clientId == gr.clientId).Select(s => s.totOverdueAMD).SingleOrDefault() + db.acras.Where(a => a.clientId == gr.clientId).Select(s => s.totOverdueUSD).SingleOrDefault() * CommonFunction.GetExchRate(2,DateTime.Now);
                lGuarant.guarantDelaySumAndDaysCnt = (delayTot > 0) ? delayTot.ToString() + " ուշացումներ"  : "";
                if (overdueSum > 0)
                    lGuarant.guarantDelaySumAndDaysCnt += " ժամկետանց գումար (ՀՀ դրամ) " + overdueSum.ToString("N2");

                this.guarantors.Add(lGuarant);
            }//foreach(var gr in guarant)

        }//public CreditCommiteeView(long applicationId) :this()


    }//public class CreditCommiteeView

    public class scoringDets
    {
        [Display(Name = "Չափանիշ")]
        public string IndicatorName { get; set; }

        [Display(Name = "Արժեք")]
        public string Value { get; set; }

        [Display(Name = "Գնահատական")]
        public string Score { get; set; }

        [Display(Name = "Ընդհանուր սքոր")]
        public string totScore { get; set; }

        [Display(Name = "Եզրակացություն")]
        public string Decision { get; set; }
    }

    public class creditPurpose
    {
        [Display(Name = "Վարկի նպատակը")]
        public string prodPurpose { get; set; }

        [Display(Name = "Վարկառուի միջոցներ")]
        public string clientInvest { get; set; }

        [Display(Name = "Վարկի միջոցներ")]
        public string itemSum { get; set; }

        [Display(Name = "Ընդհանուր")]
        public string InvestAndSum { get; set; }

        [Display(Name = "Ընդամենը վարկի գումար")]
        public string totItemSum { get; set; }

        [Display(Name = "Ընդամենը հաճախ. ներդրում")]
        public string totClientInvestSum { get; set; }

    }

    public class loanCollateral
    {
        [Display(Name = "Ապահովվածության տեսակ")]
        public string collateralType { get; set; }

        [Display(Name = "Գնահատված գրավի կազմը")]
        public string collateralName { get; set; }

        [Display(Name = "Նկարագրություն")]
        public string collateralDesc { get; set; }

        [Display(Name = "Գնահատված Արժեքը (ՀՀ դրամ)")]
        public string collateralSum { get; set; }

    }

    public class loanGuarantor
    {
        [Display(Name = "Անուն Ազգանուն")]
        public string guarantFullName { get; set; }

        [Display(Name = "Փոխկապակցվածություն")]
        public string guarantRelType { get; set; }

        [Display(Name = "Զբաղվածություն")]
        public string guarantJob { get; set; }

        [Display(Name = "Ամսական եկամուտ")]
        public string guarantIncome { get; set; }

        [Display(Name = "Գումար. Ժամկետանց օրերի քանակ ")]
        public string guarantDelaySumAndDaysCnt { get; set; }

        [Display(Name = "Երաշխավորի ընդհանուր սքորը")]
        public string guarantScore { get; set; }

        [Display(Name = "Երաշխավորի տիպը")]
        public string guarantType { get; set; }

    }

    

}