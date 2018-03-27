namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScoringIndicators
    {
        public int ID { get; set; }

        public double IndicatorValue { get; set; }

        public string userId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifDate { get; set; }

        public string FormulaText { get; set; }

        public string FormulaTextPriorityFixed { get; set; }

        [Required]
        public string IndicatorName { get; set; }

        public int IndicatorType { get; set; }
    }
}
