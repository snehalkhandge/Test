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
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;


namespace Myware.Web.API.PreSales
{
	public class ManageDuplicateDataController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();

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


		[Route("deleteDuplicateData/{personalId}")]
		public IHttpActionResult PostDeleteDuplicateByPersonalId(int personalId)
		{
			
			var entriesToRemove = db.DuplicateData.Where(x => x.PersonalInformationId == personalId).ToList();

			if(entriesToRemove.Count > 0)
			{
				db.DuplicateData.RemoveRange(entriesToRemove);
				db.SaveChanges();
			}

			return Ok();
		}

		[Route("exportAllDuplicateData")]
		[HttpGet]
		public HttpResponseMessage ExportDuplicateData()
		{
			var listExportData = new List<ExportDuplicateDataViewModel>();

			var sqlQuery = from c in db.PersonalInformations
						   join p in db.DuplicateData on c.Id equals p.PersonalInformationId
						   select new GetPartialId { Id = c.Id };

			var personalIds = sqlQuery.OrderByDescending(t => t.Id)
							   .AsNoTracking()
							   .Take(100).ToList();

			foreach (var item in personalIds)
			{
				var duplicateData = new ExportDuplicateDataViewModel();
				
				
				var result = db.PersonalInformations
							   .Include(t => t.BusinessInformation)
							   .Include(t => t.ContactEnquiries)
							   .Include(t => t.ContactNumbers)
							   .Where(r => r.Id == item.Id)
							   .SingleOrDefault();

				if (result != null)
				{
					if (result.ContactNumbers != null)
					{
						var cntNumber = new List<long>();

						foreach (var cnt in result.ContactNumbers)
						{
							cntNumber.Add(cnt.PhoneNumber);
						}

						duplicateData.ContactNumbers = cntNumber.FirstOrDefault();
					}

					var sortBusiInfo = db.BusinessInformations
										 .Include(t => t.BusinessContactNumbers)
										 .Where(t => t.PersonalInformationId == result.Id)
										 .OrderByDescending(t => t.LastUpdated)
										 .SingleOrDefault();
					#region Business Information
					duplicateData.BusinessId = sortBusiInfo.Id;
					duplicateData.CompanyName = sortBusiInfo.CompanyName;
					duplicateData.Designation = sortBusiInfo.Designation;
					duplicateData.BusinessOrIndustry = sortBusiInfo.BusinessOrIndustry;
					duplicateData.InvestmentCapacity = sortBusiInfo.InvestmentCapacity;
					duplicateData.Fax = sortBusiInfo.Fax;
					duplicateData.Website = sortBusiInfo.Website;
					duplicateData.BusinessLocality = sortBusiInfo.Locality;
					duplicateData.BusinessCity = sortBusiInfo.City;
					#endregion
					#region Personal Information
					duplicateData.FirstName = result.FirstName;
					duplicateData.LastName = result.LastName;
					duplicateData.Email = result.Email;
					duplicateData.Address = result.Address;
					duplicateData.PinCode = result.PinCode;
					duplicateData.Campaign = result.Campaign;
					duplicateData.SubCampaign = result.SubCampaign;
					duplicateData.ContactType = result.ContactType;
					duplicateData.Locality = result.Locality;
					duplicateData.City = result.City;                                            
					duplicateData.AnniversaryDate = result.AnniversaryDate;
					duplicateData.DateOfBirth = result.DateOfBirth;
					duplicateData.Remarks = result.Remarks;

					#endregion

					#region Contact Enquiries

					if (duplicateData.ContactType == "Enquiry")
					{


						var qury = db.ContactEnquiries
										  .Include(t => t.PreferredUnitTypes)
										  .Include(t => t.PreferredLocations)
										  .OrderByDescending(x => x.LastUpdated)
										  .Where(r => r.PersonalInformationId == item.Id)
										  .SingleOrDefault();


						var unit = new List<string>();

						foreach (var uType in qury.PreferredUnitTypes.ToList())
						{
							unit.Add(uType.Name);
						}

						duplicateData.PreferredUnitTypes = unit.FirstOrDefault();
						duplicateData.ContactEnquiryId = qury.Id;
						duplicateData.BudgetFrom = qury.BudgetFrom;
						duplicateData.BudgetTo = qury.BudgetTo;
						duplicateData.CarpetAreaFrom = qury.CarpetAreaFrom;
						duplicateData.CarpetAreaTo = qury.CarpetAreaTo;
						duplicateData.EnquiryDate = qury.EnquiryDate;
						duplicateData.IsFurnished = qury.IsFurnished;
						duplicateData.LeadStatus = qury.LeadStatus;
						duplicateData.LookingForType = qury.LookingForType;
						duplicateData.OfferedRate = qury.OfferedRate;
						duplicateData.PersonalInformationId = qury.PersonalInformationId;
						duplicateData.PropertyAge = qury.PropertyAge;
						duplicateData.Remarks = qury.Remarks;
						duplicateData.SaleAreaFrom = qury.SaleAreaFrom;
						duplicateData.SaleAreaTo = qury.SaleAreaTo;
						duplicateData.TransactionType = qury.TransactionType;

					}

					#endregion

					listExportData.Add(duplicateData);

					#region Duplicate Enquiries
					var duplicateEnquiries = db.DuplicateData
											   .Where(x => x.PersonalInformationId == result.Id)
											   .ToList();


					foreach (var enq in duplicateEnquiries)
					{
						var dupData = new ExportDuplicateDataViewModel();
						dupData.ContactEnquiryId = duplicateData.ContactEnquiryId;
						dupData.BusinessId = duplicateData.BusinessId;
						dupData.PersonalInformationId = duplicateData.PersonalInformationId;

						dupData.Address = enq.Address;
						dupData.AnniversaryDate = enq.AnniversaryDate;
						dupData.BudgetFrom = enq.BudgetFrom;
						dupData.BudgetTo = enq.BudgetTo;
						dupData.BusinessCity = enq.BusinessCity;
						dupData.BusinessContactNumbers = enq.BusinessContactNumbers;
						dupData.BusinessLocality = enq.BusinessLocality;
						dupData.BusinessOrIndustry = enq.BusinessOrIndustry;
						dupData.Campaign = enq.Campaign;
						dupData.CarpetAreaFrom = enq.CarpetAreaFrom;
						dupData.CarpetAreaTo = enq.CarpetAreaTo;
						dupData.City = enq.City;
						dupData.CompanyName = enq.CompanyName;
						dupData.ContactNumbers = enq.ContactNumbers;
						dupData.ContactType = enq.ContactType;
						dupData.DateOfBirth = enq.DateOfBirth;
						dupData.Designation = enq.Designation;
						dupData.Email = enq.Email;
						dupData.EnquiryDate = enq.EnquiryDate;
						dupData.Fax = enq.Fax;
						dupData.FirstName = enq.Fax;
						dupData.InvestmentCapacity = enq.InvestmentCapacity;
						dupData.IsFurnished = enq.IsFurnished;
						dupData.LastName = enq.LastName;
						dupData.LeadStatus = enq.LeadStatus;
						dupData.Locality = enq.Locality;
						dupData.LookingForType = enq.LookingForType;
						dupData.OfferedRate = enq.OfferedRate;
						dupData.PinCode = enq.PinCode;
						dupData.PreferredUnitTypes = enq.PreferredUnitTypes;
						dupData.PropertyAge = enq.PropertyAge;
						dupData.Remarks = enq.Remarks;
						dupData.SaleAreaFrom = enq.SaleAreaFrom;
						dupData.SaleAreaTo = enq.SaleAreaTo;
						dupData.SubCampaign = enq.SubCampaign;
						dupData.TransactionType = enq.TransactionType;
						dupData.Website = enq.TransactionType;


						listExportData.Add(duplicateData);


					}

					#endregion


				}

			}

			CsvExport<ExportDuplicateDataViewModel> csv = new CsvExport<ExportDuplicateDataViewModel>(listExportData);


			var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(csv.ExportToBytes()) };
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = "exportDuplicateData.csv"
			};
			return response;

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