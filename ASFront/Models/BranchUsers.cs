using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    public class BranchUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [ForeignKey("Branches")]
        [Display(Name = "Մասնաճյուղ")]
        public int BrancheId { get; set; }


        //[ForeignKey("UserASProfiles")]
        [Display(Name = "Օգտվող")]
        public int UserASProfileId { get; set; }



        public virtual Branches Branches { get; set; }
        //public virtual UserASProfiles UserASProfiles { get; set; }


    }

}