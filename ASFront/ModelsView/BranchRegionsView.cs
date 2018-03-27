using System;
using System.ComponentModel.DataAnnotations;


namespace ASFront.Models
{
    public class BranchRegionsView
    {

        [Display(Name = "Մասն. Id")]
        public int BranchId  { get; set; }


        [Display(Name ="Մասնաճյուղ")]
        public string Branch  { get; set; }


        [Display(Name = "Տարածաշրջ. Id")]
        public int RegionId  { get; set; }


        [Display(Name ="Տարածաշրջան")]
        public string Region  { get; set; }


        [Display(Name = "Թ.ամսաթիվ")]
        public DateTime rDate  { get; set; }


    }

    

}
