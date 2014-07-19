using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.TaskManagement
{
    public class ListTaskViewModel : BaseViewModel
    {
        public List<TaskViewModel> Results { get; set; }
    }
    public class TaskViewModel
    {

        public int Id { get; set; }

        public int AssignedFromId { get; set; }


        public UserViewModel AssignedByUser { get; set; }


        public int AssignedToId { get; set; }

        public UserViewModel AssignedToUser { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string TaskStatus { get; set; }

        public DateTime Created { get; set; }

        public List<TaskRelatedFileViewModel> TasksRelatedFiles { get; set; }

        public bool? IsParentTask { get; set; }

        public int? ParentTaskId { get; set; }

        public DateTime LastUpdated { get; set; }

        public string TaskType { get; set; }
     
   
    }

    public class UserViewModel
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class TaskRelatedFileViewModel
    {
        public int Id { get; set; }

        public string FileUrl { get; set; }
    }


}