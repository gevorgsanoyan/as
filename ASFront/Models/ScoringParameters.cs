using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.Models
{
    public class ScoringParameters
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public ScoringParameters()
        //{
        //    ScoringIndicatorsParameters = new HashSet<ScoringIndicatorsParameters>();
        //}


        [Key]
        public int ID { get; set; }


        //[Display(Name = "Ինդիկատոր")]
        //public int indicatorID { get; set; }


        [Display(Name = "Մուտքային պարամետրի անվանում")]
        public string InputParameterName { get; set; }


        [Display(Name = "Մուտքային պարամետրի արժեք")]
        public double InputParameterValue { get; set; }



        [Display(Name = "Պարամետր տվյալների տեսակը")]
        public int ParameterDataType { get; set; } = 1;



        [Display(Name = "Աղբյուրը-Աղյուսակ")]
        public string SourceTable { get; set; }


        [Display(Name = "Աբյուրը-Դաշտ")]
        public string SourceField { get; set; }


        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }





        [Display(Name = "Մուտքագրող օգտատեր")]
        public string userId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastModifDate { get; set; } = DateTime.Now;




        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ScoringIndicatorsParameters> ScoringIndicatorsParameters { get; set; }

    }
}