namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Suppliers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Suppliers()
        {
            Sellers = new HashSet<Sellers>();
            SupplierBranches = new HashSet<SupplierBranches>();
        }

        [Key]
        public long SupplierId { get; set; }

        [Required]
        public string SupplierName { get; set; }

        [Required]
        public string SupplierBrand { get; set; }

        [Required]
        public string regAddress { get; set; }

        [Required]
        public string curAddress { get; set; }

        [Required]
        public string SupplierDirector { get; set; }

        [Required]
        public string phoneNumb { get; set; }

        public string faxNumb { get; set; }

        [Required]
        public string mobNumb { get; set; }

        public string email { get; set; }

        public string hvhh { get; set; }

        public string bankAccnt { get; set; }

        public string note1 { get; set; }

        public string note2 { get; set; }

        public string note3 { get; set; }

        public string note4 { get; set; }

        public string note5 { get; set; }

        public string userId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifDate { get; set; }

        public string TelegramID { get; set; }

        public bool Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sellers> Sellers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierBranches> SupplierBranches { get; set; }
    }
}
