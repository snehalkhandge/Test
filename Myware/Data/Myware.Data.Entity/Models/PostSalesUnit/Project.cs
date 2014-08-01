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

        #region Other Information

        [StringLength(200)]
        [DataMember]
        public string PlotNumber { get; set; }

        [StringLength(200)]
        [DataMember]
        public string SurveyOrSectorNumber { get; set; }

        [StringLength(200)]
        [DataMember]
        public string Locality { get; set; }

        [StringLength(200)]
        [DataMember]
        public string City { get; set; }

        [StringLength(200)]
        [DataMember]
        public string PlotArea { get; set; }

        [StringLength(200)]
        [DataMember]
        public string PlotAreaUnit { get; set; }

        [StringLength(500)]
        [DataMember]
        public string Address { get; set; }

        
        [DataMember]
        public int FSI { get; set; }

        [DataMember]
        public int NumberOfBuilding { get; set; }

        [DataMember]
        public int NumberOfShops { get; set; }

        [DataMember]
        public int NumberOfFlats { get; set; }

        [DataMember]
        public int NumberOfOffices { get; set; }

        [StringLength(800)]
        [DataMember]
        public string Amneties { get; set; }

        
        [DataMember]
        public string FloorPlan { get; set; }

        public int DeveloperId { get; set; }

        [ForeignKey(name: "DeveloperId")]
        public Developer Developer { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(name: "CompanyId")]
        public Company Company { get; set; }
        #endregion

        public ICollection<ProjectBankDetails> BankDetails { get; set; }        
        public ICollection<ProjectPropertyCharges> PropertyCharges { get; set; }        
        public ICollection<ProjectParkingType> Parkings { get; set; }

    }
}
