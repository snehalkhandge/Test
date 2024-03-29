﻿using Myware.Data.Entity.Models.UserManagement;
using Myware.Repository.Repositories;
using Myware.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Reality.Services.UserManagement.Permissions
{
    public interface IPermissionService : IService<Permission>
    {
    }

    public class PermissionService : Service<Permission>, IPermissionService
    {
        public PermissionService(IRepositoryAsync<Permission> repository)
            : base(repository)
        {


        }
    }
}
