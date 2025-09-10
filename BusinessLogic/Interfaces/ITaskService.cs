using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Interfaces
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task<List<System.Threading.Tasks.Task>> GetAll();
        System.Threading.Tasks.Task<System.Threading.Tasks.Task> GetById(int id);
        System.Threading.Tasks.Task Create(System.Threading.Tasks.Task model);
        System.Threading.Tasks.Task Update(System.Threading.Tasks.Task model);
        System.Threading.Tasks.Task Delete(int id);
    }
}