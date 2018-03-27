using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class ProductLimits
    {
        
     
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }




        [Display(Name = "Պրոդուկտ")]
        public int ProductID { get; set; }



        [Display(Name = "Սահմանաչափ")]
        public int AmountLimit { get; set; }



        [Display(Name = "Սքորինգ")]
        public bool? Scoring { get; set; }


        [Display(Name = "Օղակ 1")]
        public bool? App1 { get; set; }

        [Display(Name = "Օղակ 2")]
        public bool? App2 { get; set; }


        public virtual Products Products { get; set; }
    }
}