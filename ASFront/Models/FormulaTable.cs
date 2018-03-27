using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class FormulaTable
    {

        [Key]
        public int Id { get; set; }

        public string Formula { get; set; }
        public string FormulaName { get; set; }

        public string RealFormula { get; set; }

        public string Description { get; set; }

    }
}