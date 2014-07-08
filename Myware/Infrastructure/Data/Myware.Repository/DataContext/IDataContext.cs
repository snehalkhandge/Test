using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Repository.DataContext
{

    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState(object entity);
    }
}
