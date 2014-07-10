using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.UserManagement
{
    public class User
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        [StringLength(200)] 
        public string FirstName { get; set; }

        [DataMember]
        [StringLength(200)] 
        public string LastName { get; set; }

        [DataMember]
        [StringLength(500)] 
        public string Email { get; set; }

        [DataMember]
        [Index(IsUnique = true)]
        [StringLength(200)] 
        public string UserName { get; set; }


        [DataMember]
        [StringLength(500)] 
        public string Password { get; set; }

        [DataMember]
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; set; }



        [DataMember]
        public bool IsActive { get; set; }


        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}
