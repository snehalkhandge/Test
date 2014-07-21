using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PreSales
{
   public  class ContactEnquiryUnitType
    {
       [Key]
       [DataMember]
       public int Id { get; set; }

       [Required]
       [StringLength(30)]
       [DataMember]
       public string Name { get; set; }
    }
}
