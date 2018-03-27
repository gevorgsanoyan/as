namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class clientWorkDatas
    {
        public long Id { get; set; }

        public long clientId { get; set; }

        public string companyName { get; set; }

        public string taxNumber { get; set; }

        public string CompanyTel { get; set; }

        public string companyAddress { get; set; }

        public int employmentTypeId { get; set; }

        public int companyTypeId { get; set; }

        public DateTime empRegDate { get; set; }

        public string jobTitle { get; set; }

        public float otherIncome { get; set; }

        public string otherIncomeDescr { get; set; }

        public string note1 { get; set; }

        public string note2 { get; set; }

        public string note3 { get; set; }

        public string note4 { get; set; }

        public string note5 { get; set; }

        public string userId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifDate { get; set; }

        public float salary { get; set; }
    }
}
