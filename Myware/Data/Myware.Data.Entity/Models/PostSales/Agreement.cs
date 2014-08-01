using Myware.Data.Entity.Models.PostSales.Booking;
using Myware.Data.Entity.Models.PostSalesUnit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PostSales
{
    [DataContract(IsReference = true)]
    public class Agreement
    {
        [Key]
        public int Id { get; set; }

        [DataMember]
        public string AgreementNumber { get; set; }

        [DataMember]
        public string AgreementPreparedBy { get; set; }

        [DataMember]
        public Nullable<DateTime> PreparedOn { get; set; }


        [DataMember]
        public string AgreementSignedBy { get; set; }

        [DataMember]
        public Nullable<DateTime> SignedOn { get; set; }

        [DataMember]
        public Nullable<decimal> AgreementCost { get; set; }

        [DataMember]
        public Nullable<decimal> ExtraAmentiesCost { get; set; }

        [DataMember]
        public Nullable<decimal> MarketValue { get; set; }

        [DataMember]
        public Nullable<decimal> VATPercentage { get; set; }

        [DataMember]
        public Nullable<decimal> ExemptionPercentage { get; set; }

        [DataMember]
        public Nullable<decimal> ServiceTaxPercentage { get; set; }

        [DataMember]
        public Nullable<decimal> SalesTaxPercentage { get; set; }

        [DataMember]
        public Nullable<decimal> SocietyMaintenanceCharge { get; set; }


        [DataMember]
        public Nullable<DateTime> AgreementRegisteredData { get; set; }
        

        [DataMember]
        public string Remarks { get; set; }


        public int ProjectId { get; set; }

        [ForeignKey(name: "ProjectId")]
        public Project Project { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey(name: "CustomerId")]
        public BookingCustomer Customer { get; set; }

    }
}
