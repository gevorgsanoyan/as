namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SupplierBranches
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierBranches()
        {
            Sellers = new HashSet<Sellers>();
        }

        [Key]
        public long BrancheId { get; set; }

        [Required]
        public string BrancheName { get; set; }

        public long SupplierId { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Comment { get; set; }

        public string Coordinate1 { get; set; }

        public string Coordinate2 { get; set; }

        public string note1 { get; set; }

        public string note2 { get; set; }

        public string note3 { get; set; }

        public string note4 { get; set; }

        public string note5 { get; set; }

        public bool Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sellers> Sellers { get; set; }

        public virtual Suppliers Suppliers { get; set; }
    }
}
