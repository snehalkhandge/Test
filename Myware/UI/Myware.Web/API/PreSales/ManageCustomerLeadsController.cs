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
    [Authorize]
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

			if (query.ContactNumbers != null)
			{
				sqlQuery = sqlQuery.Where(t => t.ContactNumbers.Any(x => x.PhoneNumber == query.ContactNumbers.Number));
			}

			if (query.CustomerTypes != null)
			{
				sqlQuery = sqlQuery.Where(t => t.ContactType.Contains(query.CustomerTypes.Name));
			}

			if (query.UnitTypes != null)
			{
				sqlQuery = sqlQuery.Where(t =>
								   t.ContactEnquiries.Any(x =>
								   x.PreferredUnitTypes.Any(z =>
								   z.Name == query.UnitTypes.Name)));
			}

			if (query.Localities != null)
			{
				sqlQuery = sqlQuery.Where(t =>
								   t.ContactEnquiries.Any(x =>
								   x.PreferredLocations.Any(z =>
								   z.Locality == query.Localities.Name)));
			}

			if (query.BudgetFromList != null)
			{
				sqlQuery = sqlQuery.Where(t =>
								   t.ContactEnquiries.Any(x =>
								   x.BudgetFrom == query.BudgetFromList.Budget));
			}

			if (query.BudgetToList != null)
			{
				sqlQuery = sqlQuery.Where(t =>
								   t.ContactEnquiries.Any(x =>
								   x.BudgetTo == query.BudgetToList.Budget));
			}

			#endregion

			var listCustomers = new ListCustomerQueries();
			listCustomers.Total = sqlQuery.Count();
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
						customer.Localities.Add(new PartialCustomerLocality
						{
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

					if (enquiry.BudgetFrom != null && enquiry.BudgetTo != null)
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




		[Route("saveAllDuplicateData")]
		public IHttpActionResult PostBusiness(List<DuplicateData> dataList)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			db.DuplicateData.AddRange(dataList);
			db.SaveChanges();



			return Ok();
		}


		[Route("customersDuplicateData")]
		[HttpGet]
		public ListDuplicateData SearchCustomeQueries(ListDuplicateData query)
		{
			if (query == null)
			{
				query = new ListDuplicateData();
				query.Page = 1;
				query.PageSize = 10;
			}

			var sqlQuery = from c in db.PersonalInformations
						   join p in db.DuplicateData on c.Id equals p.PersonalInformationId
						   select new GetPartialId { Id = c.Id };

			sqlQuery = sqlQuery.OrderByDescending(t => t.Id).AsNoTracking();

			var listCustomers = new ListDuplicateData();
			listCustomers.DuplicateData = new List<DuplicateDataViewModel>();
			listCustomers.Total = sqlQuery.Count();

			var personalIds = sqlQuery.Skip(query.PageSize * (query.Page - 1))
									  .Take(query.PageSize).ToList();

			foreach (var id in personalIds)
			{
				var duplicateData = new DuplicateDataViewModel();

				duplicateData.ChildrenPersonalInformation = new List<PersonalInformationViewModel>();
				duplicateData.ChildrenBusinessInformation = new List<BusinessInformationViewModel>();
				duplicateData.ChildrenContactEnquiry = new List<ContactEnquiryViewModel>();

				#region Personal Information
				var parentPersonalInfo = new PersonalInformationViewModel();
				var cntNumber = new List<PartialPersonalContactNumber>();
				var busiInfo = new List<BusinessInformationViewModel>();

				var result = db.PersonalInformations
							   .Include(t => t.BusinessInformation)
							   .Include(t => t.ContactEnquiries)                               
							   .Where(r => r.Id == id.Id)
							   .SingleOrDefault();

				if (result != null)
				{
					if(result.ContactNumbers != null)
					{
						foreach (var cnt in result.ContactNumbers)
						{
							cntNumber.Add(new PartialPersonalContactNumber
							{
								PhoneNumber = cnt.PhoneNumber,
								Type = cnt.Type

							});
						}
					}

					var sortBusiInfo = db.BusinessInformations
										 .Include(t => t.BusinessContactNumbers)
										 .Where(t => t.PersonalInformationId == result.Id)
										 .OrderByDescending(t => t.LastUpdated)
										 .ToList();
					foreach (var bi in sortBusiInfo)
					{
						busiInfo.Add(new BusinessInformationViewModel
						{
							Id = bi.Id,
							CompanyName = bi.CompanyName,
							Designation = bi.Designation,
							BusinessOrIndustry = bi.BusinessOrIndustry,
							InvestmentCapacity = bi.InvestmentCapacity,
							Fax = bi.Fax,
							Website = bi.Website,
							Locality = bi.Locality,
							City = bi.City,
							Type = bi.Type,
							PersonalInformationId = bi.PersonalInformationId
						});
					}

					parentPersonalInfo = new PersonalInformationViewModel
											{
												Id = result.Id,
												FirstName = result.FirstName,
												LastName = result.LastName,
												Email = result.Email,
												Address = result.Address,
												PinCode = result.PinCode,
												Campaign = result.Campaign,
												SubCampaign = result.SubCampaign,
												ContactType = result.ContactType,
												Locality = result.Locality,
												City = result.City,
												ContactNumbers = cntNumber,
												ImageUrl = result.ImageUrl,
												AnniversaryDate = result.AnniversaryDate,
												DateOfBirth = result.DateOfBirth,
												Remarks = result.Remarks
												
											};

					duplicateData.ParentPersonalInformation = parentPersonalInfo;
					duplicateData.ParentBusinessInformation = busiInfo.SingleOrDefault();


				}

				#endregion
				
				#region Contact Enquiries

				if (parentPersonalInfo.ContactType == "Enquiry")
				{


					var qury = db.ContactEnquiries
									  .Include(t => t.PreferredUnitTypes)
									  .Include(t => t.PreferredLocations)
									  .OrderByDescending(x => x.LastUpdated)
									  .Where(r => r.PersonalInformationId == id.Id)
									  .SingleOrDefault();

						var loc = new List<PartialContactEnquiryLocality>();

						foreach (var item in qury.PreferredLocations.ToList())
						{

							loc.Add(new PartialContactEnquiryLocality
							{
								Locality = item.Locality
							});

						}

						var unit = new List<PartialContactEnquiryUnit>();

						foreach (var item in qury.PreferredUnitTypes.ToList())
						{

							unit.Add(new PartialContactEnquiryUnit
							{
								UnitType = item.Name
							});

						}

					var parentCntEnquiry = new ContactEnquiryViewModel
										{
											Id = qury.Id,
											AssignedDate = qury.AssignedDate,
											BudgetFrom = qury.BudgetFrom,
											BudgetTo = qury.BudgetTo,
											CarpetAreaFrom = qury.CarpetAreaFrom,
											CarpetAreaTo = qury.CarpetAreaTo,
											ContactStatus = qury.ContactStatus,
											EnquiryDate = qury.EnquiryDate,
											FacingType = qury.FacingType,
											IsFurnished = qury.IsFurnished,
											LeadStatus = qury.LeadStatus,
											LookingForType = qury.LookingForType,
											OfferedRate = qury.OfferedRate,
											PersonalInformationId = qury.PersonalInformationId,
											PreferredLocations = loc,
											PreferredUnitTypes = unit,
											PropertyAge = qury.PropertyAge,
											Remarks = qury.Remarks,
											SaleAreaFrom = qury.SaleAreaFrom,
											SaleAreaTo = qury.SaleAreaTo,
											TransactionType = qury.TransactionType
										};

					duplicateData.ParentContactEnquiry = parentCntEnquiry;
				}

				#endregion

				#region Duplicate Data

				var dupEntries = db.DuplicateData
								   .Where(t => t.PersonalInformationId == id.Id)
								   .ToList();

				foreach (var item in dupEntries)
				{
					var perInfo = new PersonalInformationViewModel();                    
					var busInf = new BusinessInformationViewModel();
					perInfo.ContactNumbers = new List<PartialPersonalContactNumber>();
					#region duplicate entry personal info
						if (item.ContactNumbers != null)
						{
							perInfo.ContactNumbers.Add(new PartialPersonalContactNumber{
								PhoneNumber = (item.ContactNumbers == null ? 0 : (long)item.ContactNumbers)
							});
						}

						perInfo.Id = item.PersonalInformationId;
						perInfo.FirstName = item.FirstName;
						perInfo.LastName = item.LastName;
						perInfo.Email = item.Email;
						perInfo.Address = item.Address;
						perInfo.PinCode = item.PinCode;
						perInfo.Campaign = item.Campaign;
						perInfo.SubCampaign = item.SubCampaign;
						perInfo.ContactType = item.ContactType;
						perInfo.Locality = item.Locality;
						perInfo.City = item.City;					    
						perInfo.AnniversaryDate = item.AnniversaryDate;
						perInfo.DateOfBirth = item.DateOfBirth;
						perInfo.Remarks = item.Remarks;
					#endregion

					#region duplicate entry business info

						busInf.CompanyName = item.CompanyName;
						busInf.Designation = item.Designation;
						busInf.BusinessOrIndustry = item.BusinessOrIndustry;
						busInf.InvestmentCapacity = item.InvestmentCapacity;
						busInf.Fax = item.Fax;
						busInf.Website = item.Website;
						busInf.Locality = item.BusinessLocality;
						busInf.City = item.BusinessCity;
						busInf.PersonalInformationId = item.PersonalInformationId;                                
					#endregion
					
					#region Duplicate entry enquiry

						var cntEnqui = new ContactEnquiryViewModel();

						cntEnqui.PreferredUnitTypes = new List<PartialContactEnquiryUnit>();

						if(item.PreferredUnitTypes != "")
						{
							cntEnqui.PreferredUnitTypes.Add(new PartialContactEnquiryUnit { 
								UnitType = item.PreferredUnitTypes
							});
						}

						cntEnqui.Id = item.Id;                        
						cntEnqui.BudgetFrom = item.BudgetFrom;
						cntEnqui.BudgetTo = item.BudgetTo;
						cntEnqui.CarpetAreaFrom = item.CarpetAreaFrom;
						cntEnqui.CarpetAreaTo = item.CarpetAreaTo;                        
						cntEnqui.EnquiryDate = item.EnquiryDate;                        
						cntEnqui.IsFurnished = item.IsFurnished;
						cntEnqui.LeadStatus = item.LeadStatus;
						cntEnqui.LookingForType = item.LookingForType;
						cntEnqui.OfferedRate = item.OfferedRate;
						cntEnqui.PersonalInformationId = item.PersonalInformationId;
						cntEnqui.PropertyAge = item.PropertyAge;
						cntEnqui.Remarks = item.Remarks;
						cntEnqui.SaleAreaFrom = item.SaleAreaFrom;
						cntEnqui.SaleAreaTo = item.SaleAreaTo;
						cntEnqui.TransactionType = item.TransactionType;
						
					

					#endregion


						
					duplicateData.ChildrenPersonalInformation.Add(perInfo);
					duplicateData.ChildrenBusinessInformation.Add(busInf);
					duplicateData.ChildrenContactEnquiry.Add(cntEnqui);
					
				}



				#endregion

				listCustomers.DuplicateData.Add(duplicateData);

			}

			return listCustomers;

		}

	}
}