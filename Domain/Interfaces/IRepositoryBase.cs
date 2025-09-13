using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> FindAll();
        Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression);
        System.Threading.Tasks.Task Create(T entity);
        System.Threading.Tasks.Task Update(T entity);
        System.Threading.Tasks.Task Delete(T entity);
    }
}