using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAll();
        Task<Teacher> GetById(int id);
        System.Threading.Tasks.Task Create(Teacher model);
        System.Threading.Tasks.Task Update(Teacher model);
        System.Threading.Tasks.Task Delete(int id);
    }
}