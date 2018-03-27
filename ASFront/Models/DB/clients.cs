namespace ASFront.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class clients
    {
        [Key]
        public long clientId { get; set; }

        public string clientName { get; set; }

        public string clientLastName { get; set; }

        public string clientMidName { get; set; }

        public DateTime dob { get; set; }

        public string passpNumb { get; set; }

        public DateTime passpDate { get; set; }

        public string passpAuth { get; set; }

        public string socNumb { get; set; }

        public string rRegion { get; set; }

        public string rCity { get; set; }

        public string rStreet { get; set; }

        public string rBuilding { get; set; }

        public string rApartment { get; set; }

        public string cRegion { get; set; }

        public string cCity { get; set; }

        public string cStreet { get; set; }

        public string cBuilding { get; set; }

        public string cApartment { get; set; }

        public bool isSameAddress { get; set; }

        public bool isRented { get; set; }

        public string tel { get; set; }

        public string mob1 { get; set; }

        public string mob2 { get; set; }

        public string mob3 { get; set; }

        public string mob4 { get; set; }

        public int fMemb { get; set; }

        public int fEmpMemb { get; set; }

        public int fTenMemb { get; set; }

        public int? edu_educationId { get; set; }

        public int? sex_clientSexId { get; set; }

        public string note1 { get; set; }

        public string note2 { get; set; }

        public string note3 { get; set; }

        public virtual clientSexes clientSexes { get; set; }

        public virtual educations educations { get; set; }
    }
}
