using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSalesUnit
{
    public class TowerViewModel
    {
        public int Id { get; set; }

        public int BuildingNumber { get; set; }

        public string BuildingName { get; set; }
        public int NumberOfWings { get; set; }

        public int ProjectId { get; set; }

        public ProjectBaseViewModel Project { get; set; }

    }
}