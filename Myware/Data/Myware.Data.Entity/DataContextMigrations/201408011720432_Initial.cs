namespace Myware.Data.Entity.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agreements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgreementNumber = c.String(),
                        AgreementPreparedBy = c.String(),
                        PreparedOn = c.DateTime(),
                        AgreementSignedBy = c.String(),
                        SignedOn = c.DateTime(),
                        AgreementCost = c.Decimal(precision: 18, scale: 2),
                        ExtraAmentiesCost = c.Decimal(precision: 18, scale: 2),
                        MarketValue = c.Decimal(precision: 18, scale: 2),
                        VATPercentage = c.Decimal(precision: 18, scale: 2),
                        ExemptionPercentage = c.Decimal(precision: 18, scale: 2),
                        ServiceTaxPercentage = c.Decimal(precision: 18, scale: 2),
                        SalesTaxPercentage = c.Decimal(precision: 18, scale: 2),
                        SocietyMaintenanceCharge = c.Decimal(precision: 18, scale: 2),
                        AgreementRegisteredData = c.DateTime(),
                        Remarks = c.String(),
                        ProjectId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingCustomers", t => t.CustomerId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.BookingCustomers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Salutation = c.String(),
                        CustomerName = c.String(),
                        DateOfBirth = c.DateTime(),
                        Age = c.Decimal(precision: 18, scale: 2),
                        Email = c.String(),
                        MobileNumber = c.String(),
                        PhoneNumber = c.String(),
                        Son_Wife_Daughter_Of = c.String(),
                        PanNumber = c.String(),
                        Address = c.String(),
                        Nationality = c.String(),
                        BookingDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingDetails", t => t.BookingDetailId)
                .Index(t => t.BookingDetailId);
            
            CreateTable(
                "dbo.BookingDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReferenceNumber = c.String(),
                        BookingNumber = c.String(),
                        BookingDate = c.DateTime(),
                        SaleableArea = c.Decimal(precision: 18, scale: 2),
                        CarpetArea = c.Decimal(precision: 18, scale: 2),
                        BasicRate = c.Decimal(precision: 18, scale: 2),
                        FloorRiseRate = c.Decimal(precision: 18, scale: 2),
                        DevelopmentCharge = c.Decimal(precision: 18, scale: 2),
                        ParkingCharge = c.Decimal(precision: 18, scale: 2),
                        OtherChargeName = c.String(),
                        OtherCharge = c.Decimal(precision: 18, scale: 2),
                        TotalAgreementCost = c.Decimal(precision: 18, scale: 2),
                        MaintenanceCharge = c.Decimal(precision: 18, scale: 2),
                        LegalCharge = c.Decimal(precision: 18, scale: 2),
                        SecurityCharge = c.Decimal(precision: 18, scale: 2),
                        SocietyCharge = c.Decimal(precision: 18, scale: 2),
                        MSEBCharge = c.Decimal(precision: 18, scale: 2),
                        ClubCharge = c.Decimal(precision: 18, scale: 2),
                        MiscellaneousCharge = c.Decimal(precision: 18, scale: 2),
                        RegistrationCharge = c.Decimal(precision: 18, scale: 2),
                        VatPercentage = c.Decimal(precision: 18, scale: 2),
                        Vat = c.Decimal(precision: 18, scale: 2),
                        ServiceTaxPercentage = c.Decimal(precision: 18, scale: 2),
                        ServiceTax = c.Decimal(precision: 18, scale: 2),
                        ValueAsGovernmentPercentage = c.Decimal(precision: 18, scale: 2),
                        ValueAsGovernment = c.Decimal(precision: 18, scale: 2),
                        TotalCost = c.Decimal(precision: 18, scale: 2),
                        ChequeDate = c.DateTime(),
                        ReceiptDate = c.DateTime(),
                        ReceiptNumber = c.String(),
                        ChequeNumber = c.String(),
                        BookingAmount = c.Decimal(precision: 18, scale: 2),
                        DrawnOnBank = c.String(),
                        Branch = c.String(),
                        ReferenceBy = c.String(),
                        ProjectId = c.Int(),
                        TowerId = c.Int(),
                        WingId = c.Int(),
                        UnitId = c.Int(),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.Towers", t => t.TowerId)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .ForeignKey("dbo.Wings", t => t.WingId)
                .Index(t => t.ProjectId)
                .Index(t => t.TowerId)
                .Index(t => t.WingId)
                .Index(t => t.UnitId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.AllotedParkings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParkingType = c.String(),
                        ParkingSizeW = c.String(),
                        ParkingSizeH = c.String(),
                        BookingDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingDetails", t => t.BookingDetailId)
                .Index(t => t.BookingDetailId);
            
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstallmentNumber = c.Int(nullable: false),
                        Phase = c.String(),
                        Payment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpectedPaymentDate = c.DateTime(),
                        PaymentDate = c.DateTime(),
                        PaymentMode = c.String(),
                        AmountPaid = c.Decimal(precision: 18, scale: 2),
                        InterestAmount = c.Decimal(precision: 18, scale: 2),
                        InterestAmountPaid = c.Decimal(precision: 18, scale: 2),
                        ServiceTaxAmount = c.Decimal(precision: 18, scale: 2),
                        ServiceTaxAmountPaid = c.Decimal(precision: 18, scale: 2),
                        BookingDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingDetails", t => t.BookingDetailId)
                .Index(t => t.BookingDetailId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.String(nullable: false, maxLength: 200),
                        ProjectName = c.String(nullable: false, maxLength: 200),
                        ProjectTypeId = c.Int(nullable: false),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectTypes", t => t.ProjectTypeId)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.ProjectTypeId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.ProjectBankDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(),
                        BankName = c.String(),
                        BranchName = c.String(),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.ProjectParkingTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Count = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.ProjectOtherInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlotNumber = c.String(maxLength: 200),
                        SurveyOrSectorNumber = c.String(maxLength: 200),
                        Locality = c.String(maxLength: 200),
                        City = c.String(maxLength: 200),
                        PlotArea = c.String(maxLength: 200),
                        PlotAreaUnit = c.String(maxLength: 200),
                        Address = c.String(maxLength: 500),
                        FSI = c.Int(nullable: false),
                        NumberOfBuilding = c.Int(nullable: false),
                        NumberOfShops = c.Int(nullable: false),
                        NumberOfFlats = c.Int(nullable: false),
                        NumberOfOffices = c.Int(nullable: false),
                        Amneties = c.String(maxLength: 800),
                        FloorPlan = c.String(),
                        CompanyId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.CompanyId)
                .Index(t => t.ProjectId);
            
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
                "dbo.ProjectDevelopers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeveloperId = c.Int(nullable: false),
                        DeveloperName = c.String(),
                        ProjectOtherInformationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectOtherInformations", t => t.ProjectOtherInformationId)
                .Index(t => t.ProjectOtherInformationId);
            
            CreateTable(
                "dbo.ProjectTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        UpdatedByUserId = c.Int(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByUserId)
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.ProjectPropertyCharges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DevelopmentCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LumpSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BasicQuateRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FloorRiseRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FloorNumberOnWord = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PenaltyDefaulter = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GracePeriod = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Towers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingNumber = c.Int(nullable: false),
                        BuildingName = c.String(),
                        NumberOfWings = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BasicRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DevelopmentCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FloorRiseRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitType = c.String(),
                        BuildingType = c.String(),
                        SaleableArea = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleableAreaUnit = c.String(),
                        CarpetArea = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarpetAreaUnit = c.String(),
                        Status = c.String(),
                        WingId = c.Int(nullable: false),
                        TowerId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.Towers", t => t.TowerId)
                .ForeignKey("dbo.Wings", t => t.WingId)
                .Index(t => t.WingId)
                .Index(t => t.TowerId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Wings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WingNumber = c.String(),
                        WingName = c.String(),
                        NumberOfFloors = c.Int(nullable: false),
                        TowerId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.Towers", t => t.TowerId)
                .Index(t => t.TowerId)
                .Index(t => t.ProjectId);
            
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
                        InvestmentCapacity = c.Decimal(precision: 18, scale: 2),
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
                .Index(t => t.UpdatedByUserId);
            
            CreateTable(
                "dbo.ContactEnquiries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Remarks = c.String(),
                        AssignedDate = c.DateTime(),
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
                "dbo.DocumentManagements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileUrl = c.String(),
                        DocumentName = c.String(),
                        Remark = c.String(),
                        ProjectId = c.Int(nullable: false),
                        BookingDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingDetails", t => t.BookingDetailId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId)
                .Index(t => t.BookingDetailId);
            
            CreateTable(
                "dbo.DuplicateDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactType = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ContactNumbers = c.Long(),
                        Email = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Locality = c.String(),
                        PinCode = c.String(),
                        DateOfBirth = c.DateTime(),
                        AnniversaryDate = c.DateTime(),
                        Campaign = c.String(),
                        SubCampaign = c.String(),
                        CompanyName = c.String(),
                        Designation = c.String(),
                        BusinessOrIndustry = c.String(),
                        BusinessCity = c.String(),
                        BusinessLocality = c.String(),
                        InvestmentCapacity = c.Decimal(precision: 18, scale: 2),
                        BusinessContactNumbers = c.Long(),
                        Fax = c.String(),
                        Website = c.String(),
                        EnquiryDate = c.DateTime(),
                        LookingForType = c.String(),
                        PreferredUnitTypes = c.String(),
                        TransactionType = c.String(),
                        PropertyAge = c.String(),
                        BudgetFrom = c.Decimal(precision: 18, scale: 2),
                        BudgetTo = c.Decimal(precision: 18, scale: 2),
                        SaleAreaFrom = c.Decimal(precision: 18, scale: 2),
                        SaleAreaTo = c.Decimal(precision: 18, scale: 2),
                        CarpetAreaFrom = c.Decimal(precision: 18, scale: 2),
                        CarpetAreaTo = c.Decimal(precision: 18, scale: 2),
                        OfferedRate = c.Decimal(precision: 18, scale: 2),
                        IsFurnished = c.Boolean(),
                        PersonalInformationId = c.Int(nullable: false),
                        LeadStatus = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.HotProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Installments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstallmentNumber = c.Int(nullable: false),
                        Phase = c.String(),
                        PaymentPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServiceTaxPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ScheduleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId)
                .Index(t => t.ScheduleId);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.Installments", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.HotProperties", "UnitId", "dbo.Units");
            DropForeignKey("dbo.FacingTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DocumentManagements", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.DocumentManagements", "BookingDetailId", "dbo.BookingDetails");
            DropForeignKey("dbo.CustomerEnquiryTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactStatus", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactNumbers", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Developers", "ContactNumber_Id", "dbo.ContactNumbers");
            DropForeignKey("dbo.Campaigns", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Campaigns", "ParentCampaignId", "dbo.Campaigns");
            DropForeignKey("dbo.BusinessInformations", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonalInformations", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonalContactNumbers", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.ContactEnquiries", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactEnquiryUnitTypes", "ContactEnquiryId", "dbo.ContactEnquiries");
            DropForeignKey("dbo.ContactEnquiryLocalities", "ContactEnquiryId", "dbo.ContactEnquiries");
            DropForeignKey("dbo.ContactEnquiries", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.BusinessInformations", "PersonalInformationId", "dbo.PersonalInformations");
            DropForeignKey("dbo.BusinessContactNumbers", "BusinessInformationId", "dbo.BusinessInformations");
            DropForeignKey("dbo.AssignedTasks", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TasksRelatedFiles", "AssignedTaskId", "dbo.AssignedTasks");
            DropForeignKey("dbo.AssignedTasks", "ParentTaskId", "dbo.AssignedTasks");
            DropForeignKey("dbo.AssignedTasks", "AssignedToId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignedTasks", "AssignedFromId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Agreements", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Agreements", "CustomerId", "dbo.BookingCustomers");
            DropForeignKey("dbo.BookingDetails", "WingId", "dbo.Wings");
            DropForeignKey("dbo.BookingDetails", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookingDetails", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Units", "WingId", "dbo.Wings");
            DropForeignKey("dbo.Wings", "TowerId", "dbo.Towers");
            DropForeignKey("dbo.Wings", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Units", "TowerId", "dbo.Towers");
            DropForeignKey("dbo.Units", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.BookingDetails", "TowerId", "dbo.Towers");
            DropForeignKey("dbo.Towers", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.BookingDetails", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectPropertyCharges", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ProjectTypeId", "dbo.ProjectTypes");
            DropForeignKey("dbo.ProjectTypes", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectDevelopers", "ProjectOtherInformationId", "dbo.ProjectOtherInformations");
            DropForeignKey("dbo.ProjectOtherInformations", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectOtherInformations", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Companies", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Localities", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Locations", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Localities", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Companies", "LocalityId", "dbo.Localities");
            DropForeignKey("dbo.Brokers", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Brokers", "LocalityId", "dbo.Localities");
            DropForeignKey("dbo.BrokerContactNumbers", "BrokerId", "dbo.Brokers");
            DropForeignKey("dbo.Developers", "UpdatedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DeveloperCompanies", "DeveloperId", "dbo.Developers");
            DropForeignKey("dbo.DeveloperCompanies", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.CompanyContactNumbers", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.ProjectParkingTypes", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectBankDetails", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.PaymentDetails", "BookingDetailId", "dbo.BookingDetails");
            DropForeignKey("dbo.BookingCustomers", "BookingDetailId", "dbo.BookingDetails");
            DropForeignKey("dbo.AllotedParkings", "BookingDetailId", "dbo.BookingDetails");
            DropIndex("dbo.UnitTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.TransactionTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RolePermissions", new[] { "PermissionId" });
            DropIndex("dbo.RolePermissions", new[] { "RoleId" });
            DropIndex("dbo.Permissions", new[] { "Name" });
            DropIndex("dbo.LookingForTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Installments", new[] { "ScheduleId" });
            DropIndex("dbo.HotProperties", new[] { "UnitId" });
            DropIndex("dbo.FacingTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.DocumentManagements", new[] { "BookingDetailId" });
            DropIndex("dbo.DocumentManagements", new[] { "ProjectId" });
            DropIndex("dbo.CustomerEnquiryTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.ContactStatus", new[] { "UpdatedByUserId" });
            DropIndex("dbo.ContactNumbers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Campaigns", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Campaigns", new[] { "ParentCampaignId" });
            DropIndex("dbo.PersonalContactNumbers", new[] { "PhoneNumber" });
            DropIndex("dbo.PersonalContactNumbers", new[] { "PersonalInformationId" });
            DropIndex("dbo.ContactEnquiryUnitTypes", new[] { "ContactEnquiryId" });
            DropIndex("dbo.ContactEnquiryLocalities", new[] { "ContactEnquiryId" });
            DropIndex("dbo.ContactEnquiries", new[] { "UpdatedByUserId" });
            DropIndex("dbo.ContactEnquiries", new[] { "PersonalInformationId" });
            DropIndex("dbo.PersonalInformations", new[] { "UpdatedByUserId" });
            DropIndex("dbo.BusinessInformations", new[] { "UpdatedByUserId" });
            DropIndex("dbo.BusinessInformations", new[] { "PersonalInformationId" });
            DropIndex("dbo.BusinessContactNumbers", new[] { "BusinessInformationId" });
            DropIndex("dbo.TasksRelatedFiles", new[] { "AssignedTaskId" });
            DropIndex("dbo.AssignedTasks", new[] { "UpdatedByUserId" });
            DropIndex("dbo.AssignedTasks", new[] { "ParentTaskId" });
            DropIndex("dbo.AssignedTasks", new[] { "AssignedToId" });
            DropIndex("dbo.AssignedTasks", new[] { "AssignedFromId" });
            DropIndex("dbo.Wings", new[] { "ProjectId" });
            DropIndex("dbo.Wings", new[] { "TowerId" });
            DropIndex("dbo.Units", new[] { "ProjectId" });
            DropIndex("dbo.Units", new[] { "TowerId" });
            DropIndex("dbo.Units", new[] { "WingId" });
            DropIndex("dbo.Towers", new[] { "ProjectId" });
            DropIndex("dbo.ProjectPropertyCharges", new[] { "ProjectId" });
            DropIndex("dbo.ProjectTypes", new[] { "UpdatedByUserId" });
            DropIndex("dbo.ProjectDevelopers", new[] { "ProjectOtherInformationId" });
            DropIndex("dbo.Locations", new[] { "UpdatedByUserId" });
            DropIndex("dbo.BrokerContactNumbers", new[] { "BrokerId" });
            DropIndex("dbo.Brokers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Brokers", new[] { "LocalityId" });
            DropIndex("dbo.Localities", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Localities", new[] { "LocationId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Developers", new[] { "ContactNumber_Id" });
            DropIndex("dbo.Developers", new[] { "UpdatedByUserId" });
            DropIndex("dbo.DeveloperCompanies", new[] { "CompanyId" });
            DropIndex("dbo.DeveloperCompanies", new[] { "DeveloperId" });
            DropIndex("dbo.CompanyContactNumbers", new[] { "CompanyId" });
            DropIndex("dbo.Companies", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Companies", new[] { "LocalityId" });
            DropIndex("dbo.ProjectOtherInformations", new[] { "ProjectId" });
            DropIndex("dbo.ProjectOtherInformations", new[] { "CompanyId" });
            DropIndex("dbo.ProjectParkingTypes", new[] { "ProjectId" });
            DropIndex("dbo.ProjectBankDetails", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "UpdatedByUserId" });
            DropIndex("dbo.Projects", new[] { "ProjectTypeId" });
            DropIndex("dbo.PaymentDetails", new[] { "BookingDetailId" });
            DropIndex("dbo.AllotedParkings", new[] { "BookingDetailId" });
            DropIndex("dbo.BookingDetails", new[] { "UpdatedByUserId" });
            DropIndex("dbo.BookingDetails", new[] { "UnitId" });
            DropIndex("dbo.BookingDetails", new[] { "WingId" });
            DropIndex("dbo.BookingDetails", new[] { "TowerId" });
            DropIndex("dbo.BookingDetails", new[] { "ProjectId" });
            DropIndex("dbo.BookingCustomers", new[] { "BookingDetailId" });
            DropIndex("dbo.Agreements", new[] { "CustomerId" });
            DropIndex("dbo.Agreements", new[] { "ProjectId" });
            DropTable("dbo.UnitTypes");
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Permissions");
            DropTable("dbo.LookingForTypes");
            DropTable("dbo.Schedules");
            DropTable("dbo.Installments");
            DropTable("dbo.HotProperties");
            DropTable("dbo.FacingTypes");
            DropTable("dbo.DuplicateDatas");
            DropTable("dbo.DocumentManagements");
            DropTable("dbo.CustomerEnquiryTypes");
            DropTable("dbo.ContactStatus");
            DropTable("dbo.ContactNumbers");
            DropTable("dbo.Campaigns");
            DropTable("dbo.PersonalContactNumbers");
            DropTable("dbo.ContactEnquiryUnitTypes");
            DropTable("dbo.ContactEnquiryLocalities");
            DropTable("dbo.ContactEnquiries");
            DropTable("dbo.PersonalInformations");
            DropTable("dbo.BusinessInformations");
            DropTable("dbo.BusinessContactNumbers");
            DropTable("dbo.TasksRelatedFiles");
            DropTable("dbo.AssignedTasks");
            DropTable("dbo.Wings");
            DropTable("dbo.Units");
            DropTable("dbo.Towers");
            DropTable("dbo.ProjectPropertyCharges");
            DropTable("dbo.ProjectTypes");
            DropTable("dbo.ProjectDevelopers");
            DropTable("dbo.Locations");
            DropTable("dbo.BrokerContactNumbers");
            DropTable("dbo.Brokers");
            DropTable("dbo.Localities");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Developers");
            DropTable("dbo.DeveloperCompanies");
            DropTable("dbo.CompanyContactNumbers");
            DropTable("dbo.Companies");
            DropTable("dbo.ProjectOtherInformations");
            DropTable("dbo.ProjectParkingTypes");
            DropTable("dbo.ProjectBankDetails");
            DropTable("dbo.Projects");
            DropTable("dbo.PaymentDetails");
            DropTable("dbo.AllotedParkings");
            DropTable("dbo.BookingDetails");
            DropTable("dbo.BookingCustomers");
            DropTable("dbo.Agreements");
        }
    }
}
