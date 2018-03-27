using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class ProductPurposeDropBoxViewModel
    {
      
        public int Id { get; set; }

    
     
        [Display(Name = "Պրոդուկտ-Նպատակ")]
        public string ProductPurposeName { get; set; }



     

    }
}