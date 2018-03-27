using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class ClientShearchViewModel
    {

      

        [Display(Name = "NameStr")]
        public string NameStr { get; set; }


        [Display(Name = "PhoneStr")]
        public string PhoneStr { get; set; }


        [Display(Name = "PassOrSocNumtStr")]
        public string PassOrSocNumtStr { get; set; }


        [Display(Name = "Մարզ")]
        public string Region { get; set; }



        [Display(Name = "Քաղաք/Գյուղ/Համայնք")]
        public string City { get; set; }


        [Display(Name = "Փողոց")]
        public string Street { get; set; }

    }
}