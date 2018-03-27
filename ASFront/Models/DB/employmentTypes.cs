namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class employmentTypes
    {
        [Key]
        public int empTypeID { get; set; }

        public string employment { get; set; }
    }
}
