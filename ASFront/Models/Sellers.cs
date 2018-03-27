using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    public class Sellers
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }


        [Display(Name = "Օգտագործողի անունը")]
        public string UserName { get; set; }


        [ForeignKey("Suppliers")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [Display(Name = "Մատակարար")]
        public long SupplierId { get; set; }

        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [Display(Name = "Մատակարարողի մասնաճյուղ")]
        public long BrancheId { get; set; }




        [Display(Name = "Անուն")]
        [Required]
        public string FirstName { get; set; }



        [Display(Name = "Ազգանուն")]
        [Required]
        public string LastName { get; set; }



        [Display(Name = "Հայրանուն")]
        public string Patronymic { get; set; }



        [Display(Name = "Էլ փոստ")]
        [StringLength(250)]
        public string Email { get; set; }


        [Display(Name = "Հեռախոս")]
        public string Phone { get; set; }


        [Display(Name = "Մեկնաբանություն")]
        public string Comment { get; set; }


        [Display(Name = "TelegramID")]
        public string TelegramID { get; set; }



        [Display(Name = "Պաշտոն")]
        public string Position { get; set; }



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





        public virtual Suppliers Suppliers { get; set; }
        public virtual SupplierBranches SupplierBranchs { get; set; }
    }







}