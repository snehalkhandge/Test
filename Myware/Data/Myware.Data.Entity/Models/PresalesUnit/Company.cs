using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Myware.Data.Entity.Models.PresalesUnit
{
    [DataContract(IsReference = true)]
    public class Company : BaseEntity
    {
        

        [Required]
        [StringLength(200)]
        [DataMember]  
        public string Name { get; set; }

        [StringLength(200)]
        [DataMember]  
        public string Address { get; set; }
        
        [StringLength(200)]
        [DataMember]  
        public string Pin { get; set; }

        [DataMember]
        public ICollection<ContactNumber> ContactNumbers { get; set; }

        [StringLength(200)]
        [DataMember]  
        public string FaxNumber { get; set; }

        [StringLength(200)]
        [DataMember]  
        public string ReceiptFormat { get; set; }
        [DataMember]
        public int LocalityId { get; set; }

        [ForeignKey("LocalityId")]
        [DataMember]
        public Locality Locality { get; set; }

        

        
    }
}
