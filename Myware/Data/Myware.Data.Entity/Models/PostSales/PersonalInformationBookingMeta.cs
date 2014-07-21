using System;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.PreSales;
using System.ComponentModel.DataAnnotations.Schema;


namespace Myware.Data.Entity.Models.PostSales
{
    [DataContract(IsReference = true)]
    public class PersonalInformationBookingMeta : BaseEntity
    {
        
        [DataMember]
        public Nullable<decimal> Age { get; set; }

        [DataMember]
        public string Son_Wife_Daughter_Of { get; set; }

        [DataMember]
        public string PanNumber { get; set; }

        [DataMember]
        public string Nationality { get; set; }

        [DataMember]
        public int PersonalInformationId { get; set; }

        [DataMember]
        [ForeignKey("PersonalInformationId")]
        public virtual PersonalInformation PersonalInformation { get; set; }

        [DataMember]
        public Nullable<System.DateTime> Created { get; set; }

        
    }
}
