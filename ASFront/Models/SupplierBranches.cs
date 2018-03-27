using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class SupplierBranches
    {


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierBranches()
        {
            Sellers = new HashSet<Sellers>();
        }

        [Key]
        [Display(Name = "BrancheId")]
        public long BrancheId { get; set; }



        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [Display(Name = "Մատակարարողի մասնաճյուղի անվանում ")]
        public string BrancheName { get; set; }



        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [Display(Name = "Մատակարար")]
        public long SupplierId { get; set; }



        [Display(Name = "Քաղաք")]
        public string City { get; set; }


        [Display(Name = "Հասցե")]
        public string Address { get; set; }



        [Display(Name = "Հեռախոս")]
        public string Phone { get; set; }




        [Display(Name = "Մեկնաբանություն")]
        public string Comment { get; set; }


        [Display(Name = "Կոորդինատ1")]
        public string Coordinate1 { get; set; }



        [Display(Name = "Կոորդինատ2")]
        public string Coordinate2 { get; set; }



        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }


        [Display(Name = "Նշում 4")]
        public string note4 { get; set; }


        [Display(Name = "Նշում 5")]
        public string note5 { get; set; }





        [Display(Name = "Գործող է")]
        public bool Active { get; set; } = true;


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sellers> Sellers { get; set; }


        public virtual Suppliers Suppliers { get; set; }
    }







}