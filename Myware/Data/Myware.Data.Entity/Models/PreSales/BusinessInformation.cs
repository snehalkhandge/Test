using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.PresalesUnit;


namespace Myware.Data.Entity.Models.PreSales
{
    [DataContract(IsReference = true)]
    public  class BusinessInformation : BaseEntity
    {
        
        [StringLength(200)]
        [DataMember]  
        public string CompanyName { get; set; }
        [StringLength(200)]
        [DataMember]  
        public string Designation { get; set; }
        [StringLength(200)]
        [DataMember]  
        public string BusinessOrIndustry { get; set; }
        [StringLength(200)]
        [DataMember]
        public ICollection<ContactNumber> BusinessContactNumbers { get; set; }

        
        [DataMember]  
        public decimal InvestmentCapacity { get; set; }

        [StringLength(200)]
        [DataMember]  
        public string Fax { get; set; }

        [StringLength(200)]
        [DataMember]  
        public string Website { get; set; }


        [DataMember]
        public int BusinessLocalityId { get; set; }

        [ForeignKey("BusinessLocalityId")]
        [DataMember]
        public Locality BusinessLocality { get; set; }
        

        [DataMember]
        public int PersonalInformationId { get; set; }

        [ForeignKey("PersonalInformationId")]
        [DataMember]
        public PersonalInformation PersonalInformation { get; set; }
    }
}
