using Myware.Data.Entity.Models.PreSales;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Web.Models.PreSalesUnit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSales
{
    public class PersonalInformationViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public List<PartialPersonalContactNumber> ContactNumbers { get; set; }
        
        public string Address { get; set; }
        public string PinCode { get; set; }        
        public DateTime? DateOfBirth { get; set; }        
        public DateTime? AnniversaryDate { get; set; }        
        public string Remarks { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }        
        public string Campaign { get; set; }        
        public string SubCampaign { get; set; }        
        public string ContactType { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }

        public List<BusinessInformationViewModel> BusinessInformation { get; set; }
    }

    public class CheckUniquePersonalMessageViewModel
    {
        public int Id { get; set; }

        public bool IsUnique { get; set; }

        public PersonalInformationViewModel PersonalInformation { get; set; }
    }

    public class PartialPersonalContactNumber
    {
        public int Id { get; set; }

        public long PhoneNumber { get; set; }
        public string Type { get; set; }
    }

    public class EmailVM
    {
        public string email { get; set; }
    }

}