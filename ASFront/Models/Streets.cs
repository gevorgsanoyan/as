using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.Models
{

    public class Streets
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Մարզ")]
        public string reg { get; set; }


        [Required]
        [Display(Name = "Համայնք")]
        public string cName { get; set; }


        [Required]
        [Display(Name = "Փողոց")]
        public string Street { get; set; }

        [Display(Name = "Շենք")]
        public string building { get; set; }

        [Required]
        [Display(Name = "Բնակարան")]
        public string apartment { get; set; }



    }

}