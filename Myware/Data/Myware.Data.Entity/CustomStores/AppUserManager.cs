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
    public class AppUserManager : UserManager<User, int>
    {
        public AppUserManager(IAppUserStore store)
            : base(store)
        {

        }
    }

    public class AppClaimsPrincipal : ClaimsPrincipal
    {
        public AppClaimsPrincipal(ClaimsPrincipal principal)
            : base(principal)
        { }

        public int UserId
        {
            get { return int.Parse(this.FindFirst(ClaimTypes.Sid).Value); }
        }
    }
}
