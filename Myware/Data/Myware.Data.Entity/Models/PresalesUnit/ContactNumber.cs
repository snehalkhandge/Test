using System.Runtime.Serialization;

namespace Myware.Data.Entity.Models.PresalesUnit
{
    [DataContract(IsReference = true)]
    public class ContactNumber : BaseEntity
    {
        

        [DataMember]
        public string PhoneNumber { get; set; }


        [DataMember]
        public string Type { get; set; }


        
    }
}
