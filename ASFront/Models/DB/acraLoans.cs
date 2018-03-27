namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class acraLoans
    {
        public long acraLoansId { get; set; }

        public long acraId { get; set; }

        public long resultId { get; set; }

        public long clienId { get; set; }

        public string lType { get; set; }

        public string creditID { get; set; }

        public string cCreditSource { get; set; }

        public double creditAmount { get; set; }

        public int currencyId { get; set; }

        public DateTime creditingDate { get; set; }

        public DateTime creditCloseDate { get; set; }

        public double iterest { get; set; }

        public double balance { get; set; }

        public DateTime lastPaymentDate { get; set; }

        public string loanClass { get; set; }

        public int DelayPaymentQuantity1_12 { get; set; }

        public int DelayPaymentQuantity13_24 { get; set; }

        public int DelayPaymentFrequency1_12 { get; set; }

        public int DelayPaymentFrequency13_24 { get; set; }

        public string creditStatus { get; set; }

        public double contractAmount { get; set; }

        public string creditScope { get; set; }

        public string pledgeSubject { get; set; }

        public string collateralNotes { get; set; }

        public double collateralAmount { get; set; }

        public int collateralCurrencyId { get; set; }

        public double pmt { get; set; }
    }
}
