using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class group
    {
        [Key]
        [Display(Name = "ID")]
        public long groupId { get; set; }
        [Display(Name = "Խմբի ազգանունը")]
        public string gruopName { get; set; }
        [Display(Name = "Խմբի հասցեն")]
        public string gruopAddress { get; set; }
        [Display(Name = "Խմբի լրիվ անվանում")]
        public string gruopFullName { get; set; }

        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }
        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }
        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }

    }
}