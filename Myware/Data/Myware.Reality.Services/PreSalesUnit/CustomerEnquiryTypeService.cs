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
    public interface ICustomerEnquiryTypeService : IService<CustomerEnquiryType>
    {
    }

    public class CustomerEnquiryTypeService : Service<CustomerEnquiryType>, ICustomerEnquiryTypeService
    {
        public CustomerEnquiryTypeService(IRepositoryAsync<CustomerEnquiryType> repository)
            : base(repository)
        {


        }
    }
}
