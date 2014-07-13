[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Myware.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Myware.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Myware.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Myware.Repository.DataContext;
    using Myware.Data.Entity;
    using Myware.Repository.UnitOfWork;
    using Myware.Repository.EF;
    using Myware.Data.Entity.CustomStores;
    using Myware.Data.Entity.Models.UserManagement;
    using Myware.Repository.Repositories;
    using Myware.Data.Entity.Models.PresalesUnit;
    using Myware.Data.Entity.Models.PreSales;
    using Myware.Data.Entity.Models.UserTasks;
    using Myware.Reality.Services.UserManagement;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDataContextAsync>().To<ApplicationDbContext>().InRequestScope();            
            kernel.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope();            
            
            //Repository
            kernel.Bind<IRepositoryAsync<Permission>>().To<Repository<Permission>>();
            kernel.Bind<IRepositoryAsync<RolePermissions>>().To<Repository<RolePermissions>>();
            #region Pre Sales unit
            kernel.Bind<IRepositoryAsync<Broker>>().To<Repository<Broker>>();
            kernel.Bind<IRepositoryAsync<Campaign>>().To<Repository<Campaign>>();
            kernel.Bind<IRepositoryAsync<Company>>().To<Repository<Company>>();
            kernel.Bind<IRepositoryAsync<ContactNumber>>().To<Repository<ContactNumber>>();
            kernel.Bind<IRepositoryAsync<ContactStatus>>().To<Repository<ContactStatus>>();
            kernel.Bind<IRepositoryAsync<CustomerEnquiryType>>().To<Repository<CustomerEnquiryType>>();
            kernel.Bind<IRepositoryAsync<Developer>>().To<Repository<Developer>>();
            kernel.Bind<IRepositoryAsync<FacingType>>().To<Repository<FacingType>>();            
            kernel.Bind<IRepositoryAsync<Locality>>().To<Repository<Locality>>();
            kernel.Bind<IRepositoryAsync<Location>>().To<Repository<Location>>();
            kernel.Bind<IRepositoryAsync<LookingForType>>().To<Repository<LookingForType>>();
            kernel.Bind<IRepositoryAsync<TransactionType>>().To<Repository<TransactionType>>();
            kernel.Bind<IRepositoryAsync<UnitType>>().To<Repository<UnitType>>();
            #endregion
            #region Pre Sales
            kernel.Bind<IRepositoryAsync<BusinessInformation>>().To<Repository<BusinessInformation>>();
            kernel.Bind<IRepositoryAsync<PersonalInformation>>().To<Repository<PersonalInformation>>();
            kernel.Bind<IRepositoryAsync<ContactEnquiry>>().To<Repository<ContactEnquiry>>();
            kernel.Bind<IRepositoryAsync<CustomerEnquiryTypeCollection>>().To<Repository<CustomerEnquiryTypeCollection>>();
            #endregion
            #region User Tasks
            kernel.Bind<IRepositoryAsync<AssignedTask>>().To<Repository<AssignedTask>>();
            kernel.Bind<IRepositoryAsync<TasksRelatedFile>>().To<Repository<TasksRelatedFile>>();
            
            #endregion
            //Services
            #region Pre Sales Unit Service
            kernel.Bind<IBrokerService>().To<BrokerService>();
            kernel.Bind<ICampaignService>().To<CampaignService>();
            kernel.Bind<ICompanyService>().To<CompanyService>();
            kernel.Bind<IContactStatusService>().To<ContactStatusService>();
            kernel.Bind<ICustomerEnquiryTypeService>().To<CustomerEnquiryTypeService>();
            kernel.Bind<IDeveloperService>().To<DeveloperService>();
            kernel.Bind<IFacingTypeService>().To<FacingTypeService>();
            kernel.Bind<ILocalityService>().To<LocalityService>();
            kernel.Bind<ILookingForTypeService>().To<LookingForTypeService>();
            kernel.Bind<ITransactionTypeService>().To<TransactionTypeService>();
            kernel.Bind<IUnitTypeService>().To<UnitTypeService>();
            #endregion

            //Pre-Sales service
            
            
        }        
    }
}
