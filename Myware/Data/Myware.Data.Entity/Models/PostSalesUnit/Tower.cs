using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PostSalesUnit
{
    public class Tower 
    {
        [Key]
        public int Id { get; set; }

        public int BuildingNumber { get; set; }

        public string BuildingName { get; set; }
        public int NumberOfWings { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey(name: "ProjectId")]
        public Project Project { get; set; }

    }
}
