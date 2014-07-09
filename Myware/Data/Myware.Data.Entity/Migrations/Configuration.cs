using System.Collections.Generic;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Myware.Data.Entity.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Myware.Data.Entity.ApplicationDbContext context)
        {
            
            var adminRole = new Role { Name = "Admin" };
            var tLRole = new Role { Name = "TeamLeader" };
            var tCRole = new Role { Name = "TeleCaller" };
            var sMRole = new Role { Name = "SalesManager" };

            context.Roles.Add(adminRole);
            context.Roles.Add(tLRole);
            context.Roles.Add(tCRole);
            context.Roles.Add(sMRole);

            context.SaveChanges();
            #region Users and Roles



            var hash = "Admin_123"; //Crypto.HashPassword("Admin_123");

            var adminUser = new User
            {
                UserName = "Administrator",
                Email = "abhinav@abhinav.com",
                Password = hash,
                FirstName = " Admin ",
                LastName = "is my Last Name",
                RoleId = 1
            };



            var tLUser = new User
            {

                UserName = "Teamleader",
                Email = "abhinav1@abhinav.com",
                Password = hash,
                FirstName = "Team Leader ",
                LastName = "is my Last Name",
                RoleId = 2
            };



            var tCUser = new User
            {

                UserName = "TeleCaller",
                Email = "abhinav2@abhinav.com",
                Password = hash,
                FirstName = "Tele Caller",
                LastName = "is my Last Name",
                RoleId = 3
            };




            var sMUser = new User
            {

                UserName = "SalesManager",
                Email = "abhinav3@abhinav.com",
                Password = hash,
                FirstName = "Sales Manager",
                LastName = "is my Last Name",
                RoleId = 4
            };


            context.Users.Add(adminUser);
            context.Users.Add(tLUser);
            context.Users.Add(tCUser);
            context.Users.Add(sMUser);


            context.SaveChanges();
            #endregion


            #region Pre Sales Unit

            var facingType = new FacingType
            {
                Name = "Facing Type 1",
                UpdatedByUserId = adminUser.Id
            };
            context.FacingTypes.Add(facingType);
            context.SaveChanges();
            var unitType = new UnitType
            {
                Name = "Unit Type 1",
                UpdatedByUserId = adminUser.Id
            };
            context.UnitTypes.Add(unitType);

            var unitType1 = new UnitType
            {
                Name = "Unit Type 2",
                UpdatedByUserId = adminUser.Id
            };
            context.UnitTypes.Add(unitType1);
            context.SaveChanges();
            var transactionType = new TransactionType
            {
                Name = "Transaction Type 1",
                UpdatedByUserId = adminUser.Id
            };
            context.TransactionTypes.Add(transactionType);

            var transactionType1 = new TransactionType
            {
                Name = "Transaction Type 2",
                UpdatedByUserId = adminUser.Id
            };
            context.TransactionTypes.Add(transactionType1);
            context.SaveChanges();

            var customerEnquiryType = new CustomerEnquiryType
            {
                Name = "CustEnq1",
                UpdatedByUserId = adminUser.Id
            };

            context.CustomerEnquiryTypes.Add(customerEnquiryType);

            var customerEnquiryType1 = new CustomerEnquiryType
            {
                Name = "CustEnq2",
                UpdatedByUserId = adminUser.Id
            };

            context.CustomerEnquiryTypes.Add(customerEnquiryType1);

            context.SaveChanges();


            var contactStatus = new ContactStatus
            {
                Name = "Contact Status 1",
                UpdatedByUserId = adminUser.Id
            };
            context.ContactStatus.Add(contactStatus);

            var contactStatus1 = new ContactStatus
            {
                Name = "Contact Status 2",
                UpdatedByUserId = adminUser.Id
            };
            context.ContactStatus.Add(contactStatus1);

            var contactStatus2 = new ContactStatus
            {
                Name = "Contact Status 2",
                UpdatedByUserId = adminUser.Id
            };
            context.ContactStatus.Add(contactStatus2);

            var contactStatus3 = new ContactStatus
            {
                Name = "Contact Status 3",
                UpdatedByUserId = adminUser.Id
            };
            context.ContactStatus.Add(contactStatus3);
            context.SaveChanges();
            var location = new Location
            {
                City = "Navi Mumbai",
                State = "Maharastra",
                Country = "India",
                UpdatedByUserId = adminUser.Id
            };
            context.Locations.Add(location);

            var location1 = new Location
            {
                City = "Mumbai",
                State = "Maharastra",
                Country = "India",
                UpdatedByUserId = adminUser.Id
            };
            context.Locations.Add(location1);

            context.SaveChanges();

            var locality = new Locality
            {
                LocationId = 1,
                Name = "Vashi",
                UpdatedByUserId = adminUser.Id
            };
            context.Localities.Add(locality);



            var locality1 = new Locality
            {
                LocationId = 1,
                Name = "Vashi - Other",
                UpdatedByUserId = adminUser.Id
            };
            context.Localities.Add(locality1);

            var locality2 = new Locality
            {
                LocationId = 2,
                Name = "Santa Cruz",
                UpdatedByUserId = adminUser.Id
            };
            context.Localities.Add(locality2);

            context.SaveChanges();

            var campaign = new Campaign
            {
                IsParentCampaign = true,
                Name = "Parent Campaign",
                UpdatedByUserId = adminUser.Id
            };
            context.Campaigns.Add(campaign);

            var campaign1 = new Campaign
            {
                IsParentCampaign = true,
                Name = "Parent Campaign",
                UpdatedByUserId = adminUser.Id
            };
            context.Campaigns.Add(campaign1);
            context.SaveChanges();

            var campaign2 = new Campaign
            {
                IsParentCampaign = false,
                Name = "Child Campaign 1",
                ParentCampaignId = 1,
                UpdatedByUserId = adminUser.Id
            };
            context.Campaigns.Add(campaign2);

            var campaign3 = new Campaign
            {
                IsParentCampaign = false,
                Name = "Child Campaign 1",
                ParentCampaignId = 1,
                UpdatedByUserId = adminUser.Id
            };
            context.Campaigns.Add(campaign3);

            var campaign4 = new Campaign
            {
                IsParentCampaign = false,
                Name = "Child Campaign 1",
                ParentCampaignId = 1,
                UpdatedByUserId = adminUser.Id
            };
            context.Campaigns.Add(campaign4);
            context.SaveChanges();
            var contactNumber = new ContactNumber
            {
                Type = "Primary",
                PhoneNumber = "55555555555",
                UpdatedByUserId = adminUser.Id
            };

            context.ContactNumbers.Add(contactNumber);

            var contactNumber1 = new ContactNumber
            {
                Type = "Secondary",
                PhoneNumber = "33333333333",
                UpdatedByUserId = adminUser.Id
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
                ContactNumbers = new List<ContactNumber>
                {
                    new ContactNumber
                    {
                        Type = "Primary",
                        PhoneNumber = "5511111111",
                        UpdatedByUserId = adminUser.Id
                    },
                    new ContactNumber
                    {
                        Type = "Secondary",
                        PhoneNumber = "22222222222",
                        UpdatedByUserId = adminUser.Id
                    }
                },
                UpdatedByUserId = adminUser.Id


            };

            context.Brokers.Add(broker);

            context.SaveChanges();

            var developer = new Developer
            {
                Name = "Developer Bhai",
                Description = "Description developer",
                UpdatedByUserId = adminUser.Id
            };

            context.Developers.Add(developer);

            var developer1 = new Developer
            {
                Name = "Developer Bhai 1",
                UpdatedByUserId = adminUser.Id

            };

            context.Developers.Add(developer1);
            context.SaveChanges();
            var company = new Company
            {
                Name = "Develoeprs company",
                UpdatedByUserId = adminUser.Id,
                Address = "Develoeper address",
                Pin = "erdfdfsdsffd",
                LocalityId = 1,
                FaxNumber = "55665685665",
                ReceiptFormat = "I dont know",
                Developers = context.Developers.ToList(),
                ContactNumbers = context.ContactNumbers.ToList()
            };

            context.Companies.Add(company);


            context.SaveChanges();
            #endregion


        }
    }
}
