using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ASFront.Models
{
    public class JewelleryItemType
    {
        [Key]
        [Display(Name = "ID")]
        public int jewelleryItemTypeId { get; set; }


        [Display(Name = "Իրի տեսակ")]
        public string jewelleryItem { get; set; }


        [Display(Name = "Իրի տեսակ (eng.)")]
        public string jewelleryItemEng { get; set; }


        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }



        public virtual ICollection<Golds> gold { get; set; }


    }
    public class Golds
    {
        [Key]
        [Display(Name ="ID")]
        public long goldId { get; set; }

        
        
        [Display(Name = "Իրի տեսակ")]
        public int? jwItem { get; set; }



        [Display(Name = "Նկարագրություն")]
        public string jwItemDescr { get; set; }



        [Display(Name = "Կշիռը (գրամ)")]
        public double weight { get; set; }



        [Display(Name = "Ընդհանուր կշիռը զարդարանքի հետ միասին (գրամ)")]
        public double weightTotal { get; set; }





        [Display(Name = "Վարկավորման համար պիտանի մաքուր կշիռը (գրամ)")]
        public double weightForLend { get; set; }




        [Display(Name = "Հարգադրոշմանիշի առկայություն")]
        public bool isgSample { get; set; } = false;





        [Display(Name = "Հարգը")]
        public string gSample { get; set; }




        [Display(Name = "Գնահատված միավորի արժեքը (ՀՀ դրամ)")]
        public double unitPrice { get; set; }


        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public double totalPrice { get; set; }



        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }




        [ForeignKey("jwItem")]
        public virtual JewelleryItemType jewelleryItemType { get; set; }



    }
}