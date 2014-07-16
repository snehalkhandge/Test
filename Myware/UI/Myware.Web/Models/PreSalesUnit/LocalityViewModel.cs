using Myware.Data.Entity.Models.PresalesUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSalesUnit
{
	public class LocalityViewModel
	{
		
		public int Id { get; set; }
		public string Name { get; set; }

		public int LocationId { get; set; }
		public Location Location { get; set; }

	}

	public class ListLocalityTypeViewModel : BaseViewModel
	{
		public List<LocalityViewModel> Results { get; set; }
	}


	public class CreateLocalityViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int LocationId { get; set; }

		public int UserId { get; set; }
	}

}