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
	public class ManagePersonalInformationController : ApiController
	{
	   private ApplicationDbContext db = new ApplicationDbContext();

		// GET: api/ManagePermissions
		
	   [Route("personalInformationById/{id}")]
	   public PersonalInformationViewModel GetPersonalInformationId(int id)
	   {

		   var query = db.PersonalInformations
						 .Include(t => t.ContactNumbers)
						 .Include(t => t.BusinessInformation)
						 .OrderByDescending(x => x.Id);

		   var result = query.Where(r => r.Id == id)
						   .SingleOrDefault();


		   var cntNumber = new List<PartialPersonalContactNumber>();
		   var busiInfo = new List<BusinessInformationViewModel>();

		   if (result != null)
		   {
			   foreach (var cnt in result.ContactNumbers)
			   {
				   cntNumber.Add(new PartialPersonalContactNumber
				   {
					   PhoneNumber = cnt.PhoneNumber,
					   Type = cnt.Type

				   });
			   }

			   var sortBusiInfo = result.BusinessInformation.OrderByDescending(t => t.LastUpdated).ToList();
			   foreach (var cnt in sortBusiInfo)
			   {
				   busiInfo.Add(new BusinessInformationViewModel
				   {
					   Id = cnt.Id,
					   CompanyName = cnt.CompanyName,
					   Designation = cnt.Designation,
					   BusinessOrIndustry = cnt.BusinessOrIndustry,
					   InvestmentCapacity = cnt.InvestmentCapacity,
					   Fax = cnt.Fax,
					   Website = cnt.Website,
					   Locality = cnt.Locality,
					   City = cnt.City,
					   Type = cnt.Type,
					   PersonalInformationId = cnt.PersonalInformationId
				   });
			   }




			   return new PersonalInformationViewModel
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
				   Remarks = result.Remarks,
				   BusinessInformation = busiInfo
			   };
		   }
		   else
		   {
			   return new PersonalInformationViewModel();

		   }

	   }

	   [Route("savePersonalInformation/{id}")]
	   [ResponseType(typeof(PersonalInformationViewModel))]
	   public IHttpActionResult PostBroker(int id, PersonalInformationViewModel typeVM)
	   {


		   if (!ModelState.IsValid)
		   {
			   return BadRequest(ModelState);
		   }


		   if (id != typeVM.Id)
		   {
			   return BadRequest();
		   }

		   var type = new PersonalInformation();

		   try
		   {
			   if (typeVM.Id == 0)
			   {
				   //Check Duplicate Data
				   #region Check Duplicate Data
				   var chkDuplicateEmail = new PersonalInformation();
				   if(typeVM.Email != "")
				   {
					   chkDuplicateEmail = db.PersonalInformations.AsNoTracking()
																  .Where(t => t.Email == typeVM.Email)
																  .FirstOrDefault();
					   if(chkDuplicateEmail != null)
					   {                       
						   return BadRequest("Error, Duplicate Email Data == "+chkDuplicateEmail.Id.ToString());
					   }
				   }
				   var numbers = new List<long>();
				   foreach (var item in typeVM.ContactNumbers)
				   {
					   numbers.Add(item.PhoneNumber);
				   }

				   if(numbers.Count > 0)
				   {
					   var chkDuplicatePhoneNumber = db.PersonalContactNumbers
												   .AsNoTracking()
												   .Where(t => numbers.Contains(t.PhoneNumber))
												   .SingleOrDefault();


					   if (chkDuplicatePhoneNumber != null)
					   {
						   return BadRequest("Error, Duplicate Data Phone Number == " + chkDuplicatePhoneNumber.PersonalInformationId.ToString());
					   }
				   }
				   
				   #endregion

				   type.FirstName = typeVM.FirstName;
				   type.LastName = typeVM.LastName;
				   type.Email = typeVM.Email;
				   type.Address = typeVM.Address;
				   type.PinCode = typeVM.PinCode;
				   type.Campaign = typeVM.Campaign;
				   type.SubCampaign = typeVM.SubCampaign;
				   type.ContactType = typeVM.ContactType;
				   type.Locality = typeVM.Locality;
				   type.City = typeVM.City;                                      
				   type.AnniversaryDate = typeVM.AnniversaryDate;
				   type.DateOfBirth = typeVM.DateOfBirth;
				   type.Remarks = typeVM.Remarks;
				   type.UpdatedByUserId = typeVM.UserId;                   
				   type.LastUpdated = DateTime.UtcNow;

				   db.Entry(type).State = EntityState.Added;
				   db.SaveChanges();

				   id = type.Id;

			   }
			   else
			   {
				   var oldType = db.PersonalInformations
								   .Include(t => t.ContactNumbers)
								   .Include(t => t.BusinessInformation)
								   .Where(e => e.Id == id).SingleOrDefault();
									  
				   

				   var busiInfo = new List<BusinessInformationViewModel>();

				   foreach (var cnt in oldType.BusinessInformation)
				   {
					   busiInfo.Add(new BusinessInformationViewModel
					   {
						   Id = cnt.Id,
						   CompanyName = cnt.CompanyName,
						   Designation = cnt.Designation,
						   BusinessOrIndustry = cnt.BusinessOrIndustry,
						   InvestmentCapacity = cnt.InvestmentCapacity,
						   Fax = cnt.Fax,
						   Website = cnt.Website,
						   Locality = cnt.Locality,
						   City = cnt.City,
						   Type = cnt.Type,
						   PersonalInformationId = cnt.PersonalInformationId
					   });
				   }

				   typeVM.BusinessInformation = busiInfo;


				   oldType.FirstName = typeVM.FirstName;
				   oldType.LastName = typeVM.LastName;
				   oldType.Email = typeVM.Email;
				   oldType.Address = typeVM.Address;
				   oldType.PinCode = typeVM.PinCode;
				   oldType.Campaign = typeVM.Campaign;
				   oldType.SubCampaign = typeVM.SubCampaign;
				   oldType.ContactType = typeVM.ContactType;
				   oldType.Locality = typeVM.Locality;
				   oldType.City = typeVM.City;
				   oldType.AnniversaryDate = typeVM.AnniversaryDate;
				   oldType.DateOfBirth = typeVM.DateOfBirth;
				   oldType.Remarks = typeVM.Remarks;
				   oldType.UpdatedByUserId = typeVM.UserId;      
				   oldType.LastUpdated = DateTime.UtcNow;
				   db.Entry(oldType).State = EntityState.Modified;
				   db.SaveChanges();

				   var itemsToDelete = db.PersonalContactNumbers.Where(x => x.PersonalInformationId == id);
				   db.PersonalContactNumbers.RemoveRange(itemsToDelete);
				   db.SaveChanges();
			   }


			   if (typeVM.ContactNumbers.Count > 0)
			   {
				   var brokerContactNumbers = new List<PersonalContactNumber>();

				   foreach (var item in typeVM.ContactNumbers)
				   {
					   brokerContactNumbers.Add(new PersonalContactNumber
					   {
						   PersonalInformationId = id,
						   Type = item.Type,
						   PhoneNumber = item.PhoneNumber
					   });
				   }

				   db.PersonalContactNumbers.AddRange(brokerContactNumbers);
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


	   [Route("customerEmailIsUnique")]
	   [HttpPost]
	   public CheckUniquePersonalMessageViewModel IsCustomerEmailUnique(EmailVM data)
	   {          

		   var record = db.PersonalInformations.Where(x => x.Email == data.email).SingleOrDefault();

		   var msg = new CheckUniquePersonalMessageViewModel();

		   if (record == null)
		   {
			   msg.IsUnique = true;
			   return msg;
		   }

		   msg.IsUnique = false;
		   msg.PersonalInformation = this.GetPersonalInformationId(record.Id);

		   return msg;
	   }


	   [Route("customerContactNumberIsUnique/{searchQuery}")]
	   [HttpGet]
	   public CheckUniquePersonalMessageViewModel IsCustomerContactNumberUnique(int searchQuery = 0)
	   {


		   var record = db.PersonalContactNumbers.Where(x => x.PhoneNumber == searchQuery).SingleOrDefault();

		   var msg = new CheckUniquePersonalMessageViewModel();

		   if (record == null)
		   {
			   msg.IsUnique = true;
			   return msg;
		   }

		   msg.IsUnique = false;
		   msg.PersonalInformation = this.GetPersonalInformationId(record.PersonalInformationId);

		   return msg;
	   }

	   [Route("savePersonalInfoImage")]
	   [HttpPost]
	   public async Task<HttpResponseMessage> PostCustomerImage()
	   {	   

		   if (!Request.Content.IsMimeMultipartContent())
			   this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);

		   var uploadFolder = "~/Images/Customers";
		   var imageUrl = "";
		   var root = HttpContext.Current.Server.MapPath(uploadFolder);
		   Directory.CreateDirectory(root);


		   var provider = new MultipartFormDataStreamProvider(root + "/Temp");
		   var result = await Request.Content.ReadAsMultipartAsync(provider);
		   if (result.FormData["businessObject"] == null)
		   {
			   throw new HttpResponseException(HttpStatusCode.BadRequest);
		   }


		   //TODO: Do something with the json model which is currently a string


		   #region File upload
		   //get the files
		   foreach (var fileData in result.FileData)
		   {
			   if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
			   {
				   return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
			   }
			   string fileName = fileData.Headers.ContentDisposition.FileName;
			   if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
			   {
				   fileName = fileName.Trim('"');
			   }
			   if (fileName.Contains(@"/") || fileName.Contains(@"\"))
			   {
				   fileName = Path.GetFileName(fileName);
			   }

			   if (File.Exists(Path.Combine(root, fileName)))
			   {
				   File.Delete(Path.Combine(root, fileName));
			   }

			   File.Move(fileData.LocalFileName, Path.Combine(root, fileName));

			   #region Update Broker
			   var model = new JavaScriptSerializer().Deserialize<PartialGetId>(result.FormData["businessObject"]);
			   var existingEntity = db.PersonalInformations.Where(e => e.Id == model.id).SingleOrDefault();

			   existingEntity.ImageUrl = "Images/Customers/" + fileName;

			   db.Entry(existingEntity).State = EntityState.Modified;
			   db.SaveChanges();

			   imageUrl = existingEntity.ImageUrl;
			   #endregion

		   }
		   #endregion



		   return Request.CreateResponse(HttpStatusCode.OK, imageUrl);



	   }



	}
}