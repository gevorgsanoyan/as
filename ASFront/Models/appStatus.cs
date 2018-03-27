using System.ComponentModel.DataAnnotations;

namespace ASFront.Models
{
    public class appStatus
    {
        public int appStatusId  { get; set; }


        public string appStatusArm  { get; set; }


        public string appStatusOther  { get; set; }


        [Display(Name = "Նշում 1")]
        public string note1  { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2  { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3  { get; set; }


    }
}