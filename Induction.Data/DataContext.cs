using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.Reflection;
using Induction.Core.Domains.Users;
using Induction.Core.Domains;

namespace Induction.Data
{
    public partial class DataContext : IdentityDbContext<AppUser, Role, int, UserLogin, UserRole, UserClaim> //:DbContext
    {
        #region Ctor

        public DataContext()
            : base("DefaultConnection")
        {
            //Configuration.LazyLoadingEnabled = true;
            //Database.SetInitializer(new DataInitializer());
            Database.SetInitializer<DataContext>(null);
        }

        #endregion

        #region Methods

        public bool HasUnsavedChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == EntityState.Added
                                                      || e.State == EntityState.Modified
                                                      || e.State == EntityState.Deleted);
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {

            //if (entityEntry.Entity is Cbed &&
            //            (entityEntry.State == EntityState.Added
            //              || entityEntry.State == EntityState.Modified))
            //{
            //    var cbedToCheck = ((Cbed)entityEntry.Entity);
            //    var charterId = cbedToCheck.CharterId;
            //    var fy = cbedToCheck.FiscalYear;

            //    //check for uniqueness of CharterId and FiscalYear
            //    if (Cbeds.Any(x => x.Id != cbedToCheck.Id && x.CharterId == charterId && x.FiscalYear == fy))
            //        return
            //               new DbEntityValidationResult(entityEntry,
            //                 new List<DbValidationError>
            //                     {
            //                     new DbValidationError( "CharterId/FiscalYear",
            //                         string.Format( "A {0} count has already been entered for this Charter School.", cbedToCheck.FiscalYear))
            //                     });
            //}

            return base.ValidateEntity(entityEntry, items);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Dynamiclly load all configuration
            //System.Type configType = typeof(CAMPERMap);   //any of your configuration classes here
            //var typesToRegister = Assembly.GetAssembly(configType).GetTypes();

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !string.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            //...or do it manually below. For example,
            //modelBuilder.Configurations.Add(new CountyMap());


            base.OnModelCreating(modelBuilder);
        }

        #endregion

        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<SchoolType> SchoolTypes { get; set; }
    }
}
