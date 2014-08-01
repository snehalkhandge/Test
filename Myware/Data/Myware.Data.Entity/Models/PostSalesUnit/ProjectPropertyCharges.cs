using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PostSalesUnit
{
    public class ProjectPropertyCharges
    {
        [Key]
        public int Id { get; set; }

        public decimal DevelopmentCharge { get; set; }

        public decimal OtherCharge { get; set; }
        public decimal LumpSum { get; set; }
        public decimal BasicQuateRate { get; set; }
        public decimal FloorRiseRate { get; set; }
        public decimal FloorNumberOnWord { get; set; }
        public decimal PenaltyDefaulter { get; set; }
        public int GracePeriod { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey(name: "ProjectId")]
        public Project Project { get; set; }

    }
}
