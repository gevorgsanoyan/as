using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class DocsApllications
    {


        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Անուն")]
        public string Name { get; set; }

        [Display(Name = "Ֆայլ")]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "Փաստաթղթի տեսակ")]
        public int DocTypeId { get; set; }


        [Display(Name = "Հայտ")]
        public long? ApplicationId { get; set; }


        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում3")]
        public string note3 { get; set; }


        [Display(Name = "Նշում4")]
        public string note4 { get; set; }

        [Display(Name = "Հաճախորդ (ID)")]
        public long? clientId { get; set; }



        //public virtual applications applications { get; set; }

        public virtual DocType DocType { get; set; }
    }
}