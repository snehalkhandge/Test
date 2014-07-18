using Myware.Data.Entity.Models.PresalesUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSalesUnit
{
    public class CreateDeveloperViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
        
        public List<int> SelectedCompanies { get; set; }
        public List<DeveloperCompanies> DeveloperCompanies { get; set; }


    }

    

    public class ListDeveloperViewModel : BaseViewModel
    {
        public List<CreateDeveloperViewModel> Results { get; set; }
    }
}