using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myware.Web.Models
{

    public class LogInModel
    {
        [Required]
        public string UserName { get; set; }

        

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }


    public class PartialUserViewModel
    {
        public int  Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }



    }

    public class CheckUniqueUserViewModel
    {
        public string Name { get; set; }

        public bool IsUnique { get; set; }

        public UserManagerUserViewModel User { get; set; }
    }

    public class UserManagerUserViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }


    public class ListUserViewModel
    {
        public ListUserViewModel()
        {
            Users = new List<UserManagerUserViewModel>();
        }
        public int Total { get; set; }

        public List<UserManagerUserViewModel> Users { get; set; }
    }

}