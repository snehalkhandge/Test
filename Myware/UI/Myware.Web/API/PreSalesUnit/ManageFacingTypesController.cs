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
    public class ManageFacingTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ManagePermissions

        [Route("facingTypes/{page}/size/{pageSize}/search/{searchQuery}")]
        public ListCutomerTypeViewModel GetFacingType(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var query = db.FacingTypes.OrderByDescending(x => x.Id);

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

        [Route("facingTypes/all")]
        public ListCutomerTypeViewModel GetAllFacingTypes()
        {
            var query = db.FacingTypes.OrderByDescending(x => x.Id);

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


        [Route("facingTypeIsUnique/{searchQuery}")]
        public bool GetFacingTypes(string searchQuery = "")
        {
            return db.FacingTypes.Any(x => x.Name.Contains(searchQuery)); 
        }


        [Route("saveFacingType/{id}")]
        [ResponseType(typeof(CreateTypeViewModel))]
        public IHttpActionResult PostFacingType(int id, CreateTypeViewModel typeVM)
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
                var type = new FacingType();
                type.Name = typeVM.Name;
                type.UpdatedByUserId = typeVM.UserId;
                db.Entry(type).State = EntityState.Added;
            }
            else
            {
                var type = db.FacingTypes.Where(e => e.Id == id).SingleOrDefault();

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
                if (db.FacingTypes.Count(e => e.Id == id) > 0)
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