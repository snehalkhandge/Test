using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Myware.Data.Entity;
using Myware.Data.Entity.CustomStores;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Web.Models;
using Myware.Web.Models.PreSales;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Myware.Web.API.UserManagement
{
    public class ManageUsersController : ApiController
    {

        public ManageUsersController()
            : this(new AppUserManager(new AppUserStore(new ApplicationDbContext())))
        {
        }

        public ManageUsersController(AppUserManager userManager)
        {
            _userManager = userManager;
        }

        private AppUserManager _userManager { get; set; }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ManageUsers
        [Route("getUsers/{page}/size/{pageSize}/search/{searchQuery}")]
        [HttpGet]
        public async Task<ListUserViewModel> GetUsers(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var sqlQuery =  _userManager.Users.OrderByDescending(x => x.Id);

            var listUsers = new ListUserViewModel();

            listUsers.Total = sqlQuery.Count();

            var users = sqlQuery.Skip(pageSize * (page - 1))
                               .Take(pageSize).ToList();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user.Id);
                
                listUsers.Users.Add(new UserManagerUserViewModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RoleName = roles.FirstOrDefault(),
                    UserName = user.UserName,
                    Id = user.Id
                });
            }

            return listUsers;

        }

             
        [Route("getRolesByUserId/{id}")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRolesByUserId(int id)
        {
            var roles = await _userManager.GetRolesAsync(id);
            return Ok(roles.SingleOrDefault());
        }

        [Route("getRoleByName/{name}")]
        [HttpPost]
        public RoleViewModel GetRoleByName(string name)
        {
            var _roleManager = new RoleManager<Role, int>(new RoleStore<Role, int, AppUserRole>(db));

            return _roleManager.Roles.Include(b => b.RolePermissions.Select(p => p.Permission))
                               .Where(t => t.Name == name)
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
                                  })
                                  .SingleOrDefault();


        }


        [Route("userIsUniqueByEmail")]
        [HttpGet]
        public async Task<CheckUniqueUserViewModel> IsUserEmailUnique(EmailVM data)
        {

            var record = await _userManager.FindByEmailAsync(data.email);

            var msg = new CheckUniqueUserViewModel();

            if (record == null)
            {
                msg.IsUnique = true;
                return msg;
            }

            msg.IsUnique = false;

            var user = new UserManagerUserViewModel();

            user.Id = record.Id;
            user.FirstName = record.FirstName;
            user.LastName = record.LastName;
            user.Email = record.Email;
            
            user.UserName = record.UserName;
            msg.User = user;

            return msg;
        }



        [Route("userIsUniqueByUserName")]
        [HttpGet]
        public async Task<CheckUniqueUserViewModel> IsUniqueByUserName(EmailVM data)
        {

            var record = await _userManager.FindByNameAsync(data.email);

            var msg = new CheckUniqueUserViewModel();

            if (record == null)
            {
                msg.IsUnique = true;
                return msg;
            }

            msg.IsUnique = false;

            var user = new UserManagerUserViewModel();

            user.Id = record.Id;
            user.FirstName = record.FirstName;
            user.LastName = record.LastName;
            user.Email = record.Email;
            
            user.UserName = record.UserName;
            msg.User = user;

            return msg;
        }



        [Route("getAllUsers/all")]
        public List<PartialUserViewModel> GetAllUsers()
        {
            return _userManager.Users
                               .Select(t => new PartialUserViewModel { 
                                   Id = t.Id,
                                   FirstName = t.FirstName,
                                   LastName = t.LastName
                                }).ToList();
        }

        // GET: api/ManageUsers/5
        [Route("getUserById/{id}")]
        [ResponseType(typeof(UserManagerUserViewModel))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var usr = new UserManagerUserViewModel();

            usr.Email = user.Email;
            usr.FirstName = user.FirstName;
            usr.LastName = user.LastName;
            var roles = await _userManager.GetRolesAsync(user.Id);
            usr.RoleName = roles.FirstOrDefault();
            usr.UserName = user.UserName;
            usr.Id = user.Id;

            return Ok(usr);
        }

        // PUT: api/ManageUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ManageUsers
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }



        [Route("saveUser/{id}")]
        [ResponseType(typeof(UserManagerUserViewModel))]
        public async Task<IHttpActionResult> PostUser(int id, UserManagerUserViewModel typeVM)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (id != typeVM.Id)
            {
                return BadRequest();
            }

            var type = new User();

            try
            {
                if (typeVM.Id == 0)
                {
                    //Check Duplicate Data
                    #region Check Duplicate Data
                    var chkDuplicateEmail = new User();
                    if (typeVM.Email != "")
                    {
                        chkDuplicateEmail = _userManager.Users.Where(t => t.Email == typeVM.Email).SingleOrDefault();
                                                        
                        if (chkDuplicateEmail != null)
                        {
                            return BadRequest("Error, Duplicate Email Data == " + chkDuplicateEmail.Id.ToString());
                        }
                    }
                    
                    if (typeVM.UserName != "")
                    {
                        chkDuplicateEmail = _userManager.Users.Where(t => t.UserName == typeVM.UserName).SingleOrDefault();

                        if (chkDuplicateEmail != null)
                        {
                            return BadRequest("Error, Duplicate User Name Data == " + chkDuplicateEmail.Id.ToString());
                        }
                    }

                    if (typeVM.Password != "")
                    {
                        return BadRequest("Error, Password cannot be empty.");
                    }

                    #endregion

                    type.FirstName = typeVM.FirstName;
                    type.LastName = typeVM.LastName;
                    type.Email = typeVM.Email;
                    type.UserName = typeVM.UserName;

                    await _userManager.CreateAsync(type, typeVM.Password);
                    await _userManager.AddToRoleAsync(type.Id, typeVM.RoleName);

                    db.SaveChanges();

                    id = type.Id;

                }
                else
                {
                    var oldType = await _userManager.FindByNameAsync(typeVM.UserName);

                    oldType.FirstName = typeVM.FirstName;                    
                    oldType.LastName = typeVM.LastName;
                    oldType.Email = typeVM.Email;

                    var roles = await _userManager.GetRolesAsync(oldType.Id);

                    if (typeVM.RoleName != roles.FirstOrDefault())
                    {
                        await _userManager.RemoveFromRoleAsync(oldType.Id, roles.FirstOrDefault());
                        await _userManager.AddToRoleAsync(oldType.Id, typeVM.RoleName);
                    }

                    await _userManager.UpdateAsync(oldType);

                    db.SaveChanges();
                }



                typeVM.Id = id;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (_userManager.Users.Count(e => e.Id == id) > 0)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}