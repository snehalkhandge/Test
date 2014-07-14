using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Myware.Data.Entity;
using Myware.Data.Entity.CustomStores;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Web.Models;

namespace Myware.Web.API.UserManagement
{
    [Authorize]
    public class ManageRolesController : ApiController
    {
        public ManageRolesController()
            : this(new AppRoleManager(new AppRoleStore(new ApplicationDbContext())))
        {
        }

        public ManageRolesController(AppRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        private AppRoleManager _roleManager { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ManageRoles
        public IQueryable<RoleViewModel> GetRoles()
        {
            return _roleManager.Roles.Include(b => b.RolePermissions.Select(p => p.Permission))
                               .Select(t => new RoleViewModel
                                 {
                                     Id = t.Id,
                                     Name = t.Name,
                                     RolePermissions = t.RolePermissions.Select(b=> new RolePermissionViewModel
                                     {
                                         Id = b.Id,
                                         RoleId = b.RoleId,
                                         PermissionId = b.PermissionId,
                                         Permission = b.Permission
                                     }).ToList()
                                 });



        }

        // GET: api/ManageRoles/5
        [ResponseType(typeof(RoleViewModel))]
        public IHttpActionResult GetRole(int id)
        {
            RoleViewModel role = _roleManager.Roles.Where(t => t.Id == id).Include(b => b.RolePermissions.Select(p => p.Permission))
                                       .Select(t => new RoleViewModel
                                       {
                                           Id = t.Id,
                                           Name = t.Name,
                                           RolePermissions = t.RolePermissions.Select(b => new RolePermissionViewModel
                                           {
                                               Id = b.Id,
                                               RoleId = b.RoleId,
                                               PermissionId = b.PermissionId,
                                               Permission = b.Permission
                                           }).ToList()
                                       }).SingleOrDefault();
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/ManageRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRole(int id, RoleViewModel roleVM)
        {

            var role = new Role();

            role.Id = roleVM.Id;
            role.Name = roleVM.Name;
            foreach (var perm in roleVM.RolePermissions)
            {
                role.RolePermissions.Add(new RolePermissions
                {
                    
                });
            }



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.Id)
            {
                return BadRequest();
            }

            db.Entry(role).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ManageRoles
        [ResponseType(typeof(Role))]
        public IHttpActionResult PostRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Roles.Add(role);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = role.Id }, role);
        }
        
        // DELETE: api/ManageUsers/5
        [ResponseType(typeof(RolePermissions))]
        public IHttpActionResult DeleteRolePermission(int id)
        {
            var permission = db.RolePermissions.Find(id);
            if (permission == null)
            {
                return NotFound();
            }

            db.RolePermissions.Remove(permission);
            db.SaveChanges();

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

        private bool RoleExists(int id)
        {
            return db.Roles.Count(e => e.Id == id) > 0;
        }
    }
}