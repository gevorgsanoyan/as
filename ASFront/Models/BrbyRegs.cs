using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Infrastructure.Annotations;
//using System.Data.Entity.ModelConfiguration.Configuration;

namespace ASFront.Models
{
    public class BrbyRegs
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrbyRegsId  { get; set; }

        [Required]
        public int BranchesId  { get; set; }

        [Required]
        public int RegionsId  { get; set; }


        //[Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime rDate  { get; set; }


        public string userId  { get; set; }


        public int current  { get; set; }



    }

    //public class ASFrontDbContext:DbContext
    //{
    //    public ASFrontDbContext()
    //        :base("DefaultConnection")
    //    {

    //    }        

    //}

}