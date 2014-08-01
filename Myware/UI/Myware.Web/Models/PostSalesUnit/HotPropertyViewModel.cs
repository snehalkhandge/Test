using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSalesUnit
{
    public class HotPropertyViewModel
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public UnitViewModel Unit { get; set; }

    }
}