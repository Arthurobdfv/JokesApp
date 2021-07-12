using JokesWebApp.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JokesWebApp.DAL.Repositories.GenericRepository
{
    public class GenericRepository<T, K> : IGenericRepository<T, K> where T : class where K : DbContext
    {
        public K Context { get; set; }
        private DbSet<T> _entities;
        private string _errors = string.Empty;
        private bool _isDisposed;

        public GenericRepository(IUnitOfWork<K> uow)
        {
            Context = uow.Context;
            _entities = Context.Set<T>();
            _isDisposed = false;
        }

        protected virtual DbSet<T> Entities
        {
            get { return _entities ?? (_entities = Context.Set<T>()); }
        }

        public void Delete(T obj)
        {
            try
            {
                if (obj == null)
                    throw new ArgumentNullException("Can't delete empty entity");
                if (Context == null || _isDisposed)
                    throw new ArgumentNullException("Can't delete from null or disposed Context");
                Entities.Remove(obj);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = Entities.Where(predicate);
            return query;
        }

        public IQueryable<T> GetAll()
        {
            return Entities;
        }

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(T obj)
        {
            try
            {
                if (obj == null)
                    throw new ArgumentException("Can't insert empty entity");
                Entities.Add(obj);
                if (Context == null || _isDisposed)
                    throw new ArgumentNullException("Can't work on not initialized or disposed context");
            }
            catch
            {
                throw;
            }
        }

        public void Update(T obj)
        {
            try
            {
                if (obj == null)
                    throw new ArgumentNullException("No entity to update");
                if (Context == null || _isDisposed)
                    throw new ArgumentNullException("Can't work on not initialized or disposed context");
            }
            catch
            {
                throw;
            }
        }
    }
}
