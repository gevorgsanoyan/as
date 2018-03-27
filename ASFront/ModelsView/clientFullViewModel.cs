using System;
using System.Collections.Generic;

using ASFront.ModelsView;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASFront.Models
{

    public class MatchingClientViewModel {
        [Display(Name = "ID")]
        public long clientId { get; set; }
        [Display(Name = "Անուն")]
        public string firstName { get; set; }
        [Display(Name = "Ազգանուն")]
        public string lastName { get; set; }
        [Display(Name = "Սոց.քարտ")]
        public string socN { get; set; }
        [Display(Name = "ՍԵՖ-ում ունեցած վարկերի գումարը")]
        public double sefLoanSum { get; set; }
        [Display(Name = "Համընկնման չափանիշ")]
        public string matchingMode { get; set; }

        [Display(Name = "Համընկնած հաճ.")]
        public long mClientId { get; set; }

        [Display(Name = "Համընկնած հաճ. խումբ")]
        public long mClientGroupId { get; set; }
    }

    public class clientFullViewModel
    {
        public clients c { get; set; }
        public clientWorkDatas cw { get; set; }
        public List<MatchingClientViewModel> matchingClients { get; set; }
    }




    public class CompanyFullViewModel
    {
        public BusinessViewModel b { get; set; }
        public clients c { get; set; }
        public BusinessInfo bi { get; set; }


        //public clientWorkDatas cw { get; set; }

    }


    


}