using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class ScoringProductIndicatorsView
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }



      
        [Display(Name = "Ցուցիչ")]
        public string IndicatorName { get; set; }


       
        [Display(Name = "Պրոդուկտ")]
        public string ProductName { get; set; }


        //public virtual Products Products { get; set; }

        //public virtual ScoringIndicators ScoringIndicators { get; set; }

    }
}