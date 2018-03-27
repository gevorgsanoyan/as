namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class acras
    {
        [Key]
        public long acraId { get; set; }

        public long resultId { get; set; }

        public long clienId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public DateTime dob { get; set; }

        public string passp { get; set; }

        public string socNumb { get; set; }

        public string cAddress { get; set; }

        public DateTime incUpdateDate { get; set; }

        public string employm { get; set; }

        public int req30Count { get; set; }

        public int reqCount { get; set; }

        public int delay_tot { get; set; }

        public int delayMax { get; set; }

        public double totLiabAMD { get; set; }

        public double totLiabUSD { get; set; }

        public double totGuarAMD { get; set; }

        public double totGuarUSD { get; set; }

        public double totOverdueAMD { get; set; }

        public double totOverdueUSD { get; set; }

        public double totOverdueGAMD { get; set; }

        public double totOverdueGUSD { get; set; }

        public string userId { get; set; }
    }
}
