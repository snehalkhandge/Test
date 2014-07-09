using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using Myware.Data.Entity.Models.PreSales;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Data.Entity.Models.UserTasks;

namespace Myware.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

    
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            #region User Management
            builder.EntitySet<User>("Users");
            builder.EntitySet<Role>("Roles");
            builder.EntitySet<RolePermissions>("RolePermissions");
            #endregion
            #region Pres Sales Unit
            builder.EntitySet<Broker>("Brokers");
            builder.EntitySet<ContactStatus>("ContactStatus");
            builder.EntitySet<ContactNumber>("ContactNumbers");
            builder.EntitySet<Campaign>("Campaigns");
            builder.EntitySet<Company>("Companies");
            builder.EntitySet<Developer>("Developers");
            builder.EntitySet<FacingType>("FacingTypes");
            builder.EntitySet<Location>("Locations");
            builder.EntitySet<Locality>("Localities");
            builder.EntitySet<LookingForType>("LookingForTypes");
            builder.EntitySet<TransactionType>("TransactionTypes");
            builder.EntitySet<UnitType>("UnitTypes");
            #endregion

            #region Pre Sales lead
            builder.EntitySet<BusinessInformation>("BusinessInformations");
            builder.EntitySet<PersonalInformation>("PersonalInformations"); 
            builder.EntitySet<ContactEnquiry>("ContactEnquiries");


            #endregion

            #region Task Manager

            builder.EntitySet<AssignedTask>("AssignedTasks");
            builder.EntitySet<TasksRelatedFile>("TasksRelatedFiles"); 
            #endregion

            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());


            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
