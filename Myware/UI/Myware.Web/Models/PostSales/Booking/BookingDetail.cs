using Myware.Web.Models.PostSalesUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSales.Booking
{
    public class BookingDetailViewModel
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }        
        public string BookingNumber { get; set; }        
        public DateTime BookingDate { get; set; }        
        public decimal SaleableArea { get; set; }        
        public decimal CarpetArea { get; set; }        
        public decimal BasicRate { get; set; }        
        public decimal FloorRiseRate { get; set; }        
        public decimal DevelopmentCharge { get; set; }        
        public decimal ParkingCharge { get; set; }
        public string OtherChargeName { get; set; }        
        public decimal OtherCharge { get; set; }        
        public decimal TotalAgreementCost { get; set; }        
        public decimal MaintenanceCharge { get; set; }        
        public decimal LegalCharge { get; set; }        
        public decimal SecurityCharge { get; set; }        
        public decimal SocietyCharge { get; set; }        
        public decimal MSEBCharge { get; set; }        
        public decimal ClubCharge { get; set; }
        public decimal MiscellaneousCharge { get; set; }
        public decimal RegistrationCharge { get; set; }
        public decimal VatPercentage { get; set; }
        public decimal Vat { get; set; }
        public decimal ServiceTaxPercentage { get; set; }
        public decimal ServiceTax { get; set; }
        public decimal ValueAsGovernmentPercentage { get; set; }
        public decimal ValueAsGovernment { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime ChequeDate { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string ReceiptNumber { get; set; }
        public string ChequeNumber { get; set; }
        public decimal BookingAmount { get; set; }
        public string DrawnOnBank { get; set; }
        public string Branch { get; set; }
        public string ReferenceBy { get; set; }
        public int ProjectId { get; set; }
        public ProjectBaseViewModel Project { get; set; }
        public int TowerId { get; set; }        
        public TowerDetailViewModel Tower { get; set; }
        public int WingId { get; set; }        
        public WingDetailViewModel Wing { get; set; }
        public int UnitId { get; set; }        
        public UnitViewModel Unit { get; set; }
        public List<BookingCustomerViewModel> Customers { get; set; }
        public List<AllotedParkingViewModel> AllotedParkings { get; set; }
        public List<PaymentDetailViewModel> PaymentDetails { get; set; }


    }

    public class BookingCustomerViewModel
    {   
        public int Id { get; set; }        
        public string ImageUrl { get; set; }        
        public string Salutation { get; set; }        
        public string CustomerName { get; set; }        
        public DateTime DateOfBirth { get; set; }        
        public decimal Age { get; set; }        
        public string Email { get; set; }        
        public string MobileNumber { get; set; }        
        public string PhoneNumber { get; set; }        
        public string Son_Wife_Daughter_Of { get; set; }        
        public string PanNumber { get; set; }        
        public string Address { get; set; }        
        public string Nationality { get; set; }
    }

    public class AllotedParkingViewModel
    {
        public int Id { get; set; }
        public string ParkingType { get; set; }        
        public string ParkingSizeW { get; set; }        
        public string ParkingSizeH { get; set; }
        public int BookingDetailId { get; set; }
    }

    public class PaymentDetailViewModel
    {
        public int Id { get; set; }                
        public int InstallmentNumber { get; set; }        
        public string Phase { get; set; }        
        public decimal Payment { get; set; }
        public decimal Tax { get; set; }
        public DateTime ExpectedPaymentDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal InterestAmountPaid { get; set; }
        public decimal ServiceTaxAmount { get; set; }
        public decimal ServiceTaxAmountPaid { get; set; }
        public int BookingDetailId { get; set; }

    }


}