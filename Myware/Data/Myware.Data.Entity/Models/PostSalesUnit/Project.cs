using Myware.Data.Entity.Models.PresalesUnit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PostSalesUnit
{
    [DataContract(IsReference = true)]
    public class Project : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [DataMember]
        public string ProjectId { get; set; }

        [Required]
        [StringLength(200)]
        [DataMember]
        public string ProjectName { get; set; }

        public int ProjectTypeId { get; set; }

        [ForeignKey(name: "ProjectTypeId")]
        public ProjectType ProjectType { get; set; }

        
        
        public ICollection<ProjectBankDetails> BankDetails { get; set; }        
        public ICollection<ProjectPropertyCharges> PropertyCharges { get; set; }        
        public ICollection<ProjectParkingType> Parkings { get; set; }
        public ICollection<ProjectOtherInformation> ProjectInformation { get; set; }

    }
}
