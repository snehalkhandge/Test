using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.PostSales;
using Myware.Data.Entity.Models.PresalesUnit;

namespace Myware.Data.Entity.Models.PreSales
{
    [DataContract(IsReference = true)]
    public class PersonalInformation : BaseEntity
    {
        

        [StringLength(50)]
        [DataMember]  
        public string FirstName { get; set; }

        [StringLength(50)]
        [DataMember]  
        public string LastName { get; set; }

        [StringLength(200)]
        [DataMember]  
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataMember]
        public ICollection<ContactNumber> ContactNumbers { get; set; }
        [StringLength(200)]
        [DataMember]  
        public string Address { get; set; }

        [StringLength(30)]
        [DataMember]  
        public string PinCode { get; set; }

        
        [DataMember]  
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [DataMember]  
        public Nullable<System.DateTime> AnniversaryDate { get; set; }

        
        [DataMember]  
        public string Remarks { get; set; }

        
        
        [DataMember]
        public string Locality { get; set; }


        [DataMember]
        public string City { get; set; }

        [DataMember]
        [StringLength(50)]
        public string Campaign { get; set; }

        [DataMember]
        [StringLength(50)]
        public string SubCampaign { get; set; }

        [DataMember]
        [StringLength(50)]
        public string ContactType { get; set; }

        [DataMember]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DataMember]
        public virtual ICollection<PersonalInformationBookingMeta> PersonalInformationBookingMeta { get; set; }

        [DataMember]
        public virtual ICollection<BusinessInformation> BusinessInformation { get; set; }
    }
}
