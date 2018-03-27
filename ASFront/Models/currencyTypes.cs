using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class CurrencyTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CurrencyTypes()
        {

            CurrencyCurrents = new HashSet<CurrencyCurrents>();
           
        }




        [Key]
        public int currencyTypesId { get; set; }


        public string currencyArm { get; set; }


        public string currencyEng { get; set; }


        public string exchCode { get; set; }

           public string Sign { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurrencyCurrents> CurrencyCurrents { get; set; }


    }

}