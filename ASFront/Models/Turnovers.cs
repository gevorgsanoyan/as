using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class Turnovers
    {
        public Turnovers ShallowCopy()
        {
            Turnovers Tr = (Turnovers)this.MemberwiseClone();
            Tr.Id = 0;
            return Tr;
        }


        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Required]
        [ForeignKey("applications")]

        [Display(Name = "Հայտ")]
        public long applicationId { get; set; }


        [Required(ErrorMessage = "*")]

        [Display(Name = "Ապրանք")]
        public string productName { get; set; }



        [Required(ErrorMessage = "*")]
        [ForeignKey("MeasurementUnits")]
        [Display(Name = "Չափի միավոր")]
        public int MeasurementUnitId { get; set; }


        [Required]
        [Display(Name = "Վաճառքի քանակ")]
        public int MonthlySalesQuantity { get; set; }


        [Required(ErrorMessage = "*")]

        [Display(Name = "Ինքնարժեք")]
        public double COGS { get; set; }


        [Display(Name = "Վաճառքի գին")]
        public double SalesPrice { get; set; }


        [Display(Name = "Մնացորդ")]
        public double OutstandingQuantity { get; set; }




        [Display(Name = "Վերադիր")]
        public double Overgrown { get; set; } = 0;


        [Display(Name = "Հասույթ")]
        public double Proceeds { get; set; } = 0;

        [Display(Name = "Գումար-մնացորդ")]
        public double MoneyBalance { get; set; } = 0;


      




        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում3")]
        public string note3 { get; set; }



        public virtual MeasurementUnits MeasurementUnits { get; set; }

        public virtual applications applications { get; set; }


    }
}