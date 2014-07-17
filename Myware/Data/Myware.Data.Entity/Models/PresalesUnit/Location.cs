using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Myware.Data.Entity.Models.PresalesUnit
{
    [DataContract(IsReference = true)]
    public  class Location : BaseEntity
    {
        
        [StringLength(200)]
        [DataMember]  
        public string City { get; set; }
        [StringLength(50)]
        [DataMember]  
        public string State { get; set; }
        [StringLength(50)]
        [DataMember]  
        public string Country { get; set; }

        [DataMember]  
        public  ICollection<Locality> Localities { get; set; }

        
    }
}
