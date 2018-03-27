namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class comunities
    {
        [Key]
        public int comunityId { get; set; }

        public string reg { get; set; }

        public string cName { get; set; }

        public string areaCode { get; set; }
    }
}
