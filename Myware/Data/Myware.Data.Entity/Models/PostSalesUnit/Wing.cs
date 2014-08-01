using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PostSalesUnit
{
    public class Wing
    {

        [Key]
        public int Id { get; set; }

        public string WingNumber { get; set; }

        public string WingName { get; set; }
        public int NumberOfFloors { get; set; }

        public int TowerId { get; set; }

        [ForeignKey(name: "TowerId")]
        public Tower Tower { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey(name: "ProjectId")]
        public Project Project { get; set; }


    }
}
