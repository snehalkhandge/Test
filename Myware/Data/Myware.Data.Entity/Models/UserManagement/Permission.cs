using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Myware.Data.Entity.Models.UserManagement
{
    [DataContract(IsReference = true)]
    public class Permission
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Tag { get; set; }

        public virtual ICollection<RolePermissions> RolePermissions { get; set; }

    }
}