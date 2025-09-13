using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAll();
        Task<Student> GetById(int id);
        System.Threading.Tasks.Task Create(Student model);
        System.Threading.Tasks.Task Update(Student model);
        System.Threading.Tasks.Task Delete(int id);
    }
}
