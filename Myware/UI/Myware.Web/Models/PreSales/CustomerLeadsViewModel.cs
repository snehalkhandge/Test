using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSales
{
    public class CustomerLeadsViewModel
    {
        public CustomerLeadsViewModel()
        {
            UnitTypes = new List<PartialCustomerUnitType>();
            Localities = new List<PartialCustomerLocality>();
            ContactNumbers = new List<PartialCustomerContactNumber>();
        }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string ContactType { get; set; }
        public List<PartialCustomerUnitType> UnitTypes { get; set; }
        public List<PartialCustomerLocality> Localities { get; set; }
        public List<PartialCustomerContactNumber> ContactNumbers { get; set; }
        public string BudgetRange { get; set; }


    }

    public class ListCustomerQueries
    {
        public ListCustomerQueries()
        {
            Customers = new List<CustomerLeadsViewModel>();
        }
        public List<CustomerLeadsViewModel> Customers { get; set; }

        public int Total { get; set; }
    }

    public class CustomerQueryViewModel
    {
        public CustomerQueryViewModel()
        {
            Page = 1;
            PageSize = 7;
        }
        public PartialCustomerName CustomerNames { get; set; }

        public PartialCustomerContactNumber ContactNumbers { get; set; }

        public PartialCustomerContactType CustomerTypes { get; set; }

        public PartialCustomerUnitType UnitTypes { get; set; }

        public PartialCustomerLocality Localities { get; set; }

        public PartialCustomerBudget BudgetFromList { get; set; }

        public PartialCustomerBudget BudgetToList { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
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