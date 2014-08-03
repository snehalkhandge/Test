using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSalesUnit
{
    public class WingDetailViewModel
    {
        public int Id { get; set; }
        public string WingNumber { get; set; }
        public string WingName { get; set; }
        public int NumberOfFloors { get; set; }
        public int TowerId { get; set; }

        public TowerDetailViewModel Tower { get; set; }
        public int ProjectId { get; set; }
        public ProjectBaseViewModel Project { get; set; }



    }

    public class ListWingViewModel
    {
        public ListWingViewModel()
        {
            ProjectId = 0;
            TowerId = 0;
            Page = 1;
            PageSize = 10;
            TotalItems = 0;
            Results = new List<WingDetailViewModel>();
        }
        public int ProjectId { get; set; }

        public int TowerId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int TotalItems { get; set; }
        public List<WingDetailViewModel> Results { get; set; }

    }



}