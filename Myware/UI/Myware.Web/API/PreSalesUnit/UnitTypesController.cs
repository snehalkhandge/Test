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
    builder.EntitySet<UnitType>("UnitTypes");
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class UnitTypesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/UnitTypes
        [Queryable]
        public IQueryable<UnitType> GetUnitTypes()
        {
            return db.UnitTypes;
        }

        // GET: odata/UnitTypes(5)
        [Queryable]
        public SingleResult<UnitType> GetUnitType([FromODataUri] int key)
        {
            return SingleResult.Create(db.UnitTypes.Where(unitType => unitType.Id == key));
        }

        // PUT: odata/UnitTypes(5)
        public IHttpActionResult Put([FromODataUri] int key, UnitType unitType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != unitType.Id)
            {
                return BadRequest();
            }

            db.Entry(unitType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(unitType);
        }

        // POST: odata/UnitTypes
        public IHttpActionResult Post(UnitType unitType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UnitTypes.Add(unitType);
            db.SaveChanges();

            return Created(unitType);
        }

        // PATCH: odata/UnitTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<UnitType> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UnitType unitType = db.UnitTypes.Find(key);
            if (unitType == null)
            {
                return NotFound();
            }

            patch.Patch(unitType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(unitType);
        }

        // DELETE: odata/UnitTypes(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            UnitType unitType = db.UnitTypes.Find(key);
            if (unitType == null)
            {
                return NotFound();
            }

            db.UnitTypes.Remove(unitType);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/UnitTypes(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.UnitTypes.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UnitTypeExists(int key)
        {
            return db.UnitTypes.Count(e => e.Id == key) > 0;
        }
    }
}
