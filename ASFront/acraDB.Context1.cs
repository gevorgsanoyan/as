﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASFront
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class acramonEntities : DbContext
    {
        public acramonEntities()
            : base("name=acramonEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACRA_R_CurrentOverdue> ACRA_R_CurrentOverdue { get; set; }
        public virtual DbSet<ACRA_R_TotalLiabilities> ACRA_R_TotalLiabilities { get; set; }
        public virtual DbSet<ACRA_RequestsI> ACRA_RequestsI { get; set; }
    
        public virtual ObjectResult<ACRA_LoansGetDetEx_Result> ACRA_LoansGetDetEx(Nullable<long> r_ID)
        {
            var r_IDParameter = r_ID.HasValue ?
                new ObjectParameter("r_ID", r_ID) :
                new ObjectParameter("r_ID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ACRA_LoansGetDetEx_Result>("ACRA_LoansGetDetEx", r_IDParameter);
        }
    }
}
