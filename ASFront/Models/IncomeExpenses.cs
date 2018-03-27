using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class IncomeExpenses
    {
       
        [Key]
        [Display(Name = "ID")]
        public long Id { get; set; }


        [Display(Name = "Հաճախորդի ID")]
        public long clientId { get; set; }



        [Required]
        [ForeignKey("applications")]

        [Display(Name = "Հայտ")]
        public long applicationId { get; set; }


        [Required]
        [Display(Name = "Ամսաթիվ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; } = DateTime.Now;



        [Required]
        [Display(Name = "Հաճախորդի աշխատավարձ")]
        public double ClientsWage { get; set; } = 0;


        [Required]
        [Display(Name = "Վարկային/Տոկոսային ծախսեր")]
        public double LoanInterestExpenses { get; set; } = 0;


        [Required]
        [Display(Name = "Ընտանիքի Վարկային/Տոկոսային ծախսեր")]
        public double FamilysLoanInterestExpenses { get; set; } = 0;



        [Display(Name = "Նշում: Ե/Ծ-ի հետ կապված")]
        public string note1 { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }


        /// <summary>
        /// /2
        /// </summary>



        [Required]

        [Display(Name = "Հասույթ")]
        public double Revenue { get; set; } = 0;

        [Required]
        [Display(Name = "Ինքնարժեք")]
        public double CostOfGoodsSold { get; set; } = 0;



        [Required]
        [Display(Name = "Վարձավճար")]
        public double Rent { get; set; } = 0;

        [Required]
        [Display(Name = "Տրանսպորտային ծախսեր")]
        public double TransportationCosts { get; set; } = 0;

        [Required]
        [Display(Name = "Կոմունալ ծախսեր")]
        public double UtilityExpenses { get; set; } = 0;


        [Required]
        [Display(Name = "Աշխատավարձ")]
        public double Wage { get; set; } = 0;

        [Required]
        [Display(Name = "Այլ ծախսեր")]
        public double OtherExpenses { get; set; } = 0;


        [Required]
        [Display(Name = "Հարկեր")]
        public double Taxes { get; set; } = 0;

        /// <summary>
        /// //////3
        /// </summary>


        [Required]
        [Display(Name = "Գյուղատնտեսական եկամուտներ")]
        public double AgroIncome { get; set; } = 0;

        [Required]
        [Display(Name = "Գյուղատնտեսական ծախսեր")]
        public double AgroExpenses { get; set; } = 0;




        /// <summary>
        /// //////// 4
        /// </summary>

        [Required]
        [Display(Name = "Ընտանիքի անդամների աշխատավարձեր")]
        public double FamilyMembersWages { get; set; } = 0;


        [Required]
        [Display(Name = "Ընտանեկան այլ եկամուտներ")]
        public double OtherFamilyIncome { get; set; } = 0;

        [Required]
        [Display(Name = "Ընտանեկան ծախսեր")]
        public double FamilyExpenses { get; set; } = 0;




        public virtual applications applications { get; set; }

        public virtual clients clients { get; set; }

    }
}