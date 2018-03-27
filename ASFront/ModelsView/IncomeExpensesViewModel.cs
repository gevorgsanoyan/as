using ASFront.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class IncomeExpensesViewModel
    {


        public IncomeExpensesViewModel() { }


        public IncomeExpensesViewModel(IncomeExpenses item) : this()
        {


            this.Id = item.Id;
            this.clientId = item.clientId;
            this.Date = item.Date.ToShortDateString();
            this.note1 = item.note1;
            this.note2 = item.note2;
            this.note3 = item.note3;

            this.ClientsWage = item.ClientsWage.ToString("N0");
            this.LoanInterestExpenses = item.LoanInterestExpenses.ToString("N0");
            this.FamilysLoanInterestExpenses = item.FamilysLoanInterestExpenses.ToString("N0");
            this.Revenue = item.Revenue.ToString("N0");
            this.CostOfGoodsSold = item.CostOfGoodsSold.ToString("N0");


            this.Rent = item.Rent.ToString("N0");
            this.TransportationCosts = item.TransportationCosts.ToString("N0");
            this.UtilityExpenses = item.UtilityExpenses.ToString("N0");
            this.Wage = (item.Wage).ToString("N0");
            this.OtherExpenses = item.OtherExpenses.ToString("N0");







            this.Taxes = item.Taxes.ToString("N0");
            this.AgroIncome = item.AgroIncome.ToString("N0");

            this.AgroExpenses = item.AgroExpenses.ToString("N0");





            this.FamilyMembersWages = item.FamilyMembersWages.ToString("N0");
            this.OtherFamilyIncome = item.OtherFamilyIncome.ToString("N0");
            this.FamilyExpenses = item.FamilyExpenses.ToString("N0");



            /////////////////////////

            ///////////////////////



            double gross_profitNum = (item.Revenue - item.CostOfGoodsSold);
            double OvergrownNum = 0;
         

            if (item.CostOfGoodsSold != 0)
            {
                OvergrownNum = item.Revenue / item.CostOfGoodsSold;
                this.OvergrownNum = OvergrownNum;
            }
                



            double net_profitNum = gross_profitNum - item.Rent - item.TransportationCosts - item.UtilityExpenses - item.Wage - item.OtherExpenses - item.Taxes;

            double net_incomeNum = net_profitNum + item.FamilyMembersWages + item.OtherFamilyIncome - item.FamilyExpenses - item.LoanInterestExpenses - item.FamilysLoanInterestExpenses;

            double CoveringFactorNum = 0;

            if((item.LoanInterestExpenses + item.FamilysLoanInterestExpenses) !=0)
            CoveringFactorNum = (net_incomeNum + item.LoanInterestExpenses + item.FamilysLoanInterestExpenses)/(item.LoanInterestExpenses + item.FamilysLoanInterestExpenses);


            this.gross_profit = gross_profitNum.ToString("N0");
            this.Overgrown = OvergrownNum.ToString("N2");

            this.net_profit = net_profitNum.ToString("N0");
            this.net_income = net_incomeNum.ToString("N0");
            this.CoveringFactor = CoveringFactorNum.ToString("N2");





        }



        [Key]
        [Display(Name = "ID")]
        public long Id { get; set; } = 0;


        [Display(Name = "Հաճախորդի ID")]
        public long clientId { get; set; }

        /// <summary>
        /// //1
        /// </summary>
        /// 



        [Display(Name = "Ամսաթիվ")]

        public string Date { get; set; }




        [Display(Name = "Հաճախորդի աշխատավարձ")]
        public string ClientsWage { get; set; }



        [Display(Name = "Վարկային/Տոկոսային ծախսեր")]
        public string LoanInterestExpenses { get; set; }



        [Display(Name = "Ընտանիքի Վարկային/Տոկոսային ծախսեր")]
        public string FamilysLoanInterestExpenses { get; set; }



        [Display(Name = "Նշում: Ե/Ծ-ի հետ կապված")]
        public string note1 { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }


        /// <summary>
        /// /2
        /// </summary>





        [Display(Name = "Հասույթ")]
        public string Revenue { get; set; }


        [Display(Name = "Ինքնարժեք")]
        public string CostOfGoodsSold { get; set; }
          



        [Display(Name = "Վարձավճար")]
        public string Rent { get; set; }


        [Display(Name = "Տրանսպորտային ծախսեր")]
        public string TransportationCosts { get; set; }


        [Display(Name = "Կոմունալ ծախսեր")]
        public string UtilityExpenses { get; set; }



        [Display(Name = "Աշխատավարձ")]
        public string Wage { get; set; }


        [Display(Name = "Այլ ծախսեր")]
        public string OtherExpenses { get; set; }



        [Display(Name = "Հարկեր")]
        public string Taxes { get; set; }

        /// <summary>
        /// //////3
        /// </summary>



        [Display(Name = "Գյուղատնտեսական եկամուտներ")]
        public string AgroIncome { get; set; }


        [Display(Name = "Գյուղատնտեսական ծախսեր")]
        public string AgroExpenses { get; set; }




        /// <summary>
        /// //////// 4
        /// </summary>


        [Display(Name = "Ընտանիքի անդամների աշխատավարձեր")]
        public string FamilyMembersWages { get; set; }



        [Display(Name = "Ընտանեկան այլ եկամուտներ")]
        public string OtherFamilyIncome { get; set; }


        [Display(Name = "Ընտանեկան ծախսեր")]
        public string FamilyExpenses { get; set; }



        /// <summary>
        /// ////////
        /// </summary>
        /// 







        [Display(Name = "Համախառն շահույթ")]
        public string gross_profit { get; set; }



        [Display(Name = "Վերադիր")]
        public string Overgrown { get; set; }


        [Display(Name = "Զուտ շահույթ")]
        public string net_profit { get; set; }



        [Display(Name = "Տիրապետվող գումար")]
        public string net_income { get; set; }




        [Display(Name = "Ծածկման գործակից")]
        public string CoveringFactor { get; set; }













        [Display(Name = "Համախառն շահույթ")]
        public double gross_profitNum { get; set; } = 0;



        [Display(Name = "Վերադիր")]
        public double OvergrownNum { get; set; } = 0;


        [Display(Name = "Զուտ շահույթ")]
        public double net_profitNum { get; set; } = 0;



        [Display(Name = "Տիրապետվող գումար")]
        public double net_incomeNum { get; set; } = 0;




        [Display(Name = "Ծածկման գործակից")]
        public double CoveringFactorNum { get; set; } = 0;












    }
}