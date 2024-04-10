﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task Register(User user);
        public Task Login(User user);

    }
}