using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class Items
    {
        [Key]
        [Display(Name = "Items ID")]
        public int Id { get; set; }


        [Display(Name = "Հայտ")]
        public long applicationId { get; set; }


        [Display(Name = "Ապրանքի անուն")]
        public string ItemName { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Ապրանքի նկարագրություն")]
        public string ItemDescr { get; set; }


        [Display(Name = "Պրոդուկտ-Նպատակ")]
        public int FKProductPurposeId { get; set; }


        [Display(Name = "Քանակ")]
        public int Count { get; set; }    


        [Display(Name = "Գին")]
        public float Price { get; set; }


        [Display(Name = "Գումար")]
        public float Sum { get; set; }


        [Display(Name = "Հաճախորդի ներդրում")]
        public float ClientInvest { get; set; }


        [Display(Name = "Մատակարար")]
        public long SupplierId { get; set; } 


        [Display(Name = "Հաճախորդ")]
        public long clientId { get; set; }



        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում3")]
        public string note3 { get; set; }


        [Display(Name = "Նշում4")]
        public string note4 { get; set; }

        [Display(Name = "Նշում5")]
        public string note5 { get; set; }






        [Display(Name = "Մուտքագրող օգտատեր")]
        public string userId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastModifDate { get; set; } = DateTime.Now;

    }
}