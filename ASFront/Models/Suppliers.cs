using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class Suppliers
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Suppliers()
        {
            Sellers = new HashSet<Sellers>();
            SupplierBranches = new HashSet<SupplierBranches>();
        }

        [Key]
        [Display(Name = "Մատակարարի ID")]
        public long SupplierId  { get; set; }


        [Display(Name = "Մատակարարի անվանում")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string SupplierName  { get; set; }


        [Display(Name = "Բրենդ")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string SupplierBrand  { get; set; }


        [Display(Name = "Գրանցման հասցե")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string regAddress  { get; set; }


        [Display(Name = "Գործունեության հասցե")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string curAddress  { get; set; }


        [Display(Name = "Տնօրեն")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string SupplierDirector  { get; set; }


        [Display(Name = "Հեռախոս")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNumb  { get; set; }


        [Display(Name = "Ֆաքս")]        
        [DataType(DataType.PhoneNumber)]
        public string faxNumb  { get; set; }


        [Display(Name = "Բջջային")]
        //[Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [DataType(DataType.PhoneNumber)]
        public string mobNumb  { get; set; }


        [Display(Name = "Էլ.փոստ")]        
        [DataType(DataType.EmailAddress)]
        public string email  { get; set; }


        [Display(Name = "ՀՎՀՀ")]
        public string hvhh  { get; set; }


        [Display(Name = "Բանկային հաշիվ")]
        public string bankAccnt  { get; set; }



        [Display(Name = "TelegramID")]
        public string TelegramID { get; set; }

        [Display(Name = "Գործող է")]
        public bool Active { get; set; } = true;



        [Display(Name = "Նշում 1")]
        public string note1  { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2  { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3  { get; set; }


        [Display(Name = "Նշում 4")]
        public string note4  { get; set; }


        [Display(Name = "Նշում 5")]
        public string note5  { get; set; }


        [Display(Name = "Մուտքագրող օգտատեր")]
        public string userId  { get; set; }


     


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastModifDate { get; set; } = DateTime.Now;





        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sellers> Sellers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierBranches> SupplierBranches { get; set; }


    }







}