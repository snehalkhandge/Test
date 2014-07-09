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
    builder.EntitySet<TransactionType>("TransactionTypes");
    builder.EntitySet<ContactEnquiry>("ContactEnquiries"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TransactionTypesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/TransactionTypes
        [Queryable]
        public IQueryable<TransactionType> GetTransactionTypes()
        {
            return db.TransactionTypes;
        }

        // GET: odata/TransactionTypes(5)
        [Queryable]
        public SingleResult<TransactionType> GetTransactionType([FromODataUri] int key)
        {
            return SingleResult.Create(db.TransactionTypes.Where(transactionType => transactionType.Id == key));
        }

        // PUT: odata/TransactionTypes(5)
        public IHttpActionResult Put([FromODataUri] int key, TransactionType transactionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != transactionType.Id)
            {
                return BadRequest();
            }

            db.Entry(transactionType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(transactionType);
        }

        // POST: odata/TransactionTypes
        public IHttpActionResult Post(TransactionType transactionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TransactionTypes.Add(transactionType);
            db.SaveChanges();

            return Created(transactionType);
        }

        // PATCH: odata/TransactionTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<TransactionType> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TransactionType transactionType = db.TransactionTypes.Find(key);
            if (transactionType == null)
            {
                return NotFound();
            }

            patch.Patch(transactionType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(transactionType);
        }

        // DELETE: odata/TransactionTypes(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            TransactionType transactionType = db.TransactionTypes.Find(key);
            if (transactionType == null)
            {
                return NotFound();
            }

            db.TransactionTypes.Remove(transactionType);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/TransactionTypes(5)/ContactEnquiries
        [Queryable]
        public IQueryable<ContactEnquiry> GetContactEnquiries([FromODataUri] int key)
        {
            return db.TransactionTypes.Where(m => m.Id == key).SelectMany(m => m.ContactEnquiries);
        }

        // GET: odata/TransactionTypes(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.TransactionTypes.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionTypeExists(int key)
        {
            return db.TransactionTypes.Count(e => e.Id == key) > 0;
        }
    }
}
