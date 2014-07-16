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
    public class ManageUnitTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ManagePermissions

        [Route("unitTypes/{page}/size/{pageSize}/search/{searchQuery}")]
        public ListCutomerTypeViewModel GetUnitType(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var query = db.UnitTypes.OrderByDescending(x => x.Id);

            return new ListCutomerTypeViewModel
            {

                TotalItems = query.Count(),
                Results = query.Select(t => new CreateTypeViewModel 
                                                { 
                                                     Id = t.Id,
                                                     Name = t.Name
                                                }).Skip(pageSize * (page - 1))
                                                  .Take(pageSize).ToList()

            };

        }

        [Route("unitTypes/all")]
        public ListCutomerTypeViewModel GetAllUnitTypes()
        {
            var query = db.UnitTypes.OrderByDescending(x => x.Id);

            return new ListCutomerTypeViewModel
            {

                TotalItems = 0,
                Results = query.Select(t => new CreateTypeViewModel
                                                {
                                                    Id = t.Id,
                                                    Name = t.Name
                                                }).ToList()
            
            };

        }


        [Route("unitTypeIsUnique/{searchQuery}")]
        public bool GetUnitTypes(string searchQuery = "")
        {
            return db.UnitTypes.Any(x => x.Name.Contains(searchQuery)); 
        }


        [Route("saveUnitType/{id}")]
        [ResponseType(typeof(CreateTypeViewModel))]
        public IHttpActionResult PostUnitType(int id, CreateTypeViewModel typeVM)
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
                var type = new UnitType();
                type.Name = typeVM.Name;
                type.UpdatedByUserId = typeVM.UserId;
                db.Entry(type).State = EntityState.Added;
            }
            else
            {
                var type = db.UnitTypes.Where(e => e.Id == id).SingleOrDefault();

                type.Name = typeVM.Name;
                type.UpdatedByUserId = typeVM.UserId;

                db.Entry(type).State = EntityState.Modified;    
            }
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.UnitTypes.Count(e => e.Id == id) > 0)
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