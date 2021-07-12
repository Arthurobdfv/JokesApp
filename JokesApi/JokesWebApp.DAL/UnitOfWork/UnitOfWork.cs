using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokesWebApp.DAL.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        public TContext Context { get; }

        private string _errorMessage = string.Empty;
        private IDbContextTransaction _objTran;

        public UnitOfWork(TContext ctx)
        {
            Context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public void Commit()
        {
            _objTran.Commit();
        }

        public void CreateTransaction()
        {
            _objTran = Context.Database.BeginTransaction();
        }

        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
