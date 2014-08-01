using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PaymentSchedule
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Installment> Installments { get; set; }

    }
}
