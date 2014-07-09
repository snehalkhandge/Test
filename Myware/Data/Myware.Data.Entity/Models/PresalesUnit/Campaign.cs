using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.UserManagement;


namespace Myware.Data.Entity.Models.PresalesUnit
{
    [DataContract(IsReference = true)]
    public class Campaign : BaseEntity
    {
        [Required]
        [StringLength(200)]
        [DataMember]  
        public string Name { get; set; }

        
        [DataMember]
        public int CreatedByUserId { get; set; }
        

        [ForeignKey("CreatedByUserId")]
        [DataMember]
        public User CreatedBy { get; set; }

        [DataMember]
        public bool? IsParentCampaign { get; set; }
        [DataMember]
        public int? ParentCampaignId { get; set; }

        [ForeignKey(name: "ParentCampaignId")]
        public virtual Campaign ParentCampaign { get; set; }
    }
}