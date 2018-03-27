using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASFront.Models;


namespace ASFront.ModelsView
{
    public class UserAppTableView
    {
        [Display(Name ="Պրոդուկտ")]
        public int? prodoctId { get; set; }
        [Display(Name = "Պրոդուկտի սահմանափակումներ")]
        public int? prodLimitId { get; set; }
        [Display(Name = "Մասնաճյուղ")]
        public int? branchId { get; set; }
        [Display(Name = "Օգտատեր")]
        public string userId { get; set; }

        [Display(Name = "Օգտատերերի սահմանափակումների աղյուսակ")]
        public List<UserAppTable> usrAppTbl { get; set; }



        public UserAppTableView()
        {
            usrAppTbl = new List<Models.UserAppTable>();
        }
    }
}