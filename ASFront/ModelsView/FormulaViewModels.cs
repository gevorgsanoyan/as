using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class FormulaViewModel
    {

        public List<ScoringParametersViewModels> ParametersList { get; set; }

        public CreateFormulaPartialViewModel CreateFormula { get; set; }

        //public PagedList.IPagedList<FormulaPartialViewModel> FormulasList { get; set; }

        public int IndicatorID { get; set; } = 0;
    }

    //public class ScoringParametersPartialViewModel
    //{

    //    public int ID { get; set; }


    //    [Display(Name = "Ինդիկատոր")]
    //    public String IndicatorName { get; set; }


    //    [Display(Name = "նվազագույն սահմանաչափ")]
    //    public double minValue { get; set; }


    //    [Display(Name = "առավելագույն սահմանաչափ")]
    //    public double maxValue { get; set; }




    //}

    public class CreateFormulaPartialViewModel
    {
        [Key]
        [Display(AutoGenerateField = false)]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]

     
        public int IndicatorID { get; set; }


        [Display(Name = "Ֆորմուլա")]
        public string FormulaText { get; set; }

    }

    //public class FormulaPartialViewModel
    //{
    //    public int ID { get; set; }
    //    public string FormulaText { get; set; }
    //    public string FormulaTextPriorityFixed { get; set; }
    //}



}