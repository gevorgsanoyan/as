using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class Settings
    {

        [Key]
        public int Id { get; set; }


        [Required]
        [Range(5, 50, ErrorMessage = "Please enter a number between 5 and 50")]
        [Display(Name = "Page Size")]
        public int PageSize { get; set; }


       

    }
}