using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Interfaces
{
    public interface IProjectTeamService
    {
        System.Threading.Tasks.Task<List<Projectteam>> GetAll();
        System.Threading.Tasks.Task<Projectteam> GetById(int id);
        System.Threading.Tasks.Task Create(Projectteam model);
        System.Threading.Tasks.Task Update(Projectteam model);
        System.Threading.Tasks.Task Delete(int id);
    }
}