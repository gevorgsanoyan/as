using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    

    public class ProductSupplierViewModel
    {


        
        public int Id { get; set; }




        [Display(Name = "Պրոդուկտ")]
        public string ProductName { get; set; }


       
        [Display(Name = "Մատակարար")]
        public string SupplierName { get; set; }


    }

}