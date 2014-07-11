using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Web
{
    public class AppUserClaimsIdentityFactory : ClaimsIdentityFactory<User, int>
    {
        public override async Task<ClaimsIdentity> CreateAsync(
            UserManager<User, int> manager,
            User user,
            string authenticationType)
        {
            var identity = await base.CreateAsync(manager, user, authenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));

            return identity;
        }
    }
}