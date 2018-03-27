using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{
    public class clientsGroup
    {
        [Key]
        [Display(Name = "ID")]
        public long clientsGroupId { get; set; } 
 

        [Display(Name = "Խումբ")]
        public long groupId { get; set; } 
 


        [Display(Name = "Խմբի անդամ")]
        public long clientId { get; set; } 
 

        [Display(Name = "Փոխկապակցվածություն")]
        public int? relType { get; set; } 
 


        [Display(Name = "Նշում 1")]
        public string note1 { get; set; } 
 

        [Display(Name = "Նշում 2")]
        public string note2 { get; set; } 
 

        [Display(Name = "Նշում 3")]
        public string note3 { get; set; } 
 


        [ForeignKey("relType")]
        public virtual releationType releationType { get; set; } 
 









        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]


        [Display(Name = "Հարցման վերջին ամսաթիվ")]
        public DateTime? AcraLastRequestDate { get; set; } 
 




        [Display(Name = "Վարկային բեռ/ԱՔՌԱ")]
        public double? CreditLoadAcra { get; set; } 
 



        [Display(Name = "Ժամկետանց գումար")]
        public double? OverdueMoney { get; set; } 
 



        [Display(Name = "Ժամկետանց օրերի քանակ")]
        public int? OverdueDaysCount { get; set; } 
 





        [Display(Name = "Եկամուտ")]
        public double? Income { get; set; } 
 




    }
}