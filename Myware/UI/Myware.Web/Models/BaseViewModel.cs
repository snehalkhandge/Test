using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models
{
    public abstract class BaseViewModel
    {
        public int TotalPages { get; set; }
        public int CountPerPage { get; set; }
        public int CurrentPage { get; set; }

        public List<string> Messages { get; set; } 

    }
}