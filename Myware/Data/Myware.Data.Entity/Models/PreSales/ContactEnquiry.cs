using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;


namespace Myware.Data.Entity.Models.PreSales
{
    [DataContract(IsReference = true)]
    public class ContactEnquiry : BaseEntity
    {

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public DateTime AssignedDate { get; set; }

        [DataMember]
        public string LeadStatus { get; set; }

        [DataMember]  
        public string TransactionType { get; set; }

        [DataMember]  
        public string LookingForType { get; set; }


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
        public string FacingType { get; set; }

        
        [DataMember]
        public string ContactStatus { get; set; }

        [DataMember]  
        public virtual ICollection<ContactEnquiryUnitType> PreferredUnitTypes { get; set; }


        [DataMember]
        public virtual ICollection<ContactEnquiryLocality> PreferredLocations { get; set; }


        [DataMember]
        public virtual ICollection<RelatedUser> RelatedUsers { get; set; }
        
        [DataMember]
        public int PersonalInformationId { get; set; }


        [ForeignKey("PersonalInformationId")]
        [DataMember]
        public virtual PersonalInformation PersonalInformation { get; set; }

        
        
    }
}
