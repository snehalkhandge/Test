using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSalesUnit
{
    public class TowerDetailViewModel
    {
        public int Id { get; set; }

        public int BuildingNumber { get; set; }

        public string BuildingName { get; set; }
        public int NumberOfWings { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }
        public ProjectBaseViewModel Project { get; set; }

    }

    public class ListTowerViewModel
    {
        public ListTowerViewModel()
        {
            ProjectId = 0;
            Page = 1;
            PageSize = 10;
            TotalItems = 0;
        }
        public int ProjectId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int TotalItems { get; set; }
        public List<TowerDetailViewModel> Results { get; set; }

    }



}