using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Interfaces
{
    public interface IGroupService
    {
        System.Threading.Tasks.Task<List<Group>> GetAll();
        System.Threading.Tasks.Task<Group> GetById(int id);
        System.Threading.Tasks.Task Create(Group model);
        System.Threading.Tasks.Task Update(Group model);
        System.Threading.Tasks.Task Delete(int id);
    }
}