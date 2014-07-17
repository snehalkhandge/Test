using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.PreSales;


namespace Myware.Data.Entity.Models.PresalesUnit
{
    [DataContract(IsReference = true)]
    public  class FacingType : BaseEntity
    {
        
        [StringLength(20)]
        [DataMember]  
        public string Name { get; set; }

        [DataMember]
        public  ICollection<ContactEnquiry> ContactEnquiries { get; set; }

        
    }
}
