using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class productGroups
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public productGroups()
        {
            Products = new HashSet<Products>();
        }

        [Key]
        public int productGroupId { get; set; }

        [Display(Name = "Պրոդուկտի խմբի անունը")]
        public string prodGroupName { get; set; }


        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում3")]
        public string note3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
    }

}