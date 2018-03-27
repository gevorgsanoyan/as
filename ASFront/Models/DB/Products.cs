namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [Key]
        public int productId { get; set; }

        public int productGroupId { get; set; }

        [Required]
        public string productName { get; set; }

        public string productShortName { get; set; }

        [Required]
        public string agrType { get; set; }

        public int minMaturtity { get; set; }

        public int maxMaturity { get; set; }

        public float appFee { get; set; }

        public float upfronFee { get; set; }

        public float mothFee { get; set; }

        public float mothFeeFlat { get; set; }

        public float anualRate { get; set; }

        public string asNote1 { get; set; }

        [Required]
        public string asNote2 { get; set; }

        [Required]
        public string asSector { get; set; }

        [Required]
        public string productCurrency { get; set; }

        public bool prodStatus { get; set; }

        public string note1 { get; set; }

        public string note2 { get; set; }

        public string note3 { get; set; }

        public string note4 { get; set; }

        public string userId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifDate { get; set; }

        public virtual productGroups productGroups { get; set; }
    }
}
