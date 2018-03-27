using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity.Infrastructure.Annotations;
//using System.Data.Entity.ModelConfiguration.Configuration;

namespace ASFront.Models
{
    public class Branches
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Branches()
        {
            UserASProfiles = new HashSet<UserASProfiles>();
            FamilyCostNormatives = new HashSet<FamilyCostNormatives>();
            BranchUsers = new HashSet<BranchUsers>();
            AgroAssetIncomeNormative = new HashSet<AgroAssetIncomeNormative>();
            clients= new HashSet<clients>();
        }

        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Display(Name = "Մասնաճյուղ")]
        [Required]
        //[Index("IX_Branch", IsUnique = true)]
        public string Branch { get; set; }



        [Display(Name = "ՀԾ Կոդ")]
        public string asCode { get; set; }


        [Display(Name = "Հասցե")]
        public string Address { get; set; }




        [Display(Name = "Հեռախոս")]
        public string Phone { get; set; }



        [Display(Name = "Նշում")]
        public string Note { get; set; }



        [Display(Name = "Մարզ")]
        public string Region { get; set; }



        [Display(Name = "Քաղաք/Գյուղ/Համայնք")]
        public string City { get; set; }


        [Display(Name = "Փողոց")]
        public string Street { get; set; }





        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserASProfiles> UserASProfiles { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FamilyCostNormatives> FamilyCostNormatives { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BranchUsers> BranchUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgroAssetIncomeNormative> AgroAssetIncomeNormative { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<clients> clients { get; set; }

    }

    //public class ASFrontDbContext:DbContext
    //{
    //    public ASFrontDbContext()
    //        :base("DefaultConnection")
    //    {

    //    }        

    //}

}