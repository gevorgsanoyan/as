using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class AgroAssetIncomeNormative
    {


        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [ForeignKey("Branches")]
        [Display(Name = "Մասնաճյուղ")]
        public int BrancheId { get; set; }



        [Required]
        [ForeignKey("AgroAssetTypes")]
        [Display(Name = "Ագրո Ակտիվների տեսակ")]
        public int AgroAssetTypesId { get; set; }



        [Required]
        [Display(Name = "Ամիս")]
        public int Month { get; set; }



        [Display(Name = "Գին")]
        public double Price { get; set; } = 1;


        [Required]
        [Display(Name = "Քանակ")]
        public int Quantity { get; set; } = 1;


        [Required]
        [Display(Name = "Ծախս")]
        public int Expenses { get; set; } = 1;

        //[Required]
        [Display(Name = "Նկարագրություն")]
        public string Description { get; set; }

        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        //[Display(Name = "Նշում3")]
        //public string note3 { get; set; }


        //[Display(Name = "Նշում4")]
        //public string note4 { get; set; }



        public virtual AgroAssetTypes AgroAssetTypes { get; set; }

        public virtual Branches Branches { get; set; }

    }
}