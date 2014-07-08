using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Repository.Infrastructure
{
    public enum ObjectState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}
