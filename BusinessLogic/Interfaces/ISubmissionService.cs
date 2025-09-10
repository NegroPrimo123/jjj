using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace BusinessLogic.Interfaces
{
    public interface ISubmissionService
    {
        System.Threading.Tasks.Task<List<Submission>> GetAll();
        System.Threading.Tasks.Task<Submission> GetById(int id);
        System.Threading.Tasks.Task Create(Submission model);
        System.Threading.Tasks.Task Update(Submission model);
        System.Threading.Tasks.Task Delete(int id);
    }
}