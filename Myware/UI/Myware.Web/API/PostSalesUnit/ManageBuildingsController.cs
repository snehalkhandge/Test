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
	public class ManageBuildingsController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		#region Search Projects
		[Route("listTowers")]
		[HttpPost]
		public ListTowerViewModel PostProjects(ListTowerViewModel searchQuery)
		{
			var query = db.Towers.Include(t => t.Project);

			if(searchQuery.ProjectId != 0)
			{
				query = query.Where(t => t.ProjectId == searchQuery.ProjectId);
			}

			query = query.OrderByDescending(x => x.Id);
			var total = query.Count();
			var result = query.Select(t => new TowerDetailViewModel { 
									Id = t.Id,
									BuildingName = t.BuildingName,
									BuildingNumber = t.BuildingNumber,
									NumberOfWings = t.NumberOfWings,
									ProjectId = t.ProjectId,
									ProjectName = t.Project.ProjectName
								}).Skip(searchQuery.PageSize * (searchQuery.Page - 1))
							  .Take(searchQuery.PageSize).ToList();


			var listResult = new ListTowerViewModel();
			listResult.TotalItems = total;
			listResult.Results = result;

			return listResult;

		}

		[Route("getTowerById/{id}")]
		public TowerDetailViewModel GetTowerById(int id)
		{

			var results = db.Towers.Include(t => t.Project)
							.Select(t => new TowerDetailViewModel
								{
									Id = t.Id,
									BuildingName = t.BuildingName,
									BuildingNumber = t.BuildingNumber,
									NumberOfWings = t.NumberOfWings,
									ProjectId = t.ProjectId,
									ProjectName = t.Project.ProjectName
								}).SingleOrDefault();

			return results;			
		
		}

		[Route("projects/getWingNumbersFromBuildingById/{id}")]
		[ResponseType(typeof(int))]
		public IHttpActionResult GetWingNumbersFromBuildingById(int id)
		{
			if (id < 1)
			{
				return BadRequest();
			}

			return Ok(db.Towers.Where(x => x.Id == id)
						.Select(x => x.NumberOfWings).SingleOrDefault());
		}

		#endregion


		#region Create Edit Project Base
				
		[Route("saveProjectTower/{id}")]
		[ResponseType(typeof(TowerDetailViewModel))]
		public IHttpActionResult SavePostTower(int id, TowerDetailViewModel typeVM)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			if (id != typeVM.Id)
			{
				return BadRequest();
			}

			var type = new Tower();

			try
			{
				if (typeVM.Id == 0)
				{
					type.ProjectId = typeVM.ProjectId;
					type.BuildingName = typeVM.BuildingName;
					type.BuildingNumber = typeVM.BuildingNumber;
					type.NumberOfWings = typeVM.NumberOfWings;
					
					db.Entry(type).State = EntityState.Added;
					db.SaveChanges();

					typeVM.Id = type.Id;
				}
				else
				{
					var oldType = db.Towers.Where(t => t.Id == typeVM.Id).SingleOrDefault();

					oldType.ProjectId = typeVM.ProjectId;
					oldType.BuildingName = typeVM.BuildingName;
					oldType.BuildingNumber = typeVM.BuildingNumber;
					oldType.NumberOfWings = typeVM.NumberOfWings;
					
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


		[Route("projects/getAllTowers")]
		[ResponseType(typeof(List<ProjectNameViewModel>))]
		public IHttpActionResult GetAllTowers()
		{
			return Ok(db.Towers.Select(x => new ProjectNameViewModel { Id = x.Id, Name = x.BuildingName }).ToList());
		}


        [Route("projects/getBuildingNamesByProjectId/{id}")]
        [ResponseType(typeof(List<ProjectNameViewModel>))]
        [HttpGet]
        public IHttpActionResult GetAllTowersById(int id)
        {
            return Ok(db.Towers.Where(t => t.ProjectId == id).Select(x => new ProjectNameViewModel { Id = x.Id, Name = x.BuildingName }).ToList());
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