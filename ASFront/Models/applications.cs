using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace ASFront.Models
{
    public class applications
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public applications()
        {

            Guarantors = new HashSet<Guarantors>();
            //DocsApllications = new HashSet<DocsApllications>();


            MovableEstates = new HashSet<MovableEstates>();
            RealtyEstates = new HashSet<RealtyEstates>();
            GoldCollaterals = new HashSet<GoldCollaterals>();

            Turnovers = new HashSet<Turnovers>();


            IncomeExpenses = new HashSet<IncomeExpenses>();
            Balance = new HashSet<Balance>();
        }




        [Key]
        [Display(Name = "Հայտ ID")]  
        public long applicationId  { get; set; }




        [Display(Name = "Հայտի ամսաթիվ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime appDate  { get; set; }  = DateTime.Now;


        [Display(Name = "Հայտը մուտքագրող")]
        public string userId { get; set; } 


        [Display(Name = "Հաճախորդ")]
        public long clientId  { get; set; }


        [Display(Name = "Մասնաճյուղ")]
        public int branchId  { get; set; }



        [Required]

        [Display(Name = "Վաճառող")]
        public int SellerId { get; set; }


        [Display(Name = "Հայտի կարգավիճակը")]
        public int appStatus { get; set; } = 1;



        [Display(Name = "Հաստատող")]
        public int appuserId  { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Հաստատման մանրամասներ")]
        public string appDescr  { get; set; }




        [Display(Name = "Հայտի հաստատման ամսաթիվ")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime aprDate  { get; set; } = DateTime.Now;
        

        [Display(Name = "Պայմանագրի համար")]
        public string agrNumb  { get; set; }


        [Display(Name = "Պայմանագրի թղթային համար")]
        public string agrNumbP  { get; set; }

         [Display(Name = "Պրոդուկտ")]
        public int productId  { get; set; }



        [Display(Name = "Վարկի Ժամկետ")]
        public int CreditTerm { get; set; } = 1;


        [Display(Name = "Նշում 1")]
        public string note1  { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2  { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3  { get; set; }


        [Display(Name = "Նշում 4")]
        public string note4  { get; set; }


        [Display(Name = "Նշում 5")]
        public string note5  { get; set; }


        [Display(Name = "Վարկի գումար")]
        public double? creditSum { get; set; }

        [Display(Name = "Վարկի գումար (ՀՀ դրամ)")]
        public double? creditSumAMD { get; set; }




        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DocsApllications> DocsApllications { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Guarantors> Guarantors { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovableEstates> MovableEstates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RealtyEstates> RealtyEstates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoldCollaterals> GoldCollaterals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Turnovers> Turnovers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Balance> Balance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IncomeExpenses> IncomeExpenses { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ScoringApplicationScores> ScoringApplicationScores { get; set; }
    }

    public class ApplicationsForApprove
    {
        [Display(Name = "ID")]
        public long ApplicationsForApproveId { get; set; }
        [Display(Name = "Հայտ")]
        public long appId { get; set; }
        [Display(Name = "Հաստատող")]
        public string appUserId { get; set; }
        [Display(Name = "Ուղարկող")]
        public string sendUserId { get; set; }
        [Display(Name = "Ուղարկման ամսաթիվ")]
        public DateTime? sendDate { get; set; }
        [Display(Name = "Հաստատման ամսաթիվ")]
        public DateTime? apprDate { get; set; }
        [Display(Name = "Կարգավիճակ")]
        public string apprStatus { get; set; }

        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }
        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }
        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }

    }


}