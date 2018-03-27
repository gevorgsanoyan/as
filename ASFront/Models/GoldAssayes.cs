using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class GoldAssayes
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoldAssayes()
        {
            GoldCollateral = new HashSet<GoldCollaterals>();
            GoldPrices = new HashSet<GoldPrices>();
        }


        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Հարգը")]
        public string Assay { get; set; }


        //[Required]
        //[Display(Name = "Գինը 1 գրամի համար")]
        //public double PricePerGram{ get; set; }


        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում3")]
        public string note3 { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoldCollaterals> GoldCollateral { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoldPrices> GoldPrices { get; set; }

    }


}