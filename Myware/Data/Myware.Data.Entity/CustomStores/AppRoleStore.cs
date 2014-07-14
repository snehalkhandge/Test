using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Data.Entity.CustomStores
{
    public interface IAppRoleStore : IRoleStore<Role, int>
    {

    }

    public class AppRoleStore :
        RoleStore<Role, int, AppUserRole>,
        IAppRoleStore
    {
        public AppRoleStore()
            : base(new ApplicationDbContext())
        {

        }

        public AppRoleStore(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}
