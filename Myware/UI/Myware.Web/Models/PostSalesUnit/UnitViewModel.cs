using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSalesUnit
{
    public class UnitDetailViewModel
    {
        
        public int Id { get; set; }

        public string UnitName { get; set; }
        public decimal BasicRate { get; set; }
        public decimal DevelopmentCharge { get; set; }
        public decimal FloorRiseRate { get; set; }
        public decimal OtherCharge { get; set; }
        public string UnitType { get; set; }
        public string BuildingType { get; set; }
        public decimal SaleableArea { get; set; }
        public string SaleableAreaUnit { get; set; }
        public decimal CarpetArea { get; set; }
        public string CarpetAreaUnit { get; set; }
        public string Status { get; set; }
        public int UnitNumber { get; set; }
        public int FloorNumber { get; set; }

        public int WingId { get; set; }
        public WingDetailViewModel Wing { get; set; }
        public int TowerId { get; set; }
        public TowerDetailViewModel Tower { get; set; }
        public int ProjectId { get; set; }
        public ProjectBaseViewModel Project { get; set; }


    }

    public class ListUnitViewModel
    {
        public ListUnitViewModel()
        {
            ProjectId = 0;
            TowerId = 0;
            WingId = 0;
            Page = 1;
            PageSize = 10;
            TotalItems = 0;
        }
        public int ProjectId { get; set; }
        public int WingId { get; set; }
        public int TowerId { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }

        public int TotalItems { get; set; }
        public List<UnitDetailViewModel> Results { get; set; }

    }
}