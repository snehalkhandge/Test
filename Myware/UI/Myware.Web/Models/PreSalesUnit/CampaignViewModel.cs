using Myware.Data.Entity.Models.PresalesUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PreSalesUnit
{
	public class CampaignViewModel
	{
		public CampaignViewModel()
		{
			isCollapsed = true;
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public int? ParentCampaignId { get; set; }

		public bool isCollapsed { get; set; }
		public List<CampaignViewModel> Children { get; set; }
		

	}

	public class ListCampaignViewModel : BaseViewModel
	{
		public List<CampaignViewModel> Results { get; set; }
	}


	public class CreateCampaignViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int ParentCampaignId { get; set; }

		public int UserId { get; set; }
	}

}