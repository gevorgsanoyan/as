using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class ScoringScoreView
    {

        public int ID { get; set; }



        [Display(Name = "Ցուցիչ")]
        public int indicatorID { get; set; }


        [Display(Name = "Ցուցիչ")]
        public string IndicatorName { get; set; }


        [Display(Name = "Նվազագույն սահմանաչափ")]
        public double minValue { get; set; }


        [Display(Name = "Առավելագույն սահմանաչափ")]
        public double maxValue { get; set; }


        [Display(Name = "Գնահատական")]
        public double Score { get; set; }

        [Display(Name = "Գործակից")]
        public double Coefficient { get; set; }


        
    }
}
