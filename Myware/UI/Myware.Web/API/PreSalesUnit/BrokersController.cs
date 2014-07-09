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
    
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BrokersController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Brokers
        [Queryable]
        public IQueryable<Broker> GetBrokers()
        {
            return db.Brokers;
        }

        // GET: odata/Brokers(5)
        [Queryable]
        public SingleResult<Broker> GetBroker([FromODataUri] int key)
        {
            return SingleResult.Create(db.Brokers.Where(broker => broker.Id == key));
        }

        // PUT: odata/Brokers(5)
        public IHttpActionResult Put([FromODataUri] int key, Broker broker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != broker.Id)
            {
                return BadRequest();
            }

            db.Entry(broker).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrokerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(broker);
        }

        // POST: odata/Brokers
        public IHttpActionResult Post(Broker broker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Brokers.Add(broker);
            db.SaveChanges();

            return Created(broker);
        }

        // PATCH: odata/Brokers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Broker> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Broker broker = db.Brokers.Find(key);
            if (broker == null)
            {
                return NotFound();
            }

            patch.Patch(broker);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrokerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(broker);
        }

        // DELETE: odata/Brokers(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Broker broker = db.Brokers.Find(key);
            if (broker == null)
            {
                return NotFound();
            }

            db.Brokers.Remove(broker);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Brokers(5)/ContactNumbers
        [Queryable]
        public IQueryable<ContactNumber> GetContactNumbers([FromODataUri] int key)
        {
            return db.Brokers.Where(m => m.Id == key).SelectMany(m => m.ContactNumbers);
        }

        // GET: odata/Brokers(5)/Locality
        [Queryable]
        public SingleResult<Locality> GetLocality([FromODataUri] int key)
        {
            return SingleResult.Create(db.Brokers.Where(m => m.Id == key).Select(m => m.Locality));
        }

        // GET: odata/Brokers(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Brokers.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BrokerExists(int key)
        {
            return db.Brokers.Count(e => e.Id == key) > 0;
        }
    }
}
