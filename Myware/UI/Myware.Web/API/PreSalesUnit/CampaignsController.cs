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
    public class CampaignsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Campaigns
        [Queryable]
        public IQueryable<Campaign> GetCampaigns()
        {
            return db.Campaigns;
        }

        // GET: odata/Campaigns(5)
        [Queryable]
        public SingleResult<Campaign> GetCampaign([FromODataUri] int key)
        {
            return SingleResult.Create(db.Campaigns.Where(campaign => campaign.Id == key));
        }

        // PUT: odata/Campaigns(5)
        public IHttpActionResult Put([FromODataUri] int key, Campaign campaign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != campaign.Id)
            {
                return BadRequest();
            }

            db.Entry(campaign).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampaignExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(campaign);
        }

        // POST: odata/Campaigns
        public IHttpActionResult Post(Campaign campaign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Campaigns.Add(campaign);
            db.SaveChanges();

            return Created(campaign);
        }

        // PATCH: odata/Campaigns(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Campaign> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Campaign campaign = db.Campaigns.Find(key);
            if (campaign == null)
            {
                return NotFound();
            }

            patch.Patch(campaign);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampaignExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(campaign);
        }

        // DELETE: odata/Campaigns(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Campaign campaign = db.Campaigns.Find(key);
            if (campaign == null)
            {
                return NotFound();
            }

            db.Campaigns.Remove(campaign);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Campaigns(5)/CreatedBy
        [Queryable]
        public SingleResult<User> GetCreatedBy([FromODataUri] int key)
        {
            return SingleResult.Create(db.Campaigns.Where(m => m.Id == key).Select(m => m.CreatedBy));
        }

        // GET: odata/Campaigns(5)/ParentCampaign
        [Queryable]
        public SingleResult<Campaign> GetParentCampaign([FromODataUri] int key)
        {
            return SingleResult.Create(db.Campaigns.Where(m => m.Id == key).Select(m => m.ParentCampaign));
        }

        // GET: odata/Campaigns(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Campaigns.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CampaignExists(int key)
        {
            return db.Campaigns.Count(e => e.Id == key) > 0;
        }
    }
}
