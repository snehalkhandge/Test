using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PaymentSchedule
{
    public class Installment
    {
        [Key]
        public int Id { get; set; }
        public int InstallmentNumber { get; set; }

        public string Phase { get; set; }

        public decimal PaymentPercentage { get; set; }

        public decimal ServiceTaxPercentage { get; set; }

        public int ScheduleId { get; set; }

        [ForeignKey(name: "ScheduleId")]
        public Schedule Schedule { get; set; }

    }
}
