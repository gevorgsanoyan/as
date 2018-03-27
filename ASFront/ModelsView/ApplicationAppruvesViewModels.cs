using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ASFront.Models;

namespace ASFront.ModelsView
{
    public class ApplicationAppruvesViewModels
    {
        //[Display(Name = "ID")]
        //public long ApplicationAppruvesId { get; set; }

        //[Display(Name = "Հայտ")]
        //public long appId { get; set; }
        private ApplicationDbContext db = new Models.ApplicationDbContext();

        public ApplicationAppruvesViewModels() { }
        public ApplicationAppruvesViewModels(ApplicationAppruves item) : this()
        {

            if (item.appDate!=null)
            this.appDate = item.appDate?.ToShortDateString();

           


            this.note1 = item.note1;
            this.note2 = item.note2;
            this.note3 = item.note3;



            this.preCondit = item.preCondit;
            this.insurance = item.insurance;
            this.collateral = item.collateral;



            if (item.appUserId != null)             

            this.appUser = db.UserASProfiles.Where(p => p.UserId == item.appUserId).Select(p => p.FirstName + " " + p.LastName).FirstOrDefault();


            this.aprStatus = db.appStatus.Where(p => p.appStatusId == item.aprStatus).Select(p => p.appStatusArm).FirstOrDefault();





            this.appSum = item.appSum.ToString("N0");
            this.creditSum = item.creditSum.ToString("N0");
            this.credDur = item.credDur.ToString("N0");
            this.grPeriod = item.grPeriod.ToString("N0");
            this.clientInvest = (item.appSum - item.creditSum).ToString("N0");

            if(item.appSum > item.creditSum)
            {
                var appItems = db.Items.Where(a => a.applicationId == item.appId).Take(1).SingleOrDefault();
                appItems.ClientInvest = float.Parse(this.clientInvest);
                db.SaveChanges();
            }//if(item.appSum > item.creditSum)

            int prodId = db.applications.Where(a => a.applicationId == item.appId).Select(p => p.productId).SingleOrDefault();
            int currId = db.Products.Where(p => p.productId == prodId).Select(c => c.productCurrency).SingleOrDefault();
            this.appCurrency = db.CurrencyTypes.Where(c => c.currencyTypesId == currId).Select(s => s.currencyArm).SingleOrDefault();


        }



        [Display(Name = "Հաստատող")]
        public string appUser { get; set; }

        [Display(Name = "Հաստման ամսաթիվ")]
    
        public string appDate { get; set; }

        [Display(Name = "Հաստատման տիպ")]
        public string aprStatus { get; set; }

        [Display(Name = "Արժույթ")]
        public string appCurrency { get; set; }

        [Display(Name = "Հայտի գումար")]
        public string appSum { get; set; }

        [Display(Name = "Վարկի գումար")]
        public string creditSum { get; set; }

        [Display(Name = "Հաճախորդի ներդրում")]
        public string clientInvest { get; set; }

        [Display(Name = "Վարկի ժամկետ")]
        public string credDur { get; set; }

        [Display(Name = "Արտոնյալ ժամկետ")]
        public string grPeriod { get; set; }

        [Display(Name = "Հաստատման նախապայման")]
        public string preCondit { get; set; }

        [Display(Name = "Ապահովագրություն")]
        public string insurance { get; set; }

        [Display(Name = "Գրավի պահանջ")]
        public string collateral { get; set; }

        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }
        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }
        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }
    }
}