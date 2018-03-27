namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class currencyTypes
    {
        public int currencyTypesId { get; set; }

        public string currencyArm { get; set; }

        public string currencyEng { get; set; }

        public string exchCode { get; set; }
    }
}
