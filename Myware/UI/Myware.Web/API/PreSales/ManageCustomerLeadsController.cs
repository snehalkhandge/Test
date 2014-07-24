using Myware.Data.Entity;
using Myware.Data.Entity.Models.PreSales;
using Myware.Web.Models;
using Myware.Web.Models.PreSales;
using Myware.Web.Models.PreSalesUnit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;


namespace Myware.Web.API.PreSales
{
	public class ManageCustomerLeadsController : ApiController
	{
	   private ApplicationDbContext db = new ApplicationDbContext();

		// GET: api/ManagePermissions

	   [Route("customersPreSales")]
	   public BusinessInformationViewModel GetBusinessInformationId(int id)
	   {

		   var query = db.BusinessInformations
						 .Include(t => t.BusinessContactNumbers)						 
						 .OrderByDescending(x => x.Id);

		   var result = query.Where(r => r.Id == id)
						   .SingleOrDefault();
		   
		   var cntNumber = new List<PartialPersonalContactNumber>();

		   foreach (var cnt in result.BusinessContactNumbers)
		   {
			   cntNumber.Add(new PartialPersonalContactNumber
			   {
				   PhoneNumber = cnt.PhoneNumber,
				   Type = cnt.Type

			   });
		   }


		   return new BusinessInformationViewModel
		   {
			   BusinessOrIndustry = result.BusinessOrIndustry,
			   BusinessContactNumbers = cntNumber,
			   CompanyName = result.CompanyName,
			   City = result.City,
			   Designation = result.Designation,
			   Fax = result.Fax,
			   InvestmentCapacity =result.InvestmentCapacity,
			   Locality = result.Locality,
			   PersonalInformationId = result.PersonalInformationId,
			   Type = result.Type,
			   Website = result.Website,
			   Id = result.Id

		   };

	   }


	   [Route("customersContactNumbers/all")]
	   [HttpPost]	   
	   public List<PartialCustomerContactNumber> PostContactNumbers(PartialSearchQuery query)
	   {
           DbSqlQuery<PersonalContactNumber> data = db.PersonalContactNumbers.SqlQuery("SELECT * FROM [ApplicationDb].[dbo].[PersonalContactNumbers] WHERE  PhoneNumber LIKE '%" + query.Query + "%'");

		   var results = new List<PartialCustomerContactNumber>();

		   foreach (var item in data)
		   {
			   results.Add(new PartialCustomerContactNumber
			   {
				   PersonalInformationId = item.PersonalInformationId,
				   Number = item.PhoneNumber
			   });
		   }

		   return results;
	   }

	   [Route("customersBudgetTo/all")]
	   [HttpPost]
	   public List<PartialCustomerBudget> PostBudgetTo(PartialSearchQuery query)
	   {
		   DbSqlQuery<ContactEnquiry> data = db.ContactEnquiries.SqlQuery("SELECT * FROM [ApplicationDb].[dbo].[ContactEnquiries] WHERE  BudgetTo LIKE '%" + query.Query + "%'");

		   var results = new List<PartialCustomerBudget>();

		   foreach (var item in data)
		   {
			   results.Add(new PartialCustomerBudget
			   {
				   PersonalInformationId = item.PersonalInformationId,
				   Budget = item.BudgetTo
			   });
		   }

		   return results;
		   
	   }

	   [Route("customersBudgetFrom/all")]
	   [HttpPost]
	   public List<PartialCustomerBudget> PostBudgetFrom(PartialSearchQuery query)
	   {
		   DbSqlQuery<ContactEnquiry> data = db.ContactEnquiries.SqlQuery("SELECT * FROM [ApplicationDb].[dbo].[ContactEnquiries] WHERE  BudgetFrom LIKE '%"+query.Query+"%'");

		   var results = new List<PartialCustomerBudget>();

		   foreach (var item in data)
		   {
			   results.Add(new PartialCustomerBudget
			   {
				   PersonalInformationId = item.PersonalInformationId,
				   Budget = item.BudgetFrom
			   });
		   }

		   return results;

	   }

