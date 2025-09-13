using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGroupService
    {
        Task<List<Group>> GetAll();
        Task<Group> GetById(int id);
        System.Threading.Tasks.Task Create(Group model);
        System.Threading.Tasks.Task Update(Group model);
        System.Threading.Tasks.Task Delete(int id);
    }
}