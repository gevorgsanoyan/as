using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class MeasurementUnits
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeasurementUnits()
        {

            Turnover = new HashSet<Turnovers>();

        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Turnovers> Turnover { get; set; }


        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Required]
        [Display(Name = "Չափի միավոր")]
        public string Title { get; set; }



    }



}