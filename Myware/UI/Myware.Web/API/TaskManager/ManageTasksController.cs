using Myware.Data.Entity;
using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Data.Entity.Models.UserTasks;
using Myware.Web.Models;
using Myware.Web.Models.PreSalesUnit;
using Myware.Web.Models.TaskManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace Myware.Web.API.TaskManager
{

    [Authorize]
    public class ManageTasksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ManagePermissions

        [Route("taskmanagersAssignedByMe/user/{user}/page/{page}/size/{pageSize}/search/{searchQuery}")]
        public ListTaskViewModel GetTasksAssignedByMe(int user, int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var query = db.AssignedTasks.Include(t => t.AssignedByUser)
                          .Include(t => t.AssignedToUser)
                          .Where(t => t.AssignedFromId == user)
                          .Where(t => t.IsParentTask == true)
                          .OrderByDescending(x => x.LastUpdated);



            var total = query.Count();
            var result = query.Skip(pageSize * (page - 1))
                              .Take(pageSize).ToList();


            var listResult = new ListTaskViewModel();
            listResult.TotalItems = total;
            listResult.Results = new List<TaskViewModel>();

            foreach (var item in result)
            {


                var assignedFromUser = new UserViewModel();
                var assignedToUser = new UserViewModel();

                assignedFromUser.FirstName = item.AssignedByUser.FirstName;
                assignedFromUser.LastName = item.AssignedByUser.LastName;

                assignedToUser.FirstName = item.AssignedToUser.FirstName;
                assignedToUser.LastName = item.AssignedToUser.LastName;

                listResult.Results.Add(new TaskViewModel
                {
                    Id = item.Id,
                    AssignedFromId = item.AssignedFromId,
                    AssignedToId = item.AssignedToId,
                    AssignedToUser = assignedToUser,
                    AssignedByUser = assignedFromUser,
                    Title = item.Title,
                    Description = item.Description,
                    TaskStatus = item.TaskStatus,
                    Created = DateTime.SpecifyKind(item.Created, DateTimeKind.Local),
                    ParentTaskId = item.ParentTaskId,
                    IsParentTask = item.IsParentTask,
                    LastUpdated = DateTime.SpecifyKind(item.LastUpdated, DateTimeKind.Local)

                });

            }


            return listResult;

        }

        [Route("taskmanagersAssignedToMe/user/{user}/page/{page}/size/{pageSize}/search/{searchQuery}")]
        public ListTaskViewModel GetTasksAssignedToMe(int user, int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var query = db.AssignedTasks.Include(t => t.AssignedByUser)
                          .Include(t => t.AssignedToUser)
                          .Where(t => t.AssignedToId == user)
                          .Where(t => t.IsParentTask == true)
                          .OrderByDescending(x => x.LastUpdated);



            var total = query.Count();
            var result = query.Skip(pageSize * (page - 1))
                              .Take(pageSize).ToList();


            var listResult = new ListTaskViewModel();
            listResult.TotalItems = total;
            listResult.Results = new List<TaskViewModel>();

            foreach (var item in result)
            {


                var assignedFromUser = new UserViewModel();
                var assignedToUser = new UserViewModel();

                assignedFromUser.FirstName = item.AssignedByUser.FirstName;
                assignedFromUser.LastName = item.AssignedByUser.LastName;

                assignedToUser.FirstName = item.AssignedToUser.FirstName;
                assignedToUser.LastName = item.AssignedToUser.LastName;

                listResult.Results.Add(new TaskViewModel
                {
                    Id = item.Id,
                    AssignedFromId = item.AssignedFromId,
                    AssignedToId = item.AssignedToId,
                    AssignedToUser = assignedToUser,
                    AssignedByUser = assignedFromUser,
                    Title = item.Title,
                    Description = item.Description,
                    TaskStatus = item.TaskStatus,
                    Created = DateTime.SpecifyKind(item.Created, DateTimeKind.Local),
                    ParentTaskId = item.ParentTaskId,
                    IsParentTask = item.IsParentTask,
                    LastUpdated = DateTime.SpecifyKind(item.LastUpdated, DateTimeKind.Local)

                });

            }


            return listResult;

        }

        [Route("taskmanagerById/{id}")]
        public TaskViewModel GetTaskById(int id)
        {

            var query = db.AssignedTasks
                          .Include(t => t.AssignedByUser)
                          .Include(t => t.AssignedToUser)
                          .Include(t => t.TasksRelatedFiles);


            var result = query.Where(r => r.Id == id)
                            .SingleOrDefault();

            if(result.ParentTaskId != null)
            {
                result = query.Where(r => r.Id == result.ParentTaskId)
                            .SingleOrDefault();

            }


            var files = new List<TaskRelatedFileViewModel>();

            foreach (var file in result.TasksRelatedFiles)
            {
                files.Add(new TaskRelatedFileViewModel
                {
                    FileUrl = file.FileUrl
                });
            }

            var assignedFromUser = new UserViewModel();
            var assignedToUser = new UserViewModel();

            assignedFromUser.FirstName = result.AssignedByUser.FirstName;
            assignedFromUser.LastName = result.AssignedByUser.LastName;

            assignedToUser.FirstName = result.AssignedToUser.FirstName;
            assignedToUser.LastName = result.AssignedToUser.LastName;

            return new TaskViewModel
                                {
                                    Id = result.Id,
                                    AssignedFromId = result.AssignedFromId,
                                    AssignedToId = result.AssignedToId,
                                    AssignedToUser = assignedToUser,
                                    AssignedByUser = assignedFromUser,
                                    Title = result.Title,
                                    Description = result.Description,
                                    TaskStatus = result.TaskStatus,
                                    Created = DateTime.SpecifyKind(result.Created, DateTimeKind.Local),
                                    ParentTaskId = result.ParentTaskId,
                                    IsParentTask = result.IsParentTask,
                                    LastUpdated = DateTime.SpecifyKind(result.LastUpdated, DateTimeKind.Local),                                    
                                    TasksRelatedFiles = files,
                                    TaskType = result.TaskType

                                };

        }

        [Route("task/children/{id}")]
        public ListTaskViewModel GetTaskChildrenByParentId(int id)
        {
            var query = db.AssignedTasks.Include(t => t.AssignedByUser)
                          .Include(t => t.AssignedToUser)
                          .Where(t => t.ParentTaskId == id)
                          .OrderBy(x => x.Id);



            var total = query.Count();
            var result = query.ToList();


            var listResult = new ListTaskViewModel();
            listResult.TotalItems = total;
            listResult.Results = new List<TaskViewModel>();

            foreach (var item in result)
            {

                var files = new List<TaskRelatedFileViewModel>();

                if(item.TasksRelatedFiles != null)
                {
                    foreach (var file in item.TasksRelatedFiles)
                    {
                        files.Add(new TaskRelatedFileViewModel
                        {
                            FileUrl = file.FileUrl
                        });
                    }
                }
                
                var assignedFromUser = new UserViewModel();
                var assignedToUser = new UserViewModel();

                assignedFromUser.FirstName = item.AssignedByUser.FirstName;
                assignedFromUser.LastName = item.AssignedByUser.LastName;

                assignedToUser.FirstName = item.AssignedToUser.FirstName;
                assignedToUser.LastName = item.AssignedToUser.LastName;

                listResult.Results.Add(new TaskViewModel
                {
                    Id = item.Id,
                    AssignedFromId = item.AssignedFromId,
                    AssignedToId = item.AssignedToId,
                    AssignedToUser = assignedToUser,
                    AssignedByUser = assignedFromUser,
                    Title = item.Title,
                    Description = item.Description,
                    TaskStatus = item.TaskStatus,
                    Created = DateTime.SpecifyKind(item.Created, DateTimeKind.Local),                    
                    ParentTaskId = item.ParentTaskId,
                    IsParentTask = item.IsParentTask,
                    LastUpdated = DateTime.SpecifyKind(item.LastUpdated, DateTimeKind.Local),                    
                    TasksRelatedFiles = files,
                    TaskType = item.TaskType

                });

            }


            return listResult;
        }

        [Route("saveTask/{id}")]
        [ResponseType(typeof(CreateBrokerViewModel))]
        public IHttpActionResult PostTask(int id, TaskViewModel typeVM)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (id != typeVM.Id)
            {
                return BadRequest();
            }

            var type = new AssignedTask();

            try
            {
                if (typeVM.Id == 0)
                {

                    type.AssignedFromId = typeVM.AssignedFromId;
                    type.AssignedToId = typeVM.AssignedToId;
                    type.Title = typeVM.Title;
                    type.Description = typeVM.Description;
                    type.TaskStatus = typeVM.TaskStatus;
                    type.TaskType = typeVM.TaskType;
                    type.ParentTaskId = typeVM.ParentTaskId;

                    if (typeVM.ParentTaskId == null)
                    {
                        type.IsParentTask = true;
                    }
                    else
                    {
                        type.IsParentTask = false;
                    }
                    type.UpdatedByUserId = typeVM.AssignedFromId;
                    type.Created = DateTime.UtcNow;
                    type.LastUpdated = DateTime.UtcNow;
                    db.Entry(type).State = EntityState.Added;
                    db.SaveChanges();

                    id = type.Id;
                }
                else
                {
                    var oldType = db.AssignedTasks
                                    .Where(e => e.Id == id)
                                    .SingleOrDefault();

                    oldType.AssignedFromId = typeVM.AssignedFromId;
                    oldType.AssignedToId = typeVM.AssignedToId;
                    oldType.Title = typeVM.Title;
                    oldType.Description = typeVM.Description;
                    oldType.TaskStatus = typeVM.TaskStatus;
                    oldType.ParentTaskId = typeVM.ParentTaskId;
                    oldType.TaskType = typeVM.TaskType;

                    if (typeVM.ParentTaskId == null)
                    {
                        oldType.IsParentTask = true;
                    }
                    else
                    {
                        oldType.IsParentTask = false;
                    }

                    oldType.LastUpdated = DateTime.UtcNow;

                    db.Entry(oldType).State = EntityState.Modified;

                    oldType.UpdatedByUserId = typeVM.AssignedFromId;

                    db.SaveChanges();

                }

                if(typeVM.ParentTaskId != null)
                {

                    var updateParent = db.AssignedTasks
                                    .Where(e => e.Id == typeVM.ParentTaskId)
                                    .SingleOrDefault();

                    updateParent.LastUpdated = DateTime.UtcNow;
                    updateParent.TaskStatus = typeVM.TaskStatus;

                    db.Entry(updateParent).State = EntityState.Modified;
                    db.SaveChanges();
                }



            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.AssignedTasks.Count(e => e.Id == id) > 0)
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


        

        [Route("saveTaskFile")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostBrokerImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);

            var uploadFolder = "~/Images/Tasks";
            var imageUrl = "";
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);


            var provider = new MultipartFormDataStreamProvider(root + "/Temp");
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            if (result.FormData["taskObject"] == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }


            //TODO: Do something with the json model which is currently a string


            #region File upload
            //get the files
            foreach (var fileData in result.FileData)
            {
                if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                }
                string fileName = fileData.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = Path.GetFileName(fileName);
                }

                if (File.Exists(Path.Combine(root, fileName)))
                {
                    File.Delete(Path.Combine(root, fileName));
                }

                File.Move(fileData.LocalFileName, Path.Combine(root, fileName));

                #region Update Broker
                var model = new JavaScriptSerializer().Deserialize<PartialGetId>(result.FormData["taskObject"]);

                var newEntity = new TasksRelatedFile();
                newEntity.AssignedTaskId = model.id;
                newEntity.FileUrl = "Images/Tasks/" + fileName;

                db.Entry(newEntity).State = EntityState.Added;
                db.SaveChanges();

                imageUrl = newEntity.FileUrl;
                #endregion

            }
            #endregion



            return Request.CreateResponse(HttpStatusCode.OK, imageUrl);



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