using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.PresalesUnit;


namespace Myware.Data.Entity.Models.PreSales
{
    [DataContract(IsReference = true)]
    public class ContactEnquiry : BaseEntity
    {
        
        [DataMember]
        public int TransactionTypeId { get; set; }

        [ForeignKey("TransactionTypeId")]
        [DataMember]
        public TransactionType TransactionType { get; set; }


        [DataMember]
        public int LookingForTypeId { get; set; }

        [ForeignKey("LookingForTypeId")]
        [DataMember]
        public LookingForType LookingForType { get; set; }


        [DataMember]  
        public Nullable<decimal> BudgetFrom { get; set; }

        [DataMember]  
        public Nullable<decimal> BudgetTo { get; set; }

        [DataMember]  
        public Nullable<decimal> SaleAreaFrom { get; set; }

        [DataMember]  
        public Nullable<decimal> SaleAreaTo { get; set; }

        [DataMember]  
        public Nullable<decimal> CarpetAreaFrom { get; set; }

        [DataMember]  
        public Nullable<decimal> CarpetAreaTo { get; set; }

        [DataMember]  
        public Nullable<decimal> PropertyAge { get; set; }

        [DataMember]  
        public Nullable<bool> IsFurnished { get; set; }

        [DataMember]  
        public Nullable<decimal> OfferedRate { get; set; }

        [DataMember]  
        public System.DateTime Created { get; set; }

        [DataMember]  
        public System.DateTime Updated { get; set; }

        [DataMember]  
        public Nullable<System.DateTime> EnquiryDate { get; set; }

        [DataMember]
        public int FacingTypeId { get; set; }

        [ForeignKey("FacingTypeId")]
        [DataMember]
        public FacingType FacingType { get; set; }

        [DataMember]
        public int ContactStatusId { get; set; }

        [ForeignKey("ContactStatusId")]
        [DataMember]
        public ContactStatus ContactStatus { get; set; }

        [DataMember]  
        public virtual ICollection<UnitType> UnitTypes { get; set; }

        [DataMember]
        public int PersonalInformationId { get; set; }


        [ForeignKey("PersonalInformationId")]
        [DataMember]
        public virtual PersonalInformation PersonalInformation { get; set; }

        
        
    }
}
