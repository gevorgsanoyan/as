using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class BusinessInfo
    {

        [Key]
        [Display(Name = "Id")]
        public long Id { get; set; }





        [Required]
        [ForeignKey("clients")]

        [Display(Name = "Հաճախորդ")]
        public long clientId { get; set; }



        [Required]
        [ForeignKey("BusinessSector")]

        [Display(Name = "Ոլորտ")]
        public int BusinessSectorId { get; set; }



        [Required]
        [ForeignKey("BusinessType")]

        [Display(Name = "Տեսակ")]
        public int BusinessTypeId { get; set; }



        [Required]
        [ForeignKey("OwnershipType")]

        [Display(Name = "Սեփականության ձև")]
        public int OwnershipTypeId { get; set; }




        [Required]
        [ForeignKey("NameofBanks")]

        [Display(Name = "Սպասարկող բանկ")]
        public long NameofBanksId { get; set; }




        [ForeignKey("clientSexes")]
        [Display(Name = "Սեռը")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public int GenderId { get; set; }
















        [Display(Name = "բիզնեսի նկարագրություն")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string businessDescription { get; set; }







        [Display(Name = "Գործունեության ամիսների թիվ ")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public int NumberofMonths { get; set; }


        [Display(Name = "Բանկային հաշվի համար")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string BankAccount { get; set; }




        [Display(Name = "Անուն")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string FirstName { get; set; }



        [Display(Name = "Ազգանուն")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string LastName { get; set; }


        [Display(Name = "Հայրանուն")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string MidName { get; set; }











        [Display(Name = "Ծննդյան թիվ")]
        //[Range(typeof(DateTime), "1/01/1900", "1/01/2060")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DayofBirth { get; set; } = DateTime.Today.Date;






        [StringLength(250)]
        [Display(Name = "Սոց. քարտ")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [MaxLength(10, ErrorMessage = "առավելագույնը 10 նիշ է")]

        public string SocialNumber { get; set; }





        [Display(Name = "Հեռախոս")]
        public string tel { get; set; }


        [Display(Name = "Բջջային հեռ.")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        public string Mobile { get; set; }



        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "EmailFormat")]

        [StringLength(256)]
        [Display(Name = "Էլ. փոստ")]
        public string DirEmail { get; set; }




        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }
        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }





        public virtual clients clients { get; set; }
        public virtual clientSexes clientSexes { get; set; }




        public virtual BusinessSector BusinessSector { get; set; }

        public virtual BusinessType BusinessType { get; set; }

        public virtual OwnershipType OwnershipType { get; set; }


        public virtual NameofBanks NameofBanks { get; set; }






    }
    public class BusinessSector
    {


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessSector()
        {
            BusinessType = new HashSet<BusinessType>();

            BusinessInfo = new HashSet<BusinessInfo>();

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessInfo> BusinessInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessType> BusinessType { get; set; }


        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Required]
        [Display(Name = "Անվանում")]
        public string Name { get; set; }


        [Display(Name = "Նշում")]
        public string note { get; set; }


    }





    public class BusinessType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessType()
        {


            BusinessInfo = new HashSet<BusinessInfo>();

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessInfo> BusinessInfo { get; set; }



        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Required]
        [ForeignKey("BusinessSector")]
        [Display(Name = "Id")]
        public int BusinessSectorId { get; set; }

        [Required]
        [Display(Name = "Անվանում")]
        public string Name { get; set; }


        [Display(Name = "Նշում")]
        public string note { get; set; }


        [ForeignKey("BusinessSectorId")]
        public virtual BusinessSector BusinessSector { get; set; }
    }


    public class NameofBanks
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NameofBanks()
        {


            BusinessInfo = new HashSet<BusinessInfo>();

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessInfo> BusinessInfo { get; set; }

        [Key]
        [Display(Name = "Id")]
        public long Id { get; set; }



        [Required]
        [Display(Name = "Անվանում")]
        public string Name { get; set; }


        [Display(Name = "Նշում")]
        public string note { get; set; }


    }

    public class OwnershipType
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OwnershipType()
        {


            BusinessInfo = new HashSet<BusinessInfo>();

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessInfo> BusinessInfo { get; set; }



        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Required]
        [Display(Name = "Անվանում")]
        public string Name { get; set; }


        [Display(Name = "Նշում")]
        public string note { get; set; }


    }
}