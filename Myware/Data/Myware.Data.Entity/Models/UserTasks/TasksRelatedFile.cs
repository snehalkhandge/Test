using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Myware.Data.Entity.Models.UserTasks
{
    [DataContract(IsReference = true)]
    public class TasksRelatedFile : Myware.Repository.EF.Entity
    {

        [Key]
        [DataMember]
        public int Id { get; set; }


        [DataMember]
        public string FileUrl { get; set; }


        [DataMember]
        public int AssignedTaskId { get; set; }

        [DataMember]
        [ForeignKey("AssignedTaskId")]
        public AssignedTask Task { get; set; }
        
    }
}
