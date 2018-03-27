using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.ModelsView
{
    public class UserAppView
    {
        [Display(Name = "Հայտ")]
        public long appId { get; set; }

        [Display(Name = "Հաստատման տիպ")]
        public int aprStatus { get; set; }

        [Display(Name = "Վարկային մասնագետ")]       
        public string cuserId { get; set; }

        [Display(Name = "Հայտի գումար")]
        public double appSum { get; set; }

        [Display(Name = "Վարկի գումար")]
        public double creditSum { get; set; }

        [Display(Name = "Վարկի ժամկետ")]
        public int credDur { get; set; }

        [Display(Name = "Արտոնյալ ժամկետ")]
        public int grPeriod { get; set; }

        [Display(Name = "Հաստատման նախապայման")]
        public string preCondit { get; set; }

        [Display(Name = "Ապահովագրություն")]
        public string insurance { get; set; }

        [Display(Name = "Գրավի պահանջ")]
        public string collateral { get; set; }

        [Display(Name = "Նշում 1")]
        public string note1 { get; set; }
        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }
        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }

        [Display(Name = "Արժույթ")]
        public string appCurrency { get; set; }

    }

    public class AppErrorView
    {
        [Display(Name = "Սխալի մասին հաղորդագրություն")]
        public string appErrorDescr { get; set; }
    }

}