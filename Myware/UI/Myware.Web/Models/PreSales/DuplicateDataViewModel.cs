using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSales
{

    public class ListDuplicateData
    {
        public ListDuplicateData()
        {
            Page = 1;
            PageSize = 10;
        }
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public List<DuplicateDataViewModel> DuplicateData { get; set; }
    
    }
    public class DuplicateDataViewModel
    {

        public PersonalInformationViewModel ParentPersonalInformation { get; set; }

        public List<PersonalInformationViewModel> ChildrenPersonalInformation { get; set; }

        public BusinessInformationViewModel ParentBusinessInformation { get; set; }

        public List<BusinessInformationViewModel> ChildrenBusinessInformation { get; set; }

        public ContactEnquiryViewModel ParentContactEnquiry { get; set; }

        public List<ContactEnquiryViewModel> ChildrenContactEnquiry { get; set; }

    }

    public class GetPartialId
    {
        public int Id { get; set; }
    }

    public class ExportDuplicateDataViewModel
    {   
        public string ContactType { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public Nullable<long> ContactNumbers { get; set; }
        
        public string Email { get; set; }
        
        public string Address { get; set; }
        
        public string City { get; set; }
        
        public string Locality { get; set; }
        
        public string PinCode { get; set; }
        
        public Nullable<DateTime> DateOfBirth { get; set; }
        
        public Nullable<DateTime> AnniversaryDate { get; set; }
        
        public string Campaign { get; set; }
        
        public string SubCampaign { get; set; }
        
        public string CompanyName { get; set; }
        
        public string Designation { get; set; }
        
        public string BusinessOrIndustry { get; set; }

        
        public string BusinessCity { get; set; }

        
        public string BusinessLocality { get; set; }
        
        public Nullable<Decimal> InvestmentCapacity { get; set; }
        
        public Nullable<long> BusinessContactNumbers { get; set; }
        
        public string Fax { get; set; }
        
        public string Website { get; set; }

        public Nullable<DateTime> EnquiryDate { get; set; }

        
        public string LookingForType { get; set; }

        
        public string PreferredUnitTypes { get; set; }

        
        public string TransactionType { get; set; }

        
        public string PropertyAge { get; set; }

        
        public Nullable<decimal> BudgetFrom { get; set; }

        
        public Nullable<decimal> BudgetTo { get; set; }

        
        public Nullable<decimal> SaleAreaFrom { get; set; }

        public Nullable<decimal> SaleAreaTo { get; set; }
        
        public Nullable<decimal> CarpetAreaFrom { get; set; }
        
        public Nullable<decimal> CarpetAreaTo { get; set; }
        
        public Nullable<decimal> OfferedRate { get; set; }
        
        public bool? IsFurnished { get; set; }     
        
        public string LeadStatus { get; set; }
        
        public string Remarks { get; set; }

        public int ContactEnquiryId { get; set; }
        public int BusinessId { get; set; }
        public int PersonalInformationId { get; set; }

    }

}