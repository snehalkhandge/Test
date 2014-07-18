using Myware.Data.Entity.Models.PresalesUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSalesUnit
{
    public class ListBrokerViewModel : BaseViewModel
    {
        public List<CreateBrokerViewModel> Results { get; set; }
    }


    public class CreateBrokerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CompanyName { get; set; }

        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public int LocalityId { get; set; }
        public PartialLocality Locality { get; set; }
        public string PanCard { get; set; }
        public string ReferenceName { get; set; }

        public int UserId { get; set; }

        public DateTime LastUpdated { get; set; }
        public List<PartialBrokerContactNumber> ContactNumbers { get; set; }

    }

    public class PartialBrokerContactNumber
    {
        public int Id { get; set; }
        public int BrokerId { get; set; }        
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
    }


}