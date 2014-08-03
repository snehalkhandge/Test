using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Myware.Data.Entity;
using Myware.Data.Entity.Models.PostSalesUnit;
using Myware.Web.Models.PostSalesUnit;
using Myware.Web.Models.PreSalesUnit;
using System.Web;
using System.IO;

namespace Myware.Web.API.PostSalesUnit
{
    [Authorize]
	public class ManageProjectsController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		#region Search Projects
		[Route("listProjects")]
		[HttpPost]
		public ListSearchResultProjectViewModel GetProjects(ListSearchResultProjectViewModel searchQuery)
		{
			var query = db.ProjectInformation
						  .Include(t => t.Company)
						  .Include(t => t.Project);

			if(searchQuery.CompanyName != "")
			{
				query = query.Where(t => t.Company.Name.Contains(searchQuery.CompanyName));
			}

			if(searchQuery.City != "")
			{
				query = query.Where(t => t.City == searchQuery.City);
			}

			if(searchQuery.Locality != "")
			{
				query = query.Where(t => t.Locality == searchQuery.Locality);
			}


			query = query.OrderByDescending(x => x.Id);
			var total = query.Count();
			var result = query.Skip(searchQuery.PageSize * (searchQuery.Page - 1))
							  .Take(searchQuery.PageSize).ToList();


			var listResult = new ListSearchResultProjectViewModel();
			listResult.TotalItems = total;
			listResult.Results = new List<SearchResultProjectViewModel>();

			foreach (var item in result)
			{
			   listResult.Results.Add(new SearchResultProjectViewModel
			   {
				   CompanyName = item.Company.Name,
				   Locality = item.Locality,
				   NumberOfBuilding = item.NumberOfBuilding,
				   ProjectName = item.Project.ProjectName,
				   PlotArea = item.PlotArea,
				   Id = item.Id

			   });
								
			}


			return listResult;

		}

		[Route("projects/relatedCompanies")]
		public List<string> GetRelatedCompanies()
		{

			var results = db.ProjectInformation
							 .Include(t => t.Company)
							 .Select(t => t.Company.Name)
							 .Distinct()
							 .ToList();

			return results;			
		}

		[Route("projects/relatedCities")]
		public List<string> GetRelatedCities()
		{

			var results = db.ProjectInformation                             
							 .Select(t => t.City)
							 .Distinct()
							 .ToList();
			return results;
		}

		[Route("projects/relatedLocalities")]
		public List<string> GetRelatedLocalities()
		{

			var results = db.ProjectInformation
							 .Select(t => t.Locality)
							 .Distinct()
							 .ToList();
			return results;
		}

		#endregion


		#region Create Edit Project Base


		[Route("projects/getProjectTypes")]
		public List<ProjectTypeViewModel> GetProjectTypes()
		{

			var results = db.ProjectTypes
							.Select(t => new ProjectTypeViewModel { 
								Id = t.Id,
								Name = t.Name
							}).ToList();
			return results;
		}

		
		[Route("saveProjectBase/{id}")]
		[ResponseType(typeof(ProjectBaseViewModel))]
		public IHttpActionResult SavePostProjectBase(int id, ProjectBaseViewModel typeVM)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			if (id != typeVM.Id)
			{
				return BadRequest();
			}

			var type = new Project();

