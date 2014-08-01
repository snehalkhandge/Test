using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSales
{
    public class ScheduleDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<InstallmentViewModel> Installments { get; set; }
    }

    public class InstallmentViewModel
    {
        public int Id { get; set; }
        public int InstallmentNumber { get; set; }
        public string Phase { get; set; }
        public decimal PaymentPercentage { get; set; }
        public decimal ServiceTaxPercentage { get; set; }
        public int ScheduleId { get; set; }

    }
}