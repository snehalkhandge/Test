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
	   [HttpPost]
	   public ListCustomerQueries SearchCustomeQueries(CustomerQueryViewModel query)
	   {
		   var sqlQuery = db.PersonalInformations
							.Include(t => t.ContactNumbers)
							.OrderByDescending(t => t.Id)
							.AsNoTracking();
		   #region Queries

		   if (query.CustomerNames != null)
		   {
			   string[] words = query.CustomerNames.Name.Split(' ');

			   string firstName = words[0];
			   string lastName = words[1];

			   sqlQuery = sqlQuery.Where(t => t.FirstName.Contains(firstName) || t.LastName.Contains(lastName));
		   }

		   if(query.ContactNumbers != null)
		   {
			   sqlQuery = sqlQuery.Where(t => t.ContactNumbers.Any(x => x.PhoneNumber == query.ContactNumbers.Number));
		   }

		   if(query.CustomerTypes != null)
		   {
			   sqlQuery = sqlQuery.Where(t => t.ContactType.Contains(query.CustomerTypes.Name));
		   }

		   if(query.UnitTypes != null)
		   {
			   sqlQuery = sqlQuery.Where(t => 
								  t.ContactEnquiries.Any(x => 
								  x.PreferredUnitTypes.Any(z => 
								  z.Name == query.UnitTypes.Name)));
		   }

		   if(query.Localities != null)
		   {
			   sqlQuery = sqlQuery.Where(t => 
								  t.ContactEnquiries.Any(x => 
								  x.PreferredLocations.Any(z => 
								  z.Locality == query.Localities.Name)));
		   }

		   if(query.BudgetFromList != null)
		   {
			   sqlQuery = sqlQuery.Where(t => 
								  t.ContactEnquiries.Any(x => 
								  x.BudgetFrom == query.BudgetFromList.Budget));
		   }

		   if(query.BudgetToList != null)
		   {
			   sqlQuery = sqlQuery.Where(t => 
								  t.ContactEnquiries.Any(x => 
								  x.BudgetTo == query.BudgetToList.Budget));
		   }

		   #endregion

		   var listCustomers = new ListCustomerQueries();
		   listCustomers.Total =sqlQuery.Count();
		   var results = sqlQuery.Skip(query.PageSize * (query.Page - 1))
							  .Take(query.PageSize).ToList();

		   foreach (var item in results)
		   {

			   var customer = new CustomerLeadsViewModel();

			   customer.Name = item.FirstName + " " + item.LastName;
			   customer.ContactType = item.ContactType;
			   customer.CustomerId = item.Id;
			   #region Personal Contact Numbers
			   if (item.ContactNumbers != null)
			   {
				   var listNumbers = new List<PartialCustomerContactNumber>();

				   foreach (var cnt in item.ContactNumbers)
				   {
					   listNumbers.Add(new PartialCustomerContactNumber
					   {
						   Number = cnt.PhoneNumber,
						   PersonalInformationId = cnt.PersonalInformationId
					   });
				   }

				   customer.ContactNumbers = listNumbers;
			   }
			   else
			   {
				   customer.ContactNumbers = null;
			   }
			   #endregion

			   #region Budget Locality Unit Types
			   var enquiry = db.ContactEnquiries
								 .Include(t => t.PreferredUnitTypes)
								 .Include(t => t.PreferredLocations)
								 .OrderByDescending(t => t.LastUpdated)
								 .AsNoTracking()
								 .FirstOrDefault();
			   if (enquiry != null && item.ContactType == "Enquiry")
			   {
					  
					   foreach (var loc in enquiry.PreferredLocations)
					   {
							customer.Localities.Add(new PartialCustomerLocality{
								Name = loc.Locality,
								PersonalInformationId = item.Id
							});   
					   }

					   foreach (var unit in enquiry.PreferredUnitTypes)
					   {
						   customer.UnitTypes.Add(new PartialCustomerUnitType
						   {
							   Name = unit.Name,
							   PersonalInformationId = item.Id
						   });
					   }

					   if(enquiry.BudgetFrom != null && enquiry.BudgetTo != null)
					   {
						   customer.BudgetRange = enquiry.BudgetFrom.ToString() + " - " + enquiry.BudgetTo.ToString();                      
					   }
					   else
					   {
						   customer.BudgetRange = "";
					   }

				   listCustomers.Customers.Add(customer);
			   }
			   else
			   {
				   customer.Localities = null;
				   customer.BudgetRange = "";
				   customer.UnitTypes = null;
				   listCustomers.Customers.Add(customer);
			   }


			   #endregion

		   }

		   return listCustomers;
		   

	   }


	   [Route("customersContactNumbers/all")]
	   [HttpPost]	   
	   public List<PartialCustomerContactNumber> PostContactNumbers(PartialSearchQuery query)
	   {
		   string Query = @"SELECT DISTINCT [PhoneNumber]
							FROM [ApplicationDb].[dbo].[PersonalContactNumbers]							
							WHERE  [PhoneNumber] LIKE '%" + query.Query + "%'";

		   var data = db.Database.SqlQuery<long>(Query).ToList(); 
		   
		   var results = new List<PartialCustomerContactNumber>();

		   foreach (var item in data)
		   {
			   results.Add(new PartialCustomerContactNumber
			   {
				   PersonalInformationId = 0,
				   Number = item
			   });
		   }

		   return results;
	   }

	   [Route("customersBudgetTo/all")]
	   [HttpPost]
	   public List<PartialCustomerBudget> PostBudgetTo(PartialSearchQuery query)
	   {
		   string Query = @"SELECT DISTINCT [BudgetTo]
							FROM [ApplicationDb].[dbo].[ContactEnquiries] 
							WHERE  BudgetTo LIKE '%" + query.Query + "%'";

		   var data = db.Database.SqlQuery<decimal>(Query).ToList(); 
		   
		   var results = new List<PartialCustomerBudget>();

		   foreach (var item in data)
		   {
			   results.Add(new PartialCustomerBudget
			   {
				   PersonalInformationId = 0,
				   Budget = item
			   });
		   }

		   return results;
		   
	   }

	   [Route("customersBudgetFrom/all")]
	   [HttpPost]
	   public List<PartialCustomerBudget> PostBudgetFrom(PartialSearchQuery query)
	   {
		   string Query = @"SELECT DISTINCT [BudgetFrom]							  
							FROM [ApplicationDb].[dbo].[ContactEnquiries] 
							WHERE  BudgetFrom LIKE '%" + query.Query + "%'";

		   

		   var data = db.Database.SqlQuery<decimal>(Query).ToList(); 

		   var results = new List<PartialCustomerBudget>();

		   foreach (var item in data)
		   {
			   results.Add(new PartialCustomerBudget
			   {
				   PersonalInformationId = 0,
				   Budget = item
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