using Myware.Data.Entity.Models.PresalesUnit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSalesUnit
{
    public class CreateCompanyViewModel
    {            
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        
        public string Name { get; set; }

        [StringLength(200)]
        
        public string Address { get; set; }

        [StringLength(200)]
        
        public string Pin { get; set; }

        
        public List<CompanyContactNumber> ContactNumbers { get; set; }

        [StringLength(200)]
        
        public string FaxNumber { get; set; }

        [StringLength(200)]
        
        public string ReceiptFormat { get; set; }
        
        public int LocalityId { get; set; }

        public Locality Locality { get; set; }

        public Location Location { get; set; }

        public int UserId { get; set; }
    }


    public class ListCompanyViewModel : BaseViewModel
    {
        public List<CreateCompanyViewModel> Results { get; set; }
    }
}