			try
			{
				if (typeVM.Id == 0)
				{
					type.ProjectId = typeVM.ProjectId;
					type.ProjectName = typeVM.ProjectName;
					type.ProjectTypeId = typeVM.ProjectTypeId;
					type.LastUpdated = DateTime.UtcNow;
					type.UpdatedByUserId = typeVM.UserId;
					db.Entry(type).State = EntityState.Added;
					db.SaveChanges();

					typeVM.Id = type.Id;
				}
				else
				{
					var oldType = db.Projects.Where(t => t.Id == typeVM.Id).SingleOrDefault();

					oldType.ProjectId = typeVM.ProjectId;
					oldType.ProjectName = typeVM.ProjectName;
					oldType.ProjectTypeId = typeVM.ProjectTypeId;
					oldType.LastUpdated = DateTime.UtcNow;
					oldType.UpdatedByUserId = typeVM.UserId;
					db.Entry(oldType).State = EntityState.Modified;
					db.SaveChanges();

				}

			}
			catch (DbUpdateConcurrencyException)
			{
				if (db.Projects.Count(e => e.Id == typeVM.Id) > 0)
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

		#endregion

		#region Project Other Information

		[Route("saveProjectOtherInformation/{id}")]
		[ResponseType(typeof(ProjectOtherInformationViewModel))]
		public IHttpActionResult SaveProjectOtherInformation(int id, ProjectOtherInformationViewModel typeVM)
		{


			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			if (id != typeVM.Id)
			{
				return BadRequest();
			}

			var type = new ProjectOtherInformation();

			try
			{
				if (typeVM.Id == 0)
				{
					type.Address = typeVM.Address;
					type.Amneties = typeVM.Amneties;
					type.City = typeVM.City;
					type.CompanyId = typeVM.CompanyId;
					type.FloorPlan = typeVM.FloorPlan;
					type.FSI = typeVM.FSI;
					type.Locality = typeVM.Locality;
					type.NumberOfBuilding = typeVM.NumberOfBuilding;
					type.NumberOfFlats = typeVM.NumberOfFlats;
					type.NumberOfOffices = typeVM.NumberOfOffices;
					type.NumberOfShops = typeVM.NumberOfShops;
					type.PlotArea = typeVM.PlotArea;
					type.PlotAreaUnit = typeVM.PlotAreaUnit;
					type.PlotNumber = typeVM.PlotNumber;
					type.ProjectId = typeVM.ProjectId;
					type.SurveyOrSectorNumber = typeVM.SurveyOrSectorNumber;
					

					db.Entry(type).State = EntityState.Added;
					db.SaveChanges();

					typeVM.Id = type.Id;
				}
				else
				{
					var oldType = db.ProjectInformation.Where(t => t.Id == typeVM.Id)
									.SingleOrDefault();

					oldType.Address = typeVM.Address;
					oldType.Amneties = typeVM.Amneties;
					oldType.City = typeVM.City;
					oldType.CompanyId = typeVM.CompanyId;
					oldType.FloorPlan = typeVM.FloorPlan;
					oldType.FSI = typeVM.FSI;
					oldType.Locality = typeVM.Locality;
					oldType.NumberOfBuilding = typeVM.NumberOfBuilding;
					oldType.NumberOfFlats = typeVM.NumberOfFlats;
					oldType.NumberOfOffices = typeVM.NumberOfOffices;
					oldType.NumberOfShops = typeVM.NumberOfShops;
					oldType.PlotArea = typeVM.PlotArea;
					oldType.PlotAreaUnit = typeVM.PlotAreaUnit;
					oldType.PlotNumber = typeVM.PlotNumber;
					oldType.ProjectId = typeVM.ProjectId;
					oldType.SurveyOrSectorNumber = typeVM.SurveyOrSectorNumber;

					db.Entry(oldType).State = EntityState.Modified;
					db.SaveChanges();

					var itemsToDelete = db.ProjectDevelopers.Where(x => x.ProjectOtherInformationId == id);
					db.ProjectDevelopers.RemoveRange(itemsToDelete);
					db.SaveChanges();

				}

				if (typeVM.Developers.Count > 0)
				{
					var projectDevelopers = new List<ProjectDevelopers>();

					foreach (var item in typeVM.Developers)
					{
						projectDevelopers.Add(new ProjectDevelopers
						{
							DeveloperId = item.DeveloperId,
							DeveloperName = item.DeveloperName,
							ProjectOtherInformationId = typeVM.Id
						});
					}

					db.ProjectDevelopers.AddRange(projectDevelopers);
					db.SaveChanges();
				}


			}
			catch (DbUpdateConcurrencyException)
			{
				if (db.ProjectInformation
					.Count(e => e.Id == id) > 0)
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


		[Route("saveFloorPlans")]
		[HttpPost]
		public async Task<HttpResponseMessage> SavePostFloorPlans()
		{
			

			if (!Request.Content.IsMimeMultipartContent())
				this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);

			var uploadFolder = "~/Images/Projects/FloorPlans";
			var imageUrl = "";
			var root = HttpContext.Current.Server.MapPath(uploadFolder);
			Directory.CreateDirectory(root);


			var provider = new MultipartFormDataStreamProvider(root + "/Temp");
			var result = await Request.Content.ReadAsMultipartAsync(provider);
			if (result.FormData["projectObject"] == null)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

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

				imageUrl = "Images/Projects/FloorPlans/" + fileName;
			}
			#endregion

			return Request.CreateResponse(HttpStatusCode.OK, imageUrl);

		}

		#endregion

		#region Project Property Charges

		[Route("saveProjectPropertyCharges/{id}")]
		[ResponseType(typeof(ProjectPropertyChargesViewModel))]
		public IHttpActionResult SaveProjectPropertyCharges(int id, ProjectPropertyChargesViewModel typeVM)
		{


			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			if (id != typeVM.Id)
			{
				return BadRequest();
			}

			var type = new ProjectPropertyCharges();

			try
			{
				if (typeVM.Id == 0)
				{
					type.BasicQuateRate = typeVM.BasicQuateRate;
					type.DevelopmentCharge = typeVM.DevelopmentCharge;
					type.FloorNumberOnWord = typeVM.FloorNumberOnWord;
					type.FloorRiseRate = typeVM.FloorRiseRate;
					type.GracePeriod = typeVM.GracePeriod;
					type.LumpSum = typeVM.LumpSum;
					type.OtherCharge = typeVM.OtherCharge;
					type.PenaltyDefaulter = typeVM.PenaltyDefaulter;                    
					type.ProjectId = typeVM.ProjectId;

					db.Entry(type).State = EntityState.Added;
					db.SaveChanges();

					typeVM.Id = type.Id;
				}
				else
				{
					var oldType = db.ProjectPropertyCharges.Where(t => t.Id == typeVM.Id)
									.SingleOrDefault();

					oldType.BasicQuateRate = typeVM.BasicQuateRate;
					oldType.DevelopmentCharge = typeVM.DevelopmentCharge;
					oldType.FloorNumberOnWord = typeVM.FloorNumberOnWord;
					oldType.FloorRiseRate = typeVM.FloorRiseRate;
					oldType.GracePeriod = typeVM.GracePeriod;
					oldType.LumpSum = typeVM.LumpSum;
					oldType.OtherCharge = typeVM.OtherCharge;
					oldType.PenaltyDefaulter = typeVM.PenaltyDefaulter;
					oldType.ProjectId = typeVM.ProjectId;

					db.Entry(oldType).State = EntityState.Modified;
					db.SaveChanges();

					var itemsToDelete = db.ProjectParkingTypes.Where(x => x.ProjectId == typeVM.ProjectId);
					db.ProjectParkingTypes.RemoveRange(itemsToDelete);
					db.SaveChanges();

				}

				if (typeVM.Parkings.Count > 0)
				{
					var projectParkings = new List<ProjectParkingType>();

					foreach (var item in typeVM.Parkings)
					{
						projectParkings.Add(new ProjectParkingType
						{
							Type = item.Type,
							Count = item.Count,
							ProjectId = typeVM.ProjectId
						});
					}

					db.ProjectParkingTypes.AddRange(projectParkings);
					db.SaveChanges();
				}


			}
			catch (DbUpdateConcurrencyException)
			{
				if (db.ProjectPropertyCharges
					.Count(e => e.Id == id) > 0)
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
		
		#endregion


		#region Project Bank Details

		[Route("saveProjectBankDetails")]
		[ResponseType(typeof(ProjectBankDetailsViewModel))]
		public IHttpActionResult SaveProjectBankDetails(ProjectBankDetailsViewModel typeVM)
		{


			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var type = new ProjectBankDetails();

			try
			{
				if (typeVM.Id == 0)
				{
					type.AccountNumber = typeVM.AccountNumber;
					type.BankName = typeVM.BankName;
					type.BranchName = typeVM.BranchName;
					type.ProjectId = typeVM.ProjectId;

					db.Entry(type).State = EntityState.Added;
					db.SaveChanges();

					typeVM.Id = type.Id;
				}
				else
				{
					var oldType = db.ProjectBankDetails
									.Where(t => t.Id == typeVM.Id)
									.SingleOrDefault();

					oldType.AccountNumber = typeVM.AccountNumber;
					oldType.BankName = typeVM.BankName;
					oldType.BranchName = typeVM.BranchName;
					oldType.ProjectId = typeVM.ProjectId;
					oldType.ProjectId = typeVM.ProjectId;

					db.Entry(oldType).State = EntityState.Modified;
					db.SaveChanges();
					
				}
			}
			catch (DbUpdateConcurrencyException)
			{
				if (db.ProjectBankDetails
					.Count(e => e.Id == typeVM.Id) > 0)
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

		#endregion


		#region Project By Id

		[Route("projects/getProjectById/{id}")]
		[ResponseType(typeof(ProjectDetailViewModel))]
		public IHttpActionResult GetProjectById(int id)
		{          
			if (id < 1)
			{
				return BadRequest();
			}

			var project = new ProjectDetailViewModel();

			project.ProjectBase =   db.Projects.Where(x => x.Id == id)
												.Select(x => new ProjectBaseViewModel
												{
													Id = x.Id,
													ProjectId = x.ProjectId,
													ProjectName = x.ProjectName,
													ProjectTypeId = x.ProjectTypeId
												})
												.SingleOrDefault();

			if(project.ProjectBase == null)
			{
				return BadRequest();
			}

			project.ProjectBase.ProjectType = db.ProjectTypes.Where(x => x.Id == project.ProjectBase.ProjectTypeId)
															 .Select(x => new ProjectTypeViewModel
																{
																	Id = x.Id,
																	Name = x.Name

																}).SingleOrDefault();



			project.ProjectInformation = db.ProjectInformation.Where(x => x.Id == id)
											.Select(x => new ProjectOtherInformationViewModel
											{
												Id = x.Id,
												Address = x.Address,
												Amneties = x.Amneties,
												City = x.City,
												CompanyId = x.CompanyId,
												FloorPlan = x.FloorPlan,
												FSI = x.FSI,
												Locality = x.Locality,
												NumberOfBuilding = x.NumberOfBuilding,
												NumberOfFlats = x.NumberOfFlats,
												NumberOfOffices = x.NumberOfOffices,
												NumberOfShops = x.NumberOfShops,
												PlotArea = x.PlotArea,
												PlotAreaUnit = x.PlotAreaUnit,
												PlotNumber = x.PlotNumber,
												ProjectId = x.ProjectId ,
												SurveyOrSectorNumber = x.SurveyOrSectorNumber                                                
											}).SingleOrDefault();


			project.ProjectInformation.Company = db.Companies.Where(x => x.Id == project.ProjectInformation.CompanyId)
												   .Select(t => new CreateCompanyViewModel
												   {
														Name = t.Name,
														Id = t.Id
													}).SingleOrDefault();

			project.ProjectInformation.Developers = db.ProjectDevelopers.Where(x => x.ProjectOtherInformationId == project.ProjectInformation.Id)
													  .Select(x => new ProjectDeveloeperViewModel
															{
																DeveloperId = x.DeveloperId,
																DeveloperName = x.DeveloperName
															}).ToList();

				
			project.PropertyCharges = db.ProjectPropertyCharges.Where(x => x.ProjectId == id)
										.Select(t => new ProjectPropertyChargesViewModel
												{
													BasicQuateRate = t.BasicQuateRate,
													DevelopmentCharge = t.DevelopmentCharge,
													FloorNumberOnWord = t.FloorNumberOnWord,
													FloorRiseRate = t.FloorRiseRate,
													GracePeriod = t.GracePeriod,
													Id = t.Id,
													LumpSum = t.LumpSum,
													OtherCharge = t.OtherCharge,
													PenaltyDefaulter = t.PenaltyDefaulter,
													ProjectId = t.ProjectId
												}).SingleOrDefault();

			project.PropertyCharges.Parkings = db.ProjectParkingTypes.Where(x => x.ProjectId == id)
												 .Select(x => new ProjectParkingTypeViewModel
												 {
													 Id = x.Id,
													 Type = x.Type,
													 Count = x.Count,
													 ProjectId = x.ProjectId
												 }).ToList();


			project.BankDetails = db.ProjectBankDetails.Where(x => x.ProjectId == id)
									.Select(t => new ProjectBankDetailsViewModel
											{
												AccountNumber = t.AccountNumber,
												BankName = t.BankName,
												BranchName = t.BranchName,
												Id = t.Id,
												ProjectId = t.ProjectId
											}).ToList();

			return Ok(project);
		}


		[Route("projects/getBuildingNumbersFromProjectById/{id}")]
		[ResponseType(typeof(int))]
		public IHttpActionResult GetBuildingNumbersFromProjectById(int id)
		{
			if (id < 1)
			{
				return BadRequest();
			}

			return Ok( db.ProjectInformation.Where(x => x.ProjectId == id)
						 .Select(x => x.NumberOfBuilding).SingleOrDefault());
		}

		[Route("projects/getAllProjects")]
		[ResponseType(typeof(List<ProjectNameViewModel>))]
		public IHttpActionResult GetAllProjects()
		{
			return Ok(db.Projects.Select(x => new ProjectNameViewModel { Id = x.Id, Name=x.ProjectName}).ToList());
		}


	   #endregion

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