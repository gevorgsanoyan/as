using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class ScoringProductIndicators
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }



        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Ցուցիչ")]
        public int IndicatorID { get; set; }


        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Պրոդուկտ")]
        public int ProductID { get; set; }


        //public virtual Products Products { get; set; }

        //public virtual ScoringIndicators ScoringIndicators { get; set; }

    }
}