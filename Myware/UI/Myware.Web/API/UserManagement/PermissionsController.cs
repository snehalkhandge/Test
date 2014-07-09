using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Myware.Data.Entity;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Web.API.UserManagement
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using Myware.Data.Entity.Models.UserManagement;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Permission>("Permissions");
    builder.EntitySet<RolePermissions>("RolePermissions"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PermissionsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Permissions
        [Queryable]
        public IQueryable<Permission> GetPermissions()
        {
            return db.Permissions;
        }

        // GET: odata/Permissions(5)
        [Queryable]
        public SingleResult<Permission> GetPermission([FromODataUri] int key)
        {
            return SingleResult.Create(db.Permissions.Where(permission => permission.Id == key));
        }

        // PUT: odata/Permissions(5)
        public IHttpActionResult Put([FromODataUri] int key, Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != permission.Id)
            {
                return BadRequest();
            }

            db.Entry(permission).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(permission);
        }

        // POST: odata/Permissions
        public IHttpActionResult Post(Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Permissions.Add(permission);
            db.SaveChanges();

            return Created(permission);
        }

        // PATCH: odata/Permissions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Permission> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Permission permission = db.Permissions.Find(key);
            if (permission == null)
            {
                return NotFound();
            }

            patch.Patch(permission);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(permission);
        }

        // DELETE: odata/Permissions(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Permission permission = db.Permissions.Find(key);
            if (permission == null)
            {
                return NotFound();
            }

            db.Permissions.Remove(permission);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Permissions(5)/RolePermissions
        [Queryable]
        public IQueryable<RolePermissions> GetRolePermissions([FromODataUri] int key)
        {
            return db.Permissions.Where(m => m.Id == key).SelectMany(m => m.RolePermissions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermissionExists(int key)
        {
            return db.Permissions.Count(e => e.Id == key) > 0;
        }
    }
}
