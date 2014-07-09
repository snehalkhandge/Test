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
    
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ContactEnquiriesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/ContactEnquiries
        [Queryable]
        public IQueryable<ContactEnquiry> GetContactEnquiries()
        {
            return db.ContactEnquiries;
        }

        // GET: odata/ContactEnquiries(5)
        [Queryable]
        public SingleResult<ContactEnquiry> GetContactEnquiry([FromODataUri] int key)
        {
            return SingleResult.Create(db.ContactEnquiries.Where(contactEnquiry => contactEnquiry.Id == key));
        }

        // PUT: odata/ContactEnquiries(5)
        public IHttpActionResult Put([FromODataUri] int key, ContactEnquiry contactEnquiry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != contactEnquiry.Id)
            {
                return BadRequest();
            }

            db.Entry(contactEnquiry).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactEnquiryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(contactEnquiry);
        }

        // POST: odata/ContactEnquiries
        public IHttpActionResult Post(ContactEnquiry contactEnquiry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactEnquiries.Add(contactEnquiry);
            db.SaveChanges();

            return Created(contactEnquiry);
        }

        // PATCH: odata/ContactEnquiries(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ContactEnquiry> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ContactEnquiry contactEnquiry = db.ContactEnquiries.Find(key);
            if (contactEnquiry == null)
            {
                return NotFound();
            }

            patch.Patch(contactEnquiry);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactEnquiryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(contactEnquiry);
        }

        // DELETE: odata/ContactEnquiries(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            ContactEnquiry contactEnquiry = db.ContactEnquiries.Find(key);
            if (contactEnquiry == null)
            {
                return NotFound();
            }

            db.ContactEnquiries.Remove(contactEnquiry);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ContactEnquiries(5)/ContactStatus
        [Queryable]
        public SingleResult<ContactStatus> GetContactStatus([FromODataUri] int key)
        {
            return SingleResult.Create(db.ContactEnquiries.Where(m => m.Id == key).Select(m => m.ContactStatus));
        }

        // GET: odata/ContactEnquiries(5)/FacingType
        [Queryable]
        public SingleResult<FacingType> GetFacingType([FromODataUri] int key)
        {
            return SingleResult.Create(db.ContactEnquiries.Where(m => m.Id == key).Select(m => m.FacingType));
        }

        // GET: odata/ContactEnquiries(5)/LookingForType
        [Queryable]
        public SingleResult<LookingForType> GetLookingForType([FromODataUri] int key)
        {
            return SingleResult.Create(db.ContactEnquiries.Where(m => m.Id == key).Select(m => m.LookingForType));
        }

        // GET: odata/ContactEnquiries(5)/PersonalInformation
        [Queryable]
        public SingleResult<PersonalInformation> GetPersonalInformation([FromODataUri] int key)
        {
            return SingleResult.Create(db.ContactEnquiries.Where(m => m.Id == key).Select(m => m.PersonalInformation));
        }

        // GET: odata/ContactEnquiries(5)/TransactionType
        [Queryable]
        public SingleResult<TransactionType> GetTransactionType([FromODataUri] int key)
        {
            return SingleResult.Create(db.ContactEnquiries.Where(m => m.Id == key).Select(m => m.TransactionType));
        }

        // GET: odata/ContactEnquiries(5)/UnitTypes
        [Queryable]
        public IQueryable<UnitType> GetUnitTypes([FromODataUri] int key)
        {
            return db.ContactEnquiries.Where(m => m.Id == key).SelectMany(m => m.UnitTypes);
        }

        // GET: odata/ContactEnquiries(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.ContactEnquiries.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactEnquiryExists(int key)
        {
            return db.ContactEnquiries.Count(e => e.Id == key) > 0;
        }
    }
}
