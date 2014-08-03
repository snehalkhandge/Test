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
	public class ManageWingsController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		#region Search Projects
		[Route("listWings")]
		[HttpPost]
		public ListWingViewModel PostWings(ListWingViewModel searchQuery)
		{
			var query = db.Wings.Include(t => t.Project).Include(t => t.Tower);

			if(searchQuery.ProjectId != 0)
			{
				query = query.Where(t => t.ProjectId == searchQuery.ProjectId);
			}

			if (searchQuery.TowerId != 0)
			{
				query = query.Where(t => t.TowerId == searchQuery.TowerId);
			}
			query = query.OrderByDescending(x => x.Id);
			var total = query.Count();

			/*
			 .Select(t => new Wing
								{ 
									Id = t.Id,
									NumberOfFloors = t.NumberOfFloors,
									Project = t.Project,
									Tower = t.Tower,
									ProjectId = t.ProjectId,
									TowerId = t.TowerId,
									WingName = t.WingName,
									WingNumber = t.WingNumber
								})
			 */


			var result = query.Skip(searchQuery.PageSize * (searchQuery.Page - 1))
							  .Take(searchQuery.PageSize).ToList();


			var listResult = new ListWingViewModel();
			listResult.TotalItems = total;

			foreach (var item in result)
			{
				var tower = new TowerDetailViewModel { 
					BuildingName = item.Tower.BuildingName,
					BuildingNumber = item.Tower.BuildingNumber,
					Id = item.Tower.Id,
					NumberOfWings = item.Tower.NumberOfWings                    
				};
				
				var project = new ProjectBaseViewModel
				{
					Id = item.Project.Id,
					ProjectId = item.Project.ProjectId,
					ProjectName = item.Project.ProjectName
				};

				listResult.Results.Add(new WingDetailViewModel
				{
					Id = item.Id,
					NumberOfFloors = item.NumberOfFloors,
					Project = project,
					ProjectId = item.ProjectId,
					Tower = tower,
					TowerId = item.TowerId,
					WingName = item.WingName,
					WingNumber = item.WingNumber

				});

			}

			

			return listResult;

		}

		[Route("getWingById/{id}")]
		public WingDetailViewModel GetWingById(int id)
		{
			var query = db.Wings.Include(t => t.Project).Include(t => t.Tower).SingleOrDefault();

			var tower = new TowerDetailViewModel
			{
				BuildingName = query.Tower.BuildingName,
				BuildingNumber = query.Tower.BuildingNumber,
				Id = query.Tower.Id,
				NumberOfWings = query.Tower.NumberOfWings
			};

			var project = new ProjectBaseViewModel
			{
				Id = query.Project.Id,
				ProjectId = query.Project.ProjectId,
				ProjectName = query.Project.ProjectName
			};

			return new WingDetailViewModel
						{
							Id = query.Id,
							NumberOfFloors = query.NumberOfFloors,
							Project = project,
							ProjectId = query.ProjectId,
							Tower = tower,
							TowerId = query.TowerId,
							WingName = query.WingName,
							WingNumber = query.WingNumber

						};
			
		
		}
			

		#endregion


		#region Create Edit Project Base
				
		[Route("saveProjectWing/{id}")]
		[ResponseType(typeof(WingDetailViewModel))]
		public IHttpActionResult SavePostWing(int id, WingDetailViewModel typeVM)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			if (id != typeVM.Id)
			{
				return BadRequest();
			}

			var type = new Wing();

			try
			{
				if (typeVM.Id == 0)
				{
					type.ProjectId = typeVM.ProjectId;
					type.NumberOfFloors = typeVM.NumberOfFloors;					
					type.TowerId = typeVM.TowerId;
					type.WingName = typeVM.WingName;
					type.WingNumber = typeVM.WingNumber;
					
					
					db.Entry(type).State = EntityState.Added;
					db.SaveChanges();

					typeVM.Id = type.Id;
				}
				else
				{
					var oldType = db.Wings.Where(t => t.Id == typeVM.Id).SingleOrDefault();

					oldType.ProjectId = typeVM.ProjectId;
					oldType.NumberOfFloors = typeVM.NumberOfFloors;
					oldType.ProjectId = typeVM.ProjectId;
					oldType.TowerId = typeVM.TowerId;
					oldType.WingName = typeVM.WingName;
					oldType.WingNumber = typeVM.WingNumber;
					
					
					db.Entry(oldType).State = EntityState.Modified;
					db.SaveChanges();

				}

			}
			catch (DbUpdateConcurrencyException)
			{
				if (db.Wings.Count(e => e.Id == typeVM.Id) > 0)
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


		[Route("projects/getAllWingsByBuildingId/{id}")]
		[ResponseType(typeof(List<ProjectNameViewModel>))]
		public IHttpActionResult GetAllTowers(int id)
		{
			return Ok(db.Wings.Where(t => t.TowerId == id).Select(x => new ProjectNameViewModel { Id = x.Id, Name = x.WingName }).ToList());
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