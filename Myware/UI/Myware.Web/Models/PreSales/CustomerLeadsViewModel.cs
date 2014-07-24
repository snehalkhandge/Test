using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSales
{
    public class CustomerLeadsViewModel
    {
    }


    public class PartialCustomerName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PartialCustomerContactNumber
    {
        public int PersonalInformationId { get; set; }
        public long Number { get; set; }
    }

    public class PartialCustomerContactType
    {
        public int PersonalInformationId { get; set; }
        public string Name { get; set; }
    }

    public class PartialCustomerUnitType
    {
        public int PersonalInformationId { get; set; }
        public string Name { get; set; }
    }

    public class PartialCustomerLocality
    {
        public int PersonalInformationId { get; set; }
        public string Name { get; set; }
    }

    public class PartialCustomerBudget
    {
        public int PersonalInformationId { get; set; }
        public decimal? Budget { get; set; }
    }

    public class PartialSearchQuery
    {
        public string Query { get; set; }
    }

    
}