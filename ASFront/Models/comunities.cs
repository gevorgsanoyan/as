using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class comunities
    {
        [Key]
        [Display(Name = "Id")]
        public int comunityId { get; set; }


        [Display(Name = "Մարզ")]
        public string reg { get; set; }


        [Display(Name = "Համայնք")]
        public string cName { get; set; }


        [Display(Name = "Տեղամաս")]
        public string areaCode { get; set; }



    }



   



}