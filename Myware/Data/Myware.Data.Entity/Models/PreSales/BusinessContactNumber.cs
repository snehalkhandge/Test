using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PreSales
{
    [DataContract(IsReference = true)]
    public class BusinessContactNumber : Myware.Repository.EF.Entity
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        public int BusinessInformationId { get; set; }

        [DataMember]
        [ForeignKey("BusinessInformationId")]
        public BusinessInformation BusinessInformation { get; set; }

        [DataMember]
        [Index(IsUnique = true)]
        public long PhoneNumber { get; set; }


        [DataMember]
        public string Type { get; set; }

    }
}
