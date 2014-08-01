using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Myware.Data.Entity.Models.PreSales;


namespace Myware.Data.Entity.Models.PostSalesUnit
{
    [DataContract(IsReference = true)]
    public  class ProjectType : BaseEntity
    {
        
        [StringLength(50)]
        [DataMember]  
        public string Name { get; set; }

        
    }
}
