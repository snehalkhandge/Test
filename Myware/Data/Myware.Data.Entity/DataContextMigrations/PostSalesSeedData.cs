using Myware.Data.Entity.Models.PostSalesUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.DataContextMigrations
{
    public class PostSalesSeedData
    {
        public static void SeedData(ApplicationDbContext context)
        {
            #region Projects                       

            var project = new Project { 
                LastUpdated = DateTime.UtcNow,
                ProjectId="Project-Name-Unique-Id",
                ProjectTypeId = 1,
                ProjectName = "Project Name 1",
                UpdatedByUserId = 1               
            };

            var project1 = new Project
            {
                LastUpdated = DateTime.UtcNow,
                ProjectId = "Project-Name-Unique-Id-2",
                ProjectTypeId = 2,
                ProjectName = "Project Name 2",
                UpdatedByUserId = 1
            };

            var project2 = new Project
            {
                LastUpdated = DateTime.UtcNow,
                ProjectId = "Project-Name-Unique-Id-3",
                ProjectTypeId = 3,
                ProjectName = "Project Name 3",
                UpdatedByUserId = 1
            };
            var project3 = new Project
            {
                LastUpdated = DateTime.UtcNow,
                ProjectId = "Project-Name-Unique-Id-4",
                ProjectTypeId = 1,
                ProjectName = "Project Name 4",
                UpdatedByUserId = 1
            };

            context.Projects.Add(project);
            context.Projects.Add(project1);
            context.Projects.Add(project2);
            context.Projects.Add(project3);
            context.SaveChanges();
            #endregion Projects

            #region Project Other Information

            var projectInformations = new List<ProjectOtherInformation>
            {
                new ProjectOtherInformation{
                    Address = "Amazing Address",
                    Amneties ="Amneties",
                    City = "Navi Mumbai",
                    CompanyId = 1,
                    FSI = 2,
                    Locality = "Santa Cruze",
                    NumberOfBuilding = 4,
                    NumberOfFlats = 5,
                    NumberOfShops = 5,
                    NumberOfOffices = 4,
                    PlotArea = "555",
                    PlotAreaUnit = "Hectare",
                    PlotNumber = "AX-567",
                    ProjectId = 1,
                    SurveyOrSectorNumber = "ZSD-5556"                   
                },
                new ProjectOtherInformation{
                    Address = "Amazing Address 1",
                    Amneties ="Amneties 1",
                    City = "Navi Mumbai",
                    CompanyId = 2,
                    FSI = 2,
                    Locality = "Santa Cruze 1",
                    NumberOfBuilding = 4,
                    NumberOfFlats = 5,
                    NumberOfShops = 5,
                    NumberOfOffices = 4,
                    PlotArea = "555",
                    PlotAreaUnit = "Hectare",
                    PlotNumber = "AX-563",
                    ProjectId = 1,
                    SurveyOrSectorNumber = "ZSD-5356"                   
                },
                new ProjectOtherInformation{
                    Address = "Amazing Address 2",
                    Amneties ="Amneties 2",
                    City = "Navi Mumbai",
                    CompanyId = 1,
                    FSI = 2,
                    Locality = "Santa Cruze",
                    NumberOfBuilding = 4,
                    NumberOfFlats = 5,
                    NumberOfShops = 5,
                    NumberOfOffices = 4,
                    PlotArea = "535",
                    PlotAreaUnit = "Hectare",
                    PlotNumber = "ASX-567",
                    ProjectId = 1,
                    SurveyOrSectorNumber = "ZSX-5556"                   
                },
                new ProjectOtherInformation{
                    Address = "Amazing Address 3",
                    Amneties ="Amneties 3",
                    City = "Navi Mumbai",
                    CompanyId = 1,
                    FSI = 2,
                    Locality = "Santa Cruze",
                    NumberOfBuilding = 4,
                    NumberOfFlats = 5,
                    NumberOfShops = 5,
                    NumberOfOffices = 4,
                    PlotArea = "545",
                    PlotAreaUnit = "Hectare",
                    PlotNumber = "AXD-567",
                    ProjectId = 1,
                    SurveyOrSectorNumber = "SSD-5556"                   
                }
            };

            context.ProjectInformation.AddRange(projectInformations);
            context.SaveChanges();
            #endregion

            #region Project Developers

            var developers = new List<ProjectDevelopers>
            {
                new ProjectDevelopers{
                    DeveloperId =1,
                    DeveloperName ="Developer 1",
                    ProjectOtherInformationId = 1,
                },
                new ProjectDevelopers{
                    DeveloperId =1,
                    DeveloperName ="Developer 1",
                    ProjectOtherInformationId = 2,
                },
                new ProjectDevelopers{
                    DeveloperId =1,
                    DeveloperName ="Developer 1",
                    ProjectOtherInformationId = 3,
                },new ProjectDevelopers{
                    DeveloperId =1,
                    DeveloperName ="Developer 1",
                    ProjectOtherInformationId = 4,
                },new ProjectDevelopers{
                    DeveloperId =2,
                    DeveloperName ="Developer 2",
                    ProjectOtherInformationId = 1,
                },new ProjectDevelopers{
                    DeveloperId =1,
                    DeveloperName ="Developer 3",
                    ProjectOtherInformationId = 3,
                }

            };
            context.ProjectDevelopers.AddRange(developers);
            context.SaveChanges();
            #endregion

            #region Property Charges

            var propertyCharges = new List<ProjectPropertyCharges>
            {
                new ProjectPropertyCharges{
                    BasicQuateRate = 555,
                    DevelopmentCharge = 444,
                    FloorNumberOnWord = 333,
                    FloorRiseRate = 222,
                    GracePeriod = 30,
                    LumpSum = 5555,
                    OtherCharge = 4444,
                    PenaltyDefaulter = 444,
                    ProjectId = 1
                },
                new ProjectPropertyCharges{
                    BasicQuateRate = 5551,
                    DevelopmentCharge = 4441,
                    FloorNumberOnWord = 3331,
                    FloorRiseRate = 2221,
                    GracePeriod = 301,
                    LumpSum = 55551,
                    OtherCharge = 44441,
                    PenaltyDefaulter = 4441,
                    ProjectId = 2
                },
                new ProjectPropertyCharges{
                    BasicQuateRate = 5552,
                    DevelopmentCharge = 4442,
                    FloorNumberOnWord = 3332,
                    FloorRiseRate = 2222,
                    GracePeriod = 302,
                    LumpSum = 55552,
                    OtherCharge = 44442,
                    PenaltyDefaulter = 4442,
                    ProjectId = 3
                },
                new ProjectPropertyCharges{
                    BasicQuateRate = 5553,
                    DevelopmentCharge = 4443,
                    FloorNumberOnWord = 3333,
                    FloorRiseRate = 2223,
                    GracePeriod = 303,
                    LumpSum = 55553,
                    OtherCharge = 44443,
                    PenaltyDefaulter = 4443,
                    ProjectId = 4
                }

            };

            context.ProjectPropertyCharges.AddRange(propertyCharges);            
            context.SaveChanges();
            #endregion

            #region Project Bank Details

            var bankDetails = new List<ProjectBankDetails>
            {
                new ProjectBankDetails{
                    AccountNumber = "8898989",
                    BankName = "Abhinav Bank",
                    BranchName = "Mumbai",
                    ProjectId = 1
                },
                new ProjectBankDetails{
                    AccountNumber = "88989891",
                    BankName = "Abhinav Bank",
                    BranchName = "Mumbai",
                    ProjectId = 1
                },
                new ProjectBankDetails{
                    AccountNumber = "88989892",
                    BankName = "Abhinav Bank",
                    BranchName = "Mumbai",
                    ProjectId = 1
                },
                new ProjectBankDetails{
                    AccountNumber = "88989822419",
                    BankName = "Abhinav Bank 5",
                    BranchName = "Mumbai",
                    ProjectId = 2
                },
                new ProjectBankDetails{
                    AccountNumber = "8898955589",
                    BankName = "Abhinav Bank 5",
                    BranchName = "Mumbai",
                    ProjectId = 3
                },new ProjectBankDetails{
                    AccountNumber = "85856898989",
                    BankName = "Abhinav Bank 4",
                    BranchName = "Mumbai",
                    ProjectId = 4
                }
            };

            context.ProjectBankDetails.AddRange(bankDetails);
            context.SaveChanges();
            #endregion

            #region Project Parking types

            var parkingTypes = new List<ProjectParkingType>
            {
                new ProjectParkingType{
                    Count = 10,
                    Type = "Open",
                    ProjectId = 1
                },
                new ProjectParkingType{
                    Count = 10,
                    Type = "Open",
                    ProjectId = 2
                },
                new ProjectParkingType{
                    Count = 10,
                    Type = "Open",
                    ProjectId = 3
                },
                new ProjectParkingType{
                    Count = 10,
                    Type = "Open",
                    ProjectId = 4
                }
            };

            context.ProjectParkingTypes.AddRange(parkingTypes);
            context.SaveChanges();
            #endregion

            #region Towers
            var towerrs = new List<Tower>
            {
                new Tower{
                    BuildingName = "Building Name",
                    BuildingNumber = 5,
                    NumberOfWings = 10,
                    ProjectId = 1
                },
                new Tower{
                    BuildingName = "Building Name 1",
                    BuildingNumber = 5,
                    NumberOfWings = 10,
                    ProjectId = 2
                },
                new Tower{
                    BuildingName = "Building Name 2",
                    BuildingNumber = 5,
                    NumberOfWings = 10,
                    ProjectId = 3
                },
                new Tower{
                    BuildingName = "Building Name 3",
                    BuildingNumber = 5,
                    NumberOfWings = 10,
                    ProjectId = 4
                }
            };

            context.Towers.AddRange(towerrs);
            context.SaveChanges();
            #endregion

            #region Wings

            var wings = new List<Wing>{

                new Wing{
                    NumberOfFloors = 5,
                    ProjectId = 1,
                    TowerId =1,
                    WingName = "Wing Name",
                    WingNumber = "Wing Number"                    
                },
                new Wing{
                    NumberOfFloors = 5,
                    ProjectId = 2,
                    TowerId =2,
                    WingName = "Wing Name 2",
                    WingNumber = "Wing Number 2"                    
                },
                new Wing{
                    NumberOfFloors = 5,
                    ProjectId = 3,
                    TowerId =3,
                    WingName = "Wing Name 3",
                    WingNumber = "Wing Number 3"                    
                },
                new Wing{
                    NumberOfFloors = 5,
                    ProjectId = 4,
                    TowerId =4,
                    WingName = "Wing Name 4",
                    WingNumber = "Wing Number 4"                    
                }
            };

            context.Wings.AddRange(wings);
            context.SaveChanges();
            #endregion

            #region Units

            var units = new List<Unit>{
                new Unit{
                    BasicRate = 555,
                    BuildingType = "Residential",
                    CarpetArea = 555,
                    CarpetAreaUnit = "Sq. feet",
                    DevelopmentCharge = 555,
                    FloorNumber = 1,
                    FloorRiseRate = 5000,
                    OtherCharge = 8000,
                    ProjectId = 1,
                    SaleableArea = 4444,
                    SaleableAreaUnit = "Sq. feet",
                    Status = "Open",
                    TowerId = 1,
                    UnitName ="Unit Name ",
                    UnitNumber = 1,
                    UnitType ="1 BHK",
                    WingId = 1                   
                },
                 new Unit{
                    BasicRate = 555,
                    BuildingType = "Residential",
                    CarpetArea = 555,
                    CarpetAreaUnit = "Sq. feet",
                    DevelopmentCharge = 555,
                    FloorNumber = 1,
                    FloorRiseRate = 5000,
                    OtherCharge = 8000,
                    ProjectId = 1,
                    SaleableArea = 4444,
                    SaleableAreaUnit = "Sq. feet",
                    Status = "Open",
                    TowerId = 1,
                    UnitName ="Unit Name ",
                    UnitNumber = 2,
                    UnitType ="1 BHK",
                    WingId = 1                   
                },
                 new Unit{
                    BasicRate = 555,
                    BuildingType = "Residential",
                    CarpetArea = 555,
                    CarpetAreaUnit = "Sq. feet",
                    DevelopmentCharge = 555,
                    FloorNumber = 1,
                    FloorRiseRate = 5000,
                    OtherCharge = 8000,
                    ProjectId = 1,
                    SaleableArea = 4444,
                    SaleableAreaUnit = "Sq. feet",
                    Status = "Open",
                    TowerId = 1,
                    UnitName ="Unit Name ",
                    UnitNumber = 3,
                    UnitType ="1 BHK",
                    WingId = 1                   
                }
            };

            context.Units.AddRange(units);
            context.SaveChanges();

            #endregion

        }
    }
}