	   [Route("customerNames/all")]
	   [HttpPost]
	   public List<PartialCustomerName> PostCustomerNames(PartialSearchQuery query)
	   {
		   return db.PersonalInformations
					.Where(t => t.FirstName.Contains(query.Query) || t.LastName.Contains(query.Query))					
					.Select(t => new PartialCustomerName
					{
						Id = t.Id,
						Name = t.FirstName + " " + t.LastName
					}).ToList();

	   }
	   

		

	   [Route("saveBusinessInformation/{id}")]
	   [ResponseType(typeof(BusinessInformationViewModel))]
	   public IHttpActionResult PostBusiness(int id, BusinessInformationViewModel typeVM)
	   {


		   if (!ModelState.IsValid)
		   {
			   return BadRequest(ModelState);
		   }


		   if (id != typeVM.Id)
		   {
			   return BadRequest();
		   }

		   var type = new BusinessInformation();

		   try
		   {
			   if (typeVM.Id == 0)
			   {   
				   type.BusinessOrIndustry = typeVM.BusinessOrIndustry;
				   type.City = typeVM.City;
				   type.CompanyName = typeVM.CompanyName;
				   type.Designation = typeVM.Designation;
				   type.Fax = typeVM.Fax;
				   type.InvestmentCapacity = typeVM.InvestmentCapacity;				   
				   type.Locality = typeVM.Locality;
				   type.City = typeVM.City;
				   type.Type = typeVM.Type;
				   type.Website = typeVM.Website;
				   type.PersonalInformationId = typeVM.PersonalInformationId;

				   type.UpdatedByUserId = typeVM.UserId;                   
				   type.LastUpdated = DateTime.UtcNow;

				   db.Entry(type).State = EntityState.Added;
				   db.SaveChanges();

				   id = type.Id;

			   }
			   else
			   {
				   var oldType = db.BusinessInformations
								   .Include(t => t.BusinessContactNumbers)								   
								   .Where(e => e.Id == id).SingleOrDefault();

				   oldType.BusinessOrIndustry = typeVM.BusinessOrIndustry;
				   oldType.City = typeVM.City;
				   oldType.CompanyName = typeVM.CompanyName;
				   oldType.Designation = typeVM.Designation;
				   oldType.Fax = typeVM.Fax;
				   oldType.InvestmentCapacity = typeVM.InvestmentCapacity;
				   oldType.Locality = typeVM.Locality;
				   oldType.City = typeVM.City;
				   oldType.Type = typeVM.Type;
				   oldType.Website = typeVM.Website;
				   oldType.PersonalInformationId = typeVM.PersonalInformationId;

				   oldType.UpdatedByUserId = typeVM.UserId;
				   oldType.LastUpdated = DateTime.UtcNow;

				   db.Entry(oldType).State = EntityState.Modified;
				   db.SaveChanges();

				   var itemsToDelete = db.BusinessContactNumbers.Where(x => x.BusinessInformationId == id);
				   db.BusinessContactNumbers.RemoveRange(itemsToDelete);
				   db.SaveChanges();
			   }


			   if (typeVM.BusinessContactNumbers.Count > 0)
			   {
				   var personalContactNumbers = new List<BusinessContactNumber>();

				   foreach (var item in typeVM.BusinessContactNumbers)
				   {
					   personalContactNumbers.Add(new BusinessContactNumber
					   {
						   BusinessInformationId = id,
						   Type = item.Type,
						   PhoneNumber = item.PhoneNumber
					   });
				   }

				   db.BusinessContactNumbers.AddRange(personalContactNumbers);
				   db.SaveChanges();
			   }

			   typeVM.Id = id;

		   }
		   catch (DbUpdateConcurrencyException)
		   {
			   if (db.PersonalInformations.Count(e => e.Id == id) > 0)
			   {
				   return NotFound();
			   }
			   else
			   {
				   throw;
			   }
		   }

		   return Ok(typeVM);
	   }



	}
}