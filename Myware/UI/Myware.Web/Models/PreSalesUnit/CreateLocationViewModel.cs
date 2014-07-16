using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Myware.Web.Models.PreSalesUnit
{
    public class CreateLocationViewModel
    {
        [DataMember]        
        public int Id { get; set; }

        [DataMember]
        [StringLength(80)]
        public string City { get; set; }

        [DataMember]
        [StringLength(80)]
        public string State { get; set; }

        [DataMember]
        [StringLength(80)]
        public string Country { get; set; }

        [DataMember]
        public int UserId { get; set; }

    }

    public class ListLocationTypeViewModel : BaseViewModel
    {

        public List<CreateLocationViewModel> Results { get; set; }
    }
}