using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Data.Entity.Models.UserManagement
{
    [DataContract(IsReference = true)]
    public class RolePermissions
    {

        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        public int RoleId { get; set; }
        [DataMember]
        public int PermissionId { get; set; }


        public virtual  ICollection<Role> Roles { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; } 
    }
}