using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSales
{
    public class BusinessInformationViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
     
        public string Designation { get; set; }
     
        public string BusinessOrIndustry { get; set; }

        public List<PartialPersonalContactNumber> BusinessContactNumbers { get; set; }

        public Nullable<decimal> InvestmentCapacity { get; set; }

        public string Fax { get; set; }

        public string Website { get; set; }

        public string Locality { get; set; }
                
        public string City { get; set; }
                
        public string Type { get; set; }
        
        public int PersonalInformationId { get; set; }

        public int UserId { get; set; }

        
    }
}