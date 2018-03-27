using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ASFront.Models
{
    public class acraRequestInit
    {
        [Display(Name = "Հաճախորդի անունը")]
        public string cFirstName { get; set; }


        [Display(Name = "Հաճախորդի ազգանունը")]
        public string cLastName { get; set; }


        [Display(Name = "Սոց.քարտ")]
        public string socNumb { get; set; }


        [Display(Name = "Հաճ.համար")]
        public string cNumber { get; set; }



        public acraRequestInit()
        { }
        public acraRequestInit(string socN)
        {
            ApplicationDbContext db = new Models.ApplicationDbContext();
            var cl = db.clients.Where(c => c.socNumb.Equals(socN)).ToList();
            if (cl.Count > 0)
            {
                cFirstName = cl[0].clientName.TrimEnd();
                cLastName = cl[0].clientLastName.TrimEnd();
                cNumber = cl[0].clientId.ToString();
                socNumb = socN.TrimEnd();
            }//if(cl.Count > 0)
        }//public acraRequestInit(string socN)
    }//public class acraRequestInit


    public class acras
    {
        [Key]

        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long acraId { get; set; }


        [Display(Name = "ԱՔՌԱ հարցման ID")]
        public long resultId { get; set; }


        [Display(Name = "Հաճախորդի համակարգային ID")]
        public long clientId { get; set; }



        [Display(Name = "Հաճախորդի անուն")]
        public string firstName { get; set; }


        [Display(Name = "Հաճախորդի ազգանուն")]
        public string lastName { get; set; }


        [Display(Name = "Ծննդ. ամսաթիվ")]
        [DataType(DataType.Date)]
        public DateTime dob { get; set; }


        [Display(Name = "Անձնագիր")]
        public string passp { get; set; }


        [Display(Name = "Սոց.քարտ")]
        public string socNumb { get; set; }


        [Display(Name = "Հասցեն")]
        public string cAddress { get; set; }



        [Display(Name = "Աշխ. տվ. թարմացման ամսաթիվ")]
        [DataType(DataType.Date)]
        public DateTime incUpdateDate { get; set; }


        [Display(Name = "Աշխատանքային տվյալներ")]
        public string employm { get; set; }



        [Display(Name = "Հարցումների քանակ (վերջին 30 օր)")]
        public int req30Count { get; set; }


        [Display(Name = "Հարցումների քանակ")]
        public int reqCount { get; set; }


        [Display(Name = "Ուշացումներ")]
        public int delay_tot { get; set; }


        [Display(Name = "Ուշացումներ (Առավելագույնը)")]
        public int delayMax { get; set; }



        [Display(Name = "Ընթացիկ պատրավորություններ ՀՀ Դրամ")]
        public double totLiabAMD { get; set; }


        [Display(Name = "Ընթացիկ պատրավորություններ ԱՄՆ Դոլար")]
        public double totLiabUSD { get; set; }


        [Display(Name = "Ընթացիկ երաշխավորություններ ՀՀ Դրամ")]
        public double totGuarAMD { get; set; }


        [Display(Name = "Ընթացիկ երաշխավորություններ ԱՄՆ Դոլար")]
        public double totGuarUSD { get; set; }



        [Display(Name = "Ժամկետանց պատրավորություններ ՀՀ Դրամ")]
        public double totOverdueAMD { get; set; }


        [Display(Name = "Ժամկետանց պատրավորություններ ԱՄՆ Դոլար")]
        public double totOverdueUSD { get; set; }



        [Display(Name = "Ժամկետանց պատրավորություններ ՀՀ Դրամ (երաշխ.)")]
        public double totOverdueGAMD { get; set; }


        [Display(Name = "Ժամկետանց պատրավորություններ ԱՄՆ Դոլար (երաշխ.)")]
        public double totOverdueGUSD { get; set; }

        [Display(Name = "Առավելագույն գումարով ստացված վարկ")]
        public double maxLoan { get; set; }

        [Display(Name = "Ստացված վարկերի քանակը")]
        public double rcvLoansCount { get; set; }

        [Display(Name = "ՍԵՖ-ում ստացված վարկերի գումարը")]
        public double sefLoansSum { get; set; }
        [Display(Name = "ՍԵՖ-ում ստացված վարկերի քանակը")]
        public double sefLoansCount { get; set; }

        [Display(Name = "Կատարման ամսաթիվ")]
        [DataType(DataType.Date)]
        public DateTime reqDate { get; set; }

        [Display(Name = "Հարցում կատարողը")]
        public string userId { get; set; }


        [Display(Name = "Հաշվետվության համար")]
        public string ReportNumber { get; set; }

        [Display(Name = "Նույնականացման քարտ")]
        public string IdCardNumber { get; set; }

        [Display(Name = "Տվյալներ սնանկության վերաբերյալ")]
        public string PersonBankruptIncome { get; set; }

        [Display(Name = "Դասերի փոփոխությունների հանրագումարը")]
        public string SwitchisClassQuantity { get; set; }

        [Display(Name = "Վերջին թարմացման ամսաթիվը")]
        public string InformationReviewDate { get; set; }

        [Display(Name = "Գործող  վարկերի/ երաշխավորությունների խստագույն ռիսկի դասը")]
        public string TheWorstClassLoan { get; set; }

        [Display(Name = "Անձի վերաբերյալ վերջին 30 օրվա ընթացքում կատարված ինքնահարցումների քանակը")]
        public string SelfEnquiryQuantity30 { get; set; }

        [Display(Name = "Անձի վերաբերյալ վերջին 1 տարվա  ընթացքում կատարված ինքնահարցումների քանակը")]
        public string SelfEnquiryQuantity { get; set; }

        [Display(Name = "Վճարումների ուշացումների ընդհանուր քանակը")]
        public string DelayPaymentQuantity { get; set; }

        [Display(Name = "Գործող  վարկերի/ երաշխավորությունների խստագույն ռիսկի դասը")]
        public string TheWorsClassGuarantee { get; set; }

    }//public class acras

    public class acraRequestesInfo
    {
        [Display(Name = "Հարցման ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long acraRequestesInfoId { get; set; }


        [Display(Name = "ԱՔՌԱ ID")]
        public long acraId { get; set; }


        [Display(Name = "ԱՔՌԱ հարցման ID")]
        public long resultId { get; set; }

        [Display(Name = "Համար")]
        public string vNumber { get; set; }

        [Display(Name = "Հարցում կատարող")]
        public string vBankName { get; set; }

        [Display(Name = "Հարցման ամսաթիվ")]
        public string vDate { get; set; }

        [Display(Name = "Նպատակ")]
        public string vReason { get; set; }

        [Display(Name = "Ենթանպատակ")]
        public string vSubReason { get; set; }

    }

    public class acraInterrelated
    {
        [Display(Name = "Փոխկապակց. ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long acraInterrelatedId { get; set; }


        [Display(Name = "ԱՔՌԱ ID")]
        public long acraId { get; set; }


        [Display(Name = "ԱՔՌԱ հարցման ID")]
        public long resultId { get; set; }

        [Display(Name = "Վարկի համար")]
        public string iNumber { get; set; }

        [Display(Name = "Վարկի ամսաթիվ")]
        public string iCreditStart { get; set; }

        [Display(Name = "Վարկի մարման ամսաթիվ")]
        public string iLastInstallment { get; set; }

        [Display(Name = "Պայմանագրով նախատեսված վարկի գումարը")]
        public double? iContractAmount { get; set; }

        [Display(Name = "Փաստացի մայր գումարի մնացորդը")]
        public double? iAmountDue { get; set; }

        [Display(Name = "Ժամկետանց մայր գումարի մնացորդը")]
        public double? iAmountOverdue { get; set; }

        [Display(Name = "Ժամկետանց տոկոսը")]
        public double? iOutstandingPercent { get; set; }

        [Display(Name = "Վարկը ժամկետանց դառնալու ամսաթիվը")]
        public string iOutstandingDate { get; set; }

        [Display(Name = "Արժույթ")]
        public string iCurrency { get; set; }

        [Display(Name = "Վարկային ռիսկի դասը")]
        public string iCreditClassification { get; set; }

        [Display(Name = "Փոխկապակցված անձի համար")]
        public string vDebtorNum { get; set; }

        [Display(Name = "Փոխկապակցվածության աղբյուր")]
        public string vInterrelatedSourceName { get; set; }

    }

    public class acraLoans
    {
        [Display(Name = "Վարկի ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long acraLoansId { get; set; }


        [Display(Name = "ԱՔՌԱ ID")]
        public long acraId { get; set; }


        [Display(Name = "ԱՔՌԱ հարցման ID")]
        public long resultId { get; set; }


        [Display(Name = "Հաճախորդի համակարգային ID")]
        public long clientId { get; set; }



        [Display(Name = "Վարկի տիպը")]
        public string lType { get; set; }


        [Display(Name = "Վարկի ԱՔՌԱ ID")]
        public string creditID { get; set; }


        [Display(Name = "Վարկատու")]
        public string cCreditSource { get; set; }



        [Display(Name = "Վարկի գումար")]
        public double creditAmount { get; set; }



        [Display(Name = "Վարկի արժույթ")]
        public int currencyId { get; set; }



        [Display(Name = "Վարկի ամսաթիվ")]
        [DataType(DataType.Date)]
        public DateTime creditingDate { get; set; }



        [Display(Name = "Վարկի մարման ամսաթիվ")]
        [DataType(DataType.Date)]
        public DateTime creditCloseDate { get; set; }



        [Display(Name = "Վարկի տոկոս")]
        public double iterest { get; set; }



        [Display(Name = "Վարկի մնացորդ")]
        public double balance { get; set; }



        [Display(Name = "Վարկի վերջին վճարման ամսաթիվ")]
        [DataType(DataType.Date)]
        public DateTime lastPaymentDate { get; set; }



        [Display(Name = "Վարկի դասը")]
        public string loanClass { get; set; }



        [Display(Name = "Ուշացումների քանակը 1-12 (ամիսներ)")]
        public int DelayPaymentQuantity1_12 { get; set; }



        [Display(Name = "Ուշացումների քանակը 13-24 (ամիսներ)")]
        public int DelayPaymentQuantity13_24 { get; set; }



        [Display(Name = "Ուշացումների հաճախականությունը 1-12 (ամիսներ)")]
        public int DelayPaymentFrequency1_12 { get; set; }



        [Display(Name = "Ուշացումների հաճախականությունը 13-24 (ամիսներ)")]
        public int DelayPaymentFrequency13_24 { get; set; }



        [Display(Name = "Վարկի կարգավիճակը")]
        public string creditStatus { get; set; }



        [Display(Name = "Պայմանագրի գումարը")]
        public double contractAmount { get; set; }



        [Display(Name = "Վարկի տեսակը")]
        public string creditScope { get; set; }

        [Display(Name = "CreditUsePlace")]
        public string CreditUsePlace { get; set; }

        [Display(Name = "PersonCount")]
        public string PersonCount { get; set; }

        [Display(Name = "GuarantorCount")]
        public string GuarantorCount { get; set; }

        [Display(Name = "Գրավի առարկա")]
        public string pledgeSubject { get; set; }



        [Display(Name = "Գրավի նշումներ")]
        public string collateralNotes { get; set; }



        [Display(Name = "Գրավի արժեք")]
        public double collateralAmount { get; set; }



        [Display(Name = "Գրավի արժեքի արժույթ")]
        public int collateralCurrencyId { get; set; }



        [Display(Name = "PMT")]
        public double pmt { get; set; }

        [Display(Name = "Եկամուտների թարմացման ամսաթիվ")]
        public DateTime? IncomingDate { get; set; }

        [Display(Name = "Ընդ. ժամկետանց գումար")]
        public double? AmountOverdue { get; set; }

        [Display(Name = "Վարկի ամսաթիվ")]
        public DateTime? CreditStart { get; set; }

        [Display(Name = "Ժամկետանցության ամսաթիվ")]
        public DateTime? OutstandingDate { get; set; }

        [Display(Name = "Ժամկետանց օրեր")]
        public int? OverdueDays { get; set; }

        [Display(Name = "Ժամկետանց %")]
        public double? OutstandingPercent { get; set; }

        [Display(Name = "Վճարում")]
        public double? PaymentAmount { get; set; }

        [Display(Name = "Դասակարգման ամսաթիվ")]
        public DateTime? ClassificationLastDate { get; set; }

        [Display(Name = "Վարկի նշում")]
        public string CreditNotes { get; set; }

        [Display(Name = "Երկարացումների քանակ")]
        public int? ProlongationsNum { get; set; }

        [Display(Name = "Վարկի տիպ")]
        public string CreditType { get; set; }

        [Display(Name = "Երաշխավորության չափ")]
        public double? GuarantorAmount { get; set; }

        public long? loanDBId { get; set; }

    }//public class acraLoans



    public class acraLoanosDays
    {
        [Display(Name = "Օրվա ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long acraLoanosDaysId { get; set; }


        [Display(Name = "Վարկի/Երաշխ. ID")]
        public long loanId { get; set; }


        [Display(Name = "Տարի")]
        public string osYear { get; set; }

        [Display(Name = "Ամիս")]
        public string osMoth { get; set; }

        [Display(Name = "Արժեք")]
        public string osValue { get; set; }

    }

    

    public class acraLoanDetails
    {
        [Display(Name = "Հաճախորդ")]
        public string clientName { get; set; }

        [Display(Name = "Պայմանագրի համար")]
        public string credit_id { get; set; }

        [Display(Name = "Թարմացման ամսաթիվ")]
        public string incoming_date { get; set; }

        [Display(Name = "Վարկատու")]
        public string lender { get; set; }

        [Display(Name = "Տիպ")]
        public string l_type { get; set; }

        [Display(Name = "Կարգավիճակ")]
        public string status { get; set; }

        [Display(Name = "Պայմանագրի ամսաթիվը")]
        public string credit_date { get; set; }

        [Display(Name = "Տրամադրման ամսաթիվը")]
        public string credit_start { get; set; }

        [Display(Name = "Վերջնական մարման ամսաթիվը")]
        public string close_date { get; set; }

        [Display(Name = "Փաստացի մարման ամսաթիվը")]
        public string last_pmt_date { get; set; }

        [Display(Name = "Վճարման չափը")]
        public string payment_amount { get; set; }

        [Display(Name = "Արժույթ")]
        public string currency { get; set; }

        [Display(Name = "Ոլորտ")]
        public string credit_scope { get; set; }

        [Display(Name = "Վայր")]
        public string use_place { get; set; }

        [Display(Name = "Գրավի առարկա")]
        public string pledge { get; set; }

        [Display(Name = "Գրավի արժեք")]
        public string collateral_value { get; set; }

        [Display(Name = "Գրավի արժույթ")]
        public string clCurrency { get; set; }

        [Display(Name = "Գրավի նշումներ")]
        public string collateral_note { get; set; }

        [Display(Name = "Երաշխավորների քանակ")]
        public string guarantor_count { get; set; }


        [Display(Name = "Երաշխավորության գումար")]
        public string guarantor_amount { get; set; }

        [Display(Name = "Վարկի այլ նշում")]
        public string credit_notes { get; set; }

        [Display(Name = "Վարկի տեսակ")]
        public string credit_type { get; set; }

        [Display(Name = "Տոկոսադրույք")]
        public string Interest { get; set; }


        [Display(Name = "Պայմանագրի գումար")]
        public string contract_amount { get; set; }

        [Display(Name = "Փաստացի տրամադրված գումարը")]
        public string Amount { get; set; }

        [Display(Name = "Փաստացի մարված գումարը")]
        public string AmountBalance { get; set; }

        [Display(Name = "Մայր գումարի մնացորդ")]
        public string balance { get; set; }

        [Display(Name = "Ժամկետանց մայր գումարի մնացորդ")]
        public string amount_overdue { get; set; }

        [Display(Name = "Ժամկետանց դառնալու ամսաթիվ")]
        public string outstanding_date { get; set; }

        [Display(Name = "Ժամկետանց տոկոս")]
        public string outstanding_percent { get; set; }

        [Display(Name = "Ժամկետանց օրերի քանակ")]
        public string overdue_days { get; set; }

        [Display(Name = "Վարկային ռիսկի դասը")]
        public string loan_class { get; set; }

        [Display(Name = "Վերջին դասակարգման ամսաթիվ")]
        public string classification_last_date { get; set; }

        public string[,] lOSdays { get; set; }

    }


}