using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Interfaces
{
    public interface IProjectService
    {
        System.Threading.Tasks.Task<List<Project>> GetAll();
        System.Threading.Tasks.Task<Project> GetById(int id);
        System.Threading.Tasks.Task Create(Project model);
        System.Threading.Tasks.Task Update(Project model);
        System.Threading.Tasks.Task Delete(int id);
    }
}