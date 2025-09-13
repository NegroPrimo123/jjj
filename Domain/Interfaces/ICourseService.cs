using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICourseService
    {
        Task<List<Course>> GetAll();
        Task<Course> GetById(int id);
        System.Threading.Tasks.Task Create(Course model);
        System.Threading.Tasks.Task Update(Course model);
        System.Threading.Tasks.Task Delete(int id);
    }
}