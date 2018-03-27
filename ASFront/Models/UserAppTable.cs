using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ASFront.Models
{
    public class AppTypes
    {
        [Key]
        [Display(Name = "Id")]
        public int AppTypesId { get; set; }

        [Display(Name = "Հաստատման տեսակ")]
        public string appType { get; set; }
    }

    public class UserAppTableDetView
    {
        [Display(Name = "Id")]
        public int UserAppTableId { get; set; }

        [Display(Name = "Օգտատեր")]
        public string userId { get; set; }

        [Display(Name = "Կարգավիճակ")]
        public bool isActive { get; set; } = true;

        [Display(Name = "Ստեղծման ամսաթիվ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime createDate { get; set; }


        [Display(Name = "Փոփոխման ամսաթիվ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime changeDate { get; set; }

        [Display(Name = "Պրոդուկտ")]
        public string productName { get; set; }

        [Display(Name = "Արժույթ")]
        public string prodCurrency { get; set; }

        [Display(Name = "Հաստատման տիպ")]
        public string prodLimitId { get; set; }

        [Display(Name = "Հաստատման առավելագույն գումար")]
        public double maxLimit { get; set; }


        [Display(Name = "Ստորաբաժանում")]
        public string BrancheId { get; set; }

        [Display(Name = "Հաստատման տեսակ")]
        public string appTypeId { get; set; }

        [Display(Name = "Օգտատեր")]
        public string userNameFull { get; set; }
    }


    public class UserAppTable
    {
        [Key]
        [Display(Name = "Id")]
        public int UserAppTableId { get; set; }

        
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Օգտատեր")]
        public string userId { get; set; }

        [Display(Name = "Կարգավիճակ")]
        public bool isActive { get; set; } = true;

        [Display(Name = "Ստեղծման ամսաթիվ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime createDate { get; set; }


        [Display(Name = "Փոփոխման ամսաթիվ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime changeDate { get; set; }


        [Display(Name = "Հաստատման տիպ")]
        public int prodLimitId { get; set; }

        [Display(Name = "Հաստատման առավելագույն գումար")]
        public double maxLimit { get; set; }


        [Display(Name = "Կատարող")]
        public string cUserId { get; set; }

        public virtual Branches Branches { get; set; }
        
        [ForeignKey("Branches")]
        [Display(Name = "Ստորաբաժանում")]
        public int? BrancheId { get; set; }

        public virtual AppTypes AppTypes { get; set; }
        [ForeignKey("AppTypes")]
        [Display(Name = "Հաստատման տեսակ")]
        public int? appTypeId { get; set; }
    }
}