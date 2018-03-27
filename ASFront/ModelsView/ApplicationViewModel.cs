using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace ASFront.ModelsView
{
    public class ApplicationViewModel
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public applications()
        //{
        //    ScoringApplicationScores = new HashSet<ScoringApplicationScores>();
        //}

        [Key]
        [Display(Name = "Հայտ ID")]  
        public long applicationId  { get; set; }




        [Display(Name = "Հայտի ամսաթիվ")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime appDate  { get; set; }  = DateTime.Now;


        [Display(Name = "Հայտը մուտքագրող")]
        public string userId { get; set; }



        [Display(Name = "Հայտը մուտքագրող")]
        public string userName { get; set; }


        [Display(Name = "Հաճախորդ")]
        public long clientId  { get; set; }


        [Display(Name = "Հաճախորդ")]
        public string clientName { get; set; }


        [Display(Name = "Մասնաճյուղ")]
        public int branchId  { get; set; }


        [Display(Name = "Մասնաճյուղ")]
        public string branchName { get; set; }


        [Display(Name = "Մատակարար")]
        public string SupplierName { get; set; }


        [Display(Name = "Վաճառող")]
        public int? SellerId { get; set; }

        [Display(Name = "Վաճառող")]
        public string SellerName { get; set; }


        [Display(Name = "Հայտի կարգավիճակը")]
        public int appStatus  { get; set; }

         [Display(Name = "Հայտի կարգավիճակը")]
        public string appStatusName  { get; set; }

        [Display(Name = "Հաստատող")]
        public string appuserId  { get; set; }


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


        [Display(Name = "Պրոդուկտ")]
        public string productName { get; set; }

        [Display(Name = "Վարկի Ժամկետ")]
        public int CreditTerm { get; set; }


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



        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ScoringApplicationScores> ScoringApplicationScores { get; set; }
    }
}