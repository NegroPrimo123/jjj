using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Interfaces
{
    public interface ITeacherService
    {
        System.Threading.Tasks.Task<List<Teacher>> GetAll();
        System.Threading.Tasks.Task<Teacher> GetById(int id);
        System.Threading.Tasks.Task Create(Teacher model);
        System.Threading.Tasks.Task Update(Teacher model);
        System.Threading.Tasks.Task Delete(int id);
    }
}