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
    public interface IAppUserStore : IUserStore<User, int>
    {

    }

    public class AppUserStore :
        UserStore<User, Role, int, AppUserLogin, AppUserRole, AppUserClaim>,
        IAppUserStore
    {
        public AppUserStore()
            : base(new ApplicationDbContext())
        {

        }

        public AppUserStore(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}
