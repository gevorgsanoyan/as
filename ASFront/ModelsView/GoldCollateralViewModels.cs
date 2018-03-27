using ASFront.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.ModelsView
{
    public class GoldCollateralViewModels
    {
        public List<GoldCollaterals> Table { get; set; }
        public GoldCollaterals InputForm { get; set; }
    }


}