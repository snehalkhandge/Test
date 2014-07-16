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
    public class ManageLocalityController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ManagePermissions

        [Route("locality/{page}/size/{pageSize}/search/{searchQuery}")]
        public ListLocalityTypeViewModel GetUnitType(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var query = db.Localities.Include(t => t.Location).OrderByDescending(x => x.Id);

            return new ListLocalityTypeViewModel
            {

                TotalItems = query.Count(),
                Results = query.Select(t => new LocalityViewModel 
                                                { 
                                                     Id = t.Id,
                                                     Name = t.Name,
                                                     Location = t.Location,
                                                     LocationId = t.LocationId
                                                }).Skip(pageSize * (page - 1))
                                                  .Take(pageSize).ToList()

            };

        }

        [Route("locality/all")]
        public ListLocalityTypeViewModel GetAllLocality()
        {
            var query = db.Localities.Include(t => t.Location).OrderByDescending(x => x.Id);

            return new ListLocalityTypeViewModel
            {

                TotalItems = 0,
                Results = query.Select(t => new LocalityViewModel
                                                {
                                                    Id = t.Id,
                                                    Name = t.Name,
                                                    Location = t.Location,
                                                    LocationId = t.LocationId
                                                }).ToList()
            
            };

        }


        [Route("saveLocality/{id}")]
        [ResponseType(typeof(CreateLocalityViewModel))]
        public IHttpActionResult PostLocality(int id, CreateLocalityViewModel typeVM)
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
                var type = new Locality();
                type.Name = typeVM.Name;

                type.LocationId = typeVM.LocationId;
                type.UpdatedByUserId = typeVM.UserId;
                db.Entry(type).State = EntityState.Added;
            }
            else
            {
                var type = db.Localities.Where(e => e.Id == id).SingleOrDefault();

                type.Name = typeVM.Name;
                type.UpdatedByUserId = typeVM.UserId;
                type.LocationId = typeVM.LocationId;

                db.Entry(type).State = EntityState.Modified;    
            }
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Localities.Count(e => e.Id == id) > 0)
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