using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.PresalesUnit;
using System;


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
        public ICollection<BusinessContactNumber> BusinessContactNumbers { get; set; }
                
        [DataMember]  
        public Nullable<decimal> InvestmentCapacity { get; set; }

        [StringLength(200)]
        [DataMember]  
        public string Fax { get; set; }

        [StringLength(200)]
        [DataMember]  
        public string Website { get; set; }


        [DataMember]
        public string Locality { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Type { get; set; }


        [DataMember]
        public int PersonalInformationId { get; set; }

        [ForeignKey("PersonalInformationId")]
        [DataMember]
        public PersonalInformation PersonalInformation { get; set; }
    }
}
