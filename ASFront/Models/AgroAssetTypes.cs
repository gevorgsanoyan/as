using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class AgroAssetTypes
    {



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AgroAssetTypes()
        {
            AgroAsset = new HashSet<AgroAsset>();
            AgroAssetIncomeNormative = new HashSet<AgroAssetIncomeNormative>();
        }


        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }


        [Display(Name = "Անուն")]
        public string Name { get; set; }


        [Display(Name = "Գին")]
        public double Price { get; set; }


        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում3")]
        public string note3 { get; set; }


        [Display(Name = "Նշում4")]
        public string note4 { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgroAsset> AgroAsset { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgroAssetIncomeNormative> AgroAssetIncomeNormative { get; set; }
    }

}