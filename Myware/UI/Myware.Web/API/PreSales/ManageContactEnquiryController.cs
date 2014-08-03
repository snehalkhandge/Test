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
	public class ManageContactEnquiryController : ApiController
	{
	   private ApplicationDbContext db = new ApplicationDbContext();

		// GET: api/ManagePermissions

	   [Route("contactEnquiryById/{id}")]
	   public ContactEnquiryViewModel GetContactEnquiryId(int id)
	   {

		   var query = db.ContactEnquiries
						 .Include(t => t.PreferredUnitTypes)
						 .Include(t => t.PreferredLocations)
						 .OrderByDescending(x => x.Id);

		   var result = query.Where(r => r.Id == id)
						   .SingleOrDefault();

		   var loc = new List<PartialContactEnquiryLocality>();

		   foreach (var item in result.PreferredLocations.ToList())
		   {
			   
			   loc.Add(new PartialContactEnquiryLocality{
				   Locality = item.Locality
			   });

		   }

		   var unit = new List<PartialContactEnquiryUnit>();

		   foreach (var item in result.PreferredUnitTypes.ToList())
		   {
			   
			   unit.Add(new PartialContactEnquiryUnit{
				   UnitType = item.Name
			   });

		   }

		   return new ContactEnquiryViewModel
		   {
			   Id = result.Id,
			   AssignedDate = result.AssignedDate,
			   BudgetFrom = result.BudgetFrom,
			   BudgetTo = result.BudgetTo,
			   CarpetAreaFrom = result.CarpetAreaFrom,
			   CarpetAreaTo = result.CarpetAreaTo,
			   ContactStatus = result.ContactStatus,
			   EnquiryDate =result.EnquiryDate,
			   FacingType = result.FacingType,
			   IsFurnished = result.IsFurnished,
			   LeadStatus = result.LeadStatus,
			   LookingForType = result.LookingForType,
			   OfferedRate = result.OfferedRate,
			   PersonalInformationId = result.PersonalInformationId,
			   PreferredLocations = loc,
			   PreferredUnitTypes = unit,
			   PropertyAge = result.PropertyAge,
			   Remarks = result.Remarks,
			   SaleAreaFrom = result.SaleAreaFrom,
			   SaleAreaTo = result.SaleAreaTo,
			   TransactionType = result.TransactionType              
		   };

	   }


	   [Route("getAllContactEnquiriesByPersonalId/{id}")]
	   [HttpGet]
	   public List<ContactEnquiryViewModel> GetAllContactEnquiriesByPersonalId(int id)
	   {

		   var query = db.ContactEnquiries
						 .Include(t => t.PreferredUnitTypes)
						 .Include(t => t.PreferredLocations)
						 .OrderByDescending(x => x.LastUpdated);

		   var resultEnquiries = query.Where(r => r.PersonalInformationId == id).ToList();

		   var enquiries = new List<ContactEnquiryViewModel>();

		   foreach (var result in resultEnquiries)
		   {
			   var loc = new List<PartialContactEnquiryLocality>();

			   foreach (var item in result.PreferredLocations.ToList())
			   {

				   loc.Add(new PartialContactEnquiryLocality
				   {
					   Locality = item.Locality
				   });

			   }

			   var unit = new List<PartialContactEnquiryUnit>();

			   foreach (var item in result.PreferredUnitTypes.ToList())
			   {

				   unit.Add(new PartialContactEnquiryUnit
				   {
					   UnitType = item.Name
				   });

			   }

			   enquiries.Add(new ContactEnquiryViewModel
						   {
							   Id = result.Id,
							   AssignedDate = result.AssignedDate,
							   BudgetFrom = result.BudgetFrom,
							   BudgetTo = result.BudgetTo,
							   CarpetAreaFrom = result.CarpetAreaFrom,
							   CarpetAreaTo = result.CarpetAreaTo,
							   ContactStatus = result.ContactStatus,
							   EnquiryDate = result.EnquiryDate,
							   FacingType = result.FacingType,
							   IsFurnished = result.IsFurnished,
							   LeadStatus = result.LeadStatus,
							   LookingForType = result.LookingForType,
							   OfferedRate = result.OfferedRate,
							   PersonalInformationId = result.PersonalInformationId,
							   PreferredLocations = loc,
							   PreferredUnitTypes = unit,
							   PropertyAge = result.PropertyAge,
							   Remarks = result.Remarks,
							   SaleAreaFrom = result.SaleAreaFrom,
							   SaleAreaTo = result.SaleAreaTo,
							   TransactionType = result.TransactionType
						   });



			   
		   }

		   return enquiries;
	   }


	   [Route("saveContactEnquiry/{id}")]
	   [ResponseType(typeof(ContactEnquiryViewModel))]
	   public IHttpActionResult PostBusiness(int id, ContactEnquiryViewModel typeVM)
	   {


		   if (!ModelState.IsValid)
		   {
			   return BadRequest(ModelState);
		   }


		   if (id != typeVM.Id)
		   {
			   return BadRequest();
		   }

		   try
		   {
			   if (typeVM.Id == 0)
			   {
				   var type = new ContactEnquiry();
				   
				   type.AssignedDate = typeVM.AssignedDate;
				   type.BudgetFrom = typeVM.BudgetFrom;
				   type.BudgetTo = typeVM.BudgetTo;
				   type.CarpetAreaFrom = typeVM.CarpetAreaFrom;
				   type.CarpetAreaTo = typeVM.CarpetAreaTo;
				   type.ContactStatus = typeVM.ContactStatus;
				   type.EnquiryDate =typeVM.EnquiryDate;
				   type.FacingType = typeVM.FacingType;
				   type.IsFurnished = typeVM.IsFurnished;
				   type.LeadStatus = typeVM.LeadStatus;
				   type.LookingForType = typeVM.LookingForType;
				   type.OfferedRate = typeVM.OfferedRate;                                      
				   type.PropertyAge = typeVM.PropertyAge;
				   type.Remarks = typeVM.Remarks;
				   type.SaleAreaFrom = typeVM.SaleAreaFrom;
				   type.SaleAreaTo = typeVM.SaleAreaTo;
				   type.TransactionType = typeVM.TransactionType; 


				   type.PersonalInformationId = typeVM.PersonalInformationId;
				   type.UpdatedByUserId = typeVM.UserId;
				   type.Created = DateTime.UtcNow;
				   type.LastUpdated = DateTime.UtcNow;

				   db.Entry(type).State = EntityState.Added;
				   db.SaveChanges();

				   id = type.Id;

			   }
			   else
			   {
				   var type = db.ContactEnquiries
								   .Include(t => t.PreferredUnitTypes)
								   .Include(t => t.PreferredLocations)						 							   
								   .Where(e => e.Id == id).SingleOrDefault();


				   type.AssignedDate = typeVM.AssignedDate;
				   type.BudgetFrom = typeVM.BudgetFrom;
				   type.BudgetTo = typeVM.BudgetTo;
				   type.CarpetAreaFrom = typeVM.CarpetAreaFrom;
				   type.CarpetAreaTo = typeVM.CarpetAreaTo;
				   type.ContactStatus = typeVM.ContactStatus;
				   type.EnquiryDate = typeVM.EnquiryDate;
				   type.FacingType = typeVM.FacingType;
				   type.IsFurnished = typeVM.IsFurnished;
				   type.LeadStatus = typeVM.LeadStatus;
				   type.LookingForType = typeVM.LookingForType;
				   type.OfferedRate = typeVM.OfferedRate;
				   type.PropertyAge = typeVM.PropertyAge;
				   type.Remarks = typeVM.Remarks;
				   type.SaleAreaFrom = typeVM.SaleAreaFrom;
				   type.SaleAreaTo = typeVM.SaleAreaTo;
				   type.TransactionType = typeVM.TransactionType;
				   type.PersonalInformationId = typeVM.PersonalInformationId;
				   type.UpdatedByUserId = typeVM.UserId;
				   type.LastUpdated = DateTime.UtcNow;
									  
				   db.Entry(type).State = EntityState.Modified;
				   db.SaveChanges();

				   var itemsToDelete = db.ContactEnquiryLocalities.Where(x => x.ContactEnquiryId == id);
				   db.ContactEnquiryLocalities.RemoveRange(itemsToDelete);
				   db.SaveChanges();

				   var itemsToDel = db.ContactEnquiryUnitTypes.Where(x => x.ContactEnquiryId == id);
				   db.ContactEnquiryUnitTypes.RemoveRange(itemsToDel);
				   db.SaveChanges();
			   }


			   if (typeVM.PreferredUnitTypes.Count > 0)
			   {
				   var unitTypes = new List<ContactEnquiryUnitType>();

				   foreach (var item in typeVM.PreferredUnitTypes)
				   {
					   unitTypes.Add(new ContactEnquiryUnitType
					   {
						   ContactEnquiryId = id,
						   Name = item.UnitType
					   });
				   }

				   db.ContactEnquiryUnitTypes.AddRange(unitTypes);
				   db.SaveChanges();
			   }

			   if (typeVM.PreferredLocations.Count > 0)
			   {
				   var locTypes = new List<ContactEnquiryLocality>();

				   foreach (var item in typeVM.PreferredLocations)
				   {
					   locTypes.Add(new ContactEnquiryLocality
					   {
						   ContactEnquiryId = id,
						   Locality = item.Locality
					   });
				   }

				   db.ContactEnquiryLocalities.AddRange(locTypes);
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