﻿using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ASFront.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("UserId")]
        public virtual ICollection<UserASProfiles> UserASProfile { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        public DbSet<Regions> Regions { get; set; }




        public DbSet<Branches> Branches { get; set; }




        public DbSet<BrbyRegs> BrbyRegs { get; set; }




        public DbSet<UserASProfiles> UserASProfiles { get; set; }




        public DbSet<educations> educations { get; set; }




        public DbSet<clientSexes> clientSexes { get; set; }




        public DbSet<clients> clients { get; set; }




        public DbSet<comunities> comunities { get; set; }




        public DbSet<Suppliers> Suppliers { get; set; }




        public DbSet<CurrencyTypes> CurrencyTypes { get; set; }




        public DbSet<Products> Products { get; set; }




        public DbSet<appStatus> appStatus { get; set; }



        public DbSet<applications> applications { get; set; }


        public DbSet<acraLoans> acraLoans { get; set; }
        public DbSet<acras> acras { get; set; }



        public DbSet<employmentTypes> employmentTypes { get; set; }




        public DbSet<companyTypes> companyTypes { get; set; }




        public DbSet<CompanyNameHelpTables> CompanyNameHelpTables { get; set; }




        public DbSet<clientWorkDatas> clientWorkDatas { get; set; }




        public DbSet<productGroups> productGroups { get; set; }




        public DbSet<Purposes> Purposes { get; set; }




        public DbSet<PurposeConfigs> PurposeConfigs { get; set; }




        public DbSet<ProductSuppliers> ProductSuppliers { get; set; }




        public DbSet<ProductPurposes> ProductPurposes { get; set; }


        public DbSet<ScoringIndicators> ScoringIndicators { get; set; }


        public DbSet<ScoringParameters> ScoringParameters { get; set; }



        public DbSet<Items> Items { get; set; }



        public DbSet<ScoringIndicatorsParameters> ScoringIndicatorsParameters { get; set; }



        public DbSet<ScoringIndicatorsTypes> ScoringIndicatorsTypes { get; set; }



        public DbSet<ScoringScores> ScoringScores { get; set; }


        public DbSet<ScoringApplicationScores> ScoringApplicationScores { get; set; }


        public DbSet<ScoringScoreDecisions> ScoringScoreDecisions { get; set; }


        public DbSet<ScoringProductIndicators> ScoringProductIndicators { get; set; }


        public DbSet<ScoringDecisions> ScoringDecisions { get; set; }


        public DbSet<SupplierBranches> SupplierBranches { get; set; }

        public DbSet<Sellers> Sellers { get; set; }
        public DbSet<FormulaTable> FormulaTable { get; set; }


        public DbSet<Streets> Streets { get; set; }


        public DbSet<ApplicationSummary> ApplicationSummary { get; set; }


        public DbSet<ProductLimits> ProductLimits { get; set; }

        public DbSet<UserAppTable> UserAppTable { get; set; }

        public DbSet<AppTypes> AppTypes { get; set; }


        public DbSet<DocsApllications> DocsApllications { get; set; }
        public DbSet<DocType> DocType { get; set; }


        public DbSet<ApplicationAppruves> ApplicationAppruves { get; set; }


        public DbSet<IncomeExpenses> IncomeExpenses { get; set; }
        
        public DbSet<Balance> Balance { get; set; }


        public DbSet<Golds> gold { get; set; }

        public DbSet<JewelleryItemType> jewelleryItemType { get; set; }

        public DbSet<releationType> releationType { get; set; }

        public DbSet<group> group { get; set; }

        public DbSet<clientsGroup> clientsGroup { get; set; }

        public DbSet<BranchUsers> BranchUsers { get; set; }


        public DbSet<FamilyCostNormatives> FamilyCostNormatives { get; set; }
        
        public DbSet<AgroAssetTypes> AgroAssetTypes { get; set; }

        public DbSet<AgroAsset> AgroAsset { get; set; }


        public DbSet<AgroAssetIncomeNormative> AgroAssetIncomeNormative { get; set; }


        public DbSet<Guarantors> Guarantors { get; set; }


        public DbSet<GoldAssayes> GoldAssayes { get; set; }


        public DbSet<GoldCollaterals> GoldCollateral { get; set; }


        public DbSet<GoldPrices> GoldPrices { get; set; }


        public DbSet<GoldTypes> GoldTypes { get; set; }


        public DbSet<RealtyEstates> RealtyEstate { get; set; }


        public DbSet<RealtyTypes> RealtyTypes { get; set; }


        public DbSet<MovableEstates> MovableEstate { get; set; }


        public DbSet<MovableEstateTypes> MovableEstateTypes { get; set; }


        public DbSet<MeasurementUnits> MeasurementUnits { get; set; }


        public DbSet<Turnovers> Turnovers { get; set; }

        public DbSet<CurrencyCurrents> CurrencyCurrents { get; set; }

        public DbSet<acraRequestesInfo> acraRequestesInfo { get; set; }

        public DbSet<acraInterrelated> acraInterrelated { get; set; }

        public DbSet<acraLoanosDays> acraLoanosDays { get; set; }

        public DbSet<BusinessInfo> BusinessInfo { get; set; }

        public DbSet<BusinessSector> BusinessSector { get; set; }

        public DbSet<BusinessType> BusinessType { get; set; }

        public DbSet<NameofBanks> NameofBanks { get; set; }

        public DbSet<OwnershipType> OwnershipType { get; set; }

        public DbSet<applicationEditRoles> applicationEditRoles { get; set; }


        public DbSet<ApplicationsForApprove> ApplicationsForApprove { get; set; }

        
    }
}
