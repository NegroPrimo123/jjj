using Domain.Models;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class TaskRepository : RepositoryBase<System.Threading.Tasks.Task>, ITaskRepository
    {
        public TaskRepository(Task2DbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}