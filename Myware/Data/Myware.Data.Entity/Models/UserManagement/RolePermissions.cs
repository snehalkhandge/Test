﻿using System;
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
    public class RolePermissions : Myware.Repository.EF.Entity
    {

        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        [ForeignKey("RoleId")]
        public  Role Role { get; set; }
        

        [DataMember]
        public int PermissionId { get; set; }


        [DataMember]
        [ForeignKey("PermissionId")]
        public  Permission Permission { get; set; }
    

    }
}