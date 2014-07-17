using Myware.Data.Entity;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Web.Models;
using Myware.Web.Models.PreSalesUnit;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Myware.Web.API.UserManagement
{
    [Authorize]
    public class ManageCampaignController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ManagePermissions

        [Route("source/{page}/size/{pageSize}/search/{searchQuery}")]
        public ListCampaignViewModel GetCampaigns(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var parent = db.Campaigns.Where(t => t.IsParentCampaign == true)
                           .OrderByDescending(x => x.Id)
                           .Select(t => new CampaignViewModel { 
                                Id = t.Id,
                                Name = t.Name,
                                ParentCampaignId = t.ParentCampaignId
                            });

            var total = parent.Count();

            var parentItems = parent.Skip(pageSize * (page - 1)).Take(pageSize).ToList();

            foreach (var item in parentItems)
            {
                item.Children = db.Campaigns.Where(t => t.ParentCampaignId == item.Id)
                                   .OrderByDescending(x => x.Id)
                                   .Select(t => new CampaignViewModel { 
                                        Id = t.Id,
                                        Name = t.Name,
                                        ParentCampaignId = t.ParentCampaignId
                                    })
                                    .ToList();
            }


            return new ListCampaignViewModel
            {

                TotalItems = total,
                Results = parentItems

            };

        }

        [Route("source/parent/all")]
        public ListCampaignViewModel GetAllParentCampaign()
        {
            var parent = db.Campaigns.Where(t => t.IsParentCampaign == true)
                           .OrderByDescending(x => x.Id)
                           .Select(t => new CampaignViewModel
                           {
                               Id = t.Id,
                               Name = t.Name,
                               ParentCampaignId = t.ParentCampaignId
                           });

            return new ListCampaignViewModel
            {

                TotalItems = 0,
                Results = parent.ToList()

            };

        }


        [Route("saveSource/{id}")]
        [ResponseType(typeof(CreateCampaignViewModel))]
        public IHttpActionResult PostCampaign(int id, CreateCampaignViewModel typeVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeVM.Id)
            {
                return BadRequest();
            }
            
            if (typeVM.Id == 0)
            {
                var type = new Campaign();
                type.Name = typeVM.Name;
                type.UpdatedByUserId = typeVM.UserId;

                if(typeVM.ParentCampaignId != 0)
                {
                    type.ParentCampaignId = typeVM.ParentCampaignId;
                    type.IsParentCampaign = false;
                }
                else
                {
                    type.IsParentCampaign = true;
                }
                


                db.Entry(type).State = EntityState.Added;
            }
            else
            {
                var type = db.Campaigns.Where(e => e.Id == id).SingleOrDefault();

                type.Name = typeVM.Name;
                type.UpdatedByUserId = typeVM.UserId;

                if (typeVM.ParentCampaignId != 0)
                {
                    type.ParentCampaignId = typeVM.ParentCampaignId;
                    type.IsParentCampaign = false;
                }
                else
                {
                    type.IsParentCampaign = true;
                }
                db.Entry(type).State = EntityState.Modified;    
            }
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Localities.Count(e => e.Id == id) > 0)
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