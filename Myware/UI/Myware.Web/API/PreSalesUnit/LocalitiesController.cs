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
using Myware.Data.Entity.Models.PreSales;
using Myware.Data.Entity.Models.UserManagement;

namespace Myware.Web.API.PreSalesUnit
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using Myware.Data.Entity.Models.PresalesUnit;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Locality>("Localities");
    builder.EntitySet<Broker>("Brokers"); 
    
    builder.EntitySet<Company>("Companies"); 
    
    
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LocalitiesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Localities
        [Queryable]
        public IQueryable<Locality> GetLocalities()
        {
            return db.Localities;
        }

        // GET: odata/Localities(5)
        [Queryable]
        public SingleResult<Locality> GetLocality([FromODataUri] int key)
        {
            return SingleResult.Create(db.Localities.Where(locality => locality.Id == key));
        }

        // PUT: odata/Localities(5)
        public IHttpActionResult Put([FromODataUri] int key, Locality locality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != locality.Id)
            {
                return BadRequest();
            }

            db.Entry(locality).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(locality);
        }

        // POST: odata/Localities
        public IHttpActionResult Post(Locality locality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Localities.Add(locality);
            db.SaveChanges();

            return Created(locality);
        }

        // PATCH: odata/Localities(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Locality> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Locality locality = db.Localities.Find(key);
            if (locality == null)
            {
                return NotFound();
            }

            patch.Patch(locality);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(locality);
        }

        // DELETE: odata/Localities(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Locality locality = db.Localities.Find(key);
            if (locality == null)
            {
                return NotFound();
            }

            db.Localities.Remove(locality);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Localities(5)/Brokers
        [Queryable]
        public IQueryable<Broker> GetBrokers([FromODataUri] int key)
        {
            return db.Localities.Where(m => m.Id == key).SelectMany(m => m.Brokers);
        }

        // GET: odata/Localities(5)/BusinessInformations
        [Queryable]
        public IQueryable<BusinessInformation> GetBusinessInformations([FromODataUri] int key)
        {
            return db.Localities.Where(m => m.Id == key).SelectMany(m => m.BusinessInformations);
        }

        // GET: odata/Localities(5)/Companies
        [Queryable]
        public IQueryable<Company> GetCompanies([FromODataUri] int key)
        {
            return db.Localities.Where(m => m.Id == key).SelectMany(m => m.Companies);
        }

        // GET: odata/Localities(5)/Location
        [Queryable]
        public SingleResult<Location> GetLocation([FromODataUri] int key)
        {
            return SingleResult.Create(db.Localities.Where(m => m.Id == key).Select(m => m.Location));
        }

        // GET: odata/Localities(5)/PersonalInformations
        [Queryable]
        public IQueryable<PersonalInformation> GetPersonalInformations([FromODataUri] int key)
        {
            return db.Localities.Where(m => m.Id == key).SelectMany(m => m.PersonalInformations);
        }

        // GET: odata/Localities(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Localities.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocalityExists(int key)
        {
            return db.Localities.Count(e => e.Id == key) > 0;
        }
    }
}
