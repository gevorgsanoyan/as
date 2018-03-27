using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class ScoringParametersViewModels
    {

        public int ID { get; set; }


        [Display(Name = "Ցուցիչ")]
        public String IndicatorName { get; set; }


        [Display(Name = "Մուտքային պարամետրի անվանում")]
        public string InputParameterName { get; set; }


        [Display(Name = "Մուտքային պարամետրի արժեք")]
        public double InputParameterValue { get; set; }




        //[Display(Name = "Մուտքագրող օգտատեր")]
        //public string userId { get; set; }

        //public DateTime CreatedDate { get; set; } = DateTime.Now;

        //public DateTime LastModifDate { get; set; } = DateTime.Now;


    }
}