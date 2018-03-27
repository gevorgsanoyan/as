using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class Balance
    {

       
        [Key]
        [Display(Name = "ID")]
        public long Id { get; set; }


        [ForeignKey("clients")]
        [Display(Name = "Հաճախորդի ID")]
        public long clientId { get; set; }



        [Required]
        [ForeignKey("applications")]

        [Display(Name = "Հայտ")]
        public long applicationId { get; set; }


        /// <summary>
        /// ////////////  1
        /// </summary>
        [Required]
        [Display(Name = "Կազմման ամսաթիվ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]

        public DateTime PreparationDate { get; set; } = DateTime.Now;


        [Display(Name = "Նշում: Հաշվեկշռի հետ կապված")]
        public string note1 { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }




        /// <summary>
        /// /////// 2
        /// </summary>


        [Required]
        [Display(Name = "Դրամարկղ")]
        public double Cash { get; set; } = 0;


        [Required]
        [Display(Name = "Հաշվարկային հաշիվ")]
        public double BankAccount { get; set; } = 0;

        [Required]
        [Display(Name = "Դեբիտորական պարտք")]
        public double AccountReceivable { get; set; } = 0;

        [Required]
        [Display(Name = "ԱՆՊ")]
        public double Inventory { get; set; } = 0;

        [Required]
        [Display(Name = "Այլ ընթացիկ ակտիվներ")]
        public double OtherCurrentAssets { get; set; } = 0;



        /// <summary>
        /// ////////////3
        /// </summary>
        [Required]
        [Display(Name = "Սարքավորումներ")]
        public double Equipments { get; set; }  = 0;

        [Required]
        [Display(Name = "Շարժական գույք")]
        public double Vehicles { get; set; } = 0;

        [Required]
        [Display(Name = "Անշարժ գույք")]
        public double PropertyPlantes { get; set; } = 0;



        [Required]
        [Display(Name = "Այլ հիմնական միջոցներ")]
        public double OtherNonCurrentAssets { get; set; } = 0;

        /// <summary>
        /// ////////// 4
        /// </summary>

        [Required]

        [Display(Name = "Կարճաժամկետ վարկեր")]
        public double ShortTermLoans { get; set; } = 0;

        [Required]
        [Display(Name = "Կրեդիտորական պարտք")]
        public double AccountPayable { get; set; } = 0;




        [Required]
        [Display(Name = "Այլ ընթացիկ պարտավորություններ")]
        public double OtherCurrentLiabilities { get; set; } = 0;

        /// <summary>
        /// ////////// 5
        /// </summary>

        [Required]
        [Display(Name = "Միջին-Երկար վարկեր")]
        public double MediumLongTermLoans { get; set; } = 0;


        [Required]
        [Display(Name = "Այլ ոչ ընթացիկ պարտավորություններ")]
        public double OtherNonCurrentLiabilitiues { get; set; } = 0;





        public virtual applications applications { get; set; }

        public virtual clients clients { get; set; }


    }
}