using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.PresalesUnit;


namespace Myware.Data.Entity.Models.PreSales
{
    [DataContract(IsReference = true)]
    public class CustomerEnquiryTypeCollection : Myware.Repository.EF.Entity
    {
        [Key]
        public int Id { get; set; }

        [DataMember]
        public int PersonalInformationId { get; set; }

        [DataMember]
        [ForeignKey("PersonalInformationId")]
        public PersonalInformation PersonalInformation { get; set; }

        public int CustomerEnquiryTypeId { get; set; }

        [DataMember]
        [ForeignKey("CustomerEnquiryTypeId")]
        public CustomerEnquiryType CustomerEnquiryType { get; set; }


    }
}