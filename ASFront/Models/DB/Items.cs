namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Items
    {
        public int Id { get; set; }

        public long applicationId { get; set; }

        public string ItemName { get; set; }

        public string ItemDescr { get; set; }

        public int FKProductPurposeId { get; set; }

        public int Count { get; set; }

        public float Price { get; set; }

        public float Sum { get; set; }

        public float ClientInvest { get; set; }

        public long SupplierId { get; set; }

        public long clientId { get; set; }

        public string note1 { get; set; }

        public string note2 { get; set; }

        public string note3 { get; set; }

        public string note4 { get; set; }

        public string note5 { get; set; }

        public string userId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifDate { get; set; }
    }
}
