namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class applications
    {
        [Key]
        public long applicationId { get; set; }

        public DateTime appDate { get; set; }

        public string userId { get; set; }

        public long clientId { get; set; }

        public int branchId { get; set; }

        public int appStatus { get; set; }

        public int appuserId { get; set; }

        public string appDescr { get; set; }

        public DateTime aprDate { get; set; }

        public string agrNumb { get; set; }

        public string agrNumbP { get; set; }

        public int productId { get; set; }

        public string note1 { get; set; }

        public string note2 { get; set; }

        public string note3 { get; set; }

        public string note4 { get; set; }

        public string note5 { get; set; }

        public int? SellerId { get; set; }

        public int CreditTerm { get; set; }
    }
}
