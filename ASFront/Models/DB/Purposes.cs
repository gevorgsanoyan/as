namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purposes
    {
        public int Id { get; set; }

        public string PurposeName { get; set; }

        public string note1 { get; set; }

        public string note2 { get; set; }

        public string note3 { get; set; }

        public string userId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifDate { get; set; }
    }
}
