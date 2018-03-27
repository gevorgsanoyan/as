using ASFront.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


using System;

using System.Linq;




namespace ASFront.ModelsView
{
    public class TurnoverAppSumViewModels
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public TurnoverAppSumViewModels() { }

        public TurnoverAppSumViewModels(long ApplicationID = 0) : this()
        {
            var turnovers = db.Turnovers.Where(p => p.applicationId == ApplicationID).ToList() ?? new  List<Turnovers>();

            int TotalSalesPerMonth = turnovers.Sum(p => p.MonthlySalesQuantity);
            double TotalEarnings = turnovers.Sum(p => p.Proceeds);
            double TotalRemaining = turnovers.Sum(p => p.OutstandingQuantity);
            double TotalMoneyBalance = turnovers.Sum(p => p.MoneyBalance);
            double AverageWeightedUpward = 0;


            TurnoverTableViewModels tr = new TurnoverTableViewModels(ApplicationID);
            AverageWeightedUpward = tr.AverageWeightedUpward;

            this.TotalSalesPerMonth = TotalSalesPerMonth.ToString("N0");
            this.TotalEarnings = TotalEarnings.ToString("N0");
            this.TotalRemaining = TotalRemaining.ToString("N0");
            this.TotalMoneyBalance = TotalMoneyBalance.ToString("N0");
            this.AverageWeightedUpward = AverageWeightedUpward.ToString("N0") + "%";


        }




        



        [Display(Name = "Ընդամենը Ամս. վաճառքի քանակ (հատ)")]
        public string TotalSalesPerMonth { get; set; } = "0";


        [Display(Name = "Ընդամենը հասույթ (ՀՀ դրամ)")]
        public string TotalEarnings { get; set; } = "0";




        [Display(Name = "Ընդամենը մնացորդ (հատ)")]
        public string TotalRemaining { get; set; } = "0";


        [Display(Name = "Ընդամենը գումար-մնացորդ (ՀՀ դրամ)")]
        public string TotalMoneyBalance { get; set; } = "0";



        [Display(Name = "Միջին կշռված վերադիր, %")]
        public string AverageWeightedUpward { get; set; } = "0%";



    }


}