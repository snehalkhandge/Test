using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.PreSales;


namespace Myware.Data.Entity.Models.PresalesUnit
{
    [DataContract(IsReference = true)]
    public class Locality : BaseEntity
    {
        

        [StringLength(200)]
        [DataMember]  
        public string Name { get; set; }

        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        [DataMember]
        public virtual Location Location { get; set; }
        [DataMember]
        public virtual ICollection<Broker> Brokers { get; set; }
        [DataMember]
        public virtual ICollection<BusinessInformation> BusinessInformations { get; set; }
        [DataMember]
        public virtual ICollection<Company> Companies { get; set; }
        [DataMember]
        public virtual ICollection<PersonalInformation> PersonalInformations { get; set; }
        
    }
}
