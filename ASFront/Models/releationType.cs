using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    public class releationType
    {
        [Key]
        [Display(Name ="ID")]
        public int releationTypeId { get; set; }
        [Display(Name = "Փոխկապակցվածության տեսակ")]
        public string relType { get; set; }
        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }
        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }

        public virtual ICollection<clientsGroup> clientsGroup { get; set; }
    }
}