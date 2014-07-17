using Myware.Data.Entity;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Web.Models;
using Myware.Web.Models.PreSalesUnit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Myware.Web.API.UserManagement
{
    [Authorize]
    public class ManageCompaniesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ManagePermissions

        [Route("companies/{page}/size/{pageSize}/search/{searchQuery}")]
        public ListCompanyViewModel GetCompanies(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var query = db.Companies.Include(t => t.Locality)
                                    .Include(t => t.Locality.Location)
                                    .Include(t => t.ContactNumbers)
                                    .OrderByDescending(x => x.Id);            

            return new ListCompanyViewModel
            {

                TotalItems = query.Count(),
                Results = query.Select(t => new CreateCompanyViewModel 
                                                { 
                                                     Id = t.Id,
                                                     Name = t.Name,
                                                     Address = t.Address,
                                                     Locality = t.Locality,
                                                     Location = t.Locality.Location,
                                                     ContactNumbers = t.ContactNumbers.ToList()
                                                }).Skip(pageSize * (page - 1))
                                                  .Take(pageSize).ToList()

            };

        }


        [Route("companyById/{id}")]
        public CreateCompanyViewModel GetCompanyById(int id)
        {
            var query = db.Companies.Include(t => t.Locality)
                                    .Include(t => t.Locality.Location)
                                    .Include(t => t.ContactNumbers);

            return  query.Where(r => r.Id == id )
                         .Select(t =>new CreateCompanyViewModel
                            {
                                Id = t.Id,
                                Name = t.Name,
                                Address = t.Address,
                                Locality = t.Locality,
                                LocalityId = t.LocalityId,
                                Location = t.Locality.Location,
                                ContactNumbers = t.ContactNumbers.ToList(),
                                Pin = t.Pin,
                                FaxNumber = t.FaxNumber,
                                ReceiptFormat = t.ReceiptFormat                                
                            })
                          .SingleOrDefault();

        }

        [Route("companies/all")]
        public ListCompanyViewModel GetAllCompanies()
        {
            var query = db.Companies.OrderByDescending(x => x.Id);

            return new ListCompanyViewModel
            {

                TotalItems = 0,
                Results = query.Select(t => new CreateCompanyViewModel
                                                {
                                                    Id = t.Id,
                                                    Name = t.Name
                                                }).ToList()
            
            };

        }


        [Route("companyIsUnique/{searchQuery}")]
        public bool IsCompanyUnique(string searchQuery = "")
        {
            return db.Companies.Any(x => x.Name.Contains(searchQuery)); 
        }


        [Route("saveCompany/{id}")]
        [ResponseType(typeof(CreateCompanyViewModel))]
        public IHttpActionResult PostUnitType(int id, CreateCompanyViewModel typeVM)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (id != typeVM.Id)
            {
                return BadRequest();
            }

            var type = new Company();

            try
            {
                if (typeVM.Id == 0)
                {


                    type.Name = typeVM.Name;
                    type.Address = typeVM.Address;
                    type.LocalityId = typeVM.LocalityId;
                    type.Pin = typeVM.Pin;                    
                    type.FaxNumber = typeVM.FaxNumber;
                    type.ReceiptFormat = typeVM.ReceiptFormat;
                    type.UpdatedByUserId = typeVM.UserId;
                    type.LastUpdated = DateTime.UtcNow;
                    db.Entry(type).State = EntityState.Added;
                    db.SaveChanges();

                    id = type.Id;
                }
                else
                {
                    var oldType = db.Companies.Include(t => t.ContactNumbers).Where(e => e.Id == id).SingleOrDefault();

                    
                    oldType.Name = typeVM.Name;
                    oldType.Address = typeVM.Address;
                    oldType.LocalityId = typeVM.LocalityId;
                    oldType.Pin = typeVM.Pin;                    
                    oldType.FaxNumber = typeVM.FaxNumber;
                    oldType.ReceiptFormat = typeVM.ReceiptFormat;
                    oldType.UpdatedByUserId = typeVM.UserId;
                    oldType.LastUpdated = DateTime.UtcNow;
                    db.Entry(oldType).State = EntityState.Modified;
                    db.SaveChanges();



                    var itemsToDelete = db.CompanyContactNumbers.Where(x => x.CompanyId == id);
                    db.CompanyContactNumbers.RemoveRange(itemsToDelete);
                    db.SaveChanges();
                }


                if(typeVM.ContactNumbers.Count > 0)
                {
                    var companyContactNumbers = new List<CompanyContactNumber>();

                    foreach (var item in typeVM.ContactNumbers)
                    {
                        companyContactNumbers.Add(new CompanyContactNumber
                        {
                            CompanyId = id,
                            Type = item.Type,
                            PhoneNumber = item.PhoneNumber
                        });
                    }

                    db.CompanyContactNumbers.AddRange(companyContactNumbers);
                    db.SaveChanges();
                }
                
            }                        
            catch (DbUpdateConcurrencyException)
            {
                if (db.Companies.Count(e => e.Id == id) > 0)
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

        
    }
}