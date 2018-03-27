using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
//using System.Data.Entity.Infrastructure.Annotations;
//using System.Data.Entity.ModelConfiguration.Configuration;

namespace ASFront.Models
{
    public class Regions
    {

        [Key]
        [Display(Name = "Id")]
        public int RegionsId  { get; set; }


        [Display(Name="Տարածաշրջան")]
        public string Region  { get; set; }


    }

    //public class ASFrontDbContext:DbContext
    //{
    //    public ASFrontDbContext()
    //        :base("DefaultConnection")
    //    {

    //    }        

    //}

}