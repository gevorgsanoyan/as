using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class AgroAsset
    {

        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Display(Name = "Հայտ")]
        public long applicationId { get; set; }


        [Required]
        [ForeignKey("AgroAssetTypes")]
        [Display(Name = "Գյուղ. ակտիվի տեսակ")]
        public int AgroAssetTypesId { get; set; }


        [Required]
        [Display(Name = "Քանակ")]
        public int Quantity { get; set; }


        //[Required]
        [Display(Name = "Նկարագրություն")]
        public string Description { get; set; }

        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում3")]
        public string note3 { get; set; }


        [Display(Name = "Նշում4")]
        public string note4 { get; set; }



        public virtual AgroAssetTypes AgroAssetTypes { get; set; }



    }
}