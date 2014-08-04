using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PostSalesUnit
{
   public class Unit
   {
       [Key]
       public int Id { get; set; }

       public string UnitName { get; set; }
       public decimal BasicRate { get; set; }
       public decimal DevelopmentCharge { get; set; }
       public decimal FloorRiseRate { get; set; }
       public decimal OtherCharge { get; set; }
       public string UnitType { get; set; }       
       public string BuildingType { get; set; }       
       public decimal SaleableArea { get; set; }
       public string SaleableAreaUnit { get; set; }
       public decimal CarpetArea { get; set; }
       public string CarpetAreaUnit { get; set; }
       public string Status { get; set; }
       public int UnitNumber { get; set; }

       public int FloorNumber { get; set; }
       public int WingId { get; set; }

       [ForeignKey(name: "WingId")]
       public Wing Wing { get; set; }

       public int TowerId { get; set; }

       [ForeignKey(name: "TowerId")]
       public Tower Tower { get; set; }

       public int ProjectId { get; set; }

       [ForeignKey(name: "ProjectId")]
       public Project Project { get; set; }

   }
}
