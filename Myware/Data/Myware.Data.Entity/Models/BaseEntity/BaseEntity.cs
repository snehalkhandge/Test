using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Data.Entity.Models
{
    public class BaseEntity : Myware.Repository.EF.Entity
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        public int UpdatedByUserId { get; set; }

        [ForeignKey("UpdatedByUserId")]
        [DataMember]
        public User User { get; set; }


        
        
        [DataMember]
        [Timestamp]
        public Byte[] TimeStamp { get; set; }

        
        
        [DataMember]
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

    }
}