using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Web.Models
{
    public class PermissionViewModel : BaseViewModel
    {
        public List<Permission> Permissions { get; set; }

    }

    

}