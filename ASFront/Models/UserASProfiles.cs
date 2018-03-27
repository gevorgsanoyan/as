using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    public class UserASProfiles
    {

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public UserASProfiles()
        //{
        //    BranchUsers = new HashSet<BranchUsers>();


        //}

        [Key]
        [Display(Name = "Id")]
        public int UserASProfileId  { get; set; }


        [Display(Name = "Օգտատեր")]
        //[Key]
        public string UserId  { get; set; }


        [Display(Name = "ՀԾ օգտատեր")]
        public string asUserName  { get; set; }




        [Display(Name = "Անուն")]
        [Required]
        public string FirstName { get; set; } 



        [Display(Name = "Ազգանուն")]
        [Required]
        public string LastName { get; set; } 



        [Display(Name = "Հայրանուն")]
        public string Patronymic { get; set; }




        [Display(Name = "ՀԾ օգտ. Id")]
        public string asUserId  { get; set; }


        [Display(Name = "ՀԾ օգտ. կոդ")]
        public string asUserCode  { get; set; }



        [Display(Name = "Հեռախոսահամար")]
        public string uPhoneNumber { get; set; }

        [Display(Name = "Telegram ID")]
        public string telegramId { get; set; }

        [Display(Name = "Telegram Հեռախոսահամար")]
        public string telegramPhoneNumber { get; set; }



        [Display(Name = "Մասնաճյուղ")]
        public virtual Branches Branches { get; set; }



        [ForeignKey("Branches")]
        [Display(Name = "Մասնաճյուղ")]
        public int BrancheId { get; set; }

        //[ForeignKey("UserId")]
        //public virtual ApplicationUser User  { get; set; }


        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BranchUsers> BranchUsers { get; set; }

    }
}
