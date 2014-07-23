namespace Myware.Data.Entity.DataContextMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Myware.Data.Entity.CustomStores;
    using Myware.Data.Entity.Models.PresalesUnit;
    using Myware.Data.Entity.Models.UserManagement;
    using System.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<Myware.Data.Entity.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContextMigrations";
        }

        protected override void Seed(Myware.Data.Entity.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            
            AppUserManager userManager = new AppUserManager(new AppUserStore(context));

            RoleManager<Role, int> roleManager = new RoleManager<Role, int>(new RoleStore<Role, int, AppUserRole>(context));


            var adminRole = new Role { Name = "Admin" };
            var tLRole = new Role { Name = "TeamLeader" };
            var tCRole = new Role { Name = "TeleCaller" };
            var sMRole = new Role { Name = "SalesManager" };

            roleManager.Create(adminRole);
            roleManager.Create(tLRole);
            roleManager.Create(tCRole);
            roleManager.Create(sMRole);


            context.SaveChanges();


            #region Users and Roles



            var hash = "Admin_123";

            var adminUser = new User
            {
                UserName = "Administrator",
                Email = "abhinav@abhinav.com",
                FirstName = " Admin ",
                LastName = "is my Last Name",
                IsActive = true,
                EmailConfirmed = true
            };



            var tLUser = new User
            {

                UserName = "Teamleader",
                Email = "abhinav1@abhinav.com",
                FirstName = "Team Leader ",
                LastName = "is my Last Name",
                IsActive = true,
                EmailConfirmed = true

            };



            var tCUser = new User
            {

                UserName = "TeleCaller",
                Email = "abhinav2@abhinav.com",
                FirstName = "Tele Caller",
                LastName = "is my Last Name",
                IsActive = true,
                EmailConfirmed = true

            };




            var sMUser = new User
            {

                UserName = "SalesManager",
                Email = "abhinav3@abhinav.com",
                FirstName = "Sales Manager",
                LastName = "is my Last Name",
                IsActive = true,
                EmailConfirmed = true

            };

            userManager.Create(adminUser, hash);
            userManager.Create(tLUser, hash);
            userManager.Create(tCUser, hash);
            userManager.Create(sMUser, hash);

            context.SaveChanges();

            userManager.AddToRole(adminUser.Id, adminRole.Name);
            userManager.AddToRole(tLUser.Id, tLRole.Name);
            userManager.AddToRole(tCUser.Id, tCRole.Name);
            userManager.AddToRole(sMUser.Id, sMRole.Name);

            context.SaveChanges();
            #endregion
            

            #region Permissions


            var perm = new Permission
            {
                Name = "Permission 1"

            };
            context.Permissions.Add(perm);

            var perm1 = new Permission
            {
                Name = "Permission 2"

            };
            context.Permissions.Add(perm1);

            var perm2 = new Permission
            {
                Name = "Permission 3"

            };
            context.Permissions.Add(perm2);

            var perm3 = new Permission
            {
                Name = "Permission 4"

            };
            context.Permissions.Add(perm3);

            var perm4 = new Permission
            {
                Name = "Permission 5"

            };
            context.Permissions.Add(perm4);
            context.SaveChanges();


            var roleperm = new RolePermissions
            {
                RoleId = 1,
                PermissionId = 1
            };

            context.RolePermissions.Add(roleperm);

            var roleperm2 = new RolePermissions
            {
                RoleId = 1,
                PermissionId = 2
            };

            context.RolePermissions.Add(roleperm2);

            var roleperm3 = new RolePermissions
            {
                RoleId = 1,
                PermissionId = 3
            };

            context.RolePermissions.Add(roleperm3);

            var roleperm4 = new RolePermissions
            {
                RoleId = 1,
                PermissionId = 4
            };

            context.RolePermissions.Add(roleperm4);

            context.SaveChanges();
            #endregion

            #region Pre Sales Unit

            var facingType = new FacingType
            {
                Name = "Facing Type 1",
                UpdatedByUserId = 1
            };
            context.FacingTypes.Add(facingType);
            context.SaveChanges();
            var unitType = new UnitType
            {
                Name = "Unit Type 1",
                UpdatedByUserId = 1
            };
            context.UnitTypes.Add(unitType);

            var unitType1 = new UnitType
            {
                Name = "Unit Type 2",
                UpdatedByUserId = 1
            };
            context.UnitTypes.Add(unitType1);
            context.SaveChanges();
            var transactionType = new TransactionType
            {
                Name = "Transaction Type 1",
                UpdatedByUserId = 1
            };
            context.TransactionTypes.Add(transactionType);

            var transactionType1 = new TransactionType
            {
                Name = "Transaction Type 2",
                UpdatedByUserId = 1
            };
            context.TransactionTypes.Add(transactionType1);
            context.SaveChanges();

            var customerEnquiryType = new CustomerEnquiryType
            {
                Name = "Enquiry",
                UpdatedByUserId = 1
            };

            context.CustomerEnquiryTypes.Add(customerEnquiryType);

            var customerEnquiryType1 = new CustomerEnquiryType
            {
                Name = "Invitee",
                UpdatedByUserId = 1
            };

            context.CustomerEnquiryTypes.Add(customerEnquiryType1);

            var customerEnquiryType2 = new CustomerEnquiryType
            {
                Name = "Broker",
                UpdatedByUserId = 1
            };

            context.CustomerEnquiryTypes.Add(customerEnquiryType2);
            context.SaveChanges();


            var contactStatus = new ContactStatus
            {
                Name = "Contact Status 1",
                UpdatedByUserId = 1
            };
            context.ContactStatus.Add(contactStatus);

            var contactStatus1 = new ContactStatus
            {
                Name = "Contact Status 2",
                UpdatedByUserId = 1
            };
            context.ContactStatus.Add(contactStatus1);

            var contactStatus2 = new ContactStatus
            {
                Name = "Contact Status 2",
                UpdatedByUserId = 1
            };
            context.ContactStatus.Add(contactStatus2);

            var contactStatus3 = new ContactStatus
            {
                Name = "Contact Status 3",
                UpdatedByUserId = 1
            };
            context.ContactStatus.Add(contactStatus3);
            context.SaveChanges();
            var location = new Location
            {
                City = "Navi Mumbai",
                State = "Maharastra",
                Country = "India",
                UpdatedByUserId = 1
            };
            context.Locations.Add(location);

            var location1 = new Location
            {
                City = "Mumbai",
                State = "Maharastra",
                Country = "India",
                UpdatedByUserId = 1
            };
            context.Locations.Add(location1);

            context.SaveChanges();

            var locality = new Locality
            {
                LocationId = 1,
                Name = "Vashi",
                UpdatedByUserId = 1
            };
            context.Localities.Add(locality);



            var locality1 = new Locality
            {
                LocationId = 1,
                Name = "Vashi - Other",
                UpdatedByUserId = 1
            };
            context.Localities.Add(locality1);

            var locality2 = new Locality
            {
                LocationId = 2,
                Name = "Santa Cruz",
                UpdatedByUserId = 1
            };
            context.Localities.Add(locality2);

            context.SaveChanges();

            var campaign = new Campaign
            {
                IsParentCampaign = true,
                Name = "Parent Campaign",
                UpdatedByUserId = 1
            };
            context.Campaigns.Add(campaign);

            var campaign1 = new Campaign
            {
                IsParentCampaign = true,
                Name = "Parent Campaign",
                UpdatedByUserId = 1
            };
            context.Campaigns.Add(campaign1);
            context.SaveChanges();

            var campaign2 = new Campaign
            {
                IsParentCampaign = false,
                Name = "Child Campaign 1",
                ParentCampaignId = 1,
                UpdatedByUserId = 1
            };
            context.Campaigns.Add(campaign2);

            var campaign3 = new Campaign
            {
                IsParentCampaign = false,
                Name = "Child Campaign 1",
                ParentCampaignId = 1,
                UpdatedByUserId = 1
            };
            context.Campaigns.Add(campaign3);

            var campaign4 = new Campaign
            {
                IsParentCampaign = false,
                Name = "Child Campaign 1",
                ParentCampaignId = 1,
                UpdatedByUserId = 1
            };
            context.Campaigns.Add(campaign4);
            context.SaveChanges();
            var contactNumber = new ContactNumber
            {
                Type = "Primary",
                PhoneNumber = "55555555555",
                UpdatedByUserId = 1
            };

            context.ContactNumbers.Add(contactNumber);

            var contactNumber1 = new ContactNumber
            {
                Type = "Secondary",
                PhoneNumber = "33333333333",
                UpdatedByUserId = 1
            };

            context.ContactNumbers.Add(contactNumber1);
            context.SaveChanges();
            var broker = new Broker
            {
                LocalityId = 1,
                CompanyName = "Broker Company Name",
                Email = "broker@broker.inc",
                Name = "Broker Bhai",
                Address = "I dont know",
                PanCard = "456789",
                ReferenceName = "Kabir",
                ContactNumbers = new List<BrokerContactNumber>
                {
                    new BrokerContactNumber
                    {
                        Type = "Primary",
                        PhoneNumber = "5511111111"                        
                    },
                    new BrokerContactNumber
                    {
                        Type = "Secondary",
                        PhoneNumber = "22222222222"                        
                    }
                },
                UpdatedByUserId = 1


            };

            context.Brokers.Add(broker);

            context.SaveChanges();

            var company = new Company
            {
                Name = "Develoeprs company",
                UpdatedByUserId = 1,
                Address = "Develoeper address",
                Pin = "erdfdfsdsffd",
                LocalityId = 1,
                FaxNumber = "55665685665",
                ReceiptFormat = "I dont know"
                
            };

            context.Companies.Add(company);
            context.SaveChanges();


            var developer = new Developer
            {
                Name = "Developer Bhai",
                Description = "Description developer",
                UpdatedByUserId = 1
                
            };

            context.Developers.Add(developer);

            var developer1 = new Developer
            {
                Name = "Developer Bhai 1",
                UpdatedByUserId = 1
                

            };

            context.Developers.Add(developer1);
            context.SaveChanges();
            #endregion
           
        }
    }
}
