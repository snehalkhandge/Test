using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Myware.Data.Entity;
using Myware.Data.Entity.CustomStores;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Myware.Web.API.UserManagement
{
    [Authorize]
    public class ManageRolesController : ApiController
    {
        public ManageRolesController()
        {
            db = new ApplicationDbContext();
            _roleManager = new RoleManager<Role, int>(new RoleStore<Role, int, AppUserRole>(db));
        }

        private ApplicationDbContext db { get; set; }
        private RoleManager<Role, int> _roleManager { get; set; }

        

        // GET: api/ManageRoles
        [Route("roles/{page}/size/{pageSize}/search/{searchQuery}")]
        public ListRoleViewModel GetRoles(int page = 1, int pageSize = 10, string searchQuery = "")
        {    
             var query = _roleManager.Roles.Include(b => b.RolePermissions.Select(p => p.Permission))
                               .Select(t => new RoleViewModel
                                 {
                                     Id = t.Id,
                                     Name = t.Name,
                                     RolePermissions = t.RolePermissions.Select(b => new RolePermissionViewModel
                                     {
                                         Id = b.Id,
                                         RoleId = b.RoleId,
                                         PermissionId = b.PermissionId,
                                         Permission = b.Permission
                                     }).ToList()
                                 });

            query = query.OrderByDescending(x => x.Id); 

            return new ListRoleViewModel { 
                
                         TotalItems = query.Count(),
                         Results = query.Skip(pageSize * (page - 1))
                                            .Take(pageSize).ToList() 
             
                     };

            
        }
        
        [Route("roleIsUnique/{searchQuery}")]
        public bool GetRoles(string searchQuery = "")
        {
            return _roleManager.Roles.Any(x => x.Name.Contains(searchQuery));
        }



        [Route("saveRole/{id}")]
        [ResponseType(typeof(CreateRoleViewModel))]
        public IHttpActionResult PostRole(int id, CreateRoleViewModel roleVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roleVM.Id)
            {
                return BadRequest();
            }
            try
            {
                var role = new Role();
                
                if (roleVM.Id == 0)
                {
                    //New Role
                    role.Name = roleVM.Name;

                    _roleManager.Create(role);
                    db.SaveChanges();

                }
                else
                {
                    //Update Role
                    role = _roleManager.FindById(roleVM.Id);

                    if(role == null)
                    {
                        return BadRequest("Invalid role id");
                    }
                    
                    if(role.Name != roleVM.Name)
                    {
                        role.Name = roleVM.Name;
                        _roleManager.Update(role);
                        db.SaveChanges();
                    }

                    var itemsToDelete = db.RolePermissions.Where(x => x.RoleId == role.Id);
                    db.RolePermissions.RemoveRange(itemsToDelete);
                    db.SaveChanges();

                }


                var rolePermissions = new List<RolePermissions>();

                foreach (var item in roleVM.Permissions)
                {
                    rolePermissions.Add(new RolePermissions
                                            {
                                                RoleId = role.Id,
                                                PermissionId = item
                                            });
                }

                db.RolePermissions.AddRange(rolePermissions);

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_roleManager.FindById(roleVM.Id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(roleVM);
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