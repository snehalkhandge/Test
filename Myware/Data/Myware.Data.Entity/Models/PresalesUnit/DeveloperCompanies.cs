using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PresalesUnit
{
    [DataContract(IsReference = true)]
    public class DeveloperCompanies : Myware.Repository.EF.Entity
    {

        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        public int DeveloperId { get; set; }

        [DataMember]
        [ForeignKey("DeveloperId")]
        public Developer Developer { get; set; }


        [DataMember]
        public int CompanyId { get; set; }


        [DataMember]
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }


    }
}
