using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Myware.Data.Entity.CustomStores;

namespace Myware.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public AppClaimsPrincipal CurrentUser
        {
            get { return new AppClaimsPrincipal((ClaimsPrincipal)this.User); }
        }


        public BaseController()
        {

        }
    }
}