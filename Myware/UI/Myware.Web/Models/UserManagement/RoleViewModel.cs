using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Web.Models
{
    [DataContract(IsReference = true)]
    public class RoleViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<int> SelectedPermissions { get; set; }

        [DataMember]
        public List<RolePermissionViewModel> RolePermissions { get; set; }

    }

    [DataContract(IsReference = true)]
    public class RolePermissionViewModel
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public int PermissionId { get; set; }
        [DataMember]
        public Permission Permission { get; set; }
    }


    public class ListRoleViewModel : BaseViewModel
    {
        public List<RoleViewModel> Results { get; set; }
    }

    public class CreateRoleViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<int> Permissions { get; set; }
    }


}