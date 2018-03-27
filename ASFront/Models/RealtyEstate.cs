using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    public class RealtyEstates
    {


        [Key]
        [Display(Name = "ID")]
        public long Id { get; set; }



        [Required]
        [ForeignKey("RealtyTypes")]
        [Display(Name = "Անշարժ գույքի տեսակ")]
        public int RealtyTypeId { get; set; }



        [Required]
        [ForeignKey("applications")]

        [Display(Name = "Հայտ")]
        public long applicationId { get; set; }



        [Required]
   [Display(Name = "Հասցե")]
        
        public string Address { get; set; }



     //   [Required]

     //[Display(Name = "Անվանում")]
     //   public string Name { get; set; }


        //[Required]
        //[Display(Name = "Համար")]
        //public int Number { get; set; }




        [Required]
        [Display(Name = "Շուկայական արժեք (ՀՀ դրամ)")]
        public double mPrice { get; set; }



        [Required]
        [Display(Name = "Մակերես (ք/մ)")]
        public double area { get; set; }



        [Required]
        [Display(Name = "Գնահատված ընդհանուր արժեք (ՀՀ դրամ)")]
        public double TotRatedPrice { get; set; }


        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }


        public virtual RealtyTypes RealtyTypes { get; set; }
        public virtual applications applications { get; set; }

    }



}