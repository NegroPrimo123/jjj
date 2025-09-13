using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITaskService
    {
        Task<List<System.Threading.Tasks.Task>> GetAll();
        Task<System.Threading.Tasks.Task> GetById(int id);
        System.Threading.Tasks.Task Create(System.Threading.Tasks.Task model);
        System.Threading.Tasks.Task Update(System.Threading.Tasks.Task model);
        System.Threading.Tasks.Task Delete(int id);
        System.Threading.Tasks.Task Create(Models.Task taskDto);
        System.Threading.Tasks.Task Update(Models.Task taskDto);
    }
}