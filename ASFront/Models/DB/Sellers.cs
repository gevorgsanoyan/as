namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sellers
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public long SupplierId { get; set; }

        public long BrancheId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Comment { get; set; }

        public string TelegramID { get; set; }

        public string Position { get; set; }

        public string note1 { get; set; }

        public string note2 { get; set; }

        public string note3 { get; set; }

        public string note4 { get; set; }

        public string note5 { get; set; }

        public bool Active { get; set; }

        public virtual SupplierBranches SupplierBranches { get; set; }

        public virtual Suppliers Suppliers { get; set; }
    }
}
