using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PreSales
{
    public class ContactEnquiryLocality
    {
        [Key]
        public int Id { get; set; }

        [DataMember]
        [StringLength(150)]
        public string Locality { get; set; }

    }
}
