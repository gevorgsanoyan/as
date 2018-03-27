using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class ScoringIndicatorsView
    {
        public int ID { get; set; }


        [Display(Name = "Ցուցիչի անվանումը")]
        public string IndicatorName { get; set; }





        [Display(Name = "Ցուցիչի տեսակը")]
        public string IndicatorTypeName { get; set; }


        //[Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        //[UIHint("Number")]
        [Display(Name = "Ցուցիչի արժեքը")]
        public double IndicatorValue { get; set; } = 0;


        [Display(Name = "Ֆորմուլա")]
        public string FormulaText { get; set; }


        [Display(Name = "Ֆորմուլա ուղղված ըստ առաջնահերթության")]
        public string FormulaTextPriorityFixed { get; set; }




        [Display(Name = "Մուտքագրող օգտատեր")]
        public string userId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastModifDate { get; set; } = DateTime.Now;

    }
}