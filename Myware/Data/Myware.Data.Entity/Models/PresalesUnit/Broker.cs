using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Myware.Data.Entity.Models.PresalesUnit
{
    [DataContract(IsReference = true)]
    public class Broker : BaseEntity
    {
        

        [Required]
        [StringLength(200)]
        [DataMember]  
        public string Name { get; set; }

        [StringLength(100)]
        [DataMember]  
        public string CompanyName { get; set; }

        [StringLength(300)]
        [DataMember]  
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        [DataMember]  
        public string Email { get; set; }

        [StringLength(30)]
        [DataMember]  
        public string PanCard { get; set; }

        [StringLength(100)]
        [DataMember]  
        public string ReferenceName { get; set; }

        [DataMember]
        public ICollection<BrokerContactNumber> ContactNumbers { get; set; }


        [DataMember]
        public int LocalityId { get; set; }

        [ForeignKey("LocalityId")]
        [DataMember]
        public Locality Locality { get; set; }

        [DataType(DataType.ImageUrl)]
        [DataMember]
        public string ImageUrl { get; set; }
        
    }
}
