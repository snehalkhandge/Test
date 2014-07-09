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
using Myware.Data.Entity.Models.PreSales;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Web.API.PreSalesUnit
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using Myware.Data.Entity.Models.PresalesUnit;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class FacingTypesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/FacingTypes
        [Queryable]
        public IQueryable<FacingType> GetFacingTypes()
        {
            return db.FacingTypes;
        }

        // GET: odata/FacingTypes(5)
        [Queryable]
        public SingleResult<FacingType> GetFacingType([FromODataUri] int key)
        {
            return SingleResult.Create(db.FacingTypes.Where(facingType => facingType.Id == key));
        }

        // PUT: odata/FacingTypes(5)
        public IHttpActionResult Put([FromODataUri] int key, FacingType facingType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != facingType.Id)
            {
                return BadRequest();
            }

            db.Entry(facingType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacingTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(facingType);
        }

        // POST: odata/FacingTypes
        public IHttpActionResult Post(FacingType facingType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FacingTypes.Add(facingType);
            db.SaveChanges();

            return Created(facingType);
        }

        // PATCH: odata/FacingTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<FacingType> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FacingType facingType = db.FacingTypes.Find(key);
            if (facingType == null)
            {
                return NotFound();
            }

            patch.Patch(facingType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacingTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(facingType);
        }

        // DELETE: odata/FacingTypes(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            FacingType facingType = db.FacingTypes.Find(key);
            if (facingType == null)
            {
                return NotFound();
            }

            db.FacingTypes.Remove(facingType);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/FacingTypes(5)/ContactEnquiries
        [Queryable]
        public IQueryable<ContactEnquiry> GetContactEnquiries([FromODataUri] int key)
        {
            return db.FacingTypes.Where(m => m.Id == key).SelectMany(m => m.ContactEnquiries);
        }

        // GET: odata/FacingTypes(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.FacingTypes.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FacingTypeExists(int key)
        {
            return db.FacingTypes.Count(e => e.Id == key) > 0;
        }
    }
}
