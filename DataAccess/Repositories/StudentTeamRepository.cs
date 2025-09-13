using Domain.Models;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class StudentTeamRepository : RepositoryBase<Studentteam>, IStudentTeamRepository
    {
        public StudentTeamRepository(Task2DbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}