using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class GoldTypes
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoldTypes()
        {
            GoldCollateral = new HashSet<GoldCollaterals>();
        }

        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Required]
        [Display(Name = "Իրի տեսակ")]
        public string Title { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoldCollaterals> GoldCollateral { get; set; }
    }



}