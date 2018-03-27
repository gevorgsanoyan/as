using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class ScoringApplicationScores
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Հայտ")]
        public long ApplicationID { get; set; }

        [Display(Name = "Ինդիկատոր")]
        public int IndicatorID { get; set; }


        [Display(Name = "Արժեք")]
        public double Value { get; set; }


      


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





      

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastModifDate { get; set; } = DateTime.Now;





        //public virtual applications applications { get; set; }

        //public virtual ScoringIndicators ScoringIndicators { get; set; }

    }
}