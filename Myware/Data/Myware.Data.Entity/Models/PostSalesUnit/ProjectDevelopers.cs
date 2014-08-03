using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PostSalesUnit
{
    public class ProjectDevelopers
    {
        [Key]
        public int Id{get;set;}
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }
        public int ProjectOtherInformationId { get; set; }

        [ForeignKey(name: "ProjectOtherInformationId")]
        public ProjectOtherInformation ProjectOtherInformation { get; set; }
    }
}
