using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Myware.Data.Entity.Models.PresalesUnit
{
    [DataContract(IsReference = true)]
    public class Developer : BaseEntity
    {
        [StringLength(20)]
        [DataMember]  
        public string Name { get; set; }


        [StringLength(200)]
        [DataMember]  
        public string Description { get; set; }

        [DataMember]
        public  ICollection<Company> Companies { get; set; }

    }
}
