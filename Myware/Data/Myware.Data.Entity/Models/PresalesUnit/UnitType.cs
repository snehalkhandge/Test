using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Myware.Data.Entity.Models.PresalesUnit
{
    [DataContract(IsReference = true)]
    public  class UnitType : BaseEntity
    {
        

        [Required]
        [StringLength(30)]
        [DataMember]  
        public string Name { get; set; }

        
    }
}
