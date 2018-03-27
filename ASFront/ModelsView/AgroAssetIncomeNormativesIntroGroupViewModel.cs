using ASFront.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class AgroAssetIncomeNormativesIntroGroupViewModel
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



        //public virtual AgroAssetTypes AgroAssetTypes { get; set; }

        //public virtual Branches Branches { get; set; }

    }
}