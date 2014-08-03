using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSalesUnit
{
    public class UnitViewModel
    {
        public int Id { get; set; }        
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
        public int WingId { get; set; }
        public WingDetailViewModel Wing { get; set; }
        public int TowerId { get; set; }
        public TowerDetailViewModel Tower { get; set; }
        public int ProjectId { get; set; }
        public ProjectBaseViewModel Project { get; set; }


    }
}