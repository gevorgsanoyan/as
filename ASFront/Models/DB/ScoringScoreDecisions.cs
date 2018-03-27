namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScoringScoreDecisions
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        public double minValue { get; set; }

        public double maxValue { get; set; }

        public int DecisionID { get; set; }
    }
}
