using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{



    public class clients
    {


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public clients()
        {
          
            IncomeExpenses = new HashSet<IncomeExpenses>();
            Balance = new HashSet<Balance>();
            Guarantors = new HashSet<Guarantors>();
            clientWorkDatas = new HashSet<clientWorkDatas>();

            BusinessInfo = new HashSet<BusinessInfo>();

         
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Balance> Balance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IncomeExpenses> IncomeExpenses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessInfo> BusinessInfo { get; set; }


        [Key]

        [Display(Name = "ID")]
        public long clientId { get; set; }



        [ForeignKey("Branches")]

        [Display(Name = "Մասնաճյուղ")]
        public int BranchtId { get; set; } = 1;

        [Display(Name = "Հաճախորդի անուն")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string clientName { get; set; }



        [Display(Name = "Հաճախորդի ազգանուն")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string clientLastName { get; set; }


        [Display(Name = "Հաճախորդի հայրանուն")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string clientMidName { get; set; }



        [Display(Name = "Ծննդ. ամսաթիվ")]
        //[Range(typeof(DateTime), "1/01/1900", "1/01/2060")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime dob { get; set; } = DateTime.Today.Date;


        [StringLength(250)]
        [Display(Name = "Անձնագրի համար")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string passpNumb { get; set; }


        [Display(Name = "Անձնագրի տրման ամսաթիվ")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime passpDate { get; set; } = DateTime.Today.Date;


        [Display(Name = "Ում կողմից է տրվել")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string passpAuth { get; set; }


        [StringLength(250)]
        [Display(Name = "Սոց. քարտ")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [MaxLength(10, ErrorMessage = "առավելագույնը 10 նիշ է")]

        public string socNumb { get; set; }



        [Display(Name = "Գրանցման մարզ")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string rRegion { get; set; }



        [Display(Name = "Գրանցման քաղաք")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string rCity { get; set; }


        [Display(Name = "Գրանցման փողոց")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string rStreet { get; set; }


        [Display(Name = "Գրանցման շենք")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string rBuilding { get; set; }


        [Display(Name = "Գրանցման բնակարան")]
        public string rApartment { get; set; }


        [Display(Name = "Փաստացի մարզ")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string cRegion { get; set; }


        [Display(Name = "Փաստացի քաղաք")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string cCity { get; set; }


        [Display(Name = "Փաստացի փողոց")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string cStreet { get; set; }


        [Display(Name = "Փաստացի շենք")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string cBuilding { get; set; }


        [Display(Name = "Փաստացի բնակարան")]
        public string cApartment { get; set; }


        [Display(Name = "Փաստացի և գրանցման հասցեները համընկնում են")]
        public bool isSameAddress { get; set; }


        [Display(Name = "Բնակվում է վարձով")]
        public bool isRented { get; set; }


        [Display(Name = "Հեռ.")]
        public string tel { get; set; }


        [Display(Name = "Բջջ. 1")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string mob1 { get; set; }


        [Display(Name = "Բջջ. 2")]
        public string mob2 { get; set; }


        [Display(Name = "Բջջ. 3")]
        public string mob3 { get; set; }


        [Display(Name = "Բջջ. 4")]
        public string mob4 { get; set; }




     
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "EmailFormat")]     

        [StringLength(256)]
        [Display(Name = "Էլ. փոստ")]
        public string Email { get; set; }

        [Display(Name = "Ընտ. անդամների թիվը")]
        public int fMemb { get; set; }


        [Display(Name = "Ընտ. աշխատող անդամների թիվը")]
        public int fEmpMemb { get; set; }


        [Display(Name = "Ընտ. անչափահաս անդամների թիվը")]
        public int fTenMemb { get; set; }


        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }
        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }


        [Display(Name = "Ռեգիոնի կոդ")]
        public int? regionId { get; set; }

        [Display(Name = "Սեռը")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public int sex_clientSexId { get; set; }


        //public virtual clientSexes sex  { get; set; }


        [Display(Name = "Կրթությունը")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public int edu_educationId { get; set; }

        public virtual clientSexes clientSexes { get; set; }

        public virtual educations educations { get; set; }


        public virtual Branches Branches { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Guarantors> Guarantors { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<clientWorkDatas> clientWorkDatas { get; set; }






    }


}