using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class CompanyNameHelpTables
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Display(Name = "Կազմակերպության անվանում")]
        public string companyName { get; set; }
    }

  
}