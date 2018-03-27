using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Էլ.փոստ")]
        public string Email { get; set; }


    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }


    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }


        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }


        public string ReturnUrl { get; set; }


        public bool RememberMe { get; set; }


    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }



        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }


        public string ReturnUrl { get; set; }



        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }



        public bool RememberMe { get; set; }


    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Էլ.փոստ")]
        public string Email { get; set; }


    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Էլ.փոստ")]
        [EmailAddress]
        public string Email { get; set; }



        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառ")]
        public string Password { get; set; }



        [Display(Name = "Հիշե՞լ")]
        public bool RememberMe { get; set; }


    }

    public class RegisterViewModel
    {
        public string Name { get; set; }



        [Required]
        [EmailAddress]
        [Display(Name = "Էլ.փոստ")]
        public string Email { get; set; }



        [Required]
        [StringLength(100, ErrorMessage = "{0} - ը պետք է լինի առնվազն {2} նիշ:", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառ")]
        public string Password { get; set; }



        [DataType(DataType.Password)]
        [Display(Name = "Հաստատել գաղտնաբառը")]
        [Compare("Password", ErrorMessage = "Գաղտնաբառը եւ հաստատման գաղտնաբառը չեն համապատասխանում:")]
        public string ConfirmPassword { get; set; }


    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Էլ.փոստ")]
        public string Email { get; set; }



        [Required]
        [StringLength(100, ErrorMessage = "{0} - ը պետք է լինի առնվազն {2} նիշ:", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառ")]
        public string Password { get; set; }



        [DataType(DataType.Password)]
        [Display(Name = "Հաստատել գաղտնաբառը")]
        [Compare("Password", ErrorMessage = "Գաղտնաբառը եւ հաստատման գաղտնաբառը չեն համապատասխանում:")]
        public string ConfirmPassword { get; set; }



        public string Code { get; set; }


    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Էլ.փոստ")]
        public string Email { get; set; }


    }
    public class NameIdView
    {

      

        
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Display(Name = "Մասնաճյուղ")]
    
        public string Name { get; set; }


      
    }


    //public class UserBranches
    //{
    //    public IEnumerable<int> SelectedBranche { get; set; } = new List<int>();
    //}



    public class UserProfileView
    {
        [Key]
        [Display(Name = "Օգտագործողի Id")]
        public string UserId { get; set; }


        [Display(Name = "Անուն")]
        [Required]
        public string FirstName { get; set; }



        [Display(Name = "Ազգանուն")]
        [Required]
        public string LastName { get; set; }



        [Display(Name = "Հայրանուն")]
        public string Patronymic { get; set; }


        public int UserASProfileId { get; set; }


        [Display(Name = "Օգտագործող")]
        public string UserName { get; set; }



        [Required]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "EmailFormat")]

        [Display(Name = "Էլ. փոստ")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Դեր")]
        public string Role { get; set; }


     
        [Display(Name = "Դեր")]
        public string RoleName { get; set; }



        [Display(Name = "Մասնաճյուղ")]
        public int branchId { get; set; }

        [Display(Name = "Օգտագործողի Մասնաճյուղերը")]
        public int[] Countries { get; set; }

        [Display(Name = "Օգտագործողի Մասնաճյուղերը")]
        public int[] UserBranches { get; set; }

        [Display(Name = "Օգտագործողի Մասնաճյուղերը")]
        public IList<string> SelectedBranchesStr { get; set; }


        [Display(Name = "Օգտագործողի Մասնաճյուղերը")]
        public string SelectedStr { get; set; }


        [Display(Name = "Մասնաճյուղ")]
        public string branch { get; set; }


        [Display(Name = "ՀԾ օգտագ. անուն")]
        public string asName { get; set; }


        [Display(Name = "ՀԾ օգտագ. կոդ")]
        public string asCode { get; set; }


        [Display(Name = "ՀԾ օգտագ. Id")]
        public string asUserId { get; set; }


        [Display(Name = "Հեռախոսահամար")]
        public string uPhoneNumber { get; set; }

        [Display(Name = "Telegram ID")]
        public string telegramId { get; set; }

        [Display(Name = "Telegram Հեռախոսահամար")]
        public string telegramPhoneNumber { get; set; }

    }

}
