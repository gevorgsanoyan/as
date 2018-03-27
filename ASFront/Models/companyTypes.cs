using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class companyTypes
    {

        [Key]
        [Display(Name = "company_type_ID")]
        public int companyTypeID { get; set; }


        [Display(Name = "Կազմակերպության տիպը ")]
        public string companyTypeName { get; set; }


        [Display(Name = "Աշխատակցի տիպը")]
        public int FK_empType { get; set; }
    }

  
}