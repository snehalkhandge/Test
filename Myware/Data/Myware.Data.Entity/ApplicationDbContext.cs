using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using Myware.Data.Entity.Models.PostSales;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Data.Entity.Models.UserTasks;
using Myware.Repository.EF;
using Myware.Data.Entity.Models.PreSales;
using Myware.Repository.DataContext;
using System;
using System.Threading;
using Myware.Repository.Infrastructure;
using Myware.Data.Entity.Models.PostSalesUnit;
using Myware.Data.Entity.Models.PostSales.Booking;
using Myware.Data.Entity.Models.PaymentSchedule;

namespace Myware.Data.Entity
{

    public partial class ApplicationDbContext : IdentityDbContext<User, Role, int, AppUserLogin, AppUserRole, AppUserClaim>
    {

        #region Private Fields
        private readonly Guid _instanceId;
        #endregion Private Fields

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public ApplicationDbContext()
            : base("Name=ApplicationDbContext")
        {
            _instanceId = Guid.NewGuid();
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = false;
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }


        #region user management

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        #endregion


        #region PreSales Unit
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<BrokerContactNumber> BrokerContactNumbers { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyContactNumber> CompanyContactNumbers { get; set; }

        public DbSet<DeveloperCompanies> DeveloperCompanies { get; set; }
        public DbSet<ContactNumber> ContactNumbers { get; set; }
        public DbSet<ContactStatus> ContactStatus { get; set; }
        public DbSet<CustomerEnquiryType> CustomerEnquiryTypes { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<FacingType> FacingTypes { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LookingForType> LookingForTypes { get; set; }

        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }

        #endregion

        #region Task Manager
        public DbSet<TasksRelatedFile> TasksRelatedFiles { get; set; }
        public DbSet<AssignedTask> AssignedTasks { get; set; }
        #endregion

        #region PreSales
        public DbSet<ContactEnquiryLocality> ContactEnquiryLocalities { get; set; }
        public DbSet<ContactEnquiryUnitType> ContactEnquiryUnitTypes { get; set; }
        public DbSet<PersonalInformation> PersonalInformations { get; set; }

        public DbSet<PersonalContactNumber> PersonalContactNumbers { get; set; }

        public DbSet<BusinessContactNumber> BusinessContactNumbers { get; set; }
        public DbSet<BusinessInformation> BusinessInformations { get; set; }
        public DbSet<ContactEnquiry> ContactEnquiries { get; set; }

        public DbSet<DuplicateData> DuplicateData { get; set; }

        #endregion
        
        #region PostSalesUnit

        public DbSet<HotProperty> HotProperties { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectBankDetails> ProjectBankDetails { get; set; }
        public DbSet<ProjectParkingType> ProjectParkingTypes { get; set; }
        public DbSet<ProjectPropertyCharges> ProjectPropertyCharges { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<Tower> Towers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Wing> Wings { get; set; }
        #endregion

        #region Post Sales
        public DbSet<AllotedParking> AllotedParkings { get; set; }
        public DbSet<BookingCustomer> BookingCustomers { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<DocumentManagement> DocumentMangements { get; set; }
        #endregion

        
    }



   
}