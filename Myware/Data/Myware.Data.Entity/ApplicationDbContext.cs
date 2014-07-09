using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using Myware.Data.Entity.Models.PostSales;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Data.Entity.Models.UserTasks;
using Myware.Repository.EF;
using Myware.Data.Entity.Models.PreSales;

namespace Myware.Data.Entity
{

    public partial class ApplicationDbContext : DataContext
    {
        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public ApplicationDbContext()
            : base("Name=ApplicationDbContext")
        {
        }

        #region user management

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion


        #region PreSales Unit
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Company> Companies { get; set; }
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
        public DbSet<CustomerEnquiryTypeCollection> CustomerEnquiryTypeCollection { get; set; }
        public DbSet<PersonalInformation> PersonalInformations { get; set; }
        public DbSet<BusinessInformation> BusinessInformations { get; set; }
        public DbSet<ContactEnquiry> ContactEnquiries { get; set; }

        #endregion

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        public DbSet<PersonalInformationBookingMeta> PersonalInformationBookingMetas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = false;
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
   
}