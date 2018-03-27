using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class employmentTypes
    {
        [Key]
        [Display(Name = "Աշխատակցի տիպը")]
        public int empTypeID { get; set; }  
        

        [Display(Name = "Զբաղվածության ոլորտը")]
        public string employment { get; set; }

    }

  
}