using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PostSalesUnit
{
    public class HotProperty
    {
        [Key]
        public int Id { get; set; }
        public int UnitId{get;set;}

        [ForeignKey(name: "UnitId")]
        public Unit Unit { get; set; }

    }
}
