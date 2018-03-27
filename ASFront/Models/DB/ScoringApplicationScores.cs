namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScoringApplicationScores
    {
        public int ID { get; set; }

        public long ApplicationID { get; set; }

        public int IndicatorID { get; set; }

        public double Value { get; set; }

        public double Score { get; set; }

        public double Coefficient { get; set; }

        public string note1 { get; set; }

        public string note2 { get; set; }

        public string note3 { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifDate { get; set; }
    }
}
