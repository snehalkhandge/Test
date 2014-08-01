using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSalesUnit
{
    public class WingViewModel
    {
        public int Id { get; set; }
        public string WingNumber { get; set; }
        public string WingName { get; set; }
        public int NumberOfFloors { get; set; }
        public int TowerId { get; set; }

        public TowerViewModel Tower { get; set; }
        public int ProjectId { get; set; }
        public ProjectBaseViewModel Project { get; set; }

    }
}