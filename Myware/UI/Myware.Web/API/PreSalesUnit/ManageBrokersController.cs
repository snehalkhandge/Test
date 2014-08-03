using Myware.Data.Entity;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Web.Models;
using Myware.Web.Models.PreSalesUnit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace Myware.Web.API.UserManagement
{
	[Authorize]
	public class ManageBrokersController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: api/ManagePermissions

		[Route("brokers/{page}/size/{pageSize}/search/{searchQuery}")]
		public ListBrokerViewModel GetBrokers(int page = 1, int pageSize = 10, string searchQuery = "")
		{
			var query = db.Brokers.Include(t => t.Locality)
									.Include(t => t.Locality.Location)
									.Include(t => t.ContactNumbers)									
									.OrderByDescending(x => x.Id);
									


			var total = query.Count();
			var result = query.Skip(pageSize * (page - 1))
							  .Take(pageSize).ToList();


			var listResult = new ListBrokerViewModel();
			listResult.TotalItems = total;
			listResult.Results = new List<CreateBrokerViewModel>();
			
			foreach (var item in result)
			{
				var cntNumber = new List<PartialBrokerContactNumber>();

				foreach (var cnt in item.ContactNumbers)
				{
					cntNumber.Add(new PartialBrokerContactNumber
					{                        
						PhoneNumber = cnt.PhoneNumber,
						Type = cnt.Type

					});
				}


				var loc = new PartialLocality();

				loc.Id = item.LocalityId;
				loc.Name = item.Locality.Name;
				loc.City = item.Locality.Location.City;
				loc.State = item.Locality.Location.State;
				loc.Country = item.Locality.Location.Country;

				listResult.Results.Add(new CreateBrokerViewModel
				{
					Id = item.Id,
					Name=item.Name,
					Address = item.Address,
					ContactNumbers = cntNumber,
					LocalityId = item.LocalityId,
					Locality = loc ,
					CompanyName = item.CompanyName,
					Email = item.Email,
					PanCard = item.PanCard,
					ReferenceName = item.ReferenceName,
					ImageUrl = item.ImageUrl

				});
		 
			}


			return listResult;

		}


		[Route("brokerById/{id}")]
		public CreateBrokerViewModel GetCompanyById(int id)
		{

			var query = db.Brokers.Include(t => t.Locality)
									.Include(t => t.Locality.Location)
									.Include(t => t.ContactNumbers)									
									.OrderByDescending(x => x.Id);

			
			var result = query.Where(r => r.Id == id)
							.SingleOrDefault();


			var cntNumber = new List<PartialBrokerContactNumber>();

			foreach (var cnt in result.ContactNumbers)
			{
				cntNumber.Add(new PartialBrokerContactNumber
				{
					PhoneNumber = cnt.PhoneNumber,
					Type = cnt.Type

				});
			}


			var loc = new PartialLocality();

			loc.Id = result.LocalityId;
			loc.Name = result.Locality.Name;
			loc.City = result.Locality.Location.City;
			loc.State = result.Locality.Location.State;
			loc.Country = result.Locality.Location.Country;

			return new CreateBrokerViewModel
								{
									Id = result.Id,
									Name = result.Name,
									Address = result.Address,
									ContactNumbers = cntNumber,
									LocalityId = result.LocalityId,
									Locality = loc,
									CompanyName = result.CompanyName,
									Email = result.Email,
									PanCard = result.PanCard,
									ReferenceName = result.ReferenceName,
									ImageUrl = result.ImageUrl

								};
			
		}

		[Route("brokers/all")]
		public ListBrokerViewModel GetAllCompanies()
		{
			var query = db.Brokers						  
						  .OrderByDescending(x => x.Id);

			return new ListBrokerViewModel
			{

				TotalItems = 0,
				Results = query.Select(t => new CreateBrokerViewModel
												{
													Id = t.Id,
													Name = t.Name													
												}).ToList()
			
			};

		}


		[Route("brokerIsUnique/{searchQuery}")]
		public bool IsBrokerUnique(string searchQuery = "")
		{
			return db.Brokers.Any(x => x.Name.Contains(searchQuery)); 
		}


		[Route("saveBroker/{id}")]
		[ResponseType(typeof(CreateBrokerViewModel))]
		public IHttpActionResult PostBroker(int id, CreateBrokerViewModel typeVM)
		{
			

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			if (id != typeVM.Id)
			{
				return BadRequest();
			}

			var type = new Broker();

			try
			{
				if (typeVM.Id == 0)
				{


					type.Name = typeVM.Name;
					type.Address = typeVM.Address;
					type.LocalityId = typeVM.LocalityId;
					type.CompanyName = typeVM.CompanyName;                    
					type.Email = typeVM.Email;
					type.PanCard = typeVM.PanCard;
					type.ReferenceName = typeVM.ReferenceName;
					type.UpdatedByUserId = typeVM.UserId;
					type.ImageUrl = typeVM.ImageUrl;
					type.LastUpdated = DateTime.UtcNow;
					db.Entry(type).State = EntityState.Added;
					db.SaveChanges();

					id = type.Id;
				}
				else
				{
					var oldType  = db.Brokers.Include(t => t.Locality)
									.Include(t => t.Locality.Location)
									.Include(t => t.ContactNumbers)									
									.Where(e => e.Id == id).SingleOrDefault();

					oldType.Name = typeVM.Name;
					oldType.Address = typeVM.Address;
					oldType.LocalityId = typeVM.LocalityId;
					oldType.CompanyName = typeVM.CompanyName;
					oldType.Email = typeVM.Email;
					oldType.PanCard = typeVM.PanCard;
					oldType.ReferenceName = typeVM.ReferenceName;
					oldType.UpdatedByUserId = typeVM.UserId;
					oldType.ImageUrl = typeVM.ImageUrl;
					oldType.LastUpdated = DateTime.UtcNow;
					db.Entry(oldType).State = EntityState.Modified;
					db.SaveChanges();



					var itemsToDelete = db.BrokerContactNumbers.Where(x => x.BrokerId == id);
					db.BrokerContactNumbers.RemoveRange(itemsToDelete);
					db.SaveChanges();
				}


				if(typeVM.ContactNumbers.Count > 0)
				{
					var brokerContactNumbers = new List<BrokerContactNumber>();

					foreach (var item in typeVM.ContactNumbers)
					{
						brokerContactNumbers.Add(new BrokerContactNumber
						{
							BrokerId = id,
							Type = item.Type,
							PhoneNumber = item.PhoneNumber
						});
					}

					db.BrokerContactNumbers.AddRange(brokerContactNumbers);
					db.SaveChanges();
				}
				
			}                        
			catch (DbUpdateConcurrencyException)
			{
				if (db.Brokers.Count(e => e.Id == id) > 0)
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

		private static readonly string ServerUploadFolder = "C:\\Temp"; //Path.GetTempPath();

		[Route("saveBrokerImage")]
		[HttpPost]
		public async Task<HttpResponseMessage> PostBrokerImage()
		{
			return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");

			if(!Request.Content.IsMimeMultipartContent())
				this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);

			var uploadFolder = "~/Images/Brokers";
			var imageUrl = "";
			var root = HttpContext.Current.Server.MapPath(uploadFolder);
			Directory.CreateDirectory(root);


			var provider = new MultipartFormDataStreamProvider(root+"/Temp");
			var result = await Request.Content.ReadAsMultipartAsync(provider);
			if (result.FormData["brokerObject"] == null)
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
				var model = new JavaScriptSerializer().Deserialize<PartialGetId>(result.FormData["brokerObject"]);
				var existingEntity = db.Brokers.Where(e => e.Id == model.id).SingleOrDefault();

				existingEntity.ImageUrl = "Images/Brokers/" + fileName;

				db.Entry(existingEntity).State = EntityState.Modified;
				db.SaveChanges();

				imageUrl = existingEntity.ImageUrl;
				#endregion

			}
			#endregion



			return Request.CreateResponse(HttpStatusCode.OK, imageUrl);


		  
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		
	}
}