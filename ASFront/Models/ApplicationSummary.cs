using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class ApplicationSummary
    {

        [Key]
        [Display(Name = "ID")]
        public long Id { get; set; }


        [Display(Name = "Հայտ")]
        public long HaytID { get; set; }


        [Display(Name = "ScoreValue")]
        public double ScoreValue { get; set; }


    
        [Display(Name = "ScoreDecisionID")]
        public int ScoreDecisionID { get; set; }


        [Display(Name = "ScoreDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime? ScoreDate { get; set; }


        [Display(Name = "App1")]
        public string App1 { get; set; }


        [Display(Name = "App1user")]
        public string App1user { get; set; }



        [Display(Name = "App1Date")]
        [DataType(DataType.Date)]      
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? App1Date { get; set; }


        [Display(Name = "Appfinal")]
        public string Appfinal { get; set; }


        [Display(Name = "Appfinaluser")]
        public string Appfinaluser { get; set; }


        [Display(Name = "AppfinalDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? AppfinalDate { get; set; }


    }
}