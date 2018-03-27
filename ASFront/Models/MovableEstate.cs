using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    public class MovableEstates
    {


        [Key]
        [Display(Name = "ID")]
        public long Id { get; set; }



        [Required]
        [ForeignKey("MovableEstateTypes")]
        [Display(Name = "Շարժական գույքի տեսակ")]
        public int MovableEstateTypeId { get; set; }


        [Required]
        [ForeignKey("applications")]

        [Display(Name = "Հայտ")]
        public long applicationId { get; set; }




        [Required]
        [Display(Name = "Մակնիշ")]
        public string Name { get; set; }



        [Required]
        [Display(Name = "Նկարագրություն")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Շուկայական արժեք (ՀՀ դրամ)")]
        public double mPrice { get; set; }






        [Required]
        [Display(Name = "Գույն")]
        public string Color { get; set; }



        //[Required]
        //[Display(Name = "Մակնիշ")]
        //public string Make { get; set; }



        [Required]
        [Display(Name = "Տարեթիվ")]
        public int Year { get; set; }




        [Required]
        [Display(Name = "Վազք")]
        public double Run { get; set; }




        [Required]
        [Display(Name = "Շարժիչի հզորություն")]
        public double MotorPower { get; set; }


        [Required]
        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public double EstimatedTotalCostAM { get; set; } 


        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }



        public virtual MovableEstateTypes MovableEstateTypes { get; set; }
        public virtual applications applications { get; set; }


    }



}