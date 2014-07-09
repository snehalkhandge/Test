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
using Myware.Data.Entity.Models.UserManagement;
using Myware.Data.Entity.Models.UserTasks;

namespace Myware.Web.API.TaskManager
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using Myware.Data.Entity.Models.UserTasks;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<AssignedTask>("AssignedTasks");
    builder.EntitySet<User>("Users"); 
    builder.EntitySet<TasksRelatedFile>("TasksRelatedFiles"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AssignedTasksController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/AssignedTasks
        [Queryable]
        public IQueryable<AssignedTask> GetAssignedTasks()
        {
            return db.AssignedTasks;
        }

        // GET: odata/AssignedTasks(5)
        [Queryable]
        public SingleResult<AssignedTask> GetAssignedTask([FromODataUri] int key)
        {
            return SingleResult.Create(db.AssignedTasks.Where(assignedTask => assignedTask.Id == key));
        }

        // PUT: odata/AssignedTasks(5)
        public IHttpActionResult Put([FromODataUri] int key, AssignedTask assignedTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != assignedTask.Id)
            {
                return BadRequest();
            }

            db.Entry(assignedTask).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignedTaskExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(assignedTask);
        }

        // POST: odata/AssignedTasks
        public IHttpActionResult Post(AssignedTask assignedTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AssignedTasks.Add(assignedTask);
            db.SaveChanges();

            return Created(assignedTask);
        }

        // PATCH: odata/AssignedTasks(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<AssignedTask> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AssignedTask assignedTask = db.AssignedTasks.Find(key);
            if (assignedTask == null)
            {
                return NotFound();
            }

            patch.Patch(assignedTask);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignedTaskExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(assignedTask);
        }

        // DELETE: odata/AssignedTasks(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            AssignedTask assignedTask = db.AssignedTasks.Find(key);
            if (assignedTask == null)
            {
                return NotFound();
            }

            db.AssignedTasks.Remove(assignedTask);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AssignedTasks(5)/AssignedByUser
        [Queryable]
        public SingleResult<User> GetAssignedByUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.AssignedTasks.Where(m => m.Id == key).Select(m => m.AssignedByUser));
        }

        // GET: odata/AssignedTasks(5)/AssignedToUser
        [Queryable]
        public SingleResult<User> GetAssignedToUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.AssignedTasks.Where(m => m.Id == key).Select(m => m.AssignedToUser));
        }

        // GET: odata/AssignedTasks(5)/ParentTask
        [Queryable]
        public SingleResult<AssignedTask> GetParentTask([FromODataUri] int key)
        {
            return SingleResult.Create(db.AssignedTasks.Where(m => m.Id == key).Select(m => m.ParentTask));
        }

        // GET: odata/AssignedTasks(5)/TasksRelatedFiles
        [Queryable]
        public IQueryable<TasksRelatedFile> GetTasksRelatedFiles([FromODataUri] int key)
        {
            return db.AssignedTasks.Where(m => m.Id == key).SelectMany(m => m.TasksRelatedFiles);
        }

        // GET: odata/AssignedTasks(5)/User
        [Queryable]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.AssignedTasks.Where(m => m.Id == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssignedTaskExists(int key)
        {
            return db.AssignedTasks.Count(e => e.Id == key) > 0;
        }
    }
}
