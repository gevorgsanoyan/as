using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class ScoringScores
    {
        [Key]
        public int ID { get; set; }

       

        [Display(Name = "Ցուցիչ")]
        public int indicatorID { get; set; }


        [Display(Name = "Նվազագույն սահմանաչափ")]
        public double minValue { get; set; }


        [Display(Name = "Առավելագույն սահմանաչափ")]
        public double maxValue { get; set; }


        [Display(Name = "Գնահատական")]
        public double Score { get; set; }

        [Display(Name = "Գործակից")]
        public double Coefficient { get; set; }


        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }





        [Display(Name = "Մուտքագրող օգտատեր")]
        public string userId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastModifDate { get; set; } = DateTime.Now;


        //public virtual ScoringIndicators ScoringIndicators { get; set; }

    }
}