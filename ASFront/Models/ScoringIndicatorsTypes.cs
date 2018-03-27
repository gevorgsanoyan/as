using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class ScoringIndicatorsTypes
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public ScoringIndicatorsTypes()
        //{
        //    ScoringIndicators = new HashSet<ScoringIndicators>();
        //}

        [Key]
        public int ID { get; set; }

      
        [Display(Name = "Տիպի անուն")]
        public string TypeName { get; set; }


        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ScoringIndicators> ScoringIndicators { get; set; }


    }
}