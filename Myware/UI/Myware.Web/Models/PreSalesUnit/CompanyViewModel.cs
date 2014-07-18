using Myware.Data.Entity.Models.PresalesUnit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSalesUnit
{
    
    public class CreateCompanyViewModel
    {
        public CreateCompanyViewModel()
        {
            Locality = new PartialLocality();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        
        public string Name { get; set; }

        [StringLength(200)]
        
        public string Address { get; set; }

        [StringLength(200)]
        
        public string Pin { get; set; }


        public List<PartialCompanyContactNumber> ContactNumbers { get; set; }

        [StringLength(200)]
        
        public string FaxNumber { get; set; }

        [StringLength(200)]
        
        public string ReceiptFormat { get; set; }
        
        public int LocalityId { get; set; }

        public PartialLocality Locality { get; set; }

        public List<DeveloperCompanies> DeveloperCompanies { get; set; }
        public int UserId { get; set; }
    }


    public class PartialCompanyContactNumber
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
    }

    public class PartialLocality
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public int LocationId { get; set; }


    }



    
    public class ListCompanyViewModel : BaseViewModel
    {
        public List<CreateCompanyViewModel> Results { get; set; }
    }
}