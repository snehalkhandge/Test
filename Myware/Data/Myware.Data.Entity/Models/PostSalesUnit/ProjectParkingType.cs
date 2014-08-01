using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PostSalesUnit
{
    public class ProjectParkingType
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }

        public int Count { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey(name: "ProjectId")]
        public Project Project { get; set; }

    }
}
