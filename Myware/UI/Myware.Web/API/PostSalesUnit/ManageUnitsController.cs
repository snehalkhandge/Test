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
	public class ManageUnitsController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		#region Search Units
		[Route("listUnits")]
		[HttpPost]
		public ListUnitViewModel PostProjects(ListUnitViewModel searchQuery)
		{
			var query = db.Units.Include(t => t.Project).Include(t => t.Wing).Include(t => t.Tower);

			if(searchQuery.ProjectId != 0)
			{
				query = query.Where(t => t.ProjectId == searchQuery.ProjectId);
			}

			query = query.OrderByDescending(x => x.Id);
			var total = query.Count();
			var result = query.Skip(searchQuery.PageSize * (searchQuery.Page - 1))
							  .Take(searchQuery.PageSize).ToList();


			var listResult = new ListUnitViewModel();
			listResult.TotalItems = total;

			listResult.Results = new List<UnitDetailViewModel>();

			foreach (var item in result)
			{

				item.Tower = db.Towers.Where(t => t.Id == item.TowerId).SingleOrDefault();
				var tower = new TowerDetailViewModel();
				if(item.Tower != null)
				{
					tower = new TowerDetailViewModel
					{
						Id = item.Tower.Id,
						BuildingNumber = item.Tower.BuildingNumber,
						BuildingName = item.Tower.BuildingName,
						NumberOfWings = item.Tower.NumberOfWings

					};

				}


				item.Project = db.Projects.Where(t => t.Id == item.ProjectId).SingleOrDefault();

				var project = new ProjectBaseViewModel();
				if (item.Project != null)
				{
					project = new ProjectBaseViewModel
								{
									Id = item.Project.Id,
									ProjectId = item.Project.ProjectId,
									ProjectName = item.Project.ProjectName
								};
				}


				item.Wing = db.Wings.Where(t => t.Id == item.WingId).SingleOrDefault();

				var wing = new WingDetailViewModel();
				if (item.Wing != null)
				{
					wing = new WingDetailViewModel
							{
								Id = item.Wing.Id,
								WingNumber = item.Wing.WingNumber,
								WingName = item.Wing.WingName,
								NumberOfFloors = item.Wing.NumberOfFloors
							};
				}

				listResult.Results.Add(
					new UnitDetailViewModel
					{
						Id = item.Id,
						BasicRate = item.BasicRate,
						BuildingType = item.BuildingType,
						CarpetArea = item.CarpetArea,
						CarpetAreaUnit = item.CarpetAreaUnit,
						DevelopmentCharge = item.DevelopmentCharge,
						FloorNumber = item.FloorNumber,
						FloorRiseRate = item.FloorRiseRate,
						OtherCharge = item.OtherCharge,
						Project = project,
						ProjectId = item.ProjectId,
						SaleableArea = item.SaleableArea,
						SaleableAreaUnit = item.SaleableAreaUnit,
						Status = item.Status,
						Tower = tower,
						TowerId = item.TowerId,
						UnitName = item.UnitName,
						UnitNumber = item.UnitNumber,
						UnitType = item.UnitType,
						Wing = wing,
						WingId = item.WingId

					}
				);

			}

			return listResult;

		}

		[Route("getUnitById/{id}")]
		public UnitDetailViewModel GetUnitById(int id)
		{

			var results = db.Units.Include(t => t.Project)
							.Include(t => t.Wing)
							.Include(t => t.Tower)
							.Where(t => t.Id == id)
							.Select(item => new UnitDetailViewModel
							{
								Id = item.Id,
								BasicRate = item.BasicRate,
								BuildingType = item.BuildingType,
								CarpetArea = item.CarpetArea,
								CarpetAreaUnit = item.CarpetAreaUnit,
								DevelopmentCharge = item.DevelopmentCharge,
								FloorNumber = item.FloorNumber,
								FloorRiseRate = item.FloorRiseRate,
								OtherCharge = item.OtherCharge,
								Project = new ProjectBaseViewModel
								{
									Id = item.Project.Id,
									ProjectId = item.Project.ProjectId,
									ProjectName = item.Project.ProjectName
								},
								ProjectId = item.ProjectId,
								SaleableArea = item.SaleableArea,
								SaleableAreaUnit = item.SaleableAreaUnit,
								Status = item.Status,
								Tower = new TowerDetailViewModel
								{
									Id = item.Tower.Id,
									BuildingNumber = item.Tower.BuildingNumber,
									BuildingName = item.Tower.BuildingName,
									NumberOfWings = item.Tower.NumberOfWings

								},
								TowerId = item.TowerId,
								UnitName = item.UnitName,
								UnitNumber = item.UnitNumber,
								UnitType = item.UnitType,
								Wing = new WingDetailViewModel
								{
									Id = item.Wing.Id,
									WingNumber = item.Wing.WingNumber,
									WingName = item.Wing.WingName,
									NumberOfFloors = item.Wing.NumberOfFloors
								},
								WingId = item.WingId

							}).SingleOrDefault();

			return results;			
		
		}

		#endregion


		#region Create Edit Project Base
				
		[Route("saveProjectUnit/{id}")]
		[ResponseType(typeof(UnitDetailViewModel))]
		public IHttpActionResult SavePostTower(int id, UnitDetailViewModel typeVM)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			if (id != typeVM.Id)
			{
				return BadRequest();
			}

			var type = new Unit();

			try
			{
				if (typeVM.Id == 0)
				{
					type.BasicRate = typeVM.BasicRate;                     
					type.BuildingType = typeVM.BuildingType;
					type.CarpetArea = typeVM.CarpetArea;
					type.CarpetAreaUnit = typeVM.CarpetAreaUnit;
					type.DevelopmentCharge = typeVM.DevelopmentCharge;
					type.FloorNumber = typeVM.FloorNumber;
					type.FloorRiseRate = typeVM.FloorRiseRate;
					type.OtherCharge = typeVM.OtherCharge;                                
					type.ProjectId = typeVM.ProjectId;
					type.SaleableArea = typeVM.SaleableArea;
					type.SaleableAreaUnit = typeVM.SaleableAreaUnit;
					type.Status = typeVM.Status;                                
					type.TowerId = typeVM.TowerId;
					type.UnitName = typeVM.UnitName;
					type.UnitNumber = typeVM.UnitNumber;
					type.UnitType = typeVM.UnitType;                                
					type.WingId = typeVM.WingId;

					db.Entry(type).State = EntityState.Added;
					db.SaveChanges();

					typeVM.Id = type.Id;
				}
				else
				{
					var oldType = db.Units.Where(t => t.Id == typeVM.Id).SingleOrDefault();

					oldType.BasicRate = typeVM.BasicRate;
					oldType.BuildingType = typeVM.BuildingType;
					oldType.CarpetArea = typeVM.CarpetArea;
					oldType.CarpetAreaUnit = typeVM.CarpetAreaUnit;
					oldType.DevelopmentCharge = typeVM.DevelopmentCharge;
					oldType.FloorNumber = typeVM.FloorNumber;
					oldType.FloorRiseRate = typeVM.FloorRiseRate;
					oldType.OtherCharge = typeVM.OtherCharge;
					oldType.ProjectId = typeVM.ProjectId;
					oldType.SaleableArea = typeVM.SaleableArea;
					oldType.SaleableAreaUnit = typeVM.SaleableAreaUnit;
					oldType.Status = typeVM.Status;
					oldType.TowerId = typeVM.TowerId;
					oldType.UnitName = typeVM.UnitName;
					oldType.UnitNumber = typeVM.UnitNumber;
					oldType.UnitType = typeVM.UnitType;
					oldType.WingId = typeVM.WingId;

					db.Entry(oldType).State = EntityState.Modified;
					db.SaveChanges();

				}

			}
			catch (DbUpdateConcurrencyException)
			{
				if (db.Towers.Count(e => e.Id == typeVM.Id) > 0)
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


		[Route("projects/getAllUnitsByTowerId/{id}")]
		[ResponseType(typeof(List<ProjectNameViewModel>))]
		public IHttpActionResult GetAllUnitsByTowerId(int id)
		{
			return Ok(db.Units.Where(x => x.TowerId == id).Select(x => new ProjectNameViewModel { Id = x.Id, Name = x.UnitName }).ToList());
		}

		[Route("projects/getAllUnitsByWingId/{id}")]
		[ResponseType(typeof(List<ProjectNameViewModel>))]
		public IHttpActionResult GetAllUnitsByWingId(int id)
		{
			return Ok(db.Units.Where(x => x.WingId == id).Select(x => new ProjectNameViewModel { Id = x.Id, Name = x.UnitName }).ToList());
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