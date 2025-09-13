using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStudentTeamService
    {
        Task<List<Studentteam>> GetAll();
        Task<Studentteam> GetById(int id);
        System.Threading.Tasks.Task Create(Studentteam model);
        System.Threading.Tasks.Task Update(Studentteam model);
        System.Threading.Tasks.Task Delete(int id);
    }
}