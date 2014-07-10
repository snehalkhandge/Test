namespace Myware.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserManagment : DbMigration
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
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AssignedFromId)
                .ForeignKey("dbo.Users", t => t.AssignedToId)
                .ForeignKey("dbo.AssignedTasks", t => t.ParentTaskId)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .Index(t => t.AssignedFromId)
                .Index(t => t.AssignedToId)
                .Index(t => t.ParentTaskId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        Email = c.String(maxLength: 500),
                        UserName = c.String(maxLength: 200),
                        Password = c.String(maxLength: 500),
                        Created = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Description = c.String(),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
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
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Localities", t => t.LocalityId)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
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
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Broker_Id = c.Int(),
                        BusinessInformation_Id = c.Int(),
                        PersonalInformation_Id = c.Int(),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .ForeignKey("dbo.Brokers", t => t.Broker_Id)
                .ForeignKey("dbo.BusinessInformations", t => t.BusinessInformation_Id)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformation_Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.UpdatedByUserId)
                .Index(t => t.Broker_Id)
                .Index(t => t.BusinessInformation_Id)
                .Index(t => t.PersonalInformation_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.Localities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        LocationId = c.Int(nullable: false),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
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
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Localities", t => t.BusinessLocalityId)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformationId)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
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
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Localities", t => t.LocalityId)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .Index(t => t.LocalityId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CreatedByUserId = c.Int(nullable: false),
                        IsParentCampaign = c.Boolean(),
                        ParentCampaignId = c.Int(),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        PersonalInformation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Campaigns", t => t.ParentCampaignId)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformation_Id)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ParentCampaignId)
                .Index(t => t.UpdatedByUserId)
                .Index(t => t.PersonalInformation_Id);
            
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
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
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
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformationId)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .Index(t => t.PersonalInformationId)
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
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Localities", t => t.LocalityId)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .Index(t => t.LocalityId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 200),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.UpdatedByUserId)
                .Index(t => t.Company_Id);
            
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
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
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
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactStatus", t => t.ContactStatusId)
                .ForeignKey("dbo.FacingTypes", t => t.FacingTypeId)
                .ForeignKey("dbo.LookingForTypes", t => t.LookingForTypeId)
                .ForeignKey("dbo.PersonalInformations", t => t.PersonalInformationId)
                .ForeignKey("dbo.TransactionTypes", t => t.TransactionTypeId)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
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
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.FacingTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.LookingForTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.TransactionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.UnitTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ContactEnquiry_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UpdatedByUserId)
                .ForeignKey("dbo.ContactEnquiries", t => t.ContactEnquiry_Id)
                .Index(t => t.UpdatedByUserId)
                .Index(t => t.ContactEnquiry_Id);
            
            CreateTable(
                "dbo.PermissionRolePermissions",
                c => new
                    {
                        Permission_Id = c.Int(nullable: false),
                        RolePermissions_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.RolePermissions_Id })
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.RolePermissions", t => t.RolePermissions_Id, cascadeDelete: true)
                .Index(t => t.Permission_Id)
                .Index(t => t.RolePermissions_Id);
            
            CreateTable(
                "dbo.RolePermissionsRoles",
                c => new
                    {
                        RolePermissions_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RolePermissions_Id, t.Role_Id })
                .ForeignKey("dbo.RolePermissions", t => t.RolePermissions_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.RolePermissions_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactEnquiries", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.UnitTypes", "ContactEnquiry_Id", "dbo.ContactEnquiries");
            DropForeignKey("dbo.UnitTypes", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.TransactionTypes", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.ContactEnquiries", "TransactionTypeId", "dbo.TransactionTypes");
            DropForeignKey("dbo.ContactEnquiries", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.LookingForTypes", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.ContactEnquiries", "LookingForTypeId", "dbo.LookingForTypes");
            DropForeignKey("dbo.FacingTypes", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.ContactEnquiries", "FacingTypeId", "dbo.FacingTypes");
            DropForeignKey("dbo.ContactEnquiries", "ContactStatusId", "dbo.ContactStatus");
            DropForeignKey("dbo.ContactStatus", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Brokers", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Localities", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Locations", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Localities", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Companies", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Companies", "LocalityId", "dbo.Localities");
            DropForeignKey("dbo.Developers", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Developers", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.ContactNumbers", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.BusinessInformations", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.PersonalInformations", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.PersonalInformationBookingMetas", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.PersonalInformationBookingMetas", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.PersonalInformations", "LocalityId", "dbo.Localities");
            DropForeignKey("dbo.CustomerEnquiryTypeCollections", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.CustomerEnquiryTypeCollections", "CustomerEnquiryTypeId", "dbo.CustomerEnquiryTypes");
            DropForeignKey("dbo.CustomerEnquiryTypes", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.ContactNumbers", "PersonalInformation_Id", "dbo.PersonalInformations");
            DropForeignKey("dbo.Campaigns", "PersonalInformation_Id", "dbo.PersonalInformations");
            DropForeignKey("dbo.Campaigns", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Campaigns", "ParentCampaignId", "dbo.Campaigns");
            DropForeignKey("dbo.Campaigns", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.BusinessInformations", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.BusinessInformations", "BusinessLocalityId", "dbo.Localities");
            DropForeignKey("dbo.ContactNumbers", "BusinessInformation_Id", "dbo.BusinessInformations");
            DropForeignKey("dbo.Brokers", "LocalityId", "dbo.Localities");
            DropForeignKey("dbo.ContactNumbers", "Broker_Id", "dbo.Brokers");
            DropForeignKey("dbo.ContactNumbers", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.AssignedTasks", "UpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.TasksRelatedFiles", "AssignedTask_Id", "dbo.AssignedTasks");
            DropForeignKey("dbo.AssignedTasks", "ParentTaskId", "dbo.AssignedTasks");
            DropForeignKey("dbo.AssignedTasks", "AssignedToId", "dbo.Users");
            DropForeignKey("dbo.AssignedTasks", "AssignedFromId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RolePermissionsRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.RolePermissionsRoles", "RolePermissions_Id", "dbo.RolePermissions");
            DropForeignKey("dbo.PermissionRolePermissions", "RolePermissions_Id", "dbo.RolePermissions");
            DropForeignKey("dbo.PermissionRolePermissions", "Permission_Id", "dbo.Permissions");
            DropIndex("dbo.RolePermissionsRoles", new[] { "Role_Id" });
            DropIndex("dbo.RolePermissionsRoles", new[] { "RolePermissions_Id" });
            DropIndex("dbo.PermissionRolePermissions", new[] { "RolePermissions_Id" });
            DropIndex("dbo.PermissionRolePermissions", new[] { "Permission_Id" });
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
            DropIndex("dbo.Developers", new[] { "Company_Id" });
            DropIndex("dbo.Developers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Companies", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Companies", new[] { "LocalityId" });
            DropIndex("dbo.PersonalInformationBookingMetas", new[] { "UpdatedByUserId" });
            DropIndex("dbo.PersonalInformationBookingMetas", new[] { "PersonalInformationId" });
            DropIndex("dbo.CustomerEnquiryTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.CustomerEnquiryTypeCollections", new[] { "CustomerEnquiryTypeId" });
            DropIndex("dbo.CustomerEnquiryTypeCollections", new[] { "PersonalInformationId" });
            DropIndex("dbo.Campaigns", new[] { "PersonalInformation_Id" });
            DropIndex("dbo.Campaigns", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Campaigns", new[] { "ParentCampaignId" });
            DropIndex("dbo.Campaigns", new[] { "CreatedByUserId" });
            DropIndex("dbo.PersonalInformations", new[] { "UpdatedByUserId" });
            DropIndex("dbo.PersonalInformations", new[] { "LocalityId" });
            DropIndex("dbo.BusinessInformations", new[] { "UpdatedByUserId" });
            DropIndex("dbo.BusinessInformations", new[] { "PersonalInformationId" });
            DropIndex("dbo.BusinessInformations", new[] { "BusinessLocalityId" });
            DropIndex("dbo.Localities", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Localities", new[] { "LocationId" });
            DropIndex("dbo.ContactNumbers", new[] { "Company_Id" });
            DropIndex("dbo.ContactNumbers", new[] { "PersonalInformation_Id" });
            DropIndex("dbo.ContactNumbers", new[] { "BusinessInformation_Id" });
            DropIndex("dbo.ContactNumbers", new[] { "Broker_Id" });
            DropIndex("dbo.ContactNumbers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Brokers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Brokers", new[] { "LocalityId" });
            DropIndex("dbo.TasksRelatedFiles", new[] { "AssignedTask_Id" });
            DropIndex("dbo.Permissions", new[] { "Name" });
            DropIndex("dbo.Roles", new[] { "Name" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.AssignedTasks", new[] { "UpdatedByUserId" });
            DropIndex("dbo.AssignedTasks", new[] { "ParentTaskId" });
            DropIndex("dbo.AssignedTasks", new[] { "AssignedToId" });
            DropIndex("dbo.AssignedTasks", new[] { "AssignedFromId" });
            DropTable("dbo.RolePermissionsRoles");
            DropTable("dbo.PermissionRolePermissions");
            DropTable("dbo.UnitTypes");
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.LookingForTypes");
            DropTable("dbo.FacingTypes");
            DropTable("dbo.ContactStatus");
            DropTable("dbo.ContactEnquiries");
            DropTable("dbo.Locations");
            DropTable("dbo.Developers");
            DropTable("dbo.Companies");
            DropTable("dbo.PersonalInformationBookingMetas");
            DropTable("dbo.CustomerEnquiryTypes");
            DropTable("dbo.CustomerEnquiryTypeCollections");
            DropTable("dbo.Campaigns");
            DropTable("dbo.PersonalInformations");
            DropTable("dbo.BusinessInformations");
            DropTable("dbo.Localities");
            DropTable("dbo.ContactNumbers");
            DropTable("dbo.Brokers");
            DropTable("dbo.TasksRelatedFiles");
            DropTable("dbo.Permissions");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.AssignedTasks");
        }
    }
}
