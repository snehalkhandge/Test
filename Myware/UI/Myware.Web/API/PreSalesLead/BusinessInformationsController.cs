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

namespace Myware.Web.API.PreSalesLead
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using Myware.Data.Entity.Models.PreSales;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<BusinessInformation>("BusinessInformations");
    builder.EntitySet<ContactNumber>("ContactNumbers"); 
    builder.EntitySet<Locality>("Localities"); 
    builder.EntitySet<PersonalInformation>("PersonalInformations"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BusinessInformationsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/BusinessInformations
        [Queryable]
        public IQueryable<BusinessInformation> GetBusinessInformations()
        {
            return db.BusinessInformations;
        }

        // GET: odata/BusinessInformations(5)
        [Queryable]
        public SingleResult<BusinessInformation> GetBusinessInformation([FromODataUri] int key)
        {
            return SingleResult.Create(db.BusinessInformations.Where(businessInformation => businessInformation.Id == key));
        }

        // PUT: odata/BusinessInformations(5)
        public IHttpActionResult Put([FromODataUri] int key, BusinessInformation businessInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != businessInformation.Id)
            {
                return BadRequest();
            }

            db.Entry(businessInformation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessInformationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(businessInformation);
        }

        // POST: odata/BusinessInformations
        public IHttpActionResult Post(BusinessInformation businessInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BusinessInformations.Add(businessInformation);
            db.SaveChanges();

            return Created(businessInformation);
        }

        // PATCH: odata/BusinessInformations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<BusinessInformation> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusinessInformation businessInformation = db.BusinessInformations.Find(key);
            if (businessInformation == null)
            {
                return NotFound();
            }

            patch.Patch(businessInformation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessInformationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(businessInformation);
        }

        // DELETE: odata/BusinessInformations(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            BusinessInformation businessInformation = db.BusinessInformations.Find(key);
            if (businessInformation == null)
            {
                return NotFound();
            }

            db.BusinessInformations.Remove(businessInformation);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/BusinessInformations(5)/BusinessContactNumbers
        [Queryable]
        public IQueryable<ContactNumber> GetBusinessContactNumbers([FromODataUri] int key)
        {
            return db.BusinessInformations.Where(m => m.Id == key).SelectMany(m => m.BusinessContactNumbers);
        }

        // GET: odata/BusinessInformations(5)/BusinessLocality
        [Queryable]
        public SingleResult<Locality> GetBusinessLocality([FromODataUri] int key)
        {
            return SingleResult.Create(db.BusinessInformations.Where(m => m.Id == key).Select(m => m.BusinessLocality));
        }

        // GET: odata/BusinessInformations(5)/PersonalInformation
        [Queryable]
        public SingleResult<PersonalInformation> GetPersonalInformation([FromODataUri] int key)
        {
            return SingleResult.Create(db.BusinessInformations.Where(m => m.Id == key).Select(m => m.PersonalInformation));
        }

        // GET: odata/BusinessInformations(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.BusinessInformations.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusinessInformationExists(int key)
        {
            return db.BusinessInformations.Count(e => e.Id == key) > 0;
        }
    }
}
