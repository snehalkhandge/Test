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
    builder.EntitySet<LookingForType>("LookingForTypes");
    builder.EntitySet<ContactEnquiry>("ContactEnquiries"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LookingForTypesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/LookingForTypes
        [Queryable]
        public IQueryable<LookingForType> GetLookingForTypes()
        {
            return db.LookingForTypes;
        }

        // GET: odata/LookingForTypes(5)
        [Queryable]
        public SingleResult<LookingForType> GetLookingForType([FromODataUri] int key)
        {
            return SingleResult.Create(db.LookingForTypes.Where(lookingForType => lookingForType.Id == key));
        }

        // PUT: odata/LookingForTypes(5)
        public IHttpActionResult Put([FromODataUri] int key, LookingForType lookingForType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != lookingForType.Id)
            {
                return BadRequest();
            }

            db.Entry(lookingForType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookingForTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lookingForType);
        }

        // POST: odata/LookingForTypes
        public IHttpActionResult Post(LookingForType lookingForType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LookingForTypes.Add(lookingForType);
            db.SaveChanges();

            return Created(lookingForType);
        }

        // PATCH: odata/LookingForTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<LookingForType> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LookingForType lookingForType = db.LookingForTypes.Find(key);
            if (lookingForType == null)
            {
                return NotFound();
            }

            patch.Patch(lookingForType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookingForTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lookingForType);
        }

        // DELETE: odata/LookingForTypes(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            LookingForType lookingForType = db.LookingForTypes.Find(key);
            if (lookingForType == null)
            {
                return NotFound();
            }

            db.LookingForTypes.Remove(lookingForType);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/LookingForTypes(5)/ContactEnquiries
        [Queryable]
        public IQueryable<ContactEnquiry> GetContactEnquiries([FromODataUri] int key)
        {
            return db.LookingForTypes.Where(m => m.Id == key).SelectMany(m => m.ContactEnquiries);
        }

        // GET: odata/LookingForTypes(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.LookingForTypes.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LookingForTypeExists(int key)
        {
            return db.LookingForTypes.Count(e => e.Id == key) > 0;
        }
    }
}
