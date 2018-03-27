namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class companyTypes
    {
        [Key]
        public int companyTypeID { get; set; }

        public string companyTypeName { get; set; }

        public int FK_empType { get; set; }
    }
}
