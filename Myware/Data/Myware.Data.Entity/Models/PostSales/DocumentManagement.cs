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
    public class DocumentManagement
    {

        [Key]
        public int Id { get; set; }

        [DataMember]
        public string FileUrl { get; set; }

        [DataMember]
        public string DocumentName { get; set; }

        [DataMember]
        public string Remark { get; set; }


        public int ProjectId { get; set; }

        [ForeignKey(name: "ProjectId")]
        public Project Project { get; set; }

        public int BookingDetailId { get; set; }

        [ForeignKey(name: "BookingDetailId")]
        public BookingDetail BookingDetail{ get; set; }


    }
}
