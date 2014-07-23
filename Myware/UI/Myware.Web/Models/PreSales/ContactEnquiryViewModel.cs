using Myware.Data.Entity.Models.PreSales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSales
{
    public class ContactEnquiryViewModel
    {
        public int Id { get; set; }
        public string Remarks { get; set; }

        public DateTime AssignedDate { get; set; }
                
        public string LeadStatus { get; set; }

        public string TransactionType { get; set; }

        public string LookingForType { get; set; }
        public decimal? BudgetFrom { get; set; }
        public decimal? BudgetTo { get; set; }
        public decimal? SaleAreaFrom { get; set; }
        public decimal? SaleAreaTo { get; set; }
        public decimal? CarpetAreaFrom { get; set; }
        public decimal? CarpetAreaTo { get; set; }
        public string PropertyAge { get; set; }
        public bool? IsFurnished { get; set; }
        public decimal? OfferedRate { get; set; }                
        public System.DateTime Created { get; set; }                
        public System.DateTime Updated { get; set; }
        public DateTime? EnquiryDate { get; set; }
        public string FacingType { get; set; }
        public string ContactStatus { get; set; }
        public virtual List<PartialContactEnquiryUnit> PreferredUnitTypes { get; set; }

        public virtual List<PartialContactEnquiryLocality> PreferredLocations { get; set; }
        public int PersonalInformationId { get; set; }

        public int UserId { get; set; }
        
    }


    public class PartialContactEnquiryUnit
    {
        public string UnitType { get; set; }
    }

    public class PartialContactEnquiryLocality
    {
        public string Locality { get; set; }
    }
}