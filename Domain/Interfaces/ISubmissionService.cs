using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISubmissionService
    {
        Task<List<Submission>> GetAll();
        Task<Submission> GetById(int id);
        System.Threading.Tasks.Task Create(Submission model);
        System.Threading.Tasks.Task Update(Submission model);
        System.Threading.Tasks.Task Delete(int id);
    }
}