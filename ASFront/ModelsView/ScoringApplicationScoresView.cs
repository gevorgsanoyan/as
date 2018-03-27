using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class ScoringApplicationScoresView
    {

        public int ID { get; set; }

        [Display(Name = "Հայտ")]
        public string ApplicationName { get; set; }

        

        [Display(Name = "Ցուցիչ")]
        public  string IndicatorName { get; set; }


        [Display(Name = "Արժեք")]
        public double Value { get; set; }





        [Display(Name = "Գնահատական")]
        public double Score { get; set; }

        [Display(Name = "Գործակից")]
        public double Coefficient { get; set; }

     




 

    }
}