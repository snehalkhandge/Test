using Myware.Web.Models.PostSales.Booking;
using Myware.Web.Models.PostSalesUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSales
{
    public class AgreementViewModel
    {
        public int Id { get; set; }        
        public string AgreementNumber { get; set; }        
        public string AgreementPreparedBy { get; set; }        
        public DateTime PreparedOn { get; set; }        
        public string AgreementSignedBy { get; set; }        
        public DateTime SignedOn { get; set; }        
        public decimal AgreementCost { get; set; }        
        public decimal ExtraAmentiesCost { get; set; }        
        public decimal MarketValue { get; set; }        
        public decimal VATPercentage { get; set; }        
        public decimal ExemptionPercentage { get; set; }        
        public decimal ServiceTaxPercentage { get; set; }        
        public decimal SalesTaxPercentage { get; set; }        
        public decimal SocietyMaintenanceCharge { get; set; }        
        public DateTime AgreementRegisteredData { get; set; }        
        public string Remarks { get; set; }
        public int ProjectId { get; set; }                
        public ProjectBaseViewModel Project { get; set; }
        public int CustomerId { get; set; }
        public BookingCustomerViewModel Customer { get; set; }



    }
}