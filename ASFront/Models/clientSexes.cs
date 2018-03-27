using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class clientSexes
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public clientSexes()
        {
            clients = new HashSet<clients>();

            BusinessInfo = new HashSet<BusinessInfo>();
              
    }

  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessInfo> BusinessInfo { get; set; }


        [Key]
        [Display(Name = "Id")]
        public int clientSexId { get; set; }


        [Display(Name = "Սեռ")]
        public string sex { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<clients> clients { get; set; }
    }

  
}