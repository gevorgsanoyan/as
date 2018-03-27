namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BrbyRegs
    {
        public int BrbyRegsId { get; set; }

        public int BranchesId { get; set; }

        public int RegionsId { get; set; }

        public DateTime rDate { get; set; }

        public string userId { get; set; }

        public int current { get; set; }
    }
}
