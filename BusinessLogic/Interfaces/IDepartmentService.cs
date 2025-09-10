using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Interfaces
{
    public interface IDepartmentService
    {
        System.Threading.Tasks.Task<List<Department>> GetAll();
        System.Threading.Tasks.Task<Department> GetById(int id);
        System.Threading.Tasks.Task Create(Department model);
        System.Threading.Tasks.Task Update(Department model);
        System.Threading.Tasks.Task Delete(int id);
    }
}