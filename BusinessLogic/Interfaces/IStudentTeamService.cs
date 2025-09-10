using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Interfaces
{
    public interface IStudentTeamService
    {
        System.Threading.Tasks.Task<List<Studentteam>> GetAll();
        System.Threading.Tasks.Task<Studentteam> GetById(int id);
        System.Threading.Tasks.Task Create(Studentteam model);
        System.Threading.Tasks.Task Update(Studentteam model);
        System.Threading.Tasks.Task Delete(int id);
    }
}