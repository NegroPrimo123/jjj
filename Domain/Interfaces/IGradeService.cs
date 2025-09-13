using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGradeService
    {
        Task<List<Grade>> GetAll();
        Task<Grade> GetById(int id);
        System.Threading.Tasks.Task Create(Grade model);
        System.Threading.Tasks.Task Update(Grade model);
        System.Threading.Tasks.Task Delete(int id);
    }
}