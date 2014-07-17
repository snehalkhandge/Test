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
                        Created = c.DateTime(nullable: false),
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
                        AssignedTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssignedTasks", t => t.AssignedTask_Id)
                .Index(t => t.AssignedTask_Id);
            
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
                        BusinessInformation_Id = c.Int(),
                        PersonalInformation_Id = c.Int(),
                        Broker_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessInformations", t => t.BusinessInformation_Id)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformation_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .ForeignKey("dbo.Brokers", t => t.Broker_Id)
                .Index(t => t.UpdatedByUserId)
                .Index(t => t.BusinessInformation_Id)
                .Index(t => t.PersonalInformation_Id)
                .Index(t => t.Broker_Id);
            
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
                        BusinessLocalityId = c.Int(nullable: false),
                        PersonalInformationId = c.Int(nullable: false),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Localities", t => t.BusinessLocalityId)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformationId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.BusinessLocalityId)
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
                        ImageUrl = c.String(),
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
                "dbo.CustomerEnquiryTypeCollections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonalInformationId = c.Int(nullable: false),
                        CustomerEnquiryTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerEnquiryTypes", t => t.CustomerEnquiryTypeId)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformationId)
                .Index(t => t.PersonalInformationId)
                .Index(t => t.CustomerEnquiryTypeId);
            
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
                "dbo.ContactEnquiries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionTypeId = c.Int(nullable: false),
                        LookingForTypeId = c.Int(nullable: false),
                        BudgetFrom = c.Decimal(precision: 18, scale: 2),
                        BudgetTo = c.Decimal(precision: 18, scale: 2),
                        SaleAreaFrom = c.Decimal(precision: 18, scale: 2),
                        SaleAreaTo = c.Decimal(precision: 18, scale: 2),
                        CarpetAreaFrom = c.Decimal(precision: 18, scale: 2),
                        CarpetAreaTo = c.Decimal(precision: 18, scale: 2),
                        PropertyAge = c.Decimal(precision: 18, scale: 2),
                        IsFurnished = c.Boolean(),
                        OfferedRate = c.Decimal(precision: 18, scale: 2),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        EnquiryDate = c.DateTime(),
                        FacingTypeId = c.Int(nullable: false),
                        ContactStatusId = c.Int(nullable: false),
                        PersonalInformationId = c.Int(nullable: false),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactStatus", t => t.ContactStatusId)
                .ForeignKey("dbo.FacingTypes", t => t.FacingTypeId)
                .ForeignKey("dbo.LookingForTypes", t => t.LookingForTypeId)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformationId)
                .ForeignKey("dbo.TransactionTypes", t => t.TransactionTypeId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.TransactionTypeId)
                .Index(t => t.LookingForTypeId)
                .Index(t => t.FacingTypeId)
                .Index(t => t.ContactStatusId)
                .Index(t => t.PersonalInformationId)
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
                        ContactEnquiry_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .ForeignKey("dbo.ContactEnquiries", t => t.ContactEnquiry_Id)
                .Index(t => t.UpdatedByUserId)
                .Index(t => t.ContactEnquiry_Id);
            
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
                "dbo.CompanyDevelopers",
                c => new
                    {
                        Company_Id = c.Int(nullable: false),
                        Developer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Company_Id, t.Developer_Id })
                .ForeignKey("dbo.Companies", t => t.Company_Id, cascadeDelete: true)
                .ForeignKey("dbo.Developers", t => t.Developer_Id, cascadeDelete: true)
                .Index(t => t.Company_Id)
                .Index(t => t.Developer_Id);
            
            CreateTable(
                "dbo.CampaignPersonalInformations",
                c => new
                    {
                        Campaign_Id = c.Int(nullable: false),
                        PersonalInformation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Campaign_Id, t.PersonalInformation_Id })
                .ForeignKey("dbo.Campaigns", t => t.Campaign_Id, cascadeDelete: true)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformation_Id, cascadeDelete: true)
                .Index(t => t.Campaign_Id)
                .Index(t => t.PersonalInformation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RolePermissions", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RolePermissions", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.ContactEnquiries", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UnitTypes", "ContactEnquiry_Id", "dbo.ContactEnquiries");
            DropForeignKey("dbo.UnitTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TransactionTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactEnquiries", "TransactionTypeId", "dbo.TransactionTypes");
            DropForeignKey("dbo.ContactEnquiries", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.LookingForTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactEnquiries", "LookingForTypeId", "dbo.LookingForTypes");
            DropForeignKey("dbo.FacingTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactEnquiries", "FacingTypeId", "dbo.FacingTypes");
            DropForeignKey("dbo.ContactEnquiries", "ContactStatusId", "dbo.ContactStatus");
            DropForeignKey("dbo.ContactStatus", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Brokers", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactNumbers", "Broker_Id", "dbo.Brokers");
            DropForeignKey("dbo.ContactNumbers", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Developers", "ContactNumber_Id", "dbo.ContactNumbers");
            DropForeignKey("dbo.Developers", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Companies", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Localities", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Locations", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Localities", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Companies", "LocalityId", "dbo.Localities");
            DropForeignKey("dbo.BusinessInformations", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonalInformations", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonalInformationBookingMetas", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonalInformationBookingMetas", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.PersonalInformations", "LocalityId", "dbo.Localities");
            DropForeignKey("dbo.CustomerEnquiryTypeCollections", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.CustomerEnquiryTypeCollections", "CustomerEnquiryTypeId", "dbo.CustomerEnquiryTypes");
            DropForeignKey("dbo.CustomerEnquiryTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactNumbers", "PersonalInformation_Id", "dbo.PersonalInformations");
            DropForeignKey("dbo.Campaigns", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CampaignPersonalInformations", "PersonalInformation_Id", "dbo.PersonalInformations");
            DropForeignKey("dbo.CampaignPersonalInformations", "Campaign_Id", "dbo.Campaigns");
            DropForeignKey("dbo.Campaigns", "ParentCampaignId", "dbo.Campaigns");
            DropForeignKey("dbo.BusinessInformations", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.BusinessInformations", "BusinessLocalityId", "dbo.Localities");
            DropForeignKey("dbo.ContactNumbers", "BusinessInformation_Id", "dbo.BusinessInformations");
            DropForeignKey("dbo.Brokers", "LocalityId", "dbo.Localities");
            DropForeignKey("dbo.CompanyDevelopers", "Developer_Id", "dbo.Developers");
            DropForeignKey("dbo.CompanyDevelopers", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.CompanyContactNumbers", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.AssignedTasks", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TasksRelatedFiles", "AssignedTask_Id", "dbo.AssignedTasks");
            DropForeignKey("dbo.AssignedTasks", "ParentTaskId", "dbo.AssignedTasks");
            DropForeignKey("dbo.AssignedTasks", "AssignedToId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignedTasks", "AssignedFromId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CampaignPersonalInformations", new[] { "PersonalInformation_Id" });
            DropIndex("dbo.CampaignPersonalInformations", new[] { "Campaign_Id" });
            DropIndex("dbo.CompanyDevelopers", new[] { "Developer_Id" });
            DropIndex("dbo.CompanyDevelopers", new[] { "Company_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RolePermissions", new[] { "PermissionId" });
            DropIndex("dbo.RolePermissions", new[] { "RoleId" });
            DropIndex("dbo.Permissions", new[] { "Name" });
            DropIndex("dbo.UnitTypes", new[] { "ContactEnquiry_Id" });
            DropIndex("dbo.UnitTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.TransactionTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.LookingForTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.FacingTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.ContactStatus", new[] { "UpdatedByUserId" });
            DropIndex("dbo.ContactEnquiries", new[] { "UpdatedByUserId" });
            DropIndex("dbo.ContactEnquiries", new[] { "PersonalInformationId" });
            DropIndex("dbo.ContactEnquiries", new[] { "ContactStatusId" });
            DropIndex("dbo.ContactEnquiries", new[] { "FacingTypeId" });
            DropIndex("dbo.ContactEnquiries", new[] { "LookingForTypeId" });
            DropIndex("dbo.ContactEnquiries", new[] { "TransactionTypeId" });
            DropIndex("dbo.Locations", new[] { "UpdatedByUserId" });
            DropIndex("dbo.PersonalInformationBookingMetas", new[] { "UpdatedByUserId" });
            DropIndex("dbo.PersonalInformationBookingMetas", new[] { "PersonalInformationId" });
            DropIndex("dbo.CustomerEnquiryTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.CustomerEnquiryTypeCollections", new[] { "CustomerEnquiryTypeId" });
            DropIndex("dbo.CustomerEnquiryTypeCollections", new[] { "PersonalInformationId" });
            DropIndex("dbo.Campaigns", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Campaigns", new[] { "ParentCampaignId" });
            DropIndex("dbo.PersonalInformations", new[] { "UpdatedByUserId" });
            DropIndex("dbo.PersonalInformations", new[] { "LocalityId" });
            DropIndex("dbo.BusinessInformations", new[] { "UpdatedByUserId" });
            DropIndex("dbo.BusinessInformations", new[] { "PersonalInformationId" });
            DropIndex("dbo.BusinessInformations", new[] { "BusinessLocalityId" });
            DropIndex("dbo.Localities", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Localities", new[] { "LocationId" });
            DropIndex("dbo.CompanyContactNumbers", new[] { "CompanyId" });
            DropIndex("dbo.Companies", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Companies", new[] { "LocalityId" });
            DropIndex("dbo.Developers", new[] { "ContactNumber_Id" });
            DropIndex("dbo.Developers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.ContactNumbers", new[] { "Broker_Id" });
            DropIndex("dbo.ContactNumbers", new[] { "PersonalInformation_Id" });
            DropIndex("dbo.ContactNumbers", new[] { "BusinessInformation_Id" });
            DropIndex("dbo.ContactNumbers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Brokers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Brokers", new[] { "LocalityId" });
            DropIndex("dbo.TasksRelatedFiles", new[] { "AssignedTask_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AssignedTasks", new[] { "UpdatedByUserId" });
            DropIndex("dbo.AssignedTasks", new[] { "ParentTaskId" });
            DropIndex("dbo.AssignedTasks", new[] { "AssignedToId" });
            DropIndex("dbo.AssignedTasks", new[] { "AssignedFromId" });
            DropTable("dbo.CampaignPersonalInformations");
            DropTable("dbo.CompanyDevelopers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Permissions");
            DropTable("dbo.UnitTypes");
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.LookingForTypes");
            DropTable("dbo.FacingTypes");
            DropTable("dbo.ContactStatus");
            DropTable("dbo.ContactEnquiries");
            DropTable("dbo.Locations");
            DropTable("dbo.PersonalInformationBookingMetas");
            DropTable("dbo.CustomerEnquiryTypes");
            DropTable("dbo.CustomerEnquiryTypeCollections");
            DropTable("dbo.Campaigns");
            DropTable("dbo.PersonalInformations");
            DropTable("dbo.BusinessInformations");
            DropTable("dbo.Localities");
            DropTable("dbo.CompanyContactNumbers");
            DropTable("dbo.Companies");
            DropTable("dbo.Developers");
            DropTable("dbo.ContactNumbers");
            DropTable("dbo.Brokers");
            DropTable("dbo.TasksRelatedFiles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AssignedTasks");
        }
    }
}
