using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class BusinessViewModel
    {
        [Key]

        [Display(Name = "ID")]
        public long clientId { get; set; }



        

        [Display(Name = "Մասնաճյուղ")]
        public int BranchtId { get; set; } = 1;


        [Display(Name = "Անվանում")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string clientName { get; set; }



        //[Display(Name = "Հաճախորդի ազգանուն")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        //public string clientLastName { get; set; }


        //[Display(Name = "Հաճախորդի հայրանուն")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        //public string clientMidName { get; set; }



        [Display(Name = "Գրանցման տարեթիվ")]
        //[Range(typeof(DateTime), "1/01/1900", "1/01/2060")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime dob { get; set; } = DateTime.Today.Date;


        [StringLength(250)]
        [Display(Name = "Պետ. Ռեգ. Կոդ")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string passpNumb { get; set; }


        //[Display(Name = "Անձնագրի տրման ամսաթիվ")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //public DateTime passpDate { get; set; } = DateTime.Today.Date;


        //[Display(Name = "Ում կողմից է տրվել")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        //public string passpAuth { get; set; }


        [StringLength(250)]
        [Display(Name = "ՀՎՀՀ")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        //[MaxLength(10, ErrorMessage = "առավելագույնը 10 նիշ է")]

        public string socNumb { get; set; }



        [Display(Name = "Մարզ")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string rRegion { get; set; }



        [Display(Name = "Քաղաք/Գյուղ/Համայնք")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string rCity { get; set; }


        [Display(Name = "Փողոց")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string rStreet { get; set; }


        [Display(Name = "Շենք")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string rBuilding { get; set; }


        [Display(Name = "Բնակարան")]
        public string rApartment { get; set; }


        [Display(Name = "Մարզ")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string cRegion { get; set; }


        [Display(Name = "Քաղաք/Գյուղ/Համայնք")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string cCity { get; set; }


        [Display(Name = "Փողոց")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string cStreet { get; set; }


        [Display(Name = "Շենք")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string cBuilding { get; set; }


        [Display(Name = "Բնակարան")]
        public string cApartment { get; set; }


        [Display(Name = "Գրանցման և Գործունեության հասցեները համընկնում են")]
        public bool isSameAddress { get; set; }


        //[Display(Name = "Բնակվում է վարձով")]
        //public bool isRented { get; set; }


        [Display(Name = "Հեռ.")]
        public string tel { get; set; }


        [Display(Name = "Բջջային Հեռ.")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string mob1 { get; set; }


        //[Display(Name = "Բջջ. 2")]
        //public string mob2 { get; set; }


        //[Display(Name = "Բջջ. 3")]
        //public string mob3 { get; set; }


        //[Display(Name = "Բջջ. 4")]
        //public string mob4 { get; set; }





        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "EmailFormat")]

        [StringLength(256)]
        [Display(Name = "Էլ. փոստ")]
        public string Email { get; set; }

        [Display(Name = "Ընդհանուր աշխատողների քանակ")]
        public int fMemb { get; set; }


        [Display(Name = "Կես դրույք աշխատողների քանակ")]
        public int fEmpMemb { get; set; }


        [Display(Name = "Կին աշխատողների քանակ")]
        public int fTenMemb { get; set; }


        //[Display(Name = "Նշում 1")]
        //public string note1 { get; set; }
        //[Display(Name = "Նշում 2")]
        //public string note2 { get; set; }


        //[Display(Name = "Նշում 3")]
        //public string note3 { get; set; }


        //[Display(Name = "Ռեգիոնի կոդ")]
        //public int? regionId { get; set; }

        //[Display(Name = "Սեռը")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        //public int sex_clientSexId { get; set; }


        ////public virtual clientSexes sex  { get; set; }


        //[Display(Name = "Կրթությունը")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        //public int edu_educationId { get; set; }

    }
}