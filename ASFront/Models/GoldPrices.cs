using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class GoldPrices
    {



        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }


        [Required]
        [ForeignKey("GoldAssayes")]
        [Display(Name = "Հարգ")]
        public int GoldAssayId { get; set; }


        [Required]
        [Display(Name = "Գին")]
        public double Price { get; set; }


        [Required]
        [Display(Name = "Ամսաթիվ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; } = DateTime.Now;



        public virtual GoldAssayes GoldAssayes { get; set; }


    }

}