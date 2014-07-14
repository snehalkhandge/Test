using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Data.Entity.Models.UserTasks
{
    [DataContract(IsReference = true)]
    public class AssignedTask : BaseEntity
    {
        [DataMember]
        public int AssignedFromId { get; set; }

        [ForeignKey("AssignedFromId")]
        [DataMember]
        public User AssignedByUser { get; set; }

        [DataMember]
        public int AssignedToId { get; set; }

        [ForeignKey("AssignedToId")]
        [DataMember]
        public User AssignedToUser { get; set; }


        [Required]
        [StringLength(200)]
        [DataMember]  
        public string Title { get; set; }

        
        [StringLength(500)]
        [DataMember]  
        public string Description { get; set; }

        [Required]
        [StringLength(30)]
        [DataMember]  
        public string TaskStatus { get; set; }

        [DataMember]
        public DateTime Created { get; set; }
        

        [DataMember]
        public virtual ICollection<TasksRelatedFile> TasksRelatedFiles { get; set; }
        
        [Column(name: "IsParentTask", TypeName = "bit")]
        public bool? IsParentTask { get; set; }

        public int? ParentTaskId { get; set; }

        [ForeignKey(name: "ParentTaskId")]
        public virtual AssignedTask ParentTask { get; set; }
    }
}
