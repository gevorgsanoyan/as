using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class ScoringIndicators
    {


        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public ScoringIndicators()
        //{
        //    ScoringApplicationScores = new HashSet<ScoringApplicationScores>();
        //    ScoringIndicatorsParameters = new HashSet<ScoringIndicatorsParameters>();
        //    ScoringProductIndicators = new HashSet<ScoringProductIndicators>();
        //    ScoringScores = new HashSet<ScoringScores>();
        //}

        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Ցուցիչի անվանումը")]
        public string IndicatorName { get; set; }



        [Display(Name = "Ցուցիչի տեսակը")]
        public int IndicatorType { get; set; }


        //[Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        //[UIHint("Number")]
        [Display(Name = "Ցուցիչի արժեքը")]
        public double IndicatorValue { get; set; } = 0;


        [Display(Name = "Ֆորմուլա")]
        public string FormulaText { get; set; }


        [Display(Name = "Ֆորմուլա ուղղված ըստ առաջնահերթության")]
        public string FormulaTextPriorityFixed { get; set; }




        [Display(Name = "Մուտքագրող օգտատեր")]
        public string userId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastModifDate { get; set; } = DateTime.Now;





        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ScoringApplicationScores> ScoringApplicationScores { get; set; }

        //public virtual ScoringIndicatorsTypes ScoringIndicatorsTypes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ScoringIndicatorsParameters> ScoringIndicatorsParameters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ScoringProductIndicators> ScoringProductIndicators { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ScoringScores> ScoringScores { get; set; }
    }
}
