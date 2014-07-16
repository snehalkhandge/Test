using Myware.Data.Entity.Models.PresalesUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSalesUnit
{
    public class ListCutomerTypeViewModel : BaseViewModel
    {

        public List<CreateTypeViewModel> Results { get; set; }
    }
}