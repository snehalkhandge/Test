using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PostSalesUnit
{
    public class ProjectBankDetails
    {
        [Key]
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public string BankName { get; set; }

        public string BranchName { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey(name: "ProjectId")]
        public Project Project { get; set; }


    }
}
