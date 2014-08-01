using Myware.Data.Entity.Models.PostSalesUnit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PostSales.Booking
{
    [DataContract(IsReference = true)]
    public class BookingDetail : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        [DataMember]
        public string ReferenceNumber { get; set; }

        [DataMember]
        public string BookingNumber { get; set; }

        [DataMember]
        public Nullable<DateTime> BookingDate { get; set; }

        [DataMember]
        public Nullable<decimal> SaleableArea { get; set; }

        [DataMember]
        public Nullable<decimal> CarpetArea { get; set; }

        [DataMember]
        public Nullable<decimal> BasicRate { get; set; }

        [DataMember]
        public Nullable<decimal> FloorRiseRate { get; set; }

        [DataMember]
        public Nullable<decimal> DevelopmentCharge { get; set; }

        [DataMember]
        public Nullable<decimal> ParkingCharge { get; set; }

        [DataMember]
        public string OtherChargeName { get; set; }

        [DataMember]
        public Nullable<decimal> OtherCharge { get; set; }

        
        [DataMember]
        public Nullable<decimal> TotalAgreementCost { get; set; }

        [DataMember]
        public Nullable<decimal> MaintenanceCharge { get; set; }

        [DataMember]
        public Nullable<decimal> LegalCharge { get; set; }

        [DataMember]
        public Nullable<decimal> SecurityCharge { get; set; }

        [DataMember]
        public Nullable<decimal> SocietyCharge { get; set; }

        [DataMember]
        public Nullable<decimal> MSEBCharge { get; set; }

        [DataMember]
        public Nullable<decimal> ClubCharge { get; set; }

        [DataMember]
        public Nullable<decimal> MiscellaneousCharge { get; set; }

        [DataMember]
        public Nullable<decimal> RegistrationCharge { get; set; }

        [DataMember]
        public Nullable<decimal> VatPercentage { get; set; }

        [DataMember]
        public Nullable<decimal> Vat { get; set; }

        [DataMember]
        public Nullable<decimal> ServiceTaxPercentage { get; set; }

        [DataMember]
        public Nullable<decimal> ServiceTax { get; set; }

        [DataMember]
        public Nullable<decimal> ValueAsGovernmentPercentage { get; set; }

        [DataMember]
        public Nullable<decimal> ValueAsGovernment { get; set; }

        [DataMember]
        public Nullable<decimal> TotalCost { get; set; }

        [DataMember]
        public Nullable<DateTime> ChequeDate { get; set; }
        [DataMember]
        public Nullable<DateTime> ReceiptDate { get; set; }

        [DataMember]
        public string ReceiptNumber { get; set; }

        [DataMember]
        public string ChequeNumber { get; set; }

        [DataMember]
        public Nullable<decimal> BookingAmount { get; set; }


        [DataMember]
        public string DrawnOnBank { get; set; }

        [DataMember]
        public string Branch { get; set; }

        [DataMember]
        public string ReferenceBy { get; set; }

        public Nullable<int> ProjectId { get; set; }

        [ForeignKey(name: "ProjectId")]
        public virtual Project Project { get; set; }

        public Nullable<int> TowerId { get; set; }

        [ForeignKey(name: "TowerId")]
        public virtual Tower Tower { get; set; }

        public Nullable<int> WingId { get; set; }

        [ForeignKey(name: "WingId")]
        public virtual  Wing Wing { get; set; }

        public Nullable<int> UnitId { get; set; }

        [ForeignKey(name: "UnitId")]
        public virtual Unit Unit { get; set; }

        public ICollection<BookingCustomer> Customers { get; set; }
        public ICollection<AllotedParking> AllotedParkings { get; set; }

        public ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
