using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Myware.Web.Models.PreSalesUnit
{
    public class CreateTypeViewModel
    {
        [DataMember]        
        public int Id { get; set; }

        [DataMember]
        [StringLength(80)]
        public string Name { get; set; }

        [DataMember]
        public int UserId { get; set; }

    }
}