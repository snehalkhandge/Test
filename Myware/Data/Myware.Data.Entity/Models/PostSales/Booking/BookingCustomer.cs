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
    public class BookingCustomer
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public string Salutation { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public Nullable<DateTime> DateOfBirth { get; set; }

        [DataMember]
        public Nullable<decimal> Age { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string MobileNumber { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string Son_Wife_Daughter_Of { get; set; }

        [DataMember]
        public string PanNumber { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Nationality { get; set; }

        public int BookingDetailId { get; set; }
        [ForeignKey(name: "BookingDetailId")]
        public BookingDetail BookingDetail { get; set; }
    }
}
