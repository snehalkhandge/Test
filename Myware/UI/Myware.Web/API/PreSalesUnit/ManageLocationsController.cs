using Myware.Data.Entity;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Web.Models;
using Myware.Web.Models.PreSalesUnit;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Myware.Web.API.UserManagement
{
    [Authorize]
    public class ManageLocationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ManagePermissions

        [Route("locations/{page}/size/{pageSize}/search/{searchQuery}")]
        public ListLocationTypeViewModel GetUnitType(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var query = db.Locations.OrderByDescending(x => x.Id);

            return new ListLocationTypeViewModel
            {

                TotalItems = query.Count(),
                Results = query.Select(t => new CreateLocationViewModel 
                                                { 
                                                     Id = t.Id,
                                                     City = t.City,
                                                     State = t.State,
                                                     Country = t.Country

                                                }).Skip(pageSize * (page - 1))
                                                  .Take(pageSize).ToList()

            };

        }

        [Route("locations/all")]
        public ListLocationTypeViewModel GetAllLocations()
        {
            var query = db.Locations.OrderByDescending(x => x.Id);

            return new ListLocationTypeViewModel
            {

                TotalItems = 0,
                Results = query.Select(t => new CreateLocationViewModel
                                                {
                                                    Id = t.Id,
                                                    City = t.City,
                                                    State = t.State,
                                                    Country = t.Country
                                                }).ToList()
            
            };

        }


        [Route("cityIsUnique/{searchQuery}")]
        public bool GetLocations(string searchQuery = "")
        {
            return db.Locations.Any(x => x.City.Contains(searchQuery)); 
        }


        [Route("saveLocation/{id}")]
        [ResponseType(typeof(CreateLocationViewModel))]
        public IHttpActionResult PostUnitType(int id, CreateLocationViewModel typeVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeVM.Id)
            {
                return BadRequest();
            }
            
            if (typeVM.Id == 0)
            {
                var type = new Location();                
                type.City = typeVM.City;
                type.State = typeVM.State;
                type.Country = typeVM.Country;
                type.UpdatedByUserId = typeVM.UserId;
                db.Entry(type).State = EntityState.Added;
            }
            else
            {
                var type = db.Locations.Where(e => e.Id == id).SingleOrDefault();

                type.City = typeVM.City;
                type.State = typeVM.State;
                type.Country = typeVM.Country;
                type.UpdatedByUserId = typeVM.UserId;

                db.Entry(type).State = EntityState.Modified;    
            }
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Locations.Count(e => e.Id == id) > 0)
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