using ASFront.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class AgroAssetsViewModel
    {



        public AgroAssetsViewModel() { }
        public AgroAssetsViewModel(Balance item) : this()
        {
            this.Id = item.Id;                                         
            this.clientId = item.clientId;
            this.PreparationDate = item.PreparationDate.ToShortDateString();
            this.note1 = item.note1;
            this.note2 = item.note2;
            this.note3 = item.note3;
            this.Cash = item.Cash.ToString("N0");
            this.BankAccount = item.BankAccount.ToString("N0");
            this.AccountReceivable = item.AccountReceivable.ToString("N0");
            this.Inventory = item.Inventory.ToString("N0");
            this.OtherCurrentAssets = item.OtherCurrentAssets.ToString("N0");
            this.TotalCurrentAssets = (item.Cash + item.BankAccount + item.AccountReceivable + item.Inventory + item.OtherCurrentAssets).ToString("N0");

            this.Equipments = item.Equipments.ToString("N0");
            this.Vehicles = item.Vehicles.ToString("N0");
            this.PropertyPlantes = item.PropertyPlantes.ToString("N0");
            this.OtherNonCurrentAssets = item.OtherNonCurrentAssets.ToString("N0");
            this.TotalNonCurrentAssets = (item.Equipments + item.Vehicles + item.PropertyPlantes + item.OtherNonCurrentAssets ).ToString("N0");







            this.MediumLongTermLoans = item.MediumLongTermLoans.ToString("N0");
            this.OtherNonCurrentLiabilitiues = item.OtherNonCurrentLiabilitiues.ToString("N0");
          
            this.TotalCurrentLiabilities = (item.MediumLongTermLoans + item.OtherNonCurrentLiabilitiues).ToString("N0");





            this.ShortTermLoans = item.ShortTermLoans.ToString("N0");
            this.AccountPayable = item.AccountPayable.ToString("N0");
            this.OtherCurrentLiabilities = item.OtherCurrentLiabilities.ToString("N0");
            this.TotalNonCurrentLiabilitiues = (item.ShortTermLoans + item.AccountPayable + item.OtherCurrentLiabilities).ToString("N0");



            /////////////////////////

            ///////////////////////


            this.OwnCapital = 0.ToString("N0");
            this.LiabilityCapital = 0.ToString("N0");

            this.CurrentExperience = 0.ToString("N0");
            this.CreditLoadAcra = 0.ToString("N0");
            this.OverdueMoney = 0.ToString("N0");

        }


        [Display(Name = "ID")]
        public long Id { get; set; } = 0;


        [Display(Name = "Հաճախորդի ID")]
        public long clientId { get; set; }


        /// <summary>
        /// ////////////  1
        /// </summary>
       
        [Display(Name = "Կազմման ամսաթիվ")]
        [DataType(DataType.Date)]
     

        public string PreparationDate { get; set; }


        [Display(Name = "Նշում: Հաշվեկշռի հետ կապված")]
        public string note1 { get; set; }


        [Display(Name = "Նշում 2")]
        public string note2 { get; set; }


        [Display(Name = "Նշում 3")]
        public string note3 { get; set; }




        /// <summary>
        /// /////// 2
        /// </summary>


       
        [Display(Name = "Դրամարկղ")]
        public string Cash { get; set; }


       
        [Display(Name = "Հաշվարկային հաշիվ")]
        public string BankAccount { get; set; }

       
        [Display(Name = "Դեբիտորական պարտք")]
        public string AccountReceivable { get; set; }

       
        [Display(Name = "ԱՆՊ")]
        public string Inventory { get; set; }

       
        [Display(Name = "Այլ ընթացիկ ակտիվներ")]
        public string OtherCurrentAssets { get; set; }


         
        [Display(Name = "ԸՆԴԱՄԵՆԸ ԸՆԹԱՑԻԿ ԱԿՏԻՎՆԵՐ")]
        public string TotalCurrentAssets { get; set; }
        /// <summary>
        /// ////////////3
        /// </summary>
       
        [Display(Name = "Սարքավորումներ")]
        public string Equipments { get; set; } 

       
        [Display(Name = "Շարժական գույք")]
        public string Vehicles { get; set; }

       
        [Display(Name = "Անշարժ գույք")]
        public string PropertyPlantes { get; set; }



       
        [Display(Name = "Այլ հիմնական միջոցներ")]
        public string OtherNonCurrentAssets { get; set; }


        [Display(Name = "ԸՆԴԱՄԵՆԸ ՈՉ ԸՆԹԱՑԻԿ ԱԿՏԻՎՆԵՐ")]
        public string TotalNonCurrentAssets { get; set; }
        /// <summary>
        /// ////////// 4
        /// </summary>



        [Display(Name = "Կարճաժամկետ վարկեր")]
        public string ShortTermLoans { get; set; }

       
        [Display(Name = "Կրեդիտորական պարտք")]
        public string AccountPayable { get; set; }




       
        [Display(Name = "Այլ ընթացիկ պարտավորություններ")]
        public string OtherCurrentLiabilities { get; set; }



        [Display(Name = "ԸՆԴԱՄԵՆԸ ԸՆԹԱՑԻԿ ՊԱՐՏԱՎՈՐՈՒԹՅՈՒՆՆԵՐ")]
        public string TotalCurrentLiabilities { get; set; }
        /// <summary>
        /// ////////// 5
        /// </summary>


        [Display(Name = "Միջին-Երկար վարկեր")]
        public string MediumLongTermLoans { get; set; }


       
        [Display(Name = "Այլ ոչ ընթացիկ պարտավորություններ")]
        public string OtherNonCurrentLiabilitiues { get; set; }



        [Display(Name = "ԸՆԴԱՄԵՆԸ ՈՉ ԸՆԹԱՑԻԿ ՊԱՐՏԱՎՈՐՈՒԹՅՈՒՆՆԵՐ	")]
        public string TotalNonCurrentLiabilitiues { get; set; }


        ///////////////////

        [Display(Name = "ՍԵՓԱԿԱՆ ԿԱՊԻՏԱԼ")]
        public string OwnCapital { get; set; }

        [Display(Name = "ՊԱՐՏՔ/ԿԱՊԻՏԱԼ")]
        public string LiabilityCapital { get; set; }

        //////////


        [Display(Name = "ԸՆԹԱՑԻԿ ԻՐԱՑՎԵԼԻՈՒԹՅՈՒՆ")]
        public string CurrentExperience { get; set; }


        //////////


        [Display(Name = "Վարկային բեռ/ԱՔՌԱ")]
        public string CreditLoadAcra { get; set; }

        [Display(Name = "Ժամկետանց գումար")]
        public string OverdueMoney { get; set; }


        //////////
        /////
        /// <summary>
        /// /////////
        /// </summary>



        [Display(Name = "ՍԵՓԱԿԱՆ ԿԱՊԻՏԱԼ")]
        public double OwnCapitalNum { get; set; } = 0;

        [Display(Name = "ՊԱՐՏՔ/ԿԱՊԻՏԱԼ")]
        public double LiabilityCapitalNum { get; set; } = 0;

        //////////


        [Display(Name = "ԸՆԹԱՑԻԿ ԻՐԱՑՎԵԼԻՈՒԹՅՈՒՆ")]
        public double CurrentExperienceNum { get; set; } = 0;


        //////////


        [Display(Name = "Վարկային բեռ/ԱՔՌԱ")]
        public double CreditLoadAcraNum { get; set; } = 0;

        [Display(Name = "Ժամկետանց գումար")]
        public double OverdueMoneyNum { get; set; } = 0;





    }
}