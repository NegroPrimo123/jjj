using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> GetAll();
        Task<Project> GetById(int id);
        System.Threading.Tasks.Task Create(Project model);
        System.Threading.Tasks.Task Update(Project model);
        System.Threading.Tasks.Task Delete(int id);
    }
}