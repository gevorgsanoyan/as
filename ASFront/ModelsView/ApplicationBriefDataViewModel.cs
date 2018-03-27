using ASFront.Classes;
using ASFront.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{


    public class ApplicationSummaryViewModel
    {
        public AppSumBriefDataViewModel AppSumBriefDataViewModel { get; set; }
        public AppSumDetailPurposeViewModel AppSumDetailPurposeViewModel { get; set; }
        public List<AppSumProductDescriptionViewModel> AppSumProductDescriptionViewModel { get; set; }
        public List<AppSumScoringScoreViewModel> AppSumScoringScoreViewModel { get; set; }
        public AppSumScoringScoreDecisionViewModel AppSumScoringScoreDecisionViewModel { get; set; }
        public List<AppSumDecisionMakingViewModel> AppSumDecisionMakingViewModel { get; set; }
        public AppSumContractViewModel AppSumContractViewModel { get; set; }
        public ApplicationViewModel Application { get; set; }


        public TurnoverAppSumViewModels Turnover { get; set; }

        public BalanceViewModel Balance { get; set; }
        public IncomeExpensesViewModel IncomeExpenses { get; set; }
        public LoanInsuranceViewModel LoanInsurance { get; set; }
        public List<AgroAsset> AgroAssets { get; set; }
        public long clientId { get; set; }
        public long ApplicationID { get; set; }
    }
    public class LoanInsuranceViewModel
    {
        [Display(Name = "Հայտ")]
        public long applicationId { get; set; }


        //public List<GoldCollaterals> GoldCollateralLsit { get; set; }
        //public List<RealtyEstates> RealtyEstateList { get; set; }
        //public List<MovableEstates> MovableEstateList { get; set; }


        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public double GoldCollateralSum { get; set; } = 0;

        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public double RealtyEstateSum { get; set; } = 0;

        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public double MovableEstateSum { get; set; } = 0;




        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public string GoldCollateralSumStr { get; set; }

        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public string RealtyEstateSumStr { get; set; }

        [Display(Name = "Գնահատված ընդհանուր արժեքը (ՀՀ դրամ)")]
        public string MovableEstateSumStr { get; set; }

        [Display(Name = "Վարկ/Գրավ հարաբերակցություն")]
        public string loanCollateral { get; set; }


        public List<GuarantorsViewModel> GuarantorsList { get; set; }
    }
    public class AppSumBriefDataViewModel
    {
        //[Key]
        //[Display(Name = "ID")]
        //public long Id { get; set; }



        [Display(Name = "Հայտ")]
        public string applicationId { get; set; }



        [Display(Name = "Վարկի Ընդհանուր Գումար")]
        public string CreditTotalAmount { get; set; }


        [Display(Name = "Ներդրում")]
        public string Investment { get; set; }



        [Display(Name = "Վարկի Ժամկետ")]
        public string CreditTerm { get; set; }







        [Display(Name = "Պրոդուկտի անվանում")]
        public string productName { get; set; }


        [Display(Name = "Արժույթ")]
        public string productCurrency { get; set; }


        [Display(Name = "Տարեկան տոկոսադրույք")]
        public string anualRate { get; set; }


        [Display(Name = "Դիմումի վճար")]
        public string appFee { get; set; }


        [Display(Name = "Սպասարկման վճար")]
        public string mothFee { get; set; }


        [Display(Name = "Տրման միջնորդավճար")]
        public string upfronFee { get; set; }







        [Display(Name = "Ամսական Վճարման Չափ")]
        public string MonthlyPaymentSize { get; set; }

    }

    public class AppSumDetailPurposeViewModel
    {
        //[Key]
        //[Display(Name = "ID")]
        //public long Id { get; set; }


        [Display(Name = "Հայտը մուտքագրող մասնագետ")]
        public string userName { get; set; }



        [Display(Name = "ՀԾ Կոդ")]
        public string asCode { get; set; }



        [Display(Name = "Մասնաճյուղ")]

        public string Branch { get; set; }



        [Display(Name = "Հայտի բացման ամսաթիվ")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyyթ. MMMM dd}", ApplyFormatInEditMode = true)]
        public DateTime appDate { get; set; }


        [Display(Name = "Հայտի հաստատման ամսաթիվ")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime aprDate { get; set; }



        [Display(Name = "Հայտի կարգավիճակ")]
        public string appStatus { get; set; }



        [DataType(DataType.MultilineText)]
        [Display(Name = "Հայտի Հաստատման մանրամասներ")]
        public string appDescr { get; set; }


    }
    public class AppSumProductDescriptionViewModel
    {
        //[Key]
        //[Display(Name = "ID")]
        //public long Id { get; set; }


        [Display(Name = "Պրոդուկտ-Նպատակ")]
        public string ItemName { get; set; }



        [Display(Name = "Քանակ")]
        public int Count { get; set; }


        [Display(Name = "Գին")]
        public float Price { get; set; }




        [Display(Name = "Ներդրում")]
        public float ClientInvest { get; set; }




        [Display(Name = "Գումար")]
        public float Sum { get; set; }




    }
    public class AppSumScoringScoreViewModel
    {
        //[Key]
        //[Display(Name = "ID")]
        //public long Id { get; set; }




        [Display(Name = "Չափանիշ")]
        public string IndicatorName { get; set; }


        [Display(Name = "Արժեք")]
        public double Value { get; set; }





        [Display(Name = "Գնահատական")]
        public double Score { get; set; }


    }



    public class AppSumScoringScoreDecisionViewModel
    {
        //[Key]
        //[Display(Name = "ID")]
        //public long Id { get; set; }






        [Display(Name = "Գնահատական")]
        public double Score { get; set; }


        [Display(Name = "Գնահատման ամսաթիվ")]
        public DateTime? ScoreDate { get; set; }



        [Display(Name = "Որոշում")]
        public string Decision { get; set; }

    }

    public class AppSumDecisionMakingViewModel
    {


        [Display(Name = "Անուն")]
        public string RowName { get; set; }


        [Display(Name = "Հաստատման Պահանջ")]
        public bool? ApprovalRequirement { get; set; }


        [Display(Name = "Ընթացիկ վիճակ")]
        public string CurrentState { get; set; }



        [Display(Name = "Հաստատող")]
        public string Verifying { get; set; }



        [Display(Name = "Ծանուցում Հաստատողին")]
        public string Notification { get; set; }


        [Display(Name = "Վերջնական որոշում")]
        public string FinalDecision { get; set; }

        [Display(Name = "ButtonClass")]
        public string btnClass { get; set; }

        [Display(Name = "AppGrade")]
        public int AppGrade { get; set; } = 0;


        [Display(Name = "NotificationInfo")]
        public List<NotificationInfo> NotificationInfo { get; set; } = new List<Classes.NotificationInfo>();

        //[Display(Name = "Հայտ")]
        //public string applicationId { get; set; }

    }

    public class AppSumDecisionMakingViewModelOld
    {
        //[Key]
        //[Display(Name = "ID")]
        //public long Id { get; set; }


        [Display(Name = "Գնահատական")]
        public double Score { get; set; }


        [Display(Name = "Օղակ 1")]
        public string Ring1 { get; set; }



        [Display(Name = "Օղակ 2")]
        public string Ring2 { get; set; }



        [Display(Name = "Վերջնական որոշում")]
        public string FinalDecision { get; set; }


        [Display(Name = "Հաստատված վերջնական գումար")]
        public double ApprovedFinalAmount { get; set; }


        [Display(Name = "Հայտ")]
        public string applicationId { get; set; }
    }


    public class AppSumContractViewModel
    {
        //[Key]
        //[Display(Name = "ID")]
        //public long Id { get; set; }

        [Display(Name = "Պայմանագրի համար")]
        public string ContractNumber { get; set; }

    }


}