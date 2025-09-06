using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Interfaces
{
    public interface IStudentService
    {
        System.Threading.Tasks.Task<List<Student>> GetAll();
        System.Threading.Tasks.Task<Student> GetById(int id);
        System.Threading.Tasks.Task Create(Student model);
        System.Threading.Tasks.Task Update(Student model);
        System.Threading.Tasks.Task Delete(int id);
    }
}
