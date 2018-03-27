namespace ASFront.Models.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<acraLoans> acraLoans { get; set; }
        public virtual DbSet<acras> acras { get; set; }
        public virtual DbSet<applications> applications { get; set; }
        public virtual DbSet<appStatus> appStatus { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<BrbyRegs> BrbyRegs { get; set; }
        public virtual DbSet<clients> clients { get; set; }
        public virtual DbSet<clientSexes> clientSexes { get; set; }
        public virtual DbSet<clientWorkDatas> clientWorkDatas { get; set; }
        public virtual DbSet<CompanyNameHelpTables> CompanyNameHelpTables { get; set; }
        public virtual DbSet<companyTypes> companyTypes { get; set; }
        public virtual DbSet<comunities> comunities { get; set; }
        public virtual DbSet<currencyTypes> currencyTypes { get; set; }
        public virtual DbSet<educations> educations { get; set; }
        public virtual DbSet<employmentTypes> employmentTypes { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<productGroups> productGroups { get; set; }
        public virtual DbSet<ProductPurposes> ProductPurposes { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductSuppliers> ProductSuppliers { get; set; }
        public virtual DbSet<PurposeConfigs> PurposeConfigs { get; set; }
        public virtual DbSet<Purposes> Purposes { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<ScoringApplicationScores> ScoringApplicationScores { get; set; }
        public virtual DbSet<ScoringDecisions> ScoringDecisions { get; set; }
        public virtual DbSet<ScoringIndicators> ScoringIndicators { get; set; }
        public virtual DbSet<ScoringIndicatorsParameters> ScoringIndicatorsParameters { get; set; }
        public virtual DbSet<ScoringIndicatorsTypes> ScoringIndicatorsTypes { get; set; }
        public virtual DbSet<ScoringParameters> ScoringParameters { get; set; }
        public virtual DbSet<ScoringProductIndicators> ScoringProductIndicators { get; set; }
        public virtual DbSet<ScoringScoreDecisions> ScoringScoreDecisions { get; set; }
        public virtual DbSet<ScoringScores> ScoringScores { get; set; }
        public virtual DbSet<Sellers> Sellers { get; set; }
        public virtual DbSet<SupplierBranches> SupplierBranches { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<UserASProfiles> UserASProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.UserASProfiles)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Branches>()
                .HasMany(e => e.UserASProfiles)
                .WithOptional(e => e.Branches)
                .HasForeignKey(e => e.BrancheId);

            modelBuilder.Entity<clientSexes>()
                .HasMany(e => e.clients)
                .WithOptional(e => e.clientSexes)
                .HasForeignKey(e => e.sex_clientSexId);

            modelBuilder.Entity<educations>()
                .HasMany(e => e.clients)
                .WithOptional(e => e.educations)
                .HasForeignKey(e => e.edu_educationId);

            modelBuilder.Entity<productGroups>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.productGroups)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierBranches>()
                .HasMany(e => e.Sellers)
                .WithRequired(e => e.SupplierBranches)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Suppliers>()
                .HasMany(e => e.Sellers)
                .WithRequired(e => e.Suppliers)
                .WillCascadeOnDelete(false);
        }
    }
}
