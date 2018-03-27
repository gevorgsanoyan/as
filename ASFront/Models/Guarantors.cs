using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    //Guarantor
    public class Guarantors
    {

        [Key]
        [Display(Name = "ID")]
        public long Id { get; set; }



        [Required]
        [ForeignKey("applications")]

        [Display(Name = "Հայտ")]
        public long applicationId { get; set; }



        [Required]
        [ForeignKey("clients")]

        [Display(Name = "Հաճախորդ")]
        public long clientId { get; set; }


        public virtual applications applications { get; set; }

        public virtual clients clients { get; set; }


    }

}