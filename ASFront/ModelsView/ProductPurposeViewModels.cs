using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class ProductPurposeViewModel
    {
      
        public int Id { get; set; }

    
     
        [Display(Name = "Պրոդուկտ")]
        public string ProductName { get; set; }



        [Display(Name = "Նպատակ")]
        public string PurposeName { get; set; }


    }
}