using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Myware.Data.Entity.Models.UserManagement
{
    public class User : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>,
    IUser<int>
    {
        
        [DataMember]
        [StringLength(200)] 
        public string FirstName { get; set; }

        [DataMember]
        [StringLength(200)] 
        public string LastName { get; set; }

        

        [DataMember]
        public bool IsActive { get; set; }

    }

    public class AppUserLogin : IdentityUserLogin<int> { }

    public class AppUserRole : IdentityUserRole<int> { }

    public class AppUserClaim : IdentityUserClaim<int> { }

}
