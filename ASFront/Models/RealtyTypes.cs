using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class RealtyTypes
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RealtyTypes()
        {

            RealtyEstate = new HashSet<RealtyEstates>();

        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RealtyEstates> RealtyEstate { get; set; }


        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Required]
        [Display(Name = "Անշարժ գույքի տեսակ")]
        public string Title { get; set; }



    }



}