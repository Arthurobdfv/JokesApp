using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JokesWebApp.DAL.Repositories.GenericRepository
{
    public interface IGenericRepository<T, K> where T : class
    {
        K Context { get; }
        IQueryable<T> GetAll();
        T GetById(object id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> prediate);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
