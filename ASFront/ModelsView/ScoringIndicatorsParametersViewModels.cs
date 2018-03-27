using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class ScoringIndicatorsParametersViewModel
    {

       
        public int ID { get; set; }


       
        [Display(Name = "Ցուցիչ")]
        public string IndicatorName { get; set; }


        
        [Display(Name = "Պարամետր")]
        public string ParameterName{ get; set; }






      

    }
}