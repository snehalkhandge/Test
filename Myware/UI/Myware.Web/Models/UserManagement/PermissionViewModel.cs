using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Web.Models
{
    public class PermissionViewModel : BaseViewModel
    {
        public List<Permission> Results { get; set; }

    }

    public class CreatePermissionViewModel : BaseViewModel
    {
        [DataMember]
        [Required]
        public int Id { get; set; }

        [DataMember]
        [StringLength(80)]
        public string Name { get; set; }
        
    }


    public class ListPartialPermissionViewModel : BaseViewModel
    {
        public List<PartialPermissionViewModel> Results { get; set; }
    }


    public class PartialPermissionViewModel 
    {
        [DataMember]        
        public int Id { get; set; }

        [DataMember]        
        public string Name { get; set; }
    }



}