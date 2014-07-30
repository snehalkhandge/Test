using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PreSales
{
    [DataContract(IsReference = true)]
    public class DuplicateData
    {
        [DataMember]  
        public int Id { get; set; }
        [DataMember]  
        public string ContactType { get; set; }
        [DataMember]  
        public string FirstName { get; set; }
        [DataMember]  
        public string LastName { get; set; }
        [DataMember]  
        public Nullable<long> ContactNumbers { get; set; }
        [DataMember]  
        public string Email { get; set; }
        [DataMember]  
        public string Address { get; set; }
        [DataMember]  
        public string City { get; set; }
        [DataMember]  
        public string Locality { get; set; }
        [DataMember]  
        public string PinCode { get; set; }
        [DataMember]  
        public Nullable<DateTime> DateOfBirth { get; set; }
        [DataMember]  
        public Nullable<DateTime> AnniversaryDate { get; set; }
        [DataMember]  
        public string Campaign { get; set; }
        [DataMember]  
        public string SubCampaign { get; set; }
        [DataMember]  
        public string CompanyName { get; set; }
        [DataMember]  
        public string Designation { get; set; }
        [DataMember]  
        public string BusinessOrIndustry { get; set; }
        [DataMember]  

        public string BusinessCity { get; set; }
        
        [DataMember]  
        public string BusinessLocality { get; set; }
        [DataMember]  
        public Nullable<Decimal> InvestmentCapacity { get; set; }
        [DataMember]
        public Nullable<long> BusinessContactNumbers { get; set; }
        [DataMember]  
        public string Fax { get; set; }
        [DataMember]  
        public string Website { get; set; }

        [DataMember]  
        public Nullable<DateTime> EnquiryDate { get; set; }

        [DataMember]  
        public string LookingForType { get; set; }

        [DataMember]  
        public string PreferredUnitTypes { get; set; }

        [DataMember]  
        public string TransactionType { get; set; }

        [DataMember]  
        public string PropertyAge { get; set; }

        [DataMember]  
        public Nullable<decimal> BudgetFrom { get; set; }

        [DataMember]  
        public Nullable<decimal> BudgetTo { get; set; }

        [DataMember]  
        public Nullable<decimal> SaleAreaFrom { get; set; }

        [DataMember]  
        public Nullable<decimal> SaleAreaTo { get; set; }
        [DataMember]  
        public Nullable<decimal> CarpetAreaFrom { get; set; }
        [DataMember]  
        public Nullable<decimal> CarpetAreaTo { get; set; }
        [DataMember]  
        public Nullable<decimal> OfferedRate { get; set; }
        [DataMember]  
        public bool? IsFurnished { get; set; }
        [DataMember]  
        public int PersonalInformationId { get; set; }

        [DataMember]  
        public string LeadStatus { get; set; }
        [DataMember]  
        public string Remarks { get; set; }

    }
}
