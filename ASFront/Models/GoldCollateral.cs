using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    public class GoldCollaterals
    {


        public GoldCollaterals ShallowCopy()
        {
            GoldCollaterals Gc=(GoldCollaterals)this.MemberwiseClone();
            Gc.Id = 0;
            return Gc;
        }



        [Key]
        [Display(Name = "N")]
        public long Id { get; set; }



        [Required(ErrorMessage = "*")]
        [ForeignKey("GoldAssayes")]
        [Display(Name = "Հարգ")]
        public int GoldAssayId { get; set; }


        [Required(ErrorMessage = "*")]
        [ForeignKey("GoldTypes")]
        [Display(Name = "Իրի տեսակ")]
        public int GoldTypeId { get; set; }



        [Required(ErrorMessage = "*")]
        [ForeignKey("applications")]

        [Display(Name = "Հայտ")]
        public long applicationId { get; set; }


        //[Required]
        //[Display(Name = "Համար")]
        //public int Number { get; set; }



        [Required(ErrorMessage = "*")]
        [Display(Name = "Քանակ")]
        public int Quantity { get; set; }



        [Required(ErrorMessage = "*")]
        [Display(Name = "Նկարագր.")]
        public string Description { get; set; }








        //[Required]
        //[Display(Name = "Գրամ")]
        //public double Gram { get; set; }



        [Required(ErrorMessage = "*")]
        [Display(Name = "Ընդ. կշիռը (գր.)")]
        public double TotalWeightWithJewels { get; set; }



        [Required(ErrorMessage = "*")]
        [Display(Name = "Զուտ կշիռ (գր.)")]
        public double NetWeight { get; set; }



        [Required(ErrorMessage = "*")]
        [Display(Name = "Հարգադրոշմ.")]
        public bool PresenceOfAssayStamp { get; set; } = true;





        [Required(ErrorMessage = "*")]
        [Display(Name = "Գնահատված միավորի արժեքը")]
        public double EstimatedUnitCostAMD { get; set; }






        [Required(ErrorMessage = "*")]
        [Display(Name = "Գնահատված ընդհանուր արժեքը")]
        public double EstimatedTotalCostAMD { get; set; }








        //[Required]
        //[Display(Name = "Գինը 1 գրամի համար")]
        //public double PricePerGram { get; set; }





        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում3")]
        public string note3 { get; set; }





        public virtual GoldAssayes GoldAssayes { get; set; }
        public virtual GoldTypes GoldTypes { get; set; }

        public virtual applications applications { get; set; }

    }
}