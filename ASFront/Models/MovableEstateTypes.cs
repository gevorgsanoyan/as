using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class MovableEstateTypes
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MovableEstateTypes()
        {

            MovableEstate = new HashSet<MovableEstates>();

        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovableEstates> MovableEstate { get; set; }


        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }





        [Required]
        [Display(Name = "Շարժական գույքի տեսակ")]
        public string Title { get; set; }



    }



}