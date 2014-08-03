using Myware.Web.Models.PreSalesUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSalesUnit
{
    public class ProjectBaseViewModel
    {
        public int Id { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int ProjectTypeId { get; set; }

        public int UserId { get; set; }
        public ProjectTypeViewModel ProjectType { get; set; }

    }

    public class ProjectOtherInformationViewModel
    {
        public ProjectOtherInformationViewModel()
        {
            Company = new CreateCompanyViewModel();
            Developers = new List<ProjectDeveloeperViewModel>();
        }
        public int Id { get; set; }
        public string PlotNumber { get; set; }
        public string SurveyOrSectorNumber { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }                
        public string PlotArea { get; set; }                
        public string PlotAreaUnit { get; set; }                
        public string Address { get; set; }
        public int FSI { get; set; }                
        public int NumberOfBuilding { get; set; }                
        public int NumberOfShops { get; set; }
        public int NumberOfFlats { get; set; }
        public int NumberOfOffices { get; set; }
        public string Amneties { get; set; }
        public string FloorPlan { get; set; }
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }

        public CreateCompanyViewModel Company { get; set; }
        public List<ProjectDeveloeperViewModel> Developers { get; set; }
        

    }

    public class ProjectDeveloeperViewModel
    {
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }
        public int ProjectOtherInformationId { get; set; }
    }
    public class ProjectBankDetailsViewModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public int ProjectId { get; set; }
    }

    public class ProjectPropertyChargesViewModel
    {
        public ProjectPropertyChargesViewModel()
        {
            Parkings = new List<ProjectParkingTypeViewModel>();
        }
        public int Id { get; set; }
        public decimal DevelopmentCharge { get; set; }
        public decimal OtherCharge { get; set; }
        public decimal LumpSum { get; set; }
        public decimal BasicQuateRate { get; set; }
        public decimal FloorRiseRate { get; set; }
        public decimal FloorNumberOnWord { get; set; }
        public decimal PenaltyDefaulter { get; set; }
        public int GracePeriod { get; set; }
        public int ProjectId { get; set; }

        public List<ProjectParkingTypeViewModel> Parkings { get; set; }
    }

    public class ProjectParkingTypeViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
        public int ProjectId { get; set; }
    }

    public class ProjectTypeViewModel    
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

    }


    public class ProjectDetailViewModel
    {
        public ProjectDetailViewModel()
        {
            ProjectBase = new ProjectBaseViewModel();
            BankDetails = new List<ProjectBankDetailsViewModel>();
            Parkings = new List<ProjectParkingTypeViewModel>();
            ProjectInformation = new ProjectOtherInformationViewModel();
            PropertyCharges = new ProjectPropertyChargesViewModel();
        }
        public ProjectBaseViewModel ProjectBase { get; set; }
        public List<ProjectBankDetailsViewModel> BankDetails { get; set; }
        public List<ProjectParkingTypeViewModel> Parkings { get; set; }
        public ProjectOtherInformationViewModel ProjectInformation { get; set; }

        public ProjectPropertyChargesViewModel PropertyCharges { get; set; }

    }

    public class ListSearchResultProjectViewModel
    {
        public ListSearchResultProjectViewModel()
        {
            Page = 1;
            PageSize = 10;            
        }
        public int TotalItems { get; set; }

        public string CompanyName { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
        public List<SearchResultProjectViewModel> Results { get; set; }
    }

    public class SearchResultProjectViewModel
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }

        public string CompanyName { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }

        public string PlotArea { get; set; }

        public int NumberOfBuilding { get; set; }

    }


    public class ProjectNameViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

}