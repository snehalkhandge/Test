using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Myware.Data.Entity;
using Myware.Data.Entity.CustomStores;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Web.Models;
using Myware.Web.Models.PreSalesUnit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Myware.Web.API
{
    [Authorize]
    public class ManageDevelopersController : ApiController
    {
        public ManageDevelopersController()
        {
            db = new ApplicationDbContext();            
        }

        private ApplicationDbContext db { get; set; }
        

        // GET: api/ManageRoles
        [Route("developers/{page}/size/{pageSize}/search/{searchQuery}")]
        public ListDeveloperViewModel GetDevelopers(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var query = db.Developers.Include(t => t.DeveloperCompanies)                                    
                                    .OrderByDescending(x => x.Id);


            var result = query.Select(t => new CreateDeveloperViewModel {                                 
                                    Id = t.Id,
                                    Name = t.Name,
                                    Description = t.Description,
                                    DeveloperCompanies = t.DeveloperCompanies.ToList()                
                               }).Skip(pageSize * (page - 1))
                              .Take(pageSize).ToList();


            var listResult = new ListDeveloperViewModel();
            listResult.TotalItems = query.Count();
            listResult.Results = result;


            return listResult;
        }

        [Route("saveDeveloper/{id}")]
        [ResponseType(typeof(CreateDeveloperViewModel))]
        public IHttpActionResult PostDeveloper(int id, CreateDeveloperViewModel typeVM)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (id != typeVM.Id)
            {
                return BadRequest();
            }

            var type = new Developer();

            try
            {
                if (typeVM.Id == 0)
                {


                    type.Name = typeVM.Name;
                    type.Description = typeVM.Description;                    
                    type.UpdatedByUserId = typeVM.UserId;
                    type.LastUpdated = DateTime.UtcNow;
                    db.Entry(type).State = EntityState.Added;
                    db.SaveChanges();

                    id = type.Id;
                }
                else
                {
                    var oldType = db.Developers.Where(e => e.Id == id).SingleOrDefault();


                    oldType.Name = typeVM.Name;
                    oldType.Description = typeVM.Description;
                    oldType.UpdatedByUserId = typeVM.UserId;
                    oldType.LastUpdated = DateTime.UtcNow;

                    db.Entry(oldType).State = EntityState.Modified;
                    db.SaveChanges();



                    var itemsToDelete = db.DeveloperCompanies.Where(x => x.DeveloperId == id);
                    db.DeveloperCompanies.RemoveRange(itemsToDelete);
                    db.SaveChanges();
                }


                if (typeVM.SelectedCompanies.Count > 0)
                {

                    var developerCompanies = new List<DeveloperCompanies>();

                    foreach (var item in typeVM.SelectedCompanies)
                    {
                        developerCompanies.Add(new DeveloperCompanies
                        {
                            CompanyId = item,
                            DeveloperId = id
                        });
                    }

                    db.DeveloperCompanies.AddRange(developerCompanies);

                    db.SaveChanges();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Developers.Count(e => e.Id == id) > 0)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(typeVM);
        }


        [Route("developerIsUnique/{searchQuery}")]
        [HttpGet]
        public bool IsDeveloperUnique(string searchQuery = "")
        {
            return db.Developers.Any(x => x.Name.Contains(searchQuery));
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoleExists(int id)
        {
            return db.Roles.Count(e => e.Id == id) > 0;
        }
    }
}