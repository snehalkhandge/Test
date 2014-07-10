using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.UserManagement
{
    [DataContract(IsReference = true)]
    public class Role
    {

        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        [StringLength(200)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        public virtual ICollection<RolePermissions> RolePermissions { get; set; }
    }
}
