﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<List<Group>> GetAllWithEducationName();
        //Task<Group> UpdateGroupAsync(int? id, Group group);
        public Task UpdateGroup(Group group);

    }
}
