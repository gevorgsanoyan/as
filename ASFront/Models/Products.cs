using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    

    public class Products
    {
        public Products()
        {
            ProductLimits = new HashSet<ProductLimits>();
        }


        //public Products()
        //{
        //    ScoringProductIndicators = new HashSet<ScoringProductIndicators>();
        //    ScoringScoreDecisions = new HashSet<ScoringScoreDecisions>();
        //}

        [Key]
        [Display(Name = "Պրոդուկտի ID")]
        public int productId { get; set; }

        [Display(Name = "Պրոդուկտի խումբ")]
        public int productGroupId { get; set; }


        [Display(Name = "Պրոդուկտի անվանում")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string productName { get; set; }


        [Display(Name = "Պրոդուկտի հապավում")]
        public string productShortName { get; set; }


        [Display(Name = "ՀԾ ձևանմուշ")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string agrType { get; set; }


        [Display(Name = "Նվազագույն տևողություն")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public int minMaturtity { get; set; }


        [Display(Name = "Վարկի նվազագույն գումար")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public double minAmount { get; set; }


        [Display(Name = "Վարկի առավելագույն գումար ")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public double maxAmount  { get; set; }




        [Display(Name = "Առավելագույն տևողություն")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public int maxMaturity { get; set; }


        [Display(Name = "Դիմումի վճար")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public float appFee { get; set; }


        [Display(Name = "Տրման միջնորդավճար")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public float upfronFee { get; set; }


        [Display(Name = "Ամսեկան սպասարկման վճար")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public float mothFee { get; set; }


        [Display(Name = "Ամսեկան հաստատուն վճար")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public float mothFeeFlat { get; set; }


        [Display(Name = "Տարեկան տոկոսադրույք")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public float anualRate { get; set; }



        [Display(Name = "ՀԾ Նշում1")]
        public string asNote1 { get; set; }


        [Display(Name = "ՀԾ Նշում2")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string asNote2 { get; set; }


        [Display(Name = "ՀԾ Բաժին")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string asSector { get; set; }



        [Display(Name = "Արժույթը")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public int productCurrency { get; set; }


        [Display(Name = "Ստատուս")]
        public bool prodStatus { get; set; }

        
        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում3")]
        public string note3 { get; set; }


        [Display(Name = "Նշում4")]
        public string note4 { get; set; }



        [Display(Name = "Մուտքագրող օգտատեր")]
        public string userId { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastModifDate { get; set; } = DateTime.Now;


        public virtual productGroups productGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductLimits> ProductLimits { get; set; }


        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ScoringProductIndicators> ScoringProductIndicators { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ScoringScoreDecisions> ScoringScoreDecisions { get; set; }
    }

}