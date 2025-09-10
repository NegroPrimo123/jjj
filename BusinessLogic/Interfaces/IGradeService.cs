using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Interfaces
{
    public interface IGradeService
    {
        System.Threading.Tasks.Task<List<Grade>> GetAll();
        System.Threading.Tasks.Task<Grade> GetById(int id);
        System.Threading.Tasks.Task Create(Grade model);
        System.Threading.Tasks.Task Update(Grade model);
        System.Threading.Tasks.Task Delete(int id);
    }
}