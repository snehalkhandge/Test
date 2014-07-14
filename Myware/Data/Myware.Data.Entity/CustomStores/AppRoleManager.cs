using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Data.Entity.CustomStores
{
    public interface IAppRoleManager
    {
        
    }
    public class AppRoleManager : RoleManager<Role, int>, IAppRoleManager
    {
        public AppRoleManager(IAppRoleStore store)
            : base(store)
        {

        }
    }

}
