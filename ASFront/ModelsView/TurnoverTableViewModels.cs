using ASFront.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


using System;

using System.Linq;

namespace ASFront.ModelsView
{
    public class TurnoverTableViewModels
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public TurnoverTableViewModels() { }

        public TurnoverTableViewModels(long ApplicationID = 0) : this()
        {
            var turnovers = db.Turnovers.Where(p => p.applicationId == ApplicationID).ToList() ?? new List<Turnovers>();
            double _AcquisitionSum = turnovers.Sum(p => (p.MonthlySalesQuantity * p.COGS));

            double _AverageWeightedUpward = 0;

            foreach (var p in turnovers)
            {
                TurnoverTableRowViewModels t = new TurnoverTableRowViewModels();

                t.Acquisition = p.MonthlySalesQuantity * p.COGS;
                if (_AcquisitionSum != 0)
                    t.inTotal = t.Acquisition / _AcquisitionSum * 100;


                _AverageWeightedUpward += p.Overgrown * t.inTotal;

                Table.Add(t);
            }

            AverageWeightedUpward = _AverageWeightedUpward/100;
        }




        [Display(Name = "Միջին կշռված վերադիր")]
        public double AverageWeightedUpward { get; set; } = 0;





        public List<TurnoverTableRowViewModels> Table { get; set; } = new List<TurnoverTableRowViewModels>();







    }

    public class TurnoverTableRowViewModels
    {



        [Display(Name = "Id")]
        public int Id { get; set; }






        [Display(Name = "Հայտ")]
        public long applicationId { get; set; }




        [Display(Name = "Ապրանք")]
        public string productName { get; set; }





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



        [Display(Name = "Ձեռքբերում")]
        public double Acquisition { get; set; } = 0;




        [Display(Name = "ընդհանուրի մեջ")]
        public double inTotal { get; set; } = 0;




        [Display(Name = "Նշում1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում3")]
        public string note3 { get; set; }

    }
}