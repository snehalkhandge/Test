using Myware.Data.Entity.Models.PresalesUnit;
using Myware.Data.Entity.Models.UserManagement;
using Myware.Repository.Repositories;
using Myware.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Reality.Services.UserManagement
{
    public interface ITransactionTypeService : IService<TransactionType>
    {
    }

    public class TransactionTypeService : Service<TransactionType>, ITransactionTypeService
    {
        public TransactionTypeService(IRepositoryAsync<TransactionType> repository)
            : base(repository)
        {


        }
    }
}
