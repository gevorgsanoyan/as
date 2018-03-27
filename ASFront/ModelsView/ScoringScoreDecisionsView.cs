using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class ScoringScoreDecisionsView
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }



      


      
        [Display(Name = "Պրոդուկտ")]
        public string ProductName { get; set; }





        [Display(Name = "Նվազագույն սահմանաչափ")]
        public double minValue { get; set; }


        [Display(Name = "Առավելագույն սահմանաչափ")]
        public double maxValue { get; set; }


     


        [Display(Name = "Որոշում")]
        public string Decision { get; set; }



        //public virtual Products Products { get; set; }

        //public virtual ScoringDecisions ScoringDecisions { get; set; }


    }
}