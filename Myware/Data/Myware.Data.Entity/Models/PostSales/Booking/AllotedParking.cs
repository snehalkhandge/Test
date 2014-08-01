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
    public class AllotedParking
    {
        [Key]
        public int Id { get; set; }

        [DataMember]
        public string ParkingType { get; set; }

        [DataMember]
        public string ParkingSizeW { get; set; }
        [DataMember]
        public string ParkingSizeH { get; set; }

        public int BookingDetailId { get; set; }
        [ForeignKey(name: "BookingDetailId")]
        public BookingDetail BookingDetail { get; set; }
    }
}
