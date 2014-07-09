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
using Myware.Data.Entity.Models.PostSales;
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
    builder.EntitySet<PersonalInformation>("PersonalInformations");
    builder.EntitySet<BusinessInformation>("BusinessInformations"); 
    builder.EntitySet<Campaign>("Campaigns"); 
    builder.EntitySet<ContactNumber>("ContactNumbers"); 
    builder.EntitySet<CustomerEnquiryTypeCollection>("CustomerEnquiryTypeCollection"); 
    builder.EntitySet<Locality>("Localities"); 
    builder.EntitySet<PersonalInformationBookingMeta>("PersonalInformationBookingMetas"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PersonalInformationsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/PersonalInformations
        [Queryable]
        public IQueryable<PersonalInformation> GetPersonalInformations()
        {
            return db.PersonalInformations;
        }

        // GET: odata/PersonalInformations(5)
        [Queryable]
        public SingleResult<PersonalInformation> GetPersonalInformation([FromODataUri] int key)
        {
            return SingleResult.Create(db.PersonalInformations.Where(personalInformation => personalInformation.Id == key));
        }

        // PUT: odata/PersonalInformations(5)
        public IHttpActionResult Put([FromODataUri] int key, PersonalInformation personalInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != personalInformation.Id)
            {
                return BadRequest();
            }

            db.Entry(personalInformation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalInformationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(personalInformation);
        }

        // POST: odata/PersonalInformations
        public IHttpActionResult Post(PersonalInformation personalInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonalInformations.Add(personalInformation);
            db.SaveChanges();

            return Created(personalInformation);
        }

        // PATCH: odata/PersonalInformations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<PersonalInformation> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonalInformation personalInformation = db.PersonalInformations.Find(key);
            if (personalInformation == null)
            {
                return NotFound();
            }

            patch.Patch(personalInformation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalInformationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(personalInformation);
        }

        // DELETE: odata/PersonalInformations(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            PersonalInformation personalInformation = db.PersonalInformations.Find(key);
            if (personalInformation == null)
            {
                return NotFound();
            }

            db.PersonalInformations.Remove(personalInformation);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/PersonalInformations(5)/BusinessInformation
        [Queryable]
        public IQueryable<BusinessInformation> GetBusinessInformation([FromODataUri] int key)
        {
            return db.PersonalInformations.Where(m => m.Id == key).SelectMany(m => m.BusinessInformation);
        }

        // GET: odata/PersonalInformations(5)/Campaigns
        [Queryable]
        public IQueryable<Campaign> GetCampaigns([FromODataUri] int key)
        {
            return db.PersonalInformations.Where(m => m.Id == key).SelectMany(m => m.Campaigns);
        }

        // GET: odata/PersonalInformations(5)/ContactNumbers
        [Queryable]
        public IQueryable<ContactNumber> GetContactNumbers([FromODataUri] int key)
        {
            return db.PersonalInformations.Where(m => m.Id == key).SelectMany(m => m.ContactNumbers);
        }

        // GET: odata/PersonalInformations(5)/CustomerEnquiryTypeCollection
        [Queryable]
        public IQueryable<CustomerEnquiryTypeCollection> GetCustomerEnquiryTypeCollection([FromODataUri] int key)
        {
            return db.PersonalInformations.Where(m => m.Id == key).SelectMany(m => m.CustomerEnquiryTypeCollection);
        }

        // GET: odata/PersonalInformations(5)/Locality
        [Queryable]
        public SingleResult<Locality> GetLocality([FromODataUri] int key)
        {
            return SingleResult.Create(db.PersonalInformations.Where(m => m.Id == key).Select(m => m.Locality));
        }

        // GET: odata/PersonalInformations(5)/PersonalInformationBookingMeta
        [Queryable]
        public IQueryable<PersonalInformationBookingMeta> GetPersonalInformationBookingMeta([FromODataUri] int key)
        {
            return db.PersonalInformations.Where(m => m.Id == key).SelectMany(m => m.PersonalInformationBookingMeta);
        }

        // GET: odata/PersonalInformations(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.PersonalInformations.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonalInformationExists(int key)
        {
            return db.PersonalInformations.Count(e => e.Id == key) > 0;
        }
    }
}
