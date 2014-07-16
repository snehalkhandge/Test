using Myware.Data.Entity;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Web.Models;
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
    public class ManagePermissionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ManagePermissions

        [Route("permissions/{page}/size/{pageSize}/search/{searchQuery}")]
        public List<PermissionViewModel> GetPermissions(int page = 1, int pageSize = 10, string searchQuery="")
        {
            var permissions = new List<PermissionViewModel>();

            var query = db.Permissions.OrderByDescending(x => x.Id); ;
            permissions.Add(new PermissionViewModel
            {
                TotalItems = query.Count(),
                Results = query.Skip(pageSize * (page-1))
                                    .Take(pageSize).ToList()
            });

            return permissions;
        }

        [Route("permissions/all")]
        public ListPartialPermissionViewModel GetAllPermissions()
        {
            
            
            var query = db.Permissions.OrderByDescending(x => x.Id);

            return  new ListPartialPermissionViewModel {

                TotalItems = 0,
                Results = query.Select(t => new PartialPermissionViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList()
            
            };

        }


        [Route("permissionsIsUnique/{searchQuery}")]
        public bool GetPermissions(string searchQuery = "")
        {
            return db.Permissions.Any(x => x.Name.Contains(searchQuery)); 
        }


        [Route("savePermission/{id}")]
        [ResponseType(typeof(CreatePermissionViewModel))]
        public IHttpActionResult PostPermission(int id, CreatePermissionViewModel permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permission.Id)
            {
                return BadRequest();
            }
            var perm = new Permission();

            perm.Name = permission.Name;
            

            if (permission.Id == 0)
            {
                db.Entry(perm).State = EntityState.Added;
            }
            else
            {
                perm.Id = permission.Id;
                db.Entry(perm).State = EntityState.Modified;    
            }
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(permission);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermissionExists(int id)
        {
            return db.Permissions.Count(e => e.Id == id) > 0;
        }
    }
}