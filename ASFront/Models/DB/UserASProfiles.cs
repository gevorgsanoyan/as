namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserASProfiles
    {
        [Key]
        public int UserASProfileId { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public string asUserName { get; set; }

        public string asUserId { get; set; }

        public string asUserCode { get; set; }

        public int? BrancheId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Branches Branches { get; set; }
    }
}
