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
        public ProjectTypeViewModel ProjectType { get; set; }




    }

    public class ProjectOtherInformationViewModel
    {
        
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
        public int DeveloperId { get; set; }
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
    }

    public class ProjectBankDetailsViewModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public int ProjectId { get; set; }
    }

    public class ProjectPropertyCharges
    {
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
    }

    public class ProjectParkingType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
        public int ProjectId { get; set; }
    }

    public class ProjectTypeViewModel    
    {
        public string Name { get; set; }
        public int UserId { get; set; }

    }

}