namespace Myware.Data.Entity.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignedFromId = c.Int(nullable: false),
                        AssignedToId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 500),
                        TaskStatus = c.String(nullable: false, maxLength: 30),
                        TaskType = c.String(maxLength: 30),
                        Created = c.DateTime(nullable: false),
                        ContactEnquiryId = c.Int(),
                        IsParentTask = c.Boolean(),
                        ParentTaskId = c.Int(),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedFromId)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedToId)
                .ForeignKey("dbo.AssignedTasks", t => t.ParentTaskId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.AssignedFromId)
                .Index(t => t.AssignedToId)
                .Index(t => t.ParentTaskId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TasksRelatedFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileUrl = c.String(),
                        AssignedTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssignedTasks", t => t.AssignedTaskId)
                .Index(t => t.AssignedTaskId);
            
            CreateTable(
                "dbo.BrokerContactNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrokerId = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brokers", t => t.BrokerId)
                .Index(t => t.BrokerId);
            
            CreateTable(
                "dbo.Brokers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CompanyName = c.String(maxLength: 100),
                        Address = c.String(maxLength: 300),
                        Email = c.String(maxLength: 200),
                        PanCard = c.String(maxLength: 30),
                        ReferenceName = c.String(maxLength: 100),
                        LocalityId = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Localities", t => t.LocalityId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.LocalityId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.Localities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        LocationId = c.Int(nullable: false),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.LocationId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Address = c.String(maxLength: 200),
                        Pin = c.String(maxLength: 200),
                        FaxNumber = c.String(maxLength: 200),
                        ReceiptFormat = c.String(maxLength: 200),
                        LocalityId = c.Int(nullable: false),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Localities", t => t.LocalityId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.LocalityId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.CompanyContactNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.DeveloperCompanies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeveloperId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Developers", t => t.DeveloperId)
                .Index(t => t.DeveloperId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 200),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                        ContactNumber_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .ForeignKey("dbo.ContactNumbers", t => t.ContactNumber_Id)
                .Index(t => t.UpdatedByUserId)
                .Index(t => t.ContactNumber_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(maxLength: 200),
                        State = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.BusinessContactNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusinessInformationId = c.Int(nullable: false),
                        PhoneNumber = c.Long(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessInformations", t => t.BusinessInformationId)
                .Index(t => t.BusinessInformationId);
            
            CreateTable(
                "dbo.BusinessInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 200),
                        Designation = c.String(maxLength: 200),
                        BusinessOrIndustry = c.String(maxLength: 200),
                        InvestmentCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fax = c.String(maxLength: 200),
                        Website = c.String(maxLength: 200),
                        Locality = c.String(),
                        City = c.String(),
                        Type = c.String(),
                        PersonalInformationId = c.Int(nullable: false),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformationId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.PersonalInformationId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.PersonalInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 200),
                        Address = c.String(maxLength: 200),
                        PinCode = c.String(maxLength: 30),
                        DateOfBirth = c.DateTime(),
                        AnniversaryDate = c.DateTime(),
                        Remarks = c.String(),
                        Locality = c.String(),
                        City = c.String(),
                        Campaign = c.String(maxLength: 50),
                        SubCampaign = c.String(maxLength: 50),
                        ContactType = c.String(maxLength: 50),
                        ImageUrl = c.String(),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.Email, unique: true)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.ContactEnquiries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Remarks = c.String(),
                        AssignedDate = c.DateTime(nullable: false),
                        LeadStatus = c.String(),
                        TransactionType = c.String(),
                        LookingForType = c.String(),
                        BudgetFrom = c.Decimal(precision: 18, scale: 2),
                        BudgetTo = c.Decimal(precision: 18, scale: 2),
                        SaleAreaFrom = c.Decimal(precision: 18, scale: 2),
                        SaleAreaTo = c.Decimal(precision: 18, scale: 2),
                        CarpetAreaFrom = c.Decimal(precision: 18, scale: 2),
                        CarpetAreaTo = c.Decimal(precision: 18, scale: 2),
                        PropertyAge = c.String(),
                        IsFurnished = c.Boolean(),
                        OfferedRate = c.Decimal(precision: 18, scale: 2),
                        Created = c.DateTime(nullable: false),
                        EnquiryDate = c.DateTime(),
                        FacingType = c.String(),
                        ContactStatus = c.String(),
                        PersonalInformationId = c.Int(nullable: false),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformationId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.PersonalInformationId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.ContactEnquiryLocalities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Locality = c.String(maxLength: 150),
                        ContactEnquiryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactEnquiries", t => t.ContactEnquiryId)
                .Index(t => t.ContactEnquiryId);
            
            CreateTable(
                "dbo.ContactEnquiryUnitTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        ContactEnquiryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactEnquiries", t => t.ContactEnquiryId)
                .Index(t => t.ContactEnquiryId);
            
            CreateTable(
                "dbo.PersonalContactNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonalInformationId = c.Int(nullable: false),
                        PhoneNumber = c.Long(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformationId)
                .Index(t => t.PersonalInformationId)
                .Index(t => t.PhoneNumber, unique: true);
            
            CreateTable(
                "dbo.PersonalInformationBookingMetas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Age = c.Decimal(precision: 18, scale: 2),
                        Son_Wife_Daughter_Of = c.String(),
                        PanNumber = c.String(),
                        Nationality = c.String(),
                        PersonalInformationId = c.Int(nullable: false),
                        Created = c.DateTime(),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformationId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.PersonalInformationId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        IsParentCampaign = c.Boolean(),
                        ParentCampaignId = c.Int(),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Campaigns", t => t.ParentCampaignId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.ParentCampaignId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.ContactNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        Type = c.String(),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.ContactStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.CustomerEnquiryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.FacingTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.LookingForTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 80),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.PermissionId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TransactionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.UnitTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UnitTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TransactionTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RolePermissions", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RolePermissions", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.LookingForTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FacingTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerEnquiryTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactStatus", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactNumbers", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Developers", "ContactNumber_Id", "dbo.ContactNumbers");
            DropForeignKey("dbo.Campaigns", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Campaigns", "ParentCampaignId", "dbo.Campaigns");
            DropForeignKey("dbo.BusinessInformations", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonalInformations", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonalInformationBookingMetas", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonalInformationBookingMetas", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.PersonalContactNumbers", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.ContactEnquiries", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactEnquiryUnitTypes", "ContactEnquiryId", "dbo.ContactEnquiries");
            DropForeignKey("dbo.ContactEnquiryLocalities", "ContactEnquiryId", "dbo.ContactEnquiries");
            DropForeignKey("dbo.ContactEnquiries", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.BusinessInformations", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.BusinessContactNumbers", "BusinessInformationId", "dbo.BusinessInformations");
            DropForeignKey("dbo.Brokers", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Localities", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Locations", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Localities", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Companies", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Companies", "LocalityId", "dbo.Localities");
            DropForeignKey("dbo.Developers", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DeveloperCompanies", "DeveloperId", "dbo.Developers");
            DropForeignKey("dbo.DeveloperCompanies", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.CompanyContactNumbers", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Brokers", "LocalityId", "dbo.Localities");
            DropForeignKey("dbo.BrokerContactNumbers", "BrokerId", "dbo.Brokers");
            DropForeignKey("dbo.AssignedTasks", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TasksRelatedFiles", "AssignedTaskId", "dbo.AssignedTasks");
            DropForeignKey("dbo.AssignedTasks", "ParentTaskId", "dbo.AssignedTasks");
            DropForeignKey("dbo.AssignedTasks", "AssignedToId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignedTasks", "AssignedFromId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UnitTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.TransactionTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RolePermissions", new[] { "PermissionId" });
            DropIndex("dbo.RolePermissions", new[] { "RoleId" });
            DropIndex("dbo.Permissions", new[] { "Name" });
            DropIndex("dbo.LookingForTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.FacingTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.CustomerEnquiryTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.ContactStatus", new[] { "UpdatedByUserId" });
            DropIndex("dbo.ContactNumbers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Campaigns", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Campaigns", new[] { "ParentCampaignId" });
            DropIndex("dbo.PersonalInformationBookingMetas", new[] { "UpdatedByUserId" });
            DropIndex("dbo.PersonalInformationBookingMetas", new[] { "PersonalInformationId" });
            DropIndex("dbo.PersonalContactNumbers", new[] { "PhoneNumber" });
            DropIndex("dbo.PersonalContactNumbers", new[] { "PersonalInformationId" });
            DropIndex("dbo.ContactEnquiryUnitTypes", new[] { "ContactEnquiryId" });
            DropIndex("dbo.ContactEnquiryLocalities", new[] { "ContactEnquiryId" });
            DropIndex("dbo.ContactEnquiries", new[] { "UpdatedByUserId" });
            DropIndex("dbo.ContactEnquiries", new[] { "PersonalInformationId" });
            DropIndex("dbo.PersonalInformations", new[] { "UpdatedByUserId" });
            DropIndex("dbo.PersonalInformations", new[] { "Email" });
            DropIndex("dbo.BusinessInformations", new[] { "UpdatedByUserId" });
            DropIndex("dbo.BusinessInformations", new[] { "PersonalInformationId" });
            DropIndex("dbo.BusinessContactNumbers", new[] { "BusinessInformationId" });
            DropIndex("dbo.Locations", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Developers", new[] { "ContactNumber_Id" });
            DropIndex("dbo.Developers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.DeveloperCompanies", new[] { "CompanyId" });
            DropIndex("dbo.DeveloperCompanies", new[] { "DeveloperId" });
            DropIndex("dbo.CompanyContactNumbers", new[] { "CompanyId" });
            DropIndex("dbo.Companies", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Companies", new[] { "LocalityId" });
            DropIndex("dbo.Localities", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Localities", new[] { "LocationId" });
            DropIndex("dbo.Brokers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Brokers", new[] { "LocalityId" });
            DropIndex("dbo.BrokerContactNumbers", new[] { "BrokerId" });
            DropIndex("dbo.TasksRelatedFiles", new[] { "AssignedTaskId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AssignedTasks", new[] { "UpdatedByUserId" });
            DropIndex("dbo.AssignedTasks", new[] { "ParentTaskId" });
            DropIndex("dbo.AssignedTasks", new[] { "AssignedToId" });
            DropIndex("dbo.AssignedTasks", new[] { "AssignedFromId" });
            DropTable("dbo.UnitTypes");
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Permissions");
            DropTable("dbo.LookingForTypes");
            DropTable("dbo.FacingTypes");
            DropTable("dbo.CustomerEnquiryTypes");
            DropTable("dbo.ContactStatus");
            DropTable("dbo.ContactNumbers");
            DropTable("dbo.Campaigns");
            DropTable("dbo.PersonalInformationBookingMetas");
            DropTable("dbo.PersonalContactNumbers");
            DropTable("dbo.ContactEnquiryUnitTypes");
            DropTable("dbo.ContactEnquiryLocalities");
            DropTable("dbo.ContactEnquiries");
            DropTable("dbo.PersonalInformations");
            DropTable("dbo.BusinessInformations");
            DropTable("dbo.BusinessContactNumbers");
            DropTable("dbo.Locations");
            DropTable("dbo.Developers");
            DropTable("dbo.DeveloperCompanies");
            DropTable("dbo.CompanyContactNumbers");
            DropTable("dbo.Companies");
            DropTable("dbo.Localities");
            DropTable("dbo.Brokers");
            DropTable("dbo.BrokerContactNumbers");
            DropTable("dbo.TasksRelatedFiles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AssignedTasks");
        }
    }
}
