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
    public class PersonalContactNumber : Myware.Repository.EF.Entity
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        public int PersonalInformationId { get; set; }

        [DataMember]
        [ForeignKey("PersonalInformationId")]
        public PersonalInformation PersonalInformation { get; set; }

        [DataMember]
        [Index(IsUnique = true)]
        public long PhoneNumber { get; set; }


        [DataMember]
        public string Type { get; set; }

    }
}
