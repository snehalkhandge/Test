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
}