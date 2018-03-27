using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class applicationEditRoles
    {
        [Key]
        [Display(Name = "ID")]
        public int applicationEditRolesId { get; set; }
        [Display(Name = "Դերի անվանումը")]
        public string RoleName { get; set; }
        [Display(Name = "Կարող է խմբագրել")]
        public bool? canEdit { get; set; }
    }
}