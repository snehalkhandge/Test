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
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Web.API.PreSalesUnit
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using Myware.Data.Entity.Models.PresalesUnit;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Developer>("Developers");
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DevelopersController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Developers
        [Queryable]
        public IQueryable<Developer> GetDevelopers()
        {
            return db.Developers;
        }

        // GET: odata/Developers(5)
        [Queryable]
        public SingleResult<Developer> GetDeveloper([FromODataUri] int key)
        {
            return SingleResult.Create(db.Developers.Where(developer => developer.Id == key));
        }

        // PUT: odata/Developers(5)
        public IHttpActionResult Put([FromODataUri] int key, Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != developer.Id)
            {
                return BadRequest();
            }

            db.Entry(developer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(developer);
        }

        // POST: odata/Developers
        public IHttpActionResult Post(Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Developers.Add(developer);
            db.SaveChanges();

            return Created(developer);
        }

        // PATCH: odata/Developers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Developer> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Developer developer = db.Developers.Find(key);
            if (developer == null)
            {
                return NotFound();
            }

            patch.Patch(developer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(developer);
        }

        // DELETE: odata/Developers(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Developer developer = db.Developers.Find(key);
            if (developer == null)
            {
                return NotFound();
            }

            db.Developers.Remove(developer);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Developers(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Developers.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeveloperExists(int key)
        {
            return db.Developers.Count(e => e.Id == key) > 0;
        }
    }
}
