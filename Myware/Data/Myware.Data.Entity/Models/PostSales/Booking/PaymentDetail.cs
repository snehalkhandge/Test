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
    public class PaymentDetail
    {
        [Key]
        public int Id { get; set; }

        [DataMember]
        public int InstallmentNumber { get; set; }

        [DataMember]
        public string Phase { get; set; }

        [DataMember]
        public decimal Payment { get; set; }

        [DataMember]
        public decimal Tax { get; set; }

        [DataMember]
        public Nullable<DateTime> ExpectedPaymentDate { get; set; }

        [DataMember]
        public Nullable<DateTime> PaymentDate { get; set; }

        [DataMember]
        public string PaymentMode { get; set; }

        [DataMember]
        public Nullable<decimal> AmountPaid { get; set; }

        [DataMember]
        public Nullable<decimal> InterestAmount { get; set; }

        [DataMember]
        public Nullable<decimal> InterestAmountPaid { get; set; }

        [DataMember]
        public Nullable<decimal> ServiceTaxAmount { get; set; }

        [DataMember]
        public Nullable<decimal> ServiceTaxAmountPaid { get; set; }

        public int BookingDetailId { get; set; }
        [ForeignKey(name: "BookingDetailId")]
        public BookingDetail BookingDetail { get; set; }

    }
}
