using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    public class FamilyCostNormatives
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }


        [ForeignKey("Branches")]
        [Display(Name = "Մասնաճյուղ")]
        public int BrancheId { get; set; }


        [Display(Name = "Առավելագույն տարիքը")]
        public int maxAge  { get; set; }


        [Display(Name = "Ծախս")]
        public double Cost { get; set; }



        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում3")]
        public string note3 { get; set; }


        [Display(Name = "Նշում4")]
        public string note4 { get; set; }

        public virtual Branches Branches { get; set; }

    }

}