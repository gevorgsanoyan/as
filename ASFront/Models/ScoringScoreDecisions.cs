using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class ScoringScoreDecisions
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }



      


      
        [Display(Name = "Պրոդուկտ")]
        public int ProductID { get; set; }





        [Display(Name = "Նվազագույն սահմանաչափ")]
        public double minValue { get; set; }


        [Display(Name = "Առավելագույն սահմանաչափ")]
        public double maxValue { get; set; }


     


        [Display(Name = "Որոշում")]
        public int DecisionID { get; set; }



        //public virtual Products Products { get; set; }

        //public virtual ScoringDecisions ScoringDecisions { get; set; }


    }
}