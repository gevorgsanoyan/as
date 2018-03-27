using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    public class clientWorkDatas
    {
        [Key]
        [Display(Name = "Id")]
        public long Id { get; set; }



        [Required]
        [ForeignKey("clients")]

        [Display(Name = "Հաճախորդ")]
        public long clientId { get; set; }



        [Display(Name = "Կազմակերպության անվանում")]
        public string companyName { get; set; }



        [Display(Name = "ՀՎՀՀ")]
        public string taxNumber { get; set; }



        [Display(Name = "Կազմակերպության հեռախոսահամարը")]
        public string CompanyTel { get; set; }



        [Display(Name = "Կազմակերպության հասցեն")]
        public string companyAddress { get; set; }



        [Display(Name = "Զբաղվածության ոլորտը")]
        public int? employmentTypeId { get; set; }



        [Display(Name = "Կազմկաերպության տիպը")]
        public int? companyTypeId { get; set; }



        //[Range(typeof(DateTime), "1-01-1900", "1-01-2060")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        [Display(Name = "Աշխատանքի ընդունվելու ամսաթիվը")]
        public DateTime? empRegDate { get; set; }


        //[Display(Name = "Զբաղեցրած պաշտոնը տվյալ կազմակերպությունում")]
        [Display(Name = "Զբաղեցրած պաշտոնը")]
        public string jobTitle { get; set; }


        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [Display(Name = "Աշխատավարձի չափը ")]
        public double salary { get; set; } = 0;


        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [Display(Name = "Այլ եկամուտներ")]
        public double otherIncome { get; set; } = 0;



        [Display(Name = "Այլ եկամուտների նկարագորությունը")]
        public string otherIncomeDescr { get; set; }







        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }




        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }




        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }




        [Display(Name = "Նշում 4")]
        public string note4 { get; set; }




        [Display(Name = "Նշում 5")]
        public string note5 { get; set; }





        [Display(Name = "Մուտքագրող օգտատեր")]
        public string userId { get; set; }



        public DateTime? CreatedDate { get; set; } = DateTime.Now;



        public DateTime? LastModifDate { get; set; } = DateTime.Now;


        public virtual clients clients { get; set; }

    }


}