using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProjectTeamService
    {
        Task<List<Projectteam>> GetAll();
        Task<Projectteam> GetById(int id);
        System.Threading.Tasks.Task Create(Projectteam model);
        System.Threading.Tasks.Task Update(Projectteam model);
        System.Threading.Tasks.Task Delete(int id);
    }
}