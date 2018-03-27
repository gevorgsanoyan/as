using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class CurrencyCurrents
    {

        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



      


        [Required]
        [ForeignKey("CurrencyTypes")]
        [Display(Name = "Արժույթի տեսակները")]
        public int CurrencyTypeId { get; set; }



        [Required]
        [Display(Name = "Արժեք")]
        public double Value { get; set; }


        [Required]
        public DateTime DateStamp { get; set; } = DateTime.Now;



        public virtual CurrencyTypes CurrencyTypes { get; set; }



    }
}