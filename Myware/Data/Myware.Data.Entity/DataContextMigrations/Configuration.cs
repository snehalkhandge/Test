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
    using Myware.Data.Entity.Models.UserTasks;
    using Myware.Data.Entity.Models.PreSales;
using Myware.Data.Entity.Models.PostSalesUnit;

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


            var company2 = new Company
            {
                Name = "Develoeprs company 2",
                UpdatedByUserId = 1,
                Address = "Develoeper address 2",
                Pin = "erdfdfsdsffd",
                LocalityId = 1,
                FaxNumber = "55665685665",
                ReceiptFormat = "I dont know"

            };

            context.Companies.Add(company2);
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

            var developer2 = new Developer
            {
                Name = "Developer Bhai 2",
                UpdatedByUserId = 1

            };

            context.Developers.Add(developer2);
            context.SaveChanges();

            context.DeveloperCompanies.Add(new DeveloperCompanies
            {
                CompanyId = 1,
                DeveloperId = 1

            });


            context.DeveloperCompanies.Add(new DeveloperCompanies
            {
                CompanyId = 2,
                DeveloperId = 2

            });
            #endregion


            #region User Tasks

            var userTask = new AssignedTask
            {
                AssignedFromId = 1,
                AssignedToId = 2,
                Created = DateTime.UtcNow,
                Description = "Description sj fsd kjds dfsd ksd kfd kjfds kdjffdk dfk d",
                IsActive = true,
                IsParentTask=true,
                LastUpdated = DateTime.UtcNow,
                TaskStatus="Assigned",
                TaskType="Custom",
                Title = "Task Title 1",                
                UpdatedByUserId = 1
            };
            context.AssignedTasks.Add(userTask);
            context.SaveChanges();

            var userTask1 = new AssignedTask
            {
                AssignedFromId = 2,
                AssignedToId = 1,
                Created = DateTime.UtcNow,
                Description = "Description sj fsd kjds dfsd ksd kfd kjfds kdjffdk dfk d",
                IsActive = true,
                IsParentTask = true,
                LastUpdated = DateTime.UtcNow,
                TaskStatus = "Assigned",
                TaskType = "Custom",
                Title = "Reply Task Title 1",
                UpdatedByUserId = 1
            };
            context.AssignedTasks.Add(userTask1);
            context.SaveChanges();

            var userTask3 = new AssignedTask
            {
                AssignedFromId = 1,
                AssignedToId = 1,
                Created = DateTime.UtcNow,
                Description = "Description sj fsd kjds dfsd ksd kfd kjfds kdjffdk dfk d",
                IsActive = true,
                IsParentTask = true,
                LastUpdated = DateTime.UtcNow,
                TaskStatus = "Assigned",
                TaskType = "Notification",
                Title = "Task Title 3",
                UpdatedByUserId = 1
            };
            context.AssignedTasks.Add(userTask3);
            context.SaveChanges();

            var userTask4 = new AssignedTask
            {
                AssignedFromId = 1,
                AssignedToId = 1,
                Created = DateTime.UtcNow,
                Description = "Description sj fsd kjds dfsd ksd kfd kjfds kdjffdk dfk d",
                IsActive = true,
                IsParentTask = true,
                LastUpdated = DateTime.UtcNow,
                TaskStatus = "Assigned",
                TaskType = "ContactEnquiry",
                Title = "Task Title 3",
                UpdatedByUserId = 1,
                ContactEnquiryId = 1
            };
            context.AssignedTasks.Add(userTask4);
            context.SaveChanges();
            #endregion

            #region Customer Lead
                #region Personal Information
            var personalInfo = new PersonalInformation
            {
                Address = "djk dfsjf jkdfddfs  fd",
                AnniversaryDate = DateTime.UtcNow,
                Campaign=campaign.Name,
                SubCampaign = campaign3.Name,
                City = location.City,
                ContactType = customerEnquiryType.Name,
                DateOfBirth = DateTime.UtcNow,
                Email="mallabhinav@hotmail.com",
                FirstName="Abhinav",
                LastName="Mall",
                Locality = locality.Name,
                PinCode="NNDDD",
                Remarks="fgkgrkldgfkl l klgdkf gfdlk fdgklgfdlk gfdlk gfd gdf",
                UpdatedByUserId = 1
            };
            var personalInfo1 = new PersonalInformation
            {
                Address = "djk dfsjf jkdfddfs  fd",
                AnniversaryDate = DateTime.UtcNow,
                Campaign = campaign.Name,
                SubCampaign = campaign3.Name,
                City = location.City,
                ContactType = customerEnquiryType.Name,
                DateOfBirth = DateTime.UtcNow,
                Email = "mallabhinav1@hotmail.com",
                FirstName = "Abhinav1",
                LastName = "Mall1",
                Locality = locality.Name,
                PinCode = "NNDDD",
                Remarks = "fgkgrkldgfkl l klgdkf gfdlk fdgklgfdlk gfdlk gfd gdf",
                UpdatedByUserId = 1
            };
            var personalInfo2 = new PersonalInformation
            {
                Address = "2 djk dfsjf jkdfddfs  fd",
                AnniversaryDate = DateTime.UtcNow,
                Campaign = campaign.Name,
                SubCampaign = campaign3.Name,
                City = location.City,
                ContactType = customerEnquiryType1.Name,
                DateOfBirth = DateTime.UtcNow,
                Email = "mallabhinav2@hotmail.com",
                FirstName = "Abhinav2",
                LastName = "Mall2",
                Locality = locality.Name,
                PinCode = "NNDDD",
                Remarks = "fgkgrkldgfkl l klgdkf gfdlk fdgklgfdlk gfdlk gfd gdf",
                UpdatedByUserId = 1
            };
            var personalInfo3 = new PersonalInformation
            {
                Address = " 3 djk dfsjf jkdfddfs  fd",
                AnniversaryDate = DateTime.UtcNow,
                Campaign = campaign.Name,
                SubCampaign = campaign3.Name,
                City = location.City,
                ContactType = customerEnquiryType2.Name,
                DateOfBirth = DateTime.UtcNow,
                Email = "mallabhinav3@hotmail.com",
                FirstName = "Abhinav 3",
                LastName = "Mall 3",
                Locality = locality.Name,
                PinCode = "NNDDD",
                Remarks = "fgkgrkldgfkl l klgdkf gfdlk fdgklgfdlk gfdlk gfd gdf",
                UpdatedByUserId = 1
            };
            context.PersonalInformations.Add(personalInfo);
            context.PersonalInformations.Add(personalInfo1);
            context.PersonalInformations.Add(personalInfo2);
            context.PersonalInformations.Add(personalInfo3);            
            context.SaveChanges();

            var contNum = new PersonalContactNumber
            {
                PhoneNumber=9827564562,
                Type="Primary",
                PersonalInformationId=1,
            };
            var contNum1 = new PersonalContactNumber
            {
                PhoneNumber = 9826564562,
                Type = "Primary",
                PersonalInformationId = 1,
            };
            var contNum2 = new PersonalContactNumber
            {
                PhoneNumber = 9825564562,
                Type = "Primary",
                PersonalInformationId = 2,
            };
            var contNum3 = new PersonalContactNumber
            {
                PhoneNumber = 9828564562,
                Type = "Primary",
                PersonalInformationId = 3,
            };
            var contNum4 = new PersonalContactNumber
            {
                PhoneNumber = 9822464562,
                Type = "Primary",
                PersonalInformationId = 4,
            };
            var contNum5 = new PersonalContactNumber
            {
                PhoneNumber = 9829364562,
                Type = "Primary",
                PersonalInformationId = 2,
            };

            context.PersonalContactNumbers.Add(contNum);
            context.PersonalContactNumbers.Add(contNum1);
            context.PersonalContactNumbers.Add(contNum2);
            context.PersonalContactNumbers.Add(contNum3);
            context.PersonalContactNumbers.Add(contNum4);
            context.PersonalContactNumbers.Add(contNum5);
            context.SaveChanges();
            #endregion

                #region Business Information
            var busInfo = new BusinessInformation
            {
                BusinessOrIndustry = "Bus Ind Tel",
                City= location.City,
                CompanyName = "Company Name 1",
                Designation="Designation 1",
                Fax="897526452",
                InvestmentCapacity=8897.25m,
                IsActive=true,
                LastUpdated=DateTime.UtcNow,
                Locality= locality.Name,
                PersonalInformationId=1,
                Type="I dont know",
                UpdatedByUserId=1,
                Website="abhinav.com"
            };

            var busInfo1 = new BusinessInformation
            {
                BusinessOrIndustry = "1 Bus Ind Tel",
                City = location.City,
                CompanyName = "1 Company Name 1",
                Designation = " 1 Designation 1",
                Fax = "897526452",
                InvestmentCapacity = 8897.25m,
                IsActive = true,
                LastUpdated = DateTime.UtcNow,
                Locality = locality.Name,
                PersonalInformationId = 2,
                Type = "I dont know",
                UpdatedByUserId = 1,
                Website = "1 abhinav.com"
            };

            var busInfo2 = new BusinessInformation
            {
                BusinessOrIndustry = "2 Bus Ind Tel",
                City = location1.City,
                CompanyName = "2 Company Name 1",
                Designation = " 2 Designation 1",
                Fax = "897526452",
                InvestmentCapacity = 8897.25m,
                IsActive = true,
                LastUpdated = DateTime.UtcNow,
                Locality = locality1.Name,
                PersonalInformationId = 3,
                Type = "I dont know",
                UpdatedByUserId = 1,
                Website = "2abhinav.com"
            };

            var busInfo3 = new BusinessInformation
            {
                BusinessOrIndustry = "3 Bus Ind Tel",
                City = location.City,
                CompanyName = "3 Company Name 1",
                Designation = "3 Designation 1",
                Fax = "897526452",
                InvestmentCapacity = 8897.25m,
                IsActive = true,
                LastUpdated = DateTime.UtcNow,
                Locality = locality.Name,
                PersonalInformationId = 1,
                Type = "I dont know",
                UpdatedByUserId = 1,
                Website = "3abhinav.com"
            };

            context.BusinessInformations.Add(busInfo);
            context.BusinessInformations.Add(busInfo1);
            context.BusinessInformations.Add(busInfo2);
            context.BusinessInformations.Add(busInfo3);
            context.SaveChanges();

            var busNum = new BusinessContactNumber
            {
                PhoneNumber = 9817564562,
                Type = "Primary",
                BusinessInformationId = 1,
            };

            var busNum1 = new BusinessContactNumber
            {
                PhoneNumber = 9827564562,
                Type = "Primary",
                BusinessInformationId = 2,
            };

            var busNum2 = new BusinessContactNumber
            {
                PhoneNumber = 9837564562,
                Type = "Primary",
                BusinessInformationId = 3,
            };

            var busNum3 = new BusinessContactNumber
            {
                PhoneNumber = 9847564562,
                Type = "Primary",
                BusinessInformationId = 1,
            };

            var busNum4 = new BusinessContactNumber
            {
                PhoneNumber = 9857564562,
                Type = "Primary",
                BusinessInformationId = 1,
            };

            context.BusinessContactNumbers.Add(busNum);
            context.BusinessContactNumbers.Add(busNum1);
            context.BusinessContactNumbers.Add(busNum2);
            context.BusinessContactNumbers.Add(busNum3);
            context.BusinessContactNumbers.Add(busNum4);
            context.SaveChanges();
            #endregion

            #region Contact Enquiries
            var cntEnquiry = new ContactEnquiry
            {
                AssignedDate = DateTime.UtcNow,
                BudgetFrom= 566.00m,
                BudgetTo= 599.0m,
                CarpetAreaFrom=999.0m,
                CarpetAreaTo=999.0m,
                ContactStatus = contactStatus1.Name,
                Created = DateTime.UtcNow,
                EnquiryDate = DateTime.UtcNow,
                FacingType = facingType.Name,
                IsFurnished = true,
                IsActive = true,
                LastUpdated = DateTime.Now,
                LeadStatus = "Lead Status 1",
                LookingForType = "LF Type 1",
                OfferedRate = 11.0m,
                PersonalInformationId = 1,
                PropertyAge="1-4 years",
                Remarks="Notes are alwary ggod  idea",
                SaleAreaFrom = 887.0m,
                SaleAreaTo=999.0m,
                TransactionType = transactionType.Name,
                UpdatedByUserId = 1

            };
            var cntEnquiry1 = new ContactEnquiry
            {
                AssignedDate = DateTime.UtcNow,
                BudgetFrom = 1566.00m,
                BudgetTo = 1599.0m,
                CarpetAreaFrom = 1999.0m,
                CarpetAreaTo = 1999.0m,
                ContactStatus = contactStatus1.Name,
                Created = DateTime.UtcNow,
                EnquiryDate = DateTime.UtcNow,
                FacingType = facingType.Name,
                IsFurnished = true,
                IsActive = true,
                LastUpdated = DateTime.Now,
                LeadStatus = "1Lead Status 1",
                LookingForType = "1LF Type 1",
                OfferedRate = 11.0m,
                PersonalInformationId = 1,
                PropertyAge = "11-4 years",
                Remarks = "1Notes are alwary ggod  idea",
                SaleAreaFrom = 1887.0m,
                SaleAreaTo = 1999.0m,
                TransactionType = transactionType.Name,
                UpdatedByUserId = 1

            };

            context.ContactEnquiries.Add(cntEnquiry);
            context.ContactEnquiries.Add(cntEnquiry1);
            context.SaveChanges();

            var enqLocality = new ContactEnquiryLocality
            {
                Locality= locality.Name,
                ContactEnquiryId = cntEnquiry.Id
            };
            var enqLocality1 = new ContactEnquiryLocality
            {
                Locality = locality1.Name,
                ContactEnquiryId = cntEnquiry1.Id
            };
            context.ContactEnquiryLocalities.Add(enqLocality);
            context.ContactEnquiryLocalities.Add(enqLocality1);
            context.SaveChanges();


            var enqUnit = new ContactEnquiryUnitType
            {
                Name = unitType.Name,
                ContactEnquiryId = cntEnquiry.Id
            };
            var enqUnit1 = new ContactEnquiryUnitType
            {
                Name = unitType.Name,
                ContactEnquiryId = cntEnquiry1.Id
            };
            context.ContactEnquiryUnitTypes.Add(enqUnit);
            context.ContactEnquiryUnitTypes.Add(enqUnit1);
            context.SaveChanges();

            #endregion
            #endregion


            #region Project Type

            var projectType = new ProjectType{
                Name ="Residential",
                UpdatedByUserId = 1
            };

            var projectType1 = new ProjectType{
                Name ="Commercial",
                UpdatedByUserId = 1
            };

            var projectType2 = new ProjectType{
                Name ="Residential cum Commercial",
                UpdatedByUserId = 1
            };

            context.ProjectTypes.Add(projectType);
            context.ProjectTypes.Add(projectType1);
            context.ProjectTypes.Add(projectType2);
            context.SaveChanges();

            #endregion

            PostSalesSeedData.SeedData(context);

        }
    }
}
