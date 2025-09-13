using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAll();
        Task<Department> GetById(int id);
        System.Threading.Tasks.Task Create(Department model);
        System.Threading.Tasks.Task Update(Department model);
        System.Threading.Tasks.Task Delete(int id);
    }